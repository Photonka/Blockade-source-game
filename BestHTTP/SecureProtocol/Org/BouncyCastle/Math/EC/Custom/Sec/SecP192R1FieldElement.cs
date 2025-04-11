using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000351 RID: 849
	internal class SecP192R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002151 RID: 8529 RVA: 0x000F6241 File Offset: 0x000F4441
		public SecP192R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP192R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP192R1FieldElement", "x");
			}
			this.x = SecP192R1Field.FromBigInteger(x);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000F627F File Offset: 0x000F447F
		public SecP192R1FieldElement()
		{
			this.x = Nat192.Create();
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000F6292 File Offset: 0x000F4492
		protected internal SecP192R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000F62A1 File Offset: 0x000F44A1
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero(this.x);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x000F62AE File Offset: 0x000F44AE
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne(this.x);
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000F62BB File Offset: 0x000F44BB
		public override bool TestBitZero()
		{
			return Nat192.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000F62CC File Offset: 0x000F44CC
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger(this.x);
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000F62D9 File Offset: 0x000F44D9
		public override string FieldName
		{
			get
			{
				return "SecP192R1Field";
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x000F62E0 File Offset: 0x000F44E0
		public override int FieldSize
		{
			get
			{
				return SecP192R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000F62EC File Offset: 0x000F44EC
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Add(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000F631C File Offset: 0x000F451C
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.AddOne(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000F6344 File Offset: 0x000F4544
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Subtract(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000F6374 File Offset: 0x000F4574
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Multiply(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000F63A4 File Offset: 0x000F45A4
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192R1Field.P, ((SecP192R1FieldElement)b).x, z);
			SecP192R1Field.Multiply(z, this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000F63E0 File Offset: 0x000F45E0
		public override ECFieldElement Negate()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Negate(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000F6408 File Offset: 0x000F4608
		public override ECFieldElement Square()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Square(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000F6430 File Offset: 0x000F4630
		public override ECFieldElement Invert()
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192R1Field.P, this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000F645C File Offset: 0x000F465C
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat192.IsZero(y) || Nat192.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat192.Create();
			uint[] array2 = Nat192.Create();
			SecP192R1Field.Square(y, array);
			SecP192R1Field.Multiply(array, y, array);
			SecP192R1Field.SquareN(array, 2, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 4, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 8, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 16, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 32, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 64, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 62, array);
			SecP192R1Field.Square(array, array2);
			if (!Nat192.Eq(y, array2))
			{
				return null;
			}
			return new SecP192R1FieldElement(array);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000F6521 File Offset: 0x000F4721
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP192R1FieldElement);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000F6521 File Offset: 0x000F4721
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP192R1FieldElement);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000F652F File Offset: 0x000F472F
		public virtual bool Equals(SecP192R1FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq(this.x, other.x));
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000F654D File Offset: 0x000F474D
		public override int GetHashCode()
		{
			return SecP192R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 6);
		}

		// Token: 0x040018F9 RID: 6393
		public static readonly BigInteger Q = SecP192R1Curve.q;

		// Token: 0x040018FA RID: 6394
		protected internal readonly uint[] x;
	}
}
