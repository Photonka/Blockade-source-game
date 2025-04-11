using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000343 RID: 835
	internal class SecP160R1Curve : AbstractFpCurve
	{
		// Token: 0x0600207F RID: 8319 RVA: 0x000F321C File Offset: 0x000F141C
		public SecP160R1Curve() : base(SecP160R1Curve.q)
		{
			this.m_infinity = new SecP160R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("1C97BEFC54BD7A8B65ACF89F81D4D4ADC565FA45")));
			this.m_order = new BigInteger(1, Hex.Decode("0100000000000000000001F4C8F927AED3CA752257"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000F32A2 File Offset: 0x000F14A2
		protected override ECCurve CloneCurve()
		{
			return new SecP160R1Curve();
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x000F32A9 File Offset: 0x000F14A9
		public virtual BigInteger Q
		{
			get
			{
				return SecP160R1Curve.q;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000F32B0 File Offset: 0x000F14B0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000F32B8 File Offset: 0x000F14B8
		public override int FieldSize
		{
			get
			{
				return SecP160R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000F32C4 File Offset: 0x000F14C4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R1FieldElement(x);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000F32CC File Offset: 0x000F14CC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000F32D7 File Offset: 0x000F14D7
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000F32E4 File Offset: 0x000F14E4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat160.Copy(((SecP160R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat160.Copy(((SecP160R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecP160R1Curve.SecP160R1LookupTable(this, array, len);
		}

		// Token: 0x040018CC RID: 6348
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFF"));

		// Token: 0x040018CD RID: 6349
		private const int SECP160R1_DEFAULT_COORDS = 2;

		// Token: 0x040018CE RID: 6350
		private const int SECP160R1_FE_INTS = 5;

		// Token: 0x040018CF RID: 6351
		protected readonly SecP160R1Point m_infinity;

		// Token: 0x020008F8 RID: 2296
		private class SecP160R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DAA RID: 19882 RVA: 0x001B18E1 File Offset: 0x001AFAE1
			internal SecP160R1LookupTable(SecP160R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1C RID: 3100
			// (get) Token: 0x06004DAB RID: 19883 RVA: 0x001B18FE File Offset: 0x001AFAFE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DAC RID: 19884 RVA: 0x001B1908 File Offset: 0x001AFB08
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat160.Create();
				uint[] array2 = Nat160.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 5; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 5 + j] & num2);
					}
					num += 10;
				}
				return this.m_outer.CreateRawPoint(new SecP160R1FieldElement(array), new SecP160R1FieldElement(array2), false);
			}

			// Token: 0x04003460 RID: 13408
			private readonly SecP160R1Curve m_outer;

			// Token: 0x04003461 RID: 13409
			private readonly uint[] m_table;

			// Token: 0x04003462 RID: 13410
			private readonly int m_size;
		}
	}
}
