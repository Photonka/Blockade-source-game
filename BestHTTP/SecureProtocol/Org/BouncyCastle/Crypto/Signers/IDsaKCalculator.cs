using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048B RID: 1163
	public interface IDsaKCalculator
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002E01 RID: 11777
		bool IsDeterministic { get; }

		// Token: 0x06002E02 RID: 11778
		void Init(BigInteger n, SecureRandom random);

		// Token: 0x06002E03 RID: 11779
		void Init(BigInteger n, BigInteger d, byte[] message);

		// Token: 0x06002E04 RID: 11780
		BigInteger NextK();
	}
}
