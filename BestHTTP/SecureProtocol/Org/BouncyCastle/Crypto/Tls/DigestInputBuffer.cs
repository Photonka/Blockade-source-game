using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000407 RID: 1031
	internal class DigestInputBuffer : MemoryStream
	{
		// Token: 0x060029B5 RID: 10677 RVA: 0x00111959 File Offset: 0x0010FB59
		internal void UpdateDigest(IDigest d)
		{
			Streams.WriteBufTo(this, new DigestInputBuffer.DigStream(d));
		}

		// Token: 0x02000917 RID: 2327
		private class DigStream : BaseOutputStream
		{
			// Token: 0x06004E06 RID: 19974 RVA: 0x001B2FAD File Offset: 0x001B11AD
			internal DigStream(IDigest d)
			{
				this.d = d;
			}

			// Token: 0x06004E07 RID: 19975 RVA: 0x001B2FBC File Offset: 0x001B11BC
			public override void WriteByte(byte b)
			{
				this.d.Update(b);
			}

			// Token: 0x06004E08 RID: 19976 RVA: 0x001B2FCA File Offset: 0x001B11CA
			public override void Write(byte[] buf, int off, int len)
			{
				this.d.BlockUpdate(buf, off, len);
			}

			// Token: 0x040034BA RID: 13498
			private readonly IDigest d;
		}
	}
}
