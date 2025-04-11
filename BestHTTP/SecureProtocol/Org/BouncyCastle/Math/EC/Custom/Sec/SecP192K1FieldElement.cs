using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200034D RID: 845
	internal class SecP192K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x000F52B0 File Offset: 0x000F34B0
		public SecP192K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP192K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP192K1FieldElement", "x");
			}
			this.x = SecP192K1Field.FromBigInteger(x);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000F52EE File Offset: 0x000F34EE
		public SecP192K1FieldElement()
		{
			this.x = Nat192.Create();
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000F5301 File Offset: 0x000F3501
		protected internal SecP192K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000F5310 File Offset: 0x000F3510
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero(this.x);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000F531D File Offset: 0x000F351D
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne(this.x);
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000F532A File Offset: 0x000F352A
		public override bool TestBitZero()
		{
			return Nat192.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000F533B File Offset: 0x000F353B
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger(this.x);
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000F5348 File Offset: 0x000F3548
		public override string FieldName
		{
			get
			{
				return "SecP192K1Field";
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000F534F File Offset: 0x000F354F
		public override int FieldSize
		{
			get
			{
				return SecP192K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000F535C File Offset: 0x000F355C
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Add(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000F538C File Offset: 0x000F358C
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.AddOne(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000F53B4 File Offset: 0x000F35B4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Subtract(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000F53E4 File Offset: 0x000F35E4
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Multiply(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000F5414 File Offset: 0x000F3614
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192K1Field.P, ((SecP192K1FieldElement)b).x, z);
			SecP192K1Field.Multiply(z, this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000F5450 File Offset: 0x000F3650
		public override ECFieldElement Negate()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Negate(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000F5478 File Offset: 0x000F3678
		public override ECFieldElement Square()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Square(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000F54A0 File Offset: 0x000F36A0
		public override ECFieldElement Invert()
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192K1Field.P, this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000F54CC File Offset: 0x000F36CC
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat192.IsZero(y) || Nat192.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat192.Create();
			SecP192K1Field.Square(y, array);
			SecP192K1Field.Multiply(array, y, array);
			uint[] array2 = Nat192.Create();
			SecP192K1Field.Square(array, array2);
			SecP192K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat192.Create();
			SecP192K1Field.SquareN(array2, 3, array3);
			SecP192K1Field.Multiply(array3, array2, array3);
			uint[] array4 = array3;
			SecP192K1Field.SquareN(array3, 2, array4);
			SecP192K1Field.Multiply(array4, array, array4);
			uint[] array5 = array;
			SecP192K1Field.SquareN(array4, 8, array5);
			SecP192K1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP192K1Field.SquareN(array5, 3, array6);
			SecP192K1Field.Multiply(array6, array2, array6);
			uint[] array7 = Nat192.Create();
			SecP192K1Field.SquareN(array6, 16, array7);
			SecP192K1Field.Multiply(array7, array5, array7);
			uint[] array8 = array5;
			SecP192K1Field.SquareN(array7, 35, array8);
			SecP192K1Field.Multiply(array8, array7, array8);
			uint[] z = array7;
			SecP192K1Field.SquareN(array8, 70, z);
			SecP192K1Field.Multiply(z, array8, z);
			uint[] array9 = array8;
			SecP192K1Field.SquareN(z, 19, array9);
			SecP192K1Field.Multiply(array9, array6, array9);
			uint[] z2 = array9;
			SecP192K1Field.SquareN(z2, 20, z2);
			SecP192K1Field.Multiply(z2, array6, z2);
			SecP192K1Field.SquareN(z2, 4, z2);
			SecP192K1Field.Multiply(z2, array2, z2);
			SecP192K1Field.SquareN(z2, 6, z2);
			SecP192K1Field.Multiply(z2, array2, z2);
			SecP192K1Field.Square(z2, z2);
			uint[] array10 = array2;
			SecP192K1Field.Square(z2, array10);
			if (!Nat192.Eq(y, array10))
			{
				return null;
			}
			return new SecP192K1FieldElement(z2);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000F564D File Offset: 0x000F384D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP192K1FieldElement);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000F564D File Offset: 0x000F384D
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP192K1FieldElement);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000F565B File Offset: 0x000F385B
		public virtual bool Equals(SecP192K1FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq(this.x, other.x));
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000F5679 File Offset: 0x000F3879
		public override int GetHashCode()
		{
			return SecP192K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 6);
		}

		// Token: 0x040018EE RID: 6382
		public static readonly BigInteger Q = SecP192K1Curve.q;

		// Token: 0x040018EF RID: 6383
		protected internal readonly uint[] x;
	}
}
