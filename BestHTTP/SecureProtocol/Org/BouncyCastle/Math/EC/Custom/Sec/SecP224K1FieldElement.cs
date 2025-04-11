using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000355 RID: 853
	internal class SecP224K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x000F6F65 File Offset: 0x000F5165
		public SecP224K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP224K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP224K1FieldElement", "x");
			}
			this.x = SecP224K1Field.FromBigInteger(x);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000F6FA3 File Offset: 0x000F51A3
		public SecP224K1FieldElement()
		{
			this.x = Nat224.Create();
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000F6FB6 File Offset: 0x000F51B6
		protected internal SecP224K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000F6FC5 File Offset: 0x000F51C5
		public override bool IsZero
		{
			get
			{
				return Nat224.IsZero(this.x);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000F6FD2 File Offset: 0x000F51D2
		public override bool IsOne
		{
			get
			{
				return Nat224.IsOne(this.x);
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000F6FDF File Offset: 0x000F51DF
		public override bool TestBitZero()
		{
			return Nat224.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000F6FF0 File Offset: 0x000F51F0
		public override BigInteger ToBigInteger()
		{
			return Nat224.ToBigInteger(this.x);
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x000F6FFD File Offset: 0x000F51FD
		public override string FieldName
		{
			get
			{
				return "SecP224K1Field";
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000F7004 File Offset: 0x000F5204
		public override int FieldSize
		{
			get
			{
				return SecP224K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000F7010 File Offset: 0x000F5210
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Add(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000F7040 File Offset: 0x000F5240
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.AddOne(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000F7068 File Offset: 0x000F5268
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Subtract(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000F7098 File Offset: 0x000F5298
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Multiply(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000F70C8 File Offset: 0x000F52C8
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224K1Field.P, ((SecP224K1FieldElement)b).x, z);
			SecP224K1Field.Multiply(z, this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000F7104 File Offset: 0x000F5304
		public override ECFieldElement Negate()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Negate(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000F712C File Offset: 0x000F532C
		public override ECFieldElement Square()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Square(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000F7154 File Offset: 0x000F5354
		public override ECFieldElement Invert()
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224K1Field.P, this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000F7180 File Offset: 0x000F5380
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat224.IsZero(y) || Nat224.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat224.Create();
			SecP224K1Field.Square(y, array);
			SecP224K1Field.Multiply(array, y, array);
			uint[] array2 = array;
			SecP224K1Field.Square(array, array2);
			SecP224K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat224.Create();
			SecP224K1Field.Square(array2, array3);
			SecP224K1Field.Multiply(array3, y, array3);
			uint[] array4 = Nat224.Create();
			SecP224K1Field.SquareN(array3, 4, array4);
			SecP224K1Field.Multiply(array4, array3, array4);
			uint[] array5 = Nat224.Create();
			SecP224K1Field.SquareN(array4, 3, array5);
			SecP224K1Field.Multiply(array5, array2, array5);
			uint[] array6 = array5;
			SecP224K1Field.SquareN(array5, 8, array6);
			SecP224K1Field.Multiply(array6, array4, array6);
			uint[] array7 = array4;
			SecP224K1Field.SquareN(array6, 4, array7);
			SecP224K1Field.Multiply(array7, array3, array7);
			uint[] array8 = array3;
			SecP224K1Field.SquareN(array7, 19, array8);
			SecP224K1Field.Multiply(array8, array6, array8);
			uint[] array9 = Nat224.Create();
			SecP224K1Field.SquareN(array8, 42, array9);
			SecP224K1Field.Multiply(array9, array8, array9);
			uint[] z = array8;
			SecP224K1Field.SquareN(array9, 23, z);
			SecP224K1Field.Multiply(z, array7, z);
			uint[] array10 = array7;
			SecP224K1Field.SquareN(z, 84, array10);
			SecP224K1Field.Multiply(array10, array9, array10);
			uint[] z2 = array10;
			SecP224K1Field.SquareN(z2, 20, z2);
			SecP224K1Field.Multiply(z2, array6, z2);
			SecP224K1Field.SquareN(z2, 3, z2);
			SecP224K1Field.Multiply(z2, y, z2);
			SecP224K1Field.SquareN(z2, 2, z2);
			SecP224K1Field.Multiply(z2, y, z2);
			SecP224K1Field.SquareN(z2, 4, z2);
			SecP224K1Field.Multiply(z2, array2, z2);
			SecP224K1Field.Square(z2, z2);
			uint[] array11 = array9;
			SecP224K1Field.Square(z2, array11);
			if (Nat224.Eq(y, array11))
			{
				return new SecP224K1FieldElement(z2);
			}
			SecP224K1Field.Multiply(z2, SecP224K1FieldElement.PRECOMP_POW2, z2);
			SecP224K1Field.Square(z2, array11);
			if (Nat224.Eq(y, array11))
			{
				return new SecP224K1FieldElement(z2);
			}
			return null;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000F7359 File Offset: 0x000F5559
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP224K1FieldElement);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000F7359 File Offset: 0x000F5559
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP224K1FieldElement);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000F7367 File Offset: 0x000F5567
		public virtual bool Equals(SecP224K1FieldElement other)
		{
			return this == other || (other != null && Nat224.Eq(this.x, other.x));
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000F7385 File Offset: 0x000F5585
		public override int GetHashCode()
		{
			return SecP224K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x04001905 RID: 6405
		public static readonly BigInteger Q = SecP224K1Curve.q;

		// Token: 0x04001906 RID: 6406
		private static readonly uint[] PRECOMP_POW2 = new uint[]
		{
			868209154U,
			3707425075U,
			579297866U,
			3280018344U,
			2824165628U,
			514782679U,
			2396984652U
		};

		// Token: 0x04001907 RID: 6407
		protected internal readonly uint[] x;
	}
}
