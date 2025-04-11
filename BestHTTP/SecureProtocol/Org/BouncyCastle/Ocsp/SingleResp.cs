using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002ED RID: 749
	public class SingleResp : X509ExtensionBase
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x000D3238 File Offset: 0x000D1438
		public SingleResp(SingleResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000D3247 File Offset: 0x000D1447
		public CertificateID GetCertID()
		{
			return new CertificateID(this.resp.CertId);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000D325C File Offset: 0x000D145C
		public object GetCertStatus()
		{
			CertStatus certStatus = this.resp.CertStatus;
			if (certStatus.TagNo == 0)
			{
				return null;
			}
			if (certStatus.TagNo == 1)
			{
				return new RevokedStatus(RevokedInfo.GetInstance(certStatus.Status));
			}
			return new UnknownStatus();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000D329E File Offset: 0x000D149E
		public DateTime ThisUpdate
		{
			get
			{
				return this.resp.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000D32B0 File Offset: 0x000D14B0
		public DateTimeObject NextUpdate
		{
			get
			{
				if (this.resp.NextUpdate != null)
				{
					return new DateTimeObject(this.resp.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x000D32D6 File Offset: 0x000D14D6
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.resp.SingleExtensions;
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000D32E3 File Offset: 0x000D14E3
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleExtensions;
		}

		// Token: 0x040017D9 RID: 6105
		internal readonly SingleResponse resp;
	}
}
