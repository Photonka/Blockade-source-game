using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000772 RID: 1906
	public class EncryptedContentInfoParser
	{
		// Token: 0x0600449A RID: 17562 RVA: 0x00190FC9 File Offset: 0x0018F1C9
		public EncryptedContentInfoParser(Asn1SequenceParser seq)
		{
			this._contentType = (DerObjectIdentifier)seq.ReadObject();
			this._contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encryptedContent = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x00191009 File Offset: 0x0018F209
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x00191011 File Offset: 0x0018F211
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this._contentEncryptionAlgorithm;
			}
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x00191019 File Offset: 0x0018F219
		public IAsn1Convertible GetEncryptedContent(int tag)
		{
			return this._encryptedContent.GetObjectParser(tag, false);
		}

		// Token: 0x04002BFA RID: 11258
		private DerObjectIdentifier _contentType;

		// Token: 0x04002BFB RID: 11259
		private AlgorithmIdentifier _contentEncryptionAlgorithm;

		// Token: 0x04002BFC RID: 11260
		private Asn1TaggedObjectParser _encryptedContent;
	}
}
