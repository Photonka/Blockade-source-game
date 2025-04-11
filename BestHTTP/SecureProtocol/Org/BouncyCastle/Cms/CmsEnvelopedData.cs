using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D8 RID: 1496
	public class CmsEnvelopedData
	{
		// Token: 0x06003945 RID: 14661 RVA: 0x001692ED File Offset: 0x001674ED
		public CmsEnvelopedData(byte[] envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x001692FB File Offset: 0x001674FB
		public CmsEnvelopedData(Stream envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x0016930C File Offset: 0x0016750C
		public CmsEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			EnvelopedData instance = EnvelopedData.GetInstance(contentInfo.Content);
			Asn1Set recipientInfos = instance.RecipientInfos;
			EncryptedContentInfo encryptedContentInfo = instance.EncryptedContentInfo;
			this.encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			CmsReadable readable = new CmsProcessableByteArray(encryptedContentInfo.EncryptedContent.GetOctets());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsEnvelopedSecureReadable(this.encAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.unprotectedAttributes = instance.UnprotectedAttrs;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x00169385 File Offset: 0x00167585
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this.encAlg;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x0016938D File Offset: 0x0016758D
		public string EncryptionAlgOid
		{
			get
			{
				return this.encAlg.Algorithm.Id;
			}
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x0016939F File Offset: 0x0016759F
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x001693A7 File Offset: 0x001675A7
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x001693AF File Offset: 0x001675AF
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this.unprotectedAttributes == null)
			{
				return null;
			}
			return new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unprotectedAttributes);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x001693C6 File Offset: 0x001675C6
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x0400249F RID: 9375
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x040024A0 RID: 9376
		internal ContentInfo contentInfo;

		// Token: 0x040024A1 RID: 9377
		private AlgorithmIdentifier encAlg;

		// Token: 0x040024A2 RID: 9378
		private Asn1Set unprotectedAttributes;
	}
}
