using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D9 RID: 1241
	public class MqvPublicParameters : ICipherParameters
	{
		// Token: 0x06003004 RID: 12292 RVA: 0x00129030 File Offset: 0x00127230
		public MqvPublicParameters(ECPublicKeyParameters staticPublicKey, ECPublicKeyParameters ephemeralPublicKey)
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
			this.staticPublicKey = staticPublicKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x0012908B File Offset: 0x0012728B
		public virtual ECPublicKeyParameters StaticPublicKey
		{
			get
			{
				return this.staticPublicKey;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x00129093 File Offset: 0x00127293
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x04001ED2 RID: 7890
		private readonly ECPublicKeyParameters staticPublicKey;

		// Token: 0x04001ED3 RID: 7891
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
