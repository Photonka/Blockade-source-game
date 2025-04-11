using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000264 RID: 612
	public class FilterStream : Stream
	{
		// Token: 0x06001702 RID: 5890 RVA: 0x000B963F File Offset: 0x000B783F
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000B964E File Offset: 0x000B784E
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000B965B File Offset: 0x000B785B
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x000B9668 File Offset: 0x000B7868
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000B9675 File Offset: 0x000B7875
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x000B9682 File Offset: 0x000B7882
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x000B968F File Offset: 0x000B788F
		public override long Position
		{
			get
			{
				return this.s.Position;
			}
			set
			{
				this.s.Position = value;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000B969D File Offset: 0x000B789D
		public override void Close()
		{
			Platform.Dispose(this.s);
			base.Close();
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000B96B0 File Offset: 0x000B78B0
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x000B96BD File Offset: 0x000B78BD
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000B96CC File Offset: 0x000B78CC
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000B96DA File Offset: 0x000B78DA
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000B96EA File Offset: 0x000B78EA
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000B96F7 File Offset: 0x000B78F7
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000B9707 File Offset: 0x000B7907
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x040016CB RID: 5835
		protected readonly Stream s;
	}
}
