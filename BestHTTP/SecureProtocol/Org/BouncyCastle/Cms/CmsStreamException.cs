using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005ED RID: 1517
	[Serializable]
	public class CmsStreamException : IOException
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x000B979A File Offset: 0x000B799A
		public CmsStreamException()
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public CmsStreamException(string name) : base(name)
		{
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000B97AB File Offset: 0x000B79AB
		public CmsStreamException(string name, Exception e) : base(name, e)
		{
		}
	}
}
