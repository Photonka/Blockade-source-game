using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000293 RID: 659
	public class TimeStampTokenGenerator
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x000BC4F1 File Offset: 0x000BA6F1
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID) : this(key, cert, digestOID, tsaPolicyOID, null, null)
		{
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000BC500 File Offset: 0x000BA700
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.key = key;
			this.cert = cert;
			this.digestOID = digestOID;
			this.tsaPolicyOID = tsaPolicyOID;
			this.unsignedAttr = unsignedAttr;
			TspUtil.ValidateCertificate(cert);
			IDictionary dictionary;
			if (signedAttr != null)
			{
				dictionary = signedAttr.ToDictionary();
			}
			else
			{
				dictionary = Platform.CreateHashtable();
			}
			try
			{
				EssCertID essCertID = new EssCertID(DigestUtilities.CalculateDigest("SHA-1", cert.GetEncoded()));
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(PkcsObjectIdentifiers.IdAASigningCertificate, new DerSet(new SigningCertificate(essCertID)));
				dictionary[attribute.AttrType] = attribute;
			}
			catch (CertificateEncodingException e)
			{
				throw new TspException("Exception processing certificate.", e);
			}
			catch (SecurityUtilityException e2)
			{
				throw new TspException("Can't find a SHA-1 implementation.", e2);
			}
			this.signedAttr = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000BC5E8 File Offset: 0x000BA7E8
		public void SetCertificates(IX509Store certificates)
		{
			this.x509Certs = certificates;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000BC5F1 File Offset: 0x000BA7F1
		public void SetCrls(IX509Store crls)
		{
			this.x509Crls = crls;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000BC5FA File Offset: 0x000BA7FA
		public void SetAccuracySeconds(int accuracySeconds)
		{
			this.accuracySeconds = accuracySeconds;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000BC603 File Offset: 0x000BA803
		public void SetAccuracyMillis(int accuracyMillis)
		{
			this.accuracyMillis = accuracyMillis;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000BC60C File Offset: 0x000BA80C
		public void SetAccuracyMicros(int accuracyMicros)
		{
			this.accuracyMicros = accuracyMicros;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000BC615 File Offset: 0x000BA815
		public void SetOrdering(bool ordering)
		{
			this.ordering = ordering;
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000BC61E File Offset: 0x000BA81E
		public void SetTsa(GeneralName tsa)
		{
			this.tsa = tsa;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000BC628 File Offset: 0x000BA828
		public TimeStampToken Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			MessageImprint messageImprint = new MessageImprint(new AlgorithmIdentifier(new DerObjectIdentifier(request.MessageImprintAlgOid), DerNull.Instance), request.GetMessageImprintDigest());
			Accuracy accuracy = null;
			if (this.accuracySeconds > 0 || this.accuracyMillis > 0 || this.accuracyMicros > 0)
			{
				DerInteger seconds = null;
				if (this.accuracySeconds > 0)
				{
					seconds = new DerInteger(this.accuracySeconds);
				}
				DerInteger millis = null;
				if (this.accuracyMillis > 0)
				{
					millis = new DerInteger(this.accuracyMillis);
				}
				DerInteger micros = null;
				if (this.accuracyMicros > 0)
				{
					micros = new DerInteger(this.accuracyMicros);
				}
				accuracy = new Accuracy(seconds, millis, micros);
			}
			DerBoolean derBoolean = null;
			if (this.ordering)
			{
				derBoolean = DerBoolean.GetInstance(this.ordering);
			}
			DerInteger nonce = null;
			if (request.Nonce != null)
			{
				nonce = new DerInteger(request.Nonce);
			}
			DerObjectIdentifier tsaPolicyId = new DerObjectIdentifier(this.tsaPolicyOID);
			if (request.ReqPolicy != null)
			{
				tsaPolicyId = new DerObjectIdentifier(request.ReqPolicy);
			}
			TstInfo tstInfo = new TstInfo(tsaPolicyId, messageImprint, new DerInteger(serialNumber), new DerGeneralizedTime(genTime), accuracy, derBoolean, nonce, this.tsa, request.Extensions);
			TimeStampToken result;
			try
			{
				CmsSignedDataGenerator cmsSignedDataGenerator = new CmsSignedDataGenerator();
				byte[] derEncoded = tstInfo.GetDerEncoded();
				if (request.CertReq)
				{
					cmsSignedDataGenerator.AddCertificates(this.x509Certs);
				}
				cmsSignedDataGenerator.AddCrls(this.x509Crls);
				cmsSignedDataGenerator.AddSigner(this.key, this.cert, this.digestOID, this.signedAttr, this.unsignedAttr);
				result = new TimeStampToken(cmsSignedDataGenerator.Generate(PkcsObjectIdentifiers.IdCTTstInfo.Id, new CmsProcessableByteArray(derEncoded), true));
			}
			catch (CmsException e)
			{
				throw new TspException("Error generating time-stamp token", e);
			}
			catch (IOException e2)
			{
				throw new TspException("Exception encoding info", e2);
			}
			catch (X509StoreException e3)
			{
				throw new TspException("Exception handling CertStore", e3);
			}
			return result;
		}

		// Token: 0x0400170D RID: 5901
		private int accuracySeconds = -1;

		// Token: 0x0400170E RID: 5902
		private int accuracyMillis = -1;

		// Token: 0x0400170F RID: 5903
		private int accuracyMicros = -1;

		// Token: 0x04001710 RID: 5904
		private bool ordering;

		// Token: 0x04001711 RID: 5905
		private GeneralName tsa;

		// Token: 0x04001712 RID: 5906
		private string tsaPolicyOID;

		// Token: 0x04001713 RID: 5907
		private AsymmetricKeyParameter key;

		// Token: 0x04001714 RID: 5908
		private X509Certificate cert;

		// Token: 0x04001715 RID: 5909
		private string digestOID;

		// Token: 0x04001716 RID: 5910
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr;

		// Token: 0x04001717 RID: 5911
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr;

		// Token: 0x04001718 RID: 5912
		private IX509Store x509Certs;

		// Token: 0x04001719 RID: 5913
		private IX509Store x509Crls;
	}
}
