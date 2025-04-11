using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200030B RID: 779
	public class F2mCurve : AbstractF2mCurve
	{
		// Token: 0x06001DEB RID: 7659 RVA: 0x000E40A0 File Offset: 0x000E22A0
		[Obsolete("Use constructor taking order/cofactor")]
		public F2mCurve(int m, int k, BigInteger a, BigInteger b) : this(m, k, 0, 0, a, b, null, null)
		{
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000E40BC File Offset: 0x000E22BC
		public F2mCurve(int m, int k, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : this(m, k, 0, 0, a, b, order, cofactor)
		{
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000E40DC File Offset: 0x000E22DC
		[Obsolete("Use constructor taking order/cofactor")]
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b) : this(m, k1, k2, k3, a, b, null, null)
		{
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000E40FC File Offset: 0x000E22FC
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : base(m, k1, k2, k3)
		{
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_infinity = new F2mPoint(this, null, null, false);
			if (k1 == 0)
			{
				throw new ArgumentException("k1 must be > 0");
			}
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("k3 must be 0 if k2 == 0");
				}
			}
			else
			{
				if (k2 <= k1)
				{
					throw new ArgumentException("k2 must be > k1");
				}
				if (k3 <= k2)
				{
					throw new ArgumentException("k3 must be > k2");
				}
			}
			this.m_a = this.FromBigInteger(a);
			this.m_b = this.FromBigInteger(b);
			this.m_coord = 6;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000E41B4 File Offset: 0x000E23B4
		protected F2mCurve(int m, int k1, int k2, int k3, ECFieldElement a, ECFieldElement b, BigInteger order, BigInteger cofactor) : base(m, k1, k2, k3)
		{
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_infinity = new F2mPoint(this, null, null, false);
			this.m_a = a;
			this.m_b = b;
			this.m_coord = 6;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000E421F File Offset: 0x000E241F
		protected override ECCurve CloneCurve()
		{
			return new F2mCurve(this.m, this.k1, this.k2, this.k3, this.m_a, this.m_b, this.m_order, this.m_cofactor);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000E4256 File Offset: 0x000E2456
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord <= 1 || coord == 6;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000E4263 File Offset: 0x000E2463
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			if (this.IsKoblitz)
			{
				return new WTauNafMultiplier();
			}
			return base.CreateDefaultMultiplier();
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000E4279 File Offset: 0x000E2479
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000E4281 File Offset: 0x000E2481
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, x);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000E42A1 File Offset: 0x000E24A1
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new F2mPoint(this, x, y, withCompression);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000E42AC File Offset: 0x000E24AC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new F2mPoint(this, x, y, zs, withCompression);
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x000E42B9 File Offset: 0x000E24B9
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000E4279 File Offset: 0x000E2479
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000E42C1 File Offset: 0x000E24C1
		public bool IsTrinomial()
		{
			return this.k2 == 0 && this.k3 == 0;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000E42D6 File Offset: 0x000E24D6
		public int K1
		{
			get
			{
				return this.k1;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000E42DE File Offset: 0x000E24DE
		public int K2
		{
			get
			{
				return this.k2;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000E42E6 File Offset: 0x000E24E6
		public int K3
		{
			get
			{
				return this.k3;
			}
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000E42F0 File Offset: 0x000E24F0
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			int num = (this.m + 63) / 64;
			long[] array = new long[len * num * 2];
			int num2 = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				((F2mFieldElement)ecpoint.RawXCoord).x.CopyTo(array, num2);
				num2 += num;
				((F2mFieldElement)ecpoint.RawYCoord).x.CopyTo(array, num2);
				num2 += num;
			}
			return new F2mCurve.DefaultF2mLookupTable(this, array, len);
		}

		// Token: 0x04001826 RID: 6182
		private const int F2M_DEFAULT_COORDS = 6;

		// Token: 0x04001827 RID: 6183
		private readonly int m;

		// Token: 0x04001828 RID: 6184
		private readonly int k1;

		// Token: 0x04001829 RID: 6185
		private readonly int k2;

		// Token: 0x0400182A RID: 6186
		private readonly int k3;

		// Token: 0x0400182B RID: 6187
		protected readonly F2mPoint m_infinity;

		// Token: 0x020008E9 RID: 2281
		private class DefaultF2mLookupTable : ECLookupTable
		{
			// Token: 0x06004D8E RID: 19854 RVA: 0x001B0FA6 File Offset: 0x001AF1A6
			internal DefaultF2mLookupTable(F2mCurve outer, long[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06004D8F RID: 19855 RVA: 0x001B0FC3 File Offset: 0x001AF1C3
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004D90 RID: 19856 RVA: 0x001B0FCC File Offset: 0x001AF1CC
			public virtual ECPoint Lookup(int index)
			{
				int m = this.m_outer.m;
				int[] array2;
				if (!this.m_outer.IsTrinomial())
				{
					int[] array = new int[3];
					array[0] = this.m_outer.k1;
					array[1] = this.m_outer.k2;
					array2 = array;
					array[2] = this.m_outer.k3;
				}
				else
				{
					(array2 = new int[1])[0] = this.m_outer.k1;
				}
				int[] ks = array2;
				int num = (this.m_outer.m + 63) / 64;
				long[] array3 = new long[num];
				long[] array4 = new long[num];
				int num2 = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					long num3 = (long)((i ^ index) - 1 >> 31);
					for (int j = 0; j < num; j++)
					{
						array3[j] ^= (this.m_table[num2 + j] & num3);
						array4[j] ^= (this.m_table[num2 + num + j] & num3);
					}
					num2 += num * 2;
				}
				ECFieldElement x = new F2mFieldElement(m, ks, new LongArray(array3));
				ECFieldElement y = new F2mFieldElement(m, ks, new LongArray(array4));
				return this.m_outer.CreateRawPoint(x, y, false);
			}

			// Token: 0x04003433 RID: 13363
			private readonly F2mCurve m_outer;

			// Token: 0x04003434 RID: 13364
			private readonly long[] m_table;

			// Token: 0x04003435 RID: 13365
			private readonly int m_size;
		}
	}
}
