using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200035F RID: 863
	internal class SecP256R1Curve : AbstractFpCurve
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000F97A4 File Offset: 0x000F79A4
		public SecP256R1Curve() : base(SecP256R1Curve.q)
		{
			this.m_infinity = new SecP256R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000F982A File Offset: 0x000F7A2A
		protected override ECCurve CloneCurve()
		{
			return new SecP256R1Curve();
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000F9831 File Offset: 0x000F7A31
		public virtual BigInteger Q
		{
			get
			{
				return SecP256R1Curve.q;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000F9838 File Offset: 0x000F7A38
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x000F9840 File Offset: 0x000F7A40
		public override int FieldSize
		{
			get
			{
				return SecP256R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000F984C File Offset: 0x000F7A4C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP256R1FieldElement(x);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000F9854 File Offset: 0x000F7A54
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP256R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000F985F File Offset: 0x000F7A5F
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP256R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000F986C File Offset: 0x000F7A6C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SecP256R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SecP256R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SecP256R1Curve.SecP256R1LookupTable(this, array, len);
		}

		// Token: 0x0400191F RID: 6431
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x04001920 RID: 6432
		private const int SECP256R1_DEFAULT_COORDS = 2;

		// Token: 0x04001921 RID: 6433
		private const int SECP256R1_FE_INTS = 8;

		// Token: 0x04001922 RID: 6434
		protected readonly SecP256R1Point m_infinity;

		// Token: 0x020008FF RID: 2303
		private class SecP256R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DBF RID: 19903 RVA: 0x001B1E21 File Offset: 0x001B0021
			internal SecP256R1LookupTable(SecP256R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x001B1E3E File Offset: 0x001B003E
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DC1 RID: 19905 RVA: 0x001B1E48 File Offset: 0x001B0048
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
				return this.m_outer.CreateRawPoint(new SecP256R1FieldElement(array), new SecP256R1FieldElement(array2), false);
			}

			// Token: 0x04003475 RID: 13429
			private readonly SecP256R1Curve m_outer;

			// Token: 0x04003476 RID: 13430
			private readonly uint[] m_table;

			// Token: 0x04003477 RID: 13431
			private readonly int m_size;
		}
	}
}
