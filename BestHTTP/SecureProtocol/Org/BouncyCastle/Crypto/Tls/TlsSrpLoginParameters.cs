using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000473 RID: 1139
	public class TlsSrpLoginParameters
	{
		// Token: 0x06002CDB RID: 11483 RVA: 0x0011D1B1 File Offset: 0x0011B3B1
		public TlsSrpLoginParameters(Srp6GroupParameters group, BigInteger verifier, byte[] salt)
		{
			this.mGroup = group;
			this.mVerifier = verifier;
			this.mSalt = salt;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x0011D1CE File Offset: 0x0011B3CE
		public virtual Srp6GroupParameters Group
		{
			get
			{
				return this.mGroup;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x0011D1D6 File Offset: 0x0011B3D6
		public virtual byte[] Salt
		{
			get
			{
				return this.mSalt;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x0011D1DE File Offset: 0x0011B3DE
		public virtual BigInteger Verifier
		{
			get
			{
				return this.mVerifier;
			}
		}

		// Token: 0x04001D6E RID: 7534
		protected readonly Srp6GroupParameters mGroup;

		// Token: 0x04001D6F RID: 7535
		protected readonly BigInteger mVerifier;

		// Token: 0x04001D70 RID: 7536
		protected readonly byte[] mSalt;
	}
}
