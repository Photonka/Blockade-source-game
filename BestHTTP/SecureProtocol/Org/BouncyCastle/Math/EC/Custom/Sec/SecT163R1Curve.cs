using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200037B RID: 891
	internal class SecT163R1Curve : AbstractF2mCurve
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x00100744 File Offset: 0x000FE944
		public SecT163R1Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("07B6882CAAEFA84F9554FF8428BD88E246D2782AE2")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0713612DCDDCB40AAB946BDA29CA91F73AF958AFD9")));
			this.m_order = new BigInteger(1, Hex.Decode("03FFFFFFFFFFFFFFFFFFFF48AAB689C29CA710279B"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x001007CD File Offset: 0x000FE9CD
		protected override ECCurve CloneCurve()
		{
			return new SecT163R1Curve();
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x001007D4 File Offset: 0x000FE9D4
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000FFE03 File Offset: 0x000FE003
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x001000C8 File Offset: 0x000FE2C8
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x001007DC File Offset: 0x000FE9DC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x001007E7 File Offset: 0x000FE9E7
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x000FFE03 File Offset: 0x000FE003
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000A6441 File Offset: 0x000A4641
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x001007F4 File Offset: 0x000FE9F4
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
			return new SecT163R1Curve.SecT163R1LookupTable(this, array, len);
		}

		// Token: 0x04001956 RID: 6486
		private const int SECT163R1_DEFAULT_COORDS = 6;

		// Token: 0x04001957 RID: 6487
		private const int SECT163R1_FE_LONGS = 3;

		// Token: 0x04001958 RID: 6488
		protected readonly SecT163R1Point m_infinity;

		// Token: 0x02000907 RID: 2311
		private class SecT163R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DD7 RID: 19927 RVA: 0x001B2429 File Offset: 0x001B0629
			internal SecT163R1LookupTable(SecT163R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2B RID: 3115
			// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x001B2446 File Offset: 0x001B0646
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DD9 RID: 19929 RVA: 0x001B2450 File Offset: 0x001B0650
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

			// Token: 0x0400348D RID: 13453
			private readonly SecT163R1Curve m_outer;

			// Token: 0x0400348E RID: 13454
			private readonly ulong[] m_table;

			// Token: 0x0400348F RID: 13455
			private readonly int m_size;
		}
	}
}
