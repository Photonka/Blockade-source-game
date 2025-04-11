using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000345 RID: 837
	internal class SecP160R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x000F3738 File Offset: 0x000F1938
		public SecP160R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP160R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP160R1FieldElement", "x");
			}
			this.x = SecP160R1Field.FromBigInteger(x);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000F3776 File Offset: 0x000F1976
		public SecP160R1FieldElement()
		{
			this.x = Nat160.Create();
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000F3789 File Offset: 0x000F1989
		protected internal SecP160R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x000F3798 File Offset: 0x000F1998
		public override bool IsZero
		{
			get
			{
				return Nat160.IsZero(this.x);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x000F37A5 File Offset: 0x000F19A5
		public override bool IsOne
		{
			get
			{
				return Nat160.IsOne(this.x);
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000F37B2 File Offset: 0x000F19B2
		public override bool TestBitZero()
		{
			return Nat160.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000F37C3 File Offset: 0x000F19C3
		public override BigInteger ToBigInteger()
		{
			return Nat160.ToBigInteger(this.x);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x000F37D0 File Offset: 0x000F19D0
		public override string FieldName
		{
			get
			{
				return "SecP160R1Field";
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x000F37D7 File Offset: 0x000F19D7
		public override int FieldSize
		{
			get
			{
				return SecP160R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000F37E4 File Offset: 0x000F19E4
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Add(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000F3814 File Offset: 0x000F1A14
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.AddOne(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000F383C File Offset: 0x000F1A3C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Subtract(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000F386C File Offset: 0x000F1A6C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Multiply(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000F389C File Offset: 0x000F1A9C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R1Field.P, ((SecP160R1FieldElement)b).x, z);
			SecP160R1Field.Multiply(z, this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000F38D8 File Offset: 0x000F1AD8
		public override ECFieldElement Negate()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Negate(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000F3900 File Offset: 0x000F1B00
		public override ECFieldElement Square()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Square(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000F3928 File Offset: 0x000F1B28
		public override ECFieldElement Invert()
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R1Field.P, this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000F3954 File Offset: 0x000F1B54
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat160.IsZero(y) || Nat160.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat160.Create();
			SecP160R1Field.Square(y, array);
			SecP160R1Field.Multiply(array, y, array);
			uint[] array2 = Nat160.Create();
			SecP160R1Field.SquareN(array, 2, array2);
			SecP160R1Field.Multiply(array2, array, array2);
			uint[] array3 = array;
			SecP160R1Field.SquareN(array2, 4, array3);
			SecP160R1Field.Multiply(array3, array2, array3);
			uint[] array4 = array2;
			SecP160R1Field.SquareN(array3, 8, array4);
			SecP160R1Field.Multiply(array4, array3, array4);
			uint[] array5 = array3;
			SecP160R1Field.SquareN(array4, 16, array5);
			SecP160R1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP160R1Field.SquareN(array5, 32, array6);
			SecP160R1Field.Multiply(array6, array5, array6);
			uint[] array7 = array5;
			SecP160R1Field.SquareN(array6, 64, array7);
			SecP160R1Field.Multiply(array7, array6, array7);
			uint[] array8 = array6;
			SecP160R1Field.Square(array7, array8);
			SecP160R1Field.Multiply(array8, y, array8);
			uint[] z = array8;
			SecP160R1Field.SquareN(z, 29, z);
			uint[] array9 = array7;
			SecP160R1Field.Square(z, array9);
			if (!Nat160.Eq(y, array9))
			{
				return null;
			}
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000F3A60 File Offset: 0x000F1C60
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP160R1FieldElement);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000F3A60 File Offset: 0x000F1C60
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP160R1FieldElement);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000F3A6E File Offset: 0x000F1C6E
		public virtual bool Equals(SecP160R1FieldElement other)
		{
			return this == other || (other != null && Nat160.Eq(this.x, other.x));
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000F3A8C File Offset: 0x000F1C8C
		public override int GetHashCode()
		{
			return SecP160R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x040018D6 RID: 6358
		public static readonly BigInteger Q = SecP160R1Curve.q;

		// Token: 0x040018D7 RID: 6359
		protected internal readonly uint[] x;
	}
}
