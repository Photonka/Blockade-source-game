using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.EdEC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C3 RID: 707
	public sealed class AgreementUtilities
	{
		// Token: 0x06001A34 RID: 6708 RVA: 0x00023EF4 File Offset: 0x000220F4
		private AgreementUtilities()
		{
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000C94A4 File Offset: 0x000C76A4
		static AgreementUtilities()
		{
			AgreementUtilities.algorithms[X9ObjectIdentifiers.DHSinglePassCofactorDHSha1KdfScheme.Id] = "ECCDHWITHSHA1KDF";
			AgreementUtilities.algorithms[X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id] = "ECDHWITHSHA1KDF";
			AgreementUtilities.algorithms[X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id] = "ECMQVWITHSHA1KDF";
			AgreementUtilities.algorithms[EdECObjectIdentifiers.id_X25519.Id] = "X25519";
			AgreementUtilities.algorithms[EdECObjectIdentifiers.id_X448.Id] = "X448";
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000C9538 File Offset: 0x000C7738
		public static IBasicAgreement GetBasicAgreement(DerObjectIdentifier oid)
		{
			return AgreementUtilities.GetBasicAgreement(oid.Id);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000C9548 File Offset: 0x000C7748
		public static IBasicAgreement GetBasicAgreement(string algorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(algorithm);
			if (mechanism == "DH" || mechanism == "DIFFIEHELLMAN")
			{
				return new DHBasicAgreement();
			}
			if (mechanism == "ECDH")
			{
				return new ECDHBasicAgreement();
			}
			if (mechanism == "ECDHC" || mechanism == "ECCDH")
			{
				return new ECDHCBasicAgreement();
			}
			if (mechanism == "ECMQV")
			{
				return new ECMqvBasicAgreement();
			}
			throw new SecurityUtilityException("Basic Agreement " + algorithm + " not recognised.");
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000C95D7 File Offset: 0x000C77D7
		public static IBasicAgreement GetBasicAgreementWithKdf(DerObjectIdentifier oid, string wrapAlgorithm)
		{
			return AgreementUtilities.GetBasicAgreementWithKdf(oid.Id, wrapAlgorithm);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000C95E8 File Offset: 0x000C77E8
		public static IBasicAgreement GetBasicAgreementWithKdf(string agreeAlgorithm, string wrapAlgorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(agreeAlgorithm);
			if (mechanism == "DHWITHSHA1KDF" || mechanism == "ECDHWITHSHA1KDF")
			{
				return new ECDHWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
			}
			if (mechanism == "ECMQVWITHSHA1KDF")
			{
				return new ECMqvWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
			}
			throw new SecurityUtilityException("Basic Agreement (with KDF) " + agreeAlgorithm + " not recognised.");
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000C965A File Offset: 0x000C785A
		public static IRawAgreement GetRawAgreement(DerObjectIdentifier oid)
		{
			return AgreementUtilities.GetRawAgreement(oid.Id);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000C9668 File Offset: 0x000C7868
		public static IRawAgreement GetRawAgreement(string algorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(algorithm);
			if (mechanism == "X25519")
			{
				return new X25519Agreement();
			}
			if (mechanism == "X448")
			{
				return new X448Agreement();
			}
			throw new SecurityUtilityException("Raw Agreement " + algorithm + " not recognised.");
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000C96B7 File Offset: 0x000C78B7
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)AgreementUtilities.algorithms[oid.Id];
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000C96D0 File Offset: 0x000C78D0
		private static string GetMechanism(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)AgreementUtilities.algorithms[text];
			if (text2 != null)
			{
				return text2;
			}
			return text;
		}

		// Token: 0x040017A0 RID: 6048
		private static readonly IDictionary algorithms = Platform.CreateHashtable();
	}
}
