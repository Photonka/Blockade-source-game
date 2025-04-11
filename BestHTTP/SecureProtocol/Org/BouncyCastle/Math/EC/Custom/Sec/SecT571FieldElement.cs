using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200039C RID: 924
	internal class SecT571FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002633 RID: 9779 RVA: 0x00109417 File Offset: 0x00107617
		public SecT571FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 571)
			{
				throw new ArgumentException("value invalid for SecT571FieldElement", "x");
			}
			this.x = SecT571Field.FromBigInteger(x);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00109454 File Offset: 0x00107654
		public SecT571FieldElement()
		{
			this.x = Nat576.Create64();
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00109467 File Offset: 0x00107667
		protected internal SecT571FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00109476 File Offset: 0x00107676
		public override bool IsOne
		{
			get
			{
				return Nat576.IsOne64(this.x);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00109483 File Offset: 0x00107683
		public override bool IsZero
		{
			get
			{
				return Nat576.IsZero64(this.x);
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00109490 File Offset: 0x00107690
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x001094A1 File Offset: 0x001076A1
		public override BigInteger ToBigInteger()
		{
			return Nat576.ToBigInteger64(this.x);
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x001094AE File Offset: 0x001076AE
		public override string FieldName
		{
			get
			{
				return "SecT571Field";
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x001094B5 File Offset: 0x001076B5
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x001094BC File Offset: 0x001076BC
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Add(this.x, ((SecT571FieldElement)b).x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x001094EC File Offset: 0x001076EC
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.AddOne(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00109514 File Offset: 0x00107714
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Multiply(this.x, ((SecT571FieldElement)b).x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00109544 File Offset: 0x00107744
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT571FieldElement)b).x;
			ulong[] array2 = ((SecT571FieldElement)x).x;
			ulong[] y3 = ((SecT571FieldElement)y).x;
			ulong[] array3 = Nat576.CreateExt64();
			SecT571Field.MultiplyAddToExt(array, y2, array3);
			SecT571Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat576.Create64();
			SecT571Field.Reduce(array3, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x001095A8 File Offset: 0x001077A8
		public override ECFieldElement Square()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Square(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x001095D0 File Offset: 0x001077D0
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT571FieldElement)x).x;
			ulong[] y2 = ((SecT571FieldElement)y).x;
			ulong[] array3 = Nat576.CreateExt64();
			SecT571Field.SquareAddToExt(array, array3);
			SecT571Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat576.Create64();
			SecT571Field.Reduce(array3, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00109624 File Offset: 0x00107824
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat576.Create64();
			SecT571Field.SquareN(this.x, pow, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00109650 File Offset: 0x00107850
		public override int Trace()
		{
			return (int)SecT571Field.Trace(this.x);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00109660 File Offset: 0x00107860
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Invert(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00109688 File Offset: 0x00107888
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Sqrt(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x001094B5 File Offset: 0x001076B5
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x001096AD File Offset: 0x001078AD
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x001096B1 File Offset: 0x001078B1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT571FieldElement);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x001096B1 File Offset: 0x001078B1
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT571FieldElement);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x001096BF File Offset: 0x001078BF
		public virtual bool Equals(SecT571FieldElement other)
		{
			return this == other || (other != null && Nat576.Eq64(this.x, other.x));
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x001096DD File Offset: 0x001078DD
		public override int GetHashCode()
		{
			return 5711052 ^ Arrays.GetHashCode(this.x, 0, 9);
		}

		// Token: 0x0400198A RID: 6538
		protected internal readonly ulong[] x;
	}
}
