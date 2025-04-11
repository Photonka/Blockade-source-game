using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200033F RID: 831
	internal class SecP128R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x000F22DF File Offset: 0x000F04DF
		public SecP128R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP128R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP128R1FieldElement", "x");
			}
			this.x = SecP128R1Field.FromBigInteger(x);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000F231D File Offset: 0x000F051D
		public SecP128R1FieldElement()
		{
			this.x = Nat128.Create();
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000F2330 File Offset: 0x000F0530
		protected internal SecP128R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x000F233F File Offset: 0x000F053F
		public override bool IsZero
		{
			get
			{
				return Nat128.IsZero(this.x);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x000F234C File Offset: 0x000F054C
		public override bool IsOne
		{
			get
			{
				return Nat128.IsOne(this.x);
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000F2359 File Offset: 0x000F0559
		public override bool TestBitZero()
		{
			return Nat128.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000F236A File Offset: 0x000F056A
		public override BigInteger ToBigInteger()
		{
			return Nat128.ToBigInteger(this.x);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x000F2377 File Offset: 0x000F0577
		public override string FieldName
		{
			get
			{
				return "SecP128R1Field";
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000F237E File Offset: 0x000F057E
		public override int FieldSize
		{
			get
			{
				return SecP128R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000F238C File Offset: 0x000F058C
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Add(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000F23BC File Offset: 0x000F05BC
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.AddOne(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000F23E4 File Offset: 0x000F05E4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Subtract(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000F2414 File Offset: 0x000F0614
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Multiply(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000F2444 File Offset: 0x000F0644
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			Mod.Invert(SecP128R1Field.P, ((SecP128R1FieldElement)b).x, z);
			SecP128R1Field.Multiply(z, this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000F2480 File Offset: 0x000F0680
		public override ECFieldElement Negate()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Negate(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000F24A8 File Offset: 0x000F06A8
		public override ECFieldElement Square()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Square(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000F24D0 File Offset: 0x000F06D0
		public override ECFieldElement Invert()
		{
			uint[] z = Nat128.Create();
			Mod.Invert(SecP128R1Field.P, this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000F24FC File Offset: 0x000F06FC
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat128.IsZero(y) || Nat128.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat128.Create();
			SecP128R1Field.Square(y, array);
			SecP128R1Field.Multiply(array, y, array);
			uint[] array2 = Nat128.Create();
			SecP128R1Field.SquareN(array, 2, array2);
			SecP128R1Field.Multiply(array2, array, array2);
			uint[] array3 = Nat128.Create();
			SecP128R1Field.SquareN(array2, 4, array3);
			SecP128R1Field.Multiply(array3, array2, array3);
			uint[] array4 = array2;
			SecP128R1Field.SquareN(array3, 2, array4);
			SecP128R1Field.Multiply(array4, array, array4);
			uint[] z = array;
			SecP128R1Field.SquareN(array4, 10, z);
			SecP128R1Field.Multiply(z, array4, z);
			uint[] array5 = array3;
			SecP128R1Field.SquareN(z, 10, array5);
			SecP128R1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP128R1Field.Square(array5, array6);
			SecP128R1Field.Multiply(array6, y, array6);
			uint[] z2 = array6;
			SecP128R1Field.SquareN(z2, 95, z2);
			uint[] array7 = array5;
			SecP128R1Field.Square(z2, array7);
			if (!Nat128.Eq(y, array7))
			{
				return null;
			}
			return new SecP128R1FieldElement(z2);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000F25F1 File Offset: 0x000F07F1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP128R1FieldElement);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000F25F1 File Offset: 0x000F07F1
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP128R1FieldElement);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000F25FF File Offset: 0x000F07FF
		public virtual bool Equals(SecP128R1FieldElement other)
		{
			return this == other || (other != null && Nat128.Eq(this.x, other.x));
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000F261D File Offset: 0x000F081D
		public override int GetHashCode()
		{
			return SecP128R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x040018C6 RID: 6342
		public static readonly BigInteger Q = SecP128R1Curve.q;

		// Token: 0x040018C7 RID: 6343
		protected internal readonly uint[] x;
	}
}
