using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x0200023B RID: 571
	public interface IX509Selector : ICloneable
	{
		// Token: 0x0600152F RID: 5423
		bool Match(object obj);
	}
}
