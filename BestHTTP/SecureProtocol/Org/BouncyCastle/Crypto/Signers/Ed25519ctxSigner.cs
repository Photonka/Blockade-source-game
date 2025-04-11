using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000481 RID: 1153
	public class Ed25519ctxSigner : ISigner
	{
		// Token: 0x06002DBB RID: 11707 RVA: 0x0012110F File Offset: 0x0011F30F
		public Ed25519ctxSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x0012112E File Offset: 0x0011F32E
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519ctx";
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x00121138 File Offset: 0x0011F338
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

		// Token: 0x06002DBE RID: 11710 RVA: 0x00121187 File Offset: 0x0011F387
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x00121195 File Offset: 0x0011F395
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x001211A5 File Offset: 0x0011F3A5
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519ctxSigner not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey, this.publicKey, this.context);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x001211DF File Offset: 0x0011F3DF
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519ctxSigner not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, this.context, signature);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00121214 File Offset: 0x0011F414
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x04001D94 RID: 7572
		private readonly Ed25519ctxSigner.Buffer buffer = new Ed25519ctxSigner.Buffer();

		// Token: 0x04001D95 RID: 7573
		private readonly byte[] context;

		// Token: 0x04001D96 RID: 7574
		private bool forSigning;

		// Token: 0x04001D97 RID: 7575
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x04001D98 RID: 7576
		private Ed25519PublicKeyParameters publicKey;

		// Token: 0x02000925 RID: 2341
		private class Buffer : MemoryStream
		{
			// Token: 0x06004E32 RID: 20018 RVA: 0x001B3324 File Offset: 0x001B1524
			internal byte[] GenerateSignature(Ed25519PrivateKeyParameters privateKey, Ed25519PublicKeyParameters publicKey, byte[] ctx)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed25519PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed25519.Algorithm.Ed25519ctx, publicKey, ctx, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06004E33 RID: 20019 RVA: 0x001B3390 File Offset: 0x001B1590
			internal bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] ctx, byte[] signature)
			{
				bool result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int mLen = (int)this.Position;
					byte[] encoded = publicKey.GetEncoded();
					bool flag2 = Ed25519.Verify(signature, 0, encoded, 0, ctx, buffer, 0, mLen);
					this.Reset();
					result = flag2;
				}
				return result;
			}

			// Token: 0x06004E34 RID: 20020 RVA: 0x001B33F4 File Offset: 0x001B15F4
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
