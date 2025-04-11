using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002EC RID: 748
	public class RevokedStatus : CertificateStatus
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x000D31B9 File Offset: 0x000D13B9
		public RevokedStatus(RevokedInfo info)
		{
			this.info = info;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000D31C8 File Offset: 0x000D13C8
		public RevokedStatus(DateTime revocationDate, int reason)
		{
			this.info = new RevokedInfo(new DerGeneralizedTime(revocationDate), new CrlReason(reason));
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000D31E7 File Offset: 0x000D13E7
		public DateTime RevocationTime
		{
			get
			{
				return this.info.RevocationTime.ToDateTime();
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000D31F9 File Offset: 0x000D13F9
		public bool HasRevocationReason
		{
			get
			{
				return this.info.RevocationReason != null;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000D3209 File Offset: 0x000D1409
		public int RevocationReason
		{
			get
			{
				if (this.info.RevocationReason == null)
				{
					throw new InvalidOperationException("attempt to get a reason where none is available");
				}
				return this.info.RevocationReason.Value.IntValue;
			}
		}

		// Token: 0x040017D8 RID: 6104
		internal readonly RevokedInfo info;
	}
}
