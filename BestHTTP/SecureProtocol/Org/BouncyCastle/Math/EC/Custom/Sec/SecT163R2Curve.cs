using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200037D RID: 893
	internal class SecT163R2Curve : AbstractF2mCurve
	{
		// Token: 0x06002416 RID: 9238 RVA: 0x00100E98 File Offset: 0x000FF098
		public SecT163R2Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163R2Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("020A601907B8C953CA1481EB10512F78744A3205FD")));
			this.m_order = new BigInteger(1, Hex.Decode("040000000000000000000292FE77E70C12A4234C33"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00100F16 File Offset: 0x000FF116
		protected override ECCurve CloneCurve()
		{
			return new SecT163R2Curve();
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x00100F1D File Offset: 0x000FF11D
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000FFE03 File Offset: 0x000FE003
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x001000C8 File Offset: 0x000FE2C8
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00100F25 File Offset: 0x000FF125
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163R2Point(this, x, y, withCompression);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00100F30 File Offset: 0x000FF130
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000FFE03 File Offset: 0x000FE003
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000A6441 File Offset: 0x000A4641
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00100F40 File Offset: 0x000FF140
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
			return new SecT163R2Curve.SecT163R2LookupTable(this, array, len);
		}

		// Token: 0x04001959 RID: 6489
		private const int SECT163R2_DEFAULT_COORDS = 6;

		// Token: 0x0400195A RID: 6490
		private const int SECT163R2_FE_LONGS = 3;

		// Token: 0x0400195B RID: 6491
		protected readonly SecT163R2Point m_infinity;

		// Token: 0x02000908 RID: 2312
		private class SecT163R2LookupTable : ECLookupTable
		{
			// Token: 0x06004DDA RID: 19930 RVA: 0x001B24E9 File Offset: 0x001B06E9
			internal SecT163R2LookupTable(SecT163R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2C RID: 3116
			// (get) Token: 0x06004DDB RID: 19931 RVA: 0x001B2506 File Offset: 0x001B0706
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DDC RID: 19932 RVA: 0x001B2510 File Offset: 0x001B0710
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

			// Token: 0x04003490 RID: 13456
			private readonly SecT163R2Curve m_outer;

			// Token: 0x04003491 RID: 13457
			private readonly ulong[] m_table;

			// Token: 0x04003492 RID: 13458
			private readonly int m_size;
		}
	}
}
