using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x020003A7 RID: 935
	internal class Curve25519FieldElement : AbstractFpFieldElement
	{
		// Token: 0x060026E6 RID: 9958 RVA: 0x0010BAF2 File Offset: 0x00109CF2
		public Curve25519FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(Curve25519FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for Curve25519FieldElement", "x");
			}
			this.x = Curve25519Field.FromBigInteger(x);
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0010BB30 File Offset: 0x00109D30
		public Curve25519FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0010BB43 File Offset: 0x00109D43
		protected internal Curve25519FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0010BB52 File Offset: 0x00109D52
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0010BB5F File Offset: 0x00109D5F
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0010BB6C File Offset: 0x00109D6C
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0010BB7D File Offset: 0x00109D7D
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x0010BB8A File Offset: 0x00109D8A
		public override string FieldName
		{
			get
			{
				return "Curve25519Field";
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x0010BB91 File Offset: 0x00109D91
		public override int FieldSize
		{
			get
			{
				return Curve25519FieldElement.Q.BitLength;
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0010BBA0 File Offset: 0x00109DA0
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Add(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0010BBD0 File Offset: 0x00109DD0
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.AddOne(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0010BBF8 File Offset: 0x00109DF8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Subtract(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0010BC28 File Offset: 0x00109E28
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Multiply(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0010BC58 File Offset: 0x00109E58
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(Curve25519Field.P, ((Curve25519FieldElement)b).x, z);
			Curve25519Field.Multiply(z, this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0010BC94 File Offset: 0x00109E94
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Negate(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0010BCBC File Offset: 0x00109EBC
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Square(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0010BCE4 File Offset: 0x00109EE4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(Curve25519Field.P, this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0010BD10 File Offset: 0x00109F10
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			Curve25519Field.Square(y, array);
			Curve25519Field.Multiply(array, y, array);
			uint[] array2 = array;
			Curve25519Field.Square(array, array2);
			Curve25519Field.Multiply(array2, y, array2);
			uint[] array3 = Nat256.Create();
			Curve25519Field.Square(array2, array3);
			Curve25519Field.Multiply(array3, y, array3);
			uint[] array4 = Nat256.Create();
			Curve25519Field.SquareN(array3, 3, array4);
			Curve25519Field.Multiply(array4, array2, array4);
			uint[] array5 = array2;
			Curve25519Field.SquareN(array4, 4, array5);
			Curve25519Field.Multiply(array5, array3, array5);
			uint[] array6 = array4;
			Curve25519Field.SquareN(array5, 4, array6);
			Curve25519Field.Multiply(array6, array3, array6);
			uint[] array7 = array3;
			Curve25519Field.SquareN(array6, 15, array7);
			Curve25519Field.Multiply(array7, array6, array7);
			uint[] array8 = array6;
			Curve25519Field.SquareN(array7, 30, array8);
			Curve25519Field.Multiply(array8, array7, array8);
			uint[] array9 = array7;
			Curve25519Field.SquareN(array8, 60, array9);
			Curve25519Field.Multiply(array9, array8, array9);
			uint[] z = array8;
			Curve25519Field.SquareN(array9, 11, z);
			Curve25519Field.Multiply(z, array5, z);
			uint[] array10 = array5;
			Curve25519Field.SquareN(z, 120, array10);
			Curve25519Field.Multiply(array10, array9, array10);
			uint[] z2 = array10;
			Curve25519Field.Square(z2, z2);
			uint[] array11 = array9;
			Curve25519Field.Square(z2, array11);
			if (Nat256.Eq(y, array11))
			{
				return new Curve25519FieldElement(z2);
			}
			Curve25519Field.Multiply(z2, Curve25519FieldElement.PRECOMP_POW2, z2);
			Curve25519Field.Square(z2, array11);
			if (Nat256.Eq(y, array11))
			{
				return new Curve25519FieldElement(z2);
			}
			return null;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0010BE91 File Offset: 0x0010A091
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Curve25519FieldElement);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0010BE91 File Offset: 0x0010A091
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as Curve25519FieldElement);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0010BE9F File Offset: 0x0010A09F
		public virtual bool Equals(Curve25519FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0010BEBD File Offset: 0x0010A0BD
		public override int GetHashCode()
		{
			return Curve25519FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x040019A5 RID: 6565
		public static readonly BigInteger Q = Curve25519.q;

		// Token: 0x040019A6 RID: 6566
		private static readonly uint[] PRECOMP_POW2 = new uint[]
		{
			1242472624U,
			3303938855U,
			2905597048U,
			792926214U,
			1039914919U,
			726466713U,
			1338105611U,
			730014848U
		};

		// Token: 0x040019A7 RID: 6567
		protected internal readonly uint[] x;
	}
}
