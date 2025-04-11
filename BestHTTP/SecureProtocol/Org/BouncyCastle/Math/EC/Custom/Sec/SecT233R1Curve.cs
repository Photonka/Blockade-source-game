using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000389 RID: 905
	internal class SecT233R1Curve : AbstractF2mCurve
	{
		// Token: 0x060024E5 RID: 9445 RVA: 0x00104018 File Offset: 0x00102218
		public SecT233R1Curve() : base(233, 74, 0, 0)
		{
			this.m_infinity = new SecT233R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0066647EDE6C332C7F8C0923BB58213B333B20E9CE4281FE115F7D8F90AD")));
			this.m_order = new BigInteger(1, Hex.Decode("01000000000000000000000000000013E974E72F8A6922031D2603CFE0D7"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00104097 File Offset: 0x00102297
		protected override ECCurve CloneCurve()
		{
			return new SecT233R1Curve();
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x0010409E File Offset: 0x0010229E
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x001036C1 File Offset: 0x001018C1
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0010397D File Offset: 0x00101B7D
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT233FieldElement(x);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x001040A6 File Offset: 0x001022A6
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT233R1Point(this, x, y, withCompression);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x001040B1 File Offset: 0x001022B1
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT233R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x001036C1 File Offset: 0x001018C1
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x001038B9 File Offset: 0x00101AB9
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x001040C0 File Offset: 0x001022C0
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
			return new SecT233R1Curve.SecT233R1LookupTable(this, array, len);
		}

		// Token: 0x0400196B RID: 6507
		private const int SECT233R1_DEFAULT_COORDS = 6;

		// Token: 0x0400196C RID: 6508
		private const int SECT233R1_FE_LONGS = 4;

		// Token: 0x0400196D RID: 6509
		protected readonly SecT233R1Point m_infinity;

		// Token: 0x0200090C RID: 2316
		private class SecT233R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DE6 RID: 19942 RVA: 0x001B27E9 File Offset: 0x001B09E9
			internal SecT233R1LookupTable(SecT233R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x001B2806 File Offset: 0x001B0A06
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DE8 RID: 19944 RVA: 0x001B2810 File Offset: 0x001B0A10
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

			// Token: 0x0400349C RID: 13468
			private readonly SecT233R1Curve m_outer;

			// Token: 0x0400349D RID: 13469
			private readonly ulong[] m_table;

			// Token: 0x0400349E RID: 13470
			private readonly int m_size;
		}
	}
}
