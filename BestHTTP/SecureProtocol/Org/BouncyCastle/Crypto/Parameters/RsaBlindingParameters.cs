using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E4 RID: 1252
	public class RsaBlindingParameters : ICipherParameters
	{
		// Token: 0x06003030 RID: 12336 RVA: 0x00129354 File Offset: 0x00127554
		public RsaBlindingParameters(RsaKeyParameters publicKey, BigInteger blindingFactor)
		{
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("RSA parameters should be for a public key");
			}
			this.publicKey = publicKey;
			this.blindingFactor = blindingFactor;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x0012937D File Offset: 0x0012757D
		public RsaKeyParameters PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x00129385 File Offset: 0x00127585
		public BigInteger BlindingFactor
		{
			get
			{
				return this.blindingFactor;
			}
		}

		// Token: 0x04001EE7 RID: 7911
		private readonly RsaKeyParameters publicKey;

		// Token: 0x04001EE8 RID: 7912
		private readonly BigInteger blindingFactor;
	}
}
