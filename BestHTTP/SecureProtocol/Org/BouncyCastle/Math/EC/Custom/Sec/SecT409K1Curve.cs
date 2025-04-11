using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000397 RID: 919
	internal class SecT409K1Curve : AbstractF2mCurve
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x00107EF4 File Offset: 0x001060F4
		public SecT409K1Curve() : base(409, 87, 0, 0)
		{
			this.m_infinity = new SecT409K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.Decode("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE5F83B2D4EA20400EC4557D5ED3E3E7CA5B4B5C83B8E01E5FCF"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00107F6A File Offset: 0x0010616A
		protected override ECCurve CloneCurve()
		{
			return new SecT409K1Curve();
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x00107F71 File Offset: 0x00106171
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00107F79 File Offset: 0x00106179
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT409FieldElement(x);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00107F81 File Offset: 0x00106181
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT409K1Point(this, x, y, withCompression);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00107F8C File Offset: 0x0010618C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT409K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x00107EAD File Offset: 0x001060AD
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00107F9C File Offset: 0x0010619C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat448.Copy64(((SecT409FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat448.Copy64(((SecT409FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecT409K1Curve.SecT409K1LookupTable(this, array, len);
		}

		// Token: 0x04001981 RID: 6529
		private const int SECT409K1_DEFAULT_COORDS = 6;

		// Token: 0x04001982 RID: 6530
		private const int SECT409K1_FE_LONGS = 7;

		// Token: 0x04001983 RID: 6531
		protected readonly SecT409K1Point m_infinity;

		// Token: 0x02000910 RID: 2320
		private class SecT409K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DF2 RID: 19954 RVA: 0x001B2AEA File Offset: 0x001B0CEA
			internal SecT409K1LookupTable(SecT409K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C34 RID: 3124
			// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x001B2B07 File Offset: 0x001B0D07
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DF4 RID: 19956 RVA: 0x001B2B10 File Offset: 0x001B0D10
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat448.Create64();
				ulong[] array2 = Nat448.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 7; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 7 + j] & num2);
					}
					num += 14;
				}
				return this.m_outer.CreateRawPoint(new SecT409FieldElement(array), new SecT409FieldElement(array2), false);
			}

			// Token: 0x040034A8 RID: 13480
			private readonly SecT409K1Curve m_outer;

			// Token: 0x040034A9 RID: 13481
			private readonly ulong[] m_table;

			// Token: 0x040034AA RID: 13482
			private readonly int m_size;
		}
	}
}
