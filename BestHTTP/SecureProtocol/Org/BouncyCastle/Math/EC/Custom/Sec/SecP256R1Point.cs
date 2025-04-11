﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000362 RID: 866
	internal class SecP256R1Point : AbstractFpPoint
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x000FA1F8 File Offset: 0x000F83F8
		public SecP256R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000E5D84 File Offset: 0x000E3F84
		public SecP256R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000E5DA6 File Offset: 0x000E3FA6
		internal SecP256R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000FA204 File Offset: 0x000F8404
		protected override ECPoint Detach()
		{
			return new SecP256R1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000FA218 File Offset: 0x000F8418
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
			SecP256R1FieldElement secP256R1FieldElement = (SecP256R1FieldElement)base.RawXCoord;
			SecP256R1FieldElement secP256R1FieldElement2 = (SecP256R1FieldElement)base.RawYCoord;
			SecP256R1FieldElement secP256R1FieldElement3 = (SecP256R1FieldElement)b.RawXCoord;
			SecP256R1FieldElement secP256R1FieldElement4 = (SecP256R1FieldElement)b.RawYCoord;
			SecP256R1FieldElement secP256R1FieldElement5 = (SecP256R1FieldElement)base.RawZCoords[0];
			SecP256R1FieldElement secP256R1FieldElement6 = (SecP256R1FieldElement)b.RawZCoords[0];
			uint[] array = Nat256.CreateExt();
			uint[] array2 = Nat256.Create();
			uint[] array3 = Nat256.Create();
			uint[] array4 = Nat256.Create();
			bool isOne = secP256R1FieldElement5.IsOne;
			uint[] array5;
			uint[] array6;
			if (isOne)
			{
				array5 = secP256R1FieldElement3.x;
				array6 = secP256R1FieldElement4.x;
			}
			else
			{
				array6 = array3;
				SecP256R1Field.Square(secP256R1FieldElement5.x, array6);
				array5 = array2;
				SecP256R1Field.Multiply(array6, secP256R1FieldElement3.x, array5);
				SecP256R1Field.Multiply(array6, secP256R1FieldElement5.x, array6);
				SecP256R1Field.Multiply(array6, secP256R1FieldElement4.x, array6);
			}
			bool isOne2 = secP256R1FieldElement6.IsOne;
			uint[] array7;
			uint[] array8;
			if (isOne2)
			{
				array7 = secP256R1FieldElement.x;
				array8 = secP256R1FieldElement2.x;
			}
			else
			{
				array8 = array4;
				SecP256R1Field.Square(secP256R1FieldElement6.x, array8);
				array7 = array;
				SecP256R1Field.Multiply(array8, secP256R1FieldElement.x, array7);
				SecP256R1Field.Multiply(array8, secP256R1FieldElement6.x, array8);
				SecP256R1Field.Multiply(array8, secP256R1FieldElement2.x, array8);
			}
			uint[] array9 = Nat256.Create();
			SecP256R1Field.Subtract(array7, array5, array9);
			uint[] array10 = array2;
			SecP256R1Field.Subtract(array8, array6, array10);
			if (!Nat256.IsZero(array9))
			{
				uint[] array11 = array3;
				SecP256R1Field.Square(array9, array11);
				uint[] array12 = Nat256.Create();
				SecP256R1Field.Multiply(array11, array9, array12);
				uint[] array13 = array3;
				SecP256R1Field.Multiply(array11, array7, array13);
				SecP256R1Field.Negate(array12, array12);
				Nat256.Mul(array8, array12, array);
				SecP256R1Field.Reduce32(Nat256.AddBothTo(array13, array13, array12), array12);
				SecP256R1FieldElement secP256R1FieldElement7 = new SecP256R1FieldElement(array4);
				SecP256R1Field.Square(array10, secP256R1FieldElement7.x);
				SecP256R1Field.Subtract(secP256R1FieldElement7.x, array12, secP256R1FieldElement7.x);
				SecP256R1FieldElement secP256R1FieldElement8 = new SecP256R1FieldElement(array12);
				SecP256R1Field.Subtract(array13, secP256R1FieldElement7.x, secP256R1FieldElement8.x);
				SecP256R1Field.MultiplyAddToExt(secP256R1FieldElement8.x, array10, array);
				SecP256R1Field.Reduce(array, secP256R1FieldElement8.x);
				SecP256R1FieldElement secP256R1FieldElement9 = new SecP256R1FieldElement(array9);
				if (!isOne)
				{
					SecP256R1Field.Multiply(secP256R1FieldElement9.x, secP256R1FieldElement5.x, secP256R1FieldElement9.x);
				}
				if (!isOne2)
				{
					SecP256R1Field.Multiply(secP256R1FieldElement9.x, secP256R1FieldElement6.x, secP256R1FieldElement9.x);
				}
				ECFieldElement[] zs = new ECFieldElement[]
				{
					secP256R1FieldElement9
				};
				return new SecP256R1Point(curve, secP256R1FieldElement7, secP256R1FieldElement8, zs, base.IsCompressed);
			}
			if (Nat256.IsZero(array10))
			{
				return this.Twice();
			}
			return curve.Infinity;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000FA4E0 File Offset: 0x000F86E0
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			SecP256R1FieldElement secP256R1FieldElement = (SecP256R1FieldElement)base.RawYCoord;
			if (secP256R1FieldElement.IsZero)
			{
				return curve.Infinity;
			}
			SecP256R1FieldElement secP256R1FieldElement2 = (SecP256R1FieldElement)base.RawXCoord;
			SecP256R1FieldElement secP256R1FieldElement3 = (SecP256R1FieldElement)base.RawZCoords[0];
			uint[] array = Nat256.Create();
			uint[] array2 = Nat256.Create();
			uint[] array3 = Nat256.Create();
			SecP256R1Field.Square(secP256R1FieldElement.x, array3);
			uint[] array4 = Nat256.Create();
			SecP256R1Field.Square(array3, array4);
			bool isOne = secP256R1FieldElement3.IsOne;
			uint[] array5 = secP256R1FieldElement3.x;
			if (!isOne)
			{
				array5 = array2;
				SecP256R1Field.Square(secP256R1FieldElement3.x, array5);
			}
			SecP256R1Field.Subtract(secP256R1FieldElement2.x, array5, array);
			uint[] array6 = array2;
			SecP256R1Field.Add(secP256R1FieldElement2.x, array5, array6);
			SecP256R1Field.Multiply(array6, array, array6);
			SecP256R1Field.Reduce32(Nat256.AddBothTo(array6, array6, array6), array6);
			uint[] array7 = array3;
			SecP256R1Field.Multiply(array3, secP256R1FieldElement2.x, array7);
			SecP256R1Field.Reduce32(Nat.ShiftUpBits(8, array7, 2, 0U), array7);
			SecP256R1Field.Reduce32(Nat.ShiftUpBits(8, array4, 3, 0U, array), array);
			SecP256R1FieldElement secP256R1FieldElement4 = new SecP256R1FieldElement(array4);
			SecP256R1Field.Square(array6, secP256R1FieldElement4.x);
			SecP256R1Field.Subtract(secP256R1FieldElement4.x, array7, secP256R1FieldElement4.x);
			SecP256R1Field.Subtract(secP256R1FieldElement4.x, array7, secP256R1FieldElement4.x);
			SecP256R1FieldElement secP256R1FieldElement5 = new SecP256R1FieldElement(array7);
			SecP256R1Field.Subtract(array7, secP256R1FieldElement4.x, secP256R1FieldElement5.x);
			SecP256R1Field.Multiply(secP256R1FieldElement5.x, array6, secP256R1FieldElement5.x);
			SecP256R1Field.Subtract(secP256R1FieldElement5.x, array, secP256R1FieldElement5.x);
			SecP256R1FieldElement secP256R1FieldElement6 = new SecP256R1FieldElement(array6);
			SecP256R1Field.Twice(secP256R1FieldElement.x, secP256R1FieldElement6.x);
			if (!isOne)
			{
				SecP256R1Field.Multiply(secP256R1FieldElement6.x, secP256R1FieldElement3.x, secP256R1FieldElement6.x);
			}
			return new SecP256R1Point(curve, secP256R1FieldElement4, secP256R1FieldElement5, new ECFieldElement[]
			{
				secP256R1FieldElement6
			}, base.IsCompressed);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000FA6D8 File Offset: 0x000F88D8
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

		// Token: 0x06002268 RID: 8808 RVA: 0x000F2B70 File Offset: 0x000F0D70
		public override ECPoint ThreeTimes()
		{
			if (base.IsInfinity || base.RawYCoord.IsZero)
			{
				return this;
			}
			return this.Twice().Add(this);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000FA724 File Offset: 0x000F8924
		public override ECPoint Negate()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			return new SecP256R1Point(this.Curve, base.RawXCoord, base.RawYCoord.Negate(), base.RawZCoords, base.IsCompressed);
		}
	}
}
