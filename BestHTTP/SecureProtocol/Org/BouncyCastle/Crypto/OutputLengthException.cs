using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D6 RID: 982
	[Serializable]
	public class OutputLengthException : DataLengthException
	{
		// Token: 0x06002816 RID: 10262 RVA: 0x0010E2CA File Offset: 0x0010C4CA
		public OutputLengthException()
		{
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0010E2D2 File Offset: 0x0010C4D2
		public OutputLengthException(string message) : base(message)
		{
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0010E2DB File Offset: 0x0010C4DB
		public OutputLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
