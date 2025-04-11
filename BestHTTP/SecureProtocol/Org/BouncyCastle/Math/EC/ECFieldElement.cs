using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200030C RID: 780
	public abstract class ECFieldElement
	{
		// Token: 0x06001DFE RID: 7678
		public abstract BigInteger ToBigInteger();

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001DFF RID: 7679
		public abstract string FieldName { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001E00 RID: 7680
		public abstract int FieldSize { get; }

		// Token: 0x06001E01 RID: 7681
		public abstract ECFieldElement Add(ECFieldElement b);

		// Token: 0x06001E02 RID: 7682
		public abstract ECFieldElement AddOne();

		// Token: 0x06001E03 RID: 7683
		public abstract ECFieldElement Subtract(ECFieldElement b);

		// Token: 0x06001E04 RID: 7684
		public abstract ECFieldElement Multiply(ECFieldElement b);

		// Token: 0x06001E05 RID: 7685
		public abstract ECFieldElement Divide(ECFieldElement b);

		// Token: 0x06001E06 RID: 7686
		public abstract ECFieldElement Negate();

		// Token: 0x06001E07 RID: 7687
		public abstract ECFieldElement Square();

		// Token: 0x06001E08 RID: 7688
		public abstract ECFieldElement Invert();

		// Token: 0x06001E09 RID: 7689
		public abstract ECFieldElement Sqrt();

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x000E4365 File Offset: 0x000E2565
		public virtual int BitLength
		{
			get
			{
				return this.ToBigInteger().BitLength;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000E4372 File Offset: 0x000E2572
		public virtual bool IsOne
		{
			get
			{
				return this.BitLength == 1;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000E437D File Offset: 0x000E257D
		public virtual bool IsZero
		{
			get
			{
				return this.ToBigInteger().SignValue == 0;
			}
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000E438D File Offset: 0x000E258D
		public virtual ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.Multiply(b).Subtract(x.Multiply(y));
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000E43A2 File Offset: 0x000E25A2
		public virtual ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.Multiply(b).Add(x.Multiply(y));
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000E43B7 File Offset: 0x000E25B7
		public virtual ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.Square().Subtract(x.Multiply(y));
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000E43CB File Offset: 0x000E25CB
		public virtual ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.Square().Add(x.Multiply(y));
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000E43E0 File Offset: 0x000E25E0
		public virtual ECFieldElement SquarePow(int pow)
		{
			ECFieldElement ecfieldElement = this;
			for (int i = 0; i < pow; i++)
			{
				ecfieldElement = ecfieldElement.Square();
			}
			return ecfieldElement;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000E4403 File Offset: 0x000E2603
		public virtual bool TestBitZero()
		{
			return this.ToBigInteger().TestBit(0);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000E4411 File Offset: 0x000E2611
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECFieldElement);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000E441F File Offset: 0x000E261F
		public virtual bool Equals(ECFieldElement other)
		{
			return this == other || (other != null && this.ToBigInteger().Equals(other.ToBigInteger()));
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000E443D File Offset: 0x000E263D
		public override int GetHashCode()
		{
			return this.ToBigInteger().GetHashCode();
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000E444A File Offset: 0x000E264A
		public override string ToString()
		{
			return this.ToBigInteger().ToString(16);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000E4459 File Offset: 0x000E2659
		public virtual byte[] GetEncoded()
		{
			return BigIntegers.AsUnsignedByteArray((this.FieldSize + 7) / 8, this.ToBigInteger());
		}
	}
}
