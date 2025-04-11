using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200025E RID: 606
	public class ZOutputStream : Stream
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x000B8289 File Offset: 0x000B6489
		private static ZStream GetDefaultZStream(bool nowrap)
		{
			ZStream zstream = new ZStream();
			zstream.inflateInit(nowrap);
			return zstream;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000B859B File Offset: 0x000B679B
		public ZOutputStream(Stream output) : this(output, false)
		{
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000B85A5 File Offset: 0x000B67A5
		public ZOutputStream(Stream output, bool nowrap) : this(output, ZOutputStream.GetDefaultZStream(nowrap))
		{
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000B85B4 File Offset: 0x000B67B4
		public ZOutputStream(Stream output, ZStream z)
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
			this.output = output;
			this.compress = (z.istate == null);
			this.z = z;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000B8621 File Offset: 0x000B6821
		public ZOutputStream(Stream output, int level) : this(output, level, false)
		{
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000B862C File Offset: 0x000B682C
		public ZOutputStream(Stream output, int level, bool nowrap)
		{
			this.buf = new byte[512];
			this.buf1 = new byte[1];
			base..ctor();
			this.output = output;
			this.compress = true;
			this.z = new ZStream();
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000B8682 File Offset: 0x000B6882
		public sealed override bool CanWrite
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000B868D File Offset: 0x000B688D
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.DoClose();
			base.Close();
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000B86A4 File Offset: 0x000B68A4
		private void DoClose()
		{
			try
			{
				this.Finish();
			}
			catch (IOException)
			{
			}
			finally
			{
				this.closed = true;
				this.End();
				Platform.Dispose(this.output);
				this.output = null;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000B86F8 File Offset: 0x000B68F8
		public virtual void End()
		{
			if (this.z == null)
			{
				return;
			}
			if (this.compress)
			{
				this.z.deflateEnd();
			}
			else
			{
				this.z.inflateEnd();
			}
			this.z.free();
			this.z = null;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000B8738 File Offset: 0x000B6938
		public virtual void Finish()
		{
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.buf.Length;
				int num = this.compress ? this.z.deflate(4) : this.z.inflate(4);
				if (num != 1 && num != 0)
				{
					break;
				}
				int num2 = this.buf.Length - this.z.avail_out;
				if (num2 > 0)
				{
					this.output.Write(this.buf, 0, num2);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					goto Block_6;
				}
			}
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
			Block_6:
			this.Flush();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000B8821 File Offset: 0x000B6A21
		public override void Flush()
		{
			this.output.Flush();
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000B882E File Offset: 0x000B6A2E
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x000B8836 File Offset: 0x000B6A36
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00092231 File Offset: 0x00090431
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

		// Token: 0x060016BA RID: 5818 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000B883F File Offset: 0x000B6A3F
		public virtual long TotalIn
		{
			get
			{
				return this.z.total_in;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000B884C File Offset: 0x000B6A4C
		public virtual long TotalOut
		{
			get
			{
				return this.z.total_out;
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000B885C File Offset: 0x000B6A5C
		public override void Write(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return;
			}
			this.z.next_in = b;
			this.z.next_in_index = off;
			this.z.avail_in = len;
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.buf.Length;
				if ((this.compress ? this.z.deflate(this.flushLevel) : this.z.inflate(this.flushLevel)) != 0)
				{
					break;
				}
				this.output.Write(this.buf, 0, this.buf.Length - this.z.avail_out);
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					return;
				}
			}
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000B8965 File Offset: 0x000B6B65
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x04001689 RID: 5769
		private const int BufferSize = 512;

		// Token: 0x0400168A RID: 5770
		protected ZStream z;

		// Token: 0x0400168B RID: 5771
		protected int flushLevel;

		// Token: 0x0400168C RID: 5772
		protected byte[] buf;

		// Token: 0x0400168D RID: 5773
		protected byte[] buf1;

		// Token: 0x0400168E RID: 5774
		protected bool compress;

		// Token: 0x0400168F RID: 5775
		protected Stream output;

		// Token: 0x04001690 RID: 5776
		protected bool closed;
	}
}
