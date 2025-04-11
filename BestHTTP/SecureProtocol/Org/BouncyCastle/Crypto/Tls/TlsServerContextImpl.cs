using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200046A RID: 1130
	internal class TlsServerContextImpl : AbstractTlsContext, TlsServerContext, TlsContext
	{
		// Token: 0x06002C9B RID: 11419 RVA: 0x00116D6B File Offset: 0x00114F6B
		internal TlsServerContextImpl(SecureRandom secureRandom, SecurityParameters securityParameters) : base(secureRandom, securityParameters)
		{
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsServer
		{
			get
			{
				return true;
			}
		}
	}
}
