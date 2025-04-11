using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E1 RID: 993
	public abstract class AbstractTlsPeer : TlsPeer
	{
		// Token: 0x060028A2 RID: 10402 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool RequiresExtendedMasterSecret()
		{
			return false;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool ShouldUseGmtUnixTime()
		{
			return false;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0010F040 File Offset: 0x0010D240
		public virtual void NotifySecureRenegotiation(bool secureRenegotiation)
		{
			if (!secureRenegotiation)
			{
				throw new TlsFatalAlert(40);
			}
		}

		// Token: 0x060028A5 RID: 10405
		public abstract TlsCompression GetCompression();

		// Token: 0x060028A6 RID: 10406
		public abstract TlsCipher GetCipher();

		// Token: 0x060028A7 RID: 10407 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifyAlertRaised(byte alertLevel, byte alertDescription, string message, Exception cause)
		{
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifyAlertReceived(byte alertLevel, byte alertDescription)
		{
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifyHandshakeComplete()
		{
		}
	}
}
