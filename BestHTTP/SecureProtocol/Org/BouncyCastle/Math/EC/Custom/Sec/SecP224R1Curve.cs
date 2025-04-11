using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000357 RID: 855
	internal class SecP224R1Curve : AbstractFpCurve
	{
		// Token: 0x060021AD RID: 8621 RVA: 0x000F78E8 File Offset: 0x000F5AE8
		public SecP224R1Curve() : base(SecP224R1Curve.q)
		{
			this.m_infinity = new SecP224R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000F796E File Offset: 0x000F5B6E
		protected override ECCurve CloneCurve()
		{
			return new SecP224R1Curve();
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000F7975 File Offset: 0x000F5B75
		public virtual BigInteger Q
		{
			get
			{
				return SecP224R1Curve.q;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x000F797C File Offset: 0x000F5B7C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000F7984 File Offset: 0x000F5B84
		public override int FieldSize
		{
			get
			{
				return SecP224R1Curve.q.BitLength;
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000F7990 File Offset: 0x000F5B90
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP224R1FieldElement(x);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000F7998 File Offset: 0x000F5B98
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP224R1Point(this, x, y, withCompression);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000F79A3 File Offset: 0x000F5BA3
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP224R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000F79B0 File Offset: 0x000F5BB0
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat224.Copy(((SecP224R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat224.Copy(((SecP224R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecP224R1Curve.SecP224R1LookupTable(this, array, len);
		}

		// Token: 0x04001908 RID: 6408
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001"));

		// Token: 0x04001909 RID: 6409
		private const int SECP224R1_DEFAULT_COORDS = 2;

		// Token: 0x0400190A RID: 6410
		private const int SECP224R1_FE_INTS = 7;

		// Token: 0x0400190B RID: 6411
		protected readonly SecP224R1Point m_infinity;

		// Token: 0x020008FD RID: 2301
		private class SecP224R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DB9 RID: 19897 RVA: 0x001B1CA1 File Offset: 0x001AFEA1
			internal SecP224R1LookupTable(SecP224R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x06004DBA RID: 19898 RVA: 0x001B1CBE File Offset: 0x001AFEBE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DBB RID: 19899 RVA: 0x001B1CC8 File Offset: 0x001AFEC8
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
				return this.m_outer.CreateRawPoint(new SecP224R1FieldElement(array), new SecP224R1FieldElement(array2), false);
			}

			// Token: 0x0400346F RID: 13423
			private readonly SecP224R1Curve m_outer;

			// Token: 0x04003470 RID: 13424
			private readonly uint[] m_table;

			// Token: 0x04003471 RID: 13425
			private readonly int m_size;
		}
	}
}
