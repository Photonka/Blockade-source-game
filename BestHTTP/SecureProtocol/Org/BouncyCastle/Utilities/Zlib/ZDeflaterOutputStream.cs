using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200025B RID: 603
	[Obsolete("Use 'ZOutputStream' instead")]
	public class ZDeflaterOutputStream : Stream
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x000B7DEF File Offset: 0x000B5FEF
		public ZDeflaterOutputStream(Stream outp) : this(outp, 6, false)
		{
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000B7DFA File Offset: 0x000B5FFA
		public ZDeflaterOutputStream(Stream outp, int level) : this(outp, level, false)
		{
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000B7E08 File Offset: 0x000B6008
		public ZDeflaterOutputStream(Stream outp, int level, bool nowrap)
		{
			this.outp = outp;
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0008F863 File Offset: 0x0008DA63
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0008F863 File Offset: 0x0008DA63
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00002B75 File Offset: 0x00000D75
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

		// Token: 0x06001677 RID: 5751 RVA: 0x000B7E58 File Offset: 0x000B6058
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
				this.z.avail_out = 4192;
				if (this.z.deflate(this.flushLevel) != 0)
				{
					break;
				}
				if (this.z.avail_out < 4192)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					return;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0008F863 File Offset: 0x0008DA63
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void SetLength(long value)
		{
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000B7F3E File Offset: 0x000B613E
		public override void Flush()
		{
			this.outp.Flush();
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000B7F4B File Offset: 0x000B614B
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000B7F64 File Offset: 0x000B6164
		public void Finish()
		{
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = 4192;
				int num = this.z.deflate(4);
				if (num != 1 && num != 0)
				{
					break;
				}
				if (4192 - this.z.avail_out > 0)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					goto Block_4;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
			Block_4:
			this.Flush();
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000B802B File Offset: 0x000B622B
		public void End()
		{
			if (this.z == null)
			{
				return;
			}
			this.z.deflateEnd();
			this.z.free();
			this.z = null;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000B8054 File Offset: 0x000B6254
		public override void Close()
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
				this.End();
				Platform.Dispose(this.outp);
				this.outp = null;
			}
			base.Close();
		}

		// Token: 0x04001673 RID: 5747
		protected ZStream z = new ZStream();

		// Token: 0x04001674 RID: 5748
		protected int flushLevel;

		// Token: 0x04001675 RID: 5749
		private const int BUFSIZE = 4192;

		// Token: 0x04001676 RID: 5750
		protected byte[] buf = new byte[4192];

		// Token: 0x04001677 RID: 5751
		private byte[] buf1 = new byte[1];

		// Token: 0x04001678 RID: 5752
		protected Stream outp;
	}
}
