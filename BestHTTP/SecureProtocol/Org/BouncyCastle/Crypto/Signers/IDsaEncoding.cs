using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048A RID: 1162
	public interface IDsaEncoding
	{
		// Token: 0x06002DFF RID: 11775
		BigInteger[] Decode(BigInteger n, byte[] encoding);

		// Token: 0x06002E00 RID: 11776
		byte[] Encode(BigInteger n, BigInteger r, BigInteger s);
	}
}
