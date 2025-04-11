using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x020003A1 RID: 929
	internal class SM2P256V1Curve : AbstractFpCurve
	{
		// Token: 0x06002688 RID: 9864 RVA: 0x0010A52C File Offset: 0x0010872C
		public SM2P256V1Curve() : base(SM2P256V1Curve.q)
		{
			this.m_infinity = new SM2P256V1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93")));
			this.m_order = new BigInteger(1, Hex.Decode("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0010A5B2 File Offset: 0x001087B2
		protected override ECCurve CloneCurve()
		{
			return new SM2P256V1Curve();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000F1DFD File Offset: 0x000EFFFD
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x0010A5B9 File Offset: 0x001087B9
		public virtual BigInteger Q
		{
			get
			{
				return SM2P256V1Curve.q;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0010A5C0 File Offset: 0x001087C0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x0010A5C8 File Offset: 0x001087C8
		public override int FieldSize
		{
			get
			{
				return SM2P256V1Curve.q.BitLength;
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0010A5D4 File Offset: 0x001087D4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SM2P256V1FieldElement(x);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0010A5DC File Offset: 0x001087DC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SM2P256V1Point(this, x, y, withCompression);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0010A5E7 File Offset: 0x001087E7
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SM2P256V1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x0010A5F4 File Offset: 0x001087F4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SM2P256V1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SM2P256V1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SM2P256V1Curve.SM2P256V1LookupTable(this, array, len);
		}

		// Token: 0x04001993 RID: 6547
		public static readonly BigInteger q = new BigInteger(1, Hex.Decode("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF"));

		// Token: 0x04001994 RID: 6548
		private const int SM2P256V1_DEFAULT_COORDS = 2;

		// Token: 0x04001995 RID: 6549
		private const int SM2P256V1_FE_INTS = 8;

		// Token: 0x04001996 RID: 6550
		protected readonly SM2P256V1Point m_infinity;

		// Token: 0x02000914 RID: 2324
		private class SM2P256V1LookupTable : ECLookupTable
		{
			// Token: 0x06004DFE RID: 19966 RVA: 0x001B2DF0 File Offset: 0x001B0FF0
			internal SM2P256V1LookupTable(SM2P256V1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x06004DFF RID: 19967 RVA: 0x001B2E0D File Offset: 0x001B100D
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004E00 RID: 19968 RVA: 0x001B2E18 File Offset: 0x001B1018
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
				return this.m_outer.CreateRawPoint(new SM2P256V1FieldElement(array), new SM2P256V1FieldElement(array2), false);
			}

			// Token: 0x040034B4 RID: 13492
			private readonly SM2P256V1Curve m_outer;

			// Token: 0x040034B5 RID: 13493
			private readonly uint[] m_table;

			// Token: 0x040034B6 RID: 13494
			private readonly int m_size;
		}
	}
}
