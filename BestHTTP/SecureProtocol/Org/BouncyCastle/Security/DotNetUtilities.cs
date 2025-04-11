using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C6 RID: 710
	public sealed class DotNetUtilities
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x00023EF4 File Offset: 0x000220F4
		private DotNetUtilities()
		{
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000CB452 File Offset: 0x000C9652
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(X509CertificateStructure x509Struct)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Struct.GetDerEncoded());
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000CB45F File Offset: 0x000C965F
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(BestHTTP.SecureProtocol.Org.BouncyCastle.X509.X509Certificate x509Cert)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Cert.GetEncoded());
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000CB46C File Offset: 0x000C966C
		public static BestHTTP.SecureProtocol.Org.BouncyCastle.X509.X509Certificate FromX509Certificate(System.Security.Cryptography.X509Certificates.X509Certificate x509Cert)
		{
			return new X509CertificateParser().ReadCertificate(x509Cert.GetRawCertData());
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000CB47E File Offset: 0x000C967E
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSA dsa)
		{
			return DotNetUtilities.GetDsaKeyPair(dsa.ExportParameters(true));
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000CB48C File Offset: 0x000C968C
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			AsymmetricKeyParameter publicParameter = new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
			DsaPrivateKeyParameters privateParameter = new DsaPrivateKeyParameters(new BigInteger(1, dp.X), parameters2);
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000CB50C File Offset: 0x000C970C
		public static DsaPublicKeyParameters GetDsaPublicKey(DSA dsa)
		{
			return DotNetUtilities.GetDsaPublicKey(dsa.ExportParameters(false));
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000CB51C File Offset: 0x000C971C
		public static DsaPublicKeyParameters GetDsaPublicKey(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			return new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000CB583 File Offset: 0x000C9783
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSA rsa)
		{
			return DotNetUtilities.GetRsaKeyPair(rsa.ExportParameters(true));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000CB594 File Offset: 0x000C9794
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSAParameters rp)
		{
			BigInteger modulus = new BigInteger(1, rp.Modulus);
			BigInteger bigInteger = new BigInteger(1, rp.Exponent);
			AsymmetricKeyParameter publicParameter = new RsaKeyParameters(false, modulus, bigInteger);
			RsaPrivateCrtKeyParameters privateParameter = new RsaPrivateCrtKeyParameters(modulus, bigInteger, new BigInteger(1, rp.D), new BigInteger(1, rp.P), new BigInteger(1, rp.Q), new BigInteger(1, rp.DP), new BigInteger(1, rp.DQ), new BigInteger(1, rp.InverseQ));
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000CB619 File Offset: 0x000C9819
		public static RsaKeyParameters GetRsaPublicKey(RSA rsa)
		{
			return DotNetUtilities.GetRsaPublicKey(rsa.ExportParameters(false));
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000CB627 File Offset: 0x000C9827
		public static RsaKeyParameters GetRsaPublicKey(RSAParameters rp)
		{
			return new RsaKeyParameters(false, new BigInteger(1, rp.Modulus), new BigInteger(1, rp.Exponent));
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000CB647 File Offset: 0x000C9847
		public static AsymmetricCipherKeyPair GetKeyPair(AsymmetricAlgorithm privateKey)
		{
			if (privateKey is DSA)
			{
				return DotNetUtilities.GetDsaKeyPair((DSA)privateKey);
			}
			if (privateKey is RSA)
			{
				return DotNetUtilities.GetRsaKeyPair((RSA)privateKey);
			}
			throw new ArgumentException("Unsupported algorithm specified", "privateKey");
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000CB680 File Offset: 0x000C9880
		public static RSA ToRSA(RsaKeyParameters rsaKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(rsaKey));
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000CB68D File Offset: 0x000C988D
		public static RSA ToRSA(RsaKeyParameters rsaKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(rsaKey), csp);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000CB69B File Offset: 0x000C989B
		public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey));
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000CB6A8 File Offset: 0x000C98A8
		public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey), csp);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000CB6B6 File Offset: 0x000C98B6
		public static RSA ToRSA(RsaPrivateKeyStructure privKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey));
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000CB6C3 File Offset: 0x000C98C3
		public static RSA ToRSA(RsaPrivateKeyStructure privKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey), csp);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000CB6D4 File Offset: 0x000C98D4
		public static RSAParameters ToRSAParameters(RsaKeyParameters rsaKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Modulus = rsaKey.Modulus.ToByteArrayUnsigned();
			if (rsaKey.IsPrivate)
			{
				rsaparameters.D = DotNetUtilities.ConvertRSAParametersField(rsaKey.Exponent, rsaparameters.Modulus.Length);
			}
			else
			{
				rsaparameters.Exponent = rsaKey.Exponent.ToByteArrayUnsigned();
			}
			return rsaparameters;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000CB734 File Offset: 0x000C9934
		public static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Modulus = privKey.Modulus.ToByteArrayUnsigned();
			rsaparameters.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
			rsaparameters.P = privKey.P.ToByteArrayUnsigned();
			rsaparameters.Q = privKey.Q.ToByteArrayUnsigned();
			rsaparameters.D = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent, rsaparameters.Modulus.Length);
			rsaparameters.DP = DotNetUtilities.ConvertRSAParametersField(privKey.DP, rsaparameters.P.Length);
			rsaparameters.DQ = DotNetUtilities.ConvertRSAParametersField(privKey.DQ, rsaparameters.Q.Length);
			rsaparameters.InverseQ = DotNetUtilities.ConvertRSAParametersField(privKey.QInv, rsaparameters.Q.Length);
			return rsaparameters;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000CB7FC File Offset: 0x000C99FC
		public static RSAParameters ToRSAParameters(RsaPrivateKeyStructure privKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Modulus = privKey.Modulus.ToByteArrayUnsigned();
			rsaparameters.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
			rsaparameters.P = privKey.Prime1.ToByteArrayUnsigned();
			rsaparameters.Q = privKey.Prime2.ToByteArrayUnsigned();
			rsaparameters.D = DotNetUtilities.ConvertRSAParametersField(privKey.PrivateExponent, rsaparameters.Modulus.Length);
			rsaparameters.DP = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent1, rsaparameters.P.Length);
			rsaparameters.DQ = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent2, rsaparameters.Q.Length);
			rsaparameters.InverseQ = DotNetUtilities.ConvertRSAParametersField(privKey.Coefficient, rsaparameters.Q.Length);
			return rsaparameters;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000CB8C4 File Offset: 0x000C9AC4
		private static byte[] ConvertRSAParametersField(BigInteger n, int size)
		{
			byte[] array = n.ToByteArrayUnsigned();
			if (array.Length == size)
			{
				return array;
			}
			if (array.Length > size)
			{
				throw new ArgumentException("Specified size too small", "size");
			}
			byte[] array2 = new byte[size];
			Array.Copy(array, 0, array2, size - array.Length, array.Length);
			return array2;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000CB90E File Offset: 0x000C9B0E
		private static RSA CreateRSAProvider(RSAParameters rp)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
			{
				KeyContainerName = string.Format("BouncyCastle-{0}", Guid.NewGuid())
			});
			rsacryptoServiceProvider.ImportParameters(rp);
			return rsacryptoServiceProvider;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000CB93B File Offset: 0x000C9B3B
		private static RSA CreateRSAProvider(RSAParameters rp, CspParameters csp)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(csp);
			rsacryptoServiceProvider.ImportParameters(rp);
			return rsacryptoServiceProvider;
		}
	}
}
