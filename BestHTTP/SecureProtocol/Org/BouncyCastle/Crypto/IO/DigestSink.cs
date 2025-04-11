using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x02000528 RID: 1320
	public class DigestSink : BaseOutputStream
	{
		// Token: 0x0600326F RID: 12911 RVA: 0x00133F62 File Offset: 0x00132162
		public DigestSink(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x00133F71 File Offset: 0x00132171
		public virtual IDigest Digest
		{
			get
			{
				return this.mDigest;
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00133F79 File Offset: 0x00132179
		public override void WriteByte(byte b)
		{
			this.mDigest.Update(b);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00133F87 File Offset: 0x00132187
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mDigest.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x04002032 RID: 8242
		private readonly IDigest mDigest;
	}
}
