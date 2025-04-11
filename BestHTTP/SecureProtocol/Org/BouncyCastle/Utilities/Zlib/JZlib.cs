using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000259 RID: 601
	public sealed class JZlib
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x000B7D2E File Offset: 0x000B5F2E
		public static string version()
		{
			return "1.0.7";
		}

		// Token: 0x0400164C RID: 5708
		private const string _version = "1.0.7";

		// Token: 0x0400164D RID: 5709
		public const int Z_NO_COMPRESSION = 0;

		// Token: 0x0400164E RID: 5710
		public const int Z_BEST_SPEED = 1;

		// Token: 0x0400164F RID: 5711
		public const int Z_BEST_COMPRESSION = 9;

		// Token: 0x04001650 RID: 5712
		public const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x04001651 RID: 5713
		public const int Z_FILTERED = 1;

		// Token: 0x04001652 RID: 5714
		public const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x04001653 RID: 5715
		public const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x04001654 RID: 5716
		public const int Z_NO_FLUSH = 0;

		// Token: 0x04001655 RID: 5717
		public const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001656 RID: 5718
		public const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001657 RID: 5719
		public const int Z_FULL_FLUSH = 3;

		// Token: 0x04001658 RID: 5720
		public const int Z_FINISH = 4;

		// Token: 0x04001659 RID: 5721
		public const int Z_OK = 0;

		// Token: 0x0400165A RID: 5722
		public const int Z_STREAM_END = 1;

		// Token: 0x0400165B RID: 5723
		public const int Z_NEED_DICT = 2;

		// Token: 0x0400165C RID: 5724
		public const int Z_ERRNO = -1;

		// Token: 0x0400165D RID: 5725
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x0400165E RID: 5726
		public const int Z_DATA_ERROR = -3;

		// Token: 0x0400165F RID: 5727
		public const int Z_MEM_ERROR = -4;

		// Token: 0x04001660 RID: 5728
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04001661 RID: 5729
		public const int Z_VERSION_ERROR = -6;
	}
}
