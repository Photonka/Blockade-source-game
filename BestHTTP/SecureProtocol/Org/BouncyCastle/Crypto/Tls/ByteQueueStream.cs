using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003EA RID: 1002
	public class ByteQueueStream : Stream
	{
		// Token: 0x060028F1 RID: 10481 RVA: 0x0010FADD File Offset: 0x0010DCDD
		public ByteQueueStream()
		{
			this.buffer = new ByteQueue();
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0010FAF0 File Offset: 0x0010DCF0
		public virtual int Available
		{
			get
			{
				return this.buffer.Available;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Flush()
		{
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00092231 File Offset: 0x00090431
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0010FB00 File Offset: 0x0010DD00
		public virtual int Peek(byte[] buf)
		{
			int num = Math.Min(this.buffer.Available, buf.Length);
			this.buffer.Read(buf, 0, num, 0);
			return num;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x00092231 File Offset: 0x00090431
		public override long Position
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

		// Token: 0x060028FB RID: 10491 RVA: 0x0010FB31 File Offset: 0x0010DD31
		public virtual int Read(byte[] buf)
		{
			return this.Read(buf, 0, buf.Length);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0010FB40 File Offset: 0x0010DD40
		public override int Read(byte[] buf, int off, int len)
		{
			int num = Math.Min(this.buffer.Available, len);
			this.buffer.RemoveData(buf, off, num, 0);
			return num;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0010FB6F File Offset: 0x0010DD6F
		public override int ReadByte()
		{
			if (this.buffer.Available == 0)
			{
				return -1;
			}
			return (int)(this.buffer.RemoveData(1, 0)[0] & byte.MaxValue);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00092231 File Offset: 0x00090431
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00092231 File Offset: 0x00090431
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0010FB98 File Offset: 0x0010DD98
		public virtual int Skip(int n)
		{
			int num = Math.Min(this.buffer.Available, n);
			this.buffer.RemoveData(num);
			return num;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0010FBC4 File Offset: 0x0010DDC4
		public virtual void Write(byte[] buf)
		{
			this.buffer.AddData(buf, 0, buf.Length);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0010FBD6 File Offset: 0x0010DDD6
		public override void Write(byte[] buf, int off, int len)
		{
			this.buffer.AddData(buf, off, len);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0010FBE6 File Offset: 0x0010DDE6
		public override void WriteByte(byte b)
		{
			this.buffer.AddData(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x04001A28 RID: 6696
		private readonly ByteQueue buffer;
	}
}
