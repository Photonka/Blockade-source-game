using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045C RID: 1116
	public class TlsNoCloseNotifyException : EndOfStreamException
	{
		// Token: 0x06002C0C RID: 11276 RVA: 0x00119D5E File Offset: 0x00117F5E
		public TlsNoCloseNotifyException() : base("No close_notify alert received before connection closed")
		{
		}
	}
}
