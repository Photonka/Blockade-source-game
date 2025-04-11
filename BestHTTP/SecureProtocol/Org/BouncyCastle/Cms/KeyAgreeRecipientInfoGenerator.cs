using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Ecc;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F6 RID: 1526
	internal class KeyAgreeRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06003A4B RID: 14923 RVA: 0x00023EF4 File Offset: 0x000220F4
		internal KeyAgreeRecipientInfoGenerator()
		{
		}

		// Token: 0x1700078A RID: 1930
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x0016D654 File Offset: 0x0016B854
		internal DerObjectIdentifier KeyAgreementOID
		{
			set
			{
				this.keyAgreementOID = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (set) Token: 0x06003A4D RID: 14925 RVA: 0x0016D65D File Offset: 0x0016B85D
		internal DerObjectIdentifier KeyEncryptionOID
		{
			set
			{
				this.keyEncryptionOID = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (set) Token: 0x06003A4E RID: 14926 RVA: 0x0016D666 File Offset: 0x0016B866
		internal ICollection RecipientCerts
		{
			set
			{
				this.recipientCerts = Platform.CreateArrayList(value);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (set) Token: 0x06003A4F RID: 14927 RVA: 0x0016D674 File Offset: 0x0016B874
		internal AsymmetricCipherKeyPair SenderKeyPair
		{
			set
			{
				this.senderKeyPair = value;
			}
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x0016D680 File Offset: 0x0016B880
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			AsymmetricKeyParameter @public = this.senderKeyPair.Public;
			ICipherParameters cipherParameters = this.senderKeyPair.Private;
			OriginatorIdentifierOrKey originator;
			try
			{
				originator = new OriginatorIdentifierOrKey(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(@public));
			}
			catch (IOException arg)
			{
				throw new InvalidKeyException("cannot extract originator public key: " + arg);
			}
			Asn1OctetString ukm = null;
			if (this.keyAgreementOID.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				try
				{
					IAsymmetricCipherKeyPairGenerator keyPairGenerator = GeneratorUtilities.GetKeyPairGenerator(this.keyAgreementOID);
					keyPairGenerator.Init(((ECPublicKeyParameters)@public).CreateKeyGenerationParameters(random));
					AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
					ukm = new DerOctetString(new MQVuserKeyingMaterial(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(asymmetricCipherKeyPair.Public), null));
					cipherParameters = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters, (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private, (ECPublicKeyParameters)asymmetricCipherKeyPair.Public);
				}
				catch (IOException arg2)
				{
					throw new InvalidKeyException("cannot extract MQV ephemeral public key: " + arg2);
				}
				catch (SecurityUtilityException arg3)
				{
					throw new InvalidKeyException("cannot determine MQV ephemeral key pair parameters from public key: " + arg3);
				}
			}
			DerSequence parameters = new DerSequence(new Asn1Encodable[]
			{
				this.keyEncryptionOID,
				DerNull.Instance
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(this.keyAgreementOID, parameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.recipientCerts)
			{
				X509Certificate x509Certificate = (X509Certificate)obj;
				TbsCertificateStructure instance;
				try
				{
					instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetTbsCertificate()));
				}
				catch (Exception)
				{
					throw new ArgumentException("can't extract TBS structure from certificate");
				}
				KeyAgreeRecipientIdentifier id = new KeyAgreeRecipientIdentifier(new IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value));
				ICipherParameters cipherParameters2 = x509Certificate.GetPublicKey();
				if (this.keyAgreementOID.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
				{
					cipherParameters2 = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters2, (ECPublicKeyParameters)cipherParameters2);
				}
				IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(this.keyAgreementOID, this.keyEncryptionOID.Id);
				basicAgreementWithKdf.Init(new ParametersWithRandom(cipherParameters, random));
				BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters2);
				int qLength = GeneratorUtilities.GetDefaultKeySize(this.keyEncryptionOID) / 8;
				byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
				KeyParameter parameters2 = ParameterUtilities.CreateKeyParameter(this.keyEncryptionOID, keyBytes);
				IWrapper wrapper = KeyAgreeRecipientInfoGenerator.Helper.CreateWrapper(this.keyEncryptionOID.Id);
				wrapper.Init(true, new ParametersWithRandom(parameters2, random));
				Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new RecipientEncryptedKey(id, encryptedKey)
				});
			}
			return new RecipientInfo(new KeyAgreeRecipientInfo(originator, ukm, keyEncryptionAlgorithm, new DerSequence(asn1EncodableVector)));
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x0016D998 File Offset: 0x0016BB98
		private static OriginatorPublicKey CreateOriginatorPublicKey(AsymmetricKeyParameter publicKey)
		{
			SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			return new OriginatorPublicKey(new AlgorithmIdentifier(subjectPublicKeyInfo.AlgorithmID.Algorithm, DerNull.Instance), subjectPublicKeyInfo.PublicKeyData.GetBytes());
		}

		// Token: 0x04002522 RID: 9506
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04002523 RID: 9507
		private DerObjectIdentifier keyAgreementOID;

		// Token: 0x04002524 RID: 9508
		private DerObjectIdentifier keyEncryptionOID;

		// Token: 0x04002525 RID: 9509
		private IList recipientCerts;

		// Token: 0x04002526 RID: 9510
		private AsymmetricCipherKeyPair senderKeyPair;
	}
}
