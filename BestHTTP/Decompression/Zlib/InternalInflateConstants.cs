﻿using System;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007E8 RID: 2024
	internal static class InternalInflateConstants
	{
		// Token: 0x04002E5D RID: 11869
		internal static readonly int[] InflateMask = new int[]
		{
			0,
			1,
			3,
			7,
			15,
			31,
			63,
			127,
			255,
			511,
			1023,
			2047,
			4095,
			8191,
			16383,
			32767,
			65535
		};
	}
}
