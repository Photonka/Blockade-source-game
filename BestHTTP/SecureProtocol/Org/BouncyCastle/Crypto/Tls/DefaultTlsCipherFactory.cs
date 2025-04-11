using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003FF RID: 1023
	public class DefaultTlsCipherFactory : AbstractTlsCipherFactory
	{
		// Token: 0x06002958 RID: 10584 RVA: 0x001109A8 File Offset: 0x0010EBA8
		public override TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm)
		{
			switch (encryptionAlgorithm)
			{
			case 0:
				return this.CreateNullCipher(context, macAlgorithm);
			case 1:
			case 3:
			case 4:
			case 5:
			case 6:
				break;
			case 2:
				return this.CreateRC4Cipher(context, 16, macAlgorithm);
			case 7:
				return this.CreateDesEdeCipher(context, macAlgorithm);
			case 8:
				return this.CreateAESCipher(context, 16, macAlgorithm);
			case 9:
				return this.CreateAESCipher(context, 32, macAlgorithm);
			case 10:
				return this.CreateCipher_Aes_Gcm(context, 16, 16);
			case 11:
				return this.CreateCipher_Aes_Gcm(context, 32, 16);
			case 12:
				return this.CreateCamelliaCipher(context, 16, macAlgorithm);
			case 13:
				return this.CreateCamelliaCipher(context, 32, macAlgorithm);
			case 14:
				return this.CreateSeedCipher(context, macAlgorithm);
			case 15:
				return this.CreateCipher_Aes_Ccm(context, 16, 16);
			case 16:
				return this.CreateCipher_Aes_Ccm(context, 16, 8);
			case 17:
				return this.CreateCipher_Aes_Ccm(context, 32, 16);
			case 18:
				return this.CreateCipher_Aes_Ccm(context, 32, 8);
			case 19:
				return this.CreateCipher_Camellia_Gcm(context, 16, 16);
			case 20:
				return this.CreateCipher_Camellia_Gcm(context, 32, 16);
			case 21:
				return this.CreateChaCha20Poly1305(context);
			default:
				if (encryptionAlgorithm == 103)
				{
					return this.CreateCipher_Aes_Ocb(context, 16, 12);
				}
				if (encryptionAlgorithm == 104)
				{
					return this.CreateCipher_Aes_Ocb(context, 32, 12);
				}
				break;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x00110AF9 File Offset: 0x0010ECF9
		protected virtual TlsBlockCipher CreateAESCipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateAesBlockCipher(), this.CreateAesBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00110B1C File Offset: 0x0010ED1C
		protected virtual TlsBlockCipher CreateCamelliaCipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateCamelliaBlockCipher(), this.CreateCamelliaBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x00110B3F File Offset: 0x0010ED3F
		protected virtual TlsCipher CreateChaCha20Poly1305(TlsContext context)
		{
			return new Chacha20Poly1305(context);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00110B47 File Offset: 0x0010ED47
		protected virtual TlsAeadCipher CreateCipher_Aes_Ccm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Ccm(), this.CreateAeadBlockCipher_Aes_Ccm(), cipherKeySize, macSize);
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00110B5D File Offset: 0x0010ED5D
		protected virtual TlsAeadCipher CreateCipher_Aes_Gcm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Gcm(), this.CreateAeadBlockCipher_Aes_Gcm(), cipherKeySize, macSize);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00110B73 File Offset: 0x0010ED73
		protected virtual TlsAeadCipher CreateCipher_Aes_Ocb(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Ocb(), this.CreateAeadBlockCipher_Aes_Ocb(), cipherKeySize, macSize, 2);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00110B8A File Offset: 0x0010ED8A
		protected virtual TlsAeadCipher CreateCipher_Camellia_Gcm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Camellia_Gcm(), this.CreateAeadBlockCipher_Camellia_Gcm(), cipherKeySize, macSize);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00110BA0 File Offset: 0x0010EDA0
		protected virtual TlsBlockCipher CreateDesEdeCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateDesEdeBlockCipher(), this.CreateDesEdeBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), 24);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00110BC4 File Offset: 0x0010EDC4
		protected virtual TlsNullCipher CreateNullCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsNullCipher(context, this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm));
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00110BDA File Offset: 0x0010EDDA
		protected virtual TlsStreamCipher CreateRC4Cipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsStreamCipher(context, this.CreateRC4StreamCipher(), this.CreateRC4StreamCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize, false);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00110BFE File Offset: 0x0010EDFE
		protected virtual TlsBlockCipher CreateSeedCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateSeedBlockCipher(), this.CreateSeedBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), 16);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00110C22 File Offset: 0x0010EE22
		protected virtual IBlockCipher CreateAesEngine()
		{
			return new AesEngine();
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00110C29 File Offset: 0x0010EE29
		protected virtual IBlockCipher CreateCamelliaEngine()
		{
			return new CamelliaEngine();
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00110C30 File Offset: 0x0010EE30
		protected virtual IBlockCipher CreateAesBlockCipher()
		{
			return new CbcBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00110C3D File Offset: 0x0010EE3D
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Ccm()
		{
			return new CcmBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x00110C4A File Offset: 0x0010EE4A
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Gcm()
		{
			return new GcmBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00110C57 File Offset: 0x0010EE57
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Ocb()
		{
			return new OcbBlockCipher(this.CreateAesEngine(), this.CreateAesEngine());
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00110C6A File Offset: 0x0010EE6A
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Camellia_Gcm()
		{
			return new GcmBlockCipher(this.CreateCamelliaEngine());
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00110C77 File Offset: 0x0010EE77
		protected virtual IBlockCipher CreateCamelliaBlockCipher()
		{
			return new CbcBlockCipher(this.CreateCamelliaEngine());
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00110C84 File Offset: 0x0010EE84
		protected virtual IBlockCipher CreateDesEdeBlockCipher()
		{
			return new CbcBlockCipher(new DesEdeEngine());
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00110C90 File Offset: 0x0010EE90
		protected virtual IStreamCipher CreateRC4StreamCipher()
		{
			return new RC4Engine();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00110C97 File Offset: 0x0010EE97
		protected virtual IBlockCipher CreateSeedBlockCipher()
		{
			return new CbcBlockCipher(new SeedEngine());
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00110CA4 File Offset: 0x0010EEA4
		protected virtual IDigest CreateHMacDigest(int macAlgorithm)
		{
			switch (macAlgorithm)
			{
			case 0:
				return null;
			case 1:
				return TlsUtilities.CreateHash(1);
			case 2:
				return TlsUtilities.CreateHash(2);
			case 3:
				return TlsUtilities.CreateHash(4);
			case 4:
				return TlsUtilities.CreateHash(5);
			case 5:
				return TlsUtilities.CreateHash(6);
			default:
				throw new TlsFatalAlert(80);
			}
		}
	}
}
