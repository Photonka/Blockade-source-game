using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050A RID: 1290
	public interface IAeadBlockCipher
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600312B RID: 12587
		string AlgorithmName { get; }

		// Token: 0x0600312C RID: 12588
		IBlockCipher GetUnderlyingCipher();

		// Token: 0x0600312D RID: 12589
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x0600312E RID: 12590
		int GetBlockSize();

		// Token: 0x0600312F RID: 12591
		void ProcessAadByte(byte input);

		// Token: 0x06003130 RID: 12592
		void ProcessAadBytes(byte[] inBytes, int inOff, int len);

		// Token: 0x06003131 RID: 12593
		int ProcessByte(byte input, byte[] outBytes, int outOff);

		// Token: 0x06003132 RID: 12594
		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		// Token: 0x06003133 RID: 12595
		int DoFinal(byte[] outBytes, int outOff);

		// Token: 0x06003134 RID: 12596
		byte[] GetMac();

		// Token: 0x06003135 RID: 12597
		int GetUpdateOutputSize(int len);

		// Token: 0x06003136 RID: 12598
		int GetOutputSize(int len);

		// Token: 0x06003137 RID: 12599
		void Reset();
	}
}
