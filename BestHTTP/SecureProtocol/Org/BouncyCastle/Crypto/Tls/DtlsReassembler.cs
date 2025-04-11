using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040D RID: 1037
	internal class DtlsReassembler
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x00112A9D File Offset: 0x00110C9D
		internal DtlsReassembler(byte msg_type, int length)
		{
			this.mMsgType = msg_type;
			this.mBody = new byte[length];
			this.mMissing.Add(new DtlsReassembler.Range(0, length));
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x00112AD6 File Offset: 0x00110CD6
		internal byte MsgType
		{
			get
			{
				return this.mMsgType;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00112ADE File Offset: 0x00110CDE
		internal byte[] GetBodyIfComplete()
		{
			if (this.mMissing.Count != 0)
			{
				return null;
			}
			return this.mBody;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00112AF8 File Offset: 0x00110CF8
		internal void ContributeFragment(byte msg_type, int length, byte[] buf, int off, int fragment_offset, int fragment_length)
		{
			int num = fragment_offset + fragment_length;
			if (this.mMsgType != msg_type || this.mBody.Length != length || num > length)
			{
				return;
			}
			if (fragment_length == 0)
			{
				if (fragment_offset == 0 && this.mMissing.Count > 0 && ((DtlsReassembler.Range)this.mMissing[0]).End == 0)
				{
					this.mMissing.RemoveAt(0);
				}
				return;
			}
			for (int i = 0; i < this.mMissing.Count; i++)
			{
				DtlsReassembler.Range range = (DtlsReassembler.Range)this.mMissing[i];
				if (range.Start >= num)
				{
					break;
				}
				if (range.End > fragment_offset)
				{
					int num2 = Math.Max(range.Start, fragment_offset);
					int num3 = Math.Min(range.End, num);
					int length2 = num3 - num2;
					Array.Copy(buf, off + num2 - fragment_offset, this.mBody, num2, length2);
					if (num2 == range.Start)
					{
						if (num3 == range.End)
						{
							this.mMissing.RemoveAt(i--);
						}
						else
						{
							range.Start = num3;
						}
					}
					else
					{
						if (num3 != range.End)
						{
							this.mMissing.Insert(++i, new DtlsReassembler.Range(num3, range.End));
						}
						range.End = num2;
					}
				}
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00112C3B File Offset: 0x00110E3B
		internal void Reset()
		{
			this.mMissing.Clear();
			this.mMissing.Add(new DtlsReassembler.Range(0, this.mBody.Length));
		}

		// Token: 0x04001B8A RID: 7050
		private readonly byte mMsgType;

		// Token: 0x04001B8B RID: 7051
		private readonly byte[] mBody;

		// Token: 0x04001B8C RID: 7052
		private readonly IList mMissing = Platform.CreateArrayList();

		// Token: 0x02000919 RID: 2329
		private class Range
		{
			// Token: 0x06004E0A RID: 19978 RVA: 0x001B2FDA File Offset: 0x001B11DA
			internal Range(int start, int end)
			{
				this.mStart = start;
				this.mEnd = end;
			}

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x06004E0B RID: 19979 RVA: 0x001B2FF0 File Offset: 0x001B11F0
			// (set) Token: 0x06004E0C RID: 19980 RVA: 0x001B2FF8 File Offset: 0x001B11F8
			public int Start
			{
				get
				{
					return this.mStart;
				}
				set
				{
					this.mStart = value;
				}
			}

			// Token: 0x17000C3B RID: 3131
			// (get) Token: 0x06004E0D RID: 19981 RVA: 0x001B3001 File Offset: 0x001B1201
			// (set) Token: 0x06004E0E RID: 19982 RVA: 0x001B3009 File Offset: 0x001B1209
			public int End
			{
				get
				{
					return this.mEnd;
				}
				set
				{
					this.mEnd = value;
				}
			}

			// Token: 0x040034CD RID: 13517
			private int mStart;

			// Token: 0x040034CE RID: 13518
			private int mEnd;
		}
	}
}
