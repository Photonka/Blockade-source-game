using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200036F RID: 879
	internal class SecT113R2Curve : AbstractF2mCurve
	{
		// Token: 0x0600232E RID: 9006 RVA: 0x000FD4B0 File Offset: 0x000FB6B0
		public SecT113R2Curve() : base(113, 9, 0, 0)
		{
			this.m_infinity = new SecT113R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.Decode("00689918DBEC7E5A0DD6DFC0AA55C7")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.Decode("0095E9A9EC9B297BD4BF36E059184F")));
			this.m_order = new BigInteger(1, Hex.Decode("010000000000000108789B2496AF93"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000FD537 File Offset: 0x000FB737
		protected override ECCurve CloneCurve()
		{
			return new SecT113R2Curve();
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000FD53E File Offset: 0x000FB73E
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000FCDCB File Offset: 0x000FAFCB
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT113FieldElement(x);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000FD546 File Offset: 0x000FB746
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT113R2Point(this, x, y, withCompression);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000FD551 File Offset: 0x000FB751
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT113R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000FCAE3 File Offset: 0x000FACE3
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000FCCE5 File Offset: 0x000FAEE5
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000FD560 File Offset: 0x000FB760
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 2 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 2;
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 2;
			}
			return new SecT113R2Curve.SecT113R2LookupTable(this, array, len);
		}

		// Token: 0x04001942 RID: 6466
		private const int SECT113R2_DEFAULT_COORDS = 6;

		// Token: 0x04001943 RID: 6467
		private const int SECT113R2_FE_LONGS = 2;

		// Token: 0x04001944 RID: 6468
		protected readonly SecT113R2Point m_infinity;

		// Token: 0x02000903 RID: 2307
		private class SecT113R2LookupTable : ECLookupTable
		{
			// Token: 0x06004DCB RID: 19915 RVA: 0x001B2129 File Offset: 0x001B0329
			internal SecT113R2LookupTable(SecT113R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C27 RID: 3111
			// (get) Token: 0x06004DCC RID: 19916 RVA: 0x001B2146 File Offset: 0x001B0346
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004DCD RID: 19917 RVA: 0x001B2150 File Offset: 0x001B0350
			public virtual ECPoint Lookup(int index)
			{
				ulong[] array = Nat128.Create64();
				ulong[] array2 = Nat128.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 2; j++)
					{
						array[j] ^= (this.m_table[num + j] & num2);
						array2[j] ^= (this.m_table[num + 2 + j] & num2);
					}
					num += 4;
				}
				return this.m_outer.CreateRawPoint(new SecT113FieldElement(array), new SecT113FieldElement(array2), false);
			}

			// Token: 0x04003481 RID: 13441
			private readonly SecT113R2Curve m_outer;

			// Token: 0x04003482 RID: 13442
			private readonly ulong[] m_table;

			// Token: 0x04003483 RID: 13443
			private readonly int m_size;
		}
	}
}
