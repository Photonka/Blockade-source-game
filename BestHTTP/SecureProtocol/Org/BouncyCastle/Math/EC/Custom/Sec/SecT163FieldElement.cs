using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000378 RID: 888
	internal class SecT163FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x000FFD65 File Offset: 0x000FDF65
		public SecT163FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 163)
			{
				throw new ArgumentException("value invalid for SecT163FieldElement", "x");
			}
			this.x = SecT163Field.FromBigInteger(x);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000FFDA2 File Offset: 0x000FDFA2
		public SecT163FieldElement()
		{
			this.x = Nat192.Create64();
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000FFDB5 File Offset: 0x000FDFB5
		protected internal SecT163FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000FFDC4 File Offset: 0x000FDFC4
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne64(this.x);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000FFDD1 File Offset: 0x000FDFD1
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero64(this.x);
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000FFDDE File Offset: 0x000FDFDE
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000FFDEF File Offset: 0x000FDFEF
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger64(this.x);
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000FFDFC File Offset: 0x000FDFFC
		public override string FieldName
		{
			get
			{
				return "SecT163Field";
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x000FFE03 File Offset: 0x000FE003
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000FFE0C File Offset: 0x000FE00C
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Add(this.x, ((SecT163FieldElement)b).x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000FFE3C File Offset: 0x000FE03C
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.AddOne(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000FFE64 File Offset: 0x000FE064
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Multiply(this.x, ((SecT163FieldElement)b).x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000FFE94 File Offset: 0x000FE094
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT163FieldElement)b).x;
			ulong[] array2 = ((SecT163FieldElement)x).x;
			ulong[] y3 = ((SecT163FieldElement)y).x;
			ulong[] array3 = Nat192.CreateExt64();
			SecT163Field.MultiplyAddToExt(array, y2, array3);
			SecT163Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat192.Create64();
			SecT163Field.Reduce(array3, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000FFEF8 File Offset: 0x000FE0F8
		public override ECFieldElement Square()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Square(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000FFF20 File Offset: 0x000FE120
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT163FieldElement)x).x;
			ulong[] y2 = ((SecT163FieldElement)y).x;
			ulong[] array3 = Nat192.CreateExt64();
			SecT163Field.SquareAddToExt(array, array3);
			SecT163Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat192.Create64();
			SecT163Field.Reduce(array3, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000FFF74 File Offset: 0x000FE174
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat192.Create64();
			SecT163Field.SquareN(this.x, pow, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000FFFA0 File Offset: 0x000FE1A0
		public override int Trace()
		{
			return (int)SecT163Field.Trace(this.x);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000FFFB0 File Offset: 0x000FE1B0
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Invert(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000FFFD8 File Offset: 0x000FE1D8
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Sqrt(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x000FFE03 File Offset: 0x000FE003
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000A6441 File Offset: 0x000A4641
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00100000 File Offset: 0x000FE200
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT163FieldElement);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00100000 File Offset: 0x000FE200
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT163FieldElement);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0010000E File Offset: 0x000FE20E
		public virtual bool Equals(SecT163FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq64(this.x, other.x));
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0010002C File Offset: 0x000FE22C
		public override int GetHashCode()
		{
			return 163763 ^ Arrays.GetHashCode(this.x, 0, 3);
		}

		// Token: 0x04001952 RID: 6482
		protected internal readonly ulong[] x;
	}
}
