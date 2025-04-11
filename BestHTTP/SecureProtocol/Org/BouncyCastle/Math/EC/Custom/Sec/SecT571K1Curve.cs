using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200039D RID: 925
	internal class SecT571K1Curve : AbstractF2mCurve
	{
		// Token: 0x06002654 RID: 9812 RVA: 0x001096F4 File Offset: 0x001078F4
		public SecT571K1Curve() : base(571, 2, 5, 10)
		{
			this.m_infinity = new SecT571K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.Decode("020000000000000000000000000000000000000000000000000000000000000000000000131850E1F19A63E4B391A8DB917F4138B630D84BE5D639381E91DEB45CFE778F637C1001"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0010976A File Offset: 0x0010796A
		protected override ECCurve CloneCurve()
		{
			return new SecT571K1Curve();
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00109771 File Offset: 0x00107971
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x001094B5 File Offset: 0x001076B5
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00109779 File Offset: 0x00107979
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT571FieldElement(x);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00109781 File Offset: 0x00107981
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT571K1Point(this, x, y, withCompression);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0010978C File Offset: 0x0010798C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT571K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x001094B5 File Offset: 0x001076B5
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x001096AD File Offset: 0x001078AD
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0010979C File Offset: 0x0010799C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 9 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat576.Copy64(((SecT571FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 9;
				Nat576.Copy64(((SecT571FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 9;
			}
			return new SecT571K1Curve.SecT571K1LookupTable(this, array, len);
		}

		// Token: 0x0400198B RID: 6539
		private const int SECT571K1_DEFAULT_COORDS = 6;

		// Token: 0x0400198C RID: 6540
		private const int SECT571K1_FE_LONGS = 9;

		// Token: 0x0400198D RID: 6541
		protected readonly SecT571K1Point m_infinity;

		// Token: 0x02000912 RID: 2322
		private class SecT571K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DF8 RID: 19960 RVA: 0x001B2C6A File Offset: 0x001B0E6A
			internal SecT571K1LookupTable(SecT571K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C36 RID: 3126
			// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x001B2C87 File Offset: 0x001B0E87
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DFA RID: 19962 RVA: 0x001B2C90 File Offset: 0x001B0E90
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat576.Create64();
				ulong[] array2 = Nat576.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 9; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 9 + j] & num2);
					}
					num += 18;
				}
				return this.m_outer.CreateRawPoint(new SecT571FieldElement(array), new SecT571FieldElement(array2), false);
			}

			// Token: 0x040034AE RID: 13486
			private readonly SecT571K1Curve m_outer;

			// Token: 0x040034AF RID: 13487
			private readonly ulong[] m_table;

			// Token: 0x040034B0 RID: 13488
			private readonly int m_size;
		}
	}
}
