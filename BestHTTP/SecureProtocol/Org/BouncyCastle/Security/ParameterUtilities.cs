using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Kisa;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ntt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002CE RID: 718
	public sealed class ParameterUtilities
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x00023EF4 File Offset: 0x000220F4
		private ParameterUtilities()
		{
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000CCE54 File Offset: 0x000CB054
		static ParameterUtilities()
		{
			ParameterUtilities.AddAlgorithm("AES", new object[]
			{
				"AESWRAP"
			});
			ParameterUtilities.AddAlgorithm("AES128", new object[]
			{
				"2.16.840.1.101.3.4.2",
				NistObjectIdentifiers.IdAes128Cbc,
				NistObjectIdentifiers.IdAes128Cfb,
				NistObjectIdentifiers.IdAes128Ecb,
				NistObjectIdentifiers.IdAes128Ofb,
				NistObjectIdentifiers.IdAes128Wrap
			});
			ParameterUtilities.AddAlgorithm("AES192", new object[]
			{
				"2.16.840.1.101.3.4.22",
				NistObjectIdentifiers.IdAes192Cbc,
				NistObjectIdentifiers.IdAes192Cfb,
				NistObjectIdentifiers.IdAes192Ecb,
				NistObjectIdentifiers.IdAes192Ofb,
				NistObjectIdentifiers.IdAes192Wrap
			});
			ParameterUtilities.AddAlgorithm("AES256", new object[]
			{
				"2.16.840.1.101.3.4.42",
				NistObjectIdentifiers.IdAes256Cbc,
				NistObjectIdentifiers.IdAes256Cfb,
				NistObjectIdentifiers.IdAes256Ecb,
				NistObjectIdentifiers.IdAes256Ofb,
				NistObjectIdentifiers.IdAes256Wrap
			});
			ParameterUtilities.AddAlgorithm("BLOWFISH", new object[]
			{
				"1.3.6.1.4.1.3029.1.2"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA", new object[]
			{
				"CAMELLIAWRAP"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA128", new object[]
			{
				NttObjectIdentifiers.IdCamellia128Cbc,
				NttObjectIdentifiers.IdCamellia128Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA192", new object[]
			{
				NttObjectIdentifiers.IdCamellia192Cbc,
				NttObjectIdentifiers.IdCamellia192Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA256", new object[]
			{
				NttObjectIdentifiers.IdCamellia256Cbc,
				NttObjectIdentifiers.IdCamellia256Wrap
			});
			ParameterUtilities.AddAlgorithm("CAST5", new object[]
			{
				"1.2.840.113533.7.66.10"
			});
			ParameterUtilities.AddAlgorithm("CAST6", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("DES", new object[]
			{
				OiwObjectIdentifiers.DesCbc,
				OiwObjectIdentifiers.DesCfb,
				OiwObjectIdentifiers.DesEcb,
				OiwObjectIdentifiers.DesOfb
			});
			ParameterUtilities.AddAlgorithm("DESEDE", new object[]
			{
				"DESEDEWRAP",
				"TDEA",
				OiwObjectIdentifiers.DesEde,
				PkcsObjectIdentifiers.IdAlgCms3DesWrap
			});
			ParameterUtilities.AddAlgorithm("DESEDE3", new object[]
			{
				PkcsObjectIdentifiers.DesEde3Cbc
			});
			ParameterUtilities.AddAlgorithm("GOST28147", new object[]
			{
				"GOST",
				"GOST-28147",
				CryptoProObjectIdentifiers.GostR28147Cbc
			});
			ParameterUtilities.AddAlgorithm("HC128", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("HC256", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("IDEA", new object[]
			{
				"1.3.6.1.4.1.188.7.1.1.2"
			});
			ParameterUtilities.AddAlgorithm("NOEKEON", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("RC2", new object[]
			{
				PkcsObjectIdentifiers.RC2Cbc,
				PkcsObjectIdentifiers.IdAlgCmsRC2Wrap
			});
			ParameterUtilities.AddAlgorithm("RC4", new object[]
			{
				"ARC4",
				"1.2.840.113549.3.4"
			});
			ParameterUtilities.AddAlgorithm("RC5", new object[]
			{
				"RC5-32"
			});
			ParameterUtilities.AddAlgorithm("RC5-64", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("RC6", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("RIJNDAEL", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("SALSA20", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("SEED", new object[]
			{
				KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap,
				KisaObjectIdentifiers.IdSeedCbc
			});
			ParameterUtilities.AddAlgorithm("SERPENT", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("SKIPJACK", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("SM4", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("TEA", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("THREEFISH-256", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("THREEFISH-512", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("THREEFISH-1024", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("TNEPRES", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("TWOFISH", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("VMPC", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("VMPC-KSA3", Array.Empty<object>());
			ParameterUtilities.AddAlgorithm("XTEA", Array.Empty<object>());
			ParameterUtilities.AddBasicIVSizeEntries(8, new string[]
			{
				"BLOWFISH",
				"DES",
				"DESEDE",
				"DESEDE3"
			});
			ParameterUtilities.AddBasicIVSizeEntries(16, new string[]
			{
				"AES",
				"AES128",
				"AES192",
				"AES256",
				"CAMELLIA",
				"CAMELLIA128",
				"CAMELLIA192",
				"CAMELLIA256",
				"NOEKEON",
				"SEED",
				"SM4"
			});
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000CD2E8 File Offset: 0x000CB4E8
		private static void AddAlgorithm(string canonicalName, params object[] aliases)
		{
			ParameterUtilities.algorithms[canonicalName] = canonicalName;
			foreach (object obj in aliases)
			{
				ParameterUtilities.algorithms[obj.ToString()] = canonicalName;
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000CD328 File Offset: 0x000CB528
		private static void AddBasicIVSizeEntries(int size, params string[] algorithms)
		{
			foreach (string key in algorithms)
			{
				ParameterUtilities.basicIVSizes.Add(key, size);
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000CD35A File Offset: 0x000CB55A
		public static string GetCanonicalAlgorithmName(string algorithm)
		{
			return (string)ParameterUtilities.algorithms[Platform.ToUpperInvariant(algorithm)];
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000CD371 File Offset: 0x000CB571
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000CD383 File Offset: 0x000CB583
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000CD390 File Offset: 0x000CB590
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes, int offset, int length)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, offset, length);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000CD3A0 File Offset: 0x000CB5A0
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes, int offset, int length)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			if (canonicalAlgorithmName == "DES")
			{
				return new DesParameters(keyBytes, offset, length);
			}
			if (canonicalAlgorithmName == "DESEDE" || canonicalAlgorithmName == "DESEDE3")
			{
				return new DesEdeParameters(keyBytes, offset, length);
			}
			if (canonicalAlgorithmName == "RC2")
			{
				return new RC2Parameters(keyBytes, offset, length);
			}
			return new KeyParameter(keyBytes, offset, length);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000CD432 File Offset: 0x000CB632
		public static ICipherParameters GetCipherParameters(DerObjectIdentifier algOid, ICipherParameters key, Asn1Object asn1Params)
		{
			return ParameterUtilities.GetCipherParameters(algOid.Id, key, asn1Params);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000CD444 File Offset: 0x000CB644
		public static ICipherParameters GetCipherParameters(string algorithm, ICipherParameters key, Asn1Object asn1Params)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			byte[] array = null;
			try
			{
				if (ParameterUtilities.FindBasicIVSize(canonicalAlgorithmName) != -1 || canonicalAlgorithmName == "RIJNDAEL" || canonicalAlgorithmName == "SKIPJACK" || canonicalAlgorithmName == "TWOFISH")
				{
					array = ((Asn1OctetString)asn1Params).GetOctets();
				}
				else if (canonicalAlgorithmName == "CAST5")
				{
					array = Cast5CbcParameters.GetInstance(asn1Params).GetIV();
				}
				else if (canonicalAlgorithmName == "IDEA")
				{
					array = IdeaCbcPar.GetInstance(asn1Params).GetIV();
				}
				else if (canonicalAlgorithmName == "RC2")
				{
					array = RC2CbcParameter.GetInstance(asn1Params).GetIV();
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Could not process ASN.1 parameters", innerException);
			}
			if (array != null)
			{
				return new ParametersWithIV(key, array);
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000CD550 File Offset: 0x000CB750
		public static Asn1Encodable GenerateParameters(DerObjectIdentifier algID, SecureRandom random)
		{
			return ParameterUtilities.GenerateParameters(algID.Id, random);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000CD560 File Offset: 0x000CB760
		public static Asn1Encodable GenerateParameters(string algorithm, SecureRandom random)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			int num = ParameterUtilities.FindBasicIVSize(canonicalAlgorithmName);
			if (num != -1)
			{
				return ParameterUtilities.CreateIVOctetString(random, num);
			}
			if (canonicalAlgorithmName == "CAST5")
			{
				return new Cast5CbcParameters(ParameterUtilities.CreateIV(random, 8), 128);
			}
			if (canonicalAlgorithmName == "IDEA")
			{
				return new IdeaCbcPar(ParameterUtilities.CreateIV(random, 8));
			}
			if (canonicalAlgorithmName == "RC2")
			{
				return new RC2CbcParameter(ParameterUtilities.CreateIV(random, 8));
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000CD616 File Offset: 0x000CB816
		public static ICipherParameters WithRandom(ICipherParameters cp, SecureRandom random)
		{
			if (random != null)
			{
				cp = new ParametersWithRandom(cp, random);
			}
			return cp;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000CD625 File Offset: 0x000CB825
		private static Asn1OctetString CreateIVOctetString(SecureRandom random, int ivLength)
		{
			return new DerOctetString(ParameterUtilities.CreateIV(random, ivLength));
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000CD634 File Offset: 0x000CB834
		private static byte[] CreateIV(SecureRandom random, int ivLength)
		{
			byte[] array = new byte[ivLength];
			random.NextBytes(array);
			return array;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000CD650 File Offset: 0x000CB850
		private static int FindBasicIVSize(string canonicalName)
		{
			if (!ParameterUtilities.basicIVSizes.Contains(canonicalName))
			{
				return -1;
			}
			return (int)ParameterUtilities.basicIVSizes[canonicalName];
		}

		// Token: 0x040017A9 RID: 6057
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x040017AA RID: 6058
		private static readonly IDictionary basicIVSizes = Platform.CreateHashtable();
	}
}
