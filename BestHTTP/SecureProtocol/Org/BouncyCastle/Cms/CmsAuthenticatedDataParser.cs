using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005CE RID: 1486
	public class CmsAuthenticatedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600391B RID: 14619 RVA: 0x00168978 File Offset: 0x00166B78
		public CmsAuthenticatedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x00168988 File Offset: 0x00166B88
		public CmsAuthenticatedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this.authAttrNotRead = true;
			this.authData = new AuthenticatedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1Set instance = Asn1Set.GetInstance(this.authData.GetRecipientInfos().ToAsn1Object());
			this.macAlg = this.authData.GetMacAlgorithm();
			CmsReadable readable = new CmsProcessableInputStream(((Asn1OctetStringParser)this.authData.GetEnapsulatedContentInfo().GetContent(4)).GetOctetStream());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsAuthenticatedSecureReadable(this.macAlg, readable);
			this._recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(instance, secureReadable);
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x00168A22 File Offset: 0x00166C22
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x00168A2A File Offset: 0x00166C2A
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.Algorithm.Id;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x00168A3C File Offset: 0x00166C3C
		public Asn1Object MacAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.macAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x00168A60 File Offset: 0x00166C60
		public RecipientInformationStore GetRecipientInfos()
		{
			return this._recipientInfoStore;
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x00168A68 File Offset: 0x00166C68
		public byte[] GetMac()
		{
			if (this.mac == null)
			{
				this.GetAuthAttrs();
				this.mac = this.authData.GetMac().GetOctets();
			}
			return Arrays.Clone(this.mac);
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x00168A9C File Offset: 0x00166C9C
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null && this.authAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetAuthAttrs();
				this.authAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this.authAttrs = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.authAttrs;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x00168B1C File Offset: 0x00166D1C
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null && this.unauthAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetUnauthAttrs();
				this.unauthAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this.unauthAttrs = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.unauthAttrs;
		}

		// Token: 0x04002482 RID: 9346
		internal RecipientInformationStore _recipientInfoStore;

		// Token: 0x04002483 RID: 9347
		internal AuthenticatedDataParser authData;

		// Token: 0x04002484 RID: 9348
		private AlgorithmIdentifier macAlg;

		// Token: 0x04002485 RID: 9349
		private byte[] mac;

		// Token: 0x04002486 RID: 9350
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable authAttrs;

		// Token: 0x04002487 RID: 9351
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unauthAttrs;

		// Token: 0x04002488 RID: 9352
		private bool authAttrNotRead;

		// Token: 0x04002489 RID: 9353
		private bool unauthAttrNotRead;
	}
}
