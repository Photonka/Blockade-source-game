using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044E RID: 1102
	public abstract class TlsDsaSigner : AbstractTlsSigner
	{
		// Token: 0x06002B6A RID: 11114 RVA: 0x00118610 File Offset: 0x00116810
		public override byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
			if (algorithm == null)
			{
				signer.BlockUpdate(hash, 16, 20);
			}
			else
			{
				signer.BlockUpdate(hash, 0, hash.Length);
			}
			return signer.GenerateSignature();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0011865C File Offset: 0x0011685C
		public override bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, false, publicKey);
			if (algorithm == null)
			{
				signer.BlockUpdate(hash, 16, 20);
			}
			else
			{
				signer.BlockUpdate(hash, 0, hash.Length);
			}
			return signer.VerifySignature(sigBytes);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x00118699 File Offset: 0x00116899
		public override ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey)
		{
			return this.MakeSigner(algorithm, false, true, privateKey);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x001186A5 File Offset: 0x001168A5
		public override ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey)
		{
			return this.MakeSigner(algorithm, false, false, publicKey);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x001186B1 File Offset: 0x001168B1
		protected virtual ICipherParameters MakeInitParameters(bool forSigning, ICipherParameters cp)
		{
			return cp;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x001186B4 File Offset: 0x001168B4
		protected virtual ISigner MakeSigner(SignatureAndHashAlgorithm algorithm, bool raw, bool forSigning, ICipherParameters cp)
		{
			if (algorithm != null != TlsUtilities.IsTlsV12(this.mContext))
			{
				throw new InvalidOperationException();
			}
			if (algorithm != null && algorithm.Signature != this.SignatureAlgorithm)
			{
				throw new InvalidOperationException();
			}
			byte hashAlgorithm = (algorithm == null) ? 2 : algorithm.Hash;
			IDigest digest;
			if (!raw)
			{
				digest = TlsUtilities.CreateHash(hashAlgorithm);
			}
			else
			{
				IDigest digest2 = new NullDigest();
				digest = digest2;
			}
			IDigest digest3 = digest;
			DsaDigestSigner dsaDigestSigner = new DsaDigestSigner(this.CreateDsaImpl(hashAlgorithm), digest3);
			((ISigner)dsaDigestSigner).Init(forSigning, this.MakeInitParameters(forSigning, cp));
			return dsaDigestSigner;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002B70 RID: 11120
		protected abstract byte SignatureAlgorithm { get; }

		// Token: 0x06002B71 RID: 11121
		protected abstract IDsa CreateDsaImpl(byte hashAlgorithm);
	}
}
