using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004EA RID: 1258
	public class SM2KeyExchangePublicParameters : ICipherParameters
	{
		// Token: 0x06003058 RID: 12376 RVA: 0x0012996C File Offset: 0x00127B6C
		public SM2KeyExchangePublicParameters(ECPublicKeyParameters staticPublicKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			if (staticPublicKey == null)
			{
				throw new ArgumentNullException("staticPublicKey");
			}
			if (ephemeralPublicKey == null)
			{
				throw new ArgumentNullException("ephemeralPublicKey");
			}
			if (!staticPublicKey.Parameters.Equals(ephemeralPublicKey.Parameters))
			{
				throw new ArgumentException("Static and ephemeral public keys have different domain parameters");
			}
			this.mStaticPublicKey = staticPublicKey;
			this.mEphemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x001299C7 File Offset: 0x00127BC7
		public virtual ECPublicKeyParameters StaticPublicKey
		{
			get
			{
				return this.mStaticPublicKey;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x001299CF File Offset: 0x00127BCF
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.mEphemeralPublicKey;
			}
		}

		// Token: 0x04001F02 RID: 7938
		private readonly ECPublicKeyParameters mStaticPublicKey;

		// Token: 0x04001F03 RID: 7939
		private readonly ECPublicKeyParameters mEphemeralPublicKey;
	}
}
