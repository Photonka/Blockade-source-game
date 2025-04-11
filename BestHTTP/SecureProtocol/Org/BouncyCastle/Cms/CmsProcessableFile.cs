using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E2 RID: 1506
	public class CmsProcessableFile : CmsProcessable, CmsReadable
	{
		// Token: 0x06003990 RID: 14736 RVA: 0x0016A2BD File Offset: 0x001684BD
		public CmsProcessableFile(FileInfo file) : this(file, 32768)
		{
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0016A2CB File Offset: 0x001684CB
		public CmsProcessableFile(FileInfo file, int bufSize)
		{
			this._file = file;
			this._bufSize = bufSize;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0016A2E1 File Offset: 0x001684E1
		public virtual Stream GetInputStream()
		{
			return new FileStream(this._file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, this._bufSize);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x0016A2FC File Offset: 0x001684FC
		public virtual void Write(Stream zOut)
		{
			Stream inputStream = this.GetInputStream();
			Streams.PipeAll(inputStream, zOut);
			Platform.Dispose(inputStream);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0016A310 File Offset: 0x00168510
		[Obsolete]
		public virtual object GetContent()
		{
			return this._file;
		}

		// Token: 0x040024CC RID: 9420
		private const int DefaultBufSize = 32768;

		// Token: 0x040024CD RID: 9421
		private readonly FileInfo _file;

		// Token: 0x040024CE RID: 9422
		private readonly int _bufSize;
	}
}
