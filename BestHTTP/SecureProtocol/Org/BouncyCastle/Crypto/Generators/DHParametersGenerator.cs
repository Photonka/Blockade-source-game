using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000535 RID: 1333
	public class DHParametersGenerator
	{
		// Token: 0x060032CF RID: 13007 RVA: 0x00134FE6 File Offset: 0x001331E6
		public virtual void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x00135000 File Offset: 0x00133200
		public virtual DHParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new DHParameters(p, g, q, BigInteger.Two, null);
		}

		// Token: 0x04002057 RID: 8279
		private int size;

		// Token: 0x04002058 RID: 8280
		private int certainty;

		// Token: 0x04002059 RID: 8281
		private SecureRandom random;
	}
}
