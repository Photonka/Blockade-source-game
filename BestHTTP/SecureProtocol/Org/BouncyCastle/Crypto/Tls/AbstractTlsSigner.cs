using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E3 RID: 995
	public abstract class AbstractTlsSigner : TlsSigner
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x0010F43E File Offset: 0x0010D63E
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0010F447 File Offset: 0x0010D647
		public virtual byte[] GenerateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5AndSha1)
		{
			return this.GenerateRawSignature(null, privateKey, md5AndSha1);
		}

		// Token: 0x060028CB RID: 10443
		public abstract byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash);

		// Token: 0x060028CC RID: 10444 RVA: 0x0010F452 File Offset: 0x0010D652
		public virtual bool VerifyRawSignature(byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] md5AndSha1)
		{
			return this.VerifyRawSignature(null, sigBytes, publicKey, md5AndSha1);
		}

		// Token: 0x060028CD RID: 10445
		public abstract bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash);

		// Token: 0x060028CE RID: 10446 RVA: 0x0010F45E File Offset: 0x0010D65E
		public virtual ISigner CreateSigner(AsymmetricKeyParameter privateKey)
		{
			return this.CreateSigner(null, privateKey);
		}

		// Token: 0x060028CF RID: 10447
		public abstract ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey);

		// Token: 0x060028D0 RID: 10448 RVA: 0x0010F468 File Offset: 0x0010D668
		public virtual ISigner CreateVerifyer(AsymmetricKeyParameter publicKey)
		{
			return this.CreateVerifyer(null, publicKey);
		}

		// Token: 0x060028D1 RID: 10449
		public abstract ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey);

		// Token: 0x060028D2 RID: 10450
		public abstract bool IsValidPublicKey(AsymmetricKeyParameter publicKey);

		// Token: 0x040019F7 RID: 6647
		protected TlsContext mContext;
	}
}
