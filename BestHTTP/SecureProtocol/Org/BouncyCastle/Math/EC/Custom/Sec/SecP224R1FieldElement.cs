using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000359 RID: 857
	internal class SecP224R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x060021CB RID: 8651 RVA: 0x000F7FBD File Offset: 0x000F61BD
		public SecP224R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP224R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP224R1FieldElement", "x");
			}
			this.x = SecP224R1Field.FromBigInteger(x);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000F7FFB File Offset: 0x000F61FB
		public SecP224R1FieldElement()
		{
			this.x = Nat224.Create();
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000F800E File Offset: 0x000F620E
		protected internal SecP224R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000F801D File Offset: 0x000F621D
		public override bool IsZero
		{
			get
			{
				return Nat224.IsZero(this.x);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000F802A File Offset: 0x000F622A
		public override bool IsOne
		{
			get
			{
				return Nat224.IsOne(this.x);
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000F8037 File Offset: 0x000F6237
		public override bool TestBitZero()
		{
			return Nat224.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000F8048 File Offset: 0x000F6248
		public override BigInteger ToBigInteger()
		{
			return Nat224.ToBigInteger(this.x);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000F8055 File Offset: 0x000F6255
		public override string FieldName
		{
			get
			{
				return "SecP224R1Field";
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000F805C File Offset: 0x000F625C
		public override int FieldSize
		{
			get
			{
				return SecP224R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000F8068 File Offset: 0x000F6268
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Add(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000F8098 File Offset: 0x000F6298
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.AddOne(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000F80C0 File Offset: 0x000F62C0
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Subtract(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000F80F0 File Offset: 0x000F62F0
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Multiply(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000F8120 File Offset: 0x000F6320
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224R1Field.P, ((SecP224R1FieldElement)b).x, z);
			SecP224R1Field.Multiply(z, this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000F815C File Offset: 0x000F635C
		public override ECFieldElement Negate()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Negate(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000F8184 File Offset: 0x000F6384
		public override ECFieldElement Square()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Square(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000F81AC File Offset: 0x000F63AC
		public override ECFieldElement Invert()
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224R1Field.P, this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000F81D8 File Offset: 0x000F63D8
		public override ECFieldElement Sqrt()
		{
			uint[] array = this.x;
			if (Nat224.IsZero(array) || Nat224.IsOne(array))
			{
				return this;
			}
			uint[] array2 = Nat224.Create();
			SecP224R1Field.Negate(array, array2);
			uint[] array3 = Mod.Random(SecP224R1Field.P);
			uint[] t = Nat224.Create();
			if (!SecP224R1FieldElement.IsSquare(array))
			{
				return null;
			}
			while (!SecP224R1FieldElement.TrySqrt(array2, array3, t))
			{
				SecP224R1Field.AddOne(array3, array3);
			}
			SecP224R1Field.Square(t, array3);
			if (!Nat224.Eq(array, array3))
			{
				return null;
			}
			return new SecP224R1FieldElement(t);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000F824F File Offset: 0x000F644F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP224R1FieldElement);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000F824F File Offset: 0x000F644F
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP224R1FieldElement);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000F825D File Offset: 0x000F645D
		public virtual bool Equals(SecP224R1FieldElement other)
		{
			return this == other || (other != null && Nat224.Eq(this.x, other.x));
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000F827B File Offset: 0x000F647B
		public override int GetHashCode()
		{
			return SecP224R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000F8298 File Offset: 0x000F6498
		private static bool IsSquare(uint[] x)
		{
			uint[] z = Nat224.Create();
			uint[] array = Nat224.Create();
			Nat224.Copy(x, z);
			for (int i = 0; i < 7; i++)
			{
				Nat224.Copy(z, array);
				SecP224R1Field.SquareN(z, 1 << i, z);
				SecP224R1Field.Multiply(z, array, z);
			}
			SecP224R1Field.SquareN(z, 95, z);
			return Nat224.IsOne(z);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000F82F0 File Offset: 0x000F64F0
		private static void RM(uint[] nc, uint[] d0, uint[] e0, uint[] d1, uint[] e1, uint[] f1, uint[] t)
		{
			SecP224R1Field.Multiply(e1, e0, t);
			SecP224R1Field.Multiply(t, nc, t);
			SecP224R1Field.Multiply(d1, d0, f1);
			SecP224R1Field.Add(f1, t, f1);
			SecP224R1Field.Multiply(d1, e0, t);
			Nat224.Copy(f1, d1);
			SecP224R1Field.Multiply(e1, d0, e1);
			SecP224R1Field.Add(e1, t, e1);
			SecP224R1Field.Square(e1, f1);
			SecP224R1Field.Multiply(f1, nc, f1);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000F8360 File Offset: 0x000F6560
		private static void RP(uint[] nc, uint[] d1, uint[] e1, uint[] f1, uint[] t)
		{
			Nat224.Copy(nc, f1);
			uint[] array = Nat224.Create();
			uint[] array2 = Nat224.Create();
			for (int i = 0; i < 7; i++)
			{
				Nat224.Copy(d1, array);
				Nat224.Copy(e1, array2);
				int num = 1 << i;
				while (--num >= 0)
				{
					SecP224R1FieldElement.RS(d1, e1, f1, t);
				}
				SecP224R1FieldElement.RM(nc, array, array2, d1, e1, f1, t);
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000F83C2 File Offset: 0x000F65C2
		private static void RS(uint[] d, uint[] e, uint[] f, uint[] t)
		{
			SecP224R1Field.Multiply(e, d, e);
			SecP224R1Field.Twice(e, e);
			SecP224R1Field.Square(d, t);
			SecP224R1Field.Add(f, t, d);
			SecP224R1Field.Multiply(f, t, f);
			SecP224R1Field.Reduce32(Nat.ShiftUpBits(7, f, 2, 0U), f);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000F83FC File Offset: 0x000F65FC
		private static bool TrySqrt(uint[] nc, uint[] r, uint[] t)
		{
			uint[] array = Nat224.Create();
			Nat224.Copy(r, array);
			uint[] array2 = Nat224.Create();
			array2[0] = 1U;
			uint[] array3 = Nat224.Create();
			SecP224R1FieldElement.RP(nc, array, array2, array3, t);
			uint[] array4 = Nat224.Create();
			uint[] z = Nat224.Create();
			for (int i = 1; i < 96; i++)
			{
				Nat224.Copy(array, array4);
				Nat224.Copy(array2, z);
				SecP224R1FieldElement.RS(array, array2, array3, t);
				if (Nat224.IsZero(array))
				{
					Mod.Invert(SecP224R1Field.P, z, t);
					SecP224R1Field.Multiply(t, array4, t);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001911 RID: 6417
		public static readonly BigInteger Q = SecP224R1Curve.q;

		// Token: 0x04001912 RID: 6418
		protected internal readonly uint[] x;
	}
}
