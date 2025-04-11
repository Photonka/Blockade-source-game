using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000466 RID: 1126
	public class TlsRsaSigner : AbstractTlsSigner
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x0011BB97 File Offset: 0x00119D97
		public override byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
			signer.BlockUpdate(hash, 0, hash.Length);
			return signer.GenerateSignature();
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0011BBC3 File Offset: 0x00119DC3
		public override bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, false, publicKey);
			signer.BlockUpdate(hash, 0, hash.Length);
			return signer.VerifySignature(sigBytes);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0011BBE2 File Offset: 0x00119DE2
		public override ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey)
		{
			return this.MakeSigner(algorithm, false, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0011BBFE File Offset: 0x00119DFE
		public override ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey)
		{
			return this.MakeSigner(algorithm, false, false, publicKey);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0011BC0A File Offset: 0x00119E0A
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is RsaKeyParameters && !publicKey.IsPrivate;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0011BC20 File Offset: 0x00119E20
		protected virtual ISigner MakeSigner(SignatureAndHashAlgorithm algorithm, bool raw, bool forSigning, ICipherParameters cp)
		{
			if (algorithm != null != TlsUtilities.IsTlsV12(this.mContext))
			{
				throw new InvalidOperationException();
			}
			if (algorithm != null && algorithm.Signature != 1)
			{
				throw new InvalidOperationException();
			}
			IDigest digest;
			if (raw)
			{
				digest = new NullDigest();
			}
			else if (algorithm == null)
			{
				digest = new CombinedHash();
			}
			else
			{
				digest = TlsUtilities.CreateHash(algorithm.Hash);
			}
			ISigner signer;
			if (algorithm != null)
			{
				signer = new RsaDigestSigner(digest, TlsUtilities.GetOidForHashAlgorithm(algorithm.Hash));
			}
			else
			{
				signer = new GenericSigner(this.CreateRsaImpl(), digest);
			}
			signer.Init(forSigning, cp);
			return signer;
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0011BCA6 File Offset: 0x00119EA6
		protected virtual IAsymmetricBlockCipher CreateRsaImpl()
		{
			return new Pkcs1Encoding(new RsaBlindedEngine());
		}
	}
}
