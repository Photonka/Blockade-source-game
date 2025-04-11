using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000266 RID: 614
	public class PushbackStream : FilterStream
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x000B971D File Offset: 0x000B791D
		public PushbackStream(Stream s) : base(s)
		{
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000B972D File Offset: 0x000B792D
		public override int ReadByte()
		{
			if (this.buf != -1)
			{
				int result = this.buf;
				this.buf = -1;
				return result;
			}
			return base.ReadByte();
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000B974C File Offset: 0x000B794C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.buf != -1 && count > 0)
			{
				buffer[offset] = (byte)this.buf;
				this.buf = -1;
				return 1;
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000B9777 File Offset: 0x000B7977
		public virtual void Unread(int b)
		{
			if (this.buf != -1)
			{
				throw new InvalidOperationException("Can only push back one byte");
			}
			this.buf = (b & 255);
		}

		// Token: 0x040016CC RID: 5836
		private int buf = -1;
	}
}
