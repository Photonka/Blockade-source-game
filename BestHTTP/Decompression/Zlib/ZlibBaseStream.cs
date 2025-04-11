using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP.Decompression.Crc;
using BestHTTP.Extensions;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F6 RID: 2038
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060048A4 RID: 18596 RVA: 0x001A14F6 File Offset: 0x0019F6F6
		internal int Crc32
		{
			get
			{
				if (this.crc == null)
				{
					return 0;
				}
				return this.crc.Crc32Result;
			}
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x001A150D File Offset: 0x0019F70D
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen) : this(stream, compressionMode, level, flavor, leaveOpen, 15)
		{
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x001A1520 File Offset: 0x0019F720
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen, int windowBits)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			this.windowBitsMax = windowBits;
			if (flavor == ZlibStreamFlavor.GZIP)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x001A1599 File Offset: 0x0019F799
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x001A15A4 File Offset: 0x0019F7A4
		private ZlibCodec z
		{
			get
			{
				if (this._z == null)
				{
					bool flag = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					if (this._compressionMode == CompressionMode.Decompress)
					{
						this._z.InitializeInflate(this.windowBitsMax, flag);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, this.windowBitsMax, flag);
					}
				}
				return this._z;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x001A1620 File Offset: 0x0019F820
		private byte[] workingBuffer
		{
			get
			{
				if (this._workingBuffer == null)
				{
					this._workingBuffer = VariableSizedBufferPool.Get((long)this._bufferSize, true);
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x001A1644 File Offset: 0x0019F844
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else if (this._streamMode != ZlibBaseStream.StreamMode.Writer)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			this.z.InputBuffer = buffer;
			this._z.NextIn = offset;
			this._z.AvailableBytesIn = count;
			for (;;)
			{
				this._z.OutputBuffer = this.workingBuffer;
				this._z.NextOut = 0;
				this._z.AvailableBytesOut = this._workingBuffer.Length;
				int num = this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode);
				if (num != 0 && num != 1)
				{
					break;
				}
				this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
				bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
				if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
				{
					flag = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
				}
				if (flag)
				{
					return;
				}
			}
			throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x001A17CC File Offset: 0x0019F9CC
		private void finish()
		{
			if (this._z == null)
			{
				return;
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Writer)
			{
				int num;
				for (;;)
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
					if (num != 1 && num != 0)
					{
						break;
					}
					if (this._workingBuffer.Length - this._z.AvailableBytesOut > 0)
					{
						this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					}
					bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
					{
						flag = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
					}
					if (flag)
					{
						goto Block_12;
					}
				}
				string text = (this._wantCompress ? "de" : "in") + "flating";
				if (this._z.Message == null)
				{
					throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
				}
				throw new ZlibException(text + ": " + this._z.Message);
				Block_12:
				this.Flush();
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					if (this._wantCompress)
					{
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int value = (int)(this.crc.TotalBytesRead & (long)((ulong)-1));
						this._stream.Write(BitConverter.GetBytes(value), 0, 4);
						return;
					}
					throw new ZlibException("Writing with decompression is not supported.");
				}
			}
			else if (this._streamMode == ZlibBaseStream.StreamMode.Reader && this._flavor == ZlibStreamFlavor.GZIP)
			{
				if (this._wantCompress)
				{
					throw new ZlibException("Reading with compression is not supported.");
				}
				if (this._z.TotalBytesOut == 0L)
				{
					return;
				}
				byte[] array = VariableSizedBufferPool.Get(8L, true);
				if (this._z.AvailableBytesIn < 8)
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, this._z.AvailableBytesIn);
					int num2 = 8 - this._z.AvailableBytesIn;
					int num3 = this._stream.Read(array, this._z.AvailableBytesIn, num2);
					if (num2 != num3)
					{
						VariableSizedBufferPool.Release(array);
						throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", this._z.AvailableBytesIn + num3));
					}
				}
				else
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, 8);
				}
				int num4 = BitConverter.ToInt32(array, 0);
				int crc32Result2 = this.crc.Crc32Result;
				int num5 = BitConverter.ToInt32(array, 4);
				int num6 = (int)(this._z.TotalBytesOut & (long)((ulong)-1));
				if (crc32Result2 != num4)
				{
					VariableSizedBufferPool.Release(array);
					throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", crc32Result2, num4));
				}
				if (num6 != num5)
				{
					VariableSizedBufferPool.Release(array);
					throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num6, num5));
				}
				VariableSizedBufferPool.Release(array);
				return;
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x001A1B3C File Offset: 0x0019FD3C
		private void end()
		{
			if (this.z == null)
			{
				return;
			}
			if (this._wantCompress)
			{
				this._z.EndDeflate();
			}
			else
			{
				this._z.EndInflate();
			}
			this._z = null;
			VariableSizedBufferPool.Release(this._workingBuffer);
			this._workingBuffer = null;
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x001A1B90 File Offset: 0x0019FD90
		public override void Close()
		{
			if (this._stream == null)
			{
				return;
			}
			try
			{
				this.finish();
			}
			finally
			{
				this.end();
				if (!this._leaveOpen)
				{
					this._stream.Dispose();
				}
				this._stream = null;
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x001A1BE0 File Offset: 0x0019FDE0
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x001A1BED File Offset: 0x0019FDED
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
			this.nomoreinput = false;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x001A1C04 File Offset: 0x0019FE04
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			while (this._stream.Read(this._buf1, 0, 1) == 1)
			{
				if (this._buf1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					byte[] array = list.ToArray();
					return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x001A1C70 File Offset: 0x0019FE70
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = VariableSizedBufferPool.Get(10L, true);
			int num2 = this._stream.Read(array, 0, 10);
			if (num2 == 0)
			{
				VariableSizedBufferPool.Release(array);
				return 0;
			}
			if (num2 != 10)
			{
				VariableSizedBufferPool.Release(array);
				throw new ZlibException("Not a valid GZIP stream.");
			}
			if (array[0] != 31 || array[1] != 139 || array[2] != 8)
			{
				VariableSizedBufferPool.Release(array);
				throw new ZlibException("Bad GZIP header.");
			}
			int num3 = BitConverter.ToInt32(array, 4);
			this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
			num += num2;
			if ((array[3] & 4) == 4)
			{
				num2 = this._stream.Read(array, 0, 2);
				num += num2;
				short num4 = (short)((int)array[0] + (int)array[1] * 256);
				byte[] buffer = VariableSizedBufferPool.Get((long)num4, true);
				num2 = this._stream.Read(buffer, 0, (int)num4);
				if (num2 != (int)num4)
				{
					VariableSizedBufferPool.Release(buffer);
					VariableSizedBufferPool.Release(array);
					throw new ZlibException("Unexpected end-of-file reading GZIP header.");
				}
				num += num2;
			}
			if ((array[3] & 8) == 8)
			{
				this._GzipFileName = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 16) == 16)
			{
				this._GzipComment = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 2) == 2)
			{
				this.Read(this._buf1, 0, 1);
			}
			VariableSizedBufferPool.Release(array);
			return num;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x001A1DB4 File Offset: 0x0019FFB4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._stream.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				this._streamMode = ZlibBaseStream.StreamMode.Reader;
				this.z.AvailableBytesIn = 0;
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
					if (this._gzipHeaderByteCount == 0)
					{
						return 0;
					}
				}
			}
			if (this._streamMode != ZlibBaseStream.StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (this.nomoreinput && this._wantCompress)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < buffer.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._z.OutputBuffer = buffer;
			this._z.NextOut = offset;
			this._z.AvailableBytesOut = count;
			this._z.InputBuffer = this.workingBuffer;
			int num;
			for (;;)
			{
				if (this._z.AvailableBytesIn == 0 && !this.nomoreinput)
				{
					this._z.NextIn = 0;
					this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
					if (this._z.AvailableBytesIn == 0)
					{
						this.nomoreinput = true;
					}
				}
				num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_20;
				}
				if (((this.nomoreinput || num == 1) && this._z.AvailableBytesOut == count) || this._z.AvailableBytesOut <= 0 || this.nomoreinput || num != 0)
				{
					goto IL_20A;
				}
			}
			return 0;
			Block_20:
			throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", this._wantCompress ? "de" : "in", num, this._z.Message));
			IL_20A:
			if (this._z.AvailableBytesOut > 0)
			{
				if (num == 0)
				{
					int availableBytesIn = this._z.AvailableBytesIn;
				}
				if (this.nomoreinput && this._wantCompress)
				{
					num = this._z.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num, this._z.Message));
					}
				}
			}
			num = count - this._z.AvailableBytesOut;
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x001A2052 File Offset: 0x001A0252
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x001A205F File Offset: 0x001A025F
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060048B6 RID: 18614 RVA: 0x001A206C File Offset: 0x001A026C
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x001A2079 File Offset: 0x001A0279
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x00096B9B File Offset: 0x00094D9B
		// (set) Token: 0x060048B9 RID: 18617 RVA: 0x00096B9B File Offset: 0x00094D9B
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

		// Token: 0x04002ED1 RID: 11985
		protected internal ZlibCodec _z;

		// Token: 0x04002ED2 RID: 11986
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x04002ED3 RID: 11987
		protected internal FlushType _flushMode;

		// Token: 0x04002ED4 RID: 11988
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x04002ED5 RID: 11989
		protected internal CompressionMode _compressionMode;

		// Token: 0x04002ED6 RID: 11990
		protected internal CompressionLevel _level;

		// Token: 0x04002ED7 RID: 11991
		protected internal bool _leaveOpen;

		// Token: 0x04002ED8 RID: 11992
		protected internal byte[] _workingBuffer;

		// Token: 0x04002ED9 RID: 11993
		protected internal int _bufferSize = 16384;

		// Token: 0x04002EDA RID: 11994
		protected internal int windowBitsMax;

		// Token: 0x04002EDB RID: 11995
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x04002EDC RID: 11996
		protected internal Stream _stream;

		// Token: 0x04002EDD RID: 11997
		protected internal CompressionStrategy Strategy;

		// Token: 0x04002EDE RID: 11998
		private CRC32 crc;

		// Token: 0x04002EDF RID: 11999
		protected internal string _GzipFileName;

		// Token: 0x04002EE0 RID: 12000
		protected internal string _GzipComment;

		// Token: 0x04002EE1 RID: 12001
		protected internal DateTime _GzipMtime;

		// Token: 0x04002EE2 RID: 12002
		protected internal int _gzipHeaderByteCount;

		// Token: 0x04002EE3 RID: 12003
		private bool nomoreinput;

		// Token: 0x020009C0 RID: 2496
		internal enum StreamMode
		{
			// Token: 0x040036A2 RID: 13986
			Writer,
			// Token: 0x040036A3 RID: 13987
			Reader,
			// Token: 0x040036A4 RID: 13988
			Undefined
		}
	}
}
