using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000238 RID: 568
	public class X509V2AttributeCertificateGenerator
	{
		// Token: 0x060014F4 RID: 5364 RVA: 0x000AFB3D File Offset: 0x000ADD3D
		public X509V2AttributeCertificateGenerator()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000AFB5B File Offset: 0x000ADD5B
		public void Reset()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000AFB73 File Offset: 0x000ADD73
		public void SetHolder(AttributeCertificateHolder holder)
		{
			this.acInfoGen.SetHolder(holder.holder);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000AFB86 File Offset: 0x000ADD86
		public void SetIssuer(AttributeCertificateIssuer issuer)
		{
			this.acInfoGen.SetIssuer(AttCertIssuer.GetInstance(issuer.form));
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000AFB9E File Offset: 0x000ADD9E
		public void SetSerialNumber(BigInteger serialNumber)
		{
			this.acInfoGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000AFBB1 File Offset: 0x000ADDB1
		public void SetNotBefore(DateTime date)
		{
			this.acInfoGen.SetStartDate(new DerGeneralizedTime(date));
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000AFBC4 File Offset: 0x000ADDC4
		public void SetNotAfter(DateTime date)
		{
			this.acInfoGen.SetEndDate(new DerGeneralizedTime(date));
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000AFBD8 File Offset: 0x000ADDD8
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.acInfoGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000AFC3C File Offset: 0x000ADE3C
		public void AddAttribute(X509Attribute attribute)
		{
			this.acInfoGen.AddAttribute(AttributeX509.GetInstance(attribute.ToAsn1Object()));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000AFC54 File Offset: 0x000ADE54
		public void SetIssuerUniqueId(bool[] iui)
		{
			throw Platform.CreateNotImplementedException("SetIssuerUniqueId()");
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000AFC60 File Offset: 0x000ADE60
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000AFC75 File Offset: 0x000ADE75
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000AFC8A File Offset: 0x000ADE8A
		[Obsolete("Use Generate with an ISignatureFactory")]
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x000AFC94 File Offset: 0x000ADE94
		[Obsolete("Use Generate with an ISignatureFactory")]
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000AFCAC File Offset: 0x000ADEAC
		public IX509AttributeCertificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.acInfoGen.SetExtensions(this.extGenerator.Generate());
			}
			AlgorithmIdentifier signature = (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails;
			this.acInfoGen.SetSignature(signature);
			AttributeCertificateInfo attributeCertificateInfo = this.acInfoGen.GenerateAttributeCertificateInfo();
			byte[] derEncoded = attributeCertificateInfo.GetDerEncoded();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			IX509AttributeCertificate result;
			try
			{
				DerBitString signatureValue = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
				result = new X509V2AttributeCertificate(new AttributeCertificate(attributeCertificateInfo, signature, signatureValue));
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("constructed invalid certificate", e);
			}
			return result;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x000AF692 File Offset: 0x000AD892
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04001512 RID: 5394
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x04001513 RID: 5395
		private V2AttributeCertificateInfoGenerator acInfoGen;

		// Token: 0x04001514 RID: 5396
		private DerObjectIdentifier sigOID;

		// Token: 0x04001515 RID: 5397
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x04001516 RID: 5398
		private string signatureAlgorithm;
	}
}
