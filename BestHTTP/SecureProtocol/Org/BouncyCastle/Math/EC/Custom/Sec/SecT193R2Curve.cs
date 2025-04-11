using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000383 RID: 899
	internal class SecT193R2Curve : AbstractF2mCurve
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x001026D4 File Offset: 0x001008D4
		public SecT193R2Curve() : base(193, 15, 0, 0)
		{
			this.m_infinity = new SecT193R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("0163F35A5137C2CE3EA6ED8667190B0BC43ECD69977702709B")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("00C9BB9E8927D4D64C377E2AB2856A5B16E3EFB7F61D4316AE")));
			this.m_order = new BigInteger(1, Hex.Decode("010000000000000000000000015AAB561B005413CCD4EE99D5"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0010275E File Offset: 0x0010095E
		protected override ECCurve CloneCurve()
		{
			return new SecT193R2Curve();
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00102765 File Offset: 0x00100965
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00101D35 File Offset: 0x000FFF35
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0010200D File Offset: 0x0010020D
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT193FieldElement(x);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0010276D File Offset: 0x0010096D
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT193R2Point(this, x, y, withCompression);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00102778 File Offset: 0x00100978
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT193R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00101D35 File Offset: 0x000FFF35
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00101F2D File Offset: 0x0010012D
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00102788 File Offset: 0x00100988
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT193R2Curve.SecT193R2LookupTable(this, array, len);
		}

		// Token: 0x04001962 RID: 6498
		private const int SECT193R2_DEFAULT_COORDS = 6;

		// Token: 0x04001963 RID: 6499
		private const int SECT193R2_FE_LONGS = 4;

		// Token: 0x04001964 RID: 6500
		protected readonly SecT193R2Point m_infinity;

		// Token: 0x0200090A RID: 2314
		private class SecT193R2LookupTable : ECLookupTable
		{
			// Token: 0x06004DE0 RID: 19936 RVA: 0x001B2669 File Offset: 0x001B0869
			internal SecT193R2LookupTable(SecT193R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C2E RID: 3118
			// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x001B2686 File Offset: 0x001B0886
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DE2 RID: 19938 RVA: 0x001B2690 File Offset: 0x001B0890
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 4; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 4 + j] & num2);
					}
					num += 8;
				}
				return this.m_outer.CreateRawPoint(new SecT193FieldElement(array), new SecT193FieldElement(array2), false);
			}

			// Token: 0x04003496 RID: 13462
			private readonly SecT193R2Curve m_outer;

			// Token: 0x04003497 RID: 13463
			private readonly ulong[] m_table;

			// Token: 0x04003498 RID: 13464
			private readonly int m_size;
		}
	}
}
