using System;
using System.IO;
using System.Text;
using BestHTTP.Extensions;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007E6 RID: 2022
	public class GZipStream : Stream
	{
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x0019D7A4 File Offset: 0x0019B9A4
		// (set) Token: 0x0600485B RID: 18523 RVA: 0x0019D7AC File Offset: 0x0019B9AC
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x0019D7C8 File Offset: 0x0019B9C8
		// (set) Token: 0x0600485D RID: 18525 RVA: 0x0019D7D0 File Offset: 0x0019B9D0
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x0019D86F File Offset: 0x0019BA6F
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0019D877 File Offset: 0x0019BA77
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0019D883 File Offset: 0x0019BA83
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x0019D88F File Offset: 0x0019BA8F
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x0019D89B File Offset: 0x0019BA9B
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x0019D8B8 File Offset: 0x0019BAB8
		// (set) Token: 0x06004864 RID: 18532 RVA: 0x0019D8C5 File Offset: 0x0019BAC5
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x0019D8E6 File Offset: 0x0019BAE6
		// (set) Token: 0x06004866 RID: 18534 RVA: 0x0019D8F4 File Offset: 0x0019BAF4
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x0019D960 File Offset: 0x0019BB60
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x0019D972 File Offset: 0x0019BB72
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x0019D984 File Offset: 0x0019BB84
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x0019D9E4 File Offset: 0x0019BBE4
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x0019DA09 File Offset: 0x0019BC09
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x0019DA2E File Offset: 0x0019BC2E
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x0019DA50 File Offset: 0x0019BC50
		// (set) Token: 0x06004870 RID: 18544 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x0019DAB4 File Offset: 0x0019BCB4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return result;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0019DB13 File Offset: 0x0019BD13
		public override void SetLength(long value)
		{
			this._baseStream.SetLength(value);
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x0019DB24 File Offset: 0x0019BD24
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x0019DB84 File Offset: 0x0019BD84
		private int EmitHeader()
		{
			byte[] array = (this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment);
			byte[] array2 = (this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName);
			int num = (this.Comment == null) ? 0 : (array.Length + 1);
			int num2 = (this.FileName == null) ? 0 : (array2.Length + 1);
			byte[] array3 = VariableSizedBufferPool.Get((long)(10 + num + num2), false);
			int num3 = 0;
			array3[num3++] = 31;
			array3[num3++] = 139;
			array3[num3++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num3++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			Array.Copy(BitConverter.GetBytes((int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds), 0, array3, num3, 4);
			num3 += 4;
			array3[num3++] = 0;
			array3[num3++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num3, num2 - 1);
				num3 += num2 - 1;
				array3[num3++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num3, num - 1);
				num3 += num - 1;
				array3[num3++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			int result = array3.Length;
			VariableSizedBufferPool.Release(array3);
			return result;
		}

		// Token: 0x04002E3D RID: 11837
		public DateTime? LastModified;

		// Token: 0x04002E3E RID: 11838
		private int _headerByteCount;

		// Token: 0x04002E3F RID: 11839
		internal ZlibBaseStream _baseStream;

		// Token: 0x04002E40 RID: 11840
		private bool _disposed;

		// Token: 0x04002E41 RID: 11841
		private bool _firstReadDone;

		// Token: 0x04002E42 RID: 11842
		private string _FileName;

		// Token: 0x04002E43 RID: 11843
		private string _Comment;

		// Token: 0x04002E44 RID: 11844
		private int _Crc32;

		// Token: 0x04002E45 RID: 11845
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04002E46 RID: 11846
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
