using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DC RID: 1244
	public class NaccacheSternPrivateKeyParameters : NaccacheSternKeyParameters
	{
		// Token: 0x06003010 RID: 12304 RVA: 0x0012912B File Offset: 0x0012732B
		[Obsolete]
		public NaccacheSternPrivateKeyParameters(BigInteger g, BigInteger n, int lowerSigmaBound, ArrayList smallPrimes, BigInteger phiN) : base(true, g, n, lowerSigmaBound)
		{
			this.smallPrimes = smallPrimes;
			this.phiN = phiN;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0012912B File Offset: 0x0012732B
		public NaccacheSternPrivateKeyParameters(BigInteger g, BigInteger n, int lowerSigmaBound, IList smallPrimes, BigInteger phiN) : base(true, g, n, lowerSigmaBound)
		{
			this.smallPrimes = smallPrimes;
			this.phiN = phiN;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x00129147 File Offset: 0x00127347
		public BigInteger PhiN
		{
			get
			{
				return this.phiN;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x0012914F File Offset: 0x0012734F
		[Obsolete("Use 'SmallPrimesList' instead")]
		public ArrayList SmallPrimes
		{
			get
			{
				return new ArrayList(this.smallPrimes);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x0012915C File Offset: 0x0012735C
		public IList SmallPrimesList
		{
			get
			{
				return this.smallPrimes;
			}
		}

		// Token: 0x04001ED9 RID: 7897
		private readonly BigInteger phiN;

		// Token: 0x04001EDA RID: 7898
		private readonly IList smallPrimes;
	}
}
