using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000502 RID: 1282
	public class DefaultVerifierResult : IVerifier
	{
		// Token: 0x060030C4 RID: 12484 RVA: 0x0012AC8B File Offset: 0x00128E8B
		public DefaultVerifierResult(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0012AC9A File Offset: 0x00128E9A
		public bool IsVerified(byte[] signature)
		{
			return this.mSigner.VerifySignature(signature);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0012ACA8 File Offset: 0x00128EA8
		public bool IsVerified(byte[] sig, int sigOff, int sigLen)
		{
			byte[] signature = Arrays.CopyOfRange(sig, sigOff, sigOff + sigLen);
			return this.IsVerified(signature);
		}

		// Token: 0x04001F23 RID: 7971
		private readonly ISigner mSigner;
	}
}
