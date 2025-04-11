using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005FE RID: 1534
	public class PasswordRecipientInformation : RecipientInformation
	{
		// Token: 0x06003A77 RID: 14967 RVA: 0x0016E3E5 File Offset: 0x0016C5E5
		internal PasswordRecipientInformation(PasswordRecipientInfo info, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = new RecipientID();
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x0016E406 File Offset: 0x0016C606
		public virtual AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.info.KeyDerivationAlgorithm;
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0016E414 File Offset: 0x0016C614
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)AlgorithmIdentifier.GetInstance(this.info.KeyEncryptionAlgorithm).Parameters;
				byte[] octets = this.info.EncryptedKey.GetOctets();
				string id = DerObjectIdentifier.GetInstance(asn1Sequence[0]).Id;
				IWrapper wrapper = WrapperUtilities.GetWrapper(CmsEnvelopedHelper.Instance.GetRfc3211WrapperName(id));
				byte[] octets2 = Asn1OctetString.GetInstance(asn1Sequence[1]).GetOctets();
				ICipherParameters parameters = ((CmsPbeKey)key).GetEncoded(id);
				parameters = new ParametersWithIV(parameters, octets2);
				wrapper.Init(false, parameters);
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

		// Token: 0x04002536 RID: 9526
		private readonly PasswordRecipientInfo info;
	}
}
