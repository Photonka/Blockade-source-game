using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005DE RID: 1502
	[Serializable]
	public class CmsException : Exception
	{
		// Token: 0x0600397A RID: 14714 RVA: 0x0008E219 File Offset: 0x0008C419
		public CmsException()
		{
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x0008E285 File Offset: 0x0008C485
		public CmsException(string msg) : base(msg)
		{
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x0008E28E File Offset: 0x0008C48E
		public CmsException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
