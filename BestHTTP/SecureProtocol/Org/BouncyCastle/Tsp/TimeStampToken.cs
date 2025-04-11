using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000292 RID: 658
	public class TimeStampToken
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x000BC120 File Offset: 0x000BA320
		public TimeStampToken(BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.ContentInfo contentInfo) : this(new CmsSignedData(contentInfo))
		{
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000BC130 File Offset: 0x000BA330
		public TimeStampToken(CmsSignedData signedData)
		{
			this.tsToken = signedData;
			if (!this.tsToken.SignedContentType.Equals(PkcsObjectIdentifiers.IdCTTstInfo))
			{
				throw new TspValidationException("ContentInfo object not for a time stamp.");
			}
			ICollection signers = this.tsToken.GetSignerInfos().GetSigners();
			if (signers.Count != 1)
			{
				throw new ArgumentException("Time-stamp token signed by " + signers.Count + " signers, but it must contain just the TSA signature.");
			}
			IEnumerator enumerator = signers.GetEnumerator();
			enumerator.MoveNext();
			this.tsaSignerInfo = (SignerInformation)enumerator.Current;
			try
			{
				CmsProcessable signedContent = this.tsToken.SignedContent;
				MemoryStream memoryStream = new MemoryStream();
				signedContent.Write(memoryStream);
				this.tstInfo = new TimeStampTokenInfo(TstInfo.GetInstance(Asn1Object.FromByteArray(memoryStream.ToArray())));
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				if (attribute != null)
				{
					SigningCertificate instance = SigningCertificate.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertID.GetInstance(instance.GetCerts()[0]));
				}
				else
				{
					attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
					if (attribute == null)
					{
						throw new TspValidationException("no signing certificate attribute found, time stamp invalid.");
					}
					SigningCertificateV2 instance2 = SigningCertificateV2.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertIDv2.GetInstance(instance2.GetCerts()[0]));
				}
			}
			catch (CmsException ex)
			{
				throw new TspException(ex.Message, ex.InnerException);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000BC2B8 File Offset: 0x000BA4B8
		public TimeStampTokenInfo TimeStampInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000BC2C0 File Offset: 0x000BA4C0
		public SignerID SignerID
		{
			get
			{
				return this.tsaSignerInfo.SignerID;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000BC2CD File Offset: 0x000BA4CD
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable SignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.SignedAttributes;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x000BC2DA File Offset: 0x000BA4DA
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable UnsignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.UnsignedAttributes;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000BC2E7 File Offset: 0x000BA4E7
		public IX509Store GetCertificates(string type)
		{
			return this.tsToken.GetCertificates(type);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000BC2F5 File Offset: 0x000BA4F5
		public IX509Store GetCrls(string type)
		{
			return this.tsToken.GetCrls(type);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000BC303 File Offset: 0x000BA503
		public IX509Store GetAttributeCertificates(string type)
		{
			return this.tsToken.GetAttributeCertificates(type);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000BC314 File Offset: 0x000BA514
		public void Validate(X509Certificate cert)
		{
			try
			{
				byte[] b = DigestUtilities.CalculateDigest(this.certID.GetHashAlgorithmName(), cert.GetEncoded());
				if (!Arrays.ConstantTimeAreEqual(this.certID.GetCertHash(), b))
				{
					throw new TspValidationException("certificate hash does not match certID hash.");
				}
				if (this.certID.IssuerSerial != null)
				{
					if (!this.certID.IssuerSerial.Serial.Value.Equals(cert.SerialNumber))
					{
						throw new TspValidationException("certificate serial number does not match certID for signature.");
					}
					GeneralName[] names = this.certID.IssuerSerial.Issuer.GetNames();
					X509Name issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
					bool flag = false;
					for (int num = 0; num != names.Length; num++)
					{
						if (names[num].TagNo == 4 && X509Name.GetInstance(names[num].Name).Equivalent(issuerX509Principal))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new TspValidationException("certificate name does not match certID for signature. ");
					}
				}
				TspUtil.ValidateCertificate(cert);
				cert.CheckValidity(this.tstInfo.GenTime);
				if (!this.tsaSignerInfo.Verify(cert))
				{
					throw new TspValidationException("signature not created by certificate.");
				}
			}
			catch (CmsException ex)
			{
				if (ex.InnerException != null)
				{
					throw new TspException(ex.Message, ex.InnerException);
				}
				throw new TspException("CMS exception: " + ex, ex);
			}
			catch (CertificateEncodingException ex2)
			{
				throw new TspException("problem processing certificate: " + ex2, ex2);
			}
			catch (SecurityUtilityException ex3)
			{
				throw new TspException("cannot find algorithm: " + ex3.Message, ex3);
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x000BC4DC File Offset: 0x000BA6DC
		public CmsSignedData ToCmsSignedData()
		{
			return this.tsToken;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000BC4E4 File Offset: 0x000BA6E4
		public byte[] GetEncoded()
		{
			return this.tsToken.GetEncoded();
		}

		// Token: 0x04001709 RID: 5897
		private readonly CmsSignedData tsToken;

		// Token: 0x0400170A RID: 5898
		private readonly SignerInformation tsaSignerInfo;

		// Token: 0x0400170B RID: 5899
		private readonly TimeStampTokenInfo tstInfo;

		// Token: 0x0400170C RID: 5900
		private readonly TimeStampToken.CertID certID;

		// Token: 0x020008D8 RID: 2264
		private class CertID
		{
			// Token: 0x06004D5D RID: 19805 RVA: 0x001B0952 File Offset: 0x001AEB52
			internal CertID(EssCertID certID)
			{
				this.certID = certID;
				this.certIDv2 = null;
			}

			// Token: 0x06004D5E RID: 19806 RVA: 0x001B0968 File Offset: 0x001AEB68
			internal CertID(EssCertIDv2 certID)
			{
				this.certIDv2 = certID;
				this.certID = null;
			}

			// Token: 0x06004D5F RID: 19807 RVA: 0x001B0980 File Offset: 0x001AEB80
			public string GetHashAlgorithmName()
			{
				if (this.certID != null)
				{
					return "SHA-1";
				}
				if (NistObjectIdentifiers.IdSha256.Equals(this.certIDv2.HashAlgorithm.Algorithm))
				{
					return "SHA-256";
				}
				return this.certIDv2.HashAlgorithm.Algorithm.Id;
			}

			// Token: 0x06004D60 RID: 19808 RVA: 0x001B09D2 File Offset: 0x001AEBD2
			public AlgorithmIdentifier GetHashAlgorithm()
			{
				if (this.certID == null)
				{
					return this.certIDv2.HashAlgorithm;
				}
				return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
			}

			// Token: 0x06004D61 RID: 19809 RVA: 0x001B09F2 File Offset: 0x001AEBF2
			public byte[] GetCertHash()
			{
				if (this.certID == null)
				{
					return this.certIDv2.GetCertHash();
				}
				return this.certID.GetCertHash();
			}

			// Token: 0x17000C0C RID: 3084
			// (get) Token: 0x06004D62 RID: 19810 RVA: 0x001B0A13 File Offset: 0x001AEC13
			public IssuerSerial IssuerSerial
			{
				get
				{
					if (this.certID == null)
					{
						return this.certIDv2.IssuerSerial;
					}
					return this.certID.IssuerSerial;
				}
			}

			// Token: 0x0400337F RID: 13183
			private EssCertID certID;

			// Token: 0x04003380 RID: 13184
			private EssCertIDv2 certIDv2;
		}
	}
}
