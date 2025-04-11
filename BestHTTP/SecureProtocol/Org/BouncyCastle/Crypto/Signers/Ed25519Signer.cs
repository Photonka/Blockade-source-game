using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000483 RID: 1155
	public class Ed25519Signer : ISigner
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x001213A1 File Offset: 0x0011F5A1
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519";
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x001213A8 File Offset: 0x0011F5A8
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

		// Token: 0x06002DCE RID: 11726 RVA: 0x001213F7 File Offset: 0x0011F5F7
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00121405 File Offset: 0x0011F605
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00121415 File Offset: 0x0011F615
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519Signer not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey, this.publicKey);
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00121449 File Offset: 0x0011F649
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519Signer not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, signature);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00121478 File Offset: 0x0011F678
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x04001D9E RID: 7582
		private readonly Ed25519Signer.Buffer buffer = new Ed25519Signer.Buffer();

		// Token: 0x04001D9F RID: 7583
		private bool forSigning;

		// Token: 0x04001DA0 RID: 7584
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x04001DA1 RID: 7585
		private Ed25519PublicKeyParameters publicKey;

		// Token: 0x02000926 RID: 2342
		private class Buffer : MemoryStream
		{
			// Token: 0x06004E36 RID: 20022 RVA: 0x001B3448 File Offset: 0x001B1648
			internal byte[] GenerateSignature(Ed25519PrivateKeyParameters privateKey, Ed25519PublicKeyParameters publicKey)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed25519PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed25519.Algorithm.Ed25519, publicKey, null, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06004E37 RID: 20023 RVA: 0x001B34B4 File Offset: 0x001B16B4
			internal bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] signature)
			{
				bool result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int mLen = (int)this.Position;
					byte[] encoded = publicKey.GetEncoded();
					bool flag2 = Ed25519.Verify(signature, 0, encoded, 0, buffer, 0, mLen);
					this.Reset();
					result = flag2;
				}
				return result;
			}

			// Token: 0x06004E38 RID: 20024 RVA: 0x001B3518 File Offset: 0x001B1718
			internal void Reset()
			{
				lock (this)
				{
					long position = this.Position;
					Array.Clear(this.GetBuffer(), 0, (int)position);
					this.Position = 0L;
				}
			}
		}
	}
}
