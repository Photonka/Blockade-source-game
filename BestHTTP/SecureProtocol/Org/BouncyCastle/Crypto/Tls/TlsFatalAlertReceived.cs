using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000458 RID: 1112
	public class TlsFatalAlertReceived : TlsException
	{
		// Token: 0x06002BED RID: 11245 RVA: 0x00119B27 File Offset: 0x00117D27
		public TlsFatalAlertReceived(byte alertDescription) : base(BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls.AlertDescription.GetText(alertDescription), null)
		{
			this.alertDescription = alertDescription;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x00119B3D File Offset: 0x00117D3D
		public virtual byte AlertDescription
		{
			get
			{
				return this.alertDescription;
			}
		}

		// Token: 0x04001D08 RID: 7432
		private readonly byte alertDescription;
	}
}
