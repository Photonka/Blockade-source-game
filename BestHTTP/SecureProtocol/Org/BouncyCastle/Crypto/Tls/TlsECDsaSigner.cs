using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000453 RID: 1107
	public class TlsECDsaSigner : TlsDsaSigner
	{
		// Token: 0x06002BB4 RID: 11188 RVA: 0x001196AE File Offset: 0x001178AE
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is ECPublicKeyParameters;
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x001196B9 File Offset: 0x001178B9
		protected override IDsa CreateDsaImpl(byte hashAlgorithm)
		{
			return new ECDsaSigner(new HMacDsaKCalculator(TlsUtilities.CreateHash(hashAlgorithm)));
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000AA054 File Offset: 0x000A8254
		protected override byte SignatureAlgorithm
		{
			get
			{
				return 3;
			}
		}
	}
}
