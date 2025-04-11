using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CE RID: 974
	public interface IStreamCipher
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002800 RID: 10240
		string AlgorithmName { get; }

		// Token: 0x06002801 RID: 10241
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06002802 RID: 10242
		byte ReturnByte(byte input);

		// Token: 0x06002803 RID: 10243
		void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x06002804 RID: 10244
		void Reset();
	}
}
