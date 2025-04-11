using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005FB RID: 1531
	public class OriginatorInfoGenerator
	{
		// Token: 0x06003A69 RID: 14953 RVA: 0x0016E0F0 File Offset: 0x0016C2F0
		public OriginatorInfoGenerator(X509Certificate origCert)
		{
			this.origCerts = Platform.CreateArrayList(1);
			this.origCrls = null;
			this.origCerts.Add(origCert.CertificateStructure);
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x0016E11D File Offset: 0x0016C31D
		public OriginatorInfoGenerator(IX509Store origCerts) : this(origCerts, null)
		{
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x0016E127 File Offset: 0x0016C327
		public OriginatorInfoGenerator(IX509Store origCerts, IX509Store origCrls)
		{
			this.origCerts = CmsUtilities.GetCertificatesFromStore(origCerts);
			this.origCrls = ((origCrls == null) ? null : CmsUtilities.GetCrlsFromStore(origCrls));
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0016E150 File Offset: 0x0016C350
		public virtual OriginatorInfo Generate()
		{
			Asn1Set certs = CmsUtilities.CreateDerSetFromList(this.origCerts);
			Asn1Set crls = (this.origCrls == null) ? null : CmsUtilities.CreateDerSetFromList(this.origCrls);
			return new OriginatorInfo(certs, crls);
		}

		// Token: 0x0400252F RID: 9519
		private readonly IList origCerts;

		// Token: 0x04002530 RID: 9520
		private readonly IList origCrls;
	}
}
