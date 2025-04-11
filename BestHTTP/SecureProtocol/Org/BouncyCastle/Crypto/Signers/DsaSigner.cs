using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200047D RID: 1149
	public class DsaSigner : IDsaExt, IDsa
	{
		// Token: 0x06002D99 RID: 11673 RVA: 0x001205E2 File Offset: 0x0011E7E2
		public DsaSigner()
		{
			this.kCalculator = new RandomDsaKCalculator();
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x001205F5 File Offset: 0x0011E7F5
		public DsaSigner(IDsaKCalculator kCalculator)
		{
			this.kCalculator = kCalculator;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x00120604 File Offset: 0x0011E804
		public virtual string AlgorithmName
		{
			get
			{
				return "DSA";
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0012060C File Offset: 0x0011E80C
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			SecureRandom provided = null;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					provided = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				if (!(parameters is DsaPrivateKeyParameters))
				{
					throw new InvalidKeyException("DSA private key required for signing");
				}
				this.key = (DsaPrivateKeyParameters)parameters;
			}
			else
			{
				if (!(parameters is DsaPublicKeyParameters))
				{
					throw new InvalidKeyException("DSA public key required for verification");
				}
				this.key = (DsaPublicKeyParameters)parameters;
			}
			this.random = this.InitSecureRandom(forSigning && !this.kCalculator.IsDeterministic, provided);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x0012069B File Offset: 0x0011E89B
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.Q;
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x001206B0 File Offset: 0x0011E8B0
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.CalculateE(q, message);
			BigInteger x = ((DsaPrivateKeyParameters)this.key).X;
			if (this.kCalculator.IsDeterministic)
			{
				this.kCalculator.Init(q, x, message);
			}
			else
			{
				this.kCalculator.Init(q, this.random);
			}
			BigInteger bigInteger2 = this.kCalculator.NextK();
			BigInteger bigInteger3 = parameters.G.ModPow(bigInteger2, parameters.P).Mod(q);
			bigInteger2 = bigInteger2.ModInverse(q).Multiply(bigInteger.Add(x.Multiply(bigInteger3)));
			BigInteger bigInteger4 = bigInteger2.Mod(q);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00120778 File Offset: 0x0011E978
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.CalculateE(q, message);
			if (r.SignValue <= 0 || q.CompareTo(r) <= 0)
			{
				return false;
			}
			if (s.SignValue <= 0 || q.CompareTo(s) <= 0)
			{
				return false;
			}
			BigInteger val = s.ModInverse(q);
			BigInteger bigInteger2 = bigInteger.Multiply(val).Mod(q);
			BigInteger bigInteger3 = r.Multiply(val).Mod(q);
			BigInteger p = parameters.P;
			bigInteger2 = parameters.G.ModPow(bigInteger2, p);
			bigInteger3 = ((DsaPublicKeyParameters)this.key).Y.ModPow(bigInteger3, p);
			return bigInteger2.Multiply(bigInteger3).Mod(p).Mod(q).Equals(r);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x00120844 File Offset: 0x0011EA44
		protected virtual BigInteger CalculateE(BigInteger n, byte[] message)
		{
			int length = Math.Min(message.Length, n.BitLength / 8);
			return new BigInteger(1, message, 0, length);
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0012086B File Offset: 0x0011EA6B
		protected virtual SecureRandom InitSecureRandom(bool needed, SecureRandom provided)
		{
			if (!needed)
			{
				return null;
			}
			if (provided == null)
			{
				return new SecureRandom();
			}
			return provided;
		}

		// Token: 0x04001D88 RID: 7560
		protected readonly IDsaKCalculator kCalculator;

		// Token: 0x04001D89 RID: 7561
		protected DsaKeyParameters key;

		// Token: 0x04001D8A RID: 7562
		protected SecureRandom random;
	}
}
