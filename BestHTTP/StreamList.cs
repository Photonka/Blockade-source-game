using System;
using System.IO;
using BestHTTP.Extensions;

namespace BestHTTP
{
	// Token: 0x0200016B RID: 363
	public sealed class StreamList : Stream
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00092B1C File Offset: 0x00090D1C
		public StreamList(params Stream[] streams)
		{
			this.Streams = streams;
			this.CurrentIdx = 0;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00092B32 File Offset: 0x00090D32
		public override bool CanRead
		{
			get
			{
				return this.CurrentIdx < this.Streams.Length && this.Streams[this.CurrentIdx].CanRead;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00092B58 File Offset: 0x00090D58
		public override bool CanWrite
		{
			get
			{
				return this.CurrentIdx < this.Streams.Length && this.Streams[this.CurrentIdx].CanWrite;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00092B80 File Offset: 0x00090D80
		public override void Flush()
		{
			if (this.CurrentIdx >= this.Streams.Length)
			{
				return;
			}
			for (int i = 0; i <= this.CurrentIdx; i++)
			{
				this.Streams[i].Flush();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00092BBC File Offset: 0x00090DBC
		public override long Length
		{
			get
			{
				if (this.CurrentIdx >= this.Streams.Length)
				{
					return 0L;
				}
				long num = 0L;
				for (int i = 0; i < this.Streams.Length; i++)
				{
					num += this.Streams[i].Length;
				}
				return num;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00092C04 File Offset: 0x00090E04
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.CurrentIdx >= this.Streams.Length)
			{
				return -1;
			}
			int i;
			for (i = this.Streams[this.CurrentIdx].Read(buffer, offset, count); i < count; i += this.Streams[this.CurrentIdx].Read(buffer, offset + i, count - i))
			{
				int currentIdx = this.CurrentIdx;
				this.CurrentIdx = currentIdx + 1;
				if (currentIdx >= this.Streams.Length)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00092C77 File Offset: 0x00090E77
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.CurrentIdx >= this.Streams.Length)
			{
				return;
			}
			this.Streams[this.CurrentIdx].Write(buffer, offset, count);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00092CA0 File Offset: 0x00090EA0
		public void Write(string str)
		{
			byte[] asciibytes = str.GetASCIIBytes();
			this.Write(asciibytes, 0, asciibytes.Length);
			VariableSizedBufferPool.Release(asciibytes);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00092CC8 File Offset: 0x00090EC8
		protected override void Dispose(bool disposing)
		{
			for (int i = 0; i < this.Streams.Length; i++)
			{
				try
				{
					this.Streams[i].Dispose();
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("StreamList", "Dispose", ex);
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00092D20 File Offset: 0x00090F20
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00092D2C File Offset: 0x00090F2C
		public override long Position
		{
			get
			{
				throw new NotImplementedException("Position get");
			}
			set
			{
				throw new NotImplementedException("Position set");
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00092D38 File Offset: 0x00090F38
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.CurrentIdx >= this.Streams.Length)
			{
				return 0L;
			}
			return this.Streams[this.CurrentIdx].Seek(offset, origin);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00092D61 File Offset: 0x00090F61
		public override void SetLength(long value)
		{
			throw new NotImplementedException("SetLength");
		}

		// Token: 0x04001182 RID: 4482
		private Stream[] Streams;

		// Token: 0x04001183 RID: 4483
		private int CurrentIdx;
	}
}
