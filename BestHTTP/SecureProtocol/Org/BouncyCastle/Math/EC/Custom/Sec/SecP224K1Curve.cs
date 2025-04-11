using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000353 RID: 851
	internal class SecP224K1Curve : AbstractFpCurve
	{
		// Token: 0x06002171 RID: 8561 RVA: 0x000F6AD4 File Offset: 0x000F4CD4
		public SecP224K1Curve() : base(SecP224K1Curve.q)
		{
			this.m_infinity = new SecP224K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(5L));
			this.m_order = new BigInteger(1, Hex.Decode("010000000000000000000000000001DCE8D2EC6184CAF0A971769FB1F7"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000F6B46 File Offset: 0x000F4D46
		protected override ECCurve CloneCurve()
		{
			return new SecP224K1Curve();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000F6B4D File Offset: 0x000F4D4D
		public virtual BigInteger Q
		{
			get
			{
				return SecP224K1Curve.q;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000F6B54 File Offset: 0x000F4D54
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000F6B5C File Offset: 0x000F4D5C
		public override int FieldSize
		{
			get
			{
				return SecP224K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000F6B68 File Offset: 0x000F4D68
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP224K1FieldElement(x);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000F6B70 File Offset: 0x000F4D70
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP224K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000F6B7B File Offset: 0x000F4D7B
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP224K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000F6B88 File Offset: 0x000F4D88
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat224.Copy(((SecP224K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat224.Copy(((SecP224K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecP224K1Curve.SecP224K1LookupTable(this, array, len);
		}

		// Token: 0x040018FB RID: 6395
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFE56D"));

		// Token: 0x040018FC RID: 6396
		private const int SECP224K1_DEFAULT_COORDS = 2;

		// Token: 0x040018FD RID: 6397
		private const int SECP224K1_FE_INTS = 7;

		// Token: 0x040018FE RID: 6398
		protected readonly SecP224K1Point m_infinity;

		// Token: 0x020008FC RID: 2300
		private class SecP224K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DB6 RID: 19894 RVA: 0x001B1BE1 File Offset: 0x001AFDE1
			internal SecP224K1LookupTable(SecP224K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x001B1BFE File Offset: 0x001AFDFE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DB8 RID: 19896 RVA: 0x001B1C08 File Offset: 0x001AFE08
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat224.Create();
				uint[] array2 = Nat224.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 7; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 7 + j] & num2);
					}
					num += 14;
				}
				return this.m_outer.CreateRawPoint(new SecP224K1FieldElement(array), new SecP224K1FieldElement(array2), false);
			}

			// Token: 0x0400346C RID: 13420
			private readonly SecP224K1Curve m_outer;

			// Token: 0x0400346D RID: 13421
			private readonly uint[] m_table;

			// Token: 0x0400346E RID: 13422
			private readonly int m_size;
		}
	}
}
