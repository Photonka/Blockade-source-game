using System;
using System.Collections.Generic;

namespace BestHTTP.Caching
{
	// Token: 0x020007FE RID: 2046
	internal sealed class HTTPCacheFileLock
	{
		// Token: 0x06004951 RID: 18769 RVA: 0x001A4A74 File Offset: 0x001A2C74
		internal static object Acquire(Uri uri)
		{
			object syncRoot = HTTPCacheFileLock.SyncRoot;
			object result;
			lock (syncRoot)
			{
				object obj;
				if (!HTTPCacheFileLock.FileLocks.TryGetValue(uri, out obj))
				{
					HTTPCacheFileLock.FileLocks.Add(uri, obj = new object());
				}
				result = obj;
			}
			return result;
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x001A4AD4 File Offset: 0x001A2CD4
		internal static void Remove(Uri uri)
		{
			object syncRoot = HTTPCacheFileLock.SyncRoot;
			lock (syncRoot)
			{
				if (HTTPCacheFileLock.FileLocks.ContainsKey(uri))
				{
					HTTPCacheFileLock.FileLocks.Remove(uri);
				}
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x001A4B28 File Offset: 0x001A2D28
		internal static void Clear()
		{
			object syncRoot = HTTPCacheFileLock.SyncRoot;
			lock (syncRoot)
			{
				HTTPCacheFileLock.FileLocks.Clear();
			}
		}

		// Token: 0x04002F32 RID: 12082
		private static Dictionary<Uri, object> FileLocks = new Dictionary<Uri, object>();

		// Token: 0x04002F33 RID: 12083
		private static object SyncRoot = new object();
	}
}
