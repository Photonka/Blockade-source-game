using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000365 RID: 869
	internal class SecP384R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002287 RID: 8839 RVA: 0x000FAEEA File Offset: 0x000F90EA
		public SecP384R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP384R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP384R1FieldElement", "x");
			}
			this.x = SecP384R1Field.FromBigInteger(x);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000FAF28 File Offset: 0x000F9128
		public SecP384R1FieldElement()
		{
			this.x = Nat.Create(12);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000FAF3D File Offset: 0x000F913D
		protected internal SecP384R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000FAF4C File Offset: 0x000F914C
		public override bool IsZero
		{
			get
			{
				return Nat.IsZero(12, this.x);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000FAF5B File Offset: 0x000F915B
		public override bool IsOne
		{
			get
			{
				return Nat.IsOne(12, this.x);
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000FAF6A File Offset: 0x000F916A
		public override bool TestBitZero()
		{
			return Nat.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000FAF7B File Offset: 0x000F917B
		public override BigInteger ToBigInteger()
		{
			return Nat.ToBigInteger(12, this.x);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x000FAF8A File Offset: 0x000F918A
		public override string FieldName
		{
			get
			{
				return "SecP384R1Field";
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000FAF91 File Offset: 0x000F9191
		public override int FieldSize
		{
			get
			{
				return SecP384R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000FAFA0 File Offset: 0x000F91A0
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Add(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000FAFD4 File Offset: 0x000F91D4
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.AddOne(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000FAFFC File Offset: 0x000F91FC
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Subtract(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000FB030 File Offset: 0x000F9230
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Multiply(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000FB064 File Offset: 0x000F9264
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			Mod.Invert(SecP384R1Field.P, ((SecP384R1FieldElement)b).x, z);
			SecP384R1Field.Multiply(z, this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000FB0A4 File Offset: 0x000F92A4
		public override ECFieldElement Negate()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Negate(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000FB0CC File Offset: 0x000F92CC
		public override ECFieldElement Square()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Square(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000FB0F4 File Offset: 0x000F92F4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat.Create(12);
			Mod.Invert(SecP384R1Field.P, this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000FB120 File Offset: 0x000F9320
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat.IsZero(12, y) || Nat.IsOne(12, y))
			{
				return this;
			}
			uint[] array = Nat.Create(12);
			uint[] array2 = Nat.Create(12);
			uint[] array3 = Nat.Create(12);
			uint[] array4 = Nat.Create(12);
			SecP384R1Field.Square(y, array);
			SecP384R1Field.Multiply(array, y, array);
			SecP384R1Field.SquareN(array, 2, array2);
			SecP384R1Field.Multiply(array2, array, array2);
			SecP384R1Field.Square(array2, array2);
			SecP384R1Field.Multiply(array2, y, array2);
			SecP384R1Field.SquareN(array2, 5, array3);
			SecP384R1Field.Multiply(array3, array2, array3);
			SecP384R1Field.SquareN(array3, 5, array4);
			SecP384R1Field.Multiply(array4, array2, array4);
			SecP384R1Field.SquareN(array4, 15, array2);
			SecP384R1Field.Multiply(array2, array4, array2);
			SecP384R1Field.SquareN(array2, 2, array3);
			SecP384R1Field.Multiply(array, array3, array);
			SecP384R1Field.SquareN(array3, 28, array3);
			SecP384R1Field.Multiply(array2, array3, array2);
			SecP384R1Field.SquareN(array2, 60, array3);
			SecP384R1Field.Multiply(array3, array2, array3);
			uint[] z = array2;
			SecP384R1Field.SquareN(array3, 120, z);
			SecP384R1Field.Multiply(z, array3, z);
			SecP384R1Field.SquareN(z, 15, z);
			SecP384R1Field.Multiply(z, array4, z);
			SecP384R1Field.SquareN(z, 33, z);
			SecP384R1Field.Multiply(z, array, z);
			SecP384R1Field.SquareN(z, 64, z);
			SecP384R1Field.Multiply(z, y, z);
			SecP384R1Field.SquareN(z, 30, array);
			SecP384R1Field.Square(array, array2);
			if (!Nat.Eq(12, y, array2))
			{
				return null;
			}
			return new SecP384R1FieldElement(array);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000FB27C File Offset: 0x000F947C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP384R1FieldElement);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000FB27C File Offset: 0x000F947C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP384R1FieldElement);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000FB28A File Offset: 0x000F948A
		public virtual bool Equals(SecP384R1FieldElement other)
		{
			return this == other || (other != null && Nat.Eq(12, this.x, other.x));
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000FB2AA File Offset: 0x000F94AA
		public override int GetHashCode()
		{
			return SecP384R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 12);
		}

		// Token: 0x04001932 RID: 6450
		public static readonly BigInteger Q = SecP384R1Curve.q;

		// Token: 0x04001933 RID: 6451
		protected internal readonly uint[] x;
	}
}
