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
using BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F7 RID: 1527
	public class KeyAgreeRecipientInformation : RecipientInformation
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x0016D9E0 File Offset: 0x0016BBE0
		internal static void ReadRecipientInfo(IList infos, KeyAgreeRecipientInfo info, CmsSecureReadable secureReadable)
		{
			try
			{
				foreach (object obj in info.RecipientEncryptedKeys)
				{
					RecipientEncryptedKey instance = RecipientEncryptedKey.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
					RecipientID recipientID = new RecipientID();
					KeyAgreeRecipientIdentifier identifier = instance.Identifier;
					IssuerAndSerialNumber issuerAndSerialNumber = identifier.IssuerAndSerialNumber;
					if (issuerAndSerialNumber != null)
					{
						recipientID.Issuer = issuerAndSerialNumber.Name;
						recipientID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
					}
					else
					{
						RecipientKeyIdentifier rkeyID = identifier.RKeyID;
						recipientID.SubjectKeyIdentifier = rkeyID.SubjectKeyIdentifier.GetOctets();
					}
					infos.Add(new KeyAgreeRecipientInformation(info, recipientID, instance.EncryptedKey, secureReadable));
				}
			}
			catch (IOException innerException)
			{
				throw new ArgumentException("invalid rid in KeyAgreeRecipientInformation", innerException);
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0016DAC8 File Offset: 0x0016BCC8
		internal KeyAgreeRecipientInformation(KeyAgreeRecipientInfo info, RecipientID rid, Asn1OctetString encryptedKey, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = rid;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0016DAF0 File Offset: 0x0016BCF0
		private AsymmetricKeyParameter GetSenderPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorIdentifierOrKey originator)
		{
			OriginatorPublicKey originatorPublicKey = originator.OriginatorPublicKey;
			if (originatorPublicKey != null)
			{
				return this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, originatorPublicKey);
			}
			OriginatorID originatorID = new OriginatorID();
			IssuerAndSerialNumber issuerAndSerialNumber = originator.IssuerAndSerialNumber;
			if (issuerAndSerialNumber != null)
			{
				originatorID.Issuer = issuerAndSerialNumber.Name;
				originatorID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
			}
			else
			{
				SubjectKeyIdentifier subjectKeyIdentifier = originator.SubjectKeyIdentifier;
				originatorID.SubjectKeyIdentifier = subjectKeyIdentifier.GetKeyIdentifier();
			}
			return this.GetPublicKeyFromOriginatorID(originatorID);
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0016DB59 File Offset: 0x0016BD59
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorPublicKey originatorPublicKey)
		{
			return PublicKeyFactory.CreateKey(new SubjectPublicKeyInfo(PrivateKeyInfoFactory.CreatePrivateKeyInfo(receiverPrivateKey).PrivateKeyAlgorithm, originatorPublicKey.PublicKey.GetBytes()));
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x0016DB7B File Offset: 0x0016BD7B
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorID(OriginatorID origID)
		{
			throw new CmsException("No support for 'originator' as IssuerAndSerialNumber or SubjectKeyIdentifier");
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x0016DB88 File Offset: 0x0016BD88
		private KeyParameter CalculateAgreedWrapKey(string wrapAlg, AsymmetricKeyParameter senderPublicKey, AsymmetricKeyParameter receiverPrivateKey)
		{
			DerObjectIdentifier algorithm = this.keyEncAlg.Algorithm;
			ICipherParameters cipherParameters = senderPublicKey;
			ICipherParameters cipherParameters2 = receiverPrivateKey;
			if (algorithm.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				MQVuserKeyingMaterial instance = MQVuserKeyingMaterial.GetInstance(Asn1Object.FromByteArray(this.info.UserKeyingMaterial.GetOctets()));
				AsymmetricKeyParameter publicKeyFromOriginatorPublicKey = this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, instance.EphemeralPublicKey);
				cipherParameters = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters, (ECPublicKeyParameters)publicKeyFromOriginatorPublicKey);
				cipherParameters2 = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters2, (ECPrivateKeyParameters)cipherParameters2);
			}
			IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(algorithm, wrapAlg);
			basicAgreementWithKdf.Init(cipherParameters2);
			BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters);
			int qLength = GeneratorUtilities.GetDefaultKeySize(wrapAlg) / 8;
			byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
			return ParameterUtilities.CreateKeyParameter(wrapAlg, keyBytes);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x0016DC34 File Offset: 0x0016BE34
		private KeyParameter UnwrapSessionKey(string wrapAlg, KeyParameter agreedKey)
		{
			byte[] octets = this.encryptedKey.GetOctets();
			IWrapper wrapper = WrapperUtilities.GetWrapper(wrapAlg);
			wrapper.Init(false, agreedKey);
			byte[] keyBytes = wrapper.Unwrap(octets, 0, octets.Length);
			return ParameterUtilities.CreateKeyParameter(base.GetContentAlgorithmName(), keyBytes);
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x0016DC74 File Offset: 0x0016BE74
		internal KeyParameter GetSessionKey(AsymmetricKeyParameter receiverPrivateKey)
		{
			KeyParameter result;
			try
			{
				string id = DerObjectIdentifier.GetInstance(Asn1Sequence.GetInstance(this.keyEncAlg.Parameters)[0]).Id;
				AsymmetricKeyParameter senderPublicKey = this.GetSenderPublicKey(receiverPrivateKey, this.info.Originator);
				KeyParameter agreedKey = this.CalculateAgreedWrapKey(id, senderPublicKey, receiverPrivateKey);
				result = this.UnwrapSessionKey(id, agreedKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (Exception e3)
			{
				throw new CmsException("originator key invalid.", e3);
			}
			return result;
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x0016DD20 File Offset: 0x0016BF20
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			if (!(key is AsymmetricKeyParameter))
			{
				throw new ArgumentException("KeyAgreement requires asymmetric key", "key");
			}
			AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)key;
			if (!asymmetricKeyParameter.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "key");
			}
			KeyParameter sessionKey = this.GetSessionKey(asymmetricKeyParameter);
			return base.GetContentFromSessionKey(sessionKey);
		}

		// Token: 0x04002527 RID: 9511
		private KeyAgreeRecipientInfo info;

		// Token: 0x04002528 RID: 9512
		private Asn1OctetString encryptedKey;
	}
}
