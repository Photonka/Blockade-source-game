using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200039F RID: 927
	internal class SecT571R1Curve : AbstractF2mCurve
	{
		// Token: 0x0600266E RID: 9838 RVA: 0x00109E10 File Offset: 0x00108010
		public SecT571R1Curve() : base(571, 2, 5, 10)
		{
			this.m_infinity = new SecT571R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = SecT571R1Curve.SecT571R1_B;
			this.m_order = new BigInteger(1, Hex.Decode("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE661CE18FF55987308059B186823851EC7DD9CA1161DE93D5174D66E8382E9BB2FE84E47"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00109E7E File Offset: 0x0010807E
		protected override ECCurve CloneCurve()
		{
			return new SecT571R1Curve();
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x00109E85 File Offset: 0x00108085
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x001094B5 File Offset: 0x001076B5
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00109779 File Offset: 0x00107979
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT571FieldElement(x);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00109E8D File Offset: 0x0010808D
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT571R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00109E98 File Offset: 0x00108098
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT571R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x001094B5 File Offset: 0x001076B5
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x001096AD File Offset: 0x001078AD
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00109EA8 File Offset: 0x001080A8
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
			return new SecT571R1Curve.SecT571R1LookupTable(this, array, len);
		}

		// Token: 0x0400198E RID: 6542
		private const int SECT571R1_DEFAULT_COORDS = 6;

		// Token: 0x0400198F RID: 6543
		private const int SECT571R1_FE_LONGS = 9;

		// Token: 0x04001990 RID: 6544
		protected readonly SecT571R1Point m_infinity;

		// Token: 0x04001991 RID: 6545
		internal static readonly SecT571FieldElement SecT571R1_B = new SecT571FieldElement(new BigInteger(1, Hex.Decode("02F40E7E2221F295DE297117B7F3D62F5C6A97FFCB8CEFF1CD6BA8CE4A9A18AD84FFABBD8EFA59332BE7AD6756A66E294AFD185A78FF12AA520E4DE739BACA0C7FFEFF7F2955727A")));

		// Token: 0x04001992 RID: 6546
		internal static readonly SecT571FieldElement SecT571R1_B_SQRT = (SecT571FieldElement)SecT571R1Curve.SecT571R1_B.Sqrt();

		// Token: 0x02000913 RID: 2323
		private class SecT571R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DFB RID: 19963 RVA: 0x001B2D2C File Offset: 0x001B0F2C
			internal SecT571R1LookupTable(SecT571R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C37 RID: 3127
			// (get) Token: 0x06004DFC RID: 19964 RVA: 0x001B2D49 File Offset: 0x001B0F49
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DFD RID: 19965 RVA: 0x001B2D54 File Offset: 0x001B0F54
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

			// Token: 0x040034B1 RID: 13489
			private readonly SecT571R1Curve m_outer;

			// Token: 0x040034B2 RID: 13490
			private readonly ulong[] m_table;

			// Token: 0x040034B3 RID: 13491
			private readonly int m_size;
		}
	}
}
