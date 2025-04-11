using System;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F2 RID: 2034
	internal static class InternalConstants
	{
		// Token: 0x04002EB7 RID: 11959
		internal static readonly int MAX_BITS = 15;

		// Token: 0x04002EB8 RID: 11960
		internal static readonly int BL_CODES = 19;

		// Token: 0x04002EB9 RID: 11961
		internal static readonly int D_CODES = 30;

		// Token: 0x04002EBA RID: 11962
		internal static readonly int LITERALS = 256;

		// Token: 0x04002EBB RID: 11963
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x04002EBC RID: 11964
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x04002EBD RID: 11965
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x04002EBE RID: 11966
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04002EBF RID: 11967
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x04002EC0 RID: 11968
		internal static readonly int REPZ_11_138 = 18;
	}
}
