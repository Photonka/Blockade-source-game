using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000309 RID: 777
	public class FpCurve : AbstractFpCurve
	{
		// Token: 0x06001DD5 RID: 7637 RVA: 0x000E3BBB File Offset: 0x000E1DBB
		[Obsolete("Use constructor taking order/cofactor")]
		public FpCurve(BigInteger q, BigInteger a, BigInteger b) : this(q, a, b, null, null)
		{
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000E3BC8 File Offset: 0x000E1DC8
		public FpCurve(BigInteger q, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : base(q)
		{
			this.m_q = q;
			this.m_r = FpFieldElement.CalculateResidue(q);
			this.m_infinity = new FpPoint(this, null, null, false);
			this.m_a = this.FromBigInteger(a);
			this.m_b = this.FromBigInteger(b);
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_coord = 4;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000E3C2F File Offset: 0x000E1E2F
		[Obsolete("Use constructor taking order/cofactor")]
		protected FpCurve(BigInteger q, BigInteger r, ECFieldElement a, ECFieldElement b) : this(q, r, a, b, null, null)
		{
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000E3C40 File Offset: 0x000E1E40
		protected FpCurve(BigInteger q, BigInteger r, ECFieldElement a, ECFieldElement b, BigInteger order, BigInteger cofactor) : base(q)
		{
			this.m_q = q;
			this.m_r = r;
			this.m_infinity = new FpPoint(this, null, null, false);
			this.m_a = a;
			this.m_b = b;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_coord = 4;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000E3C97 File Offset: 0x000E1E97
		protected override ECCurve CloneCurve()
		{
			return new FpCurve(this.m_q, this.m_r, this.m_a, this.m_b, this.m_order, this.m_cofactor);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000E3CC2 File Offset: 0x000E1EC2
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord <= 2 || coord == 4;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000E3CCF File Offset: 0x000E1ECF
		public virtual BigInteger Q
		{
			get
			{
				return this.m_q;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000E3CD7 File Offset: 0x000E1ED7
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000E3CDF File Offset: 0x000E1EDF
		public override int FieldSize
		{
			get
			{
				return this.m_q.BitLength;
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000E3CEC File Offset: 0x000E1EEC
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new FpFieldElement(this.m_q, this.m_r, x);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000E3D00 File Offset: 0x000E1F00
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new FpPoint(this, x, y, withCompression);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000E3D0B File Offset: 0x000E1F0B
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new FpPoint(this, x, y, zs, withCompression);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000E3D18 File Offset: 0x000E1F18
		public override ECPoint ImportPoint(ECPoint p)
		{
			if (this != p.Curve && this.CoordinateSystem == 2 && !p.IsInfinity)
			{
				int coordinateSystem = p.Curve.CoordinateSystem;
				if (coordinateSystem - 2 <= 2)
				{
					return new FpPoint(this, this.FromBigInteger(p.RawXCoord.ToBigInteger()), this.FromBigInteger(p.RawYCoord.ToBigInteger()), new ECFieldElement[]
					{
						this.FromBigInteger(p.GetZCoord(0).ToBigInteger())
					}, p.IsCompressed);
				}
			}
			return base.ImportPoint(p);
		}

		// Token: 0x04001821 RID: 6177
		private const int FP_DEFAULT_COORDS = 4;

		// Token: 0x04001822 RID: 6178
		protected readonly BigInteger m_q;

		// Token: 0x04001823 RID: 6179
		protected readonly BigInteger m_r;

		// Token: 0x04001824 RID: 6180
		protected readonly FpPoint m_infinity;
	}
}
