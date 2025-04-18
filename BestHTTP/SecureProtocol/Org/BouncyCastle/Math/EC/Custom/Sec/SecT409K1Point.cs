﻿using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000398 RID: 920
	internal class SecT409K1Point : AbstractF2mPoint
	{
		// Token: 0x060025FB RID: 9723 RVA: 0x00108006 File Offset: 0x00106206
		public SecT409K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000FCE62 File Offset: 0x000FB062
		public SecT409K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000E731C File Offset: 0x000E551C
		internal SecT409K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00108012 File Offset: 0x00106212
		protected override ECPoint Detach()
		{
			return new SecT409K1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x00108028 File Offset: 0x00106228
		public override ECFieldElement YCoord
		{
			get
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement rawYCoord = base.RawYCoord;
				if (base.IsInfinity || rawXCoord.IsZero)
				{
					return rawYCoord;
				}
				ECFieldElement ecfieldElement = rawYCoord.Add(rawXCoord).Multiply(rawXCoord);
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				if (!ecfieldElement2.IsOne)
				{
					ecfieldElement = ecfieldElement.Divide(ecfieldElement2);
				}
				return ecfieldElement;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00108080 File Offset: 0x00106280
		protected internal override bool CompressionYTilde
		{
			get
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				return !rawXCoord.IsZero && base.RawYCoord.TestBitZero() != rawXCoord.TestBitZero();
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x001080B4 File Offset: 0x001062B4
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
			ECCurve curve = this.Curve;
			ECFieldElement ecfieldElement = base.RawXCoord;
			ECFieldElement rawXCoord = b.RawXCoord;
			if (ecfieldElement.IsZero)
			{
				if (rawXCoord.IsZero)
				{
					return curve.Infinity;
				}
				return b.Add(this);
			}
			else
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				ECFieldElement rawYCoord2 = b.RawYCoord;
				ECFieldElement ecfieldElement3 = b.RawZCoords[0];
				bool isOne = ecfieldElement2.IsOne;
				ECFieldElement ecfieldElement4 = rawXCoord;
				ECFieldElement ecfieldElement5 = rawYCoord2;
				if (!isOne)
				{
					ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement2);
					ecfieldElement5 = ecfieldElement5.Multiply(ecfieldElement2);
				}
				bool isOne2 = ecfieldElement3.IsOne;
				ECFieldElement ecfieldElement6 = ecfieldElement;
				ECFieldElement ecfieldElement7 = rawYCoord;
				if (!isOne2)
				{
					ecfieldElement6 = ecfieldElement6.Multiply(ecfieldElement3);
					ecfieldElement7 = ecfieldElement7.Multiply(ecfieldElement3);
				}
				ECFieldElement ecfieldElement8 = ecfieldElement7.Add(ecfieldElement5);
				ECFieldElement ecfieldElement9 = ecfieldElement6.Add(ecfieldElement4);
				if (!ecfieldElement9.IsZero)
				{
					ECFieldElement ecfieldElement11;
					ECFieldElement y;
					ECFieldElement ecfieldElement12;
					if (rawXCoord.IsZero)
					{
						ECPoint ecpoint = this.Normalize();
						ecfieldElement = ecpoint.XCoord;
						ECFieldElement ycoord = ecpoint.YCoord;
						ECFieldElement b2 = rawYCoord2;
						ECFieldElement ecfieldElement10 = ycoord.Add(b2).Divide(ecfieldElement);
						ecfieldElement11 = ecfieldElement10.Square().Add(ecfieldElement10).Add(ecfieldElement);
						if (ecfieldElement11.IsZero)
						{
							return new SecT409K1Point(curve, ecfieldElement11, curve.B, base.IsCompressed);
						}
						y = ecfieldElement10.Multiply(ecfieldElement.Add(ecfieldElement11)).Add(ecfieldElement11).Add(ycoord).Divide(ecfieldElement11).Add(ecfieldElement11);
						ecfieldElement12 = curve.FromBigInteger(BigInteger.One);
					}
					else
					{
						ecfieldElement9 = ecfieldElement9.Square();
						ECFieldElement ecfieldElement13 = ecfieldElement8.Multiply(ecfieldElement6);
						ECFieldElement ecfieldElement14 = ecfieldElement8.Multiply(ecfieldElement4);
						ecfieldElement11 = ecfieldElement13.Multiply(ecfieldElement14);
						if (ecfieldElement11.IsZero)
						{
							return new SecT409K1Point(curve, ecfieldElement11, curve.B, base.IsCompressed);
						}
						ECFieldElement ecfieldElement15 = ecfieldElement8.Multiply(ecfieldElement9);
						if (!isOne2)
						{
							ecfieldElement15 = ecfieldElement15.Multiply(ecfieldElement3);
						}
						y = ecfieldElement14.Add(ecfieldElement9).SquarePlusProduct(ecfieldElement15, rawYCoord.Add(ecfieldElement2));
						ecfieldElement12 = ecfieldElement15;
						if (!isOne)
						{
							ecfieldElement12 = ecfieldElement12.Multiply(ecfieldElement2);
						}
					}
					return new SecT409K1Point(curve, ecfieldElement11, y, new ECFieldElement[]
					{
						ecfieldElement12
					}, base.IsCompressed);
				}
				if (ecfieldElement8.IsZero)
				{
					return this.Twice();
				}
				return curve.Infinity;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00108308 File Offset: 0x00106508
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return curve.Infinity;
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = base.RawZCoords[0];
			bool isOne = ecfieldElement.IsOne;
			ECFieldElement ecfieldElement2 = isOne ? ecfieldElement : ecfieldElement.Square();
			ECFieldElement ecfieldElement3;
			if (isOne)
			{
				ecfieldElement3 = rawYCoord.Square().Add(rawYCoord);
			}
			else
			{
				ecfieldElement3 = rawYCoord.Add(ecfieldElement).Multiply(rawYCoord);
			}
			if (ecfieldElement3.IsZero)
			{
				return new SecT409K1Point(curve, ecfieldElement3, curve.B, base.IsCompressed);
			}
			ECFieldElement ecfieldElement4 = ecfieldElement3.Square();
			ECFieldElement ecfieldElement5 = isOne ? ecfieldElement3 : ecfieldElement3.Multiply(ecfieldElement2);
			ECFieldElement ecfieldElement6 = rawYCoord.Add(rawXCoord).Square();
			ECFieldElement b = isOne ? ecfieldElement : ecfieldElement2.Square();
			ECFieldElement y = ecfieldElement6.Add(ecfieldElement3).Add(ecfieldElement2).Multiply(ecfieldElement6).Add(b).Add(ecfieldElement4).Add(ecfieldElement5);
			return new SecT409K1Point(curve, ecfieldElement4, y, new ECFieldElement[]
			{
				ecfieldElement5
			}, base.IsCompressed);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00108428 File Offset: 0x00106628
		public override ECPoint TwicePlus(ECPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this.Twice();
			}
			ECCurve curve = this.Curve;
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return b;
			}
			ECFieldElement rawXCoord2 = b.RawXCoord;
			ECFieldElement ecfieldElement = b.RawZCoords[0];
			if (rawXCoord2.IsZero || !ecfieldElement.IsOne)
			{
				return this.Twice().Add(b);
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement2 = base.RawZCoords[0];
			ECFieldElement rawYCoord2 = b.RawYCoord;
			ECFieldElement x = rawXCoord.Square();
			ECFieldElement ecfieldElement3 = rawYCoord.Square();
			ECFieldElement ecfieldElement4 = ecfieldElement2.Square();
			ECFieldElement b2 = rawYCoord.Multiply(ecfieldElement2);
			ECFieldElement b3 = ecfieldElement3.Add(b2);
			ECFieldElement ecfieldElement5 = rawYCoord2.AddOne();
			ECFieldElement ecfieldElement6 = ecfieldElement5.Multiply(ecfieldElement4).Add(ecfieldElement3).MultiplyPlusProduct(b3, x, ecfieldElement4);
			ECFieldElement ecfieldElement7 = rawXCoord2.Multiply(ecfieldElement4);
			ECFieldElement ecfieldElement8 = ecfieldElement7.Add(b3).Square();
			if (ecfieldElement8.IsZero)
			{
				if (ecfieldElement6.IsZero)
				{
					return b.Twice();
				}
				return curve.Infinity;
			}
			else
			{
				if (ecfieldElement6.IsZero)
				{
					return new SecT409K1Point(curve, ecfieldElement6, curve.B, base.IsCompressed);
				}
				ECFieldElement x2 = ecfieldElement6.Square().Multiply(ecfieldElement7);
				ECFieldElement ecfieldElement9 = ecfieldElement6.Multiply(ecfieldElement8).Multiply(ecfieldElement4);
				ECFieldElement y = ecfieldElement6.Add(ecfieldElement8).Square().MultiplyPlusProduct(b3, ecfieldElement5, ecfieldElement9);
				return new SecT409K1Point(curve, x2, y, new ECFieldElement[]
				{
					ecfieldElement9
				}, base.IsCompressed);
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x001085B0 File Offset: 0x001067B0
		public override ECPoint Negate()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return this;
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = base.RawZCoords[0];
			return new SecT409K1Point(this.Curve, rawXCoord, rawYCoord.Add(ecfieldElement), new ECFieldElement[]
			{
				ecfieldElement
			}, base.IsCompressed);
		}
	}
}
