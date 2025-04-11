using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x020003A5 RID: 933
	internal class Curve25519 : AbstractFpCurve
	{
		// Token: 0x060026C6 RID: 9926 RVA: 0x0010B56C File Offset: 0x0010976C
		public Curve25519() : base(Curve25519.q)
		{
			this.m_infinity = new Curve25519Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("2AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA984914A144")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("7B425ED097B425ED097B425ED097B425ED097B425ED097B4260B5E9C7710C864")));
			this.m_order = new BigInteger(1, Hex.Decode("1000000000000000000000000000000014DEF9DEA2F79CD65812631A5CF5D3ED"));
			this.m_cofactor = BigInteger.ValueOf(8L);
			this.m_coord = 4;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0010B5F4 File Offset: 0x001097F4
		protected override ECCurve CloneCurve()
		{
			return new Curve25519();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0010B5FB File Offset: 0x001097FB
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 4;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x0010B604 File Offset: 0x00109804
		public virtual BigInteger Q
		{
			get
			{
				return Curve25519.q;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0010B60B File Offset: 0x0010980B
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0010B613 File Offset: 0x00109813
		public override int FieldSize
		{
			get
			{
				return Curve25519.q.BitLength;
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0010B61F File Offset: 0x0010981F
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new Curve25519FieldElement(x);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0010B627 File Offset: 0x00109827
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new Curve25519Point(this, x, y, withCompression);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0010B632 File Offset: 0x00109832
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new Curve25519Point(this, x, y, zs, withCompression);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0010B640 File Offset: 0x00109840
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((Curve25519FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((Curve25519FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new Curve25519.Curve25519LookupTable(this, array, len);
		}

		// Token: 0x0400199D RID: 6557
		public static readonly BigInteger q = Nat256.ToBigInteger(Curve25519Field.P);

		// Token: 0x0400199E RID: 6558
		private const int Curve25519_DEFAULT_COORDS = 4;

		// Token: 0x0400199F RID: 6559
		private const int CURVE25519_FE_INTS = 8;

		// Token: 0x040019A0 RID: 6560
		protected readonly Curve25519Point m_infinity;

		// Token: 0x02000915 RID: 2325
		private class Curve25519LookupTable : ECLookupTable
		{
			// Token: 0x06004E01 RID: 19969 RVA: 0x001B2EB1 File Offset: 0x001B10B1
			internal Curve25519LookupTable(Curve25519 outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x06004E02 RID: 19970 RVA: 0x001B2ECE File Offset: 0x001B10CE
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004E03 RID: 19971 RVA: 0x001B2ED8 File Offset: 0x001B10D8
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
				return this.m_outer.CreateRawPoint(new Curve25519FieldElement(array), new Curve25519FieldElement(array2), false);
			}

			// Token: 0x040034B7 RID: 13495
			private readonly Curve25519 m_outer;

			// Token: 0x040034B8 RID: 13496
			private readonly uint[] m_table;

			// Token: 0x040034B9 RID: 13497
			private readonly int m_size;
		}
	}
}
