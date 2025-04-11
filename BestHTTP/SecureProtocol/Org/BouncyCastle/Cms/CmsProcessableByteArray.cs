using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E1 RID: 1505
	public class CmsProcessableByteArray : CmsProcessable, CmsReadable
	{
		// Token: 0x0600398C RID: 14732 RVA: 0x0016A27C File Offset: 0x0016847C
		public CmsProcessableByteArray(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x0016A28B File Offset: 0x0016848B
		public virtual Stream GetInputStream()
		{
			return new MemoryStream(this.bytes, false);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0016A299 File Offset: 0x00168499
		public virtual void Write(Stream zOut)
		{
			zOut.Write(this.bytes, 0, this.bytes.Length);
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x0016A2B0 File Offset: 0x001684B0
		[Obsolete]
		public virtual object GetContent()
		{
			return this.bytes.Clone();
		}

		// Token: 0x040024CB RID: 9419
		private readonly byte[] bytes;
	}
}
