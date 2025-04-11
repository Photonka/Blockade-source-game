using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F2 RID: 1010
	public class CertificateUrl
	{
		// Token: 0x06002925 RID: 10533 RVA: 0x001101AC File Offset: 0x0010E3AC
		public CertificateUrl(byte type, IList urlAndHashList)
		{
			if (!CertChainType.IsValid(type))
			{
				throw new ArgumentException("not a valid CertChainType value", "type");
			}
			if (urlAndHashList == null || urlAndHashList.Count < 1)
			{
				throw new ArgumentException("must have length > 0", "urlAndHashList");
			}
			this.mType = type;
			this.mUrlAndHashList = urlAndHashList;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x00110201 File Offset: 0x0010E401
		public virtual byte Type
		{
			get
			{
				return this.mType;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x00110209 File Offset: 0x0010E409
		public virtual IList UrlAndHashList
		{
			get
			{
				return this.mUrlAndHashList;
			}
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x00110214 File Offset: 0x0010E414
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mType, output);
			CertificateUrl.ListBuffer16 listBuffer = new CertificateUrl.ListBuffer16();
			foreach (object obj in this.mUrlAndHashList)
			{
				((UrlAndHash)obj).Encode(listBuffer);
			}
			listBuffer.EncodeTo(output);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00110284 File Offset: 0x0010E484
		public static CertificateUrl parse(TlsContext context, Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!CertChainType.IsValid(b))
			{
				throw new TlsFatalAlert(50);
			}
			int num = TlsUtilities.ReadUint16(input);
			if (num < 1)
			{
				throw new TlsFatalAlert(50);
			}
			MemoryStream memoryStream = new MemoryStream(TlsUtilities.ReadFully(num, input), false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				UrlAndHash value = UrlAndHash.Parse(context, memoryStream);
				list.Add(value);
			}
			return new CertificateUrl(b, list);
		}

		// Token: 0x04001A38 RID: 6712
		protected readonly byte mType;

		// Token: 0x04001A39 RID: 6713
		protected readonly IList mUrlAndHashList;

		// Token: 0x02000916 RID: 2326
		internal class ListBuffer16 : MemoryStream
		{
			// Token: 0x06004E04 RID: 19972 RVA: 0x001B2F71 File Offset: 0x001B1171
			internal ListBuffer16()
			{
				TlsUtilities.WriteUint16(0, this);
			}

			// Token: 0x06004E05 RID: 19973 RVA: 0x001B2F80 File Offset: 0x001B1180
			internal void EncodeTo(Stream output)
			{
				long num = this.Length - 2L;
				TlsUtilities.CheckUint16(num);
				this.Position = 0L;
				TlsUtilities.WriteUint16((int)num, this);
				Streams.WriteBufTo(this, output);
				Platform.Dispose(this);
			}
		}
	}
}
