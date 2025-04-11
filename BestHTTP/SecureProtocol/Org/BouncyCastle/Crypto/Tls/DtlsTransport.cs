using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000412 RID: 1042
	public class DtlsTransport : DatagramTransport
	{
		// Token: 0x06002A21 RID: 10785 RVA: 0x00114777 File Offset: 0x00112977
		internal DtlsTransport(DtlsRecordLayer recordLayer)
		{
			this.mRecordLayer = recordLayer;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00114786 File Offset: 0x00112986
		public virtual int GetReceiveLimit()
		{
			return this.mRecordLayer.GetReceiveLimit();
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00114793 File Offset: 0x00112993
		public virtual int GetSendLimit()
		{
			return this.mRecordLayer.GetSendLimit();
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x001147A0 File Offset: 0x001129A0
		public virtual int Receive(byte[] buf, int off, int len, int waitMillis)
		{
			int result;
			try
			{
				result = this.mRecordLayer.Receive(buf, off, len, waitMillis);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.mRecordLayer.Fail(tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.mRecordLayer.Fail(80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.mRecordLayer.Fail(80);
				throw new TlsFatalAlert(80, alertCause);
			}
			return result;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00114824 File Offset: 0x00112A24
		public virtual void Send(byte[] buf, int off, int len)
		{
			try
			{
				this.mRecordLayer.Send(buf, off, len);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.mRecordLayer.Fail(tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.mRecordLayer.Fail(80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.mRecordLayer.Fail(80);
				throw new TlsFatalAlert(80, alertCause);
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x001148A4 File Offset: 0x00112AA4
		public virtual void Close()
		{
			this.mRecordLayer.Close();
		}

		// Token: 0x04001BB1 RID: 7089
		private readonly DtlsRecordLayer mRecordLayer;
	}
}
