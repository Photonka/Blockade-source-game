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
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200023A RID: 570
	public class X509V3CertificateGenerator
	{
		// Token: 0x06001518 RID: 5400 RVA: 0x000B0079 File Offset: 0x000AE279
		public X509V3CertificateGenerator()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000B0097 File Offset: 0x000AE297
		public void Reset()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000B00AF File Offset: 0x000AE2AF
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000B00DB File Offset: 0x000AE2DB
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000B00E9 File Offset: 0x000AE2E9
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000B00FC File Offset: 0x000AE2FC
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000B010F File Offset: 0x000AE30F
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x000B011D File Offset: 0x000AE31D
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000B0130 File Offset: 0x000AE330
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOid = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested: " + signatureAlgorithm);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOid, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000B0198 File Offset: 0x000AE398
		public void SetSubjectUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetSubjectUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000B01AC File Offset: 0x000AE3AC
		public void SetIssuerUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetIssuerUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000B01C0 File Offset: 0x000AE3C0
		private DerBitString booleanToBitString(bool[] id)
		{
			byte[] array = new byte[(id.Length + 7) / 8];
			for (int num = 0; num != id.Length; num++)
			{
				if (id[num])
				{
					byte[] array2 = array;
					int num2 = num / 8;
					array2[num2] |= (byte)(1 << 7 - num % 8);
				}
			}
			int num3 = id.Length % 8;
			if (num3 == 0)
			{
				return new DerBitString(array);
			}
			return new DerBitString(array, 8 - num3);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000B021F File Offset: 0x000AE41F
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x000B0234 File Offset: 0x000AE434
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000B0244 File Offset: 0x000AE444
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000B025E File Offset: 0x000AE45E
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000B0273 File Offset: 0x000AE473
		public void CopyAndAddExtension(string oid, bool critical, X509Certificate cert)
		{
			this.CopyAndAddExtension(new DerObjectIdentifier(oid), critical, cert);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x000B0284 File Offset: 0x000AE484
		public void CopyAndAddExtension(DerObjectIdentifier oid, bool critical, X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(oid);
			if (extensionValue == null)
			{
				throw new CertificateParsingException("extension " + oid + " not present");
			}
			try
			{
				Asn1Encodable extensionValue2 = X509ExtensionUtilities.FromExtensionValue(extensionValue);
				this.AddExtension(oid, critical, extensionValue2);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message, ex);
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000B02E4 File Offset: 0x000AE4E4
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000B02EE File Offset: 0x000AE4EE
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000B0304 File Offset: 0x000AE504
		public X509Certificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			TbsCertificateStructure tbsCertificateStructure = this.tbsGen.GenerateTbsCertificate();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateStructure.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateStructure, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000AF67E File Offset: 0x000AD87E
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlg, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, sigAlg, new DerBitString(signature)));
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000AF692 File Offset: 0x000AD892
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x0400151C RID: 5404
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x0400151D RID: 5405
		private V3TbsCertificateGenerator tbsGen;

		// Token: 0x0400151E RID: 5406
		private DerObjectIdentifier sigOid;

		// Token: 0x0400151F RID: 5407
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x04001520 RID: 5408
		private string signatureAlgorithm;
	}
}
