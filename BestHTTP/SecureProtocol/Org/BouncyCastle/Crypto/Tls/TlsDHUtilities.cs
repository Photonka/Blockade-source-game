using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044C RID: 1100
	public abstract class TlsDHUtilities
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x0011807A File Offset: 0x0011627A
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.Decode(hex));
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00118088 File Offset: 0x00116288
		private static DHParameters FromSafeP(string hexP)
		{
			BigInteger bigInteger = TlsDHUtilities.FromHex(hexP);
			BigInteger q = bigInteger.ShiftRight(1);
			return new DHParameters(bigInteger, TlsDHUtilities.Two, q);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x001180AE File Offset: 0x001162AE
		public static void AddNegotiatedDheGroupsClientExtension(IDictionary extensions, byte[] dheGroups)
		{
			extensions[ExtensionType.negotiated_ff_dhe_groups] = TlsDHUtilities.CreateNegotiatedDheGroupsClientExtension(dheGroups);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x001180C6 File Offset: 0x001162C6
		public static void AddNegotiatedDheGroupsServerExtension(IDictionary extensions, byte dheGroup)
		{
			extensions[ExtensionType.negotiated_ff_dhe_groups] = TlsDHUtilities.CreateNegotiatedDheGroupsServerExtension(dheGroup);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x001180E0 File Offset: 0x001162E0
		public static byte[] GetNegotiatedDheGroupsClientExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, ExtensionType.negotiated_ff_dhe_groups);
			if (extensionData != null)
			{
				return TlsDHUtilities.ReadNegotiatedDheGroupsClientExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x00118104 File Offset: 0x00116304
		public static short GetNegotiatedDheGroupsServerExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, ExtensionType.negotiated_ff_dhe_groups);
			if (extensionData != null)
			{
				return (short)TlsDHUtilities.ReadNegotiatedDheGroupsServerExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00118128 File Offset: 0x00116328
		public static byte[] CreateNegotiatedDheGroupsClientExtension(byte[] dheGroups)
		{
			if (dheGroups == null || dheGroups.Length < 1 || dheGroups.Length > 255)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(dheGroups);
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0011814B File Offset: 0x0011634B
		public static byte[] CreateNegotiatedDheGroupsServerExtension(byte dheGroup)
		{
			return TlsUtilities.EncodeUint8(dheGroup);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00118153 File Offset: 0x00116353
		public static byte[] ReadNegotiatedDheGroupsClientExtension(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			return array;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00118169 File Offset: 0x00116369
		public static byte ReadNegotiatedDheGroupsServerExtension(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00118171 File Offset: 0x00116371
		public static DHParameters GetParametersForDHEGroup(short dheGroup)
		{
			switch (dheGroup)
			{
			case 0:
				return TlsDHUtilities.draft_ffdhe2432;
			case 1:
				return TlsDHUtilities.draft_ffdhe3072;
			case 2:
				return TlsDHUtilities.draft_ffdhe4096;
			case 3:
				return TlsDHUtilities.draft_ffdhe6144;
			case 4:
				return TlsDHUtilities.draft_ffdhe8192;
			default:
				return null;
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x001181B0 File Offset: 0x001163B0
		public static bool ContainsDheCipherSuites(int[] cipherSuites)
		{
			for (int i = 0; i < cipherSuites.Length; i++)
			{
				if (TlsDHUtilities.IsDheCipherSuite(cipherSuites[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x001181D8 File Offset: 0x001163D8
		public static bool IsDheCipherSuite(int cipherSuite)
		{
			if (cipherSuite <= 145)
			{
				if (cipherSuite <= 64)
				{
					if (cipherSuite <= 45)
					{
						switch (cipherSuite)
						{
						case 17:
						case 18:
						case 19:
						case 20:
						case 21:
						case 22:
						case 24:
						case 27:
							break;
						case 23:
						case 25:
						case 26:
							return false;
						default:
							if (cipherSuite != 45)
							{
								return false;
							}
							break;
						}
					}
					else if (cipherSuite - 50 > 2 && cipherSuite - 56 > 2 && cipherSuite != 64)
					{
						return false;
					}
				}
				else if (cipherSuite <= 103)
				{
					if (cipherSuite - 68 > 2 && cipherSuite != 103)
					{
						return false;
					}
				}
				else if (cipherSuite - 106 > 3 && cipherSuite - 135 > 2 && cipherSuite - 142 > 3)
				{
					return false;
				}
			}
			else if (cipherSuite <= 49297)
			{
				if (cipherSuite <= 191)
				{
					switch (cipherSuite)
					{
					case 153:
					case 154:
					case 155:
					case 158:
					case 159:
					case 162:
					case 163:
					case 166:
					case 167:
					case 170:
					case 171:
					case 178:
					case 179:
					case 180:
					case 181:
						break;
					case 156:
					case 157:
					case 160:
					case 161:
					case 164:
					case 165:
					case 168:
					case 169:
					case 172:
					case 173:
					case 174:
					case 175:
					case 176:
					case 177:
						return false;
					default:
						if (cipherSuite - 189 > 2)
						{
							return false;
						}
						break;
					}
				}
				else if (cipherSuite - 195 > 2)
				{
					switch (cipherSuite)
					{
					case 49276:
					case 49277:
					case 49280:
					case 49281:
					case 49284:
					case 49285:
						break;
					case 49278:
					case 49279:
					case 49282:
					case 49283:
						return false;
					default:
						if (cipherSuite - 49296 > 1)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (cipherSuite <= 52394)
			{
				if (cipherSuite - 49302 > 1)
				{
					switch (cipherSuite)
					{
					case 49310:
					case 49311:
					case 49314:
					case 49315:
					case 49318:
					case 49319:
					case 49322:
					case 49323:
						break;
					case 49312:
					case 49313:
					case 49316:
					case 49317:
					case 49320:
					case 49321:
						return false;
					default:
						if (cipherSuite != 52394)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (cipherSuite != 52397 && cipherSuite - 65280 > 1 && cipherSuite - 65298 > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00118418 File Offset: 0x00116618
		public static bool AreCompatibleParameters(DHParameters a, DHParameters b)
		{
			return a.P.Equals(b.P) && a.G.Equals(b.G) && (a.Q == null || b.Q == null || a.Q.Equals(b.Q));
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00118470 File Offset: 0x00116670
		public static byte[] CalculateDHBasicAgreement(DHPublicKeyParameters publicKey, DHPrivateKeyParameters privateKey)
		{
			DHBasicAgreement dhbasicAgreement = new DHBasicAgreement();
			dhbasicAgreement.Init(privateKey);
			return BigIntegers.AsUnsignedByteArray(dhbasicAgreement.CalculateAgreement(publicKey));
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00118489 File Offset: 0x00116689
		public static AsymmetricCipherKeyPair GenerateDHKeyPair(SecureRandom random, DHParameters dhParams)
		{
			DHBasicKeyPairGenerator dhbasicKeyPairGenerator = new DHBasicKeyPairGenerator();
			dhbasicKeyPairGenerator.Init(new DHKeyGenerationParameters(random, dhParams));
			return dhbasicKeyPairGenerator.GenerateKeyPair();
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x001184A2 File Offset: 0x001166A2
		public static DHPrivateKeyParameters GenerateEphemeralClientKeyExchange(SecureRandom random, DHParameters dhParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsDHUtilities.GenerateDHKeyPair(random, dhParams);
			TlsDHUtilities.WriteDHParameter(((DHPublicKeyParameters)asymmetricCipherKeyPair.Public).Y, output);
			return (DHPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x001184CB File Offset: 0x001166CB
		public static DHPrivateKeyParameters GenerateEphemeralServerKeyExchange(SecureRandom random, DHParameters dhParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsDHUtilities.GenerateDHKeyPair(random, dhParams);
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)asymmetricCipherKeyPair.Public;
			TlsDHUtilities.WriteDHParameters(dhParams, output);
			TlsDHUtilities.WriteDHParameter(dhpublicKeyParameters.Y, output);
			return (DHPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x001184FB File Offset: 0x001166FB
		public static BigInteger ReadDHParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque16(input));
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0011850C File Offset: 0x0011670C
		public static DHParameters ReadDHParameters(Stream input)
		{
			BigInteger p = TlsDHUtilities.ReadDHParameter(input);
			BigInteger g = TlsDHUtilities.ReadDHParameter(input);
			return new DHParameters(p, g);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0011852C File Offset: 0x0011672C
		public static DHParameters ReceiveDHParameters(TlsDHVerifier dhVerifier, Stream input)
		{
			DHParameters dhparameters = TlsDHUtilities.ReadDHParameters(input);
			if (!dhVerifier.Accept(dhparameters))
			{
				throw new TlsFatalAlert(71);
			}
			return dhparameters;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x00118552 File Offset: 0x00116752
		public static void WriteDHParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque16(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x00118560 File Offset: 0x00116760
		public static void WriteDHParameters(DHParameters dhParameters, Stream output)
		{
			TlsDHUtilities.WriteDHParameter(dhParameters.P, output);
			TlsDHUtilities.WriteDHParameter(dhParameters.G, output);
		}

		// Token: 0x04001CF2 RID: 7410
		internal static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x04001CF3 RID: 7411
		private static readonly string draft_ffdhe2432_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE13098533C8B3FFFFFFFFFFFFFFFF";

		// Token: 0x04001CF4 RID: 7412
		internal static readonly DHParameters draft_ffdhe2432 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe2432_p);

		// Token: 0x04001CF5 RID: 7413
		private static readonly string draft_ffdhe3072_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B66C62E37FFFFFFFFFFFFFFFF";

		// Token: 0x04001CF6 RID: 7414
		internal static readonly DHParameters draft_ffdhe3072 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe3072_p);

		// Token: 0x04001CF7 RID: 7415
		private static readonly string draft_ffdhe4096_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E655F6AFFFFFFFFFFFFFFFF";

		// Token: 0x04001CF8 RID: 7416
		internal static readonly DHParameters draft_ffdhe4096 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe4096_p);

		// Token: 0x04001CF9 RID: 7417
		private static readonly string draft_ffdhe6144_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E0DD9020BFD64B645036C7A4E677D2C38532A3A23BA4442CAF53EA63BB454329B7624C8917BDD64B1C0FD4CB38E8C334C701C3ACDAD0657FCCFEC719B1F5C3E4E46041F388147FB4CFDB477A52471F7A9A96910B855322EDB6340D8A00EF092350511E30ABEC1FFF9E3A26E7FB29F8C183023C3587E38DA0077D9B4763E4E4B94B2BBC194C6651E77CAF992EEAAC0232A281BF6B3A739C1226116820AE8DB5847A67CBEF9C9091B462D538CD72B03746AE77F5E62292C311562A846505DC82DB854338AE49F5235C95B91178CCF2DD5CACEF403EC9D1810C6272B045B3B71F9DC6B80D63FDD4A8E9ADB1E6962A69526D43161C1A41D570D7938DAD4A40E329CD0E40E65FFFFFFFFFFFFFFFF";

		// Token: 0x04001CFA RID: 7418
		internal static readonly DHParameters draft_ffdhe6144 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe6144_p);

		// Token: 0x04001CFB RID: 7419
		private static readonly string draft_ffdhe8192_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E0DD9020BFD64B645036C7A4E677D2C38532A3A23BA4442CAF53EA63BB454329B7624C8917BDD64B1C0FD4CB38E8C334C701C3ACDAD0657FCCFEC719B1F5C3E4E46041F388147FB4CFDB477A52471F7A9A96910B855322EDB6340D8A00EF092350511E30ABEC1FFF9E3A26E7FB29F8C183023C3587E38DA0077D9B4763E4E4B94B2BBC194C6651E77CAF992EEAAC0232A281BF6B3A739C1226116820AE8DB5847A67CBEF9C9091B462D538CD72B03746AE77F5E62292C311562A846505DC82DB854338AE49F5235C95B91178CCF2DD5CACEF403EC9D1810C6272B045B3B71F9DC6B80D63FDD4A8E9ADB1E6962A69526D43161C1A41D570D7938DAD4A40E329CCFF46AAA36AD004CF600C8381E425A31D951AE64FDB23FCEC9509D43687FEB69EDD1CC5E0B8CC3BDF64B10EF86B63142A3AB8829555B2F747C932665CB2C0F1CC01BD70229388839D2AF05E454504AC78B7582822846C0BA35C35F5C59160CC046FD8251541FC68C9C86B022BB7099876A460E7451A8A93109703FEE1C217E6C3826E52C51AA691E0E423CFC99E9E31650C1217B624816CDAD9A95F9D5B8019488D9C0A0A1FE3075A577E23183F81D4A3F2FA4571EFC8CE0BA8A4FE8B6855DFE72B0A66EDED2FBABFBE58A30FAFABE1C5D71A87E2F741EF8C1FE86FEA6BBFDE530677F0D97D11D49F7A8443D0822E506A9F4614E011E2A94838FF88CD68C8BB7C5C6424CFFFFFFFFFFFFFFFF";

		// Token: 0x04001CFC RID: 7420
		internal static readonly DHParameters draft_ffdhe8192 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe8192_p);
	}
}
