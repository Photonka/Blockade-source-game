using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000307 RID: 775
	public abstract class ECCurve
	{
		// Token: 0x06001DAC RID: 7596 RVA: 0x000E3404 File Offset: 0x000E1604
		public static int[] GetAllCoordinateSystems()
		{
			return new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			};
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000E3417 File Offset: 0x000E1617
		protected ECCurve(IFiniteField field)
		{
			this.m_field = field;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001DAE RID: 7598
		public abstract int FieldSize { get; }

		// Token: 0x06001DAF RID: 7599
		public abstract ECFieldElement FromBigInteger(BigInteger x);

		// Token: 0x06001DB0 RID: 7600
		public abstract bool IsValidFieldElement(BigInteger x);

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000E3426 File Offset: 0x000E1626
		public virtual ECCurve.Config Configure()
		{
			return new ECCurve.Config(this, this.m_coord, this.m_endomorphism, this.m_multiplier);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000E3440 File Offset: 0x000E1640
		public virtual ECPoint ValidatePoint(BigInteger x, BigInteger y)
		{
			ECPoint ecpoint = this.CreatePoint(x, y);
			if (!ecpoint.IsValid())
			{
				throw new ArgumentException("Invalid point coordinates");
			}
			return ecpoint;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000E345D File Offset: 0x000E165D
		[Obsolete("Per-point compression property will be removed")]
		public virtual ECPoint ValidatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			ECPoint ecpoint = this.CreatePoint(x, y, withCompression);
			if (!ecpoint.IsValid())
			{
				throw new ArgumentException("Invalid point coordinates");
			}
			return ecpoint;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000E347B File Offset: 0x000E167B
		public virtual ECPoint CreatePoint(BigInteger x, BigInteger y)
		{
			return this.CreatePoint(x, y, false);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000E3486 File Offset: 0x000E1686
		[Obsolete("Per-point compression property will be removed")]
		public virtual ECPoint CreatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			return this.CreateRawPoint(this.FromBigInteger(x), this.FromBigInteger(y), withCompression);
		}

		// Token: 0x06001DB6 RID: 7606
		protected abstract ECCurve CloneCurve();

		// Token: 0x06001DB7 RID: 7607
		protected internal abstract ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression);

		// Token: 0x06001DB8 RID: 7608
		protected internal abstract ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression);

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000E34A0 File Offset: 0x000E16A0
		protected virtual ECMultiplier CreateDefaultMultiplier()
		{
			GlvEndomorphism glvEndomorphism = this.m_endomorphism as GlvEndomorphism;
			if (glvEndomorphism != null)
			{
				return new GlvMultiplier(this, glvEndomorphism);
			}
			return new WNafL2RMultiplier();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000E34C9 File Offset: 0x000E16C9
		public virtual bool SupportsCoordinateSystem(int coord)
		{
			return coord == 0;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000E34D0 File Offset: 0x000E16D0
		public virtual PreCompInfo GetPreCompInfo(ECPoint point, string name)
		{
			this.CheckPoint(point);
			IDictionary preCompTable;
			lock (point)
			{
				preCompTable = point.m_preCompTable;
			}
			if (preCompTable == null)
			{
				return null;
			}
			IDictionary obj = preCompTable;
			PreCompInfo result;
			lock (obj)
			{
				result = (PreCompInfo)preCompTable[name];
			}
			return result;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000E354C File Offset: 0x000E174C
		public virtual PreCompInfo Precompute(ECPoint point, string name, IPreCompCallback callback)
		{
			this.CheckPoint(point);
			IDictionary dictionary;
			lock (point)
			{
				dictionary = point.m_preCompTable;
				if (dictionary == null)
				{
					dictionary = (point.m_preCompTable = Platform.CreateHashtable(4));
				}
			}
			IDictionary obj = dictionary;
			PreCompInfo result;
			lock (obj)
			{
				PreCompInfo preCompInfo = (PreCompInfo)dictionary[name];
				PreCompInfo preCompInfo2 = callback.Precompute(preCompInfo);
				if (preCompInfo2 != preCompInfo)
				{
					dictionary[name] = preCompInfo2;
				}
				result = preCompInfo2;
			}
			return result;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000E35F4 File Offset: 0x000E17F4
		public virtual ECPoint ImportPoint(ECPoint p)
		{
			if (this == p.Curve)
			{
				return p;
			}
			if (p.IsInfinity)
			{
				return this.Infinity;
			}
			p = p.Normalize();
			return this.CreatePoint(p.XCoord.ToBigInteger(), p.YCoord.ToBigInteger(), p.IsCompressed);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000E3645 File Offset: 0x000E1845
		public virtual void NormalizeAll(ECPoint[] points)
		{
			this.NormalizeAll(points, 0, points.Length, null);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000E3654 File Offset: 0x000E1854
		public virtual void NormalizeAll(ECPoint[] points, int off, int len, ECFieldElement iso)
		{
			this.CheckPoints(points, off, len);
			int coordinateSystem = this.CoordinateSystem;
			if (coordinateSystem == 0 || coordinateSystem == 5)
			{
				if (iso != null)
				{
					throw new ArgumentException("not valid for affine coordinates", "iso");
				}
				return;
			}
			else
			{
				ECFieldElement[] array = new ECFieldElement[len];
				int[] array2 = new int[len];
				int num = 0;
				for (int i = 0; i < len; i++)
				{
					ECPoint ecpoint = points[off + i];
					if (ecpoint != null && (iso != null || !ecpoint.IsNormalized()))
					{
						array[num] = ecpoint.GetZCoord(0);
						array2[num++] = off + i;
					}
				}
				if (num == 0)
				{
					return;
				}
				ECAlgorithms.MontgomeryTrick(array, 0, num, iso);
				for (int j = 0; j < num; j++)
				{
					int num2 = array2[j];
					points[num2] = points[num2].Normalize(array[j]);
				}
				return;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001DC0 RID: 7616
		public abstract ECPoint Infinity { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x000E3711 File Offset: 0x000E1911
		public virtual IFiniteField Field
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x000E3719 File Offset: 0x000E1919
		public virtual ECFieldElement A
		{
			get
			{
				return this.m_a;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000E3721 File Offset: 0x000E1921
		public virtual ECFieldElement B
		{
			get
			{
				return this.m_b;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x000E3729 File Offset: 0x000E1929
		public virtual BigInteger Order
		{
			get
			{
				return this.m_order;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000E3731 File Offset: 0x000E1931
		public virtual BigInteger Cofactor
		{
			get
			{
				return this.m_cofactor;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x000E3739 File Offset: 0x000E1939
		public virtual int CoordinateSystem
		{
			get
			{
				return this.m_coord;
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000E3744 File Offset: 0x000E1944
		public virtual ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			int num = (this.FieldSize + 7) / 8;
			byte[] array = new byte[len * num * 2];
			int num2 = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				byte[] array2 = ecpoint.RawXCoord.ToBigInteger().ToByteArray();
				byte[] array3 = ecpoint.RawYCoord.ToBigInteger().ToByteArray();
				int num3 = (array2.Length > num) ? 1 : 0;
				int num4 = array2.Length - num3;
				int num5 = (array3.Length > num) ? 1 : 0;
				int num6 = array3.Length - num5;
				Array.Copy(array2, num3, array, num2 + num - num4, num4);
				num2 += num;
				Array.Copy(array3, num5, array, num2 + num - num6, num6);
				num2 += num;
			}
			return new ECCurve.DefaultLookupTable(this, array, len);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000E37FA File Offset: 0x000E19FA
		protected virtual void CheckPoint(ECPoint point)
		{
			if (point == null || this != point.Curve)
			{
				throw new ArgumentException("must be non-null and on this curve", "point");
			}
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000E3818 File Offset: 0x000E1A18
		protected virtual void CheckPoints(ECPoint[] points)
		{
			this.CheckPoints(points, 0, points.Length);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000E3828 File Offset: 0x000E1A28
		protected virtual void CheckPoints(ECPoint[] points, int off, int len)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (off < 0 || len < 0 || off > points.Length - len)
			{
				throw new ArgumentException("invalid range specified", "points");
			}
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				if (ecpoint != null && this != ecpoint.Curve)
				{
					throw new ArgumentException("entries must be null or on this curve", "points");
				}
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000E3894 File Offset: 0x000E1A94
		public virtual bool Equals(ECCurve other)
		{
			return this == other || (other != null && (this.Field.Equals(other.Field) && this.A.ToBigInteger().Equals(other.A.ToBigInteger())) && this.B.ToBigInteger().Equals(other.B.ToBigInteger()));
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000E38F9 File Offset: 0x000E1AF9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECCurve);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000E3907 File Offset: 0x000E1B07
		public override int GetHashCode()
		{
			return this.Field.GetHashCode() ^ Integers.RotateLeft(this.A.ToBigInteger().GetHashCode(), 8) ^ Integers.RotateLeft(this.B.ToBigInteger().GetHashCode(), 16);
		}

		// Token: 0x06001DCE RID: 7630
		protected abstract ECPoint DecompressPoint(int yTilde, BigInteger X1);

		// Token: 0x06001DCF RID: 7631 RVA: 0x000E3943 File Offset: 0x000E1B43
		public virtual ECEndomorphism GetEndomorphism()
		{
			return this.m_endomorphism;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000E394C File Offset: 0x000E1B4C
		public virtual ECMultiplier GetMultiplier()
		{
			ECMultiplier multiplier;
			lock (this)
			{
				if (this.m_multiplier == null)
				{
					this.m_multiplier = this.CreateDefaultMultiplier();
				}
				multiplier = this.m_multiplier;
			}
			return multiplier;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000E39A0 File Offset: 0x000E1BA0
		public virtual ECPoint DecodePoint(byte[] encoded)
		{
			int num = (this.FieldSize + 7) / 8;
			byte b = encoded[0];
			ECPoint ecpoint;
			switch (b)
			{
			case 0:
				if (encoded.Length != 1)
				{
					throw new ArgumentException("Incorrect length for infinity encoding", "encoded");
				}
				ecpoint = this.Infinity;
				goto IL_159;
			case 2:
			case 3:
			{
				if (encoded.Length != num + 1)
				{
					throw new ArgumentException("Incorrect length for compressed encoding", "encoded");
				}
				int yTilde = (int)(b & 1);
				BigInteger x = new BigInteger(1, encoded, 1, num);
				ecpoint = this.DecompressPoint(yTilde, x);
				if (!ecpoint.ImplIsValid(true, true))
				{
					throw new ArgumentException("Invalid point");
				}
				goto IL_159;
			}
			case 4:
			{
				if (encoded.Length != 2 * num + 1)
				{
					throw new ArgumentException("Incorrect length for uncompressed encoding", "encoded");
				}
				BigInteger x2 = new BigInteger(1, encoded, 1, num);
				BigInteger y = new BigInteger(1, encoded, 1 + num, num);
				ecpoint = this.ValidatePoint(x2, y);
				goto IL_159;
			}
			case 6:
			case 7:
			{
				if (encoded.Length != 2 * num + 1)
				{
					throw new ArgumentException("Incorrect length for hybrid encoding", "encoded");
				}
				BigInteger x3 = new BigInteger(1, encoded, 1, num);
				BigInteger bigInteger = new BigInteger(1, encoded, 1 + num, num);
				if (bigInteger.TestBit(0) != (b == 7))
				{
					throw new ArgumentException("Inconsistent Y coordinate in hybrid encoding", "encoded");
				}
				ecpoint = this.ValidatePoint(x3, bigInteger);
				goto IL_159;
			}
			}
			throw new FormatException("Invalid point encoding " + b);
			IL_159:
			if (b != 0 && ecpoint.IsInfinity)
			{
				throw new ArgumentException("Invalid infinity encoding", "encoded");
			}
			return ecpoint;
		}

		// Token: 0x04001811 RID: 6161
		public const int COORD_AFFINE = 0;

		// Token: 0x04001812 RID: 6162
		public const int COORD_HOMOGENEOUS = 1;

		// Token: 0x04001813 RID: 6163
		public const int COORD_JACOBIAN = 2;

		// Token: 0x04001814 RID: 6164
		public const int COORD_JACOBIAN_CHUDNOVSKY = 3;

		// Token: 0x04001815 RID: 6165
		public const int COORD_JACOBIAN_MODIFIED = 4;

		// Token: 0x04001816 RID: 6166
		public const int COORD_LAMBDA_AFFINE = 5;

		// Token: 0x04001817 RID: 6167
		public const int COORD_LAMBDA_PROJECTIVE = 6;

		// Token: 0x04001818 RID: 6168
		public const int COORD_SKEWED = 7;

		// Token: 0x04001819 RID: 6169
		protected readonly IFiniteField m_field;

		// Token: 0x0400181A RID: 6170
		protected ECFieldElement m_a;

		// Token: 0x0400181B RID: 6171
		protected ECFieldElement m_b;

		// Token: 0x0400181C RID: 6172
		protected BigInteger m_order;

		// Token: 0x0400181D RID: 6173
		protected BigInteger m_cofactor;

		// Token: 0x0400181E RID: 6174
		protected int m_coord;

		// Token: 0x0400181F RID: 6175
		protected ECEndomorphism m_endomorphism;

		// Token: 0x04001820 RID: 6176
		protected ECMultiplier m_multiplier;

		// Token: 0x020008E7 RID: 2279
		public class Config
		{
			// Token: 0x06004D86 RID: 19846 RVA: 0x001B0DF9 File Offset: 0x001AEFF9
			internal Config(ECCurve outer, int coord, ECEndomorphism endomorphism, ECMultiplier multiplier)
			{
				this.outer = outer;
				this.coord = coord;
				this.endomorphism = endomorphism;
				this.multiplier = multiplier;
			}

			// Token: 0x06004D87 RID: 19847 RVA: 0x001B0E1E File Offset: 0x001AF01E
			public ECCurve.Config SetCoordinateSystem(int coord)
			{
				this.coord = coord;
				return this;
			}

			// Token: 0x06004D88 RID: 19848 RVA: 0x001B0E28 File Offset: 0x001AF028
			public ECCurve.Config SetEndomorphism(ECEndomorphism endomorphism)
			{
				this.endomorphism = endomorphism;
				return this;
			}

			// Token: 0x06004D89 RID: 19849 RVA: 0x001B0E32 File Offset: 0x001AF032
			public ECCurve.Config SetMultiplier(ECMultiplier multiplier)
			{
				this.multiplier = multiplier;
				return this;
			}

			// Token: 0x06004D8A RID: 19850 RVA: 0x001B0E3C File Offset: 0x001AF03C
			public ECCurve Create()
			{
				if (!this.outer.SupportsCoordinateSystem(this.coord))
				{
					throw new InvalidOperationException("unsupported coordinate system");
				}
				ECCurve eccurve = this.outer.CloneCurve();
				if (eccurve == this.outer)
				{
					throw new InvalidOperationException("implementation returned current curve");
				}
				eccurve.m_coord = this.coord;
				eccurve.m_endomorphism = this.endomorphism;
				eccurve.m_multiplier = this.multiplier;
				return eccurve;
			}

			// Token: 0x0400342C RID: 13356
			protected ECCurve outer;

			// Token: 0x0400342D RID: 13357
			protected int coord;

			// Token: 0x0400342E RID: 13358
			protected ECEndomorphism endomorphism;

			// Token: 0x0400342F RID: 13359
			protected ECMultiplier multiplier;
		}

		// Token: 0x020008E8 RID: 2280
		private class DefaultLookupTable : ECLookupTable
		{
			// Token: 0x06004D8B RID: 19851 RVA: 0x001B0EAA File Offset: 0x001AF0AA
			internal DefaultLookupTable(ECCurve outer, byte[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x06004D8C RID: 19852 RVA: 0x001B0EC7 File Offset: 0x001AF0C7
			public virtual int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06004D8D RID: 19853 RVA: 0x001B0ED0 File Offset: 0x001AF0D0
			public virtual ECPoint Lookup(int index)
			{
				int num = (this.m_outer.FieldSize + 7) / 8;
				byte[] array = new byte[num];
				byte[] array2 = new byte[num];
				int num2 = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					byte b = (byte)((i ^ index) - 1 >> 31);
					for (int j = 0; j < num; j++)
					{
						byte[] array3 = array;
						int num3 = j;
						array3[num3] ^= (this.m_table[num2 + j] & b);
						byte[] array4 = array2;
						int num4 = j;
						array4[num4] ^= (this.m_table[num2 + num + j] & b);
					}
					num2 += num * 2;
				}
				ECFieldElement x = this.m_outer.FromBigInteger(new BigInteger(1, array));
				ECFieldElement y = this.m_outer.FromBigInteger(new BigInteger(1, array2));
				return this.m_outer.CreateRawPoint(x, y, false);
			}

			// Token: 0x04003430 RID: 13360
			private readonly ECCurve m_outer;

			// Token: 0x04003431 RID: 13361
			private readonly byte[] m_table;

			// Token: 0x04003432 RID: 13362
			private readonly int m_size;
		}
	}
}
