using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000487 RID: 1159
	public class Gost3410DigestSigner : ISigner
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x001218F1 File Offset: 0x0011FAF1
		public Gost3410DigestSigner(IDsa signer, IDigest digest)
		{
			this.dsaSigner = signer;
			this.digest = digest;
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x00121907 File Offset: 0x0011FB07
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsaSigner.AlgorithmName;
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0012192C File Offset: 0x0011FB2C
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
				throw new InvalidKeyException("Signing Requires Private Key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification Requires Public Key.");
			}
			this.Reset();
			this.dsaSigner.Init(forSigning, parameters);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x001219A1 File Offset: 0x0011FBA1
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x001219AF File Offset: 0x0011FBAF
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x001219C0 File Offset: 0x0011FBC0
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GOST3410DigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] result;
			try
			{
				BigInteger[] array2 = this.dsaSigner.GenerateSignature(array);
				byte[] array3 = new byte[64];
				byte[] array4 = array2[0].ToByteArrayUnsigned();
				byte[] array5 = array2[1].ToByteArrayUnsigned();
				array5.CopyTo(array3, 32 - array5.Length);
				array4.CopyTo(array3, 64 - array4.Length);
				result = array3;
			}
			catch (Exception ex)
			{
				throw new SignatureException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x00121A68 File Offset: 0x0011FC68
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger r;
			BigInteger s;
			try
			{
				r = new BigInteger(1, signature, 32, 32);
				s = new BigInteger(1, signature, 0, 32);
			}
			catch (Exception exception)
			{
				throw new SignatureException("error decoding signature bytes.", exception);
			}
			return this.dsaSigner.VerifySignature(array, r, s);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00121AEC File Offset: 0x0011FCEC
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x04001DAF RID: 7599
		private readonly IDigest digest;

		// Token: 0x04001DB0 RID: 7600
		private readonly IDsa dsaSigner;

		// Token: 0x04001DB1 RID: 7601
		private bool forSigning;
	}
}
