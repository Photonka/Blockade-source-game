using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	public class StreamOverflowException : IOException
	{
		// Token: 0x06001718 RID: 5912 RVA: 0x000B979A File Offset: 0x000B799A
		public StreamOverflowException()
		{
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public StreamOverflowException(string message) : base(message)
		{
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000B97AB File Offset: 0x000B79AB
		public StreamOverflowException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
