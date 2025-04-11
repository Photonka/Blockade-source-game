using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200036C RID: 876
	internal class SecT113FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x000FCA48 File Offset: 0x000FAC48
		public SecT113FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 113)
			{
				throw new ArgumentException("value invalid for SecT113FieldElement", "x");
			}
			this.x = SecT113Field.FromBigInteger(x);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000FCA82 File Offset: 0x000FAC82
		public SecT113FieldElement()
		{
			this.x = Nat128.Create64();
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000FCA95 File Offset: 0x000FAC95
		protected internal SecT113FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000FCAA4 File Offset: 0x000FACA4
		public override bool IsOne
		{
			get
			{
				return Nat128.IsOne64(this.x);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000FCAB1 File Offset: 0x000FACB1
		public override bool IsZero
		{
			get
			{
				return Nat128.IsZero64(this.x);
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000FCABE File Offset: 0x000FACBE
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000FCACF File Offset: 0x000FACCF
		public override BigInteger ToBigInteger()
		{
			return Nat128.ToBigInteger64(this.x);
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000FCADC File Offset: 0x000FACDC
		public override string FieldName
		{
			get
			{
				return "SecT113Field";
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000FCAE8 File Offset: 0x000FACE8
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Add(this.x, ((SecT113FieldElement)b).x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000FCB18 File Offset: 0x000FAD18
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.AddOne(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000FCB40 File Offset: 0x000FAD40
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Multiply(this.x, ((SecT113FieldElement)b).x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000FCB70 File Offset: 0x000FAD70
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT113FieldElement)b).x;
			ulong[] array2 = ((SecT113FieldElement)x).x;
			ulong[] y3 = ((SecT113FieldElement)y).x;
			ulong[] array3 = Nat128.CreateExt64();
			SecT113Field.MultiplyAddToExt(array, y2, array3);
			SecT113Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat128.Create64();
			SecT113Field.Reduce(array3, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000FCBE0 File Offset: 0x000FADE0
		public override ECFieldElement Square()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Square(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000FCC08 File Offset: 0x000FAE08
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT113FieldElement)x).x;
			ulong[] y2 = ((SecT113FieldElement)y).x;
			ulong[] array3 = Nat128.CreateExt64();
			SecT113Field.SquareAddToExt(array, array3);
			SecT113Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat128.Create64();
			SecT113Field.Reduce(array3, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000FCC5C File Offset: 0x000FAE5C
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat128.Create64();
			SecT113Field.SquareN(this.x, pow, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000FCC88 File Offset: 0x000FAE88
		public override int Trace()
		{
			return (int)SecT113Field.Trace(this.x);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000FCC98 File Offset: 0x000FAE98
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Invert(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000FCCC0 File Offset: 0x000FAEC0
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Sqrt(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000FCCE5 File Offset: 0x000FAEE5
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000FCCE9 File Offset: 0x000FAEE9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT113FieldElement);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000FCCE9 File Offset: 0x000FAEE9
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT113FieldElement);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000FCCF7 File Offset: 0x000FAEF7
		public virtual bool Equals(SecT113FieldElement other)
		{
			return this == other || (other != null && Nat128.Eq64(this.x, other.x));
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000FCD15 File Offset: 0x000FAF15
		public override int GetHashCode()
		{
			return 113009 ^ Arrays.GetHashCode(this.x, 0, 2);
		}

		// Token: 0x0400193E RID: 6462
		protected internal readonly ulong[] x;
	}
}
