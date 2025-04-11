using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000485 RID: 1157
	public class Ed448Signer : ISigner
	{
		// Token: 0x06002DDB RID: 11739 RVA: 0x001215F6 File Offset: 0x0011F7F6
		public Ed448Signer(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x00121615 File Offset: 0x0011F815
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed448";
			}
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0012161C File Offset: 0x0011F81C
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed448PrivateKeyParameters)parameters;
				this.publicKey = this.privateKey.GeneratePublicKey();
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed448PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0012166B File Offset: 0x0011F86B
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x00121679 File Offset: 0x0011F879
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x00121689 File Offset: 0x0011F889
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed448Signer not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey, this.publicKey, this.context);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x001216C3 File Offset: 0x0011F8C3
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed448Signer not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, this.context, signature);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x001216F8 File Offset: 0x0011F8F8
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x04001DA7 RID: 7591
		private readonly Ed448Signer.Buffer buffer = new Ed448Signer.Buffer();

		// Token: 0x04001DA8 RID: 7592
		private readonly byte[] context;

		// Token: 0x04001DA9 RID: 7593
		private bool forSigning;

		// Token: 0x04001DAA RID: 7594
		private Ed448PrivateKeyParameters privateKey;

		// Token: 0x04001DAB RID: 7595
		private Ed448PublicKeyParameters publicKey;

		// Token: 0x02000927 RID: 2343
		private class Buffer : MemoryStream
		{
			// Token: 0x06004E3A RID: 20026 RVA: 0x001B356C File Offset: 0x001B176C
			internal byte[] GenerateSignature(Ed448PrivateKeyParameters privateKey, Ed448PublicKeyParameters publicKey, byte[] ctx)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed448PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed448.Algorithm.Ed448, publicKey, ctx, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06004E3B RID: 20027 RVA: 0x001B35D8 File Offset: 0x001B17D8
			internal bool VerifySignature(Ed448PublicKeyParameters publicKey, byte[] ctx, byte[] signature)
			{
				bool result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int mLen = (int)this.Position;
					byte[] encoded = publicKey.GetEncoded();
					bool flag2 = Ed448.Verify(signature, 0, encoded, 0, ctx, buffer, 0, mLen);
					this.Reset();
					result = flag2;
				}
				return result;
			}

			// Token: 0x06004E3C RID: 20028 RVA: 0x001B363C File Offset: 0x001B183C
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
