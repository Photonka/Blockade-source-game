using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000391 RID: 913
	internal class SecT283K1Curve : AbstractF2mCurve
	{
		// Token: 0x06002583 RID: 9603 RVA: 0x001065C0 File Offset: 0x001047C0
		public SecT283K1Curve() : base(283, 5, 7, 12)
		{
			this.m_infinity = new SecT283K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.Decode("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE9AE2ED07577265DFF7F94451E061E163C61"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00106636 File Offset: 0x00104836
		protected override ECCurve CloneCurve()
		{
			return new SecT283K1Curve();
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x0010663D File Offset: 0x0010483D
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x0010637F File Offset: 0x0010457F
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00106645 File Offset: 0x00104845
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT283FieldElement(x);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x0010664D File Offset: 0x0010484D
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT283K1Point(this, x, y, withCompression);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00106658 File Offset: 0x00104858
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT283K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x0010637F File Offset: 0x0010457F
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00106579 File Offset: 0x00104779
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00106668 File Offset: 0x00104868
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecT283K1Curve.SecT283K1LookupTable(this, array, len);
		}

		// Token: 0x04001978 RID: 6520
		private const int SECT283K1_DEFAULT_COORDS = 6;

		// Token: 0x04001979 RID: 6521
		private const int SECT283K1_FE_LONGS = 5;

		// Token: 0x0400197A RID: 6522
		protected readonly SecT283K1Point m_infinity;

		// Token: 0x0200090E RID: 2318
		private class SecT283K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DEC RID: 19948 RVA: 0x001B2969 File Offset: 0x001B0B69
			internal SecT283K1LookupTable(SecT283K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C32 RID: 3122
			// (get) Token: 0x06004DED RID: 19949 RVA: 0x001B2986 File Offset: 0x001B0B86
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DEE RID: 19950 RVA: 0x001B2990 File Offset: 0x001B0B90
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat320.Create64();
				ulong[] array2 = Nat320.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 5; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 5 + j] & num2);
					}
					num += 10;
				}
				return this.m_outer.CreateRawPoint(new SecT283FieldElement(array), new SecT283FieldElement(array2), false);
			}

			// Token: 0x040034A2 RID: 13474
			private readonly SecT283K1Curve m_outer;

			// Token: 0x040034A3 RID: 13475
			private readonly ulong[] m_table;

			// Token: 0x040034A4 RID: 13476
			private readonly int m_size;
		}
	}
}
