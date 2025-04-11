using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003ED RID: 1005
	public class CertificateRequest
	{
		// Token: 0x0600290F RID: 10511 RVA: 0x0010FDB8 File Offset: 0x0010DFB8
		public CertificateRequest(byte[] certificateTypes, IList supportedSignatureAlgorithms, IList certificateAuthorities)
		{
			this.mCertificateTypes = certificateTypes;
			this.mSupportedSignatureAlgorithms = supportedSignatureAlgorithms;
			this.mCertificateAuthorities = certificateAuthorities;
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x0010FDD5 File Offset: 0x0010DFD5
		public virtual byte[] CertificateTypes
		{
			get
			{
				return this.mCertificateTypes;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x0010FDDD File Offset: 0x0010DFDD
		public virtual IList SupportedSignatureAlgorithms
		{
			get
			{
				return this.mSupportedSignatureAlgorithms;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x0010FDE5 File Offset: 0x0010DFE5
		public virtual IList CertificateAuthorities
		{
			get
			{
				return this.mCertificateAuthorities;
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0010FDF0 File Offset: 0x0010DFF0
		public virtual void Encode(Stream output)
		{
			if (this.mCertificateTypes == null || this.mCertificateTypes.Length == 0)
			{
				TlsUtilities.WriteUint8(0, output);
			}
			else
			{
				TlsUtilities.WriteUint8ArrayWithUint8Length(this.mCertificateTypes, output);
			}
			if (this.mSupportedSignatureAlgorithms != null)
			{
				TlsUtilities.EncodeSupportedSignatureAlgorithms(this.mSupportedSignatureAlgorithms, false, output);
			}
			if (this.mCertificateAuthorities == null || this.mCertificateAuthorities.Count < 1)
			{
				TlsUtilities.WriteUint16(0, output);
				return;
			}
			IList list = Platform.CreateArrayList(this.mCertificateAuthorities.Count);
			int num = 0;
			foreach (object obj in this.mCertificateAuthorities)
			{
				byte[] encoded = ((Asn1Encodable)obj).GetEncoded("DER");
				list.Add(encoded);
				num += encoded.Length + 2;
			}
			TlsUtilities.CheckUint16(num);
			TlsUtilities.WriteUint16(num, output);
			foreach (object obj2 in list)
			{
				TlsUtilities.WriteOpaque16((byte[])obj2, output);
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0010FF1C File Offset: 0x0010E11C
		public static CertificateRequest Parse(TlsContext context, Stream input)
		{
			int num = (int)TlsUtilities.ReadUint8(input);
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = TlsUtilities.ReadUint8(input);
			}
			IList supportedSignatureAlgorithms = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				supportedSignatureAlgorithms = TlsUtilities.ParseSupportedSignatureAlgorithms(false, input);
			}
			IList list = Platform.CreateArrayList();
			MemoryStream memoryStream = new MemoryStream(TlsUtilities.ReadOpaque16(input), false);
			while (memoryStream.Position < memoryStream.Length)
			{
				Asn1Object obj = TlsUtilities.ReadDerObject(TlsUtilities.ReadOpaque16(memoryStream));
				list.Add(X509Name.GetInstance(obj));
			}
			return new CertificateRequest(array, supportedSignatureAlgorithms, list);
		}

		// Token: 0x04001A2D RID: 6701
		protected readonly byte[] mCertificateTypes;

		// Token: 0x04001A2E RID: 6702
		protected readonly IList mSupportedSignatureAlgorithms;

		// Token: 0x04001A2F RID: 6703
		protected readonly IList mCertificateAuthorities;
	}
}
