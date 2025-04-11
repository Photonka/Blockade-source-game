using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000607 RID: 1543
	public class SignerInfoGenerator
	{
		// Token: 0x06003A9E RID: 15006 RVA: 0x0016E9BC File Offset: 0x0016CBBC
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory signerFactory) : this(sigId, signerFactory, false)
		{
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0016E9C8 File Offset: 0x0016CBC8
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory signerFactory, bool isDirectSignature)
		{
			this.sigId = sigId;
			this.contentSigner = signerFactory;
			this.isDirectSignature = isDirectSignature;
			if (this.isDirectSignature)
			{
				this.signedGen = null;
				this.unsignedGen = null;
				return;
			}
			this.signedGen = new DefaultSignedAttributeTableGenerator();
			this.unsignedGen = null;
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x0016EA19 File Offset: 0x0016CC19
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory contentSigner, CmsAttributeTableGenerator signedGen, CmsAttributeTableGenerator unsignedGen)
		{
			this.sigId = sigId;
			this.contentSigner = contentSigner;
			this.signedGen = signedGen;
			this.unsignedGen = unsignedGen;
			this.isDirectSignature = false;
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0016EA45 File Offset: 0x0016CC45
		internal void setAssociatedCertificate(X509Certificate certificate)
		{
			this.certificate = certificate;
		}

		// Token: 0x0400253E RID: 9534
		internal X509Certificate certificate;

		// Token: 0x0400253F RID: 9535
		internal ISignatureFactory contentSigner;

		// Token: 0x04002540 RID: 9536
		internal SignerIdentifier sigId;

		// Token: 0x04002541 RID: 9537
		internal CmsAttributeTableGenerator signedGen;

		// Token: 0x04002542 RID: 9538
		internal CmsAttributeTableGenerator unsignedGen;

		// Token: 0x04002543 RID: 9539
		private bool isDirectSignature;
	}
}
