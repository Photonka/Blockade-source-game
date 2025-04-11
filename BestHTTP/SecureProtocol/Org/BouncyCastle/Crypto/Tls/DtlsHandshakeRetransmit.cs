using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040B RID: 1035
	internal interface DtlsHandshakeRetransmit
	{
		// Token: 0x060029D4 RID: 10708
		void ReceivedHandshakeRecord(int epoch, byte[] buf, int off, int len);
	}
}
