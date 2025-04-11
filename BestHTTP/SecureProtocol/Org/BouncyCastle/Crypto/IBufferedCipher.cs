using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003BD RID: 957
	public interface IBufferedCipher
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060027BA RID: 10170
		string AlgorithmName { get; }

		// Token: 0x060027BB RID: 10171
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060027BC RID: 10172
		int GetBlockSize();

		// Token: 0x060027BD RID: 10173
		int GetOutputSize(int inputLen);

		// Token: 0x060027BE RID: 10174
		int GetUpdateOutputSize(int inputLen);

		// Token: 0x060027BF RID: 10175
		byte[] ProcessByte(byte input);

		// Token: 0x060027C0 RID: 10176
		int ProcessByte(byte input, byte[] output, int outOff);

		// Token: 0x060027C1 RID: 10177
		byte[] ProcessBytes(byte[] input);

		// Token: 0x060027C2 RID: 10178
		byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x060027C3 RID: 10179
		int ProcessBytes(byte[] input, byte[] output, int outOff);

		// Token: 0x060027C4 RID: 10180
		int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x060027C5 RID: 10181
		byte[] DoFinal();

		// Token: 0x060027C6 RID: 10182
		byte[] DoFinal(byte[] input);

		// Token: 0x060027C7 RID: 10183
		byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x060027C8 RID: 10184
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060027C9 RID: 10185
		int DoFinal(byte[] input, byte[] output, int outOff);

		// Token: 0x060027CA RID: 10186
		int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x060027CB RID: 10187
		void Reset();
	}
}
