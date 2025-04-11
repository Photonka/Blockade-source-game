using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E4 RID: 740
	public class OcspResp
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x000D28BF File Offset: 0x000D0ABF
		public OcspResp(OcspResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000D28CE File Offset: 0x000D0ACE
		public OcspResp(byte[] resp) : this(new Asn1InputStream(resp))
		{
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000D28DC File Offset: 0x000D0ADC
		public OcspResp(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000D28EC File Offset: 0x000D0AEC
		private OcspResp(Asn1InputStream aIn)
		{
			try
			{
				this.resp = OcspResponse.GetInstance(aIn.ReadObject());
			}
			catch (Exception ex)
			{
				throw new IOException("malformed response: " + ex.Message, ex);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000D293C File Offset: 0x000D0B3C
		public int Status
		{
			get
			{
				return this.resp.ResponseStatus.Value.IntValue;
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000D2954 File Offset: 0x000D0B54
		public object GetResponseObject()
		{
			ResponseBytes responseBytes = this.resp.ResponseBytes;
			if (responseBytes == null)
			{
				return null;
			}
			if (responseBytes.ResponseType.Equals(OcspObjectIdentifiers.PkixOcspBasic))
			{
				try
				{
					return new BasicOcspResp(BasicOcspResponse.GetInstance(Asn1Object.FromByteArray(responseBytes.Response.GetOctets())));
				}
				catch (Exception ex)
				{
					throw new OcspException("problem decoding object: " + ex, ex);
				}
			}
			return responseBytes.Response;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000D29CC File Offset: 0x000D0BCC
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000D29DC File Offset: 0x000D0BDC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			OcspResp ocspResp = obj as OcspResp;
			return ocspResp != null && this.resp.Equals(ocspResp.resp);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000D2A0C File Offset: 0x000D0C0C
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x040017C5 RID: 6085
		private OcspResponse resp;
	}
}
