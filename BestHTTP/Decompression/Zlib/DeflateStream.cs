using System;
using System.IO;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007E5 RID: 2021
	public class DeflateStream : Stream
	{
		// Token: 0x06004841 RID: 18497 RVA: 0x0019D4E1 File Offset: 0x0019B6E1
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0019D4ED File Offset: 0x0019B6ED
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0019D4F9 File Offset: 0x0019B6F9
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0019D505 File Offset: 0x0019B705
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0019D529 File Offset: 0x0019B729
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen, int windowBits)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen, windowBits);
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x0019D54F File Offset: 0x0019B74F
		// (set) Token: 0x06004847 RID: 18503 RVA: 0x0019D55C File Offset: 0x0019B75C
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004848 RID: 18504 RVA: 0x0019D57D File Offset: 0x0019B77D
		// (set) Token: 0x06004849 RID: 18505 RVA: 0x0019D58C File Offset: 0x0019B78C
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x0019D5F8 File Offset: 0x0019B7F8
		// (set) Token: 0x0600484B RID: 18507 RVA: 0x0019D605 File Offset: 0x0019B805
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x0019D626 File Offset: 0x0019B826
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x0019D638 File Offset: 0x0019B838
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0019D64C File Offset: 0x0019B84C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600484F RID: 18511 RVA: 0x0019D698 File Offset: 0x0019B898
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x0019D6BD File Offset: 0x0019B8BD
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x0019D6E2 File Offset: 0x0019B8E2
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x0019D704 File Offset: 0x0019B904
		// (set) Token: 0x06004855 RID: 18517 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x0019D750 File Offset: 0x0019B950
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0019D773 File Offset: 0x0019B973
		public override void SetLength(long value)
		{
			this._baseStream.SetLength(value);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x0019D781 File Offset: 0x0019B981
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x04002E3A RID: 11834
		internal ZlibBaseStream _baseStream;

		// Token: 0x04002E3B RID: 11835
		internal Stream _innerStream;

		// Token: 0x04002E3C RID: 11836
		private bool _disposed;
	}
}
