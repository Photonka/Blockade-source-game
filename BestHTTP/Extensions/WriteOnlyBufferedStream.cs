using System;
using System.IO;
using BestHTTP.Logger;

namespace BestHTTP.Extensions
{
	// Token: 0x020007E0 RID: 2016
	public sealed class WriteOnlyBufferedStream : Stream
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x0019AE9A File Offset: 0x0019909A
		public override long Length
		{
			get
			{
				return (long)this.buffer.Length;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x0019AEA5 File Offset: 0x001990A5
		// (set) Token: 0x0600480E RID: 18446 RVA: 0x00092D2C File Offset: 0x00090F2C
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				throw new NotImplementedException("Position set");
			}
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0019AEAE File Offset: 0x001990AE
		public WriteOnlyBufferedStream(Stream stream, int bufferSize)
		{
			this.stream = stream;
			this.buffer = VariableSizedBufferPool.Get((long)bufferSize, true);
			this._position = 0;
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0019AED4 File Offset: 0x001990D4
		public override void Flush()
		{
			if (this._position > 0)
			{
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					HTTPManager.Logger.Information("BufferStream", string.Format("Flushing {0:N0} bytes", this._position));
				}
				this.stream.Write(this.buffer, 0, this._position);
				this._position = 0;
			}
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0019AF3C File Offset: 0x0019913C
		public override void Write(byte[] bufferFrom, int offset, int count)
		{
			while (count > 0)
			{
				int num = Math.Min(count, this.buffer.Length - this._position);
				Array.Copy(bufferFrom, offset, this.buffer, this._position, num);
				this._position += num;
				offset += num;
				count -= num;
				if (this._position == this.buffer.Length)
				{
					this.Flush();
				}
			}
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0008F863 File Offset: 0x0008DA63
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void SetLength(long value)
		{
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0019AFA7 File Offset: 0x001991A7
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.buffer != null)
			{
				VariableSizedBufferPool.Release(this.buffer);
			}
			this.buffer = null;
		}

		// Token: 0x04002DE4 RID: 11748
		private int _position;

		// Token: 0x04002DE5 RID: 11749
		private byte[] buffer;

		// Token: 0x04002DE6 RID: 11750
		private Stream stream;
	}
}
