using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200038D RID: 909
	internal class SecT239K1Curve : AbstractF2mCurve
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x0010526C File Offset: 0x0010346C
		public SecT239K1Curve() : base(239, 158, 0, 0)
		{
			this.m_infinity = new SecT239K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.Decode("2000000000000000000000000000005A79FEC67CB6E91F1C1DA800E478A5"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x001052E5 File Offset: 0x001034E5
		protected override ECCurve CloneCurve()
		{
			return new SecT239K1Curve();
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x001000B9 File Offset: 0x000FE2B9
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x001052EC File Offset: 0x001034EC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00105029 File Offset: 0x00103229
		public override int FieldSize
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x001052F4 File Offset: 0x001034F4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT239FieldElement(x);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x001052FC File Offset: 0x001034FC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT239K1Point(this, x, y, withCompression);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00105307 File Offset: 0x00103507
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT239K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00105029 File Offset: 0x00103229
		public virtual int M
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00105221 File Offset: 0x00103421
		public virtual int K1
		{
			get
			{
				return 158;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00105314 File Offset: 0x00103514
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT239FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT239FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT239K1Curve.SecT239K1LookupTable(this, array, len);
		}

		// Token: 0x04001971 RID: 6513
		private const int SECT239K1_DEFAULT_COORDS = 6;

		// Token: 0x04001972 RID: 6514
		private const int SECT239K1_FE_LONGS = 4;

		// Token: 0x04001973 RID: 6515
		protected readonly SecT239K1Point m_infinity;

		// Token: 0x0200090D RID: 2317
		private class SecT239K1LookupTable : ECLookupTable
		{
			// Token: 0x06004DE9 RID: 19945 RVA: 0x001B28A9 File Offset: 0x001B0AA9
			internal SecT239K1LookupTable(SecT239K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x06004DEA RID: 19946 RVA: 0x001B28C6 File Offset: 0x001B0AC6
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DEB RID: 19947 RVA: 0x001B28D0 File Offset: 0x001B0AD0
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
				return this.m_outer.CreateRawPoint(new SecT239FieldElement(array), new SecT239FieldElement(array2), false);
			}

			// Token: 0x0400349F RID: 13471
			private readonly SecT239K1Curve m_outer;

			// Token: 0x040034A0 RID: 13472
			private readonly ulong[] m_table;

			// Token: 0x040034A1 RID: 13473
			private readonly int m_size;
		}
	}
}
