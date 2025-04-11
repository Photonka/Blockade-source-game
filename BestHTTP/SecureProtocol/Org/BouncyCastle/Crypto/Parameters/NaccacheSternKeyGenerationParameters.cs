using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DA RID: 1242
	public class NaccacheSternKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06003007 RID: 12295 RVA: 0x0012909B File Offset: 0x0012729B
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes) : base(random, strength)
		{
			if (countSmallPrimes % 2 == 1)
			{
				throw new ArgumentException("countSmallPrimes must be a multiple of 2");
			}
			if (countSmallPrimes < 30)
			{
				throw new ArgumentException("countSmallPrimes must be >= 30 for security reasons");
			}
			this.certainty = certainty;
			this.countSmallPrimes = countSmallPrimes;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x001290D7 File Offset: 0x001272D7
		[Obsolete("Use version without 'debug' parameter")]
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes, bool debug) : this(random, strength, certainty, countSmallPrimes)
		{
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x001290E4 File Offset: 0x001272E4
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x001290EC File Offset: 0x001272EC
		public int CountSmallPrimes
		{
			get
			{
				return this.countSmallPrimes;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		[Obsolete("Remove: always false")]
		public bool IsDebug
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001ED4 RID: 7892
		private readonly int certainty;

		// Token: 0x04001ED5 RID: 7893
		private readonly int countSmallPrimes;
	}
}
