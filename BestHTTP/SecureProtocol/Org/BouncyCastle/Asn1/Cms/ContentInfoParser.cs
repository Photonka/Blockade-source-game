using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000770 RID: 1904
	public class ContentInfoParser
	{
		// Token: 0x06004490 RID: 17552 RVA: 0x00190E5B File Offset: 0x0018F05B
		public ContentInfoParser(Asn1SequenceParser seq)
		{
			this.contentType = (DerObjectIdentifier)seq.ReadObject();
			this.content = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x00190E85 File Offset: 0x0018F085
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x00190E8D File Offset: 0x0018F08D
		public IAsn1Convertible GetContent(int tag)
		{
			if (this.content == null)
			{
				return null;
			}
			return this.content.GetObjectParser(tag, true);
		}

		// Token: 0x04002BF5 RID: 11253
		private DerObjectIdentifier contentType;

		// Token: 0x04002BF6 RID: 11254
		private Asn1TaggedObjectParser content;
	}
}
