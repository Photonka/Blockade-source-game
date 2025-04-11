using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E5 RID: 741
	public class OCSPRespGenerator
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x000D2A1C File Offset: 0x000D0C1C
		public OcspResp Generate(int status, object response)
		{
			if (response == null)
			{
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), null));
			}
			if (response is BasicOcspResp)
			{
				BasicOcspResp basicOcspResp = (BasicOcspResp)response;
				Asn1OctetString response2;
				try
				{
					response2 = new DerOctetString(basicOcspResp.GetEncoded());
				}
				catch (Exception e)
				{
					throw new OcspException("can't encode object.", e);
				}
				ResponseBytes responseBytes = new ResponseBytes(OcspObjectIdentifiers.PkixOcspBasic, response2);
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), responseBytes));
			}
			throw new OcspException("unknown response object");
		}

		// Token: 0x040017C6 RID: 6086
		public const int Successful = 0;

		// Token: 0x040017C7 RID: 6087
		public const int MalformedRequest = 1;

		// Token: 0x040017C8 RID: 6088
		public const int InternalError = 2;

		// Token: 0x040017C9 RID: 6089
		public const int TryLater = 3;

		// Token: 0x040017CA RID: 6090
		public const int SigRequired = 5;

		// Token: 0x040017CB RID: 6091
		public const int Unauthorized = 6;
	}
}
