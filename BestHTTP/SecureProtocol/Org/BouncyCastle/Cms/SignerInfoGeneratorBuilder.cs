using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000608 RID: 1544
	public class SignerInfoGeneratorBuilder
	{
		// Token: 0x06003AA3 RID: 15011 RVA: 0x0016EA4E File Offset: 0x0016CC4E
		public SignerInfoGeneratorBuilder SetDirectSignature(bool hasNoSignedAttributes)
		{
			this.directSignature = hasNoSignedAttributes;
			return this;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x0016EA58 File Offset: 0x0016CC58
		public SignerInfoGeneratorBuilder WithSignedAttributeGenerator(CmsAttributeTableGenerator signedGen)
		{
			this.signedGen = signedGen;
			return this;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0016EA62 File Offset: 0x0016CC62
		public SignerInfoGeneratorBuilder WithUnsignedAttributeGenerator(CmsAttributeTableGenerator unsignedGen)
		{
			this.unsignedGen = unsignedGen;
			return this;
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x0016EA6C File Offset: 0x0016CC6C
		public SignerInfoGenerator Build(ISignatureFactory contentSigner, X509Certificate certificate)
		{
			SignerIdentifier sigId = new SignerIdentifier(new IssuerAndSerialNumber(certificate.IssuerDN, new DerInteger(certificate.SerialNumber)));
			SignerInfoGenerator signerInfoGenerator = this.CreateGenerator(contentSigner, sigId);
			signerInfoGenerator.setAssociatedCertificate(certificate);
			return signerInfoGenerator;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0016EAA4 File Offset: 0x0016CCA4
		public SignerInfoGenerator Build(ISignatureFactory signerFactory, byte[] subjectKeyIdentifier)
		{
			SignerIdentifier sigId = new SignerIdentifier(new DerOctetString(subjectKeyIdentifier));
			return this.CreateGenerator(signerFactory, sigId);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x0016EAC8 File Offset: 0x0016CCC8
		private SignerInfoGenerator CreateGenerator(ISignatureFactory contentSigner, SignerIdentifier sigId)
		{
			if (this.directSignature)
			{
				return new SignerInfoGenerator(sigId, contentSigner, true);
			}
			if (this.signedGen != null || this.unsignedGen != null)
			{
				if (this.signedGen == null)
				{
					this.signedGen = new DefaultSignedAttributeTableGenerator();
				}
				return new SignerInfoGenerator(sigId, contentSigner, this.signedGen, this.unsignedGen);
			}
			return new SignerInfoGenerator(sigId, contentSigner);
		}

		// Token: 0x04002544 RID: 9540
		private bool directSignature;

		// Token: 0x04002545 RID: 9541
		private CmsAttributeTableGenerator signedGen;

		// Token: 0x04002546 RID: 9542
		private CmsAttributeTableGenerator unsignedGen;
	}
}
