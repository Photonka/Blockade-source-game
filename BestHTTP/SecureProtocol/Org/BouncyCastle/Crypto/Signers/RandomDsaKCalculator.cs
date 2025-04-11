using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000491 RID: 1169
	public class RandomDsaKCalculator : IDsaKCalculator
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsDeterministic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x00123E8F File Offset: 0x0012208F
		public virtual void Init(BigInteger n, SecureRandom random)
		{
			this.q = n;
			this.random = random;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x00121DCB File Offset: 0x0011FFCB
		public virtual void Init(BigInteger n, BigInteger d, byte[] message)
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x00123EA0 File Offset: 0x001220A0
		public virtual BigInteger NextK()
		{
			int bitLength = this.q.BitLength;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitLength, this.random);
			}
			while (bigInteger.SignValue < 1 || bigInteger.CompareTo(this.q) >= 0);
			return bigInteger;
		}

		// Token: 0x04001E00 RID: 7680
		private BigInteger q;

		// Token: 0x04001E01 RID: 7681
		private SecureRandom random;
	}
}
