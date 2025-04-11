using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x020003A3 RID: 931
	internal class SM2P256V1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x060026A6 RID: 9894 RVA: 0x0010AC37 File Offset: 0x00108E37
		public SM2P256V1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SM2P256V1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SM2P256V1FieldElement", "x");
			}
			this.x = SM2P256V1Field.FromBigInteger(x);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0010AC75 File Offset: 0x00108E75
		public SM2P256V1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0010AC88 File Offset: 0x00108E88
		protected internal SM2P256V1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x0010AC97 File Offset: 0x00108E97
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x0010ACA4 File Offset: 0x00108EA4
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0010ACB1 File Offset: 0x00108EB1
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0010ACC2 File Offset: 0x00108EC2
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x0010ACCF File Offset: 0x00108ECF
		public override string FieldName
		{
			get
			{
				return "SM2P256V1Field";
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x0010ACD6 File Offset: 0x00108ED6
		public override int FieldSize
		{
			get
			{
				return SM2P256V1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0010ACE4 File Offset: 0x00108EE4
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Add(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0010AD14 File Offset: 0x00108F14
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.AddOne(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0010AD3C File Offset: 0x00108F3C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Subtract(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0010AD6C File Offset: 0x00108F6C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Multiply(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0010AD9C File Offset: 0x00108F9C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SM2P256V1Field.P, ((SM2P256V1FieldElement)b).x, z);
			SM2P256V1Field.Multiply(z, this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x0010ADD8 File Offset: 0x00108FD8
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Negate(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0010AE00 File Offset: 0x00109000
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Square(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0010AE28 File Offset: 0x00109028
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SM2P256V1Field.P, this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0010AE54 File Offset: 0x00109054
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			SM2P256V1Field.Square(y, array);
			SM2P256V1Field.Multiply(array, y, array);
			uint[] array2 = Nat256.Create();
			SM2P256V1Field.SquareN(array, 2, array2);
			SM2P256V1Field.Multiply(array2, array, array2);
			uint[] array3 = Nat256.Create();
			SM2P256V1Field.SquareN(array2, 2, array3);
			SM2P256V1Field.Multiply(array3, array, array3);
			uint[] array4 = array;
			SM2P256V1Field.SquareN(array3, 6, array4);
			SM2P256V1Field.Multiply(array4, array3, array4);
			uint[] array5 = Nat256.Create();
			SM2P256V1Field.SquareN(array4, 12, array5);
			SM2P256V1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SM2P256V1Field.SquareN(array5, 6, array6);
			SM2P256V1Field.Multiply(array6, array3, array6);
			uint[] array7 = array3;
			SM2P256V1Field.Square(array6, array7);
			SM2P256V1Field.Multiply(array7, y, array7);
			uint[] z = array5;
			SM2P256V1Field.SquareN(array7, 31, z);
			uint[] array8 = array6;
			SM2P256V1Field.Multiply(z, array7, array8);
			SM2P256V1Field.SquareN(z, 32, z);
			SM2P256V1Field.Multiply(z, array8, z);
			SM2P256V1Field.SquareN(z, 62, z);
			SM2P256V1Field.Multiply(z, array8, z);
			SM2P256V1Field.SquareN(z, 4, z);
			SM2P256V1Field.Multiply(z, array2, z);
			SM2P256V1Field.SquareN(z, 32, z);
			SM2P256V1Field.Multiply(z, y, z);
			SM2P256V1Field.SquareN(z, 62, z);
			uint[] array9 = array2;
			SM2P256V1Field.Square(z, array9);
			if (!Nat256.Eq(y, array9))
			{
				return null;
			}
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0010AFB9 File Offset: 0x001091B9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SM2P256V1FieldElement);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0010AFB9 File Offset: 0x001091B9
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SM2P256V1FieldElement);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0010AFC7 File Offset: 0x001091C7
		public virtual bool Equals(SM2P256V1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0010AFE5 File Offset: 0x001091E5
		public override int GetHashCode()
		{
			return SM2P256V1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x0400199B RID: 6555
		public static readonly BigInteger Q = SM2P256V1Curve.q;

		// Token: 0x0400199C RID: 6556
		protected internal readonly uint[] x;
	}
}
