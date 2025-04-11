using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B4 RID: 1204
	public class DsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002F38 RID: 12088 RVA: 0x0012775F File Offset: 0x0012595F
		public DsaKeyGenerationParameters(SecureRandom random, DsaParameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x0012777C File Offset: 0x0012597C
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001E83 RID: 7811
		private readonly DsaParameters parameters;
	}
}
