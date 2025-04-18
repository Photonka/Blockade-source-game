﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000539 RID: 1337
	public class ECKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032EB RID: 13035 RVA: 0x00135A5F File Offset: 0x00133C5F
		public ECKeyPairGenerator() : this("EC")
		{
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x00135A6C File Offset: 0x00133C6C
		public ECKeyPairGenerator(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x00135A90 File Offset: 0x00133C90
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is ECKeyGenerationParameters)
			{
				ECKeyGenerationParameters eckeyGenerationParameters = (ECKeyGenerationParameters)parameters;
				this.publicKeyParamSet = eckeyGenerationParameters.PublicKeyParamSet;
				this.parameters = eckeyGenerationParameters.DomainParameters;
			}
			else
			{
				int strength = parameters.Strength;
				DerObjectIdentifier oid;
				if (strength <= 239)
				{
					if (strength == 192)
					{
						oid = X9ObjectIdentifiers.Prime192v1;
						goto IL_AA;
					}
					if (strength == 224)
					{
						oid = SecObjectIdentifiers.SecP224r1;
						goto IL_AA;
					}
					if (strength == 239)
					{
						oid = X9ObjectIdentifiers.Prime239v1;
						goto IL_AA;
					}
				}
				else
				{
					if (strength == 256)
					{
						oid = X9ObjectIdentifiers.Prime256v1;
						goto IL_AA;
					}
					if (strength == 384)
					{
						oid = SecObjectIdentifiers.SecP384r1;
						goto IL_AA;
					}
					if (strength == 521)
					{
						oid = SecObjectIdentifiers.SecP521r1;
						goto IL_AA;
					}
				}
				throw new InvalidParameterException("unknown key size.");
				IL_AA:
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(oid);
				this.publicKeyParamSet = oid;
				this.parameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			this.random = parameters.Random;
			if (this.random == null)
			{
				this.random = new SecureRandom();
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x00135BA0 File Offset: 0x00133DA0
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			BigInteger n = this.parameters.N;
			int num = n.BitLength >> 2;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(n.BitLength, this.random);
			}
			while (bigInteger.CompareTo(BigInteger.Two) < 0 || bigInteger.CompareTo(n) >= 0 || WNafUtilities.GetNafWeight(bigInteger) < num);
			ECPoint q = this.CreateBasePointMultiplier().Multiply(this.parameters.G, bigInteger);
			if (this.publicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.publicKeyParamSet), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.publicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.parameters), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.parameters));
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x00135C68 File Offset: 0x00133E68
		internal static X9ECParameters FindECCurveByOid(DerObjectIdentifier oid)
		{
			X9ECParameters byOid = CustomNamedCurves.GetByOid(oid);
			if (byOid == null)
			{
				byOid = ECNamedCurveTable.GetByOid(oid);
			}
			return byOid;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x00135C88 File Offset: 0x00133E88
		internal static ECPublicKeyParameters GetCorrespondingPublicKey(ECPrivateKeyParameters privKey)
		{
			ECDomainParameters ecdomainParameters = privKey.Parameters;
			ECPoint q = new FixedPointCombMultiplier().Multiply(ecdomainParameters.G, privKey.D);
			if (privKey.PublicKeyParamSet != null)
			{
				return new ECPublicKeyParameters(privKey.AlgorithmName, q, privKey.PublicKeyParamSet);
			}
			return new ECPublicKeyParameters(privKey.AlgorithmName, q, ecdomainParameters);
		}

		// Token: 0x04002067 RID: 8295
		private readonly string algorithm;

		// Token: 0x04002068 RID: 8296
		private ECDomainParameters parameters;

		// Token: 0x04002069 RID: 8297
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x0400206A RID: 8298
		private SecureRandom random;
	}
}
