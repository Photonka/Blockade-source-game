using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E8 RID: 1512
	public class CmsSignedDataParser : CmsContentInfoParser
	{
		// Token: 0x060039C9 RID: 14793 RVA: 0x0016AD9D File Offset: 0x00168F9D
		public CmsSignedDataParser(byte[] sigBlock) : this(new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x0016ADAC File Offset: 0x00168FAC
		public CmsSignedDataParser(CmsTypedStream signedContent, byte[] sigBlock) : this(signedContent, new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0016ADBC File Offset: 0x00168FBC
		public CmsSignedDataParser(Stream sigData) : this(null, sigData)
		{
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0016ADC8 File Offset: 0x00168FC8
		public CmsSignedDataParser(CmsTypedStream signedContent, Stream sigData) : base(sigData)
		{
			try
			{
				this._signedContent = signedContent;
				this._signedData = SignedDataParser.GetInstance(this.contentInfo.GetContent(16));
				this._digests = Platform.CreateHashtable();
				this._digestOids = new HashSet();
				Asn1SetParser digestAlgorithms = this._signedData.GetDigestAlgorithms();
				IAsn1Convertible asn1Convertible;
				while ((asn1Convertible = digestAlgorithms.ReadObject()) != null)
				{
					AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance(asn1Convertible.ToAsn1Object());
					try
					{
						string id = instance.Algorithm.Id;
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(id);
						if (!this._digests.Contains(digestAlgName))
						{
							this._digests[digestAlgName] = CmsSignedDataParser.Helper.GetDigestInstance(digestAlgName);
							this._digestOids.Add(id);
						}
					}
					catch (SecurityUtilityException)
					{
					}
				}
				ContentInfoParser encapContentInfo = this._signedData.GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				if (asn1OctetStringParser != null)
				{
					CmsTypedStream cmsTypedStream = new CmsTypedStream(encapContentInfo.ContentType.Id, asn1OctetStringParser.GetOctetStream());
					if (this._signedContent == null)
					{
						this._signedContent = cmsTypedStream;
					}
					else
					{
						cmsTypedStream.Drain();
					}
				}
				this._signedContentType = ((this._signedContent == null) ? encapContentInfo.ContentType : new DerObjectIdentifier(this._signedContent.ContentType));
			}
			catch (IOException ex)
			{
				throw new CmsException("io exception: " + ex.Message, ex);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x0016AF50 File Offset: 0x00169150
		public int Version
		{
			get
			{
				return this._signedData.Version.Value.IntValue;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x0016AF67 File Offset: 0x00169167
		public ISet DigestOids
		{
			get
			{
				return new HashSet(this._digestOids);
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0016AF74 File Offset: 0x00169174
		public SignerInformationStore GetSignerInfos()
		{
			if (this._signerInfoStore == null)
			{
				this.PopulateCertCrlSets();
				IList list = Platform.CreateArrayList();
				IDictionary dictionary = Platform.CreateHashtable();
				foreach (object key in this._digests.Keys)
				{
					dictionary[key] = DigestUtilities.DoFinal((IDigest)this._digests[key]);
				}
				try
				{
					Asn1SetParser signerInfos = this._signedData.GetSignerInfos();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = signerInfos.ReadObject()) != null)
					{
						SignerInfo instance = SignerInfo.GetInstance(asn1Convertible.ToAsn1Object());
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(instance.DigestAlgorithm.Algorithm.Id);
						byte[] digest = (byte[])dictionary[digestAlgName];
						list.Add(new SignerInformation(instance, this._signedContentType, null, new BaseDigestCalculator(digest)));
					}
				}
				catch (IOException ex)
				{
					throw new CmsException("io exception: " + ex.Message, ex);
				}
				this._signerInfoStore = new SignerInformationStore(list);
			}
			return this._signerInfoStore;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0016B0B0 File Offset: 0x001692B0
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this._attributeStore == null)
			{
				this.PopulateCertCrlSets();
				this._attributeStore = CmsSignedDataParser.Helper.CreateAttributeStore(type, this._certSet);
			}
			return this._attributeStore;
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x0016B0DD File Offset: 0x001692DD
		public IX509Store GetCertificates(string type)
		{
			if (this._certificateStore == null)
			{
				this.PopulateCertCrlSets();
				this._certificateStore = CmsSignedDataParser.Helper.CreateCertificateStore(type, this._certSet);
			}
			return this._certificateStore;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x0016B10A File Offset: 0x0016930A
		public IX509Store GetCrls(string type)
		{
			if (this._crlStore == null)
			{
				this.PopulateCertCrlSets();
				this._crlStore = CmsSignedDataParser.Helper.CreateCrlStore(type, this._crlSet);
			}
			return this._crlStore;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x0016B138 File Offset: 0x00169338
		private void PopulateCertCrlSets()
		{
			if (this._isCertCrlParsed)
			{
				return;
			}
			this._isCertCrlParsed = true;
			try
			{
				this._certSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCertificates());
				this._crlSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCrls());
			}
			catch (IOException e)
			{
				throw new CmsException("problem parsing cert/crl sets", e);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x0016B1A0 File Offset: 0x001693A0
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this._signedContentType;
			}
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0016B1A8 File Offset: 0x001693A8
		public CmsTypedStream GetSignedContent()
		{
			if (this._signedContent == null)
			{
				return null;
			}
			Stream stream = this._signedContent.ContentStream;
			foreach (object obj in this._digests.Values)
			{
				IDigest readDigest = (IDigest)obj;
				stream = new DigestStream(stream, readDigest, null);
			}
			return new CmsTypedStream(this._signedContent.ContentType, stream);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0016B230 File Offset: 0x00169430
		public static Stream ReplaceSigners(Stream original, SignerInformationStore signerInformationStore, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddSigners(signerInformationStore);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			cmsSignedDataStreamGenerator.AddAttributeCertificates(cmsSignedDataParser.GetAttributeCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCertificates(cmsSignedDataParser.GetCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCrls(cmsSignedDataParser.GetCrls("Collection"));
			Platform.Dispose(stream);
			return outStr;
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0016B2B8 File Offset: 0x001694B8
		public static Stream ReplaceCertificatesAndCrls(Stream original, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddDigests(cmsSignedDataParser.DigestOids);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			if (x509AttrCerts != null)
			{
				cmsSignedDataStreamGenerator.AddAttributeCertificates(x509AttrCerts);
			}
			if (x509Certs != null)
			{
				cmsSignedDataStreamGenerator.AddCertificates(x509Certs);
			}
			if (x509Crls != null)
			{
				cmsSignedDataStreamGenerator.AddCrls(x509Crls);
			}
			cmsSignedDataStreamGenerator.AddSigners(cmsSignedDataParser.GetSignerInfos());
			Platform.Dispose(stream);
			return outStr;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0016B343 File Offset: 0x00169543
		private static Asn1Set GetAsn1Set(Asn1SetParser asn1SetParser)
		{
			if (asn1SetParser != null)
			{
				return Asn1Set.GetInstance(asn1SetParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x040024DC RID: 9436
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x040024DD RID: 9437
		private SignedDataParser _signedData;

		// Token: 0x040024DE RID: 9438
		private DerObjectIdentifier _signedContentType;

		// Token: 0x040024DF RID: 9439
		private CmsTypedStream _signedContent;

		// Token: 0x040024E0 RID: 9440
		private IDictionary _digests;

		// Token: 0x040024E1 RID: 9441
		private ISet _digestOids;

		// Token: 0x040024E2 RID: 9442
		private SignerInformationStore _signerInfoStore;

		// Token: 0x040024E3 RID: 9443
		private Asn1Set _certSet;

		// Token: 0x040024E4 RID: 9444
		private Asn1Set _crlSet;

		// Token: 0x040024E5 RID: 9445
		private bool _isCertCrlParsed;

		// Token: 0x040024E6 RID: 9446
		private IX509Store _attributeStore;

		// Token: 0x040024E7 RID: 9447
		private IX509Store _certificateStore;

		// Token: 0x040024E8 RID: 9448
		private IX509Store _crlStore;
	}
}
