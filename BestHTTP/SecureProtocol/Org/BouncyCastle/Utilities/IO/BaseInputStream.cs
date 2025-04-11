using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000262 RID: 610
	public abstract class BaseInputStream : Stream
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000B956D File Offset: 0x000B776D
		public sealed override bool CanRead
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000B9578 File Offset: 0x000B7778
		public override void Close()
		{
			this.closed = true;
			base.Close();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00002B75 File Offset: 0x00000D75
		public sealed override void Flush()
		{
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00092231 File Offset: 0x00090431
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

		// Token: 0x060016EE RID: 5870 RVA: 0x000B9588 File Offset: 0x000B7788
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.ReadByte();
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException)
			{
				if (i == offset)
				{
					throw;
				}
			}
			return i - offset;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040016C9 RID: 5833
		private bool closed;
	}
}
