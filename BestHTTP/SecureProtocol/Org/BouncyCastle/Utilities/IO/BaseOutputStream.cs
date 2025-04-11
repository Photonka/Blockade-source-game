using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000263 RID: 611
	public abstract class BaseOutputStream : Stream
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000B95DC File Offset: 0x000B77DC
		public sealed override bool CanWrite
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000B95E7 File Offset: 0x000B77E7
		public override void Close()
		{
			this.closed = true;
			base.Close();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Flush()
		{
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00092231 File Offset: 0x00090431
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

		// Token: 0x060016FB RID: 5883 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000B95F8 File Offset: 0x000B77F8
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = offset + count;
			for (int i = offset; i < num; i++)
			{
				this.WriteByte(buffer[i]);
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000B961E File Offset: 0x000B781E
		public virtual void Write(params byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000B962B File Offset: 0x000B782B
		public override void WriteByte(byte b)
		{
			this.Write(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x040016CA RID: 5834
		private bool closed;
	}
}
