using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000372 RID: 882
	internal class SecT131FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x0600235B RID: 9051 RVA: 0x000FE3EB File Offset: 0x000FC5EB
		public SecT131FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 131)
			{
				throw new ArgumentException("value invalid for SecT131FieldElement", "x");
			}
			this.x = SecT131Field.FromBigInteger(x);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000FE428 File Offset: 0x000FC628
		public SecT131FieldElement()
		{
			this.x = Nat192.Create64();
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000FE43B File Offset: 0x000FC63B
		protected internal SecT131FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000FE44A File Offset: 0x000FC64A
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne64(this.x);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x000FE457 File Offset: 0x000FC657
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero64(this.x);
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000FE464 File Offset: 0x000FC664
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000FE475 File Offset: 0x000FC675
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger64(this.x);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000FE482 File Offset: 0x000FC682
		public override string FieldName
		{
			get
			{
				return "SecT131Field";
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000FE489 File Offset: 0x000FC689
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000FE490 File Offset: 0x000FC690
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Add(this.x, ((SecT131FieldElement)b).x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000FE4C0 File Offset: 0x000FC6C0
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.AddOne(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000FE4E8 File Offset: 0x000FC6E8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Multiply(this.x, ((SecT131FieldElement)b).x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000FE518 File Offset: 0x000FC718
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT131FieldElement)b).x;
			ulong[] array2 = ((SecT131FieldElement)x).x;
			ulong[] y3 = ((SecT131FieldElement)y).x;
			ulong[] array3 = Nat.Create64(5);
			SecT131Field.MultiplyAddToExt(array, y2, array3);
			SecT131Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat192.Create64();
			SecT131Field.Reduce(array3, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000FE57C File Offset: 0x000FC77C
		public override ECFieldElement Square()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Square(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000FE5A4 File Offset: 0x000FC7A4
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT131FieldElement)x).x;
			ulong[] y2 = ((SecT131FieldElement)y).x;
			ulong[] array3 = Nat.Create64(5);
			SecT131Field.SquareAddToExt(array, array3);
			SecT131Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat192.Create64();
			SecT131Field.Reduce(array3, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000FE5F8 File Offset: 0x000FC7F8
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat192.Create64();
			SecT131Field.SquareN(this.x, pow, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000FE624 File Offset: 0x000FC824
		public override int Trace()
		{
			return (int)SecT131Field.Trace(this.x);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000FE634 File Offset: 0x000FC834
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Invert(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000FE65C File Offset: 0x000FC85C
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Sqrt(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000FE489 File Offset: 0x000FC689
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000FE684 File Offset: 0x000FC884
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT131FieldElement);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000FE684 File Offset: 0x000FC884
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT131FieldElement);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000FE692 File Offset: 0x000FC892
		public virtual bool Equals(SecT131FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq64(this.x, other.x));
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000FE6B0 File Offset: 0x000FC8B0
		public override int GetHashCode()
		{
			return 131832 ^ Arrays.GetHashCode(this.x, 0, 3);
		}

		// Token: 0x04001948 RID: 6472
		protected internal readonly ulong[] x;
	}
}
