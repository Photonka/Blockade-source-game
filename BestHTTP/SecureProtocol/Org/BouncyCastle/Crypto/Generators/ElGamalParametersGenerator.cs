using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200053D RID: 1341
	public class ElGamalParametersGenerator
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x00135DB8 File Offset: 0x00133FB8
		public void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x00135DD0 File Offset: 0x00133FD0
		public ElGamalParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new ElGamalParameters(p, g);
		}

		// Token: 0x0400206E RID: 8302
		private int size;

		// Token: 0x0400206F RID: 8303
		private int certainty;

		// Token: 0x04002070 RID: 8304
		private SecureRandom random;
	}
}
