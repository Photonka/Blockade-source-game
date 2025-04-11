using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000396 RID: 918
	internal class SecT409FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060025CA RID: 9674 RVA: 0x00107C16 File Offset: 0x00105E16
		public SecT409FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 409)
			{
				throw new ArgumentException("value invalid for SecT409FieldElement", "x");
			}
			this.x = SecT409Field.FromBigInteger(x);
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00107C53 File Offset: 0x00105E53
		public SecT409FieldElement()
		{
			this.x = Nat448.Create64();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00107C66 File Offset: 0x00105E66
		protected internal SecT409FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x00107C75 File Offset: 0x00105E75
		public override bool IsOne
		{
			get
			{
				return Nat448.IsOne64(this.x);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00107C82 File Offset: 0x00105E82
		public override bool IsZero
		{
			get
			{
				return Nat448.IsZero64(this.x);
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00107C8F File Offset: 0x00105E8F
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00107CA0 File Offset: 0x00105EA0
		public override BigInteger ToBigInteger()
		{
			return Nat448.ToBigInteger64(this.x);
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00107CAD File Offset: 0x00105EAD
		public override string FieldName
		{
			get
			{
				return "SecT409Field";
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00107CBC File Offset: 0x00105EBC
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Add(this.x, ((SecT409FieldElement)b).x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00107CEC File Offset: 0x00105EEC
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.AddOne(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00107D14 File Offset: 0x00105F14
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Multiply(this.x, ((SecT409FieldElement)b).x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00107D44 File Offset: 0x00105F44
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT409FieldElement)b).x;
			ulong[] array2 = ((SecT409FieldElement)x).x;
			ulong[] y3 = ((SecT409FieldElement)y).x;
			ulong[] array3 = Nat.Create64(13);
			SecT409Field.MultiplyAddToExt(array, y2, array3);
			SecT409Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat448.Create64();
			SecT409Field.Reduce(array3, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00107DA8 File Offset: 0x00105FA8
		public override ECFieldElement Square()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Square(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00107DD0 File Offset: 0x00105FD0
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT409FieldElement)x).x;
			ulong[] y2 = ((SecT409FieldElement)y).x;
			ulong[] array3 = Nat.Create64(13);
			SecT409Field.SquareAddToExt(array, array3);
			SecT409Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat448.Create64();
			SecT409Field.Reduce(array3, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00107E24 File Offset: 0x00106024
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat448.Create64();
			SecT409Field.SquareN(this.x, pow, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00107E50 File Offset: 0x00106050
		public override int Trace()
		{
			return (int)SecT409Field.Trace(this.x);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00107E60 File Offset: 0x00106060
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Invert(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00107E88 File Offset: 0x00106088
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Sqrt(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00107EAD File Offset: 0x001060AD
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00107EB1 File Offset: 0x001060B1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT409FieldElement);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00107EB1 File Offset: 0x001060B1
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT409FieldElement);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00107EBF File Offset: 0x001060BF
		public virtual bool Equals(SecT409FieldElement other)
		{
			return this == other || (other != null && Nat448.Eq64(this.x, other.x));
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x00107EDD File Offset: 0x001060DD
		public override int GetHashCode()
		{
			return 4090087 ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x04001980 RID: 6528
		protected internal readonly ulong[] x;
	}
}
