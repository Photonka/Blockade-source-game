using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Abc;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200030A RID: 778
	public abstract class AbstractF2mCurve : ECCurve
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000E3DA2 File Offset: 0x000E1FA2
		public static BigInteger Inverse(int m, int[] ks, BigInteger x)
		{
			return new LongArray(x).ModInverse(m, ks).ToBigInteger();
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000E3DB8 File Offset: 0x000E1FB8
		private static IFiniteField BuildField(int m, int k1, int k2, int k3)
		{
			if (k1 == 0)
			{
				throw new ArgumentException("k1 must be > 0");
			}
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("k3 must be 0 if k2 == 0");
				}
				return FiniteFields.GetBinaryExtensionField(new int[]
				{
					0,
					k1,
					m
				});
			}
			else
			{
				if (k2 <= k1)
				{
					throw new ArgumentException("k2 must be > k1");
				}
				if (k3 <= k2)
				{
					throw new ArgumentException("k3 must be > k2");
				}
				return FiniteFields.GetBinaryExtensionField(new int[]
				{
					0,
					k1,
					k2,
					k3,
					m
				});
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000E3E31 File Offset: 0x000E2031
		protected AbstractF2mCurve(int m, int k1, int k2, int k3) : base(AbstractF2mCurve.BuildField(m, k1, k2, k3))
		{
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000E3E43 File Offset: 0x000E2043
		public override bool IsValidFieldElement(BigInteger x)
		{
			return x != null && x.SignValue >= 0 && x.BitLength <= this.FieldSize;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000E3E64 File Offset: 0x000E2064
		[Obsolete("Per-point compression property will be removed")]
		public override ECPoint CreatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(x);
			ECFieldElement ecfieldElement2 = this.FromBigInteger(y);
			int coordinateSystem = this.CoordinateSystem;
			if (coordinateSystem - 5 <= 1)
			{
				if (ecfieldElement.IsZero)
				{
					if (!ecfieldElement2.Square().Equals(this.B))
					{
						throw new ArgumentException();
					}
				}
				else
				{
					ecfieldElement2 = ecfieldElement2.Divide(ecfieldElement).Add(ecfieldElement);
				}
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement2, withCompression);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000E3EC8 File Offset: 0x000E20C8
		protected override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2 = null;
			if (ecfieldElement.IsZero)
			{
				ecfieldElement2 = this.B.Sqrt();
			}
			else
			{
				ECFieldElement beta = ecfieldElement.Square().Invert().Multiply(this.B).Add(this.A).Add(ecfieldElement);
				ECFieldElement ecfieldElement3 = this.SolveQuadraticEquation(beta);
				if (ecfieldElement3 != null)
				{
					if (ecfieldElement3.TestBitZero() != (yTilde == 1))
					{
						ecfieldElement3 = ecfieldElement3.AddOne();
					}
					int coordinateSystem = this.CoordinateSystem;
					if (coordinateSystem - 5 <= 1)
					{
						ecfieldElement2 = ecfieldElement3.Add(ecfieldElement);
					}
					else
					{
						ecfieldElement2 = ecfieldElement3.Multiply(ecfieldElement);
					}
				}
			}
			if (ecfieldElement2 == null)
			{
				throw new ArgumentException("Invalid point compression");
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement2, true);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000E3F74 File Offset: 0x000E2174
		internal ECFieldElement SolveQuadraticEquation(ECFieldElement beta)
		{
			if (beta.IsZero)
			{
				return beta;
			}
			ECFieldElement ecfieldElement = this.FromBigInteger(BigInteger.Zero);
			int fieldSize = this.FieldSize;
			for (;;)
			{
				ECFieldElement b = this.FromBigInteger(BigInteger.Arbitrary(fieldSize));
				ECFieldElement ecfieldElement2 = ecfieldElement;
				ECFieldElement ecfieldElement3 = beta;
				for (int i = 1; i < fieldSize; i++)
				{
					ECFieldElement ecfieldElement4 = ecfieldElement3.Square();
					ecfieldElement2 = ecfieldElement2.Square().Add(ecfieldElement4.Multiply(b));
					ecfieldElement3 = ecfieldElement4.Add(beta);
				}
				if (!ecfieldElement3.IsZero)
				{
					break;
				}
				ECFieldElement ecfieldElement5 = ecfieldElement2.Square().Add(ecfieldElement2);
				if (!ecfieldElement5.IsZero)
				{
					return ecfieldElement2;
				}
			}
			return null;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000E400C File Offset: 0x000E220C
		internal virtual BigInteger[] GetSi()
		{
			if (this.si == null)
			{
				lock (this)
				{
					if (this.si == null)
					{
						this.si = Tnaf.GetSi(this);
					}
				}
			}
			return this.si;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000E4064 File Offset: 0x000E2264
		public virtual bool IsKoblitz
		{
			get
			{
				return this.m_order != null && this.m_cofactor != null && this.m_b.IsOne && (this.m_a.IsZero || this.m_a.IsOne);
			}
		}

		// Token: 0x04001825 RID: 6181
		private BigInteger[] si;
	}
}
