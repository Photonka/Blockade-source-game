using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000249 RID: 585
	public class X509ExtensionUtilities
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x000B15E2 File Offset: 0x000AF7E2
		public static Asn1Object FromExtensionValue(Asn1OctetString extensionValue)
		{
			return Asn1Object.FromByteArray(extensionValue.GetOctets());
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000B15EF File Offset: 0x000AF7EF
		public static ICollection GetIssuerAlternativeNames(X509Certificate cert)
		{
			return X509ExtensionUtilities.GetAlternativeName(cert.GetExtensionValue(X509Extensions.IssuerAlternativeName));
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000B1601 File Offset: 0x000AF801
		public static ICollection GetSubjectAlternativeNames(X509Certificate cert)
		{
			return X509ExtensionUtilities.GetAlternativeName(cert.GetExtensionValue(X509Extensions.SubjectAlternativeName));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000B1614 File Offset: 0x000AF814
		private static ICollection GetAlternativeName(Asn1OctetString extVal)
		{
			IList list = Platform.CreateArrayList();
			if (extVal != null)
			{
				try
				{
					foreach (object obj in Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extVal)))
					{
						object obj2 = (Asn1Encodable)obj;
						IList list2 = Platform.CreateArrayList();
						GeneralName instance = GeneralName.GetInstance(obj2);
						list2.Add(instance.TagNo);
						switch (instance.TagNo)
						{
						case 0:
						case 3:
						case 5:
							list2.Add(instance.Name.ToAsn1Object());
							break;
						case 1:
						case 2:
						case 6:
							list2.Add(((IAsn1String)instance.Name).GetString());
							break;
						case 4:
							list2.Add(X509Name.GetInstance(instance.Name).ToString());
							break;
						case 7:
							list2.Add(Asn1OctetString.GetInstance(instance.Name).GetOctets());
							break;
						case 8:
							list2.Add(DerObjectIdentifier.GetInstance(instance.Name).Id);
							break;
						default:
							throw new IOException("Bad tag number: " + instance.TagNo);
						}
						list.Add(list2);
					}
				}
				catch (Exception ex)
				{
					throw new CertificateParsingException(ex.Message);
				}
			}
			return list;
		}
	}
}
