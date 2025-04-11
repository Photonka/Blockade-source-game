using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000373 RID: 883
	internal class SecT131R1Curve : AbstractF2mCurve
	{
		// Token: 0x0600237C RID: 9084 RVA: 0x000FE6C8 File Offset: 0x000FC8C8
		public SecT131R1Curve() : base(131, 2, 3, 8)
		{
			this.m_infinity = new SecT131R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("07A11B09A76B562144418FF3FF8C2570B8")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0217C05610884B63B9C6C7291678F9D341")));
			this.m_order = new BigInteger(1, Hex.Decode("0400000000000000023123953A9464B54D"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000FE751 File Offset: 0x000FC951
		protected override ECCurve CloneCurve()
		{
			return new SecT131R1Curve();
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000FE758 File Offset: 0x000FC958
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000FE489 File Offset: 0x000FC689
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000FE760 File Offset: 0x000FC960
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT131FieldElement(x);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000FE768 File Offset: 0x000FC968
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT131R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000FE773 File Offset: 0x000FC973
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT131R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000FE489 File Offset: 0x000FC689
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000AA054 File Offset: 0x000A8254
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000FE780 File Offset: 0x000FC980
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
			return new SecT131R1Curve.SecT131R1LookupTable(this, array, len);
		}

		// Token: 0x04001949 RID: 6473
		private const int SECT131R1_DEFAULT_COORDS = 6;

		// Token: 0x0400194A RID: 6474
		private const int SECT131R1_FE_LONGS = 3;

		// Token: 0x0400194B RID: 6475
		protected readonly SecT131R1Point m_infinity;

		// Token: 0x02000904 RID: 2308
		private class SecT131R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DCE RID: 19918 RVA: 0x001B21E9 File Offset: 0x001B03E9
			internal SecT131R1LookupTable(SecT131R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C28 RID: 3112
			// (get) Token: 0x06004DCF RID: 19919 RVA: 0x001B2206 File Offset: 0x001B0406
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DD0 RID: 19920 RVA: 0x001B2210 File Offset: 0x001B0410
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

			// Token: 0x04003484 RID: 13444
			private readonly SecT131R1Curve m_outer;

			// Token: 0x04003485 RID: 13445
			private readonly ulong[] m_table;

			// Token: 0x04003486 RID: 13446
			private readonly int m_size;
		}
	}
}
