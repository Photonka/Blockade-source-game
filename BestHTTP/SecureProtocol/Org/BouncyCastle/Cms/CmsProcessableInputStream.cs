using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E3 RID: 1507
	public class CmsProcessableInputStream : CmsProcessable, CmsReadable
	{
		// Token: 0x06003995 RID: 14741 RVA: 0x0016A318 File Offset: 0x00168518
		public CmsProcessableInputStream(Stream input)
		{
			this.input = input;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x0016A327 File Offset: 0x00168527
		public virtual Stream GetInputStream()
		{
			this.CheckSingleUsage();
			return this.input;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0016A335 File Offset: 0x00168535
		public virtual void Write(Stream output)
		{
			this.CheckSingleUsage();
			Streams.PipeAll(this.input, output);
			Platform.Dispose(this.input);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0016A354 File Offset: 0x00168554
		[Obsolete]
		public virtual object GetContent()
		{
			return this.GetInputStream();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0016A35C File Offset: 0x0016855C
		protected virtual void CheckSingleUsage()
		{
			lock (this)
			{
				if (this.used)
				{
					throw new InvalidOperationException("CmsProcessableInputStream can only be used once");
				}
				this.used = true;
			}
		}

		// Token: 0x040024CF RID: 9423
		private readonly Stream input;

		// Token: 0x040024D0 RID: 9424
		private bool used;
	}
}
