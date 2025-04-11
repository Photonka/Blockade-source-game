using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000227 RID: 551
	public class PrincipalUtilities
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x000AC7E8 File Offset: 0x000AA9E8
		public static X509Name GetIssuerX509Principal(X509Certificate cert)
		{
			X509Name issuer;
			try
			{
				issuer = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate())).Issuer;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract issuer", e);
			}
			return issuer;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000AC82C File Offset: 0x000AAA2C
		public static X509Name GetSubjectX509Principal(X509Certificate cert)
		{
			X509Name subject;
			try
			{
				subject = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate())).Subject;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract subject", e);
			}
			return subject;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000AC870 File Offset: 0x000AAA70
		public static X509Name GetIssuerX509Principal(X509Crl crl)
		{
			X509Name issuer;
			try
			{
				issuer = TbsCertificateList.GetInstance(Asn1Object.FromByteArray(crl.GetTbsCertList())).Issuer;
			}
			catch (Exception e)
			{
				throw new CrlException("Could not extract issuer", e);
			}
			return issuer;
		}
	}
}
