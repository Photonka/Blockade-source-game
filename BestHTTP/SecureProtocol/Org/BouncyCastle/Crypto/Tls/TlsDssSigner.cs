using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044F RID: 1103
	public class TlsDssSigner : TlsDsaSigner
	{
		// Token: 0x06002B73 RID: 11123 RVA: 0x00118734 File Offset: 0x00116934
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is DsaPublicKeyParameters;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0011873F File Offset: 0x0011693F
		protected override IDsa CreateDsaImpl(byte hashAlgorithm)
		{
			return new DsaSigner(new HMacDsaKCalculator(TlsUtilities.CreateHash(hashAlgorithm)));
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000A8A48 File Offset: 0x000A6C48
		protected override byte SignatureAlgorithm
		{
			get
			{
				return 2;
			}
		}
	}
}
