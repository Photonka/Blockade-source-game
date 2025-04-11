using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C2 RID: 962
	public interface IDsa
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060027D6 RID: 10198
		string AlgorithmName { get; }

		// Token: 0x060027D7 RID: 10199
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x060027D8 RID: 10200
		BigInteger[] GenerateSignature(byte[] message);

		// Token: 0x060027D9 RID: 10201
		bool VerifySignature(byte[] message, BigInteger r, BigInteger s);
	}
}
