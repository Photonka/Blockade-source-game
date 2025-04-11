using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using BestHTTP.Logger;
using BestHTTP.PlatformSupport.FileSystem;

namespace BestHTTP.Caching
{
	// Token: 0x02000801 RID: 2049
	public static class HTTPCacheService
	{
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x001A4BD8 File Offset: 0x001A2DD8
		public static bool IsSupported
		{
			get
			{
				if (HTTPCacheService.IsSupportCheckDone)
				{
					return HTTPCacheService.isSupported;
				}
				try
				{
					HTTPManager.IOService.DirectoryExists(HTTPManager.GetRootCacheFolder());
					HTTPCacheService.isSupported = true;
				}
				catch
				{
					HTTPCacheService.isSupported = false;
					HTTPManager.Logger.Warning("HTTPCacheService", "Cache Service Disabled!");
				}
				finally
				{
					HTTPCacheService.IsSupportCheckDone = true;
				}
				return HTTPCacheService.isSupported;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x001A4C50 File Offset: 0x001A2E50
		private static Dictionary<Uri, HTTPCacheFileInfo> Library
		{
			get
			{
				HTTPCacheService.LoadLibrary();
				return HTTPCacheService.library;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x001A4C5C File Offset: 0x001A2E5C
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x001A4C63 File Offset: 0x001A2E63
		internal static string CacheFolder { get; private set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x001A4C6B File Offset: 0x001A2E6B
		// (set) Token: 0x06004963 RID: 18787 RVA: 0x001A4C72 File Offset: 0x001A2E72
		private static string LibraryPath { get; set; }

		// Token: 0x06004965 RID: 18789 RVA: 0x001A4C90 File Offset: 0x001A2E90
		internal static void CheckSetup()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			try
			{
				HTTPCacheService.SetupCacheFolder();
				HTTPCacheService.LoadLibrary();
			}
			catch
			{
			}
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x001A4CC8 File Offset: 0x001A2EC8
		internal static void SetupCacheFolder()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			try
			{
				if (string.IsNullOrEmpty(HTTPCacheService.CacheFolder) || string.IsNullOrEmpty(HTTPCacheService.LibraryPath))
				{
					HTTPCacheService.CacheFolder = Path.Combine(HTTPManager.GetRootCacheFolder(), "HTTPCache");
					if (!HTTPManager.IOService.DirectoryExists(HTTPCacheService.CacheFolder))
					{
						HTTPManager.IOService.DirectoryCreate(HTTPCacheService.CacheFolder);
					}
					HTTPCacheService.LibraryPath = Path.Combine(HTTPManager.GetRootCacheFolder(), "Library");
				}
			}
			catch
			{
				HTTPCacheService.isSupported = false;
				HTTPManager.Logger.Warning("HTTPCacheService", "Cache Service Disabled!");
			}
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x001A4D70 File Offset: 0x001A2F70
		internal static ulong GetNameIdx()
		{
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			ulong result;
			lock (obj)
			{
				ulong nextNameIDX = HTTPCacheService.NextNameIDX;
				do
				{
					HTTPCacheService.NextNameIDX = (HTTPCacheService.NextNameIDX += 1UL) % ulong.MaxValue;
				}
				while (HTTPCacheService.UsedIndexes.ContainsKey(HTTPCacheService.NextNameIDX));
				result = nextNameIDX;
			}
			return result;
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		internal static bool HasEntity(Uri uri)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return false;
			}
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			bool result;
			lock (obj)
			{
				result = HTTPCacheService.Library.ContainsKey(uri);
			}
			return result;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x001A4E2C File Offset: 0x001A302C
		public static bool DeleteEntity(Uri uri, bool removeFromLibrary = true)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return false;
			}
			object obj = HTTPCacheFileLock.Acquire(uri);
			bool result;
			lock (obj)
			{
				Dictionary<Uri, HTTPCacheFileInfo> obj2 = HTTPCacheService.Library;
				lock (obj2)
				{
					HTTPCacheFileInfo httpcacheFileInfo;
					bool flag3 = HTTPCacheService.Library.TryGetValue(uri, out httpcacheFileInfo);
					if (flag3)
					{
						httpcacheFileInfo.Delete();
					}
					if (flag3 && removeFromLibrary)
					{
						HTTPCacheService.Library.Remove(uri);
						HTTPCacheService.UsedIndexes.Remove(httpcacheFileInfo.MappedNameIDX);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x001A4ED4 File Offset: 0x001A30D4
		internal static bool IsCachedEntityExpiresInTheFuture(HTTPRequest request)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return false;
			}
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				HTTPCacheFileInfo httpcacheFileInfo;
				if (HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpcacheFileInfo))
				{
					return httpcacheFileInfo.WillExpireInTheFuture();
				}
			}
			return false;
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x001A4F38 File Offset: 0x001A3138
		internal static void SetHeaders(HTTPRequest request)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			request.RemoveHeader("If-None-Match");
			request.RemoveHeader("If-Modified-Since");
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				HTTPCacheFileInfo httpcacheFileInfo;
				if (HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpcacheFileInfo))
				{
					httpcacheFileInfo.SetUpRevalidationHeaders(request);
				}
			}
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x001A4FAC File Offset: 0x001A31AC
		internal static HTTPCacheFileInfo GetEntity(Uri uri)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return null;
			}
			HTTPCacheFileInfo result = null;
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				HTTPCacheService.Library.TryGetValue(uri, out result);
			}
			return result;
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x001A5000 File Offset: 0x001A3200
		internal static HTTPResponse GetFullResponse(HTTPRequest request)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return null;
			}
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				HTTPCacheFileInfo httpcacheFileInfo;
				if (HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpcacheFileInfo))
				{
					return httpcacheFileInfo.ReadResponseTo(request);
				}
			}
			return null;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x001A5064 File Offset: 0x001A3264
		internal static bool IsCacheble(Uri uri, HTTPMethods method, HTTPResponse response)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return false;
			}
			if (method != HTTPMethods.Get)
			{
				return false;
			}
			if (response == null)
			{
				return false;
			}
			if (response.StatusCode < 200 || response.StatusCode >= 400)
			{
				return false;
			}
			List<string> headerValues = response.GetHeaderValues("cache-control");
			if (headerValues != null)
			{
				if (headerValues.Exists(delegate(string headerValue)
				{
					string text = headerValue.ToLower();
					return text.Contains("no-store") || text.Contains("no-cache");
				}))
				{
					return false;
				}
			}
			List<string> headerValues2 = response.GetHeaderValues("pragma");
			if (headerValues2 != null)
			{
				if (headerValues2.Exists(delegate(string headerValue)
				{
					string text = headerValue.ToLower();
					return text.Contains("no-store") || text.Contains("no-cache");
				}))
				{
					return false;
				}
			}
			return response.GetHeaderValues("content-range") == null;
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x001A5120 File Offset: 0x001A3320
		internal static HTTPCacheFileInfo Store(Uri uri, HTTPMethods method, HTTPResponse response)
		{
			if (response == null || response.Data == null || response.Data.Length == 0)
			{
				return null;
			}
			if (!HTTPCacheService.IsSupported)
			{
				return null;
			}
			HTTPCacheFileInfo httpcacheFileInfo = null;
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				if (!HTTPCacheService.Library.TryGetValue(uri, out httpcacheFileInfo))
				{
					HTTPCacheService.Library.Add(uri, httpcacheFileInfo = new HTTPCacheFileInfo(uri));
					HTTPCacheService.UsedIndexes.Add(httpcacheFileInfo.MappedNameIDX, httpcacheFileInfo);
				}
				try
				{
					httpcacheFileInfo.Store(response);
					if (HTTPManager.Logger.Level == Loglevels.All)
					{
						HTTPManager.Logger.Verbose("HTTPCacheService", string.Format("{0} - Saved to cache", uri.ToString()));
					}
				}
				catch
				{
					HTTPCacheService.DeleteEntity(uri, true);
					throw;
				}
			}
			return httpcacheFileInfo;
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x001A51F8 File Offset: 0x001A33F8
		internal static Stream PrepareStreamed(Uri uri, HTTPResponse response)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return null;
			}
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			Stream saveStream;
			lock (obj)
			{
				HTTPCacheFileInfo httpcacheFileInfo;
				if (!HTTPCacheService.Library.TryGetValue(uri, out httpcacheFileInfo))
				{
					HTTPCacheService.Library.Add(uri, httpcacheFileInfo = new HTTPCacheFileInfo(uri));
					HTTPCacheService.UsedIndexes.Add(httpcacheFileInfo.MappedNameIDX, httpcacheFileInfo);
				}
				try
				{
					saveStream = httpcacheFileInfo.GetSaveStream(response);
				}
				catch
				{
					HTTPCacheService.DeleteEntity(uri, true);
					throw;
				}
			}
			return saveStream;
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001A5290 File Offset: 0x001A3490
		public static void BeginClear()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			if (HTTPCacheService.InClearThread)
			{
				return;
			}
			HTTPCacheService.InClearThread = true;
			HTTPCacheService.SetupCacheFolder();
			ThreadPool.QueueUserWorkItem(delegate(object param)
			{
				HTTPCacheService.ClearImpl(param);
			});
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x001A52E0 File Offset: 0x001A34E0
		private static void ClearImpl(object param)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			try
			{
				string[] files = HTTPManager.IOService.GetFiles(HTTPCacheService.CacheFolder);
				for (int i = 0; i < files.Length; i++)
				{
					try
					{
						HTTPManager.IOService.FileDelete(files[i]);
					}
					catch
					{
					}
				}
			}
			finally
			{
				HTTPCacheService.UsedIndexes.Clear();
				HTTPCacheService.library.Clear();
				HTTPCacheService.NextNameIDX = 1UL;
				HTTPCacheService.SaveLibrary();
				HTTPCacheService.InClearThread = false;
			}
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001A536C File Offset: 0x001A356C
		public static void BeginMaintainence(HTTPCacheMaintananceParams maintananceParam)
		{
			if (maintananceParam == null)
			{
				throw new ArgumentNullException("maintananceParams == null");
			}
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			if (HTTPCacheService.InMaintainenceThread)
			{
				return;
			}
			HTTPCacheService.InMaintainenceThread = true;
			HTTPCacheService.SetupCacheFolder();
			ThreadPool.QueueUserWorkItem(delegate(object param)
			{
				try
				{
					Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
					lock (obj)
					{
						DateTime t = DateTime.UtcNow - maintananceParam.DeleteOlder;
						List<HTTPCacheFileInfo> list = new List<HTTPCacheFileInfo>();
						foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
						{
							if (keyValuePair.Value.LastAccess < t && HTTPCacheService.DeleteEntity(keyValuePair.Key, false))
							{
								list.Add(keyValuePair.Value);
							}
						}
						for (int i = 0; i < list.Count; i++)
						{
							HTTPCacheService.Library.Remove(list[i].Uri);
							HTTPCacheService.UsedIndexes.Remove(list[i].MappedNameIDX);
						}
						list.Clear();
						ulong num = HTTPCacheService.GetCacheSize();
						if (num > maintananceParam.MaxCacheSize)
						{
							List<HTTPCacheFileInfo> list2 = new List<HTTPCacheFileInfo>(HTTPCacheService.library.Count);
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair2 in HTTPCacheService.library)
							{
								list2.Add(keyValuePair2.Value);
							}
							list2.Sort();
							int num2 = 0;
							while (num >= maintananceParam.MaxCacheSize && num2 < list2.Count)
							{
								try
								{
									HTTPCacheFileInfo httpcacheFileInfo = list2[num2];
									ulong num3 = (ulong)((long)httpcacheFileInfo.BodyLength);
									HTTPCacheService.DeleteEntity(httpcacheFileInfo.Uri, true);
									num -= num3;
								}
								catch
								{
								}
								finally
								{
									num2++;
								}
							}
						}
					}
				}
				finally
				{
					HTTPCacheService.SaveLibrary();
					HTTPCacheService.InMaintainenceThread = false;
				}
			});
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001A53C8 File Offset: 0x001A35C8
		public static int GetCacheEntityCount()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return 0;
			}
			HTTPCacheService.CheckSetup();
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			int count;
			lock (obj)
			{
				count = HTTPCacheService.Library.Count;
			}
			return count;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x001A541C File Offset: 0x001A361C
		public static ulong GetCacheSize()
		{
			ulong num = 0UL;
			if (!HTTPCacheService.IsSupported)
			{
				return num;
			}
			HTTPCacheService.CheckSetup();
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
				{
					if (keyValuePair.Value.BodyLength > 0)
					{
						num += (ulong)((long)keyValuePair.Value.BodyLength);
					}
				}
			}
			return num;
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x001A54C0 File Offset: 0x001A36C0
		private static void LoadLibrary()
		{
			if (HTTPCacheService.library != null)
			{
				return;
			}
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			HTTPCacheService.library = new Dictionary<Uri, HTTPCacheFileInfo>(new UriComparer());
			if (!HTTPManager.IOService.FileExists(HTTPCacheService.LibraryPath))
			{
				HTTPCacheService.DeleteUnusedFiles();
				return;
			}
			try
			{
				Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.library;
				int num;
				lock (obj)
				{
					using (Stream stream = HTTPManager.IOService.CreateFileStream(HTTPCacheService.LibraryPath, FileStreamModes.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(stream))
						{
							num = binaryReader.ReadInt32();
							if (num > 1)
							{
								HTTPCacheService.NextNameIDX = binaryReader.ReadUInt64();
							}
							int num2 = binaryReader.ReadInt32();
							for (int i = 0; i < num2; i++)
							{
								Uri uri = new Uri(binaryReader.ReadString());
								HTTPCacheFileInfo httpcacheFileInfo = new HTTPCacheFileInfo(uri, binaryReader, num);
								if (httpcacheFileInfo.IsExists())
								{
									HTTPCacheService.library.Add(uri, httpcacheFileInfo);
									if (num > 1)
									{
										HTTPCacheService.UsedIndexes.Add(httpcacheFileInfo.MappedNameIDX, httpcacheFileInfo);
									}
								}
							}
						}
					}
				}
				if (num == 1)
				{
					HTTPCacheService.BeginClear();
				}
				else
				{
					HTTPCacheService.DeleteUnusedFiles();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x001A5618 File Offset: 0x001A3818
		internal static void SaveLibrary()
		{
			if (HTTPCacheService.library == null)
			{
				return;
			}
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			try
			{
				Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
				lock (obj)
				{
					using (Stream stream = HTTPManager.IOService.CreateFileStream(HTTPCacheService.LibraryPath, FileStreamModes.Create))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(stream))
						{
							binaryWriter.Write(2);
							binaryWriter.Write(HTTPCacheService.NextNameIDX);
							binaryWriter.Write(HTTPCacheService.Library.Count);
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
							{
								binaryWriter.Write(keyValuePair.Key.ToString());
								keyValuePair.Value.SaveTo(binaryWriter);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x001A5738 File Offset: 0x001A3938
		internal static void SetBodyLength(Uri uri, int bodyLength)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
			lock (obj)
			{
				HTTPCacheFileInfo httpcacheFileInfo;
				if (HTTPCacheService.Library.TryGetValue(uri, out httpcacheFileInfo))
				{
					httpcacheFileInfo.BodyLength = bodyLength;
				}
				else
				{
					HTTPCacheService.Library.Add(uri, httpcacheFileInfo = new HTTPCacheFileInfo(uri, DateTime.UtcNow, bodyLength));
					HTTPCacheService.UsedIndexes.Add(httpcacheFileInfo.MappedNameIDX, httpcacheFileInfo);
				}
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x001A57BC File Offset: 0x001A39BC
		private static void DeleteUnusedFiles()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			HTTPCacheService.CheckSetup();
			string[] files = HTTPManager.IOService.GetFiles(HTTPCacheService.CacheFolder);
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					string fileName = Path.GetFileName(files[i]);
					ulong key = 0UL;
					bool flag = false;
					if (ulong.TryParse(fileName, NumberStyles.AllowHexSpecifier, null, out key))
					{
						Dictionary<Uri, HTTPCacheFileInfo> obj = HTTPCacheService.Library;
						lock (obj)
						{
							flag = !HTTPCacheService.UsedIndexes.ContainsKey(key);
							goto IL_70;
						}
					}
					flag = true;
					IL_70:
					if (flag)
					{
						HTTPManager.IOService.FileDelete(files[i]);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x04002F36 RID: 12086
		private const int LibraryVersion = 2;

		// Token: 0x04002F37 RID: 12087
		private static bool isSupported;

		// Token: 0x04002F38 RID: 12088
		private static bool IsSupportCheckDone;

		// Token: 0x04002F39 RID: 12089
		private static Dictionary<Uri, HTTPCacheFileInfo> library;

		// Token: 0x04002F3A RID: 12090
		private static Dictionary<ulong, HTTPCacheFileInfo> UsedIndexes = new Dictionary<ulong, HTTPCacheFileInfo>();

		// Token: 0x04002F3D RID: 12093
		private static bool InClearThread;

		// Token: 0x04002F3E RID: 12094
		private static bool InMaintainenceThread;

		// Token: 0x04002F3F RID: 12095
		private static ulong NextNameIDX = 1UL;
	}
}
