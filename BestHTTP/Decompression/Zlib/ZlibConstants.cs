using System;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F8 RID: 2040
	public static class ZlibConstants
	{
		// Token: 0x04002EF3 RID: 12019
		public const int WindowBitsMax = 15;

		// Token: 0x04002EF4 RID: 12020
		public const int WindowBitsDefault = 15;

		// Token: 0x04002EF5 RID: 12021
		public const int Z_OK = 0;

		// Token: 0x04002EF6 RID: 12022
		public const int Z_STREAM_END = 1;

		// Token: 0x04002EF7 RID: 12023
		public const int Z_NEED_DICT = 2;

		// Token: 0x04002EF8 RID: 12024
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04002EF9 RID: 12025
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04002EFA RID: 12026
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04002EFB RID: 12027
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x04002EFC RID: 12028
		public const int WorkingBufferSizeMin = 1024;
	}
}
