using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000314 RID: 788
	public abstract class AbstractFpPoint : ECPointBase
	{
		// Token: 0x06001E93 RID: 7827 RVA: 0x000E5C24 File Offset: 0x000E3E24
		protected AbstractFpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000E5C31 File Offset: 0x000E3E31
		protected AbstractFpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000E5C40 File Offset: 0x000E3E40
		protected internal override bool CompressionYTilde
		{
			get
			{
				return this.AffineYCoord.TestBitZero();
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000E5C50 File Offset: 0x000E3E50
		protected override bool SatisfiesCurveEquation()
		{
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = this.Curve.A;
			ECFieldElement ecfieldElement2 = this.Curve.B;
			ECFieldElement ecfieldElement3 = rawYCoord.Square();
			switch (this.CurveCoordinateSystem)
			{
			case 0:
				break;
			case 1:
			{
				ECFieldElement ecfieldElement4 = base.RawZCoords[0];
				if (!ecfieldElement4.IsOne)
				{
					ECFieldElement b = ecfieldElement4.Square();
					ECFieldElement b2 = ecfieldElement4.Multiply(b);
					ecfieldElement3 = ecfieldElement3.Multiply(ecfieldElement4);
					ecfieldElement = ecfieldElement.Multiply(b);
					ecfieldElement2 = ecfieldElement2.Multiply(b2);
				}
				break;
			}
			case 2:
			case 3:
			case 4:
			{
				ECFieldElement ecfieldElement5 = base.RawZCoords[0];
				if (!ecfieldElement5.IsOne)
				{
					ECFieldElement ecfieldElement6 = ecfieldElement5.Square();
					ECFieldElement b3 = ecfieldElement6.Square();
					ECFieldElement b4 = ecfieldElement6.Multiply(b3);
					ecfieldElement = ecfieldElement.Multiply(b3);
					ecfieldElement2 = ecfieldElement2.Multiply(b4);
				}
				break;
			}
			default:
				throw new InvalidOperationException("unsupported coordinate system");
			}
			ECFieldElement other = rawXCoord.Square().Add(ecfieldElement).Multiply(rawXCoord).Add(ecfieldElement2);
			return ecfieldElement3.Equals(other);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000E5D60 File Offset: 0x000E3F60
		public override ECPoint Subtract(ECPoint b)
		{
			if (b.IsInfinity)
			{
				return this;
			}
			return this.Add(b.Negate());
		}
	}
}
