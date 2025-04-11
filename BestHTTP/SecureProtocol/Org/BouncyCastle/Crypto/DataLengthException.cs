using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B7 RID: 951
	[Serializable]
	public class DataLengthException : CryptoException
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x0010E267 File Offset: 0x0010C467
		public DataLengthException()
		{
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0010E26F File Offset: 0x0010C46F
		public DataLengthException(string message) : base(message)
		{
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0010E278 File Offset: 0x0010C478
		public DataLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
