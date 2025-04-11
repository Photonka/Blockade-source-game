using System;

namespace BestHTTP
{
	// Token: 0x02000177 RID: 375
	public sealed class HTTPRange
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0009559C File Offset: 0x0009379C
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x000955A4 File Offset: 0x000937A4
		public int FirstBytePos { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000955AD File Offset: 0x000937AD
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x000955B5 File Offset: 0x000937B5
		public int LastBytePos { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000955BE File Offset: 0x000937BE
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x000955C6 File Offset: 0x000937C6
		public int ContentLength { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000955CF File Offset: 0x000937CF
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x000955D7 File Offset: 0x000937D7
		public bool IsValid { get; private set; }

		// Token: 0x06000D78 RID: 3448 RVA: 0x000955E0 File Offset: 0x000937E0
		internal HTTPRange()
		{
			this.ContentLength = -1;
			this.IsValid = false;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000955F6 File Offset: 0x000937F6
		internal HTTPRange(int contentLength)
		{
			this.ContentLength = contentLength;
			this.IsValid = false;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0009560C File Offset: 0x0009380C
		internal HTTPRange(int firstBytePosition, int lastBytePosition, int contentLength)
		{
			this.FirstBytePos = firstBytePosition;
			this.LastBytePos = lastBytePosition;
			this.ContentLength = contentLength;
			this.IsValid = (this.FirstBytePos <= this.LastBytePos && this.ContentLength > this.LastBytePos);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0009565C File Offset: 0x0009385C
		public override string ToString()
		{
			return string.Format("{0}-{1}/{2} (valid: {3})", new object[]
			{
				this.FirstBytePos,
				this.LastBytePos,
				this.ContentLength,
				this.IsValid
			});
		}
	}
}
