using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200035B RID: 859
	internal class SecP256K1Curve : AbstractFpCurve
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x000F89F4 File Offset: 0x000F6BF4
		public SecP256K1Curve() : base(SecP256K1Curve.q)
		{
			this.m_infinity = new SecP256K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(7L));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000F8A66 File Offset: 0x000F6C66
		protected override ECCurve CloneCurve()
		{
			return new SecP256K1Curve();
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000F8A6D File Offset: 0x000F6C6D
		public virtual BigInteger Q
		{
			get
			{
				return SecP256K1Curve.q;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000F8A74 File Offset: 0x000F6C74
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000F8A7C File Offset: 0x000F6C7C
		public override int FieldSize
		{
			get
			{
				return SecP256K1Curve.q.BitLength;
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000F8A88 File Offset: 0x000F6C88
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP256K1FieldElement(x);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000F8A90 File Offset: 0x000F6C90
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP256K1Point(this, x, y, withCompression);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000F8A9B File Offset: 0x000F6C9B
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP256K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SecP256K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SecP256K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SecP256K1Curve.SecP256K1LookupTable(this, array, len);
		}

		// Token: 0x04001913 RID: 6419
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F"));

		// Token: 0x04001914 RID: 6420
		private const int SECP256K1_DEFAULT_COORDS = 2;

		// Token: 0x04001915 RID: 6421
		private const int SECP256K1_FE_INTS = 8;

		// Token: 0x04001916 RID: 6422
		protected readonly SecP256K1Point m_infinity;

		// Token: 0x020008FE RID: 2302
		private class SecP256K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DBC RID: 19900 RVA: 0x001B1D61 File Offset: 0x001AFF61
			internal SecP256K1LookupTable(SecP256K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x06004DBD RID: 19901 RVA: 0x001B1D7E File Offset: 0x001AFF7E
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DBE RID: 19902 RVA: 0x001B1D88 File Offset: 0x001AFF88
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 8; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 8 + j] & num2);
					}
					num += 16;
				}
				return this.m_outer.CreateRawPoint(new SecP256K1FieldElement(array), new SecP256K1FieldElement(array2), false);
			}

			// Token: 0x04003472 RID: 13426
			private readonly SecP256K1Curve m_outer;

			// Token: 0x04003473 RID: 13427
			private readonly uint[] m_table;

			// Token: 0x04003474 RID: 13428
			private readonly int m_size;
		}
	}
}
