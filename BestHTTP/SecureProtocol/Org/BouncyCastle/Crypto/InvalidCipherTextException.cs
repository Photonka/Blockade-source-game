using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C7 RID: 967
	[Serializable]
	public class InvalidCipherTextException : CryptoException
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x0010E267 File Offset: 0x0010C467
		public InvalidCipherTextException()
		{
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0010E26F File Offset: 0x0010C46F
		public InvalidCipherTextException(string message) : base(message)
		{
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0010E278 File Offset: 0x0010C478
		public InvalidCipherTextException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
