using System;
using System.Collections.Generic;

namespace BestHTTP.Caching
{
	// Token: 0x02000800 RID: 2048
	public sealed class UriComparer : IEqualityComparer<Uri>
	{
		// Token: 0x0600495B RID: 18779 RVA: 0x001A4BBA File Offset: 0x001A2DBA
		public bool Equals(Uri x, Uri y)
		{
			return Uri.Compare(x, y, UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped, StringComparison.Ordinal) == 0;
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001A4BCA File Offset: 0x001A2DCA
		public int GetHashCode(Uri uri)
		{
			return uri.ToString().GetHashCode();
		}
	}
}
