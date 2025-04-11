using System;
using System.IO;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DC RID: 2012
	public class ReadOnlyBufferedStream : Stream
	{
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x0019A498 File Offset: 0x00198698
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x0019A4A0 File Offset: 0x001986A0
		public bool CheckForDataAvailability { get; set; }

		// Token: 0x060047E9 RID: 18409 RVA: 0x0019A4A9 File Offset: 0x001986A9
		public ReadOnlyBufferedStream(Stream nstream)
		{
			this.stream = nstream;
			this.buf = VariableSizedBufferPool.Get(8192L, true);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0019A4CC File Offset: 0x001986CC
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (size <= this.available)
			{
				Array.Copy(this.buf, this.pos, buffer, offset, size);
				this.available -= size;
				this.pos += size;
				return size;
			}
			int num = 0;
			if (this.available > 0)
			{
				Array.Copy(this.buf, this.pos, buffer, offset, this.available);
				offset += this.available;
				num += this.available;
				this.available = 0;
				this.pos = 0;
			}
			int num2;
			for (;;)
			{
				try
				{
					this.available = this.stream.Read(this.buf, 0, this.buf.Length);
					this.pos = 0;
				}
				catch (Exception ex)
				{
					if (num > 0)
					{
						return num;
					}
					throw ex;
				}
				if (this.available < 1)
				{
					break;
				}
				num2 = size - num;
				if (num2 <= this.available)
				{
					goto Block_6;
				}
				Array.Copy(this.buf, this.pos, buffer, offset, this.available);
				offset += this.available;
				num += this.available;
				this.pos = 0;
				this.available = 0;
			}
			if (num > 0)
			{
				return num;
			}
			return 0;
			Block_6:
			Array.Copy(this.buf, this.pos, buffer, offset, num2);
			this.available -= num2;
			this.pos += num2;
			num += num2;
			return num;
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0019A638 File Offset: 0x00198838
		public override int ReadByte()
		{
			if (this.available > 0)
			{
				this.available--;
				this.pos++;
				return (int)this.buf[this.pos - 1];
			}
			try
			{
				this.available = this.stream.Read(this.buf, 0, this.buf.Length);
				this.pos = 0;
			}
			catch
			{
				return -1;
			}
			if (this.available < 1)
			{
				return -1;
			}
			this.available--;
			this.pos++;
			return (int)this.buf[this.pos - 1];
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0019A6F4 File Offset: 0x001988F4
		protected override void Dispose(bool disposing)
		{
			if (this.buf != null)
			{
				VariableSizedBufferPool.Release(this.buf);
			}
			this.buf = null;
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override bool CanSeek
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override bool CanWrite
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x00096B9B File Offset: 0x00094D9B
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002DCB RID: 11723
		private Stream stream;

		// Token: 0x04002DCC RID: 11724
		public const int READBUFFER = 8192;

		// Token: 0x04002DCD RID: 11725
		private byte[] buf;

		// Token: 0x04002DCE RID: 11726
		private int available;

		// Token: 0x04002DCF RID: 11727
		private int pos;
	}
}
