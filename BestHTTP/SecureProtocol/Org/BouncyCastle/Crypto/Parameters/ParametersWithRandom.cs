using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DF RID: 1247
	public class ParametersWithRandom : ICipherParameters
	{
		// Token: 0x0600301D RID: 12317 RVA: 0x001291F8 File Offset: 0x001273F8
		public ParametersWithRandom(ICipherParameters parameters, SecureRandom random)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			this.parameters = parameters;
			this.random = random;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0012922A File Offset: 0x0012742A
		public ParametersWithRandom(ICipherParameters parameters) : this(parameters, new SecureRandom())
		{
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00129238 File Offset: 0x00127438
		[Obsolete("Use Random property instead")]
		public SecureRandom GetRandom()
		{
			return this.Random;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x00129240 File Offset: 0x00127440
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x00129248 File Offset: 0x00127448
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001EDF RID: 7903
		private readonly ICipherParameters parameters;

		// Token: 0x04001EE0 RID: 7904
		private readonly SecureRandom random;
	}
}
