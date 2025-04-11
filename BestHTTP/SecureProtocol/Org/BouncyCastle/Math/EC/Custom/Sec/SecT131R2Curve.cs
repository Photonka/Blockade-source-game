using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000375 RID: 885
	internal class SecT131R2Curve : AbstractF2mCurve
	{
		// Token: 0x06002395 RID: 9109 RVA: 0x000FEE24 File Offset: 0x000FD024
		public SecT131R2Curve() : base(131, 2, 3, 8)
		{
			this.m_infinity = new SecT131R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("03E5A88919D7CAFCBF415F07C2176573B2")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("04B8266A46C55657AC734CE38F018F2192")));
			this.m_order = new BigInteger(1, Hex.Decode("0400000000000000016954A233049BA98F"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000FEEAD File Offset: 0x000FD0AD
		protected override ECCurve CloneCurve()
		{
			return new SecT131R2Curve();
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x000FE489 File Offset: 0x000FC689
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000FE760 File Offset: 0x000FC960
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT131FieldElement(x);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000FEEB4 File Offset: 0x000FD0B4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT131R2Point(this, x, y, withCompression);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000FEEBF File Offset: 0x000FD0BF
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT131R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000FEECC File Offset: 0x000FD0CC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000FE489 File Offset: 0x000FC689
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000FEED4 File Offset: 0x000FD0D4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 3 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy64(((SecT131FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 3;
				Nat192.Copy64(((SecT131FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 3;
			}
			return new SecT131R2Curve.SecT131R2LookupTable(this, array, len);
		}

		// Token: 0x0400194C RID: 6476
		private const int SECT131R2_DEFAULT_COORDS = 6;

		// Token: 0x0400194D RID: 6477
		private const int SECT131R2_FE_LONGS = 3;

		// Token: 0x0400194E RID: 6478
		protected readonly SecT131R2Point m_infinity;

		// Token: 0x02000905 RID: 2309
		private class SecT131R2LookupTable : ECLookupTable
		{
			// Token: 0x06004DD1 RID: 19921 RVA: 0x001B22A9 File Offset: 0x001B04A9
			internal SecT131R2LookupTable(SecT131R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C29 RID: 3113
			// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x001B22C6 File Offset: 0x001B04C6
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DD3 RID: 19923 RVA: 0x001B22D0 File Offset: 0x001B04D0
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
				return this.m_outer.CreateRawPoint(new SecT131FieldElement(array), new SecT131FieldElement(array2), false);
			}

			// Token: 0x04003487 RID: 13447
			private readonly SecT131R2Curve m_outer;

			// Token: 0x04003488 RID: 13448
			private readonly ulong[] m_table;

			// Token: 0x04003489 RID: 13449
			private readonly int m_size;
		}
	}
}
