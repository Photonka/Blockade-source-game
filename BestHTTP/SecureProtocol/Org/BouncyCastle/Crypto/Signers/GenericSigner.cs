using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000486 RID: 1158
	public class GenericSigner : ISigner
	{
		// Token: 0x06002DE3 RID: 11747 RVA: 0x00121705 File Offset: 0x0011F905
		public GenericSigner(IAsymmetricBlockCipher engine, IDigest digest)
		{
			this.engine = engine;
			this.digest = digest;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0012171C File Offset: 0x0011F91C
		public virtual string AlgorithmName
		{
			get
			{
				return string.Concat(new string[]
				{
					"Generic(",
					this.engine.AlgorithmName,
					"/",
					this.digest.AlgorithmName,
					")"
				});
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00121768 File Offset: 0x0011F968
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.engine.Init(forSigning, parameters);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x001217DD File Offset: 0x0011F9DD
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x001217EB File Offset: 0x0011F9EB
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x001217FC File Offset: 0x0011F9FC
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0012184C File Offset: 0x0011FA4C
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				byte[] array2 = this.engine.ProcessBlock(signature, 0, signature.Length);
				if (array2.Length < array.Length)
				{
					byte[] array3 = new byte[array.Length];
					Array.Copy(array2, 0, array3, array3.Length - array2.Length, array2.Length);
					array2 = array3;
				}
				result = Arrays.ConstantTimeAreEqual(array2, array);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x001218E4 File Offset: 0x0011FAE4
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x04001DAC RID: 7596
		private readonly IAsymmetricBlockCipher engine;

		// Token: 0x04001DAD RID: 7597
		private readonly IDigest digest;

		// Token: 0x04001DAE RID: 7598
		private bool forSigning;
	}
}
