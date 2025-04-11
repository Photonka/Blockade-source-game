using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000500 RID: 1280
	public class DefaultSignatureResult : IBlockResult
	{
		// Token: 0x060030BE RID: 12478 RVA: 0x0012AC2F File Offset: 0x00128E2F
		public DefaultSignatureResult(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0012AC3E File Offset: 0x00128E3E
		public byte[] Collect()
		{
			return this.mSigner.GenerateSignature();
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0012AC4B File Offset: 0x00128E4B
		public int Collect(byte[] sig, int sigOff)
		{
			byte[] array = this.Collect();
			array.CopyTo(sig, sigOff);
			return array.Length;
		}

		// Token: 0x04001F21 RID: 7969
		private readonly ISigner mSigner;
	}
}
