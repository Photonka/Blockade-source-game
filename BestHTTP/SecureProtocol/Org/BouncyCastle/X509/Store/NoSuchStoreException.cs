using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x0200023E RID: 574
	[Serializable]
	public class NoSuchStoreException : X509StoreException
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x000B03A1 File Offset: 0x000AE5A1
		public NoSuchStoreException()
		{
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000B03A9 File Offset: 0x000AE5A9
		public NoSuchStoreException(string message) : base(message)
		{
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000B03B2 File Offset: 0x000AE5B2
		public NoSuchStoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
