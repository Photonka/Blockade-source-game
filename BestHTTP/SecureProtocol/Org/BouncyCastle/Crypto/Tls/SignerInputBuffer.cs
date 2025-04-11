using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000434 RID: 1076
	internal class SignerInputBuffer : MemoryStream
	{
		// Token: 0x06002AC7 RID: 10951 RVA: 0x00115D24 File Offset: 0x00113F24
		internal void UpdateSigner(ISigner s)
		{
			Streams.WriteBufTo(this, new SignerInputBuffer.SigStream(s));
		}

		// Token: 0x02000922 RID: 2338
		private class SigStream : BaseOutputStream
		{
			// Token: 0x06004E29 RID: 20009 RVA: 0x001B326E File Offset: 0x001B146E
			internal SigStream(ISigner s)
			{
				this.s = s;
			}

			// Token: 0x06004E2A RID: 20010 RVA: 0x001B327D File Offset: 0x001B147D
			public override void WriteByte(byte b)
			{
				this.s.Update(b);
			}

			// Token: 0x06004E2B RID: 20011 RVA: 0x001B328B File Offset: 0x001B148B
			public override void Write(byte[] buf, int off, int len)
			{
				this.s.BlockUpdate(buf, off, len);
			}

			// Token: 0x040034F0 RID: 13552
			private readonly ISigner s;
		}
	}
}
