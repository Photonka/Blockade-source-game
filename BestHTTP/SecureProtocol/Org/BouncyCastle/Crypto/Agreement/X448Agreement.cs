using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B7 RID: 1463
	public sealed class X448Agreement : IRawAgreement
	{
		// Token: 0x06003889 RID: 14473 RVA: 0x00166A01 File Offset: 0x00164C01
		public void Init(ICipherParameters parameters)
		{
			this.privateKey = (X448PrivateKeyParameters)parameters;
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x00166A0F File Offset: 0x00164C0F
		public int AgreementSize
		{
			get
			{
				return X448PrivateKeyParameters.SecretSize;
			}
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x00166A16 File Offset: 0x00164C16
		public void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off)
		{
			this.privateKey.GenerateSecret((X448PublicKeyParameters)publicKey, buf, off);
		}

		// Token: 0x04002408 RID: 9224
		private X448PrivateKeyParameters privateKey;
	}
}
