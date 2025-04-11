using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005DA RID: 1498
	public class CmsEnvelopedDataParser : CmsContentInfoParser
	{
		// Token: 0x06003953 RID: 14675 RVA: 0x00169650 File Offset: 0x00167850
		public CmsEnvelopedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x00169660 File Offset: 0x00167860
		public CmsEnvelopedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this._attrNotRead = true;
			this.envelopedData = new EnvelopedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1Set instance = Asn1Set.GetInstance(this.envelopedData.GetRecipientInfos().ToAsn1Object());
			EncryptedContentInfoParser encryptedContentInfo = this.envelopedData.GetEncryptedContentInfo();
			this._encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			CmsReadable readable = new CmsProcessableInputStream(((Asn1OctetStringParser)encryptedContentInfo.GetEncryptedContent(4)).GetOctetStream());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsEnvelopedSecureReadable(this._encAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(instance, secureReadable);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x001696F7 File Offset: 0x001678F7
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this._encAlg;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x001696FF File Offset: 0x001678FF
		public string EncryptionAlgOid
		{
			get
			{
				return this._encAlg.Algorithm.Id;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x00169714 File Offset: 0x00167914
		public Asn1Object EncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this._encAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00169738 File Offset: 0x00167938
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x00169740 File Offset: 0x00167940
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this._unprotectedAttributes == null && this._attrNotRead)
			{
				Asn1SetParser unprotectedAttrs = this.envelopedData.GetUnprotectedAttrs();
				this._attrNotRead = false;
				if (unprotectedAttrs != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = unprotectedAttrs.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this._unprotectedAttributes = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this._unprotectedAttributes;
		}

		// Token: 0x040024A3 RID: 9379
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x040024A4 RID: 9380
		internal EnvelopedDataParser envelopedData;

		// Token: 0x040024A5 RID: 9381
		private AlgorithmIdentifier _encAlg;

		// Token: 0x040024A6 RID: 9382
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable _unprotectedAttributes;

		// Token: 0x040024A7 RID: 9383
		private bool _attrNotRead;
	}
}
