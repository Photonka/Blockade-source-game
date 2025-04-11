using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200035D RID: 861
	internal class SecP256K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600220C RID: 8716 RVA: 0x000F8E85 File Offset: 0x000F7085
		public SecP256K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP256K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP256K1FieldElement", "x");
			}
			this.x = SecP256K1Field.FromBigInteger(x);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000F8EC3 File Offset: 0x000F70C3
		public SecP256K1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000F8ED6 File Offset: 0x000F70D6
		protected internal SecP256K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x000F8EE5 File Offset: 0x000F70E5
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000F8EF2 File Offset: 0x000F70F2
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000F8EFF File Offset: 0x000F70FF
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000F8F10 File Offset: 0x000F7110
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x000F8F1D File Offset: 0x000F711D
		public override string FieldName
		{
			get
			{
				return "SecP256K1Field";
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000F8F24 File Offset: 0x000F7124
		public override int FieldSize
		{
			get
			{
				return SecP256K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000F8F30 File Offset: 0x000F7130
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Add(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000F8F60 File Offset: 0x000F7160
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.AddOne(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000F8F88 File Offset: 0x000F7188
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Subtract(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000F8FB8 File Offset: 0x000F71B8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Multiply(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000F8FE8 File Offset: 0x000F71E8
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256K1Field.P, ((SecP256K1FieldElement)b).x, z);
			SecP256K1Field.Multiply(z, this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000F9024 File Offset: 0x000F7224
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Negate(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000F904C File Offset: 0x000F724C
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Square(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000F9074 File Offset: 0x000F7274
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256K1Field.P, this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000F90A0 File Offset: 0x000F72A0
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			SecP256K1Field.Square(y, array);
			SecP256K1Field.Multiply(array, y, array);
			uint[] array2 = Nat256.Create();
			SecP256K1Field.Square(array, array2);
			SecP256K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat256.Create();
			SecP256K1Field.SquareN(array2, 3, array3);
			SecP256K1Field.Multiply(array3, array2, array3);
			uint[] array4 = array3;
			SecP256K1Field.SquareN(array3, 3, array4);
			SecP256K1Field.Multiply(array4, array2, array4);
			uint[] array5 = array4;
			SecP256K1Field.SquareN(array4, 2, array5);
			SecP256K1Field.Multiply(array5, array, array5);
			uint[] array6 = Nat256.Create();
			SecP256K1Field.SquareN(array5, 11, array6);
			SecP256K1Field.Multiply(array6, array5, array6);
			uint[] array7 = array5;
			SecP256K1Field.SquareN(array6, 22, array7);
			SecP256K1Field.Multiply(array7, array6, array7);
			uint[] array8 = Nat256.Create();
			SecP256K1Field.SquareN(array7, 44, array8);
			SecP256K1Field.Multiply(array8, array7, array8);
			uint[] z = Nat256.Create();
			SecP256K1Field.SquareN(array8, 88, z);
			SecP256K1Field.Multiply(z, array8, z);
			uint[] z2 = array8;
			SecP256K1Field.SquareN(z, 44, z2);
			SecP256K1Field.Multiply(z2, array7, z2);
			uint[] array9 = array7;
			SecP256K1Field.SquareN(z2, 3, array9);
			SecP256K1Field.Multiply(array9, array2, array9);
			uint[] z3 = array9;
			SecP256K1Field.SquareN(z3, 23, z3);
			SecP256K1Field.Multiply(z3, array6, z3);
			SecP256K1Field.SquareN(z3, 6, z3);
			SecP256K1Field.Multiply(z3, array, z3);
			SecP256K1Field.SquareN(z3, 2, z3);
			uint[] array10 = array;
			SecP256K1Field.Square(z3, array10);
			if (!Nat256.Eq(y, array10))
			{
				return null;
			}
			return new SecP256K1FieldElement(z3);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000F922E File Offset: 0x000F742E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP256K1FieldElement);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000F922E File Offset: 0x000F742E
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP256K1FieldElement);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000F923C File Offset: 0x000F743C
		public virtual bool Equals(SecP256K1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000F925A File Offset: 0x000F745A
		public override int GetHashCode()
		{
			return SecP256K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x0400191D RID: 6429
		public static readonly BigInteger Q = SecP256K1Curve.q;

		// Token: 0x0400191E RID: 6430
		protected internal readonly uint[] x;
	}
}
