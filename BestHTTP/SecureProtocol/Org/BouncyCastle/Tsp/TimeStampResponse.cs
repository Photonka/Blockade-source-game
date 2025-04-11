using System;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000290 RID: 656
	public class TimeStampResponse
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x000BBC28 File Offset: 0x000B9E28
		public TimeStampResponse(TimeStampResp resp)
		{
			this.resp = resp;
			if (resp.TimeStampToken != null)
			{
				this.timeStampToken = new TimeStampToken(resp.TimeStampToken);
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000BBC50 File Offset: 0x000B9E50
		public TimeStampResponse(byte[] resp) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(resp)))
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000BBC63 File Offset: 0x000B9E63
		public TimeStampResponse(Stream input) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(input)))
		{
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000BBC78 File Offset: 0x000B9E78
		private static TimeStampResp readTimeStampResp(Asn1InputStream input)
		{
			TimeStampResp instance;
			try
			{
				instance = TimeStampResp.GetInstance(input.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new TspException("malformed timestamp response: " + ex, ex);
			}
			catch (InvalidCastException ex2)
			{
				throw new TspException("malformed timestamp response: " + ex2, ex2);
			}
			return instance;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000BBCD8 File Offset: 0x000B9ED8
		public int Status
		{
			get
			{
				return this.resp.Status.Status.IntValue;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000BBCF0 File Offset: 0x000B9EF0
		public string GetStatusString()
		{
			if (this.resp.Status.StatusString == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			PkiFreeText statusString = this.resp.Status.StatusString;
			for (int num = 0; num != statusString.Count; num++)
			{
				stringBuilder.Append(statusString[num].GetString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000BBD52 File Offset: 0x000B9F52
		public PkiFailureInfo GetFailInfo()
		{
			if (this.resp.Status.FailInfo == null)
			{
				return null;
			}
			return new PkiFailureInfo(this.resp.Status.FailInfo);
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x000BBD7D File Offset: 0x000B9F7D
		public TimeStampToken TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000BBD88 File Offset: 0x000B9F88
		public void Validate(TimeStampRequest request)
		{
			TimeStampToken timeStampToken = this.TimeStampToken;
			if (timeStampToken != null)
			{
				TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
				if (request.Nonce != null && !request.Nonce.Equals(timeStampInfo.Nonce))
				{
					throw new TspValidationException("response contains wrong nonce value.");
				}
				if (this.Status != 0 && this.Status != 1)
				{
					throw new TspValidationException("time stamp token found in failed request.");
				}
				if (!Arrays.ConstantTimeAreEqual(request.GetMessageImprintDigest(), timeStampInfo.GetMessageImprintDigest()))
				{
					throw new TspValidationException("response for different message imprint digest.");
				}
				if (!timeStampInfo.MessageImprintAlgOid.Equals(request.MessageImprintAlgOid))
				{
					throw new TspValidationException("response for different message imprint algorithm.");
				}
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
				if (attribute == null && attribute2 == null)
				{
					throw new TspValidationException("no signing certificate attribute present.");
				}
				if (attribute != null)
				{
				}
				if (request.ReqPolicy != null && !request.ReqPolicy.Equals(timeStampInfo.Policy))
				{
					throw new TspValidationException("TSA policy wrong for request.");
				}
			}
			else if (this.Status == 0 || this.Status == 1)
			{
				throw new TspValidationException("no time stamp token found and one expected.");
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000BBE9F File Offset: 0x000BA09F
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x04001700 RID: 5888
		private TimeStampResp resp;

		// Token: 0x04001701 RID: 5889
		private TimeStampToken timeStampToken;
	}
}
