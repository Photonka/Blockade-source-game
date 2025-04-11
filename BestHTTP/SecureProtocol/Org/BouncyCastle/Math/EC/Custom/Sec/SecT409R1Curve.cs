using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000399 RID: 921
	internal class SecT409R1Curve : AbstractF2mCurve
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x0010860C File Offset: 0x0010680C
		public SecT409R1Curve() : base(409, 87, 0, 0)
		{
			this.m_infinity = new SecT409R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0021A5C2C8EE9FEB5C4B9A753B7B476B7FD6422EF1F3DD674761FA99D6AC27C8A9A197B272822F6CD57A55AA4F50AE317B13545F")));
			this.m_order = new BigInteger(1, Hex.Decode("010000000000000000000000000000000000000000000000000001E2AAD6A612F33307BE5FA47C3C9E052F838164CD37D9A21173"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0010868B File Offset: 0x0010688B
		protected override ECCurve CloneCurve()
		{
			return new SecT409R1Curve();
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00108692 File Offset: 0x00106892
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00107F79 File Offset: 0x00106179
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT409FieldElement(x);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0010869A File Offset: 0x0010689A
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT409R1Point(this, x, y, withCompression);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x001086A5 File Offset: 0x001068A5
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT409R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x00107EAD File Offset: 0x001060AD
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x001086B4 File Offset: 0x001068B4
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
			return new SecT409R1Curve.SecT409R1LookupTable(this, array, len);
		}

		// Token: 0x04001984 RID: 6532
		private const int SECT409R1_DEFAULT_COORDS = 6;

		// Token: 0x04001985 RID: 6533
		private const int SECT409R1_FE_LONGS = 7;

		// Token: 0x04001986 RID: 6534
		protected readonly SecT409R1Point m_infinity;

		// Token: 0x02000911 RID: 2321
		private class SecT409R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DF5 RID: 19957 RVA: 0x001B2BAA File Offset: 0x001B0DAA
			internal SecT409R1LookupTable(SecT409R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C35 RID: 3125
			// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x001B2BC7 File Offset: 0x001B0DC7
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DF7 RID: 19959 RVA: 0x001B2BD0 File Offset: 0x001B0DD0
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

			// Token: 0x040034AB RID: 13483
			private readonly SecT409R1Curve m_outer;

			// Token: 0x040034AC RID: 13484
			private readonly ulong[] m_table;

			// Token: 0x040034AD RID: 13485
			private readonly int m_size;
		}
	}
}
