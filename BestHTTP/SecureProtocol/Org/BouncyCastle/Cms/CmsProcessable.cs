using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E0 RID: 1504
	public interface CmsProcessable
	{
		// Token: 0x0600398A RID: 14730
		void Write(Stream outStream);

		// Token: 0x0600398B RID: 14731
		[Obsolete]
		object GetContent();
	}
}
