using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000347 RID: 839
	internal class SecP160R2Curve : AbstractFpCurve
	{
		// Token: 0x060020BB RID: 8379 RVA: 0x000F4014 File Offset: 0x000F2214
		public SecP160R2Curve() : base(SecP160R2Curve.q)
		{
			this.m_infinity = new SecP160R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC70")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("B4E134D3FB59EB8BAB57274904664D5AF50388BA")));
			this.m_order = new BigInteger(1, Hex.Decode("0100000000000000000000351EE786A818F3A1A16B"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000F409A File Offset: 0x000F229A
		protected override ECCurve CloneCurve()
		{
			return new SecP160R2Curve();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000F40A1 File Offset: 0x000F22A1
		public virtual BigInteger Q
		{
			get
			{
				return SecP160R2Curve.q;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000F40A8 File Offset: 0x000F22A8
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x000F40B0 File Offset: 0x000F22B0
		public override int FieldSize
		{
			get
			{
				return SecP160R2Curve.q.BitLength;
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000F2C60 File Offset: 0x000F0E60
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R2FieldElement(x);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000F40BC File Offset: 0x000F22BC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160R2Point(this, x, y, withCompression);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000F40C7 File Offset: 0x000F22C7
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000F40D4 File Offset: 0x000F22D4
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
			return new SecP160R2Curve.SecP160R2LookupTable(this, array, len);
		}

		// Token: 0x040018D8 RID: 6360
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73"));

		// Token: 0x040018D9 RID: 6361
		private const int SECP160R2_DEFAULT_COORDS = 2;

		// Token: 0x040018DA RID: 6362
		private const int SECP160R2_FE_INTS = 5;

		// Token: 0x040018DB RID: 6363
		protected readonly SecP160R2Point m_infinity;

		// Token: 0x020008F9 RID: 2297
		private class SecP160R2LookupTable : ECLookupTable
		{
			// Token: 0x06004DAD RID: 19885 RVA: 0x001B19A1 File Offset: 0x001AFBA1
			internal SecP160R2LookupTable(SecP160R2Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1D RID: 3101
			// (get) Token: 0x06004DAE RID: 19886 RVA: 0x001B19BE File Offset: 0x001AFBBE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DAF RID: 19887 RVA: 0x001B19C8 File Offset: 0x001AFBC8
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
				return this.m_outer.CreateRawPoint(new SecP160R2FieldElement(array), new SecP160R2FieldElement(array2), false);
			}

			// Token: 0x04003463 RID: 13411
			private readonly SecP160R2Curve m_outer;

			// Token: 0x04003464 RID: 13412
			private readonly uint[] m_table;

			// Token: 0x04003465 RID: 13413
			private readonly int m_size;
		}
	}
}
