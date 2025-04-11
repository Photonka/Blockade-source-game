using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000449 RID: 1097
	public class TlsDeflateCompression : TlsCompression
	{
		// Token: 0x06002B38 RID: 11064 RVA: 0x00117AE8 File Offset: 0x00115CE8
		public TlsDeflateCompression() : this(-1)
		{
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x00117AF1 File Offset: 0x00115CF1
		public TlsDeflateCompression(int level)
		{
			this.zIn = new ZStream();
			this.zIn.inflateInit();
			this.zOut = new ZStream();
			this.zOut.deflateInit(level);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00117B28 File Offset: 0x00115D28
		public virtual Stream Compress(Stream output)
		{
			return new TlsDeflateCompression.DeflateOutputStream(output, this.zOut, true);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00117B37 File Offset: 0x00115D37
		public virtual Stream Decompress(Stream output)
		{
			return new TlsDeflateCompression.DeflateOutputStream(output, this.zIn, false);
		}

		// Token: 0x04001CE4 RID: 7396
		public const int LEVEL_NONE = 0;

		// Token: 0x04001CE5 RID: 7397
		public const int LEVEL_FASTEST = 1;

		// Token: 0x04001CE6 RID: 7398
		public const int LEVEL_SMALLEST = 9;

		// Token: 0x04001CE7 RID: 7399
		public const int LEVEL_DEFAULT = -1;

		// Token: 0x04001CE8 RID: 7400
		protected readonly ZStream zIn;

		// Token: 0x04001CE9 RID: 7401
		protected readonly ZStream zOut;

		// Token: 0x02000923 RID: 2339
		protected class DeflateOutputStream : ZOutputStream
		{
			// Token: 0x06004E2C RID: 20012 RVA: 0x001B329B File Offset: 0x001B149B
			public DeflateOutputStream(Stream output, ZStream z, bool compress) : base(output, z)
			{
				this.compress = compress;
				this.FlushMode = 2;
			}

			// Token: 0x06004E2D RID: 20013 RVA: 0x00002B75 File Offset: 0x00000D75
			public override void Flush()
			{
			}
		}
	}
}
