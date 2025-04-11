using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000603 RID: 1539
	public abstract class RecipientInformation
	{
		// Token: 0x06003A8A RID: 14986 RVA: 0x0016E69F File Offset: 0x0016C89F
		internal RecipientInformation(AlgorithmIdentifier keyEncAlg, CmsSecureReadable secureReadable)
		{
			this.keyEncAlg = keyEncAlg;
			this.secureReadable = secureReadable;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x0016E6C0 File Offset: 0x0016C8C0
		internal string GetContentAlgorithmName()
		{
			return this.secureReadable.Algorithm.Algorithm.Id;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x0016E6D7 File Offset: 0x0016C8D7
		public RecipientID RecipientID
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x0016E6DF File Offset: 0x0016C8DF
		public AlgorithmIdentifier KeyEncryptionAlgorithmID
		{
			get
			{
				return this.keyEncAlg;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x0016E6E7 File Offset: 0x0016C8E7
		public string KeyEncryptionAlgOid
		{
			get
			{
				return this.keyEncAlg.Algorithm.Id;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x0016E6FC File Offset: 0x0016C8FC
		public Asn1Object KeyEncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.keyEncAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0016E720 File Offset: 0x0016C920
		internal CmsTypedStream GetContentFromSessionKey(KeyParameter sKey)
		{
			CmsReadable readable = this.secureReadable.GetReadable(sKey);
			CmsTypedStream result;
			try
			{
				result = new CmsTypedStream(readable.GetInputStream());
			}
			catch (IOException e)
			{
				throw new CmsException("error getting .", e);
			}
			return result;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0016E768 File Offset: 0x0016C968
		public byte[] GetContent(ICipherParameters key)
		{
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(this.GetContentStream(key).ContentStream);
			}
			catch (IOException arg)
			{
				throw new Exception("unable to parse internal stream: " + arg);
			}
			return result;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x0016E7AC File Offset: 0x0016C9AC
		public byte[] GetMac()
		{
			if (this.resultMac == null)
			{
				object cryptoObject = this.secureReadable.CryptoObject;
				if (cryptoObject is IMac)
				{
					this.resultMac = MacUtilities.DoFinal((IMac)cryptoObject);
				}
			}
			return Arrays.Clone(this.resultMac);
		}

		// Token: 0x06003A93 RID: 14995
		public abstract CmsTypedStream GetContentStream(ICipherParameters key);

		// Token: 0x04002538 RID: 9528
		internal RecipientID rid = new RecipientID();

		// Token: 0x04002539 RID: 9529
		internal AlgorithmIdentifier keyEncAlg;

		// Token: 0x0400253A RID: 9530
		internal CmsSecureReadable secureReadable;

		// Token: 0x0400253B RID: 9531
		private byte[] resultMac;
	}
}
