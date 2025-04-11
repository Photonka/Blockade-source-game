using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000269 RID: 617
	public class TeeInputStream : BaseInputStream
	{
		// Token: 0x06001726 RID: 5926 RVA: 0x000B9954 File Offset: 0x000B7B54
		public TeeInputStream(Stream input, Stream tee)
		{
			this.input = input;
			this.tee = tee;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000B996A File Offset: 0x000B7B6A
		public override void Close()
		{
			Platform.Dispose(this.input);
			Platform.Dispose(this.tee);
			base.Close();
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000B9988 File Offset: 0x000B7B88
		public override int Read(byte[] buf, int off, int len)
		{
			int num = this.input.Read(buf, off, len);
			if (num > 0)
			{
				this.tee.Write(buf, off, num);
			}
			return num;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000B99B8 File Offset: 0x000B7BB8
		public override int ReadByte()
		{
			int num = this.input.ReadByte();
			if (num >= 0)
			{
				this.tee.WriteByte((byte)num);
			}
			return num;
		}

		// Token: 0x040016CE RID: 5838
		private readonly Stream input;

		// Token: 0x040016CF RID: 5839
		private readonly Stream tee;
	}
}
