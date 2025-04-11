using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000363 RID: 867
	internal class SecP384R1Curve : AbstractFpCurve
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x000FA758 File Offset: 0x000F8958
		public SecP384R1Curve() : base(SecP384R1Curve.q)
		{
			this.m_infinity = new SecP384R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000FA7DE File Offset: 0x000F89DE
		protected override ECCurve CloneCurve()
		{
			return new SecP384R1Curve();
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x000FA7E5 File Offset: 0x000F89E5
		public virtual BigInteger Q
		{
			get
			{
				return SecP384R1Curve.q;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000FA7EC File Offset: 0x000F89EC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000FA7F4 File Offset: 0x000F89F4
		public override int FieldSize
		{
			get
			{
				return SecP384R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000FA800 File Offset: 0x000F8A00
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP384R1FieldElement(x);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000FA808 File Offset: 0x000F8A08
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP384R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000FA813 File Offset: 0x000F8A13
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP384R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000FA820 File Offset: 0x000F8A20
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 12 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat.Copy(12, ((SecP384R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 12;
				Nat.Copy(12, ((SecP384R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 12;
			}
			return new SecP384R1Curve.SecP384R1LookupTable(this, array, len);
		}

		// Token: 0x04001929 RID: 6441
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF"));

		// Token: 0x0400192A RID: 6442
		private const int SECP384R1_DEFAULT_COORDS = 2;

		// Token: 0x0400192B RID: 6443
		private const int SECP384R1_FE_INTS = 12;

		// Token: 0x0400192C RID: 6444
		protected readonly SecP384R1Point m_infinity;

		// Token: 0x02000900 RID: 2304
		private class SecP384R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DC2 RID: 19906 RVA: 0x001B1EE1 File Offset: 0x001B00E1
			internal SecP384R1LookupTable(SecP384R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C24 RID: 3108
			// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x001B1EFE File Offset: 0x001B00FE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DC4 RID: 19908 RVA: 0x001B1F08 File Offset: 0x001B0108
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat.Create(12);
				uint[] array2 = Nat.Create(12);
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 12; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 12 + j] & num2);
					}
					num += 24;
				}
				return this.m_outer.CreateRawPoint(new SecP384R1FieldElement(array), new SecP384R1FieldElement(array2), false);
			}

			// Token: 0x04003478 RID: 13432
			private readonly SecP384R1Curve m_outer;

			// Token: 0x04003479 RID: 13433
			private readonly uint[] m_table;

			// Token: 0x0400347A RID: 13434
			private readonly int m_size;
		}
	}
}
