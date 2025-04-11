using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Kisa;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ntt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005DC RID: 1500
	public class CmsEnvelopedGenerator
	{
		// Token: 0x06003963 RID: 14691 RVA: 0x00169AAB File Offset: 0x00167CAB
		public CmsEnvelopedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x00169AB8 File Offset: 0x00167CB8
		public CmsEnvelopedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x00169AD2 File Offset: 0x00167CD2
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x00169ADA File Offset: 0x00167CDA
		public CmsAttributeTableGenerator UnprotectedAttributeGenerator
		{
			get
			{
				return this.unprotectedAttributeGenerator;
			}
			set
			{
				this.unprotectedAttributeGenerator = value;
			}
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x00169AE4 File Offset: 0x00167CE4
		public void AddKeyTransRecipient(X509Certificate cert)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientCert = cert;
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00169B0C File Offset: 0x00167D0C
		public void AddKeyTransRecipient(AsymmetricKeyParameter pubKey, byte[] subKeyId)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientPublicKey = pubKey;
			keyTransRecipientInfoGenerator.SubjectKeyIdentifier = new DerOctetString(subKeyId);
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00169B3F File Offset: 0x00167D3F
		public void AddKekRecipient(string keyAlgorithm, KeyParameter key, byte[] keyIdentifier)
		{
			this.AddKekRecipient(keyAlgorithm, key, new KekIdentifier(keyIdentifier, null, null));
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00169B54 File Offset: 0x00167D54
		public void AddKekRecipient(string keyAlgorithm, KeyParameter key, KekIdentifier kekIdentifier)
		{
			KekRecipientInfoGenerator kekRecipientInfoGenerator = new KekRecipientInfoGenerator();
			kekRecipientInfoGenerator.KekIdentifier = kekIdentifier;
			kekRecipientInfoGenerator.KeyEncryptionKeyOID = keyAlgorithm;
			kekRecipientInfoGenerator.KeyEncryptionKey = key;
			this.recipientInfoGenerators.Add(kekRecipientInfoGenerator);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00169B8C File Offset: 0x00167D8C
		public void AddPasswordRecipient(CmsPbeKey pbeKey, string kekAlgorithmOid)
		{
			Pbkdf2Params parameters = new Pbkdf2Params(pbeKey.Salt, pbeKey.IterationCount);
			PasswordRecipientInfoGenerator passwordRecipientInfoGenerator = new PasswordRecipientInfoGenerator();
			passwordRecipientInfoGenerator.KeyDerivationAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPbkdf2, parameters);
			passwordRecipientInfoGenerator.KeyEncryptionKeyOID = kekAlgorithmOid;
			passwordRecipientInfoGenerator.KeyEncryptionKey = pbeKey.GetEncoded(kekAlgorithmOid);
			this.recipientInfoGenerators.Add(passwordRecipientInfoGenerator);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00169BE4 File Offset: 0x00167DE4
		public void AddKeyAgreementRecipient(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, X509Certificate recipientCert, string cekWrapAlgorithm)
		{
			IList list = Platform.CreateArrayList(1);
			list.Add(recipientCert);
			this.AddKeyAgreementRecipients(agreementAlgorithm, senderPrivateKey, senderPublicKey, list, cekWrapAlgorithm);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00169C10 File Offset: 0x00167E10
		public void AddKeyAgreementRecipients(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, ICollection recipientCerts, string cekWrapAlgorithm)
		{
			if (!senderPrivateKey.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "senderPrivateKey");
			}
			if (senderPublicKey.IsPrivate)
			{
				throw new ArgumentException("Expected public key", "senderPublicKey");
			}
			KeyAgreeRecipientInfoGenerator keyAgreeRecipientInfoGenerator = new KeyAgreeRecipientInfoGenerator();
			keyAgreeRecipientInfoGenerator.KeyAgreementOID = new DerObjectIdentifier(agreementAlgorithm);
			keyAgreeRecipientInfoGenerator.KeyEncryptionOID = new DerObjectIdentifier(cekWrapAlgorithm);
			keyAgreeRecipientInfoGenerator.RecipientCerts = recipientCerts;
			keyAgreeRecipientInfoGenerator.SenderKeyPair = new AsymmetricCipherKeyPair(senderPublicKey, senderPrivateKey);
			this.recipientInfoGenerators.Add(keyAgreeRecipientInfoGenerator);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00169C90 File Offset: 0x00167E90
		protected internal virtual AlgorithmIdentifier GetAlgorithmIdentifier(string encryptionOid, KeyParameter encKey, Asn1Encodable asn1Params, out ICipherParameters cipherParameters)
		{
			Asn1Object asn1Object;
			if (asn1Params != null)
			{
				asn1Object = asn1Params.ToAsn1Object();
				cipherParameters = ParameterUtilities.GetCipherParameters(encryptionOid, encKey, asn1Object);
			}
			else
			{
				asn1Object = DerNull.Instance;
				cipherParameters = encKey;
			}
			return new AlgorithmIdentifier(new DerObjectIdentifier(encryptionOid), asn1Object);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00169CCC File Offset: 0x00167ECC
		protected internal virtual Asn1Encodable GenerateAsn1Parameters(string encryptionOid, byte[] encKeyBytes)
		{
			Asn1Encodable result = null;
			try
			{
				if (encryptionOid.Equals(CmsEnvelopedGenerator.RC2Cbc))
				{
					byte[] array = new byte[8];
					this.rand.NextBytes(array);
					int num = encKeyBytes.Length * 8;
					int parameterVersion;
					if (num < 256)
					{
						parameterVersion = (int)CmsEnvelopedGenerator.rc2Table[num];
					}
					else
					{
						parameterVersion = num;
					}
					result = new RC2CbcParameter(parameterVersion, array);
				}
				else
				{
					result = ParameterUtilities.GenerateParameters(encryptionOid, this.rand);
				}
			}
			catch (SecurityUtilityException)
			{
			}
			return result;
		}

		// Token: 0x040024AC RID: 9388
		internal static readonly short[] rc2Table = new short[]
		{
			189,
			86,
			234,
			242,
			162,
			241,
			172,
			42,
			176,
			147,
			209,
			156,
			27,
			51,
			253,
			208,
			48,
			4,
			182,
			220,
			125,
			223,
			50,
			75,
			247,
			203,
			69,
			155,
			49,
			187,
			33,
			90,
			65,
			159,
			225,
			217,
			74,
			77,
			158,
			218,
			160,
			104,
			44,
			195,
			39,
			95,
			128,
			54,
			62,
			238,
			251,
			149,
			26,
			254,
			206,
			168,
			52,
			169,
			19,
			240,
			166,
			63,
			216,
			12,
			120,
			36,
			175,
			35,
			82,
			193,
			103,
			23,
			245,
			102,
			144,
			231,
			232,
			7,
			184,
			96,
			72,
			230,
			30,
			83,
			243,
			146,
			164,
			114,
			140,
			8,
			21,
			110,
			134,
			0,
			132,
			250,
			244,
			127,
			138,
			66,
			25,
			246,
			219,
			205,
			20,
			141,
			80,
			18,
			186,
			60,
			6,
			78,
			236,
			179,
			53,
			17,
			161,
			136,
			142,
			43,
			148,
			153,
			183,
			113,
			116,
			211,
			228,
			191,
			58,
			222,
			150,
			14,
			188,
			10,
			237,
			119,
			252,
			55,
			107,
			3,
			121,
			137,
			98,
			198,
			215,
			192,
			210,
			124,
			106,
			139,
			34,
			163,
			91,
			5,
			93,
			2,
			117,
			213,
			97,
			227,
			24,
			143,
			85,
			81,
			173,
			31,
			11,
			94,
			133,
			229,
			194,
			87,
			99,
			202,
			61,
			108,
			180,
			197,
			204,
			112,
			178,
			145,
			89,
			13,
			71,
			32,
			200,
			79,
			88,
			224,
			1,
			226,
			22,
			56,
			196,
			111,
			59,
			15,
			101,
			70,
			190,
			126,
			45,
			123,
			130,
			249,
			64,
			181,
			29,
			115,
			248,
			235,
			38,
			199,
			135,
			151,
			37,
			84,
			177,
			40,
			170,
			152,
			157,
			165,
			100,
			109,
			122,
			212,
			16,
			129,
			68,
			239,
			73,
			214,
			174,
			46,
			221,
			118,
			92,
			47,
			167,
			28,
			201,
			9,
			105,
			154,
			131,
			207,
			41,
			57,
			185,
			233,
			76,
			255,
			67,
			171
		};

		// Token: 0x040024AD RID: 9389
		public static readonly string DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc.Id;

		// Token: 0x040024AE RID: 9390
		public static readonly string RC2Cbc = PkcsObjectIdentifiers.RC2Cbc.Id;

		// Token: 0x040024AF RID: 9391
		public const string IdeaCbc = "1.3.6.1.4.1.188.7.1.1.2";

		// Token: 0x040024B0 RID: 9392
		public const string Cast5Cbc = "1.2.840.113533.7.66.10";

		// Token: 0x040024B1 RID: 9393
		public static readonly string Aes128Cbc = NistObjectIdentifiers.IdAes128Cbc.Id;

		// Token: 0x040024B2 RID: 9394
		public static readonly string Aes192Cbc = NistObjectIdentifiers.IdAes192Cbc.Id;

		// Token: 0x040024B3 RID: 9395
		public static readonly string Aes256Cbc = NistObjectIdentifiers.IdAes256Cbc.Id;

		// Token: 0x040024B4 RID: 9396
		public static readonly string Camellia128Cbc = NttObjectIdentifiers.IdCamellia128Cbc.Id;

		// Token: 0x040024B5 RID: 9397
		public static readonly string Camellia192Cbc = NttObjectIdentifiers.IdCamellia192Cbc.Id;

		// Token: 0x040024B6 RID: 9398
		public static readonly string Camellia256Cbc = NttObjectIdentifiers.IdCamellia256Cbc.Id;

		// Token: 0x040024B7 RID: 9399
		public static readonly string SeedCbc = KisaObjectIdentifiers.IdSeedCbc.Id;

		// Token: 0x040024B8 RID: 9400
		public static readonly string DesEde3Wrap = PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id;

		// Token: 0x040024B9 RID: 9401
		public static readonly string Aes128Wrap = NistObjectIdentifiers.IdAes128Wrap.Id;

		// Token: 0x040024BA RID: 9402
		public static readonly string Aes192Wrap = NistObjectIdentifiers.IdAes192Wrap.Id;

		// Token: 0x040024BB RID: 9403
		public static readonly string Aes256Wrap = NistObjectIdentifiers.IdAes256Wrap.Id;

		// Token: 0x040024BC RID: 9404
		public static readonly string Camellia128Wrap = NttObjectIdentifiers.IdCamellia128Wrap.Id;

		// Token: 0x040024BD RID: 9405
		public static readonly string Camellia192Wrap = NttObjectIdentifiers.IdCamellia192Wrap.Id;

		// Token: 0x040024BE RID: 9406
		public static readonly string Camellia256Wrap = NttObjectIdentifiers.IdCamellia256Wrap.Id;

		// Token: 0x040024BF RID: 9407
		public static readonly string SeedWrap = KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id;

		// Token: 0x040024C0 RID: 9408
		public static readonly string ECDHSha1Kdf = X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id;

		// Token: 0x040024C1 RID: 9409
		public static readonly string ECMqvSha1Kdf = X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id;

		// Token: 0x040024C2 RID: 9410
		internal readonly IList recipientInfoGenerators = Platform.CreateArrayList();

		// Token: 0x040024C3 RID: 9411
		internal readonly SecureRandom rand;

		// Token: 0x040024C4 RID: 9412
		internal CmsAttributeTableGenerator unprotectedAttributeGenerator;
	}
}
