using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200025C RID: 604
	[Obsolete("Use 'ZInputStream' instead")]
	public class ZInflaterInputStream : Stream
	{
		// Token: 0x06001680 RID: 5760 RVA: 0x000B80A8 File Offset: 0x000B62A8
		public ZInflaterInputStream(Stream inp) : this(inp, false)
		{
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000B80B4 File Offset: 0x000B62B4
		public ZInflaterInputStream(Stream inp, bool nowrap)
		{
			this.inp = inp;
			this.z.inflateInit(nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0008F863 File Offset: 0x0008DA63
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0008F863 File Offset: 0x0008DA63
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x00002B75 File Offset: 0x00000D75
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Write(byte[] b, int off, int len)
		{
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0008F863 File Offset: 0x0008DA63
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void SetLength(long value)
		{
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000B812C File Offset: 0x000B632C
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
					this.z.avail_in = this.inp.Read(this.buf, 0, 4192);
					if (this.z.avail_in <= 0)
					{
						this.z.avail_in = 0;
						this.nomoreinput = true;
					}
				}
				int num = this.z.inflate(this.flushLevel);
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_8;
				}
				if ((this.nomoreinput || num == 1) && this.z.avail_out == len)
				{
					return 0;
				}
				if (this.z.avail_out != len || num != 0)
				{
					goto IL_100;
				}
			}
			return 0;
			Block_8:
			throw new IOException("inflating: " + this.z.msg);
			IL_100:
			return len - this.z.avail_out;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000B8246 File Offset: 0x000B6446
		public override void Flush()
		{
			this.inp.Flush();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000B8253 File Offset: 0x000B6453
		public override void Close()
		{
			Platform.Dispose(this.inp);
			base.Close();
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000B8266 File Offset: 0x000B6466
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(this.buf1[0] & byte.MaxValue);
		}

		// Token: 0x04001679 RID: 5753
		protected ZStream z = new ZStream();

		// Token: 0x0400167A RID: 5754
		protected int flushLevel;

		// Token: 0x0400167B RID: 5755
		private const int BUFSIZE = 4192;

		// Token: 0x0400167C RID: 5756
		protected byte[] buf = new byte[4192];

		// Token: 0x0400167D RID: 5757
		private byte[] buf1 = new byte[1];

		// Token: 0x0400167E RID: 5758
		protected Stream inp;

		// Token: 0x0400167F RID: 5759
		private bool nomoreinput;
	}
}
