using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000426 RID: 1062
	public class OcspStatusRequest
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x00114BE4 File Offset: 0x00112DE4
		public OcspStatusRequest(IList responderIDList, X509Extensions requestExtensions)
		{
			this.mResponderIDList = responderIDList;
			this.mRequestExtensions = requestExtensions;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x00114BFA File Offset: 0x00112DFA
		public virtual IList ResponderIDList
		{
			get
			{
				return this.mResponderIDList;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x00114C02 File Offset: 0x00112E02
		public virtual X509Extensions RequestExtensions
		{
			get
			{
				return this.mRequestExtensions;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00114C0C File Offset: 0x00112E0C
		public virtual void Encode(Stream output)
		{
			if (this.mResponderIDList == null || this.mResponderIDList.Count < 1)
			{
				TlsUtilities.WriteUint16(0, output);
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int i = 0; i < this.mResponderIDList.Count; i++)
				{
					TlsUtilities.WriteOpaque16(((ResponderID)this.mResponderIDList[i]).GetEncoded("DER"), memoryStream);
				}
				TlsUtilities.CheckUint16(memoryStream.Length);
				TlsUtilities.WriteUint16((int)memoryStream.Length, output);
				Streams.WriteBufTo(memoryStream, output);
			}
			if (this.mRequestExtensions == null)
			{
				TlsUtilities.WriteUint16(0, output);
				return;
			}
			byte[] encoded = this.mRequestExtensions.GetEncoded("DER");
			TlsUtilities.CheckUint16(encoded.Length);
			TlsUtilities.WriteUint16(encoded.Length, output);
			output.Write(encoded, 0, encoded.Length);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00114CD4 File Offset: 0x00112ED4
		public static OcspStatusRequest Parse(Stream input)
		{
			IList list = Platform.CreateArrayList();
			int num = TlsUtilities.ReadUint16(input);
			if (num > 0)
			{
				MemoryStream memoryStream = new MemoryStream(TlsUtilities.ReadFully(num, input), false);
				do
				{
					ResponderID instance = ResponderID.GetInstance(TlsUtilities.ReadDerObject(TlsUtilities.ReadOpaque16(memoryStream)));
					list.Add(instance);
				}
				while (memoryStream.Position < memoryStream.Length);
			}
			X509Extensions requestExtensions = null;
			int num2 = TlsUtilities.ReadUint16(input);
			if (num2 > 0)
			{
				requestExtensions = X509Extensions.GetInstance(TlsUtilities.ReadDerObject(TlsUtilities.ReadFully(num2, input)));
			}
			return new OcspStatusRequest(list, requestExtensions);
		}

		// Token: 0x04001C62 RID: 7266
		protected readonly IList mResponderIDList;

		// Token: 0x04001C63 RID: 7267
		protected readonly X509Extensions mRequestExtensions;
	}
}
