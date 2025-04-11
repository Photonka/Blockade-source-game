using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200054B RID: 1355
	public class RsaBlindingFactorGenerator
	{
		// Token: 0x0600334C RID: 13132 RVA: 0x00138384 File Offset: 0x00136584
		public void Init(ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.key = (RsaKeyParameters)param;
				this.random = new SecureRandom();
			}
			if (this.key.IsPrivate)
			{
				throw new ArgumentException("generator requires RSA public key");
			}
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x001383F0 File Offset: 0x001365F0
		public BigInteger GenerateBlindingFactor()
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("generator not initialised");
			}
			BigInteger modulus = this.key.Modulus;
			int sizeInBits = modulus.BitLength - 1;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			do
			{
				bigInteger = new BigInteger(sizeInBits, this.random);
				bigInteger2 = bigInteger.Gcd(modulus);
			}
			while (bigInteger.SignValue == 0 || bigInteger.Equals(BigInteger.One) || !bigInteger2.Equals(BigInteger.One));
			return bigInteger;
		}

		// Token: 0x0400208F RID: 8335
		private RsaKeyParameters key;

		// Token: 0x04002090 RID: 8336
		private SecureRandom random;
	}
}
