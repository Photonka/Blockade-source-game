using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005AD RID: 1453
	public class DHAgreement
	{
		// Token: 0x06003856 RID: 14422 RVA: 0x00165808 File Offset: 0x00163A08
		public void Init(ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (!(asymmetricKeyParameter is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)asymmetricKeyParameter;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x00165880 File Offset: 0x00163A80
		public BigInteger CalculateMessage()
		{
			DHKeyPairGenerator dhkeyPairGenerator = new DHKeyPairGenerator();
			dhkeyPairGenerator.Init(new DHKeyGenerationParameters(this.random, this.dhParams));
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = dhkeyPairGenerator.GenerateKeyPair();
			this.privateValue = ((DHPrivateKeyParameters)asymmetricCipherKeyPair.Private).X;
			return ((DHPublicKeyParameters)asymmetricCipherKeyPair.Public).Y;
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x001658D8 File Offset: 0x00163AD8
		public BigInteger CalculateAgreement(DHPublicKeyParameters pub, BigInteger message)
		{
			if (pub == null)
			{
				throw new ArgumentNullException("pub");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (!pub.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			BigInteger p = this.dhParams.P;
			BigInteger y = pub.Y;
			if (y == null || y.CompareTo(BigInteger.One) <= 0 || y.CompareTo(p.Subtract(BigInteger.One)) >= 0)
			{
				throw new ArgumentException("Diffie-Hellman public key is weak");
			}
			BigInteger bigInteger = y.ModPow(this.privateValue, p);
			if (bigInteger.Equals(BigInteger.One))
			{
				throw new InvalidOperationException("Shared key can't be 1");
			}
			return message.ModPow(this.key.X, p).Multiply(bigInteger).Mod(p);
		}

		// Token: 0x040023BF RID: 9151
		private DHPrivateKeyParameters key;

		// Token: 0x040023C0 RID: 9152
		private DHParameters dhParams;

		// Token: 0x040023C1 RID: 9153
		private BigInteger privateValue;

		// Token: 0x040023C2 RID: 9154
		private SecureRandom random;
	}
}
