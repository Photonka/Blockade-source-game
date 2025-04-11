using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002EA RID: 746
	public class RespData : X509ExtensionBase
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x000D303F File Offset: 0x000D123F
		public RespData(ResponseData data)
		{
			this.data = data;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000D304E File Offset: 0x000D124E
		public int Version
		{
			get
			{
				return this.data.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000D3067 File Offset: 0x000D1267
		public RespID GetResponderId()
		{
			return new RespID(this.data.ResponderID);
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000D3079 File Offset: 0x000D1279
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000D308C File Offset: 0x000D128C
		public SingleResp[] GetResponses()
		{
			Asn1Sequence responses = this.data.Responses;
			SingleResp[] array = new SingleResp[responses.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
			}
			return array;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000D30D4 File Offset: 0x000D12D4
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000D30E1 File Offset: 0x000D12E1
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x040017D6 RID: 6102
		internal readonly ResponseData data;
	}
}
