using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D2 RID: 978
	public interface IWrapper
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600280A RID: 10250
		string AlgorithmName { get; }

		// Token: 0x0600280B RID: 10251
		void Init(bool forWrapping, ICipherParameters parameters);

		// Token: 0x0600280C RID: 10252
		byte[] Wrap(byte[] input, int inOff, int length);

		// Token: 0x0600280D RID: 10253
		byte[] Unwrap(byte[] input, int inOff, int length);
	}
}
