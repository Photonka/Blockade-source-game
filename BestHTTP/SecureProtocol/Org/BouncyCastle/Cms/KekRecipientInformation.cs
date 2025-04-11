using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F5 RID: 1525
	public class KekRecipientInformation : RecipientInformation
	{
		// Token: 0x06003A49 RID: 14921 RVA: 0x0016D570 File Offset: 0x0016B770
		internal KekRecipientInformation(KekRecipientInfo info, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = new RecipientID();
			KekIdentifier kekID = info.KekID;
			this.rid.KeyIdentifier = kekID.KeyIdentifier.GetOctets();
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0016D5BC File Offset: 0x0016B7BC
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				byte[] octets = this.info.EncryptedKey.GetOctets();
				IWrapper wrapper = WrapperUtilities.GetWrapper(this.keyEncAlg.Algorithm.Id);
				wrapper.Init(false, key);
				KeyParameter sKey = ParameterUtilities.CreateKeyParameter(base.GetContentAlgorithmName(), wrapper.Unwrap(octets, 0, octets.Length));
				contentFromSessionKey = base.GetContentFromSessionKey(sKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			return contentFromSessionKey;
		}

		// Token: 0x04002521 RID: 9505
		private KekRecipientInfo info;
	}
}
