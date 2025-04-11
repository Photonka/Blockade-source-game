using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000369 RID: 873
	internal class SecP521R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x000FBD31 File Offset: 0x000F9F31
		public SecP521R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP521R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP521R1FieldElement", "x");
			}
			this.x = SecP521R1Field.FromBigInteger(x);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000FBD6F File Offset: 0x000F9F6F
		public SecP521R1FieldElement()
		{
			this.x = Nat.Create(17);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000FBD84 File Offset: 0x000F9F84
		protected internal SecP521R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000FBD93 File Offset: 0x000F9F93
		public override bool IsZero
		{
			get
			{
				return Nat.IsZero(17, this.x);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x000FBDA2 File Offset: 0x000F9FA2
		public override bool IsOne
		{
			get
			{
				return Nat.IsOne(17, this.x);
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000FBDB1 File Offset: 0x000F9FB1
		public override bool TestBitZero()
		{
			return Nat.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000FBDC2 File Offset: 0x000F9FC2
		public override BigInteger ToBigInteger()
		{
			return Nat.ToBigInteger(17, this.x);
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000FBDD1 File Offset: 0x000F9FD1
		public override string FieldName
		{
			get
			{
				return "SecP521R1Field";
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000FBDD8 File Offset: 0x000F9FD8
		public override int FieldSize
		{
			get
			{
				return SecP521R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000FBDE4 File Offset: 0x000F9FE4
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Add(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000FBE18 File Offset: 0x000FA018
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.AddOne(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000FBE40 File Offset: 0x000FA040
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Subtract(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000FBE74 File Offset: 0x000FA074
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Multiply(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000FBEA8 File Offset: 0x000FA0A8
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			Mod.Invert(SecP521R1Field.P, ((SecP521R1FieldElement)b).x, z);
			SecP521R1Field.Multiply(z, this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000FBEE8 File Offset: 0x000FA0E8
		public override ECFieldElement Negate()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Negate(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000FBF10 File Offset: 0x000FA110
		public override ECFieldElement Square()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Square(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000FBF38 File Offset: 0x000FA138
		public override ECFieldElement Invert()
		{
			uint[] z = Nat.Create(17);
			Mod.Invert(SecP521R1Field.P, this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000FBF64 File Offset: 0x000FA164
		public override ECFieldElement Sqrt()
		{
			uint[] array = this.x;
			if (Nat.IsZero(17, array) || Nat.IsOne(17, array))
			{
				return this;
			}
			uint[] z = Nat.Create(17);
			uint[] array2 = Nat.Create(17);
			SecP521R1Field.SquareN(array, 519, z);
			SecP521R1Field.Square(z, array2);
			if (!Nat.Eq(17, array, array2))
			{
				return null;
			}
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP521R1FieldElement);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP521R1FieldElement);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000FBFD2 File Offset: 0x000FA1D2
		public virtual bool Equals(SecP521R1FieldElement other)
		{
			return this == other || (other != null && Nat.Eq(17, this.x, other.x));
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000FBFF2 File Offset: 0x000FA1F2
		public override int GetHashCode()
		{
			return SecP521R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 17);
		}

		// Token: 0x0400193A RID: 6458
		public static readonly BigInteger Q = SecP521R1Curve.q;

		// Token: 0x0400193B RID: 6459
		protected internal readonly uint[] x;
	}
}
