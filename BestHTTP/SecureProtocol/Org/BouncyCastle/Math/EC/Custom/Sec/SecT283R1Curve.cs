using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000393 RID: 915
	internal class SecT283R1Curve : AbstractF2mCurve
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x00106CD8 File Offset: 0x00104ED8
		public SecT283R1Curve() : base(283, 5, 7, 12)
		{
			this.m_infinity = new SecT283R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("027B680AC8B8596DA5A4AF8A19A0303FCA97FD7645309FA2A581485AF6263E313B79A2F5")));
			this.m_order = new BigInteger(1, Hex.Decode("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEF90399660FC938A90165B042A7CEFADB307"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00106D57 File Offset: 0x00104F57
		protected override ECCurve CloneCurve()
		{
			return new SecT283R1Curve();
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00106D5E File Offset: 0x00104F5E
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0010637F File Offset: 0x0010457F
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00106645 File Offset: 0x00104845
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT283FieldElement(x);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00106D66 File Offset: 0x00104F66
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT283R1Point(this, x, y, withCompression);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00106D71 File Offset: 0x00104F71
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT283R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x0010637F File Offset: 0x0010457F
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000A643E File Offset: 0x000A463E
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000FFFFD File Offset: 0x000FE1FD
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00106579 File Offset: 0x00104779
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00106D80 File Offset: 0x00104F80
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
			return new SecT283R1Curve.SecT283R1LookupTable(this, array, len);
		}

		// Token: 0x0400197B RID: 6523
		private const int SECT283R1_DEFAULT_COORDS = 6;

		// Token: 0x0400197C RID: 6524
		private const int SECT283R1_FE_LONGS = 5;

		// Token: 0x0400197D RID: 6525
		protected readonly SecT283R1Point m_infinity;

		// Token: 0x0200090F RID: 2319
		private class SecT283R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DEF RID: 19951 RVA: 0x001B2A2A File Offset: 0x001B0C2A
			internal SecT283R1LookupTable(SecT283R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C33 RID: 3123
			// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x001B2A47 File Offset: 0x001B0C47
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DF1 RID: 19953 RVA: 0x001B2A50 File Offset: 0x001B0C50
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

			// Token: 0x040034A5 RID: 13477
			private readonly SecT283R1Curve m_outer;

			// Token: 0x040034A6 RID: 13478
			private readonly ulong[] m_table;

			// Token: 0x040034A7 RID: 13479
			private readonly int m_size;
		}
	}
}
