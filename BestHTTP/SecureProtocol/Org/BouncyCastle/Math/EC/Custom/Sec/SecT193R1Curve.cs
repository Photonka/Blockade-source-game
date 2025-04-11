using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000381 RID: 897
	internal class SecT193R1Curve : AbstractF2mCurve
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x00101F74 File Offset: 0x00100174
		public SecT193R1Curve() : base(193, 15, 0, 0)
		{
			this.m_infinity = new SecT193R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("0017858FEB7A98975169E171F77B4087DE098AC8A911DF7B01")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("00FDFB49BFE6C3A89FACADAA7A1E5BBC7CC1C2E5D831478814")));
			this.m_order = new BigInteger(1, Hex.Decode("01000000000000000000000000C7F34A778F443ACC920EBA49"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00101FFE File Offset: 0x001001FE
		protected override ECCurve CloneCurve()
		{
			return new SecT193R1Curve();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x00102005 File Offset: 0x00100205
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00101D35 File Offset: 0x000FFF35
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0010200D File Offset: 0x0010020D
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT193FieldElement(x);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00102015 File Offset: 0x00100215
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT193R1Point(this, x, y, withCompression);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00102020 File Offset: 0x00100220
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT193R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x00101D35 File Offset: 0x000FFF35
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00101F2D File Offset: 0x0010012D
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00102030 File Offset: 0x00100230
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT193R1Curve.SecT193R1LookupTable(this, array, len);
		}

		// Token: 0x0400195F RID: 6495
		private const int SECT193R1_DEFAULT_COORDS = 6;

		// Token: 0x04001960 RID: 6496
		private const int SECT193R1_FE_LONGS = 4;

		// Token: 0x04001961 RID: 6497
		protected readonly SecT193R1Point m_infinity;

		// Token: 0x02000909 RID: 2313
		private class SecT193R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DDD RID: 19933 RVA: 0x001B25A9 File Offset: 0x001B07A9
			internal SecT193R1LookupTable(SecT193R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2D RID: 3117
			// (get) Token: 0x06004DDE RID: 19934 RVA: 0x001B25C6 File Offset: 0x001B07C6
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DDF RID: 19935 RVA: 0x001B25D0 File Offset: 0x001B07D0
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
				return this.m_outer.CreateRawPoint(new SecT193FieldElement(array), new SecT193FieldElement(array2), false);
			}

			// Token: 0x04003493 RID: 13459
			private readonly SecT193R1Curve m_outer;

			// Token: 0x04003494 RID: 13460
			private readonly ulong[] m_table;

			// Token: 0x04003495 RID: 13461
			private readonly int m_size;
		}
	}
}
