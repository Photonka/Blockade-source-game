using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000247 RID: 583
	public class AuthorityKeyIdentifierStructure : AuthorityKeyIdentifier
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x000B1450 File Offset: 0x000AF650
		public AuthorityKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1Sequence)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000B1464 File Offset: 0x000AF664
		private static Asn1Sequence FromCertificate(X509Certificate certificate)
		{
			Asn1Sequence result;
			try
			{
				GeneralName name = new GeneralName(PrincipalUtilities.GetIssuerX509Principal(certificate));
				if (certificate.Version == 3)
				{
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.SubjectKeyIdentifier);
					if (extensionValue != null)
					{
						return (Asn1Sequence)new AuthorityKeyIdentifier(((Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(extensionValue)).GetOctets(), new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
					}
				}
				result = (Asn1Sequence)new AuthorityKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(certificate.GetPublicKey()), new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("Exception extracting certificate details", exception);
			}
			return result;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000B150C File Offset: 0x000AF70C
		private static Asn1Sequence FromKey(AsymmetricKeyParameter pubKey)
		{
			Asn1Sequence result;
			try
			{
				result = (Asn1Sequence)new AuthorityKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey)).ToAsn1Object();
			}
			catch (Exception arg)
			{
				throw new InvalidKeyException("can't process key: " + arg);
			}
			return result;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000B1554 File Offset: 0x000AF754
		public AuthorityKeyIdentifierStructure(X509Certificate certificate) : base(AuthorityKeyIdentifierStructure.FromCertificate(certificate))
		{
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000B1562 File Offset: 0x000AF762
		public AuthorityKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(AuthorityKeyIdentifierStructure.FromKey(pubKey))
		{
		}
	}
}
