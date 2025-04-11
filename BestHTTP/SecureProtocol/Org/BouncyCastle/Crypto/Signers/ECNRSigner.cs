using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000480 RID: 1152
	public class ECNRSigner : IDsaExt, IDsa
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x00120E9A File Offset: 0x0011F09A
		public virtual string AlgorithmName
		{
			get
			{
				return "ECNR";
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00120EA4 File Offset: 0x0011F0A4
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					this.random = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				else
				{
					this.random = new SecureRandom();
				}
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x00120F2A File Offset: 0x0011F12A
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00120F3C File Offset: 0x0011F13C
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("not initialised for signing");
			}
			BigInteger order = this.Order;
			int bitLength = order.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			int bitLength2 = bigInteger.BitLength;
			ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)this.key;
			if (bitLength2 > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			AsymmetricCipherKeyPair asymmetricCipherKeyPair;
			BigInteger bigInteger2;
			do
			{
				ECKeyPairGenerator eckeyPairGenerator = new ECKeyPairGenerator();
				eckeyPairGenerator.Init(new ECKeyGenerationParameters(ecprivateKeyParameters.Parameters, this.random));
				asymmetricCipherKeyPair = eckeyPairGenerator.GenerateKeyPair();
				bigInteger2 = ((ECPublicKeyParameters)asymmetricCipherKeyPair.Public).Q.AffineXCoord.ToBigInteger().Add(bigInteger).Mod(order);
			}
			while (bigInteger2.SignValue == 0);
			BigInteger d = ecprivateKeyParameters.D;
			BigInteger bigInteger3 = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D.Subtract(bigInteger2.Multiply(d)).Mod(order);
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger3
			};
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0012102C File Offset: 0x0011F22C
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("not initialised for verifying");
			}
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)this.key;
			BigInteger n = ecpublicKeyParameters.Parameters.N;
			int bitLength = n.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			if (bigInteger.BitLength > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.Zero) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			ECPoint g = ecpublicKeyParameters.Parameters.G;
			ECPoint q = ecpublicKeyParameters.Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, s, q, r).Normalize();
			if (ecpoint.IsInfinity)
			{
				return false;
			}
			BigInteger n2 = ecpoint.AffineXCoord.ToBigInteger();
			return r.Subtract(n2).Mod(n).Equals(bigInteger);
		}

		// Token: 0x04001D91 RID: 7569
		private bool forSigning;

		// Token: 0x04001D92 RID: 7570
		private ECKeyParameters key;

		// Token: 0x04001D93 RID: 7571
		private SecureRandom random;
	}
}
