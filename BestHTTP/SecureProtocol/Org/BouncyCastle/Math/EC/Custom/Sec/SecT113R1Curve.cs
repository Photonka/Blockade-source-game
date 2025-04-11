using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200036D RID: 877
	internal class SecT113R1Curve : AbstractF2mCurve
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x000FCD2C File Offset: 0x000FAF2C
		public SecT113R1Curve() : base(113, 9, 0, 0)
		{
			this.m_infinity = new SecT113R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("003088250CA6E7C7FE649CE85820F7")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("00E8BEE4D3E2260744188BE0E9C723")));
			this.m_order = new BigInteger(1, Hex.Decode("0100000000000000D9CCEC8A39E56F"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000FCDB3 File Offset: 0x000FAFB3
		protected override ECCurve CloneCurve()
		{
			return new SecT113R1Curve();
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000FCDC3 File Offset: 0x000FAFC3
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000FCDCB File Offset: 0x000FAFCB
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT113FieldElement(x);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000FCDD3 File Offset: 0x000FAFD3
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT113R1Point(this, x, y, withCompression);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000FCDDE File Offset: 0x000FAFDE
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT113R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000FCCE5 File Offset: 0x000FAEE5
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000FCDEC File Offset: 0x000FAFEC
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 2 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 2;
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 2;
			}
			return new SecT113R1Curve.SecT113R1LookupTable(this, array, len);
		}

		// Token: 0x0400193F RID: 6463
		private const int SECT113R1_DEFAULT_COORDS = 6;

		// Token: 0x04001940 RID: 6464
		private const int SECT113R1_FE_LONGS = 2;

		// Token: 0x04001941 RID: 6465
		protected readonly SecT113R1Point m_infinity;

		// Token: 0x02000902 RID: 2306
		private class SecT113R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DC8 RID: 19912 RVA: 0x001B206B File Offset: 0x001B026B
			internal SecT113R1LookupTable(SecT113R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C26 RID: 3110
			// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x001B2088 File Offset: 0x001B0288
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DCA RID: 19914 RVA: 0x001B2090 File Offset: 0x001B0290
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat128.Create64();
				ulong[] array2 = Nat128.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 2; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 2 + j] & num2);
					}
					num += 4;
				}
				return this.m_outer.CreateRawPoint(new SecT113FieldElement(array), new SecT113FieldElement(array2), false);
			}

			// Token: 0x0400347E RID: 13438
			private readonly SecT113R1Curve m_outer;

			// Token: 0x0400347F RID: 13439
			private readonly ulong[] m_table;

			// Token: 0x04003480 RID: 13440
			private readonly int m_size;
		}
	}
}
