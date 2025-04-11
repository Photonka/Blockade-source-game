using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP.PlatformSupport.FileSystem;

namespace BestHTTP.Cookies
{
	// Token: 0x020007FC RID: 2044
	public static class CookieJar
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004913 RID: 18707 RVA: 0x001A378C File Offset: 0x001A198C
		public static bool IsSavingSupported
		{
			get
			{
				if (CookieJar.IsSupportCheckDone)
				{
					return CookieJar._isSavingSupported;
				}
				try
				{
					HTTPManager.IOService.DirectoryExists(HTTPManager.GetRootCacheFolder());
					CookieJar._isSavingSupported = true;
				}
				catch
				{
					CookieJar._isSavingSupported = false;
					HTTPManager.Logger.Warning("CookieJar", "Cookie saving and loading disabled!");
				}
				finally
				{
					CookieJar.IsSupportCheckDone = true;
				}
				return CookieJar._isSavingSupported;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x001A3804 File Offset: 0x001A1A04
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x001A380B File Offset: 0x001A1A0B
		private static string CookieFolder { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x001A3813 File Offset: 0x001A1A13
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x001A381A File Offset: 0x001A1A1A
		private static string LibraryPath { get; set; }

		// Token: 0x06004918 RID: 18712 RVA: 0x001A3824 File Offset: 0x001A1A24
		internal static void SetupFolder()
		{
			if (!CookieJar.IsSavingSupported)
			{
				return;
			}
			try
			{
				if (string.IsNullOrEmpty(CookieJar.CookieFolder) || string.IsNullOrEmpty(CookieJar.LibraryPath))
				{
					CookieJar.CookieFolder = Path.Combine(HTTPManager.GetRootCacheFolder(), "Cookies");
					CookieJar.LibraryPath = Path.Combine(CookieJar.CookieFolder, "Library");
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x001A3890 File Offset: 0x001A1A90
		internal static void Set(HTTPResponse response)
		{
			if (response == null)
			{
				return;
			}
			object locker = CookieJar.Locker;
			lock (locker)
			{
				try
				{
					CookieJar.Maintain();
					List<Cookie> list = new List<Cookie>();
					List<string> headerValues = response.GetHeaderValues("set-cookie");
					if (headerValues != null)
					{
						foreach (string header in headerValues)
						{
							try
							{
								Cookie cookie = Cookie.Parse(header, response.baseRequest.CurrentUri);
								if (cookie != null)
								{
									int num;
									Cookie cookie2 = CookieJar.Find(cookie, out num);
									if (!string.IsNullOrEmpty(cookie.Value) && cookie.WillExpireInTheFuture())
									{
										if (cookie2 == null)
										{
											CookieJar.Cookies.Add(cookie);
											list.Add(cookie);
										}
										else
										{
											cookie.Date = cookie2.Date;
											CookieJar.Cookies[num] = cookie;
											list.Add(cookie);
										}
									}
									else if (num != -1)
									{
										CookieJar.Cookies.RemoveAt(num);
									}
								}
							}
							catch
							{
							}
						}
						response.Cookies = list;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x001A39E4 File Offset: 0x001A1BE4
		internal static void Maintain()
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				try
				{
					uint num = 0U;
					int i = 0;
					while (i < CookieJar.Cookies.Count)
					{
						Cookie cookie = CookieJar.Cookies[i];
						if (!cookie.WillExpireInTheFuture() || cookie.LastAccess + CookieJar.AccessThreshold < DateTime.UtcNow)
						{
							CookieJar.Cookies.RemoveAt(i);
						}
						else
						{
							if (!cookie.IsSession)
							{
								num += cookie.GuessSize();
							}
							i++;
						}
					}
					if (num > HTTPManager.CookieJarSize)
					{
						CookieJar.Cookies.Sort();
						while (num > HTTPManager.CookieJarSize && CookieJar.Cookies.Count > 0)
						{
							Cookie cookie2 = CookieJar.Cookies[0];
							CookieJar.Cookies.RemoveAt(0);
							num -= cookie2.GuessSize();
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x001A3AE4 File Offset: 0x001A1CE4
		internal static void Persist()
		{
			if (!CookieJar.IsSavingSupported)
			{
				return;
			}
			object locker = CookieJar.Locker;
			lock (locker)
			{
				if (CookieJar.Loaded)
				{
					try
					{
						CookieJar.Maintain();
						if (!HTTPManager.IOService.DirectoryExists(CookieJar.CookieFolder))
						{
							HTTPManager.IOService.DirectoryCreate(CookieJar.CookieFolder);
						}
						using (Stream stream = HTTPManager.IOService.CreateFileStream(CookieJar.LibraryPath, FileStreamModes.Create))
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(stream))
							{
								binaryWriter.Write(1);
								int num = 0;
								using (List<Cookie>.Enumerator enumerator = CookieJar.Cookies.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (!enumerator.Current.IsSession)
										{
											num++;
										}
									}
								}
								binaryWriter.Write(num);
								foreach (Cookie cookie in CookieJar.Cookies)
								{
									if (!cookie.IsSession)
									{
										cookie.SaveTo(binaryWriter);
									}
								}
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x001A3C54 File Offset: 0x001A1E54
		internal static void Load()
		{
			if (!CookieJar.IsSavingSupported)
			{
				return;
			}
			object locker = CookieJar.Locker;
			lock (locker)
			{
				if (!CookieJar.Loaded)
				{
					CookieJar.SetupFolder();
					try
					{
						CookieJar.Cookies.Clear();
						if (!HTTPManager.IOService.DirectoryExists(CookieJar.CookieFolder))
						{
							HTTPManager.IOService.DirectoryCreate(CookieJar.CookieFolder);
						}
						if (HTTPManager.IOService.FileExists(CookieJar.LibraryPath))
						{
							using (Stream stream = HTTPManager.IOService.CreateFileStream(CookieJar.LibraryPath, FileStreamModes.Open))
							{
								using (BinaryReader binaryReader = new BinaryReader(stream))
								{
									binaryReader.ReadInt32();
									int num = binaryReader.ReadInt32();
									for (int i = 0; i < num; i++)
									{
										Cookie cookie = new Cookie();
										cookie.LoadFrom(binaryReader);
										if (cookie.WillExpireInTheFuture())
										{
											CookieJar.Cookies.Add(cookie);
										}
									}
								}
							}
						}
					}
					catch
					{
						CookieJar.Cookies.Clear();
					}
					finally
					{
						CookieJar.Loaded = true;
					}
				}
			}
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x001A3DA0 File Offset: 0x001A1FA0
		public static List<Cookie> Get(Uri uri)
		{
			object locker = CookieJar.Locker;
			List<Cookie> result;
			lock (locker)
			{
				CookieJar.Load();
				List<Cookie> list = null;
				for (int i = 0; i < CookieJar.Cookies.Count; i++)
				{
					Cookie cookie = CookieJar.Cookies[i];
					if (cookie.WillExpireInTheFuture() && uri.Host.IndexOf(cookie.Domain) != -1 && uri.AbsolutePath.StartsWith(cookie.Path))
					{
						if (list == null)
						{
							list = new List<Cookie>();
						}
						list.Add(cookie);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x001A3E4C File Offset: 0x001A204C
		public static void Set(Uri uri, Cookie cookie)
		{
			CookieJar.Set(cookie);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x001A3E54 File Offset: 0x001A2054
		public static void Set(Cookie cookie)
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				CookieJar.Load();
				int num;
				CookieJar.Find(cookie, out num);
				if (num >= 0)
				{
					CookieJar.Cookies[num] = cookie;
				}
				else
				{
					CookieJar.Cookies.Add(cookie);
				}
			}
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x001A3EB8 File Offset: 0x001A20B8
		public static List<Cookie> GetAll()
		{
			object locker = CookieJar.Locker;
			List<Cookie> cookies;
			lock (locker)
			{
				CookieJar.Load();
				cookies = CookieJar.Cookies;
			}
			return cookies;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x001A3F00 File Offset: 0x001A2100
		public static void Clear()
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				CookieJar.Load();
				CookieJar.Cookies.Clear();
			}
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x001A3F48 File Offset: 0x001A2148
		public static void Clear(TimeSpan olderThan)
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				CookieJar.Load();
				int i = 0;
				while (i < CookieJar.Cookies.Count)
				{
					Cookie cookie = CookieJar.Cookies[i];
					if (!cookie.WillExpireInTheFuture() || cookie.Date + olderThan < DateTime.UtcNow)
					{
						CookieJar.Cookies.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x001A3FD4 File Offset: 0x001A21D4
		public static void Clear(string domain)
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				CookieJar.Load();
				int i = 0;
				while (i < CookieJar.Cookies.Count)
				{
					Cookie cookie = CookieJar.Cookies[i];
					if (!cookie.WillExpireInTheFuture() || cookie.Domain.IndexOf(domain) != -1)
					{
						CookieJar.Cookies.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x001A4058 File Offset: 0x001A2258
		public static void Remove(Uri uri, string name)
		{
			object locker = CookieJar.Locker;
			lock (locker)
			{
				CookieJar.Load();
				int i = 0;
				while (i < CookieJar.Cookies.Count)
				{
					Cookie cookie = CookieJar.Cookies[i];
					if (cookie.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && uri.Host.IndexOf(cookie.Domain) != -1)
					{
						CookieJar.Cookies.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001A40E8 File Offset: 0x001A22E8
		private static Cookie Find(Cookie cookie, out int idx)
		{
			for (int i = 0; i < CookieJar.Cookies.Count; i++)
			{
				Cookie cookie2 = CookieJar.Cookies[i];
				if (cookie2.Equals(cookie))
				{
					idx = i;
					return cookie2;
				}
			}
			idx = -1;
			return null;
		}

		// Token: 0x04002F1C RID: 12060
		private const int Version = 1;

		// Token: 0x04002F1D RID: 12061
		public static TimeSpan AccessThreshold = TimeSpan.FromDays(7.0);

		// Token: 0x04002F1E RID: 12062
		private static List<Cookie> Cookies = new List<Cookie>();

		// Token: 0x04002F21 RID: 12065
		private static object Locker = new object();

		// Token: 0x04002F22 RID: 12066
		private static bool _isSavingSupported;

		// Token: 0x04002F23 RID: 12067
		private static bool IsSupportCheckDone;

		// Token: 0x04002F24 RID: 12068
		private static bool Loaded;
	}
}
