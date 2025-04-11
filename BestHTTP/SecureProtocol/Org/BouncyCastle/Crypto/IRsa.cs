using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C9 RID: 969
	public interface IRsa
	{
		// Token: 0x060027EC RID: 10220
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060027ED RID: 10221
		int GetInputBlockSize();

		// Token: 0x060027EE RID: 10222
		int GetOutputBlockSize();

		// Token: 0x060027EF RID: 10223
		BigInteger ConvertInput(byte[] buf, int off, int len);

		// Token: 0x060027F0 RID: 10224
		BigInteger ProcessBlock(BigInteger input);

		// Token: 0x060027F1 RID: 10225
		byte[] ConvertOutput(BigInteger result);
	}
}
