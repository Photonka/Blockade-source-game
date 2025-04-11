using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200053E RID: 1342
	public class Gost3410KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032FE RID: 13054 RVA: 0x00135E10 File Offset: 0x00134010
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is Gost3410KeyGenerationParameters)
			{
				this.param = (Gost3410KeyGenerationParameters)parameters;
				return;
			}
			Gost3410KeyGenerationParameters gost3410KeyGenerationParameters = new Gost3410KeyGenerationParameters(parameters.Random, CryptoProObjectIdentifiers.GostR3410x94CryptoProA);
			int strength = parameters.Strength;
			int num = gost3410KeyGenerationParameters.Parameters.P.BitLength - 1;
			this.param = gost3410KeyGenerationParameters;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x00135E64 File Offset: 0x00134064
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			SecureRandom random = this.param.Random;
			Gost3410Parameters parameters = this.param.Parameters;
			BigInteger q = parameters.Q;
			int num = 64;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(256, random);
			}
			while (bigInteger.SignValue < 1 || bigInteger.CompareTo(q) >= 0 || WNafUtilities.GetNafWeight(bigInteger) < num);
			BigInteger p = parameters.P;
			BigInteger y = parameters.A.ModPow(bigInteger, p);
			if (this.param.PublicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, this.param.PublicKeyParamSet), new Gost3410PrivateKeyParameters(bigInteger, this.param.PublicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, parameters), new Gost3410PrivateKeyParameters(bigInteger, parameters));
		}

		// Token: 0x04002071 RID: 8305
		private Gost3410KeyGenerationParameters param;
	}
}
