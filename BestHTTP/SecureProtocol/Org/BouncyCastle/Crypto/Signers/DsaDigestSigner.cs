using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200047C RID: 1148
	public class DsaDigestSigner : ISigner
	{
		// Token: 0x06002D8F RID: 11663 RVA: 0x001203BA File Offset: 0x0011E5BA
		public DsaDigestSigner(IDsa dsa, IDigest digest)
		{
			this.dsa = dsa;
			this.digest = digest;
			this.encoding = StandardDsaEncoding.Instance;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x001203DB File Offset: 0x0011E5DB
		public DsaDigestSigner(IDsaExt dsa, IDigest digest, IDsaEncoding encoding)
		{
			this.dsa = dsa;
			this.digest = digest;
			this.encoding = encoding;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x001203F8 File Offset: 0x0011E5F8
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsa.AlgorithmName;
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0012041C File Offset: 0x0011E61C
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
			this.dsa.Init(forSigning, parameters);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00120491 File Offset: 0x0011E691
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0012049F File Offset: 0x0011E69F
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x001204B0 File Offset: 0x0011E6B0
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger[] array2 = this.dsa.GenerateSignature(array);
			byte[] result;
			try
			{
				result = this.encoding.Encode(this.GetOrder(), array2[0], array2[1]);
			}
			catch (Exception)
			{
				throw new InvalidOperationException("unable to encode signature");
			}
			return result;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00120534 File Offset: 0x0011E734
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				BigInteger[] array2 = this.encoding.Decode(this.GetOrder(), signature);
				result = this.dsa.VerifySignature(array, array2[0], array2[1]);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x001205B4 File Offset: 0x0011E7B4
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x001205C1 File Offset: 0x0011E7C1
		protected virtual BigInteger GetOrder()
		{
			if (!(this.dsa is IDsaExt))
			{
				return null;
			}
			return ((IDsaExt)this.dsa).Order;
		}

		// Token: 0x04001D84 RID: 7556
		private readonly IDsa dsa;

		// Token: 0x04001D85 RID: 7557
		private readonly IDigest digest;

		// Token: 0x04001D86 RID: 7558
		private readonly IDsaEncoding encoding;

		// Token: 0x04001D87 RID: 7559
		private bool forSigning;
	}
}
