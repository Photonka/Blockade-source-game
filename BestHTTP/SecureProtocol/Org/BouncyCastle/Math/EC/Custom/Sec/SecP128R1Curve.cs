using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200033D RID: 829
	internal class SecP128R1Curve : AbstractFpCurve
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x000F1D70 File Offset: 0x000EFF70
		public SecP128R1Curve() : base(SecP128R1Curve.q)
		{
			this.m_infinity = new SecP128R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("E87579C11079F43DD824993C2CEE5ED3")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFE0000000075A30D1B9038A115"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000F1DF6 File Offset: 0x000EFFF6
		protected override ECCurve CloneCurve()
		{
			return new SecP128R1Curve();
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x000F1E06 File Offset: 0x000F0006
		public virtual BigInteger Q
		{
			get
			{
				return SecP128R1Curve.q;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x000F1E0D File Offset: 0x000F000D
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000F1E15 File Offset: 0x000F0015
		public override int FieldSize
		{
			get
			{
				return SecP128R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000F1E21 File Offset: 0x000F0021
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP128R1FieldElement(x);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000F1E29 File Offset: 0x000F0029
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP128R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000F1E34 File Offset: 0x000F0034
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP128R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000F1E44 File Offset: 0x000F0044
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat128.Copy(((SecP128R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat128.Copy(((SecP128R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecP128R1Curve.SecP128R1LookupTable(this, array, len);
		}

		// Token: 0x040018BD RID: 6333
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x040018BE RID: 6334
		private const int SECP128R1_DEFAULT_COORDS = 2;

		// Token: 0x040018BF RID: 6335
		private const int SECP128R1_FE_INTS = 4;

		// Token: 0x040018C0 RID: 6336
		protected readonly SecP128R1Point m_infinity;

		// Token: 0x020008F6 RID: 2294
		private class SecP128R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DA4 RID: 19876 RVA: 0x001B1760 File Offset: 0x001AF960
			internal SecP128R1LookupTable(SecP128R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x001B177D File Offset: 0x001AF97D
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DA6 RID: 19878 RVA: 0x001B1788 File Offset: 0x001AF988
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat128.Create();
				uint[] array2 = Nat128.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 4; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 4 + j] & num2);
					}
					num += 8;
				}
				return this.m_outer.CreateRawPoint(new SecP128R1FieldElement(array), new SecP128R1FieldElement(array2), false);
			}

			// Token: 0x0400345A RID: 13402
			private readonly SecP128R1Curve m_outer;

			// Token: 0x0400345B RID: 13403
			private readonly uint[] m_table;

			// Token: 0x0400345C RID: 13404
			private readonly int m_size;
		}
	}
}
