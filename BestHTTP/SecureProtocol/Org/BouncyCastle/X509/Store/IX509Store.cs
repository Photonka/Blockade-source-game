using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x0200023C RID: 572
	public interface IX509Store
	{
		// Token: 0x06001530 RID: 5424
		ICollection GetMatches(IX509Selector selector);
	}
}
