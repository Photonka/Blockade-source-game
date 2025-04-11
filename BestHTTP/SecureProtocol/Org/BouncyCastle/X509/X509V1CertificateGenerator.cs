using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000236 RID: 566
	public class X509V1CertificateGenerator
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x000AF4A6 File Offset: 0x000AD6A6
		public X509V1CertificateGenerator()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x000AF4B9 File Offset: 0x000AD6B9
		public void Reset()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000AF4C6 File Offset: 0x000AD6C6
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x000AF4F2 File Offset: 0x000AD6F2
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x000AF500 File Offset: 0x000AD700
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000AF513 File Offset: 0x000AD713
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000AF526 File Offset: 0x000AD726
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000AF534 File Offset: 0x000AD734
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			try
			{
				this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
			}
			catch (Exception ex)
			{
				throw new ArgumentException("unable to process key - " + ex.ToString());
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000AF57C File Offset: 0x000AD77C
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
				throw new ArgumentException("Unknown signature type requested", "signatureAlgorithm");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000AF5E4 File Offset: 0x000AD7E4
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x000AF5EE File Offset: 0x000AD7EE
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000AF604 File Offset: 0x000AD804
		public X509Certificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			TbsCertificateStructure tbsCertificateStructure = this.tbsGen.GenerateTbsCertificate();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateStructure.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateStructure, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000AF67E File Offset: 0x000AD87E
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlg, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, sigAlg, new DerBitString(signature)));
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x000AF692 File Offset: 0x000AD892
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x0400150B RID: 5387
		private V1TbsCertificateGenerator tbsGen;

		// Token: 0x0400150C RID: 5388
		private DerObjectIdentifier sigOID;

		// Token: 0x0400150D RID: 5389
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x0400150E RID: 5390
		private string signatureAlgorithm;
	}
}
