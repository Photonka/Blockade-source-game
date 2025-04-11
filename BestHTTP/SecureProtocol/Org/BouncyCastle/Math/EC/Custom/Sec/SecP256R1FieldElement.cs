using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000361 RID: 865
	internal class SecP256R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600224A RID: 8778 RVA: 0x000F9EC3 File Offset: 0x000F80C3
		public SecP256R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP256R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP256R1FieldElement", "x");
			}
			this.x = SecP256R1Field.FromBigInteger(x);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000F9F01 File Offset: 0x000F8101
		public SecP256R1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000F9F14 File Offset: 0x000F8114
		protected internal SecP256R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000F9F23 File Offset: 0x000F8123
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000F9F30 File Offset: 0x000F8130
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000F9F3D File Offset: 0x000F813D
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000F9F4E File Offset: 0x000F814E
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000F9F5B File Offset: 0x000F815B
		public override string FieldName
		{
			get
			{
				return "SecP256R1Field";
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000F9F62 File Offset: 0x000F8162
		public override int FieldSize
		{
			get
			{
				return SecP256R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000F9F70 File Offset: 0x000F8170
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Add(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000F9FA0 File Offset: 0x000F81A0
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.AddOne(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000F9FC8 File Offset: 0x000F81C8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Subtract(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000F9FF8 File Offset: 0x000F81F8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Multiply(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000FA028 File Offset: 0x000F8228
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256R1Field.P, ((SecP256R1FieldElement)b).x, z);
			SecP256R1Field.Multiply(z, this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000FA064 File Offset: 0x000F8264
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Negate(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000FA08C File Offset: 0x000F828C
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Square(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000FA0B4 File Offset: 0x000F82B4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256R1Field.P, this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000FA0E0 File Offset: 0x000F82E0
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			uint[] array2 = Nat256.Create();
			SecP256R1Field.Square(y, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 2, array2);
			SecP256R1Field.Multiply(array2, array, array2);
			SecP256R1Field.SquareN(array2, 4, array);
			SecP256R1Field.Multiply(array, array2, array);
			SecP256R1Field.SquareN(array, 8, array2);
			SecP256R1Field.Multiply(array2, array, array2);
			SecP256R1Field.SquareN(array2, 16, array);
			SecP256R1Field.Multiply(array, array2, array);
			SecP256R1Field.SquareN(array, 32, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 96, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 94, array);
			SecP256R1Field.Multiply(array, array, array2);
			if (!Nat256.Eq(y, array2))
			{
				return null;
			}
			return new SecP256R1FieldElement(array);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000FA1A6 File Offset: 0x000F83A6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP256R1FieldElement);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000FA1A6 File Offset: 0x000F83A6
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP256R1FieldElement);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000FA1B4 File Offset: 0x000F83B4
		public virtual bool Equals(SecP256R1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000FA1D2 File Offset: 0x000F83D2
		public override int GetHashCode()
		{
			return SecP256R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x04001927 RID: 6439
		public static readonly BigInteger Q = SecP256R1Curve.q;

		// Token: 0x04001928 RID: 6440
		protected internal readonly uint[] x;
	}
}
