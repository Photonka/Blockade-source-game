using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000367 RID: 871
	internal class SecP521R1Curve : AbstractFpCurve
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x000FB864 File Offset: 0x000F9A64
		public SecP521R1Curve() : base(SecP521R1Curve.q)
		{
			this.m_infinity = new SecP521R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00")));
			this.m_order = new BigInteger(1, Hex.Decode("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000FB8EA File Offset: 0x000F9AEA
		protected override ECCurve CloneCurve()
		{
			return new SecP521R1Curve();
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000FB8F1 File Offset: 0x000F9AF1
		public virtual BigInteger Q
		{
			get
			{
				return SecP521R1Curve.q;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000FB8F8 File Offset: 0x000F9AF8
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x000FB900 File Offset: 0x000F9B00
		public override int FieldSize
		{
			get
			{
				return SecP521R1Curve.q.BitLength;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000FB90C File Offset: 0x000F9B0C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP521R1FieldElement(x);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000FB914 File Offset: 0x000F9B14
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP521R1Point(this, x, y, withCompression);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000FB91F File Offset: 0x000F9B1F
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP521R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000FB92C File Offset: 0x000F9B2C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 17 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat.Copy(17, ((SecP521R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 17;
				Nat.Copy(17, ((SecP521R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 17;
			}
			return new SecP521R1Curve.SecP521R1LookupTable(this, array, len);
		}

		// Token: 0x04001934 RID: 6452
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x04001935 RID: 6453
		private const int SECP521R1_DEFAULT_COORDS = 2;

		// Token: 0x04001936 RID: 6454
		private const int SECP521R1_FE_INTS = 17;

		// Token: 0x04001937 RID: 6455
		protected readonly SecP521R1Point m_infinity;

		// Token: 0x02000901 RID: 2305
		private class SecP521R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DC5 RID: 19909 RVA: 0x001B1FA7 File Offset: 0x001B01A7
			internal SecP521R1LookupTable(SecP521R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C25 RID: 3109
			// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x001B1FC4 File Offset: 0x001B01C4
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DC7 RID: 19911 RVA: 0x001B1FCC File Offset: 0x001B01CC
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat.Create(17);
				uint[] array2 = Nat.Create(17);
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 17; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 17 + j] & num2);
					}
					num += 34;
				}
				return this.m_outer.CreateRawPoint(new SecP521R1FieldElement(array), new SecP521R1FieldElement(array2), false);
			}

			// Token: 0x0400347B RID: 13435
			private readonly SecP521R1Curve m_outer;

			// Token: 0x0400347C RID: 13436
			private readonly uint[] m_table;

			// Token: 0x0400347D RID: 13437
			private readonly int m_size;
		}
	}
}
