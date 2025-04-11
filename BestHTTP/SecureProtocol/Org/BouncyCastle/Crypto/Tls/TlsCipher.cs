using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000440 RID: 1088
	public interface TlsCipher
	{
		// Token: 0x06002B02 RID: 11010
		int GetPlaintextLimit(int ciphertextLimit);

		// Token: 0x06002B03 RID: 11011
		byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len);

		// Token: 0x06002B04 RID: 11012
		byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len);
	}
}
