using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000341 RID: 833
	internal class SecP160K1Curve : AbstractFpCurve
	{
		// Token: 0x0600206B RID: 8299 RVA: 0x000F2BCC File Offset: 0x000F0DCC
		public SecP160K1Curve() : base(SecP160K1Curve.q)
		{
			this.m_infinity = new SecP160K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(7L));
			this.m_order = new BigInteger(1, Hex.Decode("0100000000000000000001B8FA16DFAB9ACA16B6B3"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000F2C3E File Offset: 0x000F0E3E
		protected override ECCurve CloneCurve()
		{
			return new SecP160K1Curve();
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000F2C45 File Offset: 0x000F0E45
		public virtual BigInteger Q
		{
			get
			{
				return SecP160K1Curve.q;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x000F2C4C File Offset: 0x000F0E4C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000F2C54 File Offset: 0x000F0E54
		public override int FieldSize
		{
			get
			{
				return SecP160K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000F2C60 File Offset: 0x000F0E60
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R2FieldElement(x);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000F2C68 File Offset: 0x000F0E68
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000F2C73 File Offset: 0x000F0E73
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000F2C80 File Offset: 0x000F0E80
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat160.Copy(((SecP160R2FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat160.Copy(((SecP160R2FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecP160K1Curve.SecP160K1LookupTable(this, array, len);
		}

		// Token: 0x040018C8 RID: 6344
		public static readonly BigInteger q = SecP160R2Curve.q;

		// Token: 0x040018C9 RID: 6345
		private const int SECP160K1_DEFAULT_COORDS = 2;

		// Token: 0x040018CA RID: 6346
		private const int SECP160K1_FE_INTS = 5;

		// Token: 0x040018CB RID: 6347
		protected readonly SecP160K1Point m_infinity;

		// Token: 0x020008F7 RID: 2295
		private class SecP160K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DA7 RID: 19879 RVA: 0x001B1820 File Offset: 0x001AFA20
			internal SecP160K1LookupTable(SecP160K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x001B183D File Offset: 0x001AFA3D
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DA9 RID: 19881 RVA: 0x001B1848 File Offset: 0x001AFA48
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
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
				return this.m_outer.CreateRawPoint(new SecP160R2FieldElement(array), new SecP160R2FieldElement(array2), false);
			}

			// Token: 0x0400345D RID: 13405
			private readonly SecP160K1Curve m_outer;

			// Token: 0x0400345E RID: 13406
			private readonly uint[] m_table;

			// Token: 0x0400345F RID: 13407
			private readonly int m_size;
		}
	}
}
