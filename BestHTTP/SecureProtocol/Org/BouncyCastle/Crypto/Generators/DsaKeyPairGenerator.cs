using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000537 RID: 1335
	public class DsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032D7 RID: 13015 RVA: 0x00135264 File Offset: 0x00133464
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.param = (DsaKeyGenerationParameters)parameters;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x00135280 File Offset: 0x00133480
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DsaParameters parameters = this.param.Parameters;
			BigInteger x = DsaKeyPairGenerator.GeneratePrivateKey(parameters.Q, this.param.Random);
			return new AsymmetricCipherKeyPair(new DsaPublicKeyParameters(DsaKeyPairGenerator.CalculatePublicKey(parameters.P, parameters.G, x), parameters), new DsaPrivateKeyParameters(x, parameters));
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x001352D4 File Offset: 0x001334D4
		private static BigInteger GeneratePrivateKey(BigInteger q, SecureRandom random)
		{
			int num = q.BitLength >> 2;
			BigInteger bigInteger;
			do
			{
				bigInteger = BigIntegers.CreateRandomInRange(DsaKeyPairGenerator.One, q.Subtract(DsaKeyPairGenerator.One), random);
			}
			while (WNafUtilities.GetNafWeight(bigInteger) < num);
			return bigInteger;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x0013530B File Offset: 0x0013350B
		private static BigInteger CalculatePublicKey(BigInteger p, BigInteger g, BigInteger x)
		{
			return g.ModPow(x, p);
		}

		// Token: 0x0400205E RID: 8286
		private static readonly BigInteger One = BigInteger.One;

		// Token: 0x0400205F RID: 8287
		private DsaKeyGenerationParameters param;
	}
}
