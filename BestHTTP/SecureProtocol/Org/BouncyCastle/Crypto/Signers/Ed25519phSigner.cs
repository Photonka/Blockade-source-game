using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000482 RID: 1154
	public class Ed25519phSigner : ISigner
	{
		// Token: 0x06002DC3 RID: 11715 RVA: 0x00121221 File Offset: 0x0011F421
		public Ed25519phSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x00121240 File Offset: 0x0011F440
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519ph";
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x00121248 File Offset: 0x0011F448
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed25519PrivateKeyParameters)parameters;
				this.publicKey = this.privateKey.GeneratePublicKey();
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed25519PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x00121297 File Offset: 0x0011F497
		public virtual void Update(byte b)
		{
			this.prehash.Update(b);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x001212A5 File Offset: 0x0011F4A5
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.prehash.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x001212B8 File Offset: 0x0011F4B8
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519phSigner not initialised for signature generation.");
			}
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != this.prehash.DoFinal(array, 0))
			{
				throw new InvalidOperationException("Prehash digest failed");
			}
			byte[] array2 = new byte[Ed25519PrivateKeyParameters.SignatureSize];
			this.privateKey.Sign(Ed25519.Algorithm.Ed25519ph, this.publicKey, this.context, array, 0, Ed25519.PrehashSize, array2, 0);
			return array2;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00121338 File Offset: 0x0011F538
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519phSigner not initialised for verification");
			}
			byte[] encoded = this.publicKey.GetEncoded();
			return Ed25519.VerifyPrehash(signature, 0, encoded, 0, this.context, this.prehash);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x00121381 File Offset: 0x0011F581
		public void Reset()
		{
			this.prehash.Reset();
		}

		// Token: 0x04001D99 RID: 7577
		private readonly IDigest prehash = Ed25519.CreatePrehash();

		// Token: 0x04001D9A RID: 7578
		private readonly byte[] context;

		// Token: 0x04001D9B RID: 7579
		private bool forSigning;

		// Token: 0x04001D9C RID: 7580
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x04001D9D RID: 7581
		private Ed25519PublicKeyParameters publicKey;
	}
}
