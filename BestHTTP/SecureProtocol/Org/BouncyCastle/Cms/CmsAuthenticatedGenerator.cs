using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D0 RID: 1488
	public class CmsAuthenticatedGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x0600392C RID: 14636 RVA: 0x00168E67 File Offset: 0x00167067
		public CmsAuthenticatedGenerator()
		{
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x00168E6F File Offset: 0x0016706F
		public CmsAuthenticatedGenerator(SecureRandom rand) : base(rand)
		{
		}
	}
}
