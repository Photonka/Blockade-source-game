using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200025F RID: 607
	public sealed class ZStream
	{
		// Token: 0x060016C1 RID: 5825 RVA: 0x000B897E File Offset: 0x000B6B7E
		public int inflateInit()
		{
			return this.inflateInit(15);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000B8988 File Offset: 0x000B6B88
		public int inflateInit(bool nowrap)
		{
			return this.inflateInit(15, nowrap);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000B8993 File Offset: 0x000B6B93
		public int inflateInit(int w)
		{
			return this.inflateInit(w, false);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000B899D File Offset: 0x000B6B9D
		public int inflateInit(int w, bool nowrap)
		{
			this.istate = new Inflate();
			return this.istate.inflateInit(this, nowrap ? (-w) : w);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000B89BE File Offset: 0x000B6BBE
		public int inflate(int f)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflate(this, f);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000B89D8 File Offset: 0x000B6BD8
		public int inflateEnd()
		{
			if (this.istate == null)
			{
				return -2;
			}
			int result = this.istate.inflateEnd(this);
			this.istate = null;
			return result;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000B89F8 File Offset: 0x000B6BF8
		public int inflateSync()
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSync(this);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000B8A11 File Offset: 0x000B6C11
		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000B8A2C File Offset: 0x000B6C2C
		public int deflateInit(int level)
		{
			return this.deflateInit(level, 15);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000B8A37 File Offset: 0x000B6C37
		public int deflateInit(int level, bool nowrap)
		{
			return this.deflateInit(level, 15, nowrap);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000B8A43 File Offset: 0x000B6C43
		public int deflateInit(int level, int bits)
		{
			return this.deflateInit(level, bits, false);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000B8A4E File Offset: 0x000B6C4E
		public int deflateInit(int level, int bits, bool nowrap)
		{
			this.dstate = new Deflate();
			return this.dstate.deflateInit(this, level, nowrap ? (-bits) : bits);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000B8A70 File Offset: 0x000B6C70
		public int deflate(int flush)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflate(this, flush);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000B8A8A File Offset: 0x000B6C8A
		public int deflateEnd()
		{
			if (this.dstate == null)
			{
				return -2;
			}
			int result = this.dstate.deflateEnd();
			this.dstate = null;
			return result;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000B8AA9 File Offset: 0x000B6CA9
		public int deflateParams(int level, int strategy)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateParams(this, level, strategy);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000B8AC4 File Offset: 0x000B6CC4
		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000B8AE0 File Offset: 0x000B6CE0
		internal void flush_pending()
		{
			int pending = this.dstate.pending;
			if (pending > this.avail_out)
			{
				pending = this.avail_out;
			}
			if (pending == 0)
			{
				return;
			}
			if (this.dstate.pending_buf.Length > this.dstate.pending_out && this.next_out.Length > this.next_out_index && this.dstate.pending_buf.Length >= this.dstate.pending_out + pending)
			{
				int num = this.next_out.Length;
				int num2 = this.next_out_index + pending;
			}
			Array.Copy(this.dstate.pending_buf, this.dstate.pending_out, this.next_out, this.next_out_index, pending);
			this.next_out_index += pending;
			this.dstate.pending_out += pending;
			this.total_out += (long)pending;
			this.avail_out -= pending;
			this.dstate.pending -= pending;
			if (this.dstate.pending == 0)
			{
				this.dstate.pending_out = 0;
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000B8BF8 File Offset: 0x000B6DF8
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.avail_in;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.avail_in -= num;
			if (this.dstate.noheader == 0)
			{
				this.adler = this._adler.adler32(this.adler, this.next_in, this.next_in_index, num);
			}
			Array.Copy(this.next_in, this.next_in_index, buf, start, num);
			this.next_in_index += num;
			this.total_in += (long)num;
			return num;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000B8C88 File Offset: 0x000B6E88
		public void free()
		{
			this.next_in = null;
			this.next_out = null;
			this.msg = null;
			this._adler = null;
		}

		// Token: 0x04001691 RID: 5777
		private const int MAX_WBITS = 15;

		// Token: 0x04001692 RID: 5778
		private const int DEF_WBITS = 15;

		// Token: 0x04001693 RID: 5779
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04001694 RID: 5780
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001695 RID: 5781
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001696 RID: 5782
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x04001697 RID: 5783
		private const int Z_FINISH = 4;

		// Token: 0x04001698 RID: 5784
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04001699 RID: 5785
		private const int Z_OK = 0;

		// Token: 0x0400169A RID: 5786
		private const int Z_STREAM_END = 1;

		// Token: 0x0400169B RID: 5787
		private const int Z_NEED_DICT = 2;

		// Token: 0x0400169C RID: 5788
		private const int Z_ERRNO = -1;

		// Token: 0x0400169D RID: 5789
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x0400169E RID: 5790
		private const int Z_DATA_ERROR = -3;

		// Token: 0x0400169F RID: 5791
		private const int Z_MEM_ERROR = -4;

		// Token: 0x040016A0 RID: 5792
		private const int Z_BUF_ERROR = -5;

		// Token: 0x040016A1 RID: 5793
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x040016A2 RID: 5794
		public byte[] next_in;

		// Token: 0x040016A3 RID: 5795
		public int next_in_index;

		// Token: 0x040016A4 RID: 5796
		public int avail_in;

		// Token: 0x040016A5 RID: 5797
		public long total_in;

		// Token: 0x040016A6 RID: 5798
		public byte[] next_out;

		// Token: 0x040016A7 RID: 5799
		public int next_out_index;

		// Token: 0x040016A8 RID: 5800
		public int avail_out;

		// Token: 0x040016A9 RID: 5801
		public long total_out;

		// Token: 0x040016AA RID: 5802
		public string msg;

		// Token: 0x040016AB RID: 5803
		internal Deflate dstate;

		// Token: 0x040016AC RID: 5804
		internal Inflate istate;

		// Token: 0x040016AD RID: 5805
		internal int data_type;

		// Token: 0x040016AE RID: 5806
		public long adler;

		// Token: 0x040016AF RID: 5807
		internal Adler32 _adler = new Adler32();
	}
}
