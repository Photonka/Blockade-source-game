using System;
using System.Collections.Generic;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000162 RID: 354
	public sealed class LegacyTlsClient : DefaultTlsClient
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00091CD8 File Offset: 0x0008FED8
		public LegacyTlsClient(Uri targetUri, ICertificateVerifyer verifyer, IClientCredentialsProvider prov, List<string> hostNames)
		{
			this.TargetUri = targetUri;
			this.verifyer = verifyer;
			this.credProvider = prov;
			base.HostNames = hostNames;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00091CFD File Offset: 0x0008FEFD
		public override TlsAuthentication GetAuthentication()
		{
			return new LegacyTlsAuthentication(this.TargetUri, this.verifyer, this.credProvider);
		}

		// Token: 0x04001162 RID: 4450
		private readonly Uri TargetUri;

		// Token: 0x04001163 RID: 4451
		private readonly ICertificateVerifyer verifyer;

		// Token: 0x04001164 RID: 4452
		private readonly IClientCredentialsProvider credProvider;
	}
}
