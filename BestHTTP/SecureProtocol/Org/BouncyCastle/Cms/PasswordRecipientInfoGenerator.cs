using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005FD RID: 1533
	internal class PasswordRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06003A71 RID: 14961 RVA: 0x00023EF4 File Offset: 0x000220F4
		internal PasswordRecipientInfoGenerator()
		{
		}

		// Token: 0x17000791 RID: 1937
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x0016E2FC File Offset: 0x0016C4FC
		internal AlgorithmIdentifier KeyDerivationAlgorithm
		{
			set
			{
				this.keyDerivationAlgorithm = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x0016E305 File Offset: 0x0016C505
		internal KeyParameter KeyEncryptionKey
		{
			set
			{
				this.keyEncryptionKey = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x0016E30E File Offset: 0x0016C50E
		internal string KeyEncryptionKeyOID
		{
			set
			{
				this.keyEncryptionKeyOID = value;
			}
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x0016E318 File Offset: 0x0016C518
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			string rfc3211WrapperName = PasswordRecipientInfoGenerator.Helper.GetRfc3211WrapperName(this.keyEncryptionKeyOID);
			IWrapper wrapper = PasswordRecipientInfoGenerator.Helper.CreateWrapper(rfc3211WrapperName);
			byte[] array = new byte[Platform.StartsWith(rfc3211WrapperName, "DESEDE") ? 8 : 16];
			random.NextBytes(array);
			ICipherParameters parameters = new ParametersWithIV(this.keyEncryptionKey, array);
			wrapper.Init(true, new ParametersWithRandom(parameters, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			DerSequence parameters2 = new DerSequence(new Asn1Encodable[]
			{
				new DerObjectIdentifier(this.keyEncryptionKeyOID),
				new DerOctetString(array)
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgPwriKek, parameters2);
			return new RecipientInfo(new PasswordRecipientInfo(this.keyDerivationAlgorithm, keyEncryptionAlgorithm, encryptedKey));
		}

		// Token: 0x04002532 RID: 9522
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04002533 RID: 9523
		private AlgorithmIdentifier keyDerivationAlgorithm;

		// Token: 0x04002534 RID: 9524
		private KeyParameter keyEncryptionKey;

		// Token: 0x04002535 RID: 9525
		private string keyEncryptionKeyOID;
	}
}
