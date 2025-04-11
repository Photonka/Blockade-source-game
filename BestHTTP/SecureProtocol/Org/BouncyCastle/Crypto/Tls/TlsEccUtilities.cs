using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000450 RID: 1104
	public abstract class TlsEccUtilities
	{
		// Token: 0x06002B77 RID: 11127 RVA: 0x00118759 File Offset: 0x00116959
		public static void AddSupportedEllipticCurvesExtension(IDictionary extensions, int[] namedCurves)
		{
			extensions[10] = TlsEccUtilities.CreateSupportedEllipticCurvesExtension(namedCurves);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0011876E File Offset: 0x0011696E
		public static void AddSupportedPointFormatsExtension(IDictionary extensions, byte[] ecPointFormats)
		{
			extensions[11] = TlsEccUtilities.CreateSupportedPointFormatsExtension(ecPointFormats);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00118784 File Offset: 0x00116984
		public static int[] GetSupportedEllipticCurvesExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 10);
			if (extensionData != null)
			{
				return TlsEccUtilities.ReadSupportedEllipticCurvesExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x001187A8 File Offset: 0x001169A8
		public static byte[] GetSupportedPointFormatsExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 11);
			if (extensionData != null)
			{
				return TlsEccUtilities.ReadSupportedPointFormatsExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x001187C9 File Offset: 0x001169C9
		public static byte[] CreateSupportedEllipticCurvesExtension(int[] namedCurves)
		{
			if (namedCurves == null || namedCurves.Length < 1)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint16ArrayWithUint16Length(namedCurves);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x001187E2 File Offset: 0x001169E2
		public static byte[] CreateSupportedPointFormatsExtension(byte[] ecPointFormats)
		{
			if (ecPointFormats == null || !Arrays.Contains(ecPointFormats, 0))
			{
				ecPointFormats = Arrays.Append(ecPointFormats, 0);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(ecPointFormats);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00118800 File Offset: 0x00116A00
		public static int[] ReadSupportedEllipticCurvesExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			int num = TlsUtilities.ReadUint16(memoryStream);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int[] result = TlsUtilities.ReadUint16Array(num / 2, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0011884A File Offset: 0x00116A4A
		public static byte[] ReadSupportedPointFormatsExtension(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (!Arrays.Contains(array, 0))
			{
				throw new TlsFatalAlert(47);
			}
			return array;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x00118863 File Offset: 0x00116A63
		public static string GetNameOfNamedCurve(int namedCurve)
		{
			if (!TlsEccUtilities.IsSupportedNamedCurve(namedCurve))
			{
				return null;
			}
			return TlsEccUtilities.CurveNames[namedCurve - 1];
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00118878 File Offset: 0x00116A78
		public static ECDomainParameters GetParametersForNamedCurve(int namedCurve)
		{
			string nameOfNamedCurve = TlsEccUtilities.GetNameOfNamedCurve(namedCurve);
			if (nameOfNamedCurve == null)
			{
				return null;
			}
			X9ECParameters byName = CustomNamedCurves.GetByName(nameOfNamedCurve);
			if (byName == null)
			{
				byName = ECNamedCurveTable.GetByName(nameOfNamedCurve);
				if (byName == null)
				{
					return null;
				}
			}
			return new ECDomainParameters(byName.Curve, byName.G, byName.N, byName.H, byName.GetSeed());
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x001188CA File Offset: 0x00116ACA
		public static bool HasAnySupportedNamedCurves()
		{
			return TlsEccUtilities.CurveNames.Length != 0;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x001188D8 File Offset: 0x00116AD8
		public static bool ContainsEccCipherSuites(int[] cipherSuites)
		{
			for (int i = 0; i < cipherSuites.Length; i++)
			{
				if (TlsEccUtilities.IsEccCipherSuite(cipherSuites[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x00118900 File Offset: 0x00116B00
		public static bool IsEccCipherSuite(int cipherSuite)
		{
			if (cipherSuite <= 49307)
			{
				if (cipherSuite <= 49211)
				{
					if (cipherSuite - 49153 > 24 && cipherSuite - 49187 > 24)
					{
						return false;
					}
				}
				else if (cipherSuite - 49266 > 7 && cipherSuite - 49286 > 7 && cipherSuite - 49306 > 1)
				{
					return false;
				}
			}
			else if (cipherSuite <= 52393)
			{
				if (cipherSuite - 49324 > 3 && cipherSuite - 52392 > 1)
				{
					return false;
				}
			}
			else if (cipherSuite != 52396 && cipherSuite - 65282 > 3 && cipherSuite - 65300 > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x00118992 File Offset: 0x00116B92
		public static bool AreOnSameCurve(ECDomainParameters a, ECDomainParameters b)
		{
			return a != null && a.Equals(b);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x001189A0 File Offset: 0x00116BA0
		public static bool IsSupportedNamedCurve(int namedCurve)
		{
			return namedCurve > 0 && namedCurve <= TlsEccUtilities.CurveNames.Length;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x001189B8 File Offset: 0x00116BB8
		public static bool IsCompressionPreferred(byte[] ecPointFormats, byte compressionFormat)
		{
			if (ecPointFormats == null)
			{
				return false;
			}
			foreach (byte b in ecPointFormats)
			{
				if (b == 0)
				{
					return false;
				}
				if (b == compressionFormat)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x001189E8 File Offset: 0x00116BE8
		public static byte[] SerializeECFieldElement(int fieldSize, BigInteger x)
		{
			return BigIntegers.AsUnsignedByteArray((fieldSize + 7) / 8, x);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x001189F8 File Offset: 0x00116BF8
		public static byte[] SerializeECPoint(byte[] ecPointFormats, ECPoint point)
		{
			ECCurve curve = point.Curve;
			bool compressed = false;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				compressed = TlsEccUtilities.IsCompressionPreferred(ecPointFormats, 1);
			}
			else if (ECAlgorithms.IsF2mCurve(curve))
			{
				compressed = TlsEccUtilities.IsCompressionPreferred(ecPointFormats, 2);
			}
			return point.GetEncoded(compressed);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00118A37 File Offset: 0x00116C37
		public static byte[] SerializeECPublicKey(byte[] ecPointFormats, ECPublicKeyParameters keyParameters)
		{
			return TlsEccUtilities.SerializeECPoint(ecPointFormats, keyParameters.Q);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00118A48 File Offset: 0x00116C48
		public static BigInteger DeserializeECFieldElement(int fieldSize, byte[] encoding)
		{
			int num = (fieldSize + 7) / 8;
			if (encoding.Length != num)
			{
				throw new TlsFatalAlert(50);
			}
			return new BigInteger(1, encoding);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00118A70 File Offset: 0x00116C70
		public static ECPoint DeserializeECPoint(byte[] ecPointFormats, ECCurve curve, byte[] encoding)
		{
			if (encoding == null || encoding.Length < 1)
			{
				throw new TlsFatalAlert(47);
			}
			byte b;
			switch (encoding[0])
			{
			case 2:
			case 3:
				if (ECAlgorithms.IsF2mCurve(curve))
				{
					b = 2;
					goto IL_69;
				}
				if (ECAlgorithms.IsFpCurve(curve))
				{
					b = 1;
					goto IL_69;
				}
				throw new TlsFatalAlert(47);
			case 4:
				b = 0;
				goto IL_69;
			}
			throw new TlsFatalAlert(47);
			IL_69:
			if (b != 0 && (ecPointFormats == null || !Arrays.Contains(ecPointFormats, b)))
			{
				throw new TlsFatalAlert(47);
			}
			return curve.DecodePoint(encoding);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00118B04 File Offset: 0x00116D04
		public static ECPublicKeyParameters DeserializeECPublicKey(byte[] ecPointFormats, ECDomainParameters curve_params, byte[] encoding)
		{
			ECPublicKeyParameters result;
			try
			{
				result = new ECPublicKeyParameters(TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve_params.Curve, encoding), curve_params);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x00118B44 File Offset: 0x00116D44
		public static byte[] CalculateECDHBasicAgreement(ECPublicKeyParameters publicKey, ECPrivateKeyParameters privateKey)
		{
			ECDHBasicAgreement ecdhbasicAgreement = new ECDHBasicAgreement();
			ecdhbasicAgreement.Init(privateKey);
			BigInteger n = ecdhbasicAgreement.CalculateAgreement(publicKey);
			return BigIntegers.AsUnsignedByteArray(ecdhbasicAgreement.GetFieldSize(), n);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00118B70 File Offset: 0x00116D70
		public static AsymmetricCipherKeyPair GenerateECKeyPair(SecureRandom random, ECDomainParameters ecParams)
		{
			ECKeyPairGenerator eckeyPairGenerator = new ECKeyPairGenerator();
			eckeyPairGenerator.Init(new ECKeyGenerationParameters(ecParams, random));
			return eckeyPairGenerator.GenerateKeyPair();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00118B8C File Offset: 0x00116D8C
		public static ECPrivateKeyParameters GenerateEphemeralClientKeyExchange(SecureRandom random, byte[] ecPointFormats, ECDomainParameters ecParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsEccUtilities.GenerateECKeyPair(random, ecParams);
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)asymmetricCipherKeyPair.Public;
			TlsEccUtilities.WriteECPoint(ecPointFormats, ecpublicKeyParameters.Q, output);
			return (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00118BC4 File Offset: 0x00116DC4
		internal static ECPrivateKeyParameters GenerateEphemeralServerKeyExchange(SecureRandom random, int[] namedCurves, byte[] ecPointFormats, Stream output)
		{
			int num = -1;
			if (namedCurves == null)
			{
				num = 23;
			}
			else
			{
				foreach (int num2 in namedCurves)
				{
					if (NamedCurve.IsValid(num2) && TlsEccUtilities.IsSupportedNamedCurve(num2))
					{
						num = num2;
						break;
					}
				}
			}
			ECDomainParameters ecdomainParameters = null;
			if (num >= 0)
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(num);
			}
			else if (Arrays.Contains(namedCurves, 65281))
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(23);
			}
			else if (Arrays.Contains(namedCurves, 65282))
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(10);
			}
			if (ecdomainParameters == null)
			{
				throw new TlsFatalAlert(80);
			}
			if (num < 0)
			{
				TlsEccUtilities.WriteExplicitECParameters(ecPointFormats, ecdomainParameters, output);
			}
			else
			{
				TlsEccUtilities.WriteNamedECParameters(num, output);
			}
			return TlsEccUtilities.GenerateEphemeralClientKeyExchange(random, ecPointFormats, ecdomainParameters, output);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public static ECPublicKeyParameters ValidateECPublicKey(ECPublicKeyParameters key)
		{
			return key;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00118C68 File Offset: 0x00116E68
		public static int ReadECExponent(int fieldSize, Stream input)
		{
			BigInteger bigInteger = TlsEccUtilities.ReadECParameter(input);
			if (bigInteger.BitLength < 32)
			{
				int intValue = bigInteger.IntValue;
				if (intValue > 0 && intValue < fieldSize)
				{
					return intValue;
				}
			}
			throw new TlsFatalAlert(47);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x00118C9E File Offset: 0x00116E9E
		public static BigInteger ReadECFieldElement(int fieldSize, Stream input)
		{
			return TlsEccUtilities.DeserializeECFieldElement(fieldSize, TlsUtilities.ReadOpaque8(input));
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00118CAC File Offset: 0x00116EAC
		public static BigInteger ReadECParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque8(input));
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x00118CBC File Offset: 0x00116EBC
		public static ECDomainParameters ReadECParameters(int[] namedCurves, byte[] ecPointFormats, Stream input)
		{
			ECDomainParameters result;
			try
			{
				switch (TlsUtilities.ReadUint8(input))
				{
				case 1:
				{
					TlsEccUtilities.CheckNamedCurve(namedCurves, 65281);
					BigInteger bigInteger = TlsEccUtilities.ReadECParameter(input);
					BigInteger a = TlsEccUtilities.ReadECFieldElement(bigInteger.BitLength, input);
					BigInteger b = TlsEccUtilities.ReadECFieldElement(bigInteger.BitLength, input);
					byte[] encoding = TlsUtilities.ReadOpaque8(input);
					BigInteger bigInteger2 = TlsEccUtilities.ReadECParameter(input);
					BigInteger bigInteger3 = TlsEccUtilities.ReadECParameter(input);
					ECCurve curve = new FpCurve(bigInteger, a, b, bigInteger2, bigInteger3);
					ECPoint g = TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve, encoding);
					result = new ECDomainParameters(curve, g, bigInteger2, bigInteger3);
					break;
				}
				case 2:
				{
					TlsEccUtilities.CheckNamedCurve(namedCurves, 65282);
					int num = TlsUtilities.ReadUint16(input);
					byte b2 = TlsUtilities.ReadUint8(input);
					if (!ECBasisType.IsValid(b2))
					{
						throw new TlsFatalAlert(47);
					}
					int num2 = TlsEccUtilities.ReadECExponent(num, input);
					int k = -1;
					int k2 = -1;
					if (b2 == 2)
					{
						k = TlsEccUtilities.ReadECExponent(num, input);
						k2 = TlsEccUtilities.ReadECExponent(num, input);
					}
					BigInteger a2 = TlsEccUtilities.ReadECFieldElement(num, input);
					BigInteger b3 = TlsEccUtilities.ReadECFieldElement(num, input);
					byte[] encoding2 = TlsUtilities.ReadOpaque8(input);
					BigInteger bigInteger4 = TlsEccUtilities.ReadECParameter(input);
					BigInteger bigInteger5 = TlsEccUtilities.ReadECParameter(input);
					ECCurve curve2 = (b2 == 2) ? new F2mCurve(num, num2, k, k2, a2, b3, bigInteger4, bigInteger5) : new F2mCurve(num, num2, a2, b3, bigInteger4, bigInteger5);
					ECPoint g2 = TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve2, encoding2);
					result = new ECDomainParameters(curve2, g2, bigInteger4, bigInteger5);
					break;
				}
				case 3:
				{
					int namedCurve = TlsUtilities.ReadUint16(input);
					if (!NamedCurve.RefersToASpecificNamedCurve(namedCurve))
					{
						throw new TlsFatalAlert(47);
					}
					TlsEccUtilities.CheckNamedCurve(namedCurves, namedCurve);
					result = TlsEccUtilities.GetParametersForNamedCurve(namedCurve);
					break;
				}
				default:
					throw new TlsFatalAlert(47);
				}
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00118E7C File Offset: 0x0011707C
		private static void CheckNamedCurve(int[] namedCurves, int namedCurve)
		{
			if (namedCurves != null && !Arrays.Contains(namedCurves, namedCurve))
			{
				throw new TlsFatalAlert(47);
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00118E92 File Offset: 0x00117092
		public static void WriteECExponent(int k, Stream output)
		{
			TlsEccUtilities.WriteECParameter(BigInteger.ValueOf((long)k), output);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00118EA1 File Offset: 0x001170A1
		public static void WriteECFieldElement(ECFieldElement x, Stream output)
		{
			TlsUtilities.WriteOpaque8(x.GetEncoded(), output);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00118EAF File Offset: 0x001170AF
		public static void WriteECFieldElement(int fieldSize, BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECFieldElement(fieldSize, x), output);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00118EBE File Offset: 0x001170BE
		public static void WriteECParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque8(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00118ECC File Offset: 0x001170CC
		public static void WriteExplicitECParameters(byte[] ecPointFormats, ECDomainParameters ecParameters, Stream output)
		{
			ECCurve curve = ecParameters.Curve;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				TlsUtilities.WriteUint8(1, output);
				TlsEccUtilities.WriteECParameter(curve.Field.Characteristic, output);
			}
			else
			{
				if (!ECAlgorithms.IsF2mCurve(curve))
				{
					throw new ArgumentException("'ecParameters' not a known curve type");
				}
				int[] exponentsPresent = ((IPolynomialExtensionField)curve.Field).MinimalPolynomial.GetExponentsPresent();
				TlsUtilities.WriteUint8(2, output);
				int i = exponentsPresent[exponentsPresent.Length - 1];
				TlsUtilities.CheckUint16(i);
				TlsUtilities.WriteUint16(i, output);
				if (exponentsPresent.Length == 3)
				{
					TlsUtilities.WriteUint8(1, output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[1], output);
				}
				else
				{
					if (exponentsPresent.Length != 5)
					{
						throw new ArgumentException("Only trinomial and pentomial curves are supported");
					}
					TlsUtilities.WriteUint8(2, output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[1], output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[2], output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[3], output);
				}
			}
			TlsEccUtilities.WriteECFieldElement(curve.A, output);
			TlsEccUtilities.WriteECFieldElement(curve.B, output);
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECPoint(ecPointFormats, ecParameters.G), output);
			TlsEccUtilities.WriteECParameter(ecParameters.N, output);
			TlsEccUtilities.WriteECParameter(ecParameters.H, output);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x00118FD7 File Offset: 0x001171D7
		public static void WriteECPoint(byte[] ecPointFormats, ECPoint point, Stream output)
		{
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECPoint(ecPointFormats, point), output);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x00118FE6 File Offset: 0x001171E6
		public static void WriteNamedECParameters(int namedCurve, Stream output)
		{
			if (!NamedCurve.RefersToASpecificNamedCurve(namedCurve))
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteUint8(3, output);
			TlsUtilities.CheckUint16(namedCurve);
			TlsUtilities.WriteUint16(namedCurve, output);
		}

		// Token: 0x04001CFD RID: 7421
		private static readonly string[] CurveNames = new string[]
		{
			"sect163k1",
			"sect163r1",
			"sect163r2",
			"sect193r1",
			"sect193r2",
			"sect233k1",
			"sect233r1",
			"sect239k1",
			"sect283k1",
			"sect283r1",
			"sect409k1",
			"sect409r1",
			"sect571k1",
			"sect571r1",
			"secp160k1",
			"secp160r1",
			"secp160r2",
			"secp192k1",
			"secp192r1",
			"secp224k1",
			"secp224r1",
			"secp256k1",
			"secp256r1",
			"secp384r1",
			"secp521r1",
			"brainpoolP256r1",
			"brainpoolP384r1",
			"brainpoolP512r1"
		};
	}
}
