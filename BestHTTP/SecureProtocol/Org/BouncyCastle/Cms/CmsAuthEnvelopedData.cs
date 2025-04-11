using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D1 RID: 1489
	internal class CmsAuthEnvelopedData
	{
		// Token: 0x0600392E RID: 14638 RVA: 0x00168E78 File Offset: 0x00167078
		public CmsAuthEnvelopedData(byte[] authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x00168E86 File Offset: 0x00167086
		public CmsAuthEnvelopedData(Stream authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x00168E94 File Offset: 0x00167094
		public CmsAuthEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthEnvelopedData instance = AuthEnvelopedData.GetInstance(contentInfo.Content);
			this.originator = instance.OriginatorInfo;
			Asn1Set recipientInfos = instance.RecipientInfos;
			EncryptedContentInfo authEncryptedContentInfo = instance.AuthEncryptedContentInfo;
			this.authEncAlg = authEncryptedContentInfo.ContentEncryptionAlgorithm;
			CmsSecureReadable secureReadable = new CmsAuthEnvelopedData.AuthEnvelopedSecureReadable(this);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.authAttrs = instance.AuthAttrs;
			this.mac = instance.Mac.GetOctets();
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x0400248C RID: 9356
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x0400248D RID: 9357
		internal ContentInfo contentInfo;

		// Token: 0x0400248E RID: 9358
		private OriginatorInfo originator;

		// Token: 0x0400248F RID: 9359
		private AlgorithmIdentifier authEncAlg;

		// Token: 0x04002490 RID: 9360
		private Asn1Set authAttrs;

		// Token: 0x04002491 RID: 9361
		private byte[] mac;

		// Token: 0x04002492 RID: 9362
		private Asn1Set unauthAttrs;

		// Token: 0x02000959 RID: 2393
		private class AuthEnvelopedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06004EE2 RID: 20194 RVA: 0x001B6AC0 File Offset: 0x001B4CC0
			internal AuthEnvelopedSecureReadable(CmsAuthEnvelopedData parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000C49 RID: 3145
			// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x001B6ACF File Offset: 0x001B4CCF
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.parent.authEncAlg;
				}
			}

			// Token: 0x17000C4A RID: 3146
			// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x0008F86E File Offset: 0x0008DA6E
			public object CryptoObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004EE5 RID: 20197 RVA: 0x001B6ADC File Offset: 0x001B4CDC
			public CmsReadable GetReadable(KeyParameter key)
			{
				throw new CmsException("AuthEnveloped data decryption not yet implemented");
			}

			// Token: 0x040035AF RID: 13743
			private readonly CmsAuthEnvelopedData parent;
		}
	}
}
