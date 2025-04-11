using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000310 RID: 784
	public class F2mFieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06001E3E RID: 7742 RVA: 0x000E5020 File Offset: 0x000E3220
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public F2mFieldElement(int m, int k1, int k2, int k3, BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > m)
			{
				throw new ArgumentException("value invalid in F2m field element", "x");
			}
			if (k2 == 0 && k3 == 0)
			{
				this.representation = 2;
				this.ks = new int[]
				{
					k1
				};
			}
			else
			{
				if (k2 >= k3)
				{
					throw new ArgumentException("k2 must be smaller than k3");
				}
				if (k2 <= 0)
				{
					throw new ArgumentException("k2 must be larger than 0");
				}
				this.representation = 3;
				this.ks = new int[]
				{
					k1,
					k2,
					k3
				};
			}
			this.m = m;
			this.x = new LongArray(x);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000E50CE File Offset: 0x000E32CE
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public F2mFieldElement(int m, int k, BigInteger x) : this(m, k, 0, 0, x)
		{
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000E50DB File Offset: 0x000E32DB
		internal F2mFieldElement(int m, int[] ks, LongArray x)
		{
			this.m = m;
			this.representation = ((ks.Length == 1) ? 2 : 3);
			this.ks = ks;
			this.x = x;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x000E5108 File Offset: 0x000E3308
		public override int BitLength
		{
			get
			{
				return this.x.Degree();
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000E5115 File Offset: 0x000E3315
		public override bool IsOne
		{
			get
			{
				return this.x.IsOne();
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x000E5122 File Offset: 0x000E3322
		public override bool IsZero
		{
			get
			{
				return this.x.IsZero();
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000E512F File Offset: 0x000E332F
		public override bool TestBitZero()
		{
			return this.x.TestBitZero();
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000E513C File Offset: 0x000E333C
		public override BigInteger ToBigInteger()
		{
			return this.x.ToBigInteger();
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000E5149 File Offset: 0x000E3349
		public override string FieldName
		{
			get
			{
				return "F2m";
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000E5150 File Offset: 0x000E3350
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000E5158 File Offset: 0x000E3358
		public static void CheckFieldElements(ECFieldElement a, ECFieldElement b)
		{
			if (!(a is F2mFieldElement) || !(b is F2mFieldElement))
			{
				throw new ArgumentException("Field elements are not both instances of F2mFieldElement");
			}
			F2mFieldElement f2mFieldElement = (F2mFieldElement)a;
			F2mFieldElement f2mFieldElement2 = (F2mFieldElement)b;
			if (f2mFieldElement.representation != f2mFieldElement2.representation)
			{
				throw new ArgumentException("One of the F2m field elements has incorrect representation");
			}
			if (f2mFieldElement.m != f2mFieldElement2.m || !Arrays.AreEqual(f2mFieldElement.ks, f2mFieldElement2.ks))
			{
				throw new ArgumentException("Field elements are not elements of the same field F2m");
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000E51D4 File Offset: 0x000E33D4
		public override ECFieldElement Add(ECFieldElement b)
		{
			LongArray longArray = this.x.Copy();
			F2mFieldElement f2mFieldElement = (F2mFieldElement)b;
			longArray.AddShiftedByWords(f2mFieldElement.x, 0);
			return new F2mFieldElement(this.m, this.ks, longArray);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000E5213 File Offset: 0x000E3413
		public override ECFieldElement AddOne()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.AddOne());
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000E523A File Offset: 0x000E343A
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModMultiply(((F2mFieldElement)b).x, this.m, this.ks));
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000E527C File Offset: 0x000E347C
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			LongArray longArray = this.x;
			LongArray longArray2 = ((F2mFieldElement)b).x;
			LongArray longArray3 = ((F2mFieldElement)x).x;
			LongArray other = ((F2mFieldElement)y).x;
			LongArray longArray4 = longArray.Multiply(longArray2, this.m, this.ks);
			LongArray other2 = longArray3.Multiply(other, this.m, this.ks);
			if (longArray4 == longArray || longArray4 == longArray2)
			{
				longArray4 = longArray4.Copy();
			}
			longArray4.AddShiftedByWords(other2, 0);
			longArray4.Reduce(this.m, this.ks);
			return new F2mFieldElement(this.m, this.ks, longArray4);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000E5318 File Offset: 0x000E3518
		public override ECFieldElement Divide(ECFieldElement b)
		{
			ECFieldElement b2 = b.Invert();
			return this.Multiply(b2);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000E5333 File Offset: 0x000E3533
		public override ECFieldElement Square()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModSquare(this.m, this.ks));
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000E5368 File Offset: 0x000E3568
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			LongArray longArray = this.x;
			LongArray longArray2 = ((F2mFieldElement)x).x;
			LongArray other = ((F2mFieldElement)y).x;
			LongArray longArray3 = longArray.Square(this.m, this.ks);
			LongArray other2 = longArray2.Multiply(other, this.m, this.ks);
			if (longArray3 == longArray)
			{
				longArray3 = longArray3.Copy();
			}
			longArray3.AddShiftedByWords(other2, 0);
			longArray3.Reduce(this.m, this.ks);
			return new F2mFieldElement(this.m, this.ks, longArray3);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000E53F0 File Offset: 0x000E35F0
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow >= 1)
			{
				return new F2mFieldElement(this.m, this.ks, this.x.ModSquareN(pow, this.m, this.ks));
			}
			return this;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000E5421 File Offset: 0x000E3621
		public override ECFieldElement Invert()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModInverse(this.m, this.ks));
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000E544B File Offset: 0x000E364B
		public override ECFieldElement Sqrt()
		{
			if (!this.x.IsZero() && !this.x.IsOne())
			{
				return this.SquarePow(this.m - 1);
			}
			return this;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000E5477 File Offset: 0x000E3677
		public int Representation
		{
			get
			{
				return this.representation;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000E5150 File Offset: 0x000E3350
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000E547F File Offset: 0x000E367F
		public int K1
		{
			get
			{
				return this.ks[0];
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000E5489 File Offset: 0x000E3689
		public int K2
		{
			get
			{
				if (this.ks.Length < 2)
				{
					return 0;
				}
				return this.ks[1];
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000E54A0 File Offset: 0x000E36A0
		public int K3
		{
			get
			{
				if (this.ks.Length < 3)
				{
					return 0;
				}
				return this.ks[2];
			}
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000E54B8 File Offset: 0x000E36B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			F2mFieldElement f2mFieldElement = obj as F2mFieldElement;
			return f2mFieldElement != null && this.Equals(f2mFieldElement);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000E54E0 File Offset: 0x000E36E0
		public virtual bool Equals(F2mFieldElement other)
		{
			return this.m == other.m && this.representation == other.representation && Arrays.AreEqual(this.ks, other.ks) && this.x.Equals(other.x);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000E552F File Offset: 0x000E372F
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.m ^ Arrays.GetHashCode(this.ks);
		}

		// Token: 0x0400182F RID: 6191
		public const int Gnb = 1;

		// Token: 0x04001830 RID: 6192
		public const int Tpb = 2;

		// Token: 0x04001831 RID: 6193
		public const int Ppb = 3;

		// Token: 0x04001832 RID: 6194
		private int representation;

		// Token: 0x04001833 RID: 6195
		private int m;

		// Token: 0x04001834 RID: 6196
		private int[] ks;

		// Token: 0x04001835 RID: 6197
		internal LongArray x;
	}
}
