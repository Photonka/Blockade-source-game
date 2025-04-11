using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000484 RID: 1156
	public class Ed448phSigner : ISigner
	{
		// Token: 0x06002DD3 RID: 11731 RVA: 0x00121485 File Offset: 0x0011F685
		public Ed448phSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x001214A4 File Offset: 0x0011F6A4
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed448ph";
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x001214AC File Offset: 0x0011F6AC
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

		// Token: 0x06002DD6 RID: 11734 RVA: 0x001214FB File Offset: 0x0011F6FB
		public virtual void Update(byte b)
		{
			this.prehash.Update(b);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x00121509 File Offset: 0x0011F709
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.prehash.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0012151C File Offset: 0x0011F71C
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed448phSigner not initialised for signature generation.");
			}
			byte[] array = new byte[Ed448.PrehashSize];
			if (Ed448.PrehashSize != this.prehash.DoFinal(array, 0, Ed448.PrehashSize))
			{
				throw new InvalidOperationException("Prehash digest failed");
			}
			byte[] array2 = new byte[Ed448PrivateKeyParameters.SignatureSize];
			this.privateKey.Sign(Ed448.Algorithm.Ed448ph, this.publicKey, this.context, array, 0, Ed448.PrehashSize, array2, 0);
			return array2;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x001215A0 File Offset: 0x0011F7A0
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed448phSigner not initialised for verification");
			}
			byte[] encoded = this.publicKey.GetEncoded();
			return Ed448.VerifyPrehash(signature, 0, encoded, 0, this.context, this.prehash);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x001215E9 File Offset: 0x0011F7E9
		public void Reset()
		{
			this.prehash.Reset();
		}

		// Token: 0x04001DA2 RID: 7586
		private readonly IXof prehash = Ed448.CreatePrehash();

		// Token: 0x04001DA3 RID: 7587
		private readonly byte[] context;

		// Token: 0x04001DA4 RID: 7588
		private bool forSigning;

		// Token: 0x04001DA5 RID: 7589
		private Ed448PrivateKeyParameters privateKey;

		// Token: 0x04001DA6 RID: 7590
		private Ed448PublicKeyParameters publicKey;
	}
}
