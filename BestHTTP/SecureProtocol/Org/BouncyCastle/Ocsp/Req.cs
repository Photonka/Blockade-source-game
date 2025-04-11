using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E9 RID: 745
	public class Req : X509ExtensionBase
	{
		// Token: 0x06001B69 RID: 7017 RVA: 0x000D3009 File Offset: 0x000D1209
		public Req(Request req)
		{
			this.req = req;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000D3018 File Offset: 0x000D1218
		public CertificateID GetCertID()
		{
			return new CertificateID(this.req.ReqCert);
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000D302A File Offset: 0x000D122A
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.req.SingleRequestExtensions;
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000D3037 File Offset: 0x000D1237
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleRequestExtensions;
		}

		// Token: 0x040017D5 RID: 6101
		private Request req;
	}
}
