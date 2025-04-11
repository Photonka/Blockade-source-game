using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200052A RID: 1322
	public class MacSink : BaseOutputStream
	{
		// Token: 0x06003284 RID: 12932 RVA: 0x0013410B File Offset: 0x0013230B
		public MacSink(IMac mac)
		{
			this.mMac = mac;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x0013411A File Offset: 0x0013231A
		public virtual IMac Mac
		{
			get
			{
				return this.mMac;
			}
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00134122 File Offset: 0x00132322
		public override void WriteByte(byte b)
		{
			this.mMac.Update(b);
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x00134130 File Offset: 0x00132330
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mMac.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x04002036 RID: 8246
		private readonly IMac mMac;
	}
}
