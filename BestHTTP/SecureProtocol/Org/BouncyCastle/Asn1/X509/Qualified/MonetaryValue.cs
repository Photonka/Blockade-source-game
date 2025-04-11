using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006BA RID: 1722
	public class MonetaryValue : Asn1Encodable
	{
		// Token: 0x06003FD3 RID: 16339 RVA: 0x0017F6A8 File Offset: 0x0017D8A8
		public static MonetaryValue GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryValue)
			{
				return (MonetaryValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryValue(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x0017F6F8 File Offset: 0x0017D8F8
		private MonetaryValue(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.currency = Iso4217CurrencyCode.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x0017F76A File Offset: 0x0017D96A
		public MonetaryValue(Iso4217CurrencyCode currency, int amount, int exponent)
		{
			this.currency = currency;
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0017F791 File Offset: 0x0017D991
		public Iso4217CurrencyCode Currency
		{
			get
			{
				return this.currency;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x0017F799 File Offset: 0x0017D999
		public BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x0017F7A6 File Offset: 0x0017D9A6
		public BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0017F7B3 File Offset: 0x0017D9B3
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x040027AF RID: 10159
		internal Iso4217CurrencyCode currency;

		// Token: 0x040027B0 RID: 10160
		internal DerInteger amount;

		// Token: 0x040027B1 RID: 10161
		internal DerInteger exponent;
	}
}
