using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Kisa;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ntt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F4 RID: 1524
	internal class KekRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06003A42 RID: 14914 RVA: 0x00023EF4 File Offset: 0x000220F4
		internal KekRecipientInfoGenerator()
		{
		}

		// Token: 0x17000787 RID: 1927
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x0016D3A7 File Offset: 0x0016B5A7
		internal KekIdentifier KekIdentifier
		{
			set
			{
				this.kekIdentifier = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x0016D3B0 File Offset: 0x0016B5B0
		internal KeyParameter KeyEncryptionKey
		{
			set
			{
				this.keyEncryptionKey = value;
				this.keyEncryptionAlgorithm = KekRecipientInfoGenerator.DetermineKeyEncAlg(this.keyEncryptionKeyOID, this.keyEncryptionKey);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x0016D3D0 File Offset: 0x0016B5D0
		internal string KeyEncryptionKeyOID
		{
			set
			{
				this.keyEncryptionKeyOID = value;
			}
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x0016D3DC File Offset: 0x0016B5DC
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			IWrapper wrapper = KekRecipientInfoGenerator.Helper.CreateWrapper(this.keyEncryptionAlgorithm.Algorithm.Id);
			wrapper.Init(true, new ParametersWithRandom(this.keyEncryptionKey, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			return new RecipientInfo(new KekRecipientInfo(this.kekIdentifier, this.keyEncryptionAlgorithm, encryptedKey));
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x0016D444 File Offset: 0x0016B644
		private static AlgorithmIdentifier DetermineKeyEncAlg(string algorithm, KeyParameter key)
		{
			if (Platform.StartsWith(algorithm, "DES"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCms3DesWrap, DerNull.Instance);
			}
			if (Platform.StartsWith(algorithm, "RC2"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCmsRC2Wrap, new DerInteger(58));
			}
			if (Platform.StartsWith(algorithm, "AES"))
			{
				int num = key.GetKey().Length * 8;
				DerObjectIdentifier algorithm2;
				if (num == 128)
				{
					algorithm2 = NistObjectIdentifiers.IdAes128Wrap;
				}
				else if (num == 192)
				{
					algorithm2 = NistObjectIdentifiers.IdAes192Wrap;
				}
				else
				{
					if (num != 256)
					{
						throw new ArgumentException("illegal keysize in AES");
					}
					algorithm2 = NistObjectIdentifiers.IdAes256Wrap;
				}
				return new AlgorithmIdentifier(algorithm2);
			}
			if (Platform.StartsWith(algorithm, "SEED"))
			{
				return new AlgorithmIdentifier(KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap);
			}
			if (Platform.StartsWith(algorithm, "CAMELLIA"))
			{
				int num2 = key.GetKey().Length * 8;
				DerObjectIdentifier algorithm3;
				if (num2 == 128)
				{
					algorithm3 = NttObjectIdentifiers.IdCamellia128Wrap;
				}
				else if (num2 == 192)
				{
					algorithm3 = NttObjectIdentifiers.IdCamellia192Wrap;
				}
				else
				{
					if (num2 != 256)
					{
						throw new ArgumentException("illegal keysize in Camellia");
					}
					algorithm3 = NttObjectIdentifiers.IdCamellia256Wrap;
				}
				return new AlgorithmIdentifier(algorithm3);
			}
			throw new ArgumentException("unknown algorithm");
		}

		// Token: 0x0400251C RID: 9500
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x0400251D RID: 9501
		private KeyParameter keyEncryptionKey;

		// Token: 0x0400251E RID: 9502
		private string keyEncryptionKeyOID;

		// Token: 0x0400251F RID: 9503
		private KekIdentifier kekIdentifier;

		// Token: 0x04002520 RID: 9504
		private AlgorithmIdentifier keyEncryptionAlgorithm;
	}
}
