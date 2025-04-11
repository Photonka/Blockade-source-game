using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200036A RID: 874
	internal class SecP521R1Point : AbstractFpPoint
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x000FC019 File Offset: 0x000FA219
		public SecP521R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000E5D84 File Offset: 0x000E3F84
		public SecP521R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000E5DA6 File Offset: 0x000E3FA6
		internal SecP521R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000FC025 File Offset: 0x000FA225
		protected override ECPoint Detach()
		{
			return new SecP521R1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000FC03C File Offset: 0x000FA23C
		public override ECPoint Add(ECPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this;
			}
			if (this == b)
			{
				return this.Twice();
			}
			ECCurve curve = this.Curve;
			SecP521R1FieldElement secP521R1FieldElement = (SecP521R1FieldElement)base.RawXCoord;
			SecP521R1FieldElement secP521R1FieldElement2 = (SecP521R1FieldElement)base.RawYCoord;
			SecP521R1FieldElement secP521R1FieldElement3 = (SecP521R1FieldElement)b.RawXCoord;
			SecP521R1FieldElement secP521R1FieldElement4 = (SecP521R1FieldElement)b.RawYCoord;
			SecP521R1FieldElement secP521R1FieldElement5 = (SecP521R1FieldElement)base.RawZCoords[0];
			SecP521R1FieldElement secP521R1FieldElement6 = (SecP521R1FieldElement)b.RawZCoords[0];
			uint[] array = Nat.Create(17);
			uint[] array2 = Nat.Create(17);
			uint[] array3 = Nat.Create(17);
			uint[] array4 = Nat.Create(17);
			bool isOne = secP521R1FieldElement5.IsOne;
			uint[] array5;
			uint[] array6;
			if (isOne)
			{
				array5 = secP521R1FieldElement3.x;
				array6 = secP521R1FieldElement4.x;
			}
			else
			{
				array6 = array3;
				SecP521R1Field.Square(secP521R1FieldElement5.x, array6);
				array5 = array2;
				SecP521R1Field.Multiply(array6, secP521R1FieldElement3.x, array5);
				SecP521R1Field.Multiply(array6, secP521R1FieldElement5.x, array6);
				SecP521R1Field.Multiply(array6, secP521R1FieldElement4.x, array6);
			}
			bool isOne2 = secP521R1FieldElement6.IsOne;
			uint[] array7;
			uint[] array8;
			if (isOne2)
			{
				array7 = secP521R1FieldElement.x;
				array8 = secP521R1FieldElement2.x;
			}
			else
			{
				array8 = array4;
				SecP521R1Field.Square(secP521R1FieldElement6.x, array8);
				array7 = array;
				SecP521R1Field.Multiply(array8, secP521R1FieldElement.x, array7);
				SecP521R1Field.Multiply(array8, secP521R1FieldElement6.x, array8);
				SecP521R1Field.Multiply(array8, secP521R1FieldElement2.x, array8);
			}
			uint[] array9 = Nat.Create(17);
			SecP521R1Field.Subtract(array7, array5, array9);
			uint[] array10 = array2;
			SecP521R1Field.Subtract(array8, array6, array10);
			if (!Nat.IsZero(17, array9))
			{
				uint[] array11 = array3;
				SecP521R1Field.Square(array9, array11);
				uint[] array12 = Nat.Create(17);
				SecP521R1Field.Multiply(array11, array9, array12);
				uint[] array13 = array3;
				SecP521R1Field.Multiply(array11, array7, array13);
				SecP521R1Field.Multiply(array8, array12, array);
				SecP521R1FieldElement secP521R1FieldElement7 = new SecP521R1FieldElement(array4);
				SecP521R1Field.Square(array10, secP521R1FieldElement7.x);
				SecP521R1Field.Add(secP521R1FieldElement7.x, array12, secP521R1FieldElement7.x);
				SecP521R1Field.Subtract(secP521R1FieldElement7.x, array13, secP521R1FieldElement7.x);
				SecP521R1Field.Subtract(secP521R1FieldElement7.x, array13, secP521R1FieldElement7.x);
				SecP521R1FieldElement secP521R1FieldElement8 = new SecP521R1FieldElement(array12);
				SecP521R1Field.Subtract(array13, secP521R1FieldElement7.x, secP521R1FieldElement8.x);
				SecP521R1Field.Multiply(secP521R1FieldElement8.x, array10, array2);
				SecP521R1Field.Subtract(array2, array, secP521R1FieldElement8.x);
				SecP521R1FieldElement secP521R1FieldElement9 = new SecP521R1FieldElement(array9);
				if (!isOne)
				{
					SecP521R1Field.Multiply(secP521R1FieldElement9.x, secP521R1FieldElement5.x, secP521R1FieldElement9.x);
				}
				if (!isOne2)
				{
					SecP521R1Field.Multiply(secP521R1FieldElement9.x, secP521R1FieldElement6.x, secP521R1FieldElement9.x);
				}
				ECFieldElement[] zs = new ECFieldElement[]
				{
					secP521R1FieldElement9
				};
				return new SecP521R1Point(curve, secP521R1FieldElement7, secP521R1FieldElement8, zs, base.IsCompressed);
			}
			if (Nat.IsZero(17, array10))
			{
				return this.Twice();
			}
			return curve.Infinity;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000FC324 File Offset: 0x000FA524
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			SecP521R1FieldElement secP521R1FieldElement = (SecP521R1FieldElement)base.RawYCoord;
			if (secP521R1FieldElement.IsZero)
			{
				return curve.Infinity;
			}
			SecP521R1FieldElement secP521R1FieldElement2 = (SecP521R1FieldElement)base.RawXCoord;
			SecP521R1FieldElement secP521R1FieldElement3 = (SecP521R1FieldElement)base.RawZCoords[0];
			uint[] array = Nat.Create(17);
			uint[] array2 = Nat.Create(17);
			uint[] array3 = Nat.Create(17);
			SecP521R1Field.Square(secP521R1FieldElement.x, array3);
			uint[] array4 = Nat.Create(17);
			SecP521R1Field.Square(array3, array4);
			bool isOne = secP521R1FieldElement3.IsOne;
			uint[] array5 = secP521R1FieldElement3.x;
			if (!isOne)
			{
				array5 = array2;
				SecP521R1Field.Square(secP521R1FieldElement3.x, array5);
			}
			SecP521R1Field.Subtract(secP521R1FieldElement2.x, array5, array);
			uint[] array6 = array2;
			SecP521R1Field.Add(secP521R1FieldElement2.x, array5, array6);
			SecP521R1Field.Multiply(array6, array, array6);
			Nat.AddBothTo(17, array6, array6, array6);
			SecP521R1Field.Reduce23(array6);
			uint[] array7 = array3;
			SecP521R1Field.Multiply(array3, secP521R1FieldElement2.x, array7);
			Nat.ShiftUpBits(17, array7, 2, 0U);
			SecP521R1Field.Reduce23(array7);
			Nat.ShiftUpBits(17, array4, 3, 0U, array);
			SecP521R1Field.Reduce23(array);
			SecP521R1FieldElement secP521R1FieldElement4 = new SecP521R1FieldElement(array4);
			SecP521R1Field.Square(array6, secP521R1FieldElement4.x);
			SecP521R1Field.Subtract(secP521R1FieldElement4.x, array7, secP521R1FieldElement4.x);
			SecP521R1Field.Subtract(secP521R1FieldElement4.x, array7, secP521R1FieldElement4.x);
			SecP521R1FieldElement secP521R1FieldElement5 = new SecP521R1FieldElement(array7);
			SecP521R1Field.Subtract(array7, secP521R1FieldElement4.x, secP521R1FieldElement5.x);
			SecP521R1Field.Multiply(secP521R1FieldElement5.x, array6, secP521R1FieldElement5.x);
			SecP521R1Field.Subtract(secP521R1FieldElement5.x, array, secP521R1FieldElement5.x);
			SecP521R1FieldElement secP521R1FieldElement6 = new SecP521R1FieldElement(array6);
			SecP521R1Field.Twice(secP521R1FieldElement.x, secP521R1FieldElement6.x);
			if (!isOne)
			{
				SecP521R1Field.Multiply(secP521R1FieldElement6.x, secP521R1FieldElement3.x, secP521R1FieldElement6.x);
			}
			return new SecP521R1Point(curve, secP521R1FieldElement4, secP521R1FieldElement5, new ECFieldElement[]
			{
				secP521R1FieldElement6
			}, base.IsCompressed);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000FC52C File Offset: 0x000FA72C
		public override ECPoint TwicePlus(ECPoint b)
		{
			if (this == b)
			{
				return this.ThreeTimes();
			}
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this.Twice();
			}
			if (base.RawYCoord.IsZero)
			{
				return b;
			}
			return this.Twice().Add(b);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000F2B70 File Offset: 0x000F0D70
		public override ECPoint ThreeTimes()
		{
			if (base.IsInfinity || base.RawYCoord.IsZero)
			{
				return this;
			}
			return this.Twice().Add(this);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000FC578 File Offset: 0x000FA778
		public override ECPoint Negate()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			return new SecP521R1Point(this.Curve, base.RawXCoord, base.RawYCoord.Negate(), base.RawZCoords, base.IsCompressed);
		}
	}
}
