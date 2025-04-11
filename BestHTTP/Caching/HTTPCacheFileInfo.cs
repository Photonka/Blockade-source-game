using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP.Extensions;
using BestHTTP.PlatformSupport.FileSystem;

namespace BestHTTP.Caching
{
	// Token: 0x020007FD RID: 2045
	public class HTTPCacheFileInfo : IComparable<HTTPCacheFileInfo>
	{
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x001A4151 File Offset: 0x001A2351
		// (set) Token: 0x06004928 RID: 18728 RVA: 0x001A4159 File Offset: 0x001A2359
		internal Uri Uri { get; set; }

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x001A4162 File Offset: 0x001A2362
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x001A416A File Offset: 0x001A236A
		internal DateTime LastAccess { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x001A4173 File Offset: 0x001A2373
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x001A417B File Offset: 0x001A237B
		public int BodyLength { get; set; }

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x001A4184 File Offset: 0x001A2384
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x001A418C File Offset: 0x001A238C
		private string ETag { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x001A4195 File Offset: 0x001A2395
		// (set) Token: 0x06004930 RID: 18736 RVA: 0x001A419D File Offset: 0x001A239D
		private string LastModified { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x001A41A6 File Offset: 0x001A23A6
		// (set) Token: 0x06004932 RID: 18738 RVA: 0x001A41AE File Offset: 0x001A23AE
		private DateTime Expires { get; set; }

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x001A41B7 File Offset: 0x001A23B7
		// (set) Token: 0x06004934 RID: 18740 RVA: 0x001A41BF File Offset: 0x001A23BF
		private long Age { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x001A41C8 File Offset: 0x001A23C8
		// (set) Token: 0x06004936 RID: 18742 RVA: 0x001A41D0 File Offset: 0x001A23D0
		private long MaxAge { get; set; }

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x001A41D9 File Offset: 0x001A23D9
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x001A41E1 File Offset: 0x001A23E1
		private DateTime Date { get; set; }

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x001A41EA File Offset: 0x001A23EA
		// (set) Token: 0x0600493A RID: 18746 RVA: 0x001A41F2 File Offset: 0x001A23F2
		private bool MustRevalidate { get; set; }

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x001A41FB File Offset: 0x001A23FB
		// (set) Token: 0x0600493C RID: 18748 RVA: 0x001A4203 File Offset: 0x001A2403
		private DateTime Received { get; set; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x001A420C File Offset: 0x001A240C
		// (set) Token: 0x0600493E RID: 18750 RVA: 0x001A4214 File Offset: 0x001A2414
		private string ConstructedPath { get; set; }

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x001A421D File Offset: 0x001A241D
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x001A4225 File Offset: 0x001A2425
		internal ulong MappedNameIDX { get; set; }

		// Token: 0x06004941 RID: 18753 RVA: 0x001A422E File Offset: 0x001A242E
		internal HTTPCacheFileInfo(Uri uri) : this(uri, DateTime.UtcNow, -1)
		{
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x001A423D File Offset: 0x001A243D
		internal HTTPCacheFileInfo(Uri uri, DateTime lastAcces, int bodyLength)
		{
			this.Uri = uri;
			this.LastAccess = lastAcces;
			this.BodyLength = bodyLength;
			this.MaxAge = -1L;
			this.MappedNameIDX = HTTPCacheService.GetNameIdx();
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x001A4270 File Offset: 0x001A2470
		internal HTTPCacheFileInfo(Uri uri, BinaryReader reader, int version)
		{
			this.Uri = uri;
			this.LastAccess = DateTime.FromBinary(reader.ReadInt64());
			this.BodyLength = reader.ReadInt32();
			if (version != 1)
			{
				if (version != 2)
				{
					return;
				}
				this.MappedNameIDX = reader.ReadUInt64();
			}
			this.ETag = reader.ReadString();
			this.LastModified = reader.ReadString();
			this.Expires = DateTime.FromBinary(reader.ReadInt64());
			this.Age = reader.ReadInt64();
			this.MaxAge = reader.ReadInt64();
			this.Date = DateTime.FromBinary(reader.ReadInt64());
			this.MustRevalidate = reader.ReadBoolean();
			this.Received = DateTime.FromBinary(reader.ReadInt64());
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x001A432C File Offset: 0x001A252C
		internal void SaveTo(BinaryWriter writer)
		{
			writer.Write(this.LastAccess.ToBinary());
			writer.Write(this.BodyLength);
			writer.Write(this.MappedNameIDX);
			writer.Write(this.ETag);
			writer.Write(this.LastModified);
			writer.Write(this.Expires.ToBinary());
			writer.Write(this.Age);
			writer.Write(this.MaxAge);
			writer.Write(this.Date.ToBinary());
			writer.Write(this.MustRevalidate);
			writer.Write(this.Received.ToBinary());
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x001A43E0 File Offset: 0x001A25E0
		public string GetPath()
		{
			if (this.ConstructedPath != null)
			{
				return this.ConstructedPath;
			}
			return this.ConstructedPath = Path.Combine(HTTPCacheService.CacheFolder, this.MappedNameIDX.ToString("X"));
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001A4422 File Offset: 0x001A2622
		public bool IsExists()
		{
			return HTTPCacheService.IsSupported && HTTPManager.IOService.FileExists(this.GetPath());
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x001A4440 File Offset: 0x001A2640
		internal void Delete()
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			string path = this.GetPath();
			try
			{
				HTTPManager.IOService.FileDelete(path);
			}
			catch
			{
			}
			finally
			{
				this.Reset();
			}
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x001A4490 File Offset: 0x001A2690
		private void Reset()
		{
			this.BodyLength = -1;
			this.ETag = string.Empty;
			this.Expires = DateTime.FromBinary(0L);
			this.LastModified = string.Empty;
			this.Age = 0L;
			this.MaxAge = -1L;
			this.Date = DateTime.FromBinary(0L);
			this.MustRevalidate = false;
			this.Received = DateTime.FromBinary(0L);
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001A44F8 File Offset: 0x001A26F8
		private void SetUpCachingValues(HTTPResponse response)
		{
			response.CacheFileInfo = this;
			this.ETag = response.GetFirstHeaderValue("ETag").ToStrOrEmpty();
			this.Expires = response.GetFirstHeaderValue("Expires").ToDateTime(DateTime.FromBinary(0L));
			this.LastModified = response.GetFirstHeaderValue("Last-Modified").ToStrOrEmpty();
			this.Age = response.GetFirstHeaderValue("Age").ToInt64(0L);
			this.Date = response.GetFirstHeaderValue("Date").ToDateTime(DateTime.FromBinary(0L));
			string firstHeaderValue = response.GetFirstHeaderValue("cache-control");
			if (!string.IsNullOrEmpty(firstHeaderValue))
			{
				string[] array = firstHeaderValue.FindOption("max-age");
				double num;
				if (array != null && double.TryParse(array[1], out num))
				{
					this.MaxAge = (long)((int)num);
				}
				this.MustRevalidate = firstHeaderValue.ToLower().Contains("must-revalidate");
			}
			this.Received = DateTime.UtcNow;
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x001A45E4 File Offset: 0x001A27E4
		internal bool WillExpireInTheFuture()
		{
			if (!this.IsExists())
			{
				return false;
			}
			if (this.MustRevalidate)
			{
				return false;
			}
			if (this.MaxAge != -1L)
			{
				long num = Math.Max(Math.Max(0L, (long)(this.Received - this.Date).TotalSeconds), this.Age);
				long num2 = (long)(DateTime.UtcNow - this.Date).TotalSeconds;
				return num + num2 < this.MaxAge;
			}
			return this.Expires > DateTime.UtcNow;
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001A4670 File Offset: 0x001A2870
		internal void SetUpRevalidationHeaders(HTTPRequest request)
		{
			if (!this.IsExists())
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.ETag))
			{
				request.SetHeader("If-None-Match", this.ETag);
			}
			if (!string.IsNullOrEmpty(this.LastModified))
			{
				request.SetHeader("If-Modified-Since", this.LastModified);
			}
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x001A46C2 File Offset: 0x001A28C2
		public Stream GetBodyStream(out int length)
		{
			if (!this.IsExists())
			{
				length = 0;
				return null;
			}
			length = this.BodyLength;
			this.LastAccess = DateTime.UtcNow;
			Stream stream = HTTPManager.IOService.CreateFileStream(this.GetPath(), FileStreamModes.Open);
			stream.Seek((long)(-(long)length), SeekOrigin.End);
			return stream;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x001A4704 File Offset: 0x001A2904
		internal HTTPResponse ReadResponseTo(HTTPRequest request)
		{
			if (!this.IsExists())
			{
				return null;
			}
			this.LastAccess = DateTime.UtcNow;
			HTTPResponse result;
			using (Stream stream = HTTPManager.IOService.CreateFileStream(this.GetPath(), FileStreamModes.Open))
			{
				HTTPResponse httpresponse = new HTTPResponse(request, stream, request.UseStreaming, true);
				httpresponse.CacheFileInfo = this;
				httpresponse.Receive(this.BodyLength, true);
				result = httpresponse;
			}
			return result;
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x001A477C File Offset: 0x001A297C
		internal void Store(HTTPResponse response)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return;
			}
			string path = this.GetPath();
			if (path.Length > HTTPManager.MaxPathLength)
			{
				return;
			}
			if (HTTPManager.IOService.FileExists(path))
			{
				this.Delete();
			}
			using (Stream stream = HTTPManager.IOService.CreateFileStream(this.GetPath(), FileStreamModes.Create))
			{
				stream.WriteLine("HTTP/1.1 {0} {1}", new object[]
				{
					response.StatusCode,
					response.Message
				});
				foreach (KeyValuePair<string, List<string>> keyValuePair in response.Headers)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						stream.WriteLine("{0}: {1}", new object[]
						{
							keyValuePair.Key,
							keyValuePair.Value[i]
						});
					}
				}
				stream.WriteLine();
				stream.Write(response.Data, 0, response.Data.Length);
			}
			this.BodyLength = response.Data.Length;
			this.LastAccess = DateTime.UtcNow;
			this.SetUpCachingValues(response);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x001A48CC File Offset: 0x001A2ACC
		internal Stream GetSaveStream(HTTPResponse response)
		{
			if (!HTTPCacheService.IsSupported)
			{
				return null;
			}
			this.LastAccess = DateTime.UtcNow;
			string path = this.GetPath();
			if (HTTPManager.IOService.FileExists(path))
			{
				this.Delete();
			}
			if (path.Length > HTTPManager.MaxPathLength)
			{
				return null;
			}
			using (Stream stream = HTTPManager.IOService.CreateFileStream(this.GetPath(), FileStreamModes.Create))
			{
				stream.WriteLine("HTTP/1.1 {0} {1}", new object[]
				{
					response.StatusCode,
					response.Message
				});
				foreach (KeyValuePair<string, List<string>> keyValuePair in response.Headers)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						stream.WriteLine("{0}: {1}", new object[]
						{
							keyValuePair.Key,
							keyValuePair.Value[i]
						});
					}
				}
				stream.WriteLine();
			}
			if (response.IsFromCache && !response.Headers.ContainsKey("content-length"))
			{
				response.Headers.Add("content-length", new List<string>
				{
					this.BodyLength.ToString()
				});
			}
			this.SetUpCachingValues(response);
			return HTTPManager.IOService.CreateFileStream(this.GetPath(), FileStreamModes.Append);
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x001A4A50 File Offset: 0x001A2C50
		public int CompareTo(HTTPCacheFileInfo other)
		{
			return this.LastAccess.CompareTo(other.LastAccess);
		}
	}
}
