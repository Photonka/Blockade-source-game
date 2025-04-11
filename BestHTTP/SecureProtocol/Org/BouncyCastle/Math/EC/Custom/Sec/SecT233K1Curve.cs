using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000387 RID: 903
	internal class SecT233K1Curve : AbstractF2mCurve
	{
		// Token: 0x060024CB RID: 9419 RVA: 0x00103900 File Offset: 0x00101B00
		public SecT233K1Curve() : base(233, 74, 0, 0)
		{
			this.m_infinity = new SecT233K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.Decode("8000000000000000000000000000069D5BB915BCD46EFB1AD5F173ABDF"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00103976 File Offset: 0x00101B76
		protected override ECCurve CloneCurve()
		{
			return new SecT233K1Curve();
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x001036C1 File Offset: 0x001018C1
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0010397D File Offset: 0x00101B7D
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT233FieldElement(x);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00103985 File Offset: 0x00101B85
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT233K1Point(this, x, y, withCompression);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00103990 File Offset: 0x00101B90
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT233K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x0010399D File Offset: 0x00101B9D
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x001036C1 File Offset: 0x001018C1
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x001038B9 File Offset: 0x00101AB9
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x001039A8 File Offset: 0x00101BA8
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT233K1Curve.SecT233K1LookupTable(this, array, len);
		}

		// Token: 0x04001968 RID: 6504
		private const int SECT233K1_DEFAULT_COORDS = 6;

		// Token: 0x04001969 RID: 6505
		private const int SECT233K1_FE_LONGS = 4;

		// Token: 0x0400196A RID: 6506
		protected readonly SecT233K1Point m_infinity;

		// Token: 0x0200090B RID: 2315
		private class SecT233K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DE3 RID: 19939 RVA: 0x001B2729 File Offset: 0x001B0929
			internal SecT233K1LookupTable(SecT233K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2F RID: 3119
			// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x001B2746 File Offset: 0x001B0946
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DE5 RID: 19941 RVA: 0x001B2750 File Offset: 0x001B0950
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 4; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 4 + j] & num2);
					}
					num += 8;
				}
				return this.m_outer.CreateRawPoint(new SecT233FieldElement(array), new SecT233FieldElement(array2), false);
			}

			// Token: 0x04003499 RID: 13465
			private readonly SecT233K1Curve m_outer;

			// Token: 0x0400349A RID: 13466
			private readonly ulong[] m_table;

			// Token: 0x0400349B RID: 13467
			private readonly int m_size;
		}
	}
}
