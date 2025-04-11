using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B6 RID: 1462
	public sealed class X25519Agreement : IRawAgreement
	{
		// Token: 0x06003885 RID: 14469 RVA: 0x001669D7 File Offset: 0x00164BD7
		public void Init(ICipherParameters parameters)
		{
			this.privateKey = (X25519PrivateKeyParameters)parameters;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x001669E5 File Offset: 0x00164BE5
		public int AgreementSize
		{
			get
			{
				return X25519PrivateKeyParameters.SecretSize;
			}
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x001669EC File Offset: 0x00164BEC
		public void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off)
		{
			this.privateKey.GenerateSecret((X25519PublicKeyParameters)publicKey, buf, off);
		}

		// Token: 0x04002407 RID: 9223
		private X25519PrivateKeyParameters privateKey;
	}
}
