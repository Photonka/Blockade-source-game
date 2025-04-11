using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200034B RID: 843
	internal class SecP192K1Curve : AbstractFpCurve
	{
		// Token: 0x060020F7 RID: 8439 RVA: 0x000F4E20 File Offset: 0x000F3020
		public SecP192K1Curve() : base(SecP192K1Curve.q)
		{
			this.m_infinity = new SecP192K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(3L));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFE26F2FC170F69466A74DEFD8D"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000F4E92 File Offset: 0x000F3092
		protected override ECCurve CloneCurve()
		{
			return new SecP192K1Curve();
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x000F4E99 File Offset: 0x000F3099
		public virtual BigInteger Q
		{
			get
			{
				return SecP192K1Curve.q;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000F4EA0 File Offset: 0x000F30A0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000F4EA8 File Offset: 0x000F30A8
		public override int FieldSize
		{
			get
			{
				return SecP192K1Curve.q.BitLength;
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000F4EB4 File Offset: 0x000F30B4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP192K1FieldElement(x);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000F4EBC File Offset: 0x000F30BC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP192K1Point(this, x, y, withCompression);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000F4EC7 File Offset: 0x000F30C7
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP192K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000F4ED4 File Offset: 0x000F30D4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 6 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy(((SecP192K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 6;
				Nat192.Copy(((SecP192K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 6;
			}
			return new SecP192K1Curve.SecP192K1LookupTable(this, array, len);
		}

		// Token: 0x040018E4 RID: 6372
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFEE37"));

		// Token: 0x040018E5 RID: 6373
		private const int SECP192K1_DEFAULT_COORDS = 2;

		// Token: 0x040018E6 RID: 6374
		private const int SECP192K1_FE_INTS = 6;

		// Token: 0x040018E7 RID: 6375
		protected readonly SecP192K1Point m_infinity;

		// Token: 0x020008FA RID: 2298
		private class SecP192K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DB0 RID: 19888 RVA: 0x001B1A61 File Offset: 0x001AFC61
			internal SecP192K1LookupTable(SecP192K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1E RID: 3102
			// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x001B1A7E File Offset: 0x001AFC7E
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DB2 RID: 19890 RVA: 0x001B1A88 File Offset: 0x001AFC88
			public virtual ECPoint Lookup(int index)
			{
				uint[] array = Nat192.Create();
				uint[] array2 = Nat192.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 6; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 6 + j] & num2);
					}
					num += 12;
				}
				return this.m_outer.CreateRawPoint(new SecP192K1FieldElement(array), new SecP192K1FieldElement(array2), false);
			}

			// Token: 0x04003466 RID: 13414
			private readonly SecP192K1Curve m_outer;

			// Token: 0x04003467 RID: 13415
			private readonly uint[] m_table;

			// Token: 0x04003468 RID: 13416
			private readonly int m_size;
		}
	}
}
