using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000379 RID: 889
	internal class SecT163K1Curve : AbstractF2mCurve
	{
		// Token: 0x060023E3 RID: 9187 RVA: 0x00100044 File Offset: 0x000FE244
		public SecT163K1Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.m_a;
			this.m_order = new BigInteger(1, Hex.Decode("04000000000000000000020108A2E0CC0D99F8A5EF"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x001000B2 File Offset: 0x000FE2B2
		protected override ECCurve CloneCurve()
		{
			return new SecT163K1Curve();
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x001000C0 File Offset: 0x000FE2C0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000FFE03 File Offset: 0x000FE003
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x001000C8 File Offset: 0x000FE2C8
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x001000D0 File Offset: 0x000FE2D0
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163K1Point(this, x, y, withCompression);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x001000DB File Offset: 0x000FE2DB
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000FFE03 File Offset: 0x000FE003
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000A6441 File Offset: 0x000A4641
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x001000E8 File Offset: 0x000FE2E8
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 3 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 3;
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 3;
			}
			return new SecT163K1Curve.SecT163K1LookupTable(this, array, len);
		}

		// Token: 0x04001953 RID: 6483
		private const int SECT163K1_DEFAULT_COORDS = 6;

		// Token: 0x04001954 RID: 6484
		private const int SECT163K1_FE_LONGS = 3;

		// Token: 0x04001955 RID: 6485
		protected readonly SecT163K1Point m_infinity;

		// Token: 0x02000906 RID: 2310
		private class SecT163K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DD4 RID: 19924 RVA: 0x001B2369 File Offset: 0x001B0569
			internal SecT163K1LookupTable(SecT163K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2A RID: 3114
			// (get) Token: 0x06004DD5 RID: 19925 RVA: 0x001B2386 File Offset: 0x001B0586
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DD6 RID: 19926 RVA: 0x001B2390 File Offset: 0x001B0590
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat192.Create64();
				ulong[] array2 = Nat192.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 3; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 3 + j] & num2);
					}
					num += 6;
				}
				return this.m_outer.CreateRawPoint(new SecT163FieldElement(array), new SecT163FieldElement(array2), false);
			}

			// Token: 0x0400348A RID: 13450
			private readonly SecT163K1Curve m_outer;

			// Token: 0x0400348B RID: 13451
			private readonly ulong[] m_table;

			// Token: 0x0400348C RID: 13452
			private readonly int m_size;
		}
	}
}
