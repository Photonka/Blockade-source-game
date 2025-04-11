using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000410 RID: 1040
	internal class DtlsReplayWindow
	{
		// Token: 0x06002A0B RID: 10763 RVA: 0x00113AE0 File Offset: 0x00111CE0
		internal bool ShouldDiscard(long seq)
		{
			if ((seq & 281474976710655L) != seq)
			{
				return true;
			}
			if (seq <= this.mLatestConfirmedSeq)
			{
				long num = this.mLatestConfirmedSeq - seq;
				if (num >= 64L)
				{
					return true;
				}
				if ((this.mBitmap & 1L << (int)num) != 0L)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00113B2C File Offset: 0x00111D2C
		internal void ReportAuthenticated(long seq)
		{
			if ((seq & 281474976710655L) != seq)
			{
				throw new ArgumentException("out of range", "seq");
			}
			if (seq <= this.mLatestConfirmedSeq)
			{
				long num = this.mLatestConfirmedSeq - seq;
				if (num < 64L)
				{
					this.mBitmap |= 1L << (int)num;
					return;
				}
			}
			else
			{
				long num2 = seq - this.mLatestConfirmedSeq;
				if (num2 >= 64L)
				{
					this.mBitmap = 1L;
				}
				else
				{
					this.mBitmap <<= (int)num2;
					this.mBitmap |= 1L;
				}
				this.mLatestConfirmedSeq = seq;
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00113BC6 File Offset: 0x00111DC6
		internal void Reset()
		{
			this.mLatestConfirmedSeq = -1L;
			this.mBitmap = 0L;
		}

		// Token: 0x04001BAC RID: 7084
		private const long VALID_SEQ_MASK = 281474976710655L;

		// Token: 0x04001BAD RID: 7085
		private const long WINDOW_SIZE = 64L;

		// Token: 0x04001BAE RID: 7086
		private long mLatestConfirmedSeq = -1L;

		// Token: 0x04001BAF RID: 7087
		private long mBitmap;
	}
}
