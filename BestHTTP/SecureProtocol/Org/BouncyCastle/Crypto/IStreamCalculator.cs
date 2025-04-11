using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CD RID: 973
	public interface IStreamCalculator
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060027FE RID: 10238
		Stream Stream { get; }

		// Token: 0x060027FF RID: 10239
		object GetResult();
	}
}
