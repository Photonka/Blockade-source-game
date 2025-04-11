using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BD RID: 1213
	public abstract class ECKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F73 RID: 12147 RVA: 0x00127E38 File Offset: 0x00126038
		protected ECKeyParameters(string algorithm, bool isPrivate, ECDomainParameters parameters) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
			this.parameters = parameters;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00127E70 File Offset: 0x00126070
		protected ECKeyParameters(string algorithm, bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
			this.parameters = ECKeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x00127EBF File Offset: 0x001260BF
		public string AlgorithmName
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x00127EC7 File Offset: 0x001260C7
		public ECDomainParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x00127ECF File Offset: 0x001260CF
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x00127ED8 File Offset: 0x001260D8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x00127EFE File Offset: 0x001260FE
		protected bool Equals(ECKeyParameters other)
		{
			return this.parameters.Equals(other.parameters) && base.Equals(other);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00127F1C File Offset: 0x0012611C
		public override int GetHashCode()
		{
			return this.parameters.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x00127F30 File Offset: 0x00126130
		internal ECKeyGenerationParameters CreateKeyGenerationParameters(SecureRandom random)
		{
			if (this.publicKeyParamSet != null)
			{
				return new ECKeyGenerationParameters(this.publicKeyParamSet, random);
			}
			return new ECKeyGenerationParameters(this.parameters, random);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x00127F53 File Offset: 0x00126153
		internal static string VerifyAlgorithmName(string algorithm)
		{
			string result = Platform.ToUpperInvariant(algorithm);
			if (Array.IndexOf<string>(ECKeyParameters.algorithms, algorithm, 0, ECKeyParameters.algorithms.Length) < 0)
			{
				throw new ArgumentException("unrecognised algorithm: " + algorithm, "algorithm");
			}
			return result;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x00127F88 File Offset: 0x00126188
		internal static ECDomainParameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			ECDomainParameters ecdomainParameters = ECGost3410NamedCurves.GetByOid(publicKeyParamSet);
			if (ecdomainParameters == null)
			{
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(publicKeyParamSet);
				if (x9ECParameters == null)
				{
					throw new ArgumentException("OID is not a valid public key parameter set", "publicKeyParamSet");
				}
				ecdomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			return ecdomainParameters;
		}

		// Token: 0x04001E9D RID: 7837
		private static readonly string[] algorithms = new string[]
		{
			"EC",
			"ECDSA",
			"ECDH",
			"ECDHC",
			"ECGOST3410",
			"ECMQV"
		};

		// Token: 0x04001E9E RID: 7838
		private readonly string algorithm;

		// Token: 0x04001E9F RID: 7839
		private readonly ECDomainParameters parameters;

		// Token: 0x04001EA0 RID: 7840
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
