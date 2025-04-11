using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200054C RID: 1356
	public class RsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x0600334F RID: 13135 RVA: 0x0013845D File Offset: 0x0013665D
		public virtual void Init(KeyGenerationParameters parameters)
		{
			if (parameters is RsaKeyGenerationParameters)
			{
				this.parameters = (RsaKeyGenerationParameters)parameters;
				return;
			}
			this.parameters = new RsaKeyGenerationParameters(RsaKeyPairGenerator.DefaultPublicExponent, parameters.Random, parameters.Strength, 100);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00138494 File Offset: 0x00136694
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			int num2;
			BigInteger publicExponent;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger bigInteger3;
			BigInteger bigInteger5;
			BigInteger bigInteger6;
			BigInteger bigInteger7;
			do
			{
				int strength = this.parameters.Strength;
				int num = (strength + 1) / 2;
				num2 = strength - num;
				int num3 = strength / 3;
				int num4 = strength >> 2;
				publicExponent = this.parameters.PublicExponent;
				bigInteger = this.ChooseRandomPrime(num, publicExponent);
				for (;;)
				{
					bigInteger2 = this.ChooseRandomPrime(num2, publicExponent);
					if (bigInteger2.Subtract(bigInteger).Abs().BitLength >= num3)
					{
						bigInteger3 = bigInteger.Multiply(bigInteger2);
						if (bigInteger3.BitLength != strength)
						{
							bigInteger = bigInteger.Max(bigInteger2);
						}
						else
						{
							if (WNafUtilities.GetNafWeight(bigInteger3) >= num4)
							{
								break;
							}
							bigInteger = this.ChooseRandomPrime(num, publicExponent);
						}
					}
				}
				if (bigInteger.CompareTo(bigInteger2) < 0)
				{
					BigInteger bigInteger4 = bigInteger;
					bigInteger = bigInteger2;
					bigInteger2 = bigInteger4;
				}
				bigInteger5 = bigInteger.Subtract(RsaKeyPairGenerator.One);
				bigInteger6 = bigInteger2.Subtract(RsaKeyPairGenerator.One);
				BigInteger val = bigInteger5.Gcd(bigInteger6);
				BigInteger m = bigInteger5.Divide(val).Multiply(bigInteger6);
				bigInteger7 = publicExponent.ModInverse(m);
			}
			while (bigInteger7.BitLength <= num2);
			BigInteger dP = bigInteger7.Remainder(bigInteger5);
			BigInteger dQ = bigInteger7.Remainder(bigInteger6);
			BigInteger qInv = bigInteger2.ModInverse(bigInteger);
			return new AsymmetricCipherKeyPair(new RsaKeyParameters(false, bigInteger3, publicExponent), new RsaPrivateCrtKeyParameters(bigInteger3, publicExponent, bigInteger7, bigInteger, bigInteger2, dP, dQ, qInv));
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x001385E0 File Offset: 0x001367E0
		protected virtual BigInteger ChooseRandomPrime(int bitlength, BigInteger e)
		{
			bool flag = e.BitLength <= RsaKeyPairGenerator.SPECIAL_E_BITS && Arrays.Contains(RsaKeyPairGenerator.SPECIAL_E_VALUES, e.IntValue);
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitlength, 1, this.parameters.Random);
			}
			while (bigInteger.Mod(e).Equals(RsaKeyPairGenerator.One) || !bigInteger.IsProbablePrime(this.parameters.Certainty, true) || (!flag && !e.Gcd(bigInteger.Subtract(RsaKeyPairGenerator.One)).Equals(RsaKeyPairGenerator.One)));
			return bigInteger;
		}

		// Token: 0x04002091 RID: 8337
		private static readonly int[] SPECIAL_E_VALUES = new int[]
		{
			3,
			5,
			17,
			257,
			65537
		};

		// Token: 0x04002092 RID: 8338
		private static readonly int SPECIAL_E_HIGHEST = RsaKeyPairGenerator.SPECIAL_E_VALUES[RsaKeyPairGenerator.SPECIAL_E_VALUES.Length - 1];

		// Token: 0x04002093 RID: 8339
		private static readonly int SPECIAL_E_BITS = BigInteger.ValueOf((long)RsaKeyPairGenerator.SPECIAL_E_HIGHEST).BitLength;

		// Token: 0x04002094 RID: 8340
		protected static readonly BigInteger One = BigInteger.One;

		// Token: 0x04002095 RID: 8341
		protected static readonly BigInteger DefaultPublicExponent = BigInteger.ValueOf(65537L);

		// Token: 0x04002096 RID: 8342
		protected const int DefaultTests = 100;

		// Token: 0x04002097 RID: 8343
		protected RsaKeyGenerationParameters parameters;
	}
}
