using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000386 RID: 902
	internal class SecT233FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060024AA RID: 9386 RVA: 0x00103623 File Offset: 0x00101823
		public SecT233FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 233)
			{
				throw new ArgumentException("value invalid for SecT233FieldElement", "x");
			}
			this.x = SecT233Field.FromBigInteger(x);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00103660 File Offset: 0x00101860
		public SecT233FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00103673 File Offset: 0x00101873
		protected internal SecT233FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00103682 File Offset: 0x00101882
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x0010368F File Offset: 0x0010188F
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0010369C File Offset: 0x0010189C
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x001036AD File Offset: 0x001018AD
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x001036BA File Offset: 0x001018BA
		public override string FieldName
		{
			get
			{
				return "SecT233Field";
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x001036C1 File Offset: 0x001018C1
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x001036C8 File Offset: 0x001018C8
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Add(this.x, ((SecT233FieldElement)b).x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x001036F8 File Offset: 0x001018F8
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.AddOne(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00103720 File Offset: 0x00101920
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Multiply(this.x, ((SecT233FieldElement)b).x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00103750 File Offset: 0x00101950
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT233FieldElement)b).x;
			ulong[] array2 = ((SecT233FieldElement)x).x;
			ulong[] y3 = ((SecT233FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT233Field.MultiplyAddToExt(array, y2, array3);
			SecT233Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT233Field.Reduce(array3, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x001037B4 File Offset: 0x001019B4
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Square(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x001037DC File Offset: 0x001019DC
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT233FieldElement)x).x;
			ulong[] y2 = ((SecT233FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT233Field.SquareAddToExt(array, array3);
			SecT233Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT233Field.Reduce(array3, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00103830 File Offset: 0x00101A30
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT233Field.SquareN(this.x, pow, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0010385C File Offset: 0x00101A5C
		public override int Trace()
		{
			return (int)SecT233Field.Trace(this.x);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0010386C File Offset: 0x00101A6C
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Invert(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00103894 File Offset: 0x00101A94
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Sqrt(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x001036C1 File Offset: 0x001018C1
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x001038B9 File Offset: 0x00101AB9
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x001038BD File Offset: 0x00101ABD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT233FieldElement);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x001038BD File Offset: 0x00101ABD
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT233FieldElement);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x001038CB File Offset: 0x00101ACB
		public virtual bool Equals(SecT233FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x001038E9 File Offset: 0x00101AE9
		public override int GetHashCode()
		{
			return 2330074 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001967 RID: 6503
		protected internal readonly ulong[] x;
	}
}
