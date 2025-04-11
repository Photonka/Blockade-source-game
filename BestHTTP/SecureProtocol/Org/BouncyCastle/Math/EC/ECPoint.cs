using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000312 RID: 786
	public abstract class ECPoint
	{
		// Token: 0x06001E61 RID: 7777 RVA: 0x000E5550 File Offset: 0x000E3750
		protected static ECFieldElement[] GetInitialZCoords(ECCurve curve)
		{
			int num = (curve == null) ? 0 : curve.CoordinateSystem;
			if (num == 0 || num == 5)
			{
				return ECPoint.EMPTY_ZS;
			}
			ECFieldElement ecfieldElement = curve.FromBigInteger(BigInteger.One);
			switch (num)
			{
			case 1:
			case 2:
			case 6:
				return new ECFieldElement[]
				{
					ecfieldElement
				};
			case 3:
				return new ECFieldElement[]
				{
					ecfieldElement,
					ecfieldElement,
					ecfieldElement
				};
			case 4:
				return new ECFieldElement[]
				{
					ecfieldElement,
					curve.A
				};
			}
			throw new ArgumentException("unknown coordinate system");
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000E55E1 File Offset: 0x000E37E1
		protected ECPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : this(curve, x, y, ECPoint.GetInitialZCoords(curve), withCompression)
		{
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000E55F4 File Offset: 0x000E37F4
		internal ECPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			this.m_curve = curve;
			this.m_x = x;
			this.m_y = y;
			this.m_zs = zs;
			this.m_withCompression = withCompression;
		}

		// Token: 0x06001E64 RID: 7780
		protected abstract bool SatisfiesCurveEquation();

		// Token: 0x06001E65 RID: 7781 RVA: 0x000E5624 File Offset: 0x000E3824
		protected virtual bool SatisfiesOrder()
		{
			if (BigInteger.One.Equals(this.Curve.Cofactor))
			{
				return true;
			}
			BigInteger order = this.Curve.Order;
			return order == null || ECAlgorithms.ReferenceMultiply(this, order).IsInfinity;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000E5667 File Offset: 0x000E3867
		public ECPoint GetDetachedPoint()
		{
			return this.Normalize().Detach();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000E5674 File Offset: 0x000E3874
		public virtual ECCurve Curve
		{
			get
			{
				return this.m_curve;
			}
		}

		// Token: 0x06001E68 RID: 7784
		protected abstract ECPoint Detach();

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000E567C File Offset: 0x000E387C
		protected virtual int CurveCoordinateSystem
		{
			get
			{
				if (this.m_curve != null)
				{
					return this.m_curve.CoordinateSystem;
				}
				return 0;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000E5693 File Offset: 0x000E3893
		public virtual ECFieldElement AffineXCoord
		{
			get
			{
				this.CheckNormalized();
				return this.XCoord;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000E56A1 File Offset: 0x000E38A1
		public virtual ECFieldElement AffineYCoord
		{
			get
			{
				this.CheckNormalized();
				return this.YCoord;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000E56AF File Offset: 0x000E38AF
		public virtual ECFieldElement XCoord
		{
			get
			{
				return this.m_x;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000E56B7 File Offset: 0x000E38B7
		public virtual ECFieldElement YCoord
		{
			get
			{
				return this.m_y;
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000E56BF File Offset: 0x000E38BF
		public virtual ECFieldElement GetZCoord(int index)
		{
			if (index >= 0 && index < this.m_zs.Length)
			{
				return this.m_zs[index];
			}
			return null;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000E56DC File Offset: 0x000E38DC
		public virtual ECFieldElement[] GetZCoords()
		{
			int num = this.m_zs.Length;
			if (num == 0)
			{
				return this.m_zs;
			}
			ECFieldElement[] array = new ECFieldElement[num];
			Array.Copy(this.m_zs, 0, array, 0, num);
			return array;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000E56AF File Offset: 0x000E38AF
		protected internal ECFieldElement RawXCoord
		{
			get
			{
				return this.m_x;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000E56B7 File Offset: 0x000E38B7
		protected internal ECFieldElement RawYCoord
		{
			get
			{
				return this.m_y;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000E5713 File Offset: 0x000E3913
		protected internal ECFieldElement[] RawZCoords
		{
			get
			{
				return this.m_zs;
			}
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000E571B File Offset: 0x000E391B
		protected virtual void CheckNormalized()
		{
			if (!this.IsNormalized())
			{
				throw new InvalidOperationException("point not in normal form");
			}
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000E5730 File Offset: 0x000E3930
		public virtual bool IsNormalized()
		{
			int curveCoordinateSystem = this.CurveCoordinateSystem;
			return curveCoordinateSystem == 0 || curveCoordinateSystem == 5 || this.IsInfinity || this.RawZCoords[0].IsOne;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000E5764 File Offset: 0x000E3964
		public virtual ECPoint Normalize()
		{
			if (this.IsInfinity)
			{
				return this;
			}
			int curveCoordinateSystem = this.CurveCoordinateSystem;
			if (curveCoordinateSystem == 0 || curveCoordinateSystem == 5)
			{
				return this;
			}
			ECFieldElement ecfieldElement = this.RawZCoords[0];
			if (ecfieldElement.IsOne)
			{
				return this;
			}
			return this.Normalize(ecfieldElement.Invert());
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000E57AC File Offset: 0x000E39AC
		internal virtual ECPoint Normalize(ECFieldElement zInv)
		{
			switch (this.CurveCoordinateSystem)
			{
			case 1:
			case 6:
				return this.CreateScaledPoint(zInv, zInv);
			case 2:
			case 3:
			case 4:
			{
				ECFieldElement ecfieldElement = zInv.Square();
				ECFieldElement sy = ecfieldElement.Multiply(zInv);
				return this.CreateScaledPoint(ecfieldElement, sy);
			}
			}
			throw new InvalidOperationException("not a projective coordinate system");
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000E580D File Offset: 0x000E3A0D
		protected virtual ECPoint CreateScaledPoint(ECFieldElement sx, ECFieldElement sy)
		{
			return this.Curve.CreateRawPoint(this.RawXCoord.Multiply(sx), this.RawYCoord.Multiply(sy), this.IsCompressed);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000E5838 File Offset: 0x000E3A38
		public bool IsInfinity
		{
			get
			{
				return this.m_x == null && this.m_y == null;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x000E584D File Offset: 0x000E3A4D
		public bool IsCompressed
		{
			get
			{
				return this.m_withCompression;
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000E5855 File Offset: 0x000E3A55
		public bool IsValid()
		{
			return this.ImplIsValid(false, true);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000E585F File Offset: 0x000E3A5F
		internal bool IsValidPartial()
		{
			return this.ImplIsValid(false, false);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000E586C File Offset: 0x000E3A6C
		internal bool ImplIsValid(bool decompressed, bool checkOrder)
		{
			if (this.IsInfinity)
			{
				return true;
			}
			ECPoint.ValidityCallback callback = new ECPoint.ValidityCallback(this, decompressed, checkOrder);
			return !((ValidityPreCompInfo)this.Curve.Precompute(this, ValidityPreCompInfo.PRECOMP_NAME, callback)).HasFailed();
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000E58AB File Offset: 0x000E3AAB
		public virtual ECPoint ScaleX(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord.Multiply(scale), this.RawYCoord, this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000E58E0 File Offset: 0x000E3AE0
		public virtual ECPoint ScaleY(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord, this.RawYCoord.Multiply(scale), this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000E5915 File Offset: 0x000E3B15
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECPoint);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000E5924 File Offset: 0x000E3B24
		public virtual bool Equals(ECPoint other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			ECCurve curve = this.Curve;
			ECCurve curve2 = other.Curve;
			bool flag = curve == null;
			bool flag2 = curve2 == null;
			bool isInfinity = this.IsInfinity;
			bool isInfinity2 = other.IsInfinity;
			if (isInfinity || isInfinity2)
			{
				return isInfinity && isInfinity2 && (flag || flag2 || curve.Equals(curve2));
			}
			ECPoint ecpoint = this;
			ECPoint ecpoint2 = other;
			if (!flag || !flag2)
			{
				if (flag)
				{
					ecpoint2 = ecpoint2.Normalize();
				}
				else if (flag2)
				{
					ecpoint = ecpoint.Normalize();
				}
				else
				{
					if (!curve.Equals(curve2))
					{
						return false;
					}
					ECPoint[] array = new ECPoint[]
					{
						this,
						curve.ImportPoint(ecpoint2)
					};
					curve.NormalizeAll(array);
					ecpoint = array[0];
					ecpoint2 = array[1];
				}
			}
			return ecpoint.XCoord.Equals(ecpoint2.XCoord) && ecpoint.YCoord.Equals(ecpoint2.YCoord);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000E5A0C File Offset: 0x000E3C0C
		public override int GetHashCode()
		{
			ECCurve curve = this.Curve;
			int num = (curve == null) ? 0 : (~curve.GetHashCode());
			if (!this.IsInfinity)
			{
				ECPoint ecpoint = this.Normalize();
				num ^= ecpoint.XCoord.GetHashCode() * 17;
				num ^= ecpoint.YCoord.GetHashCode() * 257;
			}
			return num;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000E5A64 File Offset: 0x000E3C64
		public override string ToString()
		{
			if (this.IsInfinity)
			{
				return "INF";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			stringBuilder.Append(this.RawXCoord);
			stringBuilder.Append(',');
			stringBuilder.Append(this.RawYCoord);
			for (int i = 0; i < this.m_zs.Length; i++)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.m_zs[i]);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000E5AEB File Offset: 0x000E3CEB
		public virtual byte[] GetEncoded()
		{
			return this.GetEncoded(this.m_withCompression);
		}

		// Token: 0x06001E84 RID: 7812
		public abstract byte[] GetEncoded(bool compressed);

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001E85 RID: 7813
		protected internal abstract bool CompressionYTilde { get; }

		// Token: 0x06001E86 RID: 7814
		public abstract ECPoint Add(ECPoint b);

		// Token: 0x06001E87 RID: 7815
		public abstract ECPoint Subtract(ECPoint b);

		// Token: 0x06001E88 RID: 7816
		public abstract ECPoint Negate();

		// Token: 0x06001E89 RID: 7817 RVA: 0x000E5AFC File Offset: 0x000E3CFC
		public virtual ECPoint TimesPow2(int e)
		{
			if (e < 0)
			{
				throw new ArgumentException("cannot be negative", "e");
			}
			ECPoint ecpoint = this;
			while (--e >= 0)
			{
				ecpoint = ecpoint.Twice();
			}
			return ecpoint;
		}

		// Token: 0x06001E8A RID: 7818
		public abstract ECPoint Twice();

		// Token: 0x06001E8B RID: 7819
		public abstract ECPoint Multiply(BigInteger b);

		// Token: 0x06001E8C RID: 7820 RVA: 0x000E5B32 File Offset: 0x000E3D32
		public virtual ECPoint TwicePlus(ECPoint b)
		{
			return this.Twice().Add(b);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000E5B40 File Offset: 0x000E3D40
		public virtual ECPoint ThreeTimes()
		{
			return this.TwicePlus(this);
		}

		// Token: 0x04001836 RID: 6198
		protected static ECFieldElement[] EMPTY_ZS = new ECFieldElement[0];

		// Token: 0x04001837 RID: 6199
		protected internal readonly ECCurve m_curve;

		// Token: 0x04001838 RID: 6200
		protected internal readonly ECFieldElement m_x;

		// Token: 0x04001839 RID: 6201
		protected internal readonly ECFieldElement m_y;

		// Token: 0x0400183A RID: 6202
		protected internal readonly ECFieldElement[] m_zs;

		// Token: 0x0400183B RID: 6203
		protected internal readonly bool m_withCompression;

		// Token: 0x0400183C RID: 6204
		protected internal IDictionary m_preCompTable;

		// Token: 0x020008EA RID: 2282
		private class ValidityCallback : IPreCompCallback
		{
			// Token: 0x06004D91 RID: 19857 RVA: 0x001B10FE File Offset: 0x001AF2FE
			internal ValidityCallback(ECPoint outer, bool decompressed, bool checkOrder)
			{
				this.m_outer = outer;
				this.m_decompressed = decompressed;
				this.m_checkOrder = checkOrder;
			}

			// Token: 0x06004D92 RID: 19858 RVA: 0x001B111C File Offset: 0x001AF31C
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				ValidityPreCompInfo validityPreCompInfo = existing as ValidityPreCompInfo;
				if (validityPreCompInfo == null)
				{
					validityPreCompInfo = new ValidityPreCompInfo();
				}
				if (validityPreCompInfo.HasFailed())
				{
					return validityPreCompInfo;
				}
				if (!validityPreCompInfo.HasCurveEquationPassed())
				{
					if (!this.m_decompressed && !this.m_outer.SatisfiesCurveEquation())
					{
						validityPreCompInfo.ReportFailed();
						return validityPreCompInfo;
					}
					validityPreCompInfo.ReportCurveEquationPassed();
				}
				if (this.m_checkOrder && !validityPreCompInfo.HasOrderPassed())
				{
					if (!this.m_outer.SatisfiesOrder())
					{
						validityPreCompInfo.ReportFailed();
						return validityPreCompInfo;
					}
					validityPreCompInfo.ReportOrderPassed();
				}
				return validityPreCompInfo;
			}

			// Token: 0x04003436 RID: 13366
			private readonly ECPoint m_outer;

			// Token: 0x04003437 RID: 13367
			private readonly bool m_decompressed;

			// Token: 0x04003438 RID: 13368
			private readonly bool m_checkOrder;
		}
	}
}
