using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003EE RID: 1006
	public class CertificateStatus
	{
		// Token: 0x06002915 RID: 10517 RVA: 0x0010FFAD File Offset: 0x0010E1AD
		public CertificateStatus(byte statusType, object response)
		{
			if (!CertificateStatus.IsCorrectType(statusType, response))
			{
				throw new ArgumentException("not an instance of the correct type", "response");
			}
			this.mStatusType = statusType;
			this.mResponse = response;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0010FFDC File Offset: 0x0010E1DC
		public virtual byte StatusType
		{
			get
			{
				return this.mStatusType;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x0010FFE4 File Offset: 0x0010E1E4
		public virtual object Response
		{
			get
			{
				return this.mResponse;
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0010FFEC File Offset: 0x0010E1EC
		public virtual OcspResponse GetOcspResponse()
		{
			if (!CertificateStatus.IsCorrectType(1, this.mResponse))
			{
				throw new InvalidOperationException("'response' is not an OcspResponse");
			}
			return (OcspResponse)this.mResponse;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00110014 File Offset: 0x0010E214
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mStatusType, output);
			byte b = this.mStatusType;
			if (b == 1)
			{
				TlsUtilities.WriteOpaque24(((OcspResponse)this.mResponse).GetEncoded("DER"), output);
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0011005C File Offset: 0x0010E25C
		public static CertificateStatus Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (b == 1)
			{
				object instance = OcspResponse.GetInstance(TlsUtilities.ReadDerObject(TlsUtilities.ReadOpaque24(input)));
				return new CertificateStatus(b, instance);
			}
			throw new TlsFatalAlert(50);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00110096 File Offset: 0x0010E296
		protected static bool IsCorrectType(byte statusType, object response)
		{
			if (statusType == 1)
			{
				return response is OcspResponse;
			}
			throw new ArgumentException("unsupported CertificateStatusType", "statusType");
		}

		// Token: 0x04001A30 RID: 6704
		protected readonly byte mStatusType;

		// Token: 0x04001A31 RID: 6705
		protected readonly object mResponse;
	}
}
