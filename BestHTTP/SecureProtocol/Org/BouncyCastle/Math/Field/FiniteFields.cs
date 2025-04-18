﻿using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x020002FE RID: 766
	public abstract class FiniteFields
	{
		// Token: 0x06001D78 RID: 7544 RVA: 0x000E276C File Offset: 0x000E096C
		public static IPolynomialExtensionField GetBinaryExtensionField(int[] exponents)
		{
			if (exponents[0] != 0)
			{
				throw new ArgumentException("Irreducible polynomials in GF(2) must have constant term", "exponents");
			}
			for (int i = 1; i < exponents.Length; i++)
			{
				if (exponents[i] <= exponents[i - 1])
				{
					throw new ArgumentException("Polynomial exponents must be montonically increasing", "exponents");
				}
			}
			return new GenericPolynomialExtensionField(FiniteFields.GF_2, new GF2Polynomial(exponents));
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000E27C8 File Offset: 0x000E09C8
		public static IFiniteField GetPrimeField(BigInteger characteristic)
		{
			int bitLength = characteristic.BitLength;
			if (characteristic.SignValue <= 0 || bitLength < 2)
			{
				throw new ArgumentException("Must be >= 2", "characteristic");
			}
			if (bitLength < 3)
			{
				int intValue = characteristic.IntValue;
				if (intValue == 2)
				{
					return FiniteFields.GF_2;
				}
				if (intValue == 3)
				{
					return FiniteFields.GF_3;
				}
			}
			return new PrimeField(characteristic);
		}

		// Token: 0x0400180B RID: 6155
		internal static readonly IFiniteField GF_2 = new PrimeField(BigInteger.ValueOf(2L));

		// Token: 0x0400180C RID: 6156
		internal static readonly IFiniteField GF_3 = new PrimeField(BigInteger.ValueOf(3L));
	}
}
