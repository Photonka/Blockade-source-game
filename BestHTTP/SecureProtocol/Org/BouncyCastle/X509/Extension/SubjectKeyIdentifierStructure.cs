using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000248 RID: 584
	public class SubjectKeyIdentifierStructure : SubjectKeyIdentifier
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x000B1570 File Offset: 0x000AF770
		public SubjectKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000B1584 File Offset: 0x000AF784
		private static Asn1OctetString FromPublicKey(AsymmetricKeyParameter pubKey)
		{
			Asn1OctetString result;
			try
			{
				result = (Asn1OctetString)new SubjectKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey)).ToAsn1Object();
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException("Exception extracting certificate details: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000B15D4 File Offset: 0x000AF7D4
		public SubjectKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(SubjectKeyIdentifierStructure.FromPublicKey(pubKey))
		{
		}
	}
}
