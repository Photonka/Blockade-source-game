using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000161 RID: 353
	public class LegacyTlsAuthentication : TlsAuthentication
	{
		// Token: 0x06000C83 RID: 3203 RVA: 0x00091C7F File Offset: 0x0008FE7F
		public LegacyTlsAuthentication(Uri targetUri, ICertificateVerifyer verifyer, IClientCredentialsProvider prov)
		{
			this.TargetUri = targetUri;
			this.verifyer = verifyer;
			this.credProvider = prov;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00091C9C File Offset: 0x0008FE9C
		public virtual void NotifyServerCertificate(Certificate serverCertificate)
		{
			if (!this.verifyer.IsValid(this.TargetUri, serverCertificate.GetCertificateList()))
			{
				throw new TlsFatalAlert(90);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00091CBF File Offset: 0x0008FEBF
		public virtual TlsCredentials GetClientCredentials(TlsContext context, CertificateRequest certificateRequest)
		{
			if (this.credProvider != null)
			{
				return this.credProvider.GetClientCredentials(context, certificateRequest);
			}
			return null;
		}

		// Token: 0x0400115F RID: 4447
		protected ICertificateVerifyer verifyer;

		// Token: 0x04001160 RID: 4448
		protected IClientCredentialsProvider credProvider;

		// Token: 0x04001161 RID: 4449
		protected Uri TargetUri;
	}
}
