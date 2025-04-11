using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000713 RID: 1811
	public class MonetaryLimit : Asn1Encodable
	{
		// Token: 0x06004218 RID: 16920 RVA: 0x00188124 File Offset: 0x00186324
		public static MonetaryLimit GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryLimit)
			{
				return (MonetaryLimit)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryLimit(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00188174 File Offset: 0x00186374
		private MonetaryLimit(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.currency = DerPrintableString.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x001881E1 File Offset: 0x001863E1
		public MonetaryLimit(string currency, int amount, int exponent)
		{
			this.currency = new DerPrintableString(currency, true);
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x0018820E File Offset: 0x0018640E
		public virtual string Currency
		{
			get
			{
				return this.currency.GetString();
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x0018821B File Offset: 0x0018641B
		public virtual BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x00188228 File Offset: 0x00186428
		public virtual BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00188235 File Offset: 0x00186435
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x04002A2E RID: 10798
		private readonly DerPrintableString currency;

		// Token: 0x04002A2F RID: 10799
		private readonly DerInteger amount;

		// Token: 0x04002A30 RID: 10800
		private readonly DerInteger exponent;
	}
}
