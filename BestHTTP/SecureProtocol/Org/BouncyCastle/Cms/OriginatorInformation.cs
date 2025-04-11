using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005FC RID: 1532
	public class OriginatorInformation
	{
		// Token: 0x06003A6D RID: 14957 RVA: 0x0016E185 File Offset: 0x0016C385
		internal OriginatorInformation(OriginatorInfo originatorInfo)
		{
			this.originatorInfo = originatorInfo;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x0016E194 File Offset: 0x0016C394
		public virtual IX509Store GetCertificates()
		{
			Asn1Set certificates = this.originatorInfo.Certificates;
			if (certificates != null)
			{
				IList list = Platform.CreateArrayList(certificates.Count);
				foreach (object obj in certificates)
				{
					Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
					if (asn1Object is Asn1Sequence)
					{
						list.Add(new X509Certificate(X509CertificateStructure.GetInstance(asn1Object)));
					}
				}
				return X509StoreFactory.Create("Certificate/Collection", new X509CollectionStoreParameters(list));
			}
			return X509StoreFactory.Create("Certificate/Collection", new X509CollectionStoreParameters(Platform.CreateArrayList()));
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0016E244 File Offset: 0x0016C444
		public virtual IX509Store GetCrls()
		{
			Asn1Set certificates = this.originatorInfo.Certificates;
			if (certificates != null)
			{
				IList list = Platform.CreateArrayList(certificates.Count);
				foreach (object obj in certificates)
				{
					Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
					if (asn1Object is Asn1Sequence)
					{
						list.Add(new X509Crl(CertificateList.GetInstance(asn1Object)));
					}
				}
				return X509StoreFactory.Create("CRL/Collection", new X509CollectionStoreParameters(list));
			}
			return X509StoreFactory.Create("CRL/Collection", new X509CollectionStoreParameters(Platform.CreateArrayList()));
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x0016E2F4 File Offset: 0x0016C4F4
		public virtual OriginatorInfo ToAsn1Structure()
		{
			return this.originatorInfo;
		}

		// Token: 0x04002531 RID: 9521
		private readonly OriginatorInfo originatorInfo;
	}
}
