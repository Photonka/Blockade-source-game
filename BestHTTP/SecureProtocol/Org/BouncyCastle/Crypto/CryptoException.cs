using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B6 RID: 950
	[Serializable]
	public class CryptoException : Exception
	{
		// Token: 0x060027A2 RID: 10146 RVA: 0x0008E219 File Offset: 0x0008C419
		public CryptoException()
		{
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0008E285 File Offset: 0x0008C485
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0008E28E File Offset: 0x0008C48E
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
