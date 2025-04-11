using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000533 RID: 1331
	internal class DHKeyGeneratorHelper
	{
		// Token: 0x060032C8 RID: 13000 RVA: 0x00023EF4 File Offset: 0x000220F4
		private DHKeyGeneratorHelper()
		{
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00134ED4 File Offset: 0x001330D4
		internal BigInteger CalculatePrivate(DHParameters dhParams, SecureRandom random)
		{
			int l = dhParams.L;
			if (l != 0)
			{
				int num = l >> 2;
				BigInteger bigInteger;
				do
				{
					bigInteger = new BigInteger(l, random).SetBit(l - 1);
				}
				while (WNafUtilities.GetNafWeight(bigInteger) < num);
				return bigInteger;
			}
			BigInteger min = BigInteger.Two;
			int m = dhParams.M;
			if (m != 0)
			{
				min = BigInteger.One.ShiftLeft(m - 1);
			}
			BigInteger bigInteger2 = dhParams.Q;
			if (bigInteger2 == null)
			{
				bigInteger2 = dhParams.P;
			}
			BigInteger bigInteger3 = bigInteger2.Subtract(BigInteger.Two);
			int num2 = bigInteger3.BitLength >> 2;
			BigInteger bigInteger4;
			do
			{
				bigInteger4 = BigIntegers.CreateRandomInRange(min, bigInteger3, random);
			}
			while (WNafUtilities.GetNafWeight(bigInteger4) < num2);
			return bigInteger4;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x00134F6E File Offset: 0x0013316E
		internal BigInteger CalculatePublic(DHParameters dhParams, BigInteger x)
		{
			return dhParams.G.ModPow(x, dhParams.P);
		}

		// Token: 0x04002055 RID: 8277
		internal static readonly DHKeyGeneratorHelper Instance = new DHKeyGeneratorHelper();
	}
}
