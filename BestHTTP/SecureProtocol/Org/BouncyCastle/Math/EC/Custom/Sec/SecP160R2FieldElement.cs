using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000349 RID: 841
	internal class SecP160R2FieldElement : AbstractFpFieldElement
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x000F44B0 File Offset: 0x000F26B0
		public SecP160R2FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP160R2FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP160R2FieldElement", "x");
			}
			this.x = SecP160R2Field.FromBigInteger(x);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000F44EE File Offset: 0x000F26EE
		public SecP160R2FieldElement()
		{
			this.x = Nat160.Create();
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000F4501 File Offset: 0x000F2701
		protected internal SecP160R2FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000F4510 File Offset: 0x000F2710
		public override bool IsZero
		{
			get
			{
				return Nat160.IsZero(this.x);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000F451D File Offset: 0x000F271D
		public override bool IsOne
		{
			get
			{
				return Nat160.IsOne(this.x);
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000F452A File Offset: 0x000F272A
		public override bool TestBitZero()
		{
			return Nat160.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000F453B File Offset: 0x000F273B
		public override BigInteger ToBigInteger()
		{
			return Nat160.ToBigInteger(this.x);
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000F4548 File Offset: 0x000F2748
		public override string FieldName
		{
			get
			{
				return "SecP160R2Field";
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000F454F File Offset: 0x000F274F
		public override int FieldSize
		{
			get
			{
				return SecP160R2FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000F455C File Offset: 0x000F275C
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Add(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000F458C File Offset: 0x000F278C
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.AddOne(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000F45B4 File Offset: 0x000F27B4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Subtract(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000F45E4 File Offset: 0x000F27E4
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Multiply(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000F4614 File Offset: 0x000F2814
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R2Field.P, ((SecP160R2FieldElement)b).x, z);
			SecP160R2Field.Multiply(z, this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000F4650 File Offset: 0x000F2850
		public override ECFieldElement Negate()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Negate(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000F4678 File Offset: 0x000F2878
		public override ECFieldElement Square()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Square(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000F46A0 File Offset: 0x000F28A0
		public override ECFieldElement Invert()
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R2Field.P, this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000F46CC File Offset: 0x000F28CC
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat160.IsZero(y) || Nat160.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat160.Create();
			SecP160R2Field.Square(y, array);
			SecP160R2Field.Multiply(array, y, array);
			uint[] array2 = Nat160.Create();
			SecP160R2Field.Square(array, array2);
			SecP160R2Field.Multiply(array2, y, array2);
			uint[] array3 = Nat160.Create();
			SecP160R2Field.Square(array2, array3);
			SecP160R2Field.Multiply(array3, y, array3);
			uint[] array4 = Nat160.Create();
			SecP160R2Field.SquareN(array3, 3, array4);
			SecP160R2Field.Multiply(array4, array2, array4);
			uint[] array5 = array3;
			SecP160R2Field.SquareN(array4, 7, array5);
			SecP160R2Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP160R2Field.SquareN(array5, 3, array6);
			SecP160R2Field.Multiply(array6, array2, array6);
			uint[] array7 = Nat160.Create();
			SecP160R2Field.SquareN(array6, 14, array7);
			SecP160R2Field.Multiply(array7, array5, array7);
			uint[] array8 = array5;
			SecP160R2Field.SquareN(array7, 31, array8);
			SecP160R2Field.Multiply(array8, array7, array8);
			uint[] z = array7;
			SecP160R2Field.SquareN(array8, 62, z);
			SecP160R2Field.Multiply(z, array8, z);
			uint[] array9 = array8;
			SecP160R2Field.SquareN(z, 3, array9);
			SecP160R2Field.Multiply(array9, array2, array9);
			uint[] z2 = array9;
			SecP160R2Field.SquareN(z2, 18, z2);
			SecP160R2Field.Multiply(z2, array6, z2);
			SecP160R2Field.SquareN(z2, 2, z2);
			SecP160R2Field.Multiply(z2, y, z2);
			SecP160R2Field.SquareN(z2, 3, z2);
			SecP160R2Field.Multiply(z2, array, z2);
			SecP160R2Field.SquareN(z2, 6, z2);
			SecP160R2Field.Multiply(z2, array2, z2);
			SecP160R2Field.SquareN(z2, 2, z2);
			SecP160R2Field.Multiply(z2, y, z2);
			uint[] array10 = array;
			SecP160R2Field.Square(z2, array10);
			if (!Nat160.Eq(y, array10))
			{
				return null;
			}
			return new SecP160R2FieldElement(z2);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000F486D File Offset: 0x000F2A6D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP160R2FieldElement);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000F486D File Offset: 0x000F2A6D
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP160R2FieldElement);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000F487B File Offset: 0x000F2A7B
		public virtual bool Equals(SecP160R2FieldElement other)
		{
			return this == other || (other != null && Nat160.Eq(this.x, other.x));
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000F4899 File Offset: 0x000F2A99
		public override int GetHashCode()
		{
			return SecP160R2FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x040018E2 RID: 6370
		public static readonly BigInteger Q = SecP160R2Curve.q;

		// Token: 0x040018E3 RID: 6371
		protected internal readonly uint[] x;
	}
}
