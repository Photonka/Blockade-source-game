using System;
using System.IO;
using UnityEngine;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D3 RID: 2003
	public sealed class BufferPoolMemoryStream : Stream
	{
		// Token: 0x06004786 RID: 18310 RVA: 0x00198F42 File Offset: 0x00197142
		public BufferPoolMemoryStream() : this(0)
		{
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00198F4C File Offset: 0x0019714C
		public BufferPoolMemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.canWrite = true;
			this.internalBuffer = ((capacity > 0) ? VariableSizedBufferPool.Get((long)capacity, true) : VariableSizedBufferPool.NoData);
			this.capacity = this.internalBuffer.Length;
			this.expandable = true;
			this.allowGetBuffer = true;
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00198FAA File Offset: 0x001971AA
		public BufferPoolMemoryStream(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InternalConstructor(buffer, 0, buffer.Length, true, false);
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x00198FCD File Offset: 0x001971CD
		public BufferPoolMemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InternalConstructor(buffer, 0, buffer.Length, writable, false);
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00198FF0 File Offset: 0x001971F0
		public BufferPoolMemoryStream(byte[] buffer, int index, int count)
		{
			this.InternalConstructor(buffer, index, count, true, false);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00199003 File Offset: 0x00197203
		public BufferPoolMemoryStream(byte[] buffer, int index, int count, bool writable)
		{
			this.InternalConstructor(buffer, index, count, writable, false);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00199017 File Offset: 0x00197217
		public BufferPoolMemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			this.InternalConstructor(buffer, index, count, writable, publiclyVisible);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x0019902C File Offset: 0x0019722C
		private void InternalConstructor(byte[] buffer, int index, int count, bool writable, bool publicallyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("index or count is less than 0.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("index+count", "The size of the buffer is less than index + count.");
			}
			this.canWrite = writable;
			this.internalBuffer = buffer;
			this.capacity = count + index;
			this.length = this.capacity;
			this.position = index;
			this.initialIndex = index;
			this.allowGetBuffer = publicallyVisible;
			this.expandable = false;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001990B3 File Offset: 0x001972B3
		private void CheckIfClosedThrowDisposed()
		{
			if (this.streamClosed)
			{
				throw new ObjectDisposedException("MemoryStream");
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x001990C8 File Offset: 0x001972C8
		public override bool CanRead
		{
			get
			{
				return !this.streamClosed;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x001990C8 File Offset: 0x001972C8
		public override bool CanSeek
		{
			get
			{
				return !this.streamClosed;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x001990D3 File Offset: 0x001972D3
		public override bool CanWrite
		{
			get
			{
				return !this.streamClosed && this.canWrite;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x001990E5 File Offset: 0x001972E5
		// (set) Token: 0x06004793 RID: 18323 RVA: 0x001990FC File Offset: 0x001972FC
		public int Capacity
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return this.capacity - this.initialIndex;
			}
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value == this.capacity)
				{
					return;
				}
				if (!this.expandable)
				{
					throw new NotSupportedException("Cannot expand this MemoryStream");
				}
				if (value < 0 || value < this.length)
				{
					throw new ArgumentOutOfRangeException("value", string.Concat(new object[]
					{
						"New capacity cannot be negative or less than the current capacity ",
						value,
						" ",
						this.capacity
					}));
				}
				byte[] dst = null;
				if (value != 0)
				{
					dst = VariableSizedBufferPool.Get((long)value, true);
					Buffer.BlockCopy(this.internalBuffer, 0, dst, 0, this.length);
				}
				this.dirty_bytes = 0;
				VariableSizedBufferPool.Release(this.internalBuffer);
				this.internalBuffer = dst;
				this.capacity = ((this.internalBuffer != null) ? this.internalBuffer.Length : 0);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x001991CA File Offset: 0x001973CA
		public override long Length
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.length - this.initialIndex);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x001991E0 File Offset: 0x001973E0
		// (set) Token: 0x06004796 RID: 18326 RVA: 0x001991F8 File Offset: 0x001973F8
		public override long Position
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.position - this.initialIndex);
			}
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Position cannot be negative");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Position must be non-negative and less than 2^31 - 1 - origin");
				}
				this.position = this.initialIndex + (int)value;
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00199248 File Offset: 0x00197448
		protected override void Dispose(bool disposing)
		{
			this.streamClosed = true;
			this.expandable = false;
			if (this.internalBuffer != null)
			{
				VariableSizedBufferPool.Release(this.internalBuffer);
			}
			this.internalBuffer = null;
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Flush()
		{
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00199272 File Offset: 0x00197472
		public byte[] GetBuffer()
		{
			if (!this.allowGetBuffer)
			{
				throw new UnauthorizedAccessException();
			}
			return this.internalBuffer;
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00199288 File Offset: 0x00197488
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position >= this.length || count == 0)
			{
				return 0;
			}
			if (this.position > this.length - count)
			{
				count = this.length - this.position;
			}
			Buffer.BlockCopy(this.internalBuffer, this.position, buffer, offset, count);
			this.position += count;
			return count;
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0019932C File Offset: 0x0019752C
		public override int ReadByte()
		{
			this.CheckIfClosedThrowDisposed();
			if (this.position >= this.length)
			{
				return -1;
			}
			byte[] array = this.internalBuffer;
			int num = this.position;
			this.position = num + 1;
			return array[num];
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00199368 File Offset: 0x00197568
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.CheckIfClosedThrowDisposed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("Offset out of range. " + offset);
			}
			int num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				num = this.initialIndex;
				break;
			case SeekOrigin.Current:
				num = this.position;
				break;
			case SeekOrigin.End:
				num = this.length;
				break;
			default:
				throw new ArgumentException("loc", "Invalid SeekOrigin");
			}
			num += (int)offset;
			if (num < this.initialIndex)
			{
				throw new IOException("Attempted to seek before start of MemoryStream.");
			}
			this.position = num;
			return (long)this.position;
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x00199410 File Offset: 0x00197610
		private int CalculateNewCapacity(int minimum)
		{
			if (minimum < 256)
			{
				minimum = 256;
			}
			if (minimum < this.capacity * 2)
			{
				minimum = this.capacity * 2;
			}
			if (!Mathf.IsPowerOfTwo(minimum))
			{
				minimum = Mathf.NextPowerOfTwo(minimum);
			}
			return minimum;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00199447 File Offset: 0x00197647
		private void Expand(int newSize)
		{
			if (newSize > this.capacity)
			{
				this.Capacity = this.CalculateNewCapacity(newSize);
				return;
			}
			if (this.dirty_bytes > 0)
			{
				Array.Clear(this.internalBuffer, this.length, this.dirty_bytes);
				this.dirty_bytes = 0;
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00199488 File Offset: 0x00197688
		public override void SetLength(long value)
		{
			if (!this.expandable && value > (long)this.capacity)
			{
				throw new NotSupportedException("Expanding this MemoryStream is not supported");
			}
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this MemoryStream");
			}
			if (value < 0L || value + (long)this.initialIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = (int)value + this.initialIndex;
			if (num > this.length)
			{
				this.Expand(num);
			}
			else if (num < this.length)
			{
				this.dirty_bytes += this.length - num;
			}
			this.length = num;
			if (this.position > this.length)
			{
				this.position = this.length;
			}
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x00199542 File Offset: 0x00197742
		public byte[] ToArray()
		{
			return this.ToArray(false);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0019954C File Offset: 0x0019774C
		public byte[] ToArray(bool canBeLarger)
		{
			int num = this.length - this.initialIndex;
			byte[] array = (num > 0) ? VariableSizedBufferPool.Get((long)num, canBeLarger) : VariableSizedBufferPool.NoData;
			if (this.internalBuffer != null)
			{
				Buffer.BlockCopy(this.internalBuffer, this.initialIndex, array, 0, num);
			}
			return array;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00199598 File Offset: 0x00197798
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position > this.length - count)
			{
				this.Expand(this.position + count);
			}
			Buffer.BlockCopy(buffer, offset, this.internalBuffer, this.position, count);
			this.position += count;
			if (this.position >= this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0019964C File Offset: 0x0019784C
		public override void WriteByte(byte value)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (this.position >= this.length)
			{
				this.Expand(this.position + 1);
				this.length = this.position + 1;
			}
			byte[] array = this.internalBuffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = value;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x001996B5 File Offset: 0x001978B5
		public void WriteTo(Stream stream)
		{
			this.CheckIfClosedThrowDisposed();
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.Write(this.internalBuffer, this.initialIndex, this.length - this.initialIndex);
		}

		// Token: 0x04002DB3 RID: 11699
		private bool canWrite;

		// Token: 0x04002DB4 RID: 11700
		private bool allowGetBuffer;

		// Token: 0x04002DB5 RID: 11701
		private int capacity;

		// Token: 0x04002DB6 RID: 11702
		private int length;

		// Token: 0x04002DB7 RID: 11703
		private byte[] internalBuffer;

		// Token: 0x04002DB8 RID: 11704
		private int initialIndex;

		// Token: 0x04002DB9 RID: 11705
		private bool expandable;

		// Token: 0x04002DBA RID: 11706
		private bool streamClosed;

		// Token: 0x04002DBB RID: 11707
		private int position;

		// Token: 0x04002DBC RID: 11708
		private int dirty_bytes;
	}
}
