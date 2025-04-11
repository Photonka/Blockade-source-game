using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040F RID: 1039
	internal class DtlsReliableHandshake
	{
		// Token: 0x060029F7 RID: 10743 RVA: 0x00113448 File Offset: 0x00111648
		internal DtlsReliableHandshake(TlsContext context, DtlsRecordLayer transport)
		{
			this.mRecordLayer = transport;
			this.mHandshakeHash = new DeferredHash();
			this.mHandshakeHash.Init(context);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00113496 File Offset: 0x00111696
		internal void NotifyHelloComplete()
		{
			this.mHandshakeHash = this.mHandshakeHash.NotifyPrfDetermined();
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x001134A9 File Offset: 0x001116A9
		internal TlsHandshakeHash HandshakeHash
		{
			get
			{
				return this.mHandshakeHash;
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x001134B1 File Offset: 0x001116B1
		internal TlsHandshakeHash PrepareToFinish()
		{
			TlsHandshakeHash result = this.mHandshakeHash;
			this.mHandshakeHash = this.mHandshakeHash.StopTracking();
			return result;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x001134CC File Offset: 0x001116CC
		internal void SendMessage(byte msg_type, byte[] body)
		{
			TlsUtilities.CheckUint24(body.Length);
			if (!this.mSending)
			{
				this.CheckInboundFlight();
				this.mSending = true;
				this.mOutboundFlight.Clear();
			}
			int num = this.mMessageSeq;
			this.mMessageSeq = num + 1;
			DtlsReliableHandshake.Message message = new DtlsReliableHandshake.Message(num, msg_type, body);
			this.mOutboundFlight.Add(message);
			this.WriteMessage(message);
			this.UpdateHandshakeMessagesDigest(message);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x00113536 File Offset: 0x00111736
		internal byte[] ReceiveMessageBody(byte msg_type)
		{
			DtlsReliableHandshake.Message message = this.ReceiveMessage();
			if (message.Type != msg_type)
			{
				throw new TlsFatalAlert(10);
			}
			return message.Body;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00113554 File Offset: 0x00111754
		internal DtlsReliableHandshake.Message ReceiveMessage()
		{
			if (this.mSending)
			{
				this.mSending = false;
				this.PrepareInboundFlight(Platform.CreateHashtable());
			}
			byte[] array = null;
			int num = 1000;
			DtlsReliableHandshake.Message result;
			for (;;)
			{
				try
				{
					DtlsReliableHandshake.Message pendingMessage;
					for (;;)
					{
						pendingMessage = this.GetPendingMessage();
						if (pendingMessage != null)
						{
							break;
						}
						int receiveLimit = this.mRecordLayer.GetReceiveLimit();
						if (array == null || array.Length < receiveLimit)
						{
							array = new byte[receiveLimit];
						}
						int num2 = this.mRecordLayer.Receive(array, 0, receiveLimit, num);
						if (num2 < 0)
						{
							goto IL_87;
						}
						if (this.ProcessRecord(16, this.mRecordLayer.ReadEpoch, array, 0, num2))
						{
							num = this.BackOff(num);
						}
					}
					result = pendingMessage;
					break;
					IL_87:;
				}
				catch (IOException)
				{
				}
				this.ResendOutboundFlight();
				num = this.BackOff(num);
			}
			return result;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00113610 File Offset: 0x00111810
		internal void Finish()
		{
			DtlsHandshakeRetransmit retransmit = null;
			if (!this.mSending)
			{
				this.CheckInboundFlight();
			}
			else
			{
				this.PrepareInboundFlight(null);
				if (this.mPreviousInboundFlight != null)
				{
					retransmit = new DtlsReliableHandshake.Retransmit(this);
				}
			}
			this.mRecordLayer.HandshakeSuccessful(retransmit);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00113651 File Offset: 0x00111851
		internal void ResetHandshakeMessagesDigest()
		{
			this.mHandshakeHash.Reset();
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0011365E File Offset: 0x0011185E
		private int BackOff(int timeoutMillis)
		{
			return Math.Min(timeoutMillis * 2, 60000);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00113670 File Offset: 0x00111870
		private void CheckInboundFlight()
		{
			foreach (object obj in this.mCurrentInboundFlight.Keys)
			{
				int num = (int)obj;
				int num2 = this.mNextReceiveSeq;
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x001136D0 File Offset: 0x001118D0
		private DtlsReliableHandshake.Message GetPendingMessage()
		{
			DtlsReassembler dtlsReassembler = (DtlsReassembler)this.mCurrentInboundFlight[this.mNextReceiveSeq];
			if (dtlsReassembler != null)
			{
				byte[] bodyIfComplete = dtlsReassembler.GetBodyIfComplete();
				if (bodyIfComplete != null)
				{
					this.mPreviousInboundFlight = null;
					int num = this.mNextReceiveSeq;
					this.mNextReceiveSeq = num + 1;
					return this.UpdateHandshakeMessagesDigest(new DtlsReliableHandshake.Message(num, dtlsReassembler.MsgType, bodyIfComplete));
				}
			}
			return null;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00113732 File Offset: 0x00111932
		private void PrepareInboundFlight(IDictionary nextFlight)
		{
			DtlsReliableHandshake.ResetAll(this.mCurrentInboundFlight);
			this.mPreviousInboundFlight = this.mCurrentInboundFlight;
			this.mCurrentInboundFlight = nextFlight;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00113754 File Offset: 0x00111954
		private bool ProcessRecord(int windowSize, int epoch, byte[] buf, int off, int len)
		{
			bool flag = false;
			while (len >= 12)
			{
				int num = TlsUtilities.ReadUint24(buf, off + 9);
				int num2 = num + 12;
				if (len < num2)
				{
					break;
				}
				int num3 = TlsUtilities.ReadUint24(buf, off + 1);
				int num4 = TlsUtilities.ReadUint24(buf, off + 6);
				if (num4 + num > num3)
				{
					break;
				}
				byte b = TlsUtilities.ReadUint8(buf, off);
				int num5 = (b == 20) ? 1 : 0;
				if (epoch != num5)
				{
					break;
				}
				int num6 = TlsUtilities.ReadUint16(buf, off + 4);
				if (num6 < this.mNextReceiveSeq + windowSize)
				{
					if (num6 >= this.mNextReceiveSeq)
					{
						DtlsReassembler dtlsReassembler = (DtlsReassembler)this.mCurrentInboundFlight[num6];
						if (dtlsReassembler == null)
						{
							dtlsReassembler = new DtlsReassembler(b, num3);
							this.mCurrentInboundFlight[num6] = dtlsReassembler;
						}
						dtlsReassembler.ContributeFragment(b, num3, buf, off + 12, num4, num);
					}
					else if (this.mPreviousInboundFlight != null)
					{
						DtlsReassembler dtlsReassembler2 = (DtlsReassembler)this.mPreviousInboundFlight[num6];
						if (dtlsReassembler2 != null)
						{
							dtlsReassembler2.ContributeFragment(b, num3, buf, off + 12, num4, num);
							flag = true;
						}
					}
				}
				off += num2;
				len -= num2;
			}
			bool flag2 = flag && DtlsReliableHandshake.CheckAll(this.mPreviousInboundFlight);
			if (flag2)
			{
				this.ResendOutboundFlight();
				DtlsReliableHandshake.ResetAll(this.mPreviousInboundFlight);
			}
			return flag2;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x001138AC File Offset: 0x00111AAC
		private void ResendOutboundFlight()
		{
			this.mRecordLayer.ResetWriteEpoch();
			for (int i = 0; i < this.mOutboundFlight.Count; i++)
			{
				this.WriteMessage((DtlsReliableHandshake.Message)this.mOutboundFlight[i]);
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x001138F4 File Offset: 0x00111AF4
		private DtlsReliableHandshake.Message UpdateHandshakeMessagesDigest(DtlsReliableHandshake.Message message)
		{
			if (message.Type != 0)
			{
				byte[] body = message.Body;
				byte[] array = new byte[12];
				TlsUtilities.WriteUint8(message.Type, array, 0);
				TlsUtilities.WriteUint24(body.Length, array, 1);
				TlsUtilities.WriteUint16(message.Seq, array, 4);
				TlsUtilities.WriteUint24(0, array, 6);
				TlsUtilities.WriteUint24(body.Length, array, 9);
				this.mHandshakeHash.BlockUpdate(array, 0, array.Length);
				this.mHandshakeHash.BlockUpdate(body, 0, body.Length);
			}
			return message;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00113970 File Offset: 0x00111B70
		private void WriteMessage(DtlsReliableHandshake.Message message)
		{
			int num = this.mRecordLayer.GetSendLimit() - 12;
			if (num < 1)
			{
				throw new TlsFatalAlert(80);
			}
			int num2 = message.Body.Length;
			int num3 = 0;
			do
			{
				int num4 = Math.Min(num2 - num3, num);
				this.WriteHandshakeFragment(message, num3, num4);
				num3 += num4;
			}
			while (num3 < num2);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x001139C0 File Offset: 0x00111BC0
		private void WriteHandshakeFragment(DtlsReliableHandshake.Message message, int fragment_offset, int fragment_length)
		{
			DtlsReliableHandshake.RecordLayerBuffer recordLayerBuffer = new DtlsReliableHandshake.RecordLayerBuffer(12 + fragment_length);
			TlsUtilities.WriteUint8(message.Type, recordLayerBuffer);
			TlsUtilities.WriteUint24(message.Body.Length, recordLayerBuffer);
			TlsUtilities.WriteUint16(message.Seq, recordLayerBuffer);
			TlsUtilities.WriteUint24(fragment_offset, recordLayerBuffer);
			TlsUtilities.WriteUint24(fragment_length, recordLayerBuffer);
			recordLayerBuffer.Write(message.Body, fragment_offset, fragment_length);
			recordLayerBuffer.SendToRecordLayer(this.mRecordLayer);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00113A28 File Offset: 0x00111C28
		private static bool CheckAll(IDictionary inboundFlight)
		{
			using (IEnumerator enumerator = inboundFlight.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DtlsReassembler)enumerator.Current).GetBodyIfComplete() == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00113A88 File Offset: 0x00111C88
		private static void ResetAll(IDictionary inboundFlight)
		{
			foreach (object obj in inboundFlight.Values)
			{
				((DtlsReassembler)obj).Reset();
			}
		}

		// Token: 0x04001BA2 RID: 7074
		private const int MaxReceiveAhead = 16;

		// Token: 0x04001BA3 RID: 7075
		private const int MessageHeaderLength = 12;

		// Token: 0x04001BA4 RID: 7076
		private readonly DtlsRecordLayer mRecordLayer;

		// Token: 0x04001BA5 RID: 7077
		private TlsHandshakeHash mHandshakeHash;

		// Token: 0x04001BA6 RID: 7078
		private IDictionary mCurrentInboundFlight = Platform.CreateHashtable();

		// Token: 0x04001BA7 RID: 7079
		private IDictionary mPreviousInboundFlight;

		// Token: 0x04001BA8 RID: 7080
		private IList mOutboundFlight = Platform.CreateArrayList();

		// Token: 0x04001BA9 RID: 7081
		private bool mSending = true;

		// Token: 0x04001BAA RID: 7082
		private int mMessageSeq;

		// Token: 0x04001BAB RID: 7083
		private int mNextReceiveSeq;

		// Token: 0x0200091A RID: 2330
		internal class Message
		{
			// Token: 0x06004E0F RID: 19983 RVA: 0x001B3012 File Offset: 0x001B1212
			internal Message(int message_seq, byte msg_type, byte[] body)
			{
				this.mMessageSeq = message_seq;
				this.mMsgType = msg_type;
				this.mBody = body;
			}

			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x06004E10 RID: 19984 RVA: 0x001B302F File Offset: 0x001B122F
			public int Seq
			{
				get
				{
					return this.mMessageSeq;
				}
			}

			// Token: 0x17000C3D RID: 3133
			// (get) Token: 0x06004E11 RID: 19985 RVA: 0x001B3037 File Offset: 0x001B1237
			public byte Type
			{
				get
				{
					return this.mMsgType;
				}
			}

			// Token: 0x17000C3E RID: 3134
			// (get) Token: 0x06004E12 RID: 19986 RVA: 0x001B303F File Offset: 0x001B123F
			public byte[] Body
			{
				get
				{
					return this.mBody;
				}
			}

			// Token: 0x040034CF RID: 13519
			private readonly int mMessageSeq;

			// Token: 0x040034D0 RID: 13520
			private readonly byte mMsgType;

			// Token: 0x040034D1 RID: 13521
			private readonly byte[] mBody;
		}

		// Token: 0x0200091B RID: 2331
		internal class RecordLayerBuffer : MemoryStream
		{
			// Token: 0x06004E13 RID: 19987 RVA: 0x001B3047 File Offset: 0x001B1247
			internal RecordLayerBuffer(int size) : base(size)
			{
			}

			// Token: 0x06004E14 RID: 19988 RVA: 0x001B3050 File Offset: 0x001B1250
			internal void SendToRecordLayer(DtlsRecordLayer recordLayer)
			{
				byte[] buffer = this.GetBuffer();
				int len = (int)this.Length;
				recordLayer.Send(buffer, 0, len);
				Platform.Dispose(this);
			}
		}

		// Token: 0x0200091C RID: 2332
		internal class Retransmit : DtlsHandshakeRetransmit
		{
			// Token: 0x06004E15 RID: 19989 RVA: 0x001B307B File Offset: 0x001B127B
			internal Retransmit(DtlsReliableHandshake outer)
			{
				this.mOuter = outer;
			}

			// Token: 0x06004E16 RID: 19990 RVA: 0x001B308A File Offset: 0x001B128A
			public void ReceivedHandshakeRecord(int epoch, byte[] buf, int off, int len)
			{
				this.mOuter.ProcessRecord(0, epoch, buf, off, len);
			}

			// Token: 0x040034D2 RID: 13522
			private readonly DtlsReliableHandshake mOuter;
		}
	}
}
