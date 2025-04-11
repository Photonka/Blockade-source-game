using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D6 RID: 1750
	public class CertBag : Asn1Encodable
	{
		// Token: 0x06004088 RID: 16520 RVA: 0x00182C80 File Offset: 0x00180E80
		public CertBag(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.certID = DerObjectIdentifier.GetInstance(seq[0]);
			this.certValue = Asn1TaggedObject.GetInstance(seq[1]).GetObject();
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x00182CD5 File Offset: 0x00180ED5
		public CertBag(DerObjectIdentifier certID, Asn1Object certValue)
		{
			this.certID = certID;
			this.certValue = certValue;
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x00182CEB File Offset: 0x00180EEB
		public DerObjectIdentifier CertID
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x00182CF3 File Offset: 0x00180EF3
		public Asn1Object CertValue
		{
			get
			{
				return this.certValue;
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x00182CFB File Offset: 0x00180EFB
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.certID,
				new DerTaggedObject(0, this.certValue)
			});
		}

		// Token: 0x04002876 RID: 10358
		private readonly DerObjectIdentifier certID;

		// Token: 0x04002877 RID: 10359
		private readonly Asn1Object certValue;
	}
}
