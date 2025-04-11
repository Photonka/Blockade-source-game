using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x0200027A RID: 634
	public interface ITranslator
	{
		// Token: 0x0600177D RID: 6013
		int GetEncodedBlockSize();

		// Token: 0x0600177E RID: 6014
		int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);

		// Token: 0x0600177F RID: 6015
		int GetDecodedBlockSize();

		// Token: 0x06001780 RID: 6016
		int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);
	}
}
