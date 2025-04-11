using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200025D RID: 605
	public class ZInputStream : Stream
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x000B8289 File Offset: 0x000B6489
		private static ZStream GetDefaultZStream(bool nowrap)
		{
			ZStream zstream = new ZStream();
			zstream.inflateInit(nowrap);
			return zstream;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000B8298 File Offset: 0x000B6498
		public ZInputStream(Stream input) : this(input, false)
		{
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000B82A2 File Offset: 0x000B64A2
		public ZInputStream(Stream input, bool nowrap) : this(input, ZInputStream.GetDefaultZStream(nowrap))
		{
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000B82B4 File Offset: 0x000B64B4
		public ZInputStream(Stream input, ZStream z)
		{
			this.buf = new byte[512];
			this.buf1 = new byte[1];
			base..ctor();
			if (z == null)
			{
				z = new ZStream();
			}
			if (z.istate == null && z.dstate == null)
			{
				z.inflateInit();
			}
			this.input = input;
			this.compress = (z.istate == null);
			this.z = z;
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000B834A File Offset: 0x000B654A
		public ZInputStream(Stream input, int level) : this(input, level, false)
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000B8358 File Offset: 0x000B6558
		public ZInputStream(Stream input, int level, bool nowrap)
		{
			this.buf = new byte[512];
			this.buf1 = new byte[1];
			base..ctor();
			this.input = input;
			this.compress = true;
			this.z = new ZStream();
			this.z.deflateInit(level, nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000B83D7 File Offset: 0x000B65D7
		public sealed override bool CanRead
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000B83E2 File Offset: 0x000B65E2
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.closed = true;
			Platform.Dispose(this.input);
			base.Close();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00002B75 File Offset: 0x00000D75
		public sealed override void Flush()
		{
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000B8405 File Offset: 0x000B6605
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x000B840D File Offset: 0x000B660D
		public virtual int FlushMode
		{
			get
			{
				return this.flushLevel;
			}
			set
			{
				this.flushLevel = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000B8418 File Offset: 0x000B6618
		public override int Read(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return 0;
			}
			this.z.next_out = b;
			this.z.next_out_index = off;
			this.z.avail_out = len;
			for (;;)
			{
				if (this.z.avail_in == 0 && !this.nomoreinput)
				{
					this.z.next_in_index = 0;
					this.z.avail_in = this.input.Read(this.buf, 0, this.buf.Length);
					if (this.z.avail_in <= 0)
					{
						this.z.avail_in = 0;
						this.nomoreinput = true;
					}
				}
				int num = this.compress ? this.z.deflate(this.flushLevel) : this.z.inflate(this.flushLevel);
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_9;
				}
				if ((this.nomoreinput || num == 1) && this.z.avail_out == len)
				{
					return 0;
				}
				if (this.z.avail_out != len || num != 0)
				{
					goto IL_132;
				}
			}
			return 0;
			Block_9:
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
			IL_132:
			return len - this.z.avail_out;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000B8564 File Offset: 0x000B6764
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)this.buf1[0];
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000B8581 File Offset: 0x000B6781
		public virtual long TotalIn
		{
			get
			{
				return this.z.total_in;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000B858E File Offset: 0x000B678E
		public virtual long TotalOut
		{
			get
			{
				return this.z.total_out;
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001680 RID: 5760
		private const int BufferSize = 512;

		// Token: 0x04001681 RID: 5761
		protected ZStream z;

		// Token: 0x04001682 RID: 5762
		protected int flushLevel;

		// Token: 0x04001683 RID: 5763
		protected byte[] buf;

		// Token: 0x04001684 RID: 5764
		protected byte[] buf1;

		// Token: 0x04001685 RID: 5765
		protected bool compress;

		// Token: 0x04001686 RID: 5766
		protected Stream input;

		// Token: 0x04001687 RID: 5767
		protected bool closed;

		// Token: 0x04001688 RID: 5768
		private bool nomoreinput;
	}
}
