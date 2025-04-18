﻿using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005CD RID: 1485
	public class CmsAuthenticatedDataGenerator : CmsAuthenticatedGenerator
	{
		// Token: 0x06003917 RID: 14615 RVA: 0x00168759 File Offset: 0x00166959
		public CmsAuthenticatedDataGenerator()
		{
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x00168761 File Offset: 0x00166961
		public CmsAuthenticatedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x0016876C File Offset: 0x0016696C
		private CmsAuthenticatedData Generate(CmsProcessable content, string macOid, CipherKeyGenerator keyGen)
		{
			KeyParameter keyParameter;
			AlgorithmIdentifier algorithmIdentifier;
			Asn1OctetString content2;
			Asn1OctetString mac2;
			try
			{
				byte[] array = keyGen.GenerateKey();
				keyParameter = ParameterUtilities.CreateKeyParameter(macOid, array);
				Asn1Encodable asn1Params = this.GenerateAsn1Parameters(macOid, array);
				ICipherParameters cipherParameters;
				algorithmIdentifier = this.GetAlgorithmIdentifier(macOid, keyParameter, asn1Params, out cipherParameters);
				IMac mac = MacUtilities.GetMac(macOid);
				mac.Init(keyParameter);
				MemoryStream memoryStream = new MemoryStream();
				Stream stream = new TeeOutputStream(memoryStream, new MacSink(mac));
				content.Write(stream);
				Platform.Dispose(stream);
				content2 = new BerOctetString(memoryStream.ToArray());
				mac2 = new DerOctetString(MacUtilities.DoFinal(mac));
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
			ContentInfo encapsulatedContent = new ContentInfo(CmsObjectIdentifiers.Data, content2);
			return new CmsAuthenticatedData(new ContentInfo(CmsObjectIdentifiers.AuthenticatedData, new AuthenticatedData(null, new DerSet(asn1EncodableVector), algorithmIdentifier, null, encapsulatedContent, null, mac2, null)));
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x00168920 File Offset: 0x00166B20
		public CmsAuthenticatedData Generate(CmsProcessable content, string encryptionOid)
		{
			CmsAuthenticatedData result;
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
	}
}
