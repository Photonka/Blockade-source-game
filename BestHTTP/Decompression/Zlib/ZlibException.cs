using System;
using System.Runtime.InteropServices;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F0 RID: 2032
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	internal class ZlibException : Exception
	{
		// Token: 0x06004897 RID: 18583 RVA: 0x0008E219 File Offset: 0x0008C419
		public ZlibException()
		{
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0008E285 File Offset: 0x0008C485
		public ZlibException(string s) : base(s)
		{
		}
	}
}
