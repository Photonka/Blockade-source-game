using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200030E RID: 782
	public class FpFieldElement : AbstractFpFieldElement
	{
		// Token: 0x06001E1A RID: 7706 RVA: 0x000E4478 File Offset: 0x000E2678
		internal static BigInteger CalculateResidue(BigInteger p)
		{
			int bitLength = p.BitLength;
			if (bitLength >= 96)
			{
				if (p.ShiftRight(bitLength - 64).LongValue == -1L)
				{
					return BigInteger.One.ShiftLeft(bitLength).Subtract(p);
				}
				if ((bitLength & 7) == 0)
				{
					return BigInteger.One.ShiftLeft(bitLength << 1).Divide(p).Negate();
				}
			}
			return null;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000E44D5 File Offset: 0x000E26D5
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public FpFieldElement(BigInteger q, BigInteger x) : this(q, FpFieldElement.CalculateResidue(q), x)
		{
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000E44E8 File Offset: 0x000E26E8
		internal FpFieldElement(BigInteger q, BigInteger r, BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(q) >= 0)
			{
				throw new ArgumentException("value invalid in Fp field element", "x");
			}
			this.q = q;
			this.r = r;
			this.x = x;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000E4536 File Offset: 0x000E2736
		public override BigInteger ToBigInteger()
		{
			return this.x;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x000E453E File Offset: 0x000E273E
		public override string FieldName
		{
			get
			{
				return "Fp";
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x000E4545 File Offset: 0x000E2745
		public override int FieldSize
		{
			get
			{
				return this.q.BitLength;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000E4552 File Offset: 0x000E2752
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000E455A File Offset: 0x000E275A
		public override ECFieldElement Add(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModAdd(this.x, b.ToBigInteger()));
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000E4580 File Offset: 0x000E2780
		public override ECFieldElement AddOne()
		{
			BigInteger bigInteger = this.x.Add(BigInteger.One);
			if (bigInteger.CompareTo(this.q) == 0)
			{
				bigInteger = BigInteger.Zero;
			}
			return new FpFieldElement(this.q, this.r, bigInteger);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000E45C4 File Offset: 0x000E27C4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModSubtract(this.x, b.ToBigInteger()));
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000E45E9 File Offset: 0x000E27E9
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, b.ToBigInteger()));
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000E4610 File Offset: 0x000E2810
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger val = b.ToBigInteger();
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val2 = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(val);
			BigInteger n = bigInteger2.Multiply(val2);
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger3.Subtract(n)));
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000E4668 File Offset: 0x000E2868
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger val = b.ToBigInteger();
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val2 = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(val);
			BigInteger value = bigInteger2.Multiply(val2);
			BigInteger bigInteger4 = bigInteger3.Add(value);
			if (this.r != null && this.r.SignValue < 0 && bigInteger4.BitLength > this.q.BitLength << 1)
			{
				bigInteger4 = bigInteger4.Subtract(this.q.ShiftLeft(this.q.BitLength));
			}
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger4));
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000E470A File Offset: 0x000E290A
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, this.ModInverse(b.ToBigInteger())));
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000E4735 File Offset: 0x000E2935
		public override ECFieldElement Negate()
		{
			if (this.x.SignValue != 0)
			{
				return new FpFieldElement(this.q, this.r, this.q.Subtract(this.x));
			}
			return this;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000E4768 File Offset: 0x000E2968
		public override ECFieldElement Square()
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, this.x));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000E4790 File Offset: 0x000E2990
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger);
			BigInteger n = bigInteger2.Multiply(val);
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger3.Subtract(n)));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000E47E0 File Offset: 0x000E29E0
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger);
			BigInteger value = bigInteger2.Multiply(val);
			BigInteger bigInteger4 = bigInteger3.Add(value);
			if (this.r != null && this.r.SignValue < 0 && bigInteger4.BitLength > this.q.BitLength << 1)
			{
				bigInteger4 = bigInteger4.Subtract(this.q.ShiftLeft(this.q.BitLength));
			}
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger4));
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000E4876 File Offset: 0x000E2A76
		public override ECFieldElement Invert()
		{
			return new FpFieldElement(this.q, this.r, this.ModInverse(this.x));
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000E4898 File Offset: 0x000E2A98
		public override ECFieldElement Sqrt()
		{
			if (this.IsZero || this.IsOne)
			{
				return this;
			}
			if (!this.q.TestBit(0))
			{
				throw Platform.CreateNotImplementedException("even value of q");
			}
			if (this.q.TestBit(1))
			{
				BigInteger e = this.q.ShiftRight(2).Add(BigInteger.One);
				return this.CheckSqrt(new FpFieldElement(this.q, this.r, this.x.ModPow(e, this.q)));
			}
			if (this.q.TestBit(2))
			{
				BigInteger bigInteger = this.x.ModPow(this.q.ShiftRight(3), this.q);
				BigInteger x = this.ModMult(bigInteger, this.x);
				if (this.ModMult(x, bigInteger).Equals(BigInteger.One))
				{
					return this.CheckSqrt(new FpFieldElement(this.q, this.r, x));
				}
				BigInteger x2 = BigInteger.Two.ModPow(this.q.ShiftRight(2), this.q);
				BigInteger bigInteger2 = this.ModMult(x, x2);
				return this.CheckSqrt(new FpFieldElement(this.q, this.r, bigInteger2));
			}
			else
			{
				BigInteger bigInteger3 = this.q.ShiftRight(1);
				if (!this.x.ModPow(bigInteger3, this.q).Equals(BigInteger.One))
				{
					return null;
				}
				BigInteger bigInteger4 = this.x;
				BigInteger bigInteger5 = this.ModDouble(this.ModDouble(bigInteger4));
				BigInteger k = bigInteger3.Add(BigInteger.One);
				BigInteger obj = this.q.Subtract(BigInteger.One);
				BigInteger bigInteger8;
				for (;;)
				{
					BigInteger bigInteger6 = BigInteger.Arbitrary(this.q.BitLength);
					if (bigInteger6.CompareTo(this.q) < 0 && this.ModReduce(bigInteger6.Multiply(bigInteger6).Subtract(bigInteger5)).ModPow(bigInteger3, this.q).Equals(obj))
					{
						BigInteger[] array = this.LucasSequence(bigInteger6, bigInteger4, k);
						BigInteger bigInteger7 = array[0];
						bigInteger8 = array[1];
						if (this.ModMult(bigInteger8, bigInteger8).Equals(bigInteger5))
						{
							break;
						}
						if (!bigInteger7.Equals(BigInteger.One) && !bigInteger7.Equals(obj))
						{
							goto Block_11;
						}
					}
				}
				return new FpFieldElement(this.q, this.r, this.ModHalfAbs(bigInteger8));
				Block_11:
				return null;
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000E4AE0 File Offset: 0x000E2CE0
		private ECFieldElement CheckSqrt(ECFieldElement z)
		{
			if (!z.Square().Equals(this))
			{
				return null;
			}
			return z;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000E4AF4 File Offset: 0x000E2CF4
		private BigInteger[] LucasSequence(BigInteger P, BigInteger Q, BigInteger k)
		{
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			BigInteger bigInteger = BigInteger.One;
			BigInteger bigInteger2 = BigInteger.Two;
			BigInteger bigInteger3 = P;
			BigInteger bigInteger4 = BigInteger.One;
			BigInteger bigInteger5 = BigInteger.One;
			for (int i = bitLength - 1; i >= lowestSetBit + 1; i--)
			{
				bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
				if (k.TestBit(i))
				{
					bigInteger5 = this.ModMult(bigInteger4, Q);
					bigInteger = this.ModMult(bigInteger, bigInteger3);
					bigInteger2 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
					bigInteger3 = this.ModReduce(bigInteger3.Multiply(bigInteger3).Subtract(bigInteger5.ShiftLeft(1)));
				}
				else
				{
					bigInteger5 = bigInteger4;
					bigInteger = this.ModReduce(bigInteger.Multiply(bigInteger2).Subtract(bigInteger4));
					bigInteger3 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
					bigInteger2 = this.ModReduce(bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)));
				}
			}
			bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
			bigInteger5 = this.ModMult(bigInteger4, Q);
			bigInteger = this.ModReduce(bigInteger.Multiply(bigInteger2).Subtract(bigInteger4));
			bigInteger2 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
			bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
			for (int j = 1; j <= lowestSetBit; j++)
			{
				bigInteger = this.ModMult(bigInteger, bigInteger2);
				bigInteger2 = this.ModReduce(bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)));
				bigInteger4 = this.ModMult(bigInteger4, bigInteger4);
			}
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000E4C98 File Offset: 0x000E2E98
		protected virtual BigInteger ModAdd(BigInteger x1, BigInteger x2)
		{
			BigInteger bigInteger = x1.Add(x2);
			if (bigInteger.CompareTo(this.q) >= 0)
			{
				bigInteger = bigInteger.Subtract(this.q);
			}
			return bigInteger;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000E4CCC File Offset: 0x000E2ECC
		protected virtual BigInteger ModDouble(BigInteger x)
		{
			BigInteger bigInteger = x.ShiftLeft(1);
			if (bigInteger.CompareTo(this.q) >= 0)
			{
				bigInteger = bigInteger.Subtract(this.q);
			}
			return bigInteger;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000E4CFE File Offset: 0x000E2EFE
		protected virtual BigInteger ModHalf(BigInteger x)
		{
			if (x.TestBit(0))
			{
				x = this.q.Add(x);
			}
			return x.ShiftRight(1);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000E4D1E File Offset: 0x000E2F1E
		protected virtual BigInteger ModHalfAbs(BigInteger x)
		{
			if (x.TestBit(0))
			{
				x = this.q.Subtract(x);
			}
			return x.ShiftRight(1);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000E4D40 File Offset: 0x000E2F40
		protected virtual BigInteger ModInverse(BigInteger x)
		{
			int fieldSize = this.FieldSize;
			int len = fieldSize + 31 >> 5;
			uint[] p = Nat.FromBigInteger(fieldSize, this.q);
			uint[] array = Nat.FromBigInteger(fieldSize, x);
			uint[] z = Nat.Create(len);
			Mod.Invert(p, array, z);
			return Nat.ToBigInteger(len, z);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000E4D84 File Offset: 0x000E2F84
		protected virtual BigInteger ModMult(BigInteger x1, BigInteger x2)
		{
			return this.ModReduce(x1.Multiply(x2));
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000E4D94 File Offset: 0x000E2F94
		protected virtual BigInteger ModReduce(BigInteger x)
		{
			if (this.r == null)
			{
				x = x.Mod(this.q);
			}
			else
			{
				bool flag = x.SignValue < 0;
				if (flag)
				{
					x = x.Abs();
				}
				int bitLength = this.q.BitLength;
				if (this.r.SignValue > 0)
				{
					BigInteger n = BigInteger.One.ShiftLeft(bitLength);
					bool flag2 = this.r.Equals(BigInteger.One);
					while (x.BitLength > bitLength + 1)
					{
						BigInteger bigInteger = x.ShiftRight(bitLength);
						BigInteger value = x.Remainder(n);
						if (!flag2)
						{
							bigInteger = bigInteger.Multiply(this.r);
						}
						x = bigInteger.Add(value);
					}
				}
				else
				{
					int num = (bitLength - 1 & 31) + 1;
					BigInteger bigInteger2 = this.r.Negate().Multiply(x.ShiftRight(bitLength - num)).ShiftRight(bitLength + num).Multiply(this.q);
					BigInteger bigInteger3 = BigInteger.One.ShiftLeft(bitLength + num);
					bigInteger2 = bigInteger2.Remainder(bigInteger3);
					x = x.Remainder(bigInteger3);
					x = x.Subtract(bigInteger2);
					if (x.SignValue < 0)
					{
						x = x.Add(bigInteger3);
					}
				}
				while (x.CompareTo(this.q) >= 0)
				{
					x = x.Subtract(this.q);
				}
				if (flag && x.SignValue != 0)
				{
					x = this.q.Subtract(x);
				}
			}
			return x;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000E4F00 File Offset: 0x000E3100
		protected virtual BigInteger ModSubtract(BigInteger x1, BigInteger x2)
		{
			BigInteger bigInteger = x1.Subtract(x2);
			if (bigInteger.SignValue < 0)
			{
				bigInteger = bigInteger.Add(this.q);
			}
			return bigInteger;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000E4F2C File Offset: 0x000E312C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FpFieldElement fpFieldElement = obj as FpFieldElement;
			return fpFieldElement != null && this.Equals(fpFieldElement);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000E4F52 File Offset: 0x000E3152
		public virtual bool Equals(FpFieldElement other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000E4F70 File Offset: 0x000E3170
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x0400182C RID: 6188
		private readonly BigInteger q;

		// Token: 0x0400182D RID: 6189
		private readonly BigInteger r;

		// Token: 0x0400182E RID: 6190
		private readonly BigInteger x;
	}
}
