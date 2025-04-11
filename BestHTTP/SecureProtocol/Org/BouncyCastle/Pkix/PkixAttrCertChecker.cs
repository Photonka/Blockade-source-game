using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x0200029A RID: 666
	public abstract class PkixAttrCertChecker
	{
		// Token: 0x06001897 RID: 6295
		public abstract ISet GetSupportedExtensions();

		// Token: 0x06001898 RID: 6296
		public abstract void Check(IX509AttributeCertificate attrCert, PkixCertPath certPath, PkixCertPath holderCertPath, ICollection unresolvedCritExts);

		// Token: 0x06001899 RID: 6297
		public abstract PkixAttrCertChecker Clone();
	}
}
