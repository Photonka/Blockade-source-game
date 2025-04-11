using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003FD RID: 1021
	public interface DatagramTransport
	{
		// Token: 0x06002950 RID: 10576
		int GetReceiveLimit();

		// Token: 0x06002951 RID: 10577
		int GetSendLimit();

		// Token: 0x06002952 RID: 10578
		int Receive(byte[] buf, int off, int len, int waitMillis);

		// Token: 0x06002953 RID: 10579
		void Send(byte[] buf, int off, int len);

		// Token: 0x06002954 RID: 10580
		void Close();
	}
}
