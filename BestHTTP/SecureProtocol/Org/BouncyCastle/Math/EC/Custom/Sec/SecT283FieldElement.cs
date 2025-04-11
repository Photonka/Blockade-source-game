using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000390 RID: 912
	internal class SecT283FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002562 RID: 9570 RVA: 0x001062E1 File Offset: 0x001044E1
		public SecT283FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 283)
			{
				throw new ArgumentException("value invalid for SecT283FieldElement", "x");
			}
			this.x = SecT283Field.FromBigInteger(x);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0010631E File Offset: 0x0010451E
		public SecT283FieldElement()
		{
			this.x = Nat320.Create64();
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00106331 File Offset: 0x00104531
		protected internal SecT283FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x00106340 File Offset: 0x00104540
		public override bool IsOne
		{
			get
			{
				return Nat320.IsOne64(this.x);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0010634D File Offset: 0x0010454D
		public override bool IsZero
		{
			get
			{
				return Nat320.IsZero64(this.x);
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0010635A File Offset: 0x0010455A
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0010636B File Offset: 0x0010456B
		public override BigInteger ToBigInteger()
		{
			return Nat320.ToBigInteger64(this.x);
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x00106378 File Offset: 0x00104578
		public override string FieldName
		{
			get
			{
				return "SecT283Field";
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x0010637F File Offset: 0x0010457F
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00106388 File Offset: 0x00104588
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Add(this.x, ((SecT283FieldElement)b).x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x001063B8 File Offset: 0x001045B8
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.AddOne(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x001063E0 File Offset: 0x001045E0
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Multiply(this.x, ((SecT283FieldElement)b).x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00106410 File Offset: 0x00104610
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT283FieldElement)b).x;
			ulong[] array2 = ((SecT283FieldElement)x).x;
			ulong[] y3 = ((SecT283FieldElement)y).x;
			ulong[] array3 = Nat.Create64(9);
			SecT283Field.MultiplyAddToExt(array, y2, array3);
			SecT283Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat320.Create64();
			SecT283Field.Reduce(array3, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00106474 File Offset: 0x00104674
		public override ECFieldElement Square()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Square(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0010649C File Offset: 0x0010469C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT283FieldElement)x).x;
			ulong[] y2 = ((SecT283FieldElement)y).x;
			ulong[] array3 = Nat.Create64(9);
			SecT283Field.SquareAddToExt(array, array3);
			SecT283Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat320.Create64();
			SecT283Field.Reduce(array3, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x001064F0 File Offset: 0x001046F0
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat320.Create64();
			SecT283Field.SquareN(this.x, pow, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0010651C File Offset: 0x0010471C
		public override int Trace()
		{
			return (int)SecT283Field.Trace(this.x);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0010652C File Offset: 0x0010472C
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Invert(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00106554 File Offset: 0x00104754
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Sqrt(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x0010637F File Offset: 0x0010457F
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00106579 File Offset: 0x00104779
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0010657D File Offset: 0x0010477D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT283FieldElement);
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0010657D File Offset: 0x0010477D
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT283FieldElement);
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0010658B File Offset: 0x0010478B
		public virtual bool Equals(SecT283FieldElement other)
		{
			return this == other || (other != null && Nat320.Eq64(this.x, other.x));
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x001065A9 File Offset: 0x001047A9
		public override int GetHashCode()
		{
			return 2831275 ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x04001977 RID: 6519
		protected internal readonly ulong[] x;
	}
}
