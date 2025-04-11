using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005DD RID: 1501
	internal class CmsEnvelopedHelper
	{
		// Token: 0x06003971 RID: 14705 RVA: 0x00169E88 File Offset: 0x00168088
		static CmsEnvelopedHelper()
		{
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.DesEde3Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes128Cbc, 128);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes192Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes256Cbc, 256);
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.DesEde3Cbc, "DESEDE");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes128Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes192Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes256Cbc, "AES");
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00169F67 File Offset: 0x00168167
		private string GetAsymmetricEncryptionAlgName(string encryptionAlgOid)
		{
			if (PkcsObjectIdentifiers.RsaEncryption.Id.Equals(encryptionAlgOid))
			{
				return "RSA/ECB/PKCS1Padding";
			}
			return encryptionAlgOid;
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00169F84 File Offset: 0x00168184
		internal IBufferedCipher CreateAsymmetricCipher(string encryptionOid)
		{
			string asymmetricEncryptionAlgName = this.GetAsymmetricEncryptionAlgName(encryptionOid);
			if (!asymmetricEncryptionAlgName.Equals(encryptionOid))
			{
				try
				{
					return CipherUtilities.GetCipher(asymmetricEncryptionAlgName);
				}
				catch (SecurityUtilityException)
				{
				}
			}
			return CipherUtilities.GetCipher(encryptionOid);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00169FC8 File Offset: 0x001681C8
		internal IWrapper CreateWrapper(string encryptionOid)
		{
			IWrapper wrapper;
			try
			{
				wrapper = WrapperUtilities.GetWrapper(encryptionOid);
			}
			catch (SecurityUtilityException)
			{
				wrapper = WrapperUtilities.GetWrapper(this.GetAsymmetricEncryptionAlgName(encryptionOid));
			}
			return wrapper;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x0016A000 File Offset: 0x00168200
		internal string GetRfc3211WrapperName(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			string text = (string)CmsEnvelopedHelper.BaseCipherNames[oid];
			if (text == null)
			{
				throw new ArgumentException("no name for " + oid, "oid");
			}
			return text + "RFC3211Wrap";
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x0016A04E File Offset: 0x0016824E
		internal int GetKeySize(string oid)
		{
			if (!CmsEnvelopedHelper.KeySizes.Contains(oid))
			{
				throw new ArgumentException("no keysize for " + oid, "oid");
			}
			return (int)CmsEnvelopedHelper.KeySizes[oid];
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x0016A084 File Offset: 0x00168284
		internal static RecipientInformationStore BuildRecipientInformationStore(Asn1Set recipientInfos, CmsSecureReadable secureReadable)
		{
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != recipientInfos.Count; num++)
			{
				RecipientInfo instance = RecipientInfo.GetInstance(recipientInfos[num]);
				CmsEnvelopedHelper.ReadRecipientInfo(list, instance, secureReadable);
			}
			return new RecipientInformationStore(list);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x0016A0C4 File Offset: 0x001682C4
		private static void ReadRecipientInfo(IList infos, RecipientInfo info, CmsSecureReadable secureReadable)
		{
			Asn1Encodable info2 = info.Info;
			if (info2 is KeyTransRecipientInfo)
			{
				infos.Add(new KeyTransRecipientInformation((KeyTransRecipientInfo)info2, secureReadable));
				return;
			}
			if (info2 is KekRecipientInfo)
			{
				infos.Add(new KekRecipientInformation((KekRecipientInfo)info2, secureReadable));
				return;
			}
			if (info2 is KeyAgreeRecipientInfo)
			{
				KeyAgreeRecipientInformation.ReadRecipientInfo(infos, (KeyAgreeRecipientInfo)info2, secureReadable);
				return;
			}
			if (info2 is PasswordRecipientInfo)
			{
				infos.Add(new PasswordRecipientInformation((PasswordRecipientInfo)info2, secureReadable));
			}
		}

		// Token: 0x040024C5 RID: 9413
		internal static readonly CmsEnvelopedHelper Instance = new CmsEnvelopedHelper();

		// Token: 0x040024C6 RID: 9414
		private static readonly IDictionary KeySizes = Platform.CreateHashtable();

		// Token: 0x040024C7 RID: 9415
		private static readonly IDictionary BaseCipherNames = Platform.CreateHashtable();

		// Token: 0x0200095C RID: 2396
		internal class CmsAuthenticatedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06004EEE RID: 20206 RVA: 0x001B6C2B File Offset: 0x001B4E2B
			internal CmsAuthenticatedSecureReadable(AlgorithmIdentifier algorithm, CmsReadable readable)
			{
				this.algorithm = algorithm;
				this.readable = readable;
			}

			// Token: 0x17000C4B RID: 3147
			// (get) Token: 0x06004EEF RID: 20207 RVA: 0x001B6C41 File Offset: 0x001B4E41
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.algorithm;
				}
			}

			// Token: 0x17000C4C RID: 3148
			// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x001B6C49 File Offset: 0x001B4E49
			public object CryptoObject
			{
				get
				{
					return this.mac;
				}
			}

			// Token: 0x06004EF1 RID: 20209 RVA: 0x001B6C54 File Offset: 0x001B4E54
			public CmsReadable GetReadable(KeyParameter sKey)
			{
				string id = this.algorithm.Algorithm.Id;
				try
				{
					this.mac = MacUtilities.GetMac(id);
					this.mac.Init(sKey);
				}
				catch (SecurityUtilityException e)
				{
					throw new CmsException("couldn't create cipher.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key invalid in message.", e2);
				}
				catch (IOException e3)
				{
					throw new CmsException("error decoding algorithm parameters.", e3);
				}
				CmsReadable result;
				try
				{
					result = new CmsProcessableInputStream(new TeeInputStream(this.readable.GetInputStream(), new MacSink(this.mac)));
				}
				catch (IOException e4)
				{
					throw new CmsException("error reading content.", e4);
				}
				return result;
			}

			// Token: 0x040035B9 RID: 13753
			private AlgorithmIdentifier algorithm;

			// Token: 0x040035BA RID: 13754
			private IMac mac;

			// Token: 0x040035BB RID: 13755
			private CmsReadable readable;
		}

		// Token: 0x0200095D RID: 2397
		internal class CmsEnvelopedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06004EF2 RID: 20210 RVA: 0x001B6D20 File Offset: 0x001B4F20
			internal CmsEnvelopedSecureReadable(AlgorithmIdentifier algorithm, CmsReadable readable)
			{
				this.algorithm = algorithm;
				this.readable = readable;
			}

			// Token: 0x17000C4D RID: 3149
			// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x001B6D36 File Offset: 0x001B4F36
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.algorithm;
				}
			}

			// Token: 0x17000C4E RID: 3150
			// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x001B6D3E File Offset: 0x001B4F3E
			public object CryptoObject
			{
				get
				{
					return this.cipher;
				}
			}

			// Token: 0x06004EF5 RID: 20213 RVA: 0x001B6D48 File Offset: 0x001B4F48
			public CmsReadable GetReadable(KeyParameter sKey)
			{
				try
				{
					this.cipher = CipherUtilities.GetCipher(this.algorithm.Algorithm);
					Asn1Encodable parameters = this.algorithm.Parameters;
					Asn1Object asn1Object = (parameters == null) ? null : parameters.ToAsn1Object();
					ICipherParameters cipherParameters = sKey;
					if (asn1Object != null && !(asn1Object is Asn1Null))
					{
						cipherParameters = ParameterUtilities.GetCipherParameters(this.algorithm.Algorithm, cipherParameters, asn1Object);
					}
					else
					{
						string id = this.algorithm.Algorithm.Id;
						if (id.Equals(CmsEnvelopedGenerator.DesEde3Cbc) || id.Equals("1.3.6.1.4.1.188.7.1.1.2") || id.Equals("1.2.840.113533.7.66.10"))
						{
							cipherParameters = new ParametersWithIV(cipherParameters, new byte[8]);
						}
					}
					this.cipher.Init(false, cipherParameters);
				}
				catch (SecurityUtilityException e)
				{
					throw new CmsException("couldn't create cipher.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key invalid in message.", e2);
				}
				catch (IOException e3)
				{
					throw new CmsException("error decoding algorithm parameters.", e3);
				}
				CmsReadable result;
				try
				{
					result = new CmsProcessableInputStream(new CipherStream(this.readable.GetInputStream(), this.cipher, null));
				}
				catch (IOException e4)
				{
					throw new CmsException("error reading content.", e4);
				}
				return result;
			}

			// Token: 0x040035BC RID: 13756
			private AlgorithmIdentifier algorithm;

			// Token: 0x040035BD RID: 13757
			private IBufferedCipher cipher;

			// Token: 0x040035BE RID: 13758
			private CmsReadable readable;
		}
	}
}
