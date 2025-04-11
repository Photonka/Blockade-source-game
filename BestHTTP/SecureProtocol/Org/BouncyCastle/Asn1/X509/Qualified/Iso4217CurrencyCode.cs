using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006B9 RID: 1721
	public class Iso4217CurrencyCode : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003FCC RID: 16332 RVA: 0x0017F554 File Offset: 0x0017D754
		public static Iso4217CurrencyCode GetInstance(object obj)
		{
			if (obj == null || obj is Iso4217CurrencyCode)
			{
				return (Iso4217CurrencyCode)obj;
			}
			if (obj is DerInteger)
			{
				return new Iso4217CurrencyCode(DerInteger.GetInstance(obj).Value.IntValue);
			}
			if (obj is DerPrintableString)
			{
				return new Iso4217CurrencyCode(DerPrintableString.GetInstance(obj).GetString());
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0017F5C4 File Offset: 0x0017D7C4
		public Iso4217CurrencyCode(int numeric)
		{
			if (numeric > 999 || numeric < 1)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"wrong size in numeric code : not in (",
					1,
					"..",
					999,
					")"
				}));
			}
			this.obj = new DerInteger(numeric);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0017F62E File Offset: 0x0017D82E
		public Iso4217CurrencyCode(string alphabetic)
		{
			if (alphabetic.Length > 3)
			{
				throw new ArgumentException("wrong size in alphabetic code : max size is " + 3);
			}
			this.obj = new DerPrintableString(alphabetic);
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x0017F661 File Offset: 0x0017D861
		public bool IsAlphabetic
		{
			get
			{
				return this.obj is DerPrintableString;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x0017F671 File Offset: 0x0017D871
		public string Alphabetic
		{
			get
			{
				return ((DerPrintableString)this.obj).GetString();
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x0017F683 File Offset: 0x0017D883
		public int Numeric
		{
			get
			{
				return ((DerInteger)this.obj).Value.IntValue;
			}
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0017F69A File Offset: 0x0017D89A
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x040027AB RID: 10155
		internal const int AlphabeticMaxSize = 3;

		// Token: 0x040027AC RID: 10156
		internal const int NumericMinSize = 1;

		// Token: 0x040027AD RID: 10157
		internal const int NumericMaxSize = 999;

		// Token: 0x040027AE RID: 10158
		internal Asn1Encodable obj;
	}
}
