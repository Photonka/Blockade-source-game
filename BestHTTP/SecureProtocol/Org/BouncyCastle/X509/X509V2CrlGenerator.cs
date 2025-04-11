using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000239 RID: 569
	public class X509V2CrlGenerator
	{
		// Token: 0x06001504 RID: 5380 RVA: 0x000AFD74 File Offset: 0x000ADF74
		public X509V2CrlGenerator()
		{
			this.tbsGen = new V2TbsCertListGenerator();
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000AFD92 File Offset: 0x000ADF92
		public void Reset()
		{
			this.tbsGen = new V2TbsCertListGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000AFDAA File Offset: 0x000ADFAA
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000AFDB8 File Offset: 0x000ADFB8
		public void SetThisUpdate(DateTime date)
		{
			this.tbsGen.SetThisUpdate(new Time(date));
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000AFDCB File Offset: 0x000ADFCB
		public void SetNextUpdate(DateTime date)
		{
			this.tbsGen.SetNextUpdate(new Time(date));
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000AFDDE File Offset: 0x000ADFDE
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000AFDF8 File Offset: 0x000ADFF8
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason, DateTime invalidityDate)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason, new DerGeneralizedTime(invalidityDate));
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000AFE19 File Offset: 0x000AE019
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, X509Extensions extensions)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), extensions);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000AFE34 File Offset: 0x000AE034
		public void AddCrl(X509Crl other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ISet revokedCertificates = other.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry x509CrlEntry = (X509CrlEntry)obj;
					try
					{
						this.tbsGen.AddCrlEntry(Asn1Sequence.GetInstance(Asn1Object.FromByteArray(x509CrlEntry.GetEncoded())));
					}
					catch (IOException e)
					{
						throw new CrlException("exception processing encoding of CRL", e);
					}
				}
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000AFED4 File Offset: 0x000AE0D4
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Unknown signature type requested", innerException);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000AFF38 File Offset: 0x000AE138
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000AFF4D File Offset: 0x000AE14D
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000AFF5D File Offset: 0x000AE15D
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000AFF77 File Offset: 0x000AE177
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000AFF8C File Offset: 0x000AE18C
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Crl Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000AFF96 File Offset: 0x000AE196
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Crl Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000AFFAC File Offset: 0x000AE1AC
		public X509Crl Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			TbsCertificateList tbsCertificateList = this.GenerateCertList();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateList.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateList, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000B0021 File Offset: 0x000AE221
		private TbsCertificateList GenerateCertList()
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			return this.tbsGen.GenerateTbsCertList();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000B0051 File Offset: 0x000AE251
		private X509Crl GenerateJcaObject(TbsCertificateList tbsCrl, AlgorithmIdentifier algId, byte[] signature)
		{
			return new X509Crl(CertificateList.GetInstance(new DerSequence(new Asn1Encodable[]
			{
				tbsCrl,
				algId,
				new DerBitString(signature)
			})));
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000AF692 File Offset: 0x000AD892
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04001517 RID: 5399
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x04001518 RID: 5400
		private V2TbsCertListGenerator tbsGen;

		// Token: 0x04001519 RID: 5401
		private DerObjectIdentifier sigOID;

		// Token: 0x0400151A RID: 5402
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x0400151B RID: 5403
		private string signatureAlgorithm;
	}
}
