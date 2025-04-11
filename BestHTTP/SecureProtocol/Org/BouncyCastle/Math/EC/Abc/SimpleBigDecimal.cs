using System;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x020003A9 RID: 937
	internal class SimpleBigDecimal
	{
		// Token: 0x0600270A RID: 9994 RVA: 0x0010C588 File Offset: 0x0010A788
		public static SimpleBigDecimal GetInstance(BigInteger val, int scale)
		{
			return new SimpleBigDecimal(val.ShiftLeft(scale), scale);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0010C597 File Offset: 0x0010A797
		public SimpleBigDecimal(BigInteger bigInt, int scale)
		{
			if (scale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			this.bigInt = bigInt;
			this.scale = scale;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0010C5BC File Offset: 0x0010A7BC
		private SimpleBigDecimal(SimpleBigDecimal limBigDec)
		{
			this.bigInt = limBigDec.bigInt;
			this.scale = limBigDec.scale;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0010C5DC File Offset: 0x0010A7DC
		private void CheckScale(SimpleBigDecimal b)
		{
			if (this.scale != b.scale)
			{
				throw new ArgumentException("Only SimpleBigDecimal of same scale allowed in arithmetic operations");
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0010C5F7 File Offset: 0x0010A7F7
		public SimpleBigDecimal AdjustScale(int newScale)
		{
			if (newScale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			if (newScale == this.scale)
			{
				return this;
			}
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(newScale - this.scale), newScale);
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0010C62C File Offset: 0x0010A82C
		public SimpleBigDecimal Add(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Add(b.bigInt), this.scale);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0010C651 File Offset: 0x0010A851
		public SimpleBigDecimal Add(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Add(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0010C675 File Offset: 0x0010A875
		public SimpleBigDecimal Negate()
		{
			return new SimpleBigDecimal(this.bigInt.Negate(), this.scale);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0010C68D File Offset: 0x0010A88D
		public SimpleBigDecimal Subtract(SimpleBigDecimal b)
		{
			return this.Add(b.Negate());
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0010C69B File Offset: 0x0010A89B
		public SimpleBigDecimal Subtract(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Subtract(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0010C6BF File Offset: 0x0010A8BF
		public SimpleBigDecimal Multiply(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Multiply(b.bigInt), this.scale + this.scale);
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0010C6EB File Offset: 0x0010A8EB
		public SimpleBigDecimal Multiply(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Multiply(b), this.scale);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0010C704 File Offset: 0x0010A904
		public SimpleBigDecimal Divide(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(this.scale).Divide(b.bigInt), this.scale);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0010C734 File Offset: 0x0010A934
		public SimpleBigDecimal Divide(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Divide(b), this.scale);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0010C74D File Offset: 0x0010A94D
		public SimpleBigDecimal ShiftLeft(int n)
		{
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(n), this.scale);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0010C766 File Offset: 0x0010A966
		public int CompareTo(SimpleBigDecimal val)
		{
			this.CheckScale(val);
			return this.bigInt.CompareTo(val.bigInt);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0010C780 File Offset: 0x0010A980
		public int CompareTo(BigInteger val)
		{
			return this.bigInt.CompareTo(val.ShiftLeft(this.scale));
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0010C799 File Offset: 0x0010A999
		public BigInteger Floor()
		{
			return this.bigInt.ShiftRight(this.scale);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0010C7AC File Offset: 0x0010A9AC
		public BigInteger Round()
		{
			SimpleBigDecimal simpleBigDecimal = new SimpleBigDecimal(BigInteger.One, 1);
			return this.Add(simpleBigDecimal.AdjustScale(this.scale)).Floor();
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0010C7DC File Offset: 0x0010A9DC
		public int IntValue
		{
			get
			{
				return this.Floor().IntValue;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x0010C7E9 File Offset: 0x0010A9E9
		public long LongValue
		{
			get
			{
				return this.Floor().LongValue;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0010C7F6 File Offset: 0x0010A9F6
		public int Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0010C800 File Offset: 0x0010AA00
		public override string ToString()
		{
			if (this.scale == 0)
			{
				return this.bigInt.ToString();
			}
			BigInteger bigInteger = this.Floor();
			BigInteger bigInteger2 = this.bigInt.Subtract(bigInteger.ShiftLeft(this.scale));
			if (this.bigInt.SignValue < 0)
			{
				bigInteger2 = BigInteger.One.ShiftLeft(this.scale).Subtract(bigInteger2);
			}
			if (bigInteger.SignValue == -1 && !bigInteger2.Equals(BigInteger.Zero))
			{
				bigInteger = bigInteger.Add(BigInteger.One);
			}
			string value = bigInteger.ToString();
			char[] array = new char[this.scale];
			string text = bigInteger2.ToString(2);
			int length = text.Length;
			int num = this.scale - length;
			for (int i = 0; i < num; i++)
			{
				array[i] = '0';
			}
			for (int j = 0; j < length; j++)
			{
				array[num + j] = text[j];
			}
			string value2 = new string(array);
			StringBuilder stringBuilder = new StringBuilder(value);
			stringBuilder.Append(".");
			stringBuilder.Append(value2);
			return stringBuilder.ToString();
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0010C918 File Offset: 0x0010AB18
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			SimpleBigDecimal simpleBigDecimal = obj as SimpleBigDecimal;
			return simpleBigDecimal != null && this.bigInt.Equals(simpleBigDecimal.bigInt) && this.scale == simpleBigDecimal.scale;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0010C95A File Offset: 0x0010AB5A
		public override int GetHashCode()
		{
			return this.bigInt.GetHashCode() ^ this.scale;
		}

		// Token: 0x040019A8 RID: 6568
		private readonly BigInteger bigInt;

		// Token: 0x040019A9 RID: 6569
		private readonly int scale;
	}
}
