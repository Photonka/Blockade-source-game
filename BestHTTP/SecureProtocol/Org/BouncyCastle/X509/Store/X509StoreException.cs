using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000245 RID: 581
	[Serializable]
	public class X509StoreException : Exception
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x0008E219 File Offset: 0x0008C419
		public X509StoreException()
		{
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0008E285 File Offset: 0x0008C485
		public X509StoreException(string message) : base(message)
		{
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0008E28E File Offset: 0x0008C48E
		public X509StoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
