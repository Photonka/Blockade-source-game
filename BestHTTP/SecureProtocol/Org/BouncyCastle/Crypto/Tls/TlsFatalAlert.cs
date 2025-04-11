using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000457 RID: 1111
	public class TlsFatalAlert : TlsException
	{
		// Token: 0x06002BEA RID: 11242 RVA: 0x00119AFF File Offset: 0x00117CFF
		public TlsFatalAlert(byte alertDescription) : this(alertDescription, null)
		{
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00119B09 File Offset: 0x00117D09
		public TlsFatalAlert(byte alertDescription, Exception alertCause) : base(BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls.AlertDescription.GetText(alertDescription), alertCause)
		{
			this.alertDescription = alertDescription;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x00119B1F File Offset: 0x00117D1F
		public virtual byte AlertDescription
		{
			get
			{
				return this.alertDescription;
			}
		}

		// Token: 0x04001D07 RID: 7431
		private readonly byte alertDescription;
	}
}
