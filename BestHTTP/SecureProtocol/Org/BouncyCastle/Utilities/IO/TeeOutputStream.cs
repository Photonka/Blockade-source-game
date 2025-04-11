using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x0200026A RID: 618
	public class TeeOutputStream : BaseOutputStream
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x000B99E3 File Offset: 0x000B7BE3
		public TeeOutputStream(Stream output, Stream tee)
		{
			this.output = output;
			this.tee = tee;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000B99F9 File Offset: 0x000B7BF9
		public override void Close()
		{
			Platform.Dispose(this.output);
			Platform.Dispose(this.tee);
			base.Close();
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000B9A17 File Offset: 0x000B7C17
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.output.Write(buffer, offset, count);
			this.tee.Write(buffer, offset, count);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000B9A35 File Offset: 0x000B7C35
		public override void WriteByte(byte b)
		{
			this.output.WriteByte(b);
			this.tee.WriteByte(b);
		}

		// Token: 0x040016D0 RID: 5840
		private readonly Stream output;

		// Token: 0x040016D1 RID: 5841
		private readonly Stream tee;
	}
}
