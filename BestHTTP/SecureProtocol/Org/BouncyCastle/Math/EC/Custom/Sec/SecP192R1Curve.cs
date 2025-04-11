using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200034F RID: 847
	internal class SecP192R1Curve : AbstractFpCurve
	{
		// Token: 0x06002133 RID: 8499 RVA: 0x000F5BC4 File Offset: 0x000F3DC4
		public SecP192R1Curve() : base(SecP192R1Curve.q)
		{
			this.m_infinity = new SecP192R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000F5C4A File Offset: 0x000F3E4A
		protected override ECCurve CloneCurve()
		{
			return new SecP192R1Curve();
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x000F5C51 File Offset: 0x000F3E51
		public virtual BigInteger Q
		{
			get
			{
				return SecP192R1Curve.q;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x000F5C58 File Offset: 0x000F3E58
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000F5C60 File Offset: 0x000F3E60
		public override int FieldSize
		{
			get
			{
				return SecP192R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000F5C6C File Offset: 0x000F3E6C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP192R1FieldElement(x);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000F5C74 File Offset: 0x000F3E74
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP192R1Point(this, x, y, withCompression);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000F5C7F File Offset: 0x000F3E7F
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP192R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000F5C8C File Offset: 0x000F3E8C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 6 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy(((SecP192R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 6;
				Nat192.Copy(((SecP192R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 6;
			}
			return new SecP192R1Curve.SecP192R1LookupTable(this, array, len);
		}

		// Token: 0x040018F0 RID: 6384
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF"));

		// Token: 0x040018F1 RID: 6385
		private const int SECP192R1_DEFAULT_COORDS = 2;

		// Token: 0x040018F2 RID: 6386
		private const int SECP192R1_FE_INTS = 6;

		// Token: 0x040018F3 RID: 6387
		protected readonly SecP192R1Point m_infinity;

		// Token: 0x020008FB RID: 2299
		private class SecP192R1LookupTable : ECLookupTable
		{
			// Token: 0x06004DB3 RID: 19891 RVA: 0x001B1B21 File Offset: 0x001AFD21
			internal SecP192R1LookupTable(SecP192R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C1F RID: 3103
			// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x001B1B3E File Offset: 0x001AFD3E
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DB5 RID: 19893 RVA: 0x001B1B48 File Offset: 0x001AFD48
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
				return this.m_outer.CreateRawPoint(new SecP192R1FieldElement(array), new SecP192R1FieldElement(array2), false);
			}

			// Token: 0x04003469 RID: 13417
			private readonly SecP192R1Curve m_outer;

			// Token: 0x0400346A RID: 13418
			private readonly uint[] m_table;

			// Token: 0x0400346B RID: 13419
			private readonly int m_size;
		}
	}
}
