using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005EF RID: 1519
	internal class CmsUtilities
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x0016CE08 File Offset: 0x0016B008
		internal static int MaximumMemory
		{
			get
			{
				long num = 2147483647L;
				if (num > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)num;
			}
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x0016CE2D File Offset: 0x0016B02D
		internal static ContentInfo ReadContentInfo(byte[] input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input));
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x0016CE3A File Offset: 0x0016B03A
		internal static ContentInfo ReadContentInfo(Stream input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input, CmsUtilities.MaximumMemory));
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x0016CE4C File Offset: 0x0016B04C
		private static ContentInfo ReadContentInfo(Asn1InputStream aIn)
		{
			ContentInfo instance;
			try
			{
				instance = ContentInfo.GetInstance(aIn.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Malformed content.", e2);
			}
			catch (ArgumentException e3)
			{
				throw new CmsException("Malformed content.", e3);
			}
			return instance;
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x0016CEB8 File Offset: 0x0016B0B8
		public static byte[] StreamToByteArray(Stream inStream)
		{
			return Streams.ReadAll(inStream);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x0016CEC0 File Offset: 0x0016B0C0
		public static byte[] StreamToByteArray(Stream inStream, int limit)
		{
			return Streams.ReadAllLimited(inStream, limit);
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x0016CECC File Offset: 0x0016B0CC
		public static IList GetCertificatesFromStore(IX509Store certStore)
		{
			IList result;
			try
			{
				IList list = Platform.CreateArrayList();
				if (certStore != null)
				{
					foreach (object obj in certStore.GetMatches(null))
					{
						X509Certificate x509Certificate = (X509Certificate)obj;
						list.Add(X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CertificateEncodingException e)
			{
				throw new CmsException("error encoding certs", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing certs", e2);
			}
			return result;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0016CF7C File Offset: 0x0016B17C
		public static IList GetCrlsFromStore(IX509Store crlStore)
		{
			IList result;
			try
			{
				IList list = Platform.CreateArrayList();
				if (crlStore != null)
				{
					foreach (object obj in crlStore.GetMatches(null))
					{
						X509Crl x509Crl = (X509Crl)obj;
						list.Add(CertificateList.GetInstance(Asn1Object.FromByteArray(x509Crl.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CrlException e)
			{
				throw new CmsException("error encoding crls", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing crls", e2);
			}
			return result;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x0016D02C File Offset: 0x0016B22C
		public static Asn1Set CreateBerSetFromList(IList berObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in berObjects)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return new BerSet(asn1EncodableVector);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0016D09C File Offset: 0x0016B29C
		public static Asn1Set CreateDerSetFromList(IList derObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in derObjects)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return new DerSet(asn1EncodableVector);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0016D10C File Offset: 0x0016B30C
		internal static Stream CreateBerOctetOutputStream(Stream s, int tagNo, bool isExplicit, int bufferSize)
		{
			return new BerOctetStringGenerator(s, tagNo, isExplicit).GetOctetOutputStream(bufferSize);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0016D11C File Offset: 0x0016B31C
		internal static TbsCertificateStructure GetTbsCertificateStructure(X509Certificate cert)
		{
			return TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x0016D130 File Offset: 0x0016B330
		internal static IssuerAndSerialNumber GetIssuerAndSerialNumber(X509Certificate cert)
		{
			TbsCertificateStructure tbsCertificateStructure = CmsUtilities.GetTbsCertificateStructure(cert);
			return new IssuerAndSerialNumber(tbsCertificateStructure.Issuer, tbsCertificateStructure.SerialNumber.Value);
		}
	}
}
