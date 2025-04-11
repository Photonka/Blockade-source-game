using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Kisa;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ntt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C4 RID: 708
	public sealed class CipherUtilities
	{
		// Token: 0x06001A3E RID: 6718 RVA: 0x000C96FC File Offset: 0x000C78FC
		static CipherUtilities()
		{
			((CipherUtilities.CipherAlgorithm)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherAlgorithm))).ToString();
			((CipherUtilities.CipherMode)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherMode))).ToString();
			((CipherUtilities.CipherPadding)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherPadding))).ToString();
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1PADDING"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RsaEncryption.Id] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.IdRsaesOaep.Id] = "RSA//OAEPPADDING";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCbc.Id] = "DES/CBC";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCfb.Id] = "DES/CFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEcb.Id] = "DES/ECB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesOfb.Id] = "DES/OFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEde.Id] = "DESEDE";
			CipherUtilities.algorithms["TDEA"] = "DESEDE";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.DesEde3Cbc.Id] = "DESEDE/CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RC2Cbc.Id] = "RC2/CBC";
			CipherUtilities.algorithms["1.3.6.1.4.1.188.7.1.1.2"] = "IDEA/CBC";
			CipherUtilities.algorithms["1.2.840.113533.7.66.10"] = "CAST5/CBC";
			CipherUtilities.algorithms["RC4"] = "ARC4";
			CipherUtilities.algorithms["ARCFOUR"] = "ARC4";
			CipherUtilities.algorithms["1.2.840.113549.3.4"] = "ARC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC4"] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC4.Id] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC4"] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd40BitRC4.Id] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDES"] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndDesCbc.Id] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDRC2"] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndRC2Cbc.Id] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND3-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHAAND3KEYTRIPLEDES"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc.Id] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDESEDE"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND2-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd2KeyTripleDesCbc.Id] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC2-CBC"] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC2Cbc.Id] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC2-CBC"] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc.Id] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND128BITAES-CBC-BC"] = "PBEWITHSHA256AND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND192BITAES-CBC-BC"] = "PBEWITHSHA256AND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND256BITAES-CBC-BC"] = "PBEWITHSHA256AND256BITAES-CBC-BC";
			CipherUtilities.algorithms["GOST"] = "GOST28147";
			CipherUtilities.algorithms["GOST-28147"] = "GOST28147";
			CipherUtilities.algorithms[CryptoProObjectIdentifiers.GostR28147Cbc.Id] = "GOST28147/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["RC5-32"] = "RC5";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[KisaObjectIdentifiers.IdSeedCbc.Id] = "SEED/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["1.3.6.1.4.1.3029.1.2"] = "BLOWFISH/CBC";
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00023EF4 File Offset: 0x000220F4
		private CipherUtilities()
		{
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000C9D94 File Offset: 0x000C7F94
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = Platform.ToUpperInvariant(mechanism);
			string text = (string)CipherUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)CipherUtilities.oids[mechanism];
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x000C9DDE File Offset: 0x000C7FDE
		public static ICollection Algorithms
		{
			get
			{
				return CipherUtilities.oids.Keys;
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000C9DEA File Offset: 0x000C7FEA
		public static IBufferedCipher GetCipher(DerObjectIdentifier oid)
		{
			return CipherUtilities.GetCipher(oid.Id);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000C9DF8 File Offset: 0x000C7FF8
		public static IBufferedCipher GetCipher(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			algorithm = Platform.ToUpperInvariant(algorithm);
			string text = (string)CipherUtilities.algorithms[algorithm];
			if (text != null)
			{
				algorithm = text;
			}
			IBasicAgreement basicAgreement = null;
			if (algorithm == "IES")
			{
				basicAgreement = new DHBasicAgreement();
			}
			else if (algorithm == "ECIES")
			{
				basicAgreement = new ECDHBasicAgreement();
			}
			if (basicAgreement != null)
			{
				return new BufferedIesCipher(new IesEngine(basicAgreement, new Kdf2BytesGenerator(new Sha1Digest()), new HMac(new Sha1Digest())));
			}
			if (Platform.StartsWith(algorithm, "PBE"))
			{
				if (Platform.EndsWith(algorithm, "-CBC"))
				{
					if (algorithm == "PBEWITHSHA1ANDDES-CBC")
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEngine()));
					}
					if (algorithm == "PBEWITHSHA1ANDRC2-CBC")
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					}
					if (Strings.IsOneOf(algorithm, new string[]
					{
						"PBEWITHSHAAND2-KEYTRIPLEDES-CBC",
						"PBEWITHSHAAND3-KEYTRIPLEDES-CBC"
					}))
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEdeEngine()));
					}
					if (Strings.IsOneOf(algorithm, new string[]
					{
						"PBEWITHSHAAND128BITRC2-CBC",
						"PBEWITHSHAAND40BITRC2-CBC"
					}))
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					}
				}
				else if ((Platform.EndsWith(algorithm, "-BC") || Platform.EndsWith(algorithm, "-OPENSSL")) && Strings.IsOneOf(algorithm, new string[]
				{
					"PBEWITHSHAAND128BITAES-CBC-BC",
					"PBEWITHSHAAND192BITAES-CBC-BC",
					"PBEWITHSHAAND256BITAES-CBC-BC",
					"PBEWITHSHA256AND128BITAES-CBC-BC",
					"PBEWITHSHA256AND192BITAES-CBC-BC",
					"PBEWITHSHA256AND256BITAES-CBC-BC",
					"PBEWITHMD5AND128BITAES-CBC-OPENSSL",
					"PBEWITHMD5AND192BITAES-CBC-OPENSSL",
					"PBEWITHMD5AND256BITAES-CBC-OPENSSL"
				}))
				{
					return new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
				}
			}
			string[] array = algorithm.Split(new char[]
			{
				'/'
			});
			IBlockCipher blockCipher = null;
			IAsymmetricBlockCipher asymmetricBlockCipher = null;
			IStreamCipher streamCipher = null;
			string text2 = array[0];
			string text3 = (string)CipherUtilities.algorithms[text2];
			if (text3 != null)
			{
				text2 = text3;
			}
			CipherUtilities.CipherAlgorithm cipherAlgorithm;
			try
			{
				cipherAlgorithm = (CipherUtilities.CipherAlgorithm)Enums.GetEnumValue(typeof(CipherUtilities.CipherAlgorithm), text2);
			}
			catch (ArgumentException)
			{
				throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
			}
			switch (cipherAlgorithm)
			{
			case CipherUtilities.CipherAlgorithm.AES:
				blockCipher = new AesEngine();
				break;
			case CipherUtilities.CipherAlgorithm.ARC4:
				streamCipher = new RC4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.BLOWFISH:
				blockCipher = new BlowfishEngine();
				break;
			case CipherUtilities.CipherAlgorithm.CAMELLIA:
				blockCipher = new CamelliaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.CAST5:
				blockCipher = new Cast5Engine();
				break;
			case CipherUtilities.CipherAlgorithm.CAST6:
				blockCipher = new Cast6Engine();
				break;
			case CipherUtilities.CipherAlgorithm.DES:
				blockCipher = new DesEngine();
				break;
			case CipherUtilities.CipherAlgorithm.DESEDE:
				blockCipher = new DesEdeEngine();
				break;
			case CipherUtilities.CipherAlgorithm.ELGAMAL:
				asymmetricBlockCipher = new ElGamalEngine();
				break;
			case CipherUtilities.CipherAlgorithm.GOST28147:
				blockCipher = new Gost28147Engine();
				break;
			case CipherUtilities.CipherAlgorithm.HC128:
				streamCipher = new HC128Engine();
				break;
			case CipherUtilities.CipherAlgorithm.HC256:
				streamCipher = new HC256Engine();
				break;
			case CipherUtilities.CipherAlgorithm.IDEA:
				blockCipher = new IdeaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.NOEKEON:
				blockCipher = new NoekeonEngine();
				break;
			case CipherUtilities.CipherAlgorithm.PBEWITHSHAAND128BITRC4:
			case CipherUtilities.CipherAlgorithm.PBEWITHSHAAND40BITRC4:
				streamCipher = new RC4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC2:
				blockCipher = new RC2Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC5:
				blockCipher = new RC532Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC5_64:
				blockCipher = new RC564Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC6:
				blockCipher = new RC6Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RIJNDAEL:
				blockCipher = new RijndaelEngine();
				break;
			case CipherUtilities.CipherAlgorithm.RSA:
				asymmetricBlockCipher = new RsaBlindedEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SALSA20:
				streamCipher = new Salsa20Engine();
				break;
			case CipherUtilities.CipherAlgorithm.SEED:
				blockCipher = new SeedEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SERPENT:
				blockCipher = new SerpentEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SKIPJACK:
				blockCipher = new SkipjackEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SM4:
				blockCipher = new SM4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.TEA:
				blockCipher = new TeaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_256:
				blockCipher = new ThreefishEngine(256);
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_512:
				blockCipher = new ThreefishEngine(512);
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_1024:
				blockCipher = new ThreefishEngine(1024);
				break;
			case CipherUtilities.CipherAlgorithm.TNEPRES:
				blockCipher = new TnepresEngine();
				break;
			case CipherUtilities.CipherAlgorithm.TWOFISH:
				blockCipher = new TwofishEngine();
				break;
			case CipherUtilities.CipherAlgorithm.VMPC:
				streamCipher = new VmpcEngine();
				break;
			case CipherUtilities.CipherAlgorithm.VMPC_KSA3:
				streamCipher = new VmpcKsa3Engine();
				break;
			case CipherUtilities.CipherAlgorithm.XTEA:
				blockCipher = new XteaEngine();
				break;
			default:
				throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
			}
			if (streamCipher != null)
			{
				if (array.Length > 1)
				{
					throw new ArgumentException("Modes and paddings not used for stream ciphers");
				}
				return new BufferedStreamCipher(streamCipher);
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				IBlockCipherPadding blockCipherPadding = null;
				IAeadBlockCipher aeadBlockCipher = null;
				if (array.Length > 2)
				{
					if (streamCipher != null)
					{
						throw new ArgumentException("Paddings not used for stream ciphers");
					}
					string text4 = array[2];
					CipherUtilities.CipherPadding cipherPadding;
					if (text4 == "")
					{
						cipherPadding = CipherUtilities.CipherPadding.RAW;
					}
					else if (text4 == "X9.23PADDING")
					{
						cipherPadding = CipherUtilities.CipherPadding.X923PADDING;
					}
					else
					{
						try
						{
							cipherPadding = (CipherUtilities.CipherPadding)Enums.GetEnumValue(typeof(CipherUtilities.CipherPadding), text4);
						}
						catch (ArgumentException)
						{
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
					}
					switch (cipherPadding)
					{
					case CipherUtilities.CipherPadding.NOPADDING:
						flag2 = false;
						break;
					case CipherUtilities.CipherPadding.RAW:
						break;
					case CipherUtilities.CipherPadding.ISO10126PADDING:
					case CipherUtilities.CipherPadding.ISO10126D2PADDING:
					case CipherUtilities.CipherPadding.ISO10126_2PADDING:
						blockCipherPadding = new ISO10126d2Padding();
						break;
					case CipherUtilities.CipherPadding.ISO7816_4PADDING:
					case CipherUtilities.CipherPadding.ISO9797_1PADDING:
						blockCipherPadding = new ISO7816d4Padding();
						break;
					case CipherUtilities.CipherPadding.ISO9796_1:
					case CipherUtilities.CipherPadding.ISO9796_1PADDING:
						asymmetricBlockCipher = new ISO9796d1Encoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.OAEP:
					case CipherUtilities.CipherPadding.OAEPPADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.OAEPWITHMD5ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new MD5Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA1ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_1ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha1Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA224ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_224ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha224Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA256ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_256ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha256Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA384ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_384ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha384Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA512ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_512ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha512Digest());
						break;
					case CipherUtilities.CipherPadding.PKCS1:
					case CipherUtilities.CipherPadding.PKCS1PADDING:
						asymmetricBlockCipher = new Pkcs1Encoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.PKCS5:
					case CipherUtilities.CipherPadding.PKCS5PADDING:
					case CipherUtilities.CipherPadding.PKCS7:
					case CipherUtilities.CipherPadding.PKCS7PADDING:
						blockCipherPadding = new Pkcs7Padding();
						break;
					case CipherUtilities.CipherPadding.TBCPADDING:
						blockCipherPadding = new TbcPadding();
						break;
					case CipherUtilities.CipherPadding.WITHCTS:
						flag = true;
						break;
					case CipherUtilities.CipherPadding.X923PADDING:
						blockCipherPadding = new X923Padding();
						break;
					case CipherUtilities.CipherPadding.ZEROBYTEPADDING:
						blockCipherPadding = new ZeroBytePadding();
						break;
					default:
						throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
					}
				}
				if (array.Length > 1)
				{
					string text5 = array[1];
					int digitIndex = CipherUtilities.GetDigitIndex(text5);
					string text6 = (digitIndex >= 0) ? text5.Substring(0, digitIndex) : text5;
					try
					{
						switch ((text6 == "") ? CipherUtilities.CipherMode.NONE : ((CipherUtilities.CipherMode)Enums.GetEnumValue(typeof(CipherUtilities.CipherMode), text6)))
						{
						case CipherUtilities.CipherMode.ECB:
						case CipherUtilities.CipherMode.NONE:
							break;
						case CipherUtilities.CipherMode.CBC:
							blockCipher = new CbcBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CCM:
							aeadBlockCipher = new CcmBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CFB:
						{
							int bitBlockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text5.Substring(digitIndex));
							blockCipher = new CfbBlockCipher(blockCipher, bitBlockSize);
							break;
						}
						case CipherUtilities.CipherMode.CTR:
							blockCipher = new SicBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CTS:
							flag = true;
							blockCipher = new CbcBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.EAX:
							aeadBlockCipher = new EaxBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.GCM:
							aeadBlockCipher = new GcmBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.GOFB:
							blockCipher = new GOfbBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.OCB:
							aeadBlockCipher = new OcbBlockCipher(blockCipher, CipherUtilities.CreateBlockCipher(cipherAlgorithm));
							break;
						case CipherUtilities.CipherMode.OFB:
						{
							int blockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text5.Substring(digitIndex));
							blockCipher = new OfbBlockCipher(blockCipher, blockSize);
							break;
						}
						case CipherUtilities.CipherMode.OPENPGPCFB:
							blockCipher = new OpenPgpCfbBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.SIC:
							if (blockCipher.GetBlockSize() < 16)
							{
								throw new ArgumentException("Warning: SIC-Mode can become a twotime-pad if the blocksize of the cipher is too small. Use a cipher with a block size of at least 128 bits (e.g. AES)");
							}
							blockCipher = new SicBlockCipher(blockCipher);
							break;
						default:
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
					}
					catch (ArgumentException)
					{
						throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
					}
				}
				if (aeadBlockCipher != null)
				{
					if (flag)
					{
						throw new SecurityUtilityException("CTS mode not valid for AEAD ciphers.");
					}
					if (flag2 && array.Length > 2 && array[2] != "")
					{
						throw new SecurityUtilityException("Bad padding specified for AEAD cipher.");
					}
					return new BufferedAeadBlockCipher(aeadBlockCipher);
				}
				else if (blockCipher != null)
				{
					if (flag)
					{
						return new CtsBlockCipher(blockCipher);
					}
					if (blockCipherPadding != null)
					{
						return new PaddedBufferedBlockCipher(blockCipher, blockCipherPadding);
					}
					if (!flag2 || blockCipher.IsPartialBlockOkay)
					{
						return new BufferedBlockCipher(blockCipher);
					}
					return new PaddedBufferedBlockCipher(blockCipher);
				}
				else
				{
					if (asymmetricBlockCipher != null)
					{
						return new BufferedAsymmetricBlockCipher(asymmetricBlockCipher);
					}
					throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
				}
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000CA6E8 File Offset: 0x000C88E8
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)CipherUtilities.algorithms[oid.Id];
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000CA700 File Offset: 0x000C8900
		private static int GetDigitIndex(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsDigit(s[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000CA730 File Offset: 0x000C8930
		private static IBlockCipher CreateBlockCipher(CipherUtilities.CipherAlgorithm cipherAlgorithm)
		{
			switch (cipherAlgorithm)
			{
			case CipherUtilities.CipherAlgorithm.AES:
				return new AesEngine();
			case CipherUtilities.CipherAlgorithm.BLOWFISH:
				return new BlowfishEngine();
			case CipherUtilities.CipherAlgorithm.CAMELLIA:
				return new CamelliaEngine();
			case CipherUtilities.CipherAlgorithm.CAST5:
				return new Cast5Engine();
			case CipherUtilities.CipherAlgorithm.CAST6:
				return new Cast6Engine();
			case CipherUtilities.CipherAlgorithm.DES:
				return new DesEngine();
			case CipherUtilities.CipherAlgorithm.DESEDE:
				return new DesEdeEngine();
			case CipherUtilities.CipherAlgorithm.GOST28147:
				return new Gost28147Engine();
			case CipherUtilities.CipherAlgorithm.IDEA:
				return new IdeaEngine();
			case CipherUtilities.CipherAlgorithm.NOEKEON:
				return new NoekeonEngine();
			case CipherUtilities.CipherAlgorithm.RC2:
				return new RC2Engine();
			case CipherUtilities.CipherAlgorithm.RC5:
				return new RC532Engine();
			case CipherUtilities.CipherAlgorithm.RC5_64:
				return new RC564Engine();
			case CipherUtilities.CipherAlgorithm.RC6:
				return new RC6Engine();
			case CipherUtilities.CipherAlgorithm.RIJNDAEL:
				return new RijndaelEngine();
			case CipherUtilities.CipherAlgorithm.SEED:
				return new SeedEngine();
			case CipherUtilities.CipherAlgorithm.SERPENT:
				return new SerpentEngine();
			case CipherUtilities.CipherAlgorithm.SKIPJACK:
				return new SkipjackEngine();
			case CipherUtilities.CipherAlgorithm.SM4:
				return new SM4Engine();
			case CipherUtilities.CipherAlgorithm.TEA:
				return new TeaEngine();
			case CipherUtilities.CipherAlgorithm.THREEFISH_256:
				return new ThreefishEngine(256);
			case CipherUtilities.CipherAlgorithm.THREEFISH_512:
				return new ThreefishEngine(512);
			case CipherUtilities.CipherAlgorithm.THREEFISH_1024:
				return new ThreefishEngine(1024);
			case CipherUtilities.CipherAlgorithm.TNEPRES:
				return new TnepresEngine();
			case CipherUtilities.CipherAlgorithm.TWOFISH:
				return new TwofishEngine();
			case CipherUtilities.CipherAlgorithm.XTEA:
				return new XteaEngine();
			}
			throw new SecurityUtilityException("Cipher " + cipherAlgorithm + " not recognised or not a block cipher");
		}

		// Token: 0x040017A1 RID: 6049
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x040017A2 RID: 6050
		private static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x020008DD RID: 2269
		private enum CipherAlgorithm
		{
			// Token: 0x04003395 RID: 13205
			AES,
			// Token: 0x04003396 RID: 13206
			ARC4,
			// Token: 0x04003397 RID: 13207
			BLOWFISH,
			// Token: 0x04003398 RID: 13208
			CAMELLIA,
			// Token: 0x04003399 RID: 13209
			CAST5,
			// Token: 0x0400339A RID: 13210
			CAST6,
			// Token: 0x0400339B RID: 13211
			DES,
			// Token: 0x0400339C RID: 13212
			DESEDE,
			// Token: 0x0400339D RID: 13213
			ELGAMAL,
			// Token: 0x0400339E RID: 13214
			GOST28147,
			// Token: 0x0400339F RID: 13215
			HC128,
			// Token: 0x040033A0 RID: 13216
			HC256,
			// Token: 0x040033A1 RID: 13217
			IDEA,
			// Token: 0x040033A2 RID: 13218
			NOEKEON,
			// Token: 0x040033A3 RID: 13219
			PBEWITHSHAAND128BITRC4,
			// Token: 0x040033A4 RID: 13220
			PBEWITHSHAAND40BITRC4,
			// Token: 0x040033A5 RID: 13221
			RC2,
			// Token: 0x040033A6 RID: 13222
			RC5,
			// Token: 0x040033A7 RID: 13223
			RC5_64,
			// Token: 0x040033A8 RID: 13224
			RC6,
			// Token: 0x040033A9 RID: 13225
			RIJNDAEL,
			// Token: 0x040033AA RID: 13226
			RSA,
			// Token: 0x040033AB RID: 13227
			SALSA20,
			// Token: 0x040033AC RID: 13228
			SEED,
			// Token: 0x040033AD RID: 13229
			SERPENT,
			// Token: 0x040033AE RID: 13230
			SKIPJACK,
			// Token: 0x040033AF RID: 13231
			SM4,
			// Token: 0x040033B0 RID: 13232
			TEA,
			// Token: 0x040033B1 RID: 13233
			THREEFISH_256,
			// Token: 0x040033B2 RID: 13234
			THREEFISH_512,
			// Token: 0x040033B3 RID: 13235
			THREEFISH_1024,
			// Token: 0x040033B4 RID: 13236
			TNEPRES,
			// Token: 0x040033B5 RID: 13237
			TWOFISH,
			// Token: 0x040033B6 RID: 13238
			VMPC,
			// Token: 0x040033B7 RID: 13239
			VMPC_KSA3,
			// Token: 0x040033B8 RID: 13240
			XTEA
		}

		// Token: 0x020008DE RID: 2270
		private enum CipherMode
		{
			// Token: 0x040033BA RID: 13242
			ECB,
			// Token: 0x040033BB RID: 13243
			NONE,
			// Token: 0x040033BC RID: 13244
			CBC,
			// Token: 0x040033BD RID: 13245
			CCM,
			// Token: 0x040033BE RID: 13246
			CFB,
			// Token: 0x040033BF RID: 13247
			CTR,
			// Token: 0x040033C0 RID: 13248
			CTS,
			// Token: 0x040033C1 RID: 13249
			EAX,
			// Token: 0x040033C2 RID: 13250
			GCM,
			// Token: 0x040033C3 RID: 13251
			GOFB,
			// Token: 0x040033C4 RID: 13252
			OCB,
			// Token: 0x040033C5 RID: 13253
			OFB,
			// Token: 0x040033C6 RID: 13254
			OPENPGPCFB,
			// Token: 0x040033C7 RID: 13255
			SIC
		}

		// Token: 0x020008DF RID: 2271
		private enum CipherPadding
		{
			// Token: 0x040033C9 RID: 13257
			NOPADDING,
			// Token: 0x040033CA RID: 13258
			RAW,
			// Token: 0x040033CB RID: 13259
			ISO10126PADDING,
			// Token: 0x040033CC RID: 13260
			ISO10126D2PADDING,
			// Token: 0x040033CD RID: 13261
			ISO10126_2PADDING,
			// Token: 0x040033CE RID: 13262
			ISO7816_4PADDING,
			// Token: 0x040033CF RID: 13263
			ISO9797_1PADDING,
			// Token: 0x040033D0 RID: 13264
			ISO9796_1,
			// Token: 0x040033D1 RID: 13265
			ISO9796_1PADDING,
			// Token: 0x040033D2 RID: 13266
			OAEP,
			// Token: 0x040033D3 RID: 13267
			OAEPPADDING,
			// Token: 0x040033D4 RID: 13268
			OAEPWITHMD5ANDMGF1PADDING,
			// Token: 0x040033D5 RID: 13269
			OAEPWITHSHA1ANDMGF1PADDING,
			// Token: 0x040033D6 RID: 13270
			OAEPWITHSHA_1ANDMGF1PADDING,
			// Token: 0x040033D7 RID: 13271
			OAEPWITHSHA224ANDMGF1PADDING,
			// Token: 0x040033D8 RID: 13272
			OAEPWITHSHA_224ANDMGF1PADDING,
			// Token: 0x040033D9 RID: 13273
			OAEPWITHSHA256ANDMGF1PADDING,
			// Token: 0x040033DA RID: 13274
			OAEPWITHSHA_256ANDMGF1PADDING,
			// Token: 0x040033DB RID: 13275
			OAEPWITHSHA384ANDMGF1PADDING,
			// Token: 0x040033DC RID: 13276
			OAEPWITHSHA_384ANDMGF1PADDING,
			// Token: 0x040033DD RID: 13277
			OAEPWITHSHA512ANDMGF1PADDING,
			// Token: 0x040033DE RID: 13278
			OAEPWITHSHA_512ANDMGF1PADDING,
			// Token: 0x040033DF RID: 13279
			PKCS1,
			// Token: 0x040033E0 RID: 13280
			PKCS1PADDING,
			// Token: 0x040033E1 RID: 13281
			PKCS5,
			// Token: 0x040033E2 RID: 13282
			PKCS5PADDING,
			// Token: 0x040033E3 RID: 13283
			PKCS7,
			// Token: 0x040033E4 RID: 13284
			PKCS7PADDING,
			// Token: 0x040033E5 RID: 13285
			TBCPADDING,
			// Token: 0x040033E6 RID: 13286
			WITHCTS,
			// Token: 0x040033E7 RID: 13287
			X923PADDING,
			// Token: 0x040033E8 RID: 13288
			ZEROBYTEPADDING
		}
	}
}
