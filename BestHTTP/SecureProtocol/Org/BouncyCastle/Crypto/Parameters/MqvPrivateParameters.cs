using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D8 RID: 1240
	public class MqvPrivateParameters : ICipherParameters
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x00128F70 File Offset: 0x00127170
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey) : this(staticPrivateKey, ephemeralPrivateKey, null)
		{
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00128F7C File Offset: 0x0012717C
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			if (staticPrivateKey == null)
			{
				throw new ArgumentNullException("staticPrivateKey");
			}
			if (ephemeralPrivateKey == null)
			{
				throw new ArgumentNullException("ephemeralPrivateKey");
			}
			ECDomainParameters parameters = staticPrivateKey.Parameters;
			if (!parameters.Equals(ephemeralPrivateKey.Parameters))
			{
				throw new ArgumentException("Static and ephemeral private keys have different domain parameters");
			}
			if (ephemeralPublicKey == null)
			{
				ephemeralPublicKey = new ECPublicKeyParameters(parameters.G.Multiply(ephemeralPrivateKey.D), parameters);
			}
			else if (!parameters.Equals(ephemeralPublicKey.Parameters))
			{
				throw new ArgumentException("Ephemeral public key has different domain parameters");
			}
			this.staticPrivateKey = staticPrivateKey;
			this.ephemeralPrivateKey = ephemeralPrivateKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x00129017 File Offset: 0x00127217
		public virtual ECPrivateKeyParameters StaticPrivateKey
		{
			get
			{
				return this.staticPrivateKey;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x0012901F File Offset: 0x0012721F
		public virtual ECPrivateKeyParameters EphemeralPrivateKey
		{
			get
			{
				return this.ephemeralPrivateKey;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x00129027 File Offset: 0x00127227
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x04001ECF RID: 7887
		private readonly ECPrivateKeyParameters staticPrivateKey;

		// Token: 0x04001ED0 RID: 7888
		private readonly ECPrivateKeyParameters ephemeralPrivateKey;

		// Token: 0x04001ED1 RID: 7889
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
