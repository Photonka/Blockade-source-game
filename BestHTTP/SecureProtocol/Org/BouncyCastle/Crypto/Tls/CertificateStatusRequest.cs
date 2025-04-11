using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003EF RID: 1007
	public class CertificateStatusRequest
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x001100B5 File Offset: 0x0010E2B5
		public CertificateStatusRequest(byte statusType, object request)
		{
			if (!CertificateStatusRequest.IsCorrectType(statusType, request))
			{
				throw new ArgumentException("not an instance of the correct type", "request");
			}
			this.mStatusType = statusType;
			this.mRequest = request;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x001100E4 File Offset: 0x0010E2E4
		public virtual byte StatusType
		{
			get
			{
				return this.mStatusType;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x001100EC File Offset: 0x0010E2EC
		public virtual object Request
		{
			get
			{
				return this.mRequest;
			}
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x001100F4 File Offset: 0x0010E2F4
		public virtual OcspStatusRequest GetOcspStatusRequest()
		{
			if (!CertificateStatusRequest.IsCorrectType(1, this.mRequest))
			{
				throw new InvalidOperationException("'request' is not an OCSPStatusRequest");
			}
			return (OcspStatusRequest)this.mRequest;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0011011C File Offset: 0x0010E31C
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mStatusType, output);
			byte b = this.mStatusType;
			if (b == 1)
			{
				((OcspStatusRequest)this.mRequest).Encode(output);
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0011015C File Offset: 0x0010E35C
		public static CertificateStatusRequest Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (b == 1)
			{
				object request = OcspStatusRequest.Parse(input);
				return new CertificateStatusRequest(b, request);
			}
			throw new TlsFatalAlert(50);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0011018C File Offset: 0x0010E38C
		protected static bool IsCorrectType(byte statusType, object request)
		{
			if (statusType == 1)
			{
				return request is OcspStatusRequest;
			}
			throw new ArgumentException("unsupported CertificateStatusType", "statusType");
		}

		// Token: 0x04001A32 RID: 6706
		protected readonly byte mStatusType;

		// Token: 0x04001A33 RID: 6707
		protected readonly object mRequest;
	}
}
