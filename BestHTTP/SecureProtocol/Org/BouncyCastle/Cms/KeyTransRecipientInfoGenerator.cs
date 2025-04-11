using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F8 RID: 1528
	internal class KeyTransRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x00023EF4 File Offset: 0x000220F4
		internal KeyTransRecipientInfoGenerator()
		{
		}

		// Token: 0x1700078E RID: 1934
		// (set) Token: 0x06003A5D RID: 14941 RVA: 0x0016DD73 File Offset: 0x0016BF73
		internal X509Certificate RecipientCert
		{
			set
			{
				this.recipientTbsCert = CmsUtilities.GetTbsCertificateStructure(value);
				this.recipientPublicKey = value.GetPublicKey();
				this.info = this.recipientTbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x0016DDA0 File Offset: 0x0016BFA0
		internal AsymmetricKeyParameter RecipientPublicKey
		{
			set
			{
				this.recipientPublicKey = value;
				try
				{
					this.info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(this.recipientPublicKey);
				}
				catch (IOException)
				{
					throw new ArgumentException("can't extract key algorithm from this key");
				}
			}
		}

		// Token: 0x17000790 RID: 1936
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x0016DDE4 File Offset: 0x0016BFE4
		internal Asn1OctetString SubjectKeyIdentifier
		{
			set
			{
				this.subjectKeyIdentifier = value;
			}
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x0016DDF0 File Offset: 0x0016BFF0
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			AlgorithmIdentifier algorithmID = this.info.AlgorithmID;
			IWrapper wrapper = KeyTransRecipientInfoGenerator.Helper.CreateWrapper(algorithmID.Algorithm.Id);
			wrapper.Init(true, new ParametersWithRandom(this.recipientPublicKey, random));
			byte[] str = wrapper.Wrap(key, 0, key.Length);
			RecipientIdentifier rid;
			if (this.recipientTbsCert != null)
			{
				rid = new RecipientIdentifier(new IssuerAndSerialNumber(this.recipientTbsCert.Issuer, this.recipientTbsCert.SerialNumber.Value));
			}
			else
			{
				rid = new RecipientIdentifier(this.subjectKeyIdentifier);
			}
			return new RecipientInfo(new KeyTransRecipientInfo(rid, algorithmID, new DerOctetString(str)));
		}

		// Token: 0x04002529 RID: 9513
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x0400252A RID: 9514
		private TbsCertificateStructure recipientTbsCert;

		// Token: 0x0400252B RID: 9515
		private AsymmetricKeyParameter recipientPublicKey;

		// Token: 0x0400252C RID: 9516
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x0400252D RID: 9517
		private SubjectPublicKeyInfo info;
	}
}
