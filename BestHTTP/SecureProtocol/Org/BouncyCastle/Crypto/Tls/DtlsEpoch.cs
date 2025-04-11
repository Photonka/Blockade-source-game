using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040A RID: 1034
	internal class DtlsEpoch
	{
		// Token: 0x060029CE RID: 10702 RVA: 0x001128F8 File Offset: 0x00110AF8
		internal DtlsEpoch(int epoch, TlsCipher cipher)
		{
			if (epoch < 0)
			{
				throw new ArgumentException("must be >= 0", "epoch");
			}
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.mEpoch = epoch;
			this.mCipher = cipher;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00112948 File Offset: 0x00110B48
		internal long AllocateSequenceNumber()
		{
			long num = this.mSequenceNumber;
			this.mSequenceNumber = num + 1L;
			return num;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x00112967 File Offset: 0x00110B67
		internal TlsCipher Cipher
		{
			get
			{
				return this.mCipher;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0011296F File Offset: 0x00110B6F
		internal int Epoch
		{
			get
			{
				return this.mEpoch;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x00112977 File Offset: 0x00110B77
		internal DtlsReplayWindow ReplayWindow
		{
			get
			{
				return this.mReplayWindow;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x0011297F File Offset: 0x00110B7F
		internal long SequenceNumber
		{
			get
			{
				return this.mSequenceNumber;
			}
		}

		// Token: 0x04001B85 RID: 7045
		private readonly DtlsReplayWindow mReplayWindow = new DtlsReplayWindow();

		// Token: 0x04001B86 RID: 7046
		private readonly int mEpoch;

		// Token: 0x04001B87 RID: 7047
		private readonly TlsCipher mCipher;

		// Token: 0x04001B88 RID: 7048
		private long mSequenceNumber;
	}
}
