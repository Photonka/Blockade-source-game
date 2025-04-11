using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A1 RID: 1697
	public class CrlEntry : Asn1Encodable
	{
		// Token: 0x06003EEE RID: 16110 RVA: 0x0017B8F0 File Offset: 0x00179AF0
		public CrlEntry(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.seq = seq;
			this.userCertificate = DerInteger.GetInstance(seq[0]);
			this.revocationDate = Time.GetInstance(seq[1]);
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x0017B95B File Offset: 0x00179B5B
		public DerInteger UserCertificate
		{
			get
			{
				return this.userCertificate;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x0017B963 File Offset: 0x00179B63
		public Time RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x0017B96B File Offset: 0x00179B6B
		public X509Extensions Extensions
		{
			get
			{
				if (this.crlEntryExtensions == null && this.seq.Count == 3)
				{
					this.crlEntryExtensions = X509Extensions.GetInstance(this.seq[2]);
				}
				return this.crlEntryExtensions;
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0017B9A0 File Offset: 0x00179BA0
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x040026EB RID: 9963
		internal Asn1Sequence seq;

		// Token: 0x040026EC RID: 9964
		internal DerInteger userCertificate;

		// Token: 0x040026ED RID: 9965
		internal Time revocationDate;

		// Token: 0x040026EE RID: 9966
		internal X509Extensions crlEntryExtensions;
	}
}
