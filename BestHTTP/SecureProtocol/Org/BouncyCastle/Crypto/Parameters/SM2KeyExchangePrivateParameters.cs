using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E9 RID: 1257
	public class SM2KeyExchangePrivateParameters : ICipherParameters
	{
		// Token: 0x06003052 RID: 12370 RVA: 0x001298A8 File Offset: 0x00127AA8
		public SM2KeyExchangePrivateParameters(bool initiator, ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey)
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
			this.mInitiator = initiator;
			this.mStaticPrivateKey = staticPrivateKey;
			this.mStaticPublicPoint = parameters.G.Multiply(staticPrivateKey.D).Normalize();
			this.mEphemeralPrivateKey = ephemeralPrivateKey;
			this.mEphemeralPublicPoint = parameters.G.Multiply(ephemeralPrivateKey.D).Normalize();
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x00129944 File Offset: 0x00127B44
		public virtual bool IsInitiator
		{
			get
			{
				return this.mInitiator;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0012994C File Offset: 0x00127B4C
		public virtual ECPrivateKeyParameters StaticPrivateKey
		{
			get
			{
				return this.mStaticPrivateKey;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x00129954 File Offset: 0x00127B54
		public virtual ECPoint StaticPublicPoint
		{
			get
			{
				return this.mStaticPublicPoint;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0012995C File Offset: 0x00127B5C
		public virtual ECPrivateKeyParameters EphemeralPrivateKey
		{
			get
			{
				return this.mEphemeralPrivateKey;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x00129964 File Offset: 0x00127B64
		public virtual ECPoint EphemeralPublicPoint
		{
			get
			{
				return this.mEphemeralPublicPoint;
			}
		}

		// Token: 0x04001EFD RID: 7933
		private readonly bool mInitiator;

		// Token: 0x04001EFE RID: 7934
		private readonly ECPrivateKeyParameters mStaticPrivateKey;

		// Token: 0x04001EFF RID: 7935
		private readonly ECPoint mStaticPublicPoint;

		// Token: 0x04001F00 RID: 7936
		private readonly ECPrivateKeyParameters mEphemeralPrivateKey;

		// Token: 0x04001F01 RID: 7937
		private readonly ECPoint mEphemeralPublicPoint;
	}
}
