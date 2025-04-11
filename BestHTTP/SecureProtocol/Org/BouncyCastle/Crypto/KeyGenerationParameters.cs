using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D4 RID: 980
	public class KeyGenerationParameters
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x0010E282 File Offset: 0x0010C482
		public KeyGenerationParameters(SecureRandom random, int strength)
		{
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			if (strength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "strength");
			}
			this.random = random;
			this.strength = strength;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x0010E2BA File Offset: 0x0010C4BA
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0010E2C2 File Offset: 0x0010C4C2
		public int Strength
		{
			get
			{
				return this.strength;
			}
		}

		// Token: 0x040019CA RID: 6602
		private SecureRandom random;

		// Token: 0x040019CB RID: 6603
		private int strength;
	}
}
