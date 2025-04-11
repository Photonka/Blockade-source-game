using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DB RID: 1243
	public class NaccacheSternKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x0600300C RID: 12300 RVA: 0x001290F4 File Offset: 0x001272F4
		public NaccacheSternKeyParameters(bool privateKey, BigInteger g, BigInteger n, int lowerSigmaBound) : base(privateKey)
		{
			this.g = g;
			this.n = n;
			this.lowerSigmaBound = lowerSigmaBound;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x00129113 File Offset: 0x00127313
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x0012911B File Offset: 0x0012731B
		public int LowerSigmaBound
		{
			get
			{
				return this.lowerSigmaBound;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x00129123 File Offset: 0x00127323
		public BigInteger Modulus
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x04001ED6 RID: 7894
		private readonly BigInteger g;

		// Token: 0x04001ED7 RID: 7895
		private readonly BigInteger n;

		// Token: 0x04001ED8 RID: 7896
		private readonly int lowerSigmaBound;
	}
}
