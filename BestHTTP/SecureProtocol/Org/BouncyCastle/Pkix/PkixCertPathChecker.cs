using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A2 RID: 674
	public abstract class PkixCertPathChecker
	{
		// Token: 0x060018C0 RID: 6336
		public abstract void Init(bool forward);

		// Token: 0x060018C1 RID: 6337
		public abstract bool IsForwardCheckingSupported();

		// Token: 0x060018C2 RID: 6338
		public abstract ISet GetSupportedExtensions();

		// Token: 0x060018C3 RID: 6339
		public abstract void Check(X509Certificate cert, ISet unresolvedCritExts);

		// Token: 0x060018C4 RID: 6340 RVA: 0x000BE197 File Offset: 0x000BC397
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
