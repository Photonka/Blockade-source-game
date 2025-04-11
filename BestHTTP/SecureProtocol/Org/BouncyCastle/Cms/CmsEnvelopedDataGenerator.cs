using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D9 RID: 1497
	public class CmsEnvelopedDataGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x0600394E RID: 14670 RVA: 0x00168E67 File Offset: 0x00167067
		public CmsEnvelopedDataGenerator()
		{
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x00168E6F File Offset: 0x0016706F
		public CmsEnvelopedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x001693D4 File Offset: 0x001675D4
		private CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, CipherKeyGenerator keyGen)
		{
			AlgorithmIdentifier contentEncryptionAlgorithm = null;
			KeyParameter keyParameter;
			Asn1OctetString encryptedContent;
			try
			{
				byte[] array = keyGen.GenerateKey();
				keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
				Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
				ICipherParameters parameters;
				contentEncryptionAlgorithm = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out parameters);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encryptionOid);
				cipher.Init(true, new ParametersWithRandom(parameters, this.rand));
				MemoryStream memoryStream = new MemoryStream();
				CipherStream cipherStream = new CipherStream(memoryStream, null, cipher);
				content.Write(cipherStream);
				Platform.Dispose(cipherStream);
				encryptedContent = new BerOctetString(memoryStream.ToArray());
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
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						recipientInfoGenerator.Generate(keyParameter, this.rand)
					});
				}
				catch (InvalidKeyException e4)
				{
					throw new CmsException("key inappropriate for algorithm.", e4);
				}
				catch (GeneralSecurityException e5)
				{
					throw new CmsException("error making encrypted content.", e5);
				}
			}
			EncryptedContentInfo encryptedContentInfo = new EncryptedContentInfo(CmsObjectIdentifiers.Data, contentEncryptionAlgorithm, encryptedContent);
			Asn1Set unprotectedAttrs = null;
			if (this.unprotectedAttributeGenerator != null)
			{
				unprotectedAttrs = new BerSet(this.unprotectedAttributeGenerator.GetAttributes(Platform.CreateHashtable()).ToAsn1EncodableVector());
			}
			return new CmsEnvelopedData(new ContentInfo(CmsObjectIdentifiers.EnvelopedData, new EnvelopedData(null, new DerSet(asn1EncodableVector), encryptedContentInfo, unprotectedAttrs)));
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x001695A8 File Offset: 0x001677A8
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x00169600 File Offset: 0x00167800
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, int keySize)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}
	}
}
