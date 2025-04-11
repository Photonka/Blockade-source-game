using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E6 RID: 1510
	public class CmsSignedData
	{
		// Token: 0x0600399E RID: 14750 RVA: 0x0016A3AC File Offset: 0x001685AC
		private CmsSignedData(CmsSignedData c)
		{
			this.signedData = c.signedData;
			this.contentInfo = c.contentInfo;
			this.signedContent = c.signedContent;
			this.signerInfoStore = c.signerInfoStore;
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x0016A3E4 File Offset: 0x001685E4
		public CmsSignedData(byte[] sigBlock) : this(CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x0016A3F8 File Offset: 0x001685F8
		public CmsSignedData(CmsProcessable signedContent, byte[] sigBlock) : this(signedContent, CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x0016A40D File Offset: 0x0016860D
		public CmsSignedData(IDictionary hashes, byte[] sigBlock) : this(hashes, CmsUtilities.ReadContentInfo(sigBlock))
		{
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0016A41C File Offset: 0x0016861C
		public CmsSignedData(CmsProcessable signedContent, Stream sigData) : this(signedContent, CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0016A42B File Offset: 0x0016862B
		public CmsSignedData(Stream sigData) : this(CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0016A439 File Offset: 0x00168639
		public CmsSignedData(CmsProcessable signedContent, ContentInfo sigData)
		{
			this.signedContent = signedContent;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x0016A465 File Offset: 0x00168665
		public CmsSignedData(IDictionary hashes, ContentInfo sigData)
		{
			this.hashes = hashes;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0016A494 File Offset: 0x00168694
		public CmsSignedData(ContentInfo sigData)
		{
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
			if (this.signedData.EncapContentInfo.Content != null)
			{
				this.signedContent = new CmsProcessableByteArray(((Asn1OctetString)this.signedData.EncapContentInfo.Content).GetOctets());
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x0016A4FB File Offset: 0x001686FB
		public int Version
		{
			get
			{
				return this.signedData.Version.Value.IntValue;
			}
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x0016A514 File Offset: 0x00168714
		public SignerInformationStore GetSignerInfos()
		{
			if (this.signerInfoStore == null)
			{
				IList list = Platform.CreateArrayList();
				foreach (object obj in this.signedData.SignerInfos)
				{
					SignerInfo instance = SignerInfo.GetInstance(obj);
					DerObjectIdentifier contentType = this.signedData.EncapContentInfo.ContentType;
					if (this.hashes == null)
					{
						list.Add(new SignerInformation(instance, contentType, this.signedContent, null));
					}
					else
					{
						byte[] digest = (byte[])this.hashes[instance.DigestAlgorithm.Algorithm.Id];
						list.Add(new SignerInformation(instance, contentType, null, new BaseDigestCalculator(digest)));
					}
				}
				this.signerInfoStore = new SignerInformationStore(list);
			}
			return this.signerInfoStore;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0016A5FC File Offset: 0x001687FC
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this.attrCertStore == null)
			{
				this.attrCertStore = CmsSignedData.Helper.CreateAttributeStore(type, this.signedData.Certificates);
			}
			return this.attrCertStore;
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0016A628 File Offset: 0x00168828
		public IX509Store GetCertificates(string type)
		{
			if (this.certificateStore == null)
			{
				this.certificateStore = CmsSignedData.Helper.CreateCertificateStore(type, this.signedData.Certificates);
			}
			return this.certificateStore;
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0016A654 File Offset: 0x00168854
		public IX509Store GetCrls(string type)
		{
			if (this.crlStore == null)
			{
				this.crlStore = CmsSignedData.Helper.CreateCrlStore(type, this.signedData.CRLs);
			}
			return this.crlStore;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x0016A680 File Offset: 0x00168880
		[Obsolete("Use 'SignedContentType' property instead.")]
		public string SignedContentTypeOid
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType.Id;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x0016A697 File Offset: 0x00168897
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x0016A6A9 File Offset: 0x001688A9
		public CmsProcessable SignedContent
		{
			get
			{
				return this.signedContent;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x0016A6B1 File Offset: 0x001688B1
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0016A6B9 File Offset: 0x001688B9
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0016A6C8 File Offset: 0x001688C8
		public static CmsSignedData ReplaceSigners(CmsSignedData signedData, SignerInformationStore signerInformationStore)
		{
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			cmsSignedData.signerInfoStore = signerInformationStore;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in signerInformationStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					CmsSignedData.Helper.FixAlgID(signerInformation.DigestAlgorithmID)
				});
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					signerInformation.ToSignerInfo()
				});
			}
			Asn1Set asn1Set = new DerSet(asn1EncodableVector);
			Asn1Set asn1Set2 = new DerSet(asn1EncodableVector2);
			Asn1Sequence asn1Sequence = (Asn1Sequence)signedData.signedData.ToAsn1Object();
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
			{
				asn1Sequence[0],
				asn1Set
			});
			for (int num = 2; num != asn1Sequence.Count - 1; num++)
			{
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					asn1Sequence[num]
				});
			}
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				asn1Set2
			});
			cmsSignedData.signedData = SignedData.GetInstance(new BerSequence(asn1EncodableVector2));
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0016A828 File Offset: 0x00168A28
		public static CmsSignedData ReplaceCertificatesAndCrls(CmsSignedData signedData, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts)
		{
			if (x509AttrCerts != null)
			{
				throw Platform.CreateNotImplementedException("Currently can't replace attribute certificates");
			}
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			Asn1Set certificates = null;
			try
			{
				Asn1Set asn1Set = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCertificatesFromStore(x509Certs));
				if (asn1Set.Count != 0)
				{
					certificates = asn1Set;
				}
			}
			catch (X509StoreException e)
			{
				throw new CmsException("error getting certificates from store", e);
			}
			Asn1Set crls = null;
			try
			{
				Asn1Set asn1Set2 = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCrlsFromStore(x509Crls));
				if (asn1Set2.Count != 0)
				{
					crls = asn1Set2;
				}
			}
			catch (X509StoreException e2)
			{
				throw new CmsException("error getting CRLs from store", e2);
			}
			SignedData signedData2 = signedData.signedData;
			cmsSignedData.signedData = new SignedData(signedData2.DigestAlgorithms, signedData2.EncapContentInfo, certificates, crls, signedData2.SignerInfos);
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x040024D1 RID: 9425
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x040024D2 RID: 9426
		private readonly CmsProcessable signedContent;

		// Token: 0x040024D3 RID: 9427
		private SignedData signedData;

		// Token: 0x040024D4 RID: 9428
		private ContentInfo contentInfo;

		// Token: 0x040024D5 RID: 9429
		private SignerInformationStore signerInfoStore;

		// Token: 0x040024D6 RID: 9430
		private IX509Store attrCertStore;

		// Token: 0x040024D7 RID: 9431
		private IX509Store certificateStore;

		// Token: 0x040024D8 RID: 9432
		private IX509Store crlStore;

		// Token: 0x040024D9 RID: 9433
		private IDictionary hashes;
	}
}
