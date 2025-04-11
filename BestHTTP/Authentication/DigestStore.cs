using System;
using System.Collections.Generic;

namespace BestHTTP.Authentication
{
	// Token: 0x02000805 RID: 2053
	public static class DigestStore
	{
		// Token: 0x0600499C RID: 18844 RVA: 0x001A60CC File Offset: 0x001A42CC
		public static Digest Get(Uri uri)
		{
			object locker = DigestStore.Locker;
			Digest result;
			lock (locker)
			{
				Digest digest = null;
				if (DigestStore.Digests.TryGetValue(uri.Host, out digest) && !digest.IsUriProtected(uri))
				{
					result = null;
				}
				else
				{
					result = digest;
				}
			}
			return result;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001A612C File Offset: 0x001A432C
		public static Digest GetOrCreate(Uri uri)
		{
			object locker = DigestStore.Locker;
			Digest result;
			lock (locker)
			{
				Digest digest = null;
				if (!DigestStore.Digests.TryGetValue(uri.Host, out digest))
				{
					DigestStore.Digests.Add(uri.Host, digest = new Digest(uri));
				}
				result = digest;
			}
			return result;
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x001A6198 File Offset: 0x001A4398
		public static void Remove(Uri uri)
		{
			object locker = DigestStore.Locker;
			lock (locker)
			{
				DigestStore.Digests.Remove(uri.Host);
			}
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x001A61E4 File Offset: 0x001A43E4
		public static string FindBest(List<string> authHeaders)
		{
			if (authHeaders == null || authHeaders.Count == 0)
			{
				return string.Empty;
			}
			List<string> list = new List<string>(authHeaders.Count);
			for (int j = 0; j < authHeaders.Count; j++)
			{
				list.Add(authHeaders[j].ToLower());
			}
			int i;
			int i2;
			for (i = 0; i < DigestStore.SupportedAlgorithms.Length; i = i2)
			{
				int num = list.FindIndex((string header) => header.StartsWith(DigestStore.SupportedAlgorithms[i]));
				if (num != -1)
				{
					return authHeaders[num];
				}
				i2 = i + 1;
			}
			return string.Empty;
		}

		// Token: 0x04002F52 RID: 12114
		private static Dictionary<string, Digest> Digests = new Dictionary<string, Digest>();

		// Token: 0x04002F53 RID: 12115
		private static object Locker = new object();

		// Token: 0x04002F54 RID: 12116
		private static string[] SupportedAlgorithms = new string[]
		{
			"digest",
			"basic"
		};
	}
}
