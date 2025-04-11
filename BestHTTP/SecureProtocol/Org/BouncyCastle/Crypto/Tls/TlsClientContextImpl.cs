using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000444 RID: 1092
	internal class TlsClientContextImpl : AbstractTlsContext, TlsClientContext, TlsContext
	{
		// Token: 0x06002B1A RID: 11034 RVA: 0x00116D6B File Offset: 0x00114F6B
		internal TlsClientContextImpl(SecureRandom secureRandom, SecurityParameters securityParameters) : base(secureRandom, securityParameters)
		{
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsServer
		{
			get
			{
				return false;
			}
		}
	}
}
