using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040E RID: 1038
	internal class DtlsRecordLayer : DatagramTransport
	{
		// Token: 0x060029E1 RID: 10721 RVA: 0x00112C64 File Offset: 0x00110E64
		internal DtlsRecordLayer(DatagramTransport transport, TlsContext context, TlsPeer peer, byte contentType)
		{
			this.mTransport = transport;
			this.mContext = context;
			this.mPeer = peer;
			this.mInHandshake = true;
			this.mCurrentEpoch = new DtlsEpoch(0, new TlsNullCipher(context));
			this.mPendingEpoch = null;
			this.mReadEpoch = this.mCurrentEpoch;
			this.mWriteEpoch = this.mCurrentEpoch;
			this.SetPlaintextLimit(16384);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00112CDC File Offset: 0x00110EDC
		internal virtual void SetPlaintextLimit(int plaintextLimit)
		{
			this.mPlaintextLimit = plaintextLimit;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x00112CE7 File Offset: 0x00110EE7
		internal virtual int ReadEpoch
		{
			get
			{
				return this.mReadEpoch.Epoch;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x00112CF4 File Offset: 0x00110EF4
		// (set) Token: 0x060029E5 RID: 10725 RVA: 0x00112CFE File Offset: 0x00110EFE
		internal virtual ProtocolVersion ReadVersion
		{
			get
			{
				return this.mReadVersion;
			}
			set
			{
				this.mReadVersion = value;
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00112D09 File Offset: 0x00110F09
		internal virtual void SetWriteVersion(ProtocolVersion writeVersion)
		{
			this.mWriteVersion = writeVersion;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00112D14 File Offset: 0x00110F14
		internal virtual void InitPendingEpoch(TlsCipher pendingCipher)
		{
			if (this.mPendingEpoch != null)
			{
				throw new InvalidOperationException();
			}
			this.mPendingEpoch = new DtlsEpoch(this.mWriteEpoch.Epoch + 1, pendingCipher);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x00112D40 File Offset: 0x00110F40
		internal virtual void HandshakeSuccessful(DtlsHandshakeRetransmit retransmit)
		{
			if (this.mReadEpoch == this.mCurrentEpoch || this.mWriteEpoch == this.mCurrentEpoch)
			{
				throw new InvalidOperationException();
			}
			if (retransmit != null)
			{
				this.mRetransmit = retransmit;
				this.mRetransmitEpoch = this.mCurrentEpoch;
				this.mRetransmitExpiry = DateTimeUtilities.CurrentUnixMs() + 240000L;
			}
			this.mInHandshake = false;
			this.mCurrentEpoch = this.mPendingEpoch;
			this.mPendingEpoch = null;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00112DB3 File Offset: 0x00110FB3
		internal virtual void ResetWriteEpoch()
		{
			if (this.mRetransmitEpoch != null)
			{
				this.mWriteEpoch = this.mRetransmitEpoch;
				return;
			}
			this.mWriteEpoch = this.mCurrentEpoch;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00112DD6 File Offset: 0x00110FD6
		public virtual int GetReceiveLimit()
		{
			return Math.Min(this.mPlaintextLimit, this.mReadEpoch.Cipher.GetPlaintextLimit(this.mTransport.GetReceiveLimit() - 13));
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00112E03 File Offset: 0x00111003
		public virtual int GetSendLimit()
		{
			return Math.Min(this.mPlaintextLimit, this.mWriteEpoch.Cipher.GetPlaintextLimit(this.mTransport.GetSendLimit() - 13));
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00112E30 File Offset: 0x00111030
		public virtual int Receive(byte[] buf, int off, int len, int waitMillis)
		{
			byte[] array = null;
			int result;
			for (;;)
			{
				int num = Math.Min(len, this.GetReceiveLimit()) + 13;
				if (array == null || array.Length < num)
				{
					array = new byte[num];
				}
				try
				{
					if (this.mRetransmit != null && DateTimeUtilities.CurrentUnixMs() > this.mRetransmitExpiry)
					{
						this.mRetransmit = null;
						this.mRetransmitEpoch = null;
					}
					int num2 = this.ReceiveRecord(array, 0, num, waitMillis);
					if (num2 < 0)
					{
						result = num2;
					}
					else
					{
						if (num2 < 13)
						{
							continue;
						}
						int num3 = TlsUtilities.ReadUint16(array, 11);
						if (num2 != num3 + 13)
						{
							continue;
						}
						byte b = TlsUtilities.ReadUint8(array, 0);
						if (b - 20 > 4)
						{
							continue;
						}
						int num4 = TlsUtilities.ReadUint16(array, 3);
						DtlsEpoch dtlsEpoch = null;
						if (num4 == this.mReadEpoch.Epoch)
						{
							dtlsEpoch = this.mReadEpoch;
						}
						else if (b == 22 && this.mRetransmitEpoch != null && num4 == this.mRetransmitEpoch.Epoch)
						{
							dtlsEpoch = this.mRetransmitEpoch;
						}
						if (dtlsEpoch == null)
						{
							continue;
						}
						long num5 = TlsUtilities.ReadUint48(array, 5);
						if (dtlsEpoch.ReplayWindow.ShouldDiscard(num5))
						{
							continue;
						}
						ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(array, 1);
						if (!protocolVersion.IsDtls)
						{
							continue;
						}
						if (this.mReadVersion != null && !this.mReadVersion.Equals(protocolVersion))
						{
							continue;
						}
						byte[] array2 = dtlsEpoch.Cipher.DecodeCiphertext(DtlsRecordLayer.GetMacSequenceNumber(dtlsEpoch.Epoch, num5), b, array, 13, num2 - 13);
						dtlsEpoch.ReplayWindow.ReportAuthenticated(num5);
						if (array2.Length > this.mPlaintextLimit)
						{
							continue;
						}
						if (this.mReadVersion == null)
						{
							this.mReadVersion = protocolVersion;
						}
						switch (b)
						{
						case 20:
							for (int i = 0; i < array2.Length; i++)
							{
								if (TlsUtilities.ReadUint8(array2, i) == 1 && this.mPendingEpoch != null)
								{
									this.mReadEpoch = this.mPendingEpoch;
								}
							}
							break;
						case 21:
							if (array2.Length == 2)
							{
								byte b2 = array2[0];
								byte b3 = array2[1];
								this.mPeer.NotifyAlertReceived(b2, b3);
								if (b2 == 2)
								{
									this.Failed();
									throw new TlsFatalAlert(b3);
								}
								if (b3 == 0)
								{
									this.CloseTransport();
								}
							}
							break;
						case 22:
							if (this.mInHandshake)
							{
								goto IL_268;
							}
							if (this.mRetransmit != null)
							{
								this.mRetransmit.ReceivedHandshakeRecord(num4, array2, 0, array2.Length);
							}
							break;
						case 23:
							if (!this.mInHandshake)
							{
								goto IL_268;
							}
							break;
						case 24:
							break;
						default:
							goto IL_268;
						}
						continue;
						IL_268:
						if (!this.mInHandshake && this.mRetransmit != null)
						{
							this.mRetransmit = null;
							this.mRetransmitEpoch = null;
						}
						Array.Copy(array2, 0, buf, off, array2.Length);
						result = array2.Length;
					}
				}
				catch (IOException ex)
				{
					throw ex;
				}
				break;
			}
			return result;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x001130FC File Offset: 0x001112FC
		public virtual void Send(byte[] buf, int off, int len)
		{
			byte contentType = 23;
			if (this.mInHandshake || this.mWriteEpoch == this.mRetransmitEpoch)
			{
				contentType = 22;
				if (TlsUtilities.ReadUint8(buf, off) == 20)
				{
					DtlsEpoch dtlsEpoch = null;
					if (this.mInHandshake)
					{
						dtlsEpoch = this.mPendingEpoch;
					}
					else if (this.mWriteEpoch == this.mRetransmitEpoch)
					{
						dtlsEpoch = this.mCurrentEpoch;
					}
					if (dtlsEpoch == null)
					{
						throw new InvalidOperationException();
					}
					byte[] array = new byte[]
					{
						1
					};
					this.SendRecord(20, array, 0, array.Length);
					this.mWriteEpoch = dtlsEpoch;
				}
			}
			this.SendRecord(contentType, buf, off, len);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0011318E File Offset: 0x0011138E
		public virtual void Close()
		{
			if (!this.mClosed)
			{
				if (this.mInHandshake)
				{
					this.Warn(90, "User canceled handshake");
				}
				this.CloseTransport();
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x001131B7 File Offset: 0x001113B7
		internal virtual void Failed()
		{
			if (!this.mClosed)
			{
				this.mFailed = true;
				this.CloseTransport();
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x001131D4 File Offset: 0x001113D4
		internal virtual void Fail(byte alertDescription)
		{
			if (!this.mClosed)
			{
				try
				{
					this.RaiseAlert(2, alertDescription, null, null);
				}
				catch (Exception)
				{
				}
				this.mFailed = true;
				this.CloseTransport();
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0011321C File Offset: 0x0011141C
		internal virtual void Warn(byte alertDescription, string message)
		{
			this.RaiseAlert(1, alertDescription, message, null);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00113228 File Offset: 0x00111428
		private void CloseTransport()
		{
			if (!this.mClosed)
			{
				try
				{
					if (!this.mFailed)
					{
						this.Warn(0, null);
					}
					this.mTransport.Close();
				}
				catch (Exception)
				{
				}
				this.mClosed = true;
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0011327C File Offset: 0x0011147C
		private void RaiseAlert(byte alertLevel, byte alertDescription, string message, Exception cause)
		{
			this.mPeer.NotifyAlertRaised(alertLevel, alertDescription, message, cause);
			this.SendRecord(21, new byte[]
			{
				alertLevel,
				alertDescription
			}, 0, 2);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x001132B4 File Offset: 0x001114B4
		private int ReceiveRecord(byte[] buf, int off, int len, int waitMillis)
		{
			if (this.mRecordQueue.Available > 0)
			{
				int num = 0;
				if (this.mRecordQueue.Available >= 13)
				{
					byte[] buf2 = new byte[2];
					this.mRecordQueue.Read(buf2, 0, 2, 11);
					num = TlsUtilities.ReadUint16(buf2, 0);
				}
				int num2 = Math.Min(this.mRecordQueue.Available, 13 + num);
				this.mRecordQueue.RemoveData(buf, off, num2, 0);
				return num2;
			}
			int num3 = this.mTransport.Receive(buf, off, len, waitMillis);
			if (num3 >= 13)
			{
				int num4 = TlsUtilities.ReadUint16(buf, off + 11);
				int num5 = 13 + num4;
				if (num3 > num5)
				{
					this.mRecordQueue.AddData(buf, off + num5, num3 - num5);
					num3 = num5;
				}
			}
			return num3;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0011336C File Offset: 0x0011156C
		private void SendRecord(byte contentType, byte[] buf, int off, int len)
		{
			if (this.mWriteVersion == null)
			{
				return;
			}
			if (len > this.mPlaintextLimit)
			{
				throw new TlsFatalAlert(80);
			}
			if (len < 1 && contentType != 23)
			{
				throw new TlsFatalAlert(80);
			}
			int epoch = this.mWriteEpoch.Epoch;
			long num = this.mWriteEpoch.AllocateSequenceNumber();
			byte[] array = this.mWriteEpoch.Cipher.EncodePlaintext(DtlsRecordLayer.GetMacSequenceNumber(epoch, num), contentType, buf, off, len);
			byte[] array2 = new byte[array.Length + 13];
			TlsUtilities.WriteUint8(contentType, array2, 0);
			TlsUtilities.WriteVersion(this.mWriteVersion, array2, 1);
			TlsUtilities.WriteUint16(epoch, array2, 3);
			TlsUtilities.WriteUint48(num, array2, 5);
			TlsUtilities.WriteUint16(array.Length, array2, 11);
			Array.Copy(array, 0, array2, 13, array.Length);
			this.mTransport.Send(array2, 0, array2.Length);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0011343A File Offset: 0x0011163A
		private static long GetMacSequenceNumber(int epoch, long sequence_number)
		{
			return ((long)epoch & (long)((ulong)-1)) << 48 | sequence_number;
		}

		// Token: 0x04001B8D RID: 7053
		private const int RECORD_HEADER_LENGTH = 13;

		// Token: 0x04001B8E RID: 7054
		private const int MAX_FRAGMENT_LENGTH = 16384;

		// Token: 0x04001B8F RID: 7055
		private const long TCP_MSL = 120000L;

		// Token: 0x04001B90 RID: 7056
		private const long RETRANSMIT_TIMEOUT = 240000L;

		// Token: 0x04001B91 RID: 7057
		private readonly DatagramTransport mTransport;

		// Token: 0x04001B92 RID: 7058
		private readonly TlsContext mContext;

		// Token: 0x04001B93 RID: 7059
		private readonly TlsPeer mPeer;

		// Token: 0x04001B94 RID: 7060
		private readonly ByteQueue mRecordQueue = new ByteQueue();

		// Token: 0x04001B95 RID: 7061
		private volatile bool mClosed;

		// Token: 0x04001B96 RID: 7062
		private volatile bool mFailed;

		// Token: 0x04001B97 RID: 7063
		private volatile ProtocolVersion mReadVersion;

		// Token: 0x04001B98 RID: 7064
		private volatile ProtocolVersion mWriteVersion;

		// Token: 0x04001B99 RID: 7065
		private volatile bool mInHandshake;

		// Token: 0x04001B9A RID: 7066
		private volatile int mPlaintextLimit;

		// Token: 0x04001B9B RID: 7067
		private DtlsEpoch mCurrentEpoch;

		// Token: 0x04001B9C RID: 7068
		private DtlsEpoch mPendingEpoch;

		// Token: 0x04001B9D RID: 7069
		private DtlsEpoch mReadEpoch;

		// Token: 0x04001B9E RID: 7070
		private DtlsEpoch mWriteEpoch;

		// Token: 0x04001B9F RID: 7071
		private DtlsHandshakeRetransmit mRetransmit;

		// Token: 0x04001BA0 RID: 7072
		private DtlsEpoch mRetransmitEpoch;

		// Token: 0x04001BA1 RID: 7073
		private long mRetransmitExpiry;
	}
}
