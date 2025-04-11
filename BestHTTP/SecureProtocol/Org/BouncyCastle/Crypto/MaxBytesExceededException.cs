using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D5 RID: 981
	[Serializable]
	public class MaxBytesExceededException : CryptoException
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x0010E267 File Offset: 0x0010C467
		public MaxBytesExceededException()
		{
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0010E26F File Offset: 0x0010C46F
		public MaxBytesExceededException(string message) : base(message)
		{
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0010E278 File Offset: 0x0010C478
		public MaxBytesExceededException(string message, Exception e) : base(message, e)
		{
		}
	}
}
