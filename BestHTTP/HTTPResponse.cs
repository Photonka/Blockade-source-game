using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Decompression.Zlib;
using BestHTTP.Extensions;
using BestHTTP.Logger;
using UnityEngine;

namespace BestHTTP
{
	// Token: 0x02000181 RID: 385
	public class HTTPResponse : IDisposable
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00096CAB File Offset: 0x00094EAB
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00096CB3 File Offset: 0x00094EB3
		public int VersionMajor { get; protected set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00096CBC File Offset: 0x00094EBC
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00096CC4 File Offset: 0x00094EC4
		public int VersionMinor { get; protected set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00096CCD File Offset: 0x00094ECD
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00096CD5 File Offset: 0x00094ED5
		public int StatusCode { get; protected set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00096CDE File Offset: 0x00094EDE
		public bool IsSuccess
		{
			get
			{
				return (this.StatusCode >= 200 && this.StatusCode < 300) || this.StatusCode == 304;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00096D09 File Offset: 0x00094F09
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00096D11 File Offset: 0x00094F11
		public string Message { get; protected set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00096D1A File Offset: 0x00094F1A
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00096D22 File Offset: 0x00094F22
		public bool IsStreamed { get; protected set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00096D2B File Offset: 0x00094F2B
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00096D33 File Offset: 0x00094F33
		public bool IsStreamingFinished { get; internal set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00096D3C File Offset: 0x00094F3C
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x00096D44 File Offset: 0x00094F44
		public bool IsFromCache { get; internal set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00096D4D File Offset: 0x00094F4D
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00096D55 File Offset: 0x00094F55
		public HTTPCacheFileInfo CacheFileInfo { get; internal set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00096D5E File Offset: 0x00094F5E
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00096D66 File Offset: 0x00094F66
		public bool IsCacheOnly { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00096D6F File Offset: 0x00094F6F
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00096D77 File Offset: 0x00094F77
		public Dictionary<string, List<string>> Headers { get; protected set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00096D80 File Offset: 0x00094F80
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00096D88 File Offset: 0x00094F88
		public byte[] Data { get; internal set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00096D91 File Offset: 0x00094F91
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00096D99 File Offset: 0x00094F99
		public bool IsUpgraded { get; protected set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00096DA2 File Offset: 0x00094FA2
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00096DAA File Offset: 0x00094FAA
		public List<Cookie> Cookies { get; internal set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00096DB4 File Offset: 0x00094FB4
		public string DataAsText
		{
			get
			{
				if (this.Data == null)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(this.dataAsText))
				{
					return this.dataAsText;
				}
				return this.dataAsText = Encoding.UTF8.GetString(this.Data, 0, this.Data.Length);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00096E08 File Offset: 0x00095008
		public Texture2D DataAsTexture2D
		{
			get
			{
				if (this.Data == null)
				{
					return null;
				}
				if (this.texture != null)
				{
					return this.texture;
				}
				this.texture = new Texture2D(0, 0, 5, false);
				ImageConversion.LoadImage(this.texture, this.Data);
				return this.texture;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00096E5B File Offset: 0x0009505B
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00096E63 File Offset: 0x00095063
		public bool IsClosedManually { get; protected set; }

		// Token: 0x06000E4A RID: 3658 RVA: 0x00096E6C File Offset: 0x0009506C
		public HTTPResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
		{
			this.baseRequest = request;
			this.Stream = stream;
			this.IsStreamed = isStreamed;
			this.IsFromCache = isFromCache;
			this.IsCacheOnly = request.CacheOnly;
			this.IsClosedManually = false;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00096EBC File Offset: 0x000950BC
		public virtual bool Receive(int forceReadRawContentLength = -1, bool readPayloadData = true)
		{
			string text = string.Empty;
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging(string.Format("Receive. forceReadRawContentLength: '{0:N0}', readPayloadData: '{1:N0}'", forceReadRawContentLength, readPayloadData));
			}
			try
			{
				text = HTTPResponse.ReadTo(this.Stream, 32);
			}
			catch
			{
				if (!this.baseRequest.DisableRetry)
				{
					HTTPManager.Logger.Warning("HTTPResponse", string.Format("{0} - Failed to read Status Line! Retry is enabled, returning with false.", this.baseRequest.CurrentUri.ToString()));
					return false;
				}
				HTTPManager.Logger.Warning("HTTPResponse", string.Format("{0} - Failed to read Status Line! Retry is disabled, re-throwing exception.", this.baseRequest.CurrentUri.ToString()));
				throw;
			}
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging(string.Format("Status Line: '{0}'", text));
			}
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(new char[]
				{
					'/',
					'.'
				});
				this.VersionMajor = int.Parse(array[1]);
				this.VersionMinor = int.Parse(array[2]);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("HTTP Version: '{0}.{1}'", this.VersionMajor.ToString(), this.VersionMinor.ToString()));
				}
				string text2 = HTTPResponse.NoTrimReadTo(this.Stream, 32, 10);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("Status Code: '{0}'", text2));
				}
				int statusCode;
				if (this.baseRequest.DisableRetry)
				{
					statusCode = int.Parse(text2);
				}
				else if (!int.TryParse(text2, out statusCode))
				{
					return false;
				}
				this.StatusCode = statusCode;
				if (text2.Length > 0 && (byte)text2[text2.Length - 1] != 10 && (byte)text2[text2.Length - 1] != 13)
				{
					this.Message = HTTPResponse.ReadTo(this.Stream, 10);
					if (HTTPManager.Logger.Level == Loglevels.All)
					{
						this.VerboseLogging(string.Format("Status Message: '{0}'", this.Message));
					}
				}
				else
				{
					HTTPManager.Logger.Warning("HTTPResponse", string.Format("{0} - Skipping Status Message reading!", this.baseRequest.CurrentUri.ToString()));
					this.Message = string.Empty;
				}
				this.ReadHeaders(this.Stream);
				this.IsUpgraded = (this.StatusCode == 101 && (this.HasHeaderWithValue("connection", "upgrade") || this.HasHeader("upgrade")));
				if (this.IsUpgraded && HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging("Request Upgraded!");
				}
				return !readPayloadData || this.ReadPayload(forceReadRawContentLength);
			}
			if (!this.baseRequest.DisableRetry)
			{
				return false;
			}
			throw new Exception("Remote server closed the connection before sending response header!");
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0009718C File Offset: 0x0009538C
		protected bool ReadPayload(int forceReadRawContentLength)
		{
			if (forceReadRawContentLength != -1)
			{
				this.IsFromCache = true;
				this.ReadRaw(this.Stream, (long)forceReadRawContentLength);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging("ReadPayload Finished!");
				}
				return true;
			}
			if ((this.StatusCode >= 100 && this.StatusCode < 200) || this.StatusCode == 204 || this.StatusCode == 304 || this.baseRequest.MethodType == HTTPMethods.Head)
			{
				return true;
			}
			if (this.HasHeaderWithValue("transfer-encoding", "chunked"))
			{
				this.ReadChunked(this.Stream);
			}
			else
			{
				List<string> headerValues = this.GetHeaderValues("content-length");
				List<string> headerValues2 = this.GetHeaderValues("content-range");
				if (headerValues != null && headerValues2 == null)
				{
					this.ReadRaw(this.Stream, long.Parse(headerValues[0]));
				}
				else if (headerValues2 != null)
				{
					if (headerValues != null)
					{
						this.ReadRaw(this.Stream, long.Parse(headerValues[0]));
					}
					else
					{
						HTTPRange range = this.GetRange();
						this.ReadRaw(this.Stream, (long)(range.LastBytePos - range.FirstBytePos + 1));
					}
				}
				else
				{
					this.ReadUnknownSize(this.Stream);
				}
			}
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging("ReadPayload Finished!");
			}
			return true;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000972D0 File Offset: 0x000954D0
		protected void ReadHeaders(Stream stream)
		{
			string text = HTTPResponse.ReadTo(stream, 58, 10);
			while (text != string.Empty)
			{
				string text2 = HTTPResponse.ReadTo(stream, 10);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("Header - '{0}': '{1}'", text, text2));
				}
				this.AddHeader(text, text2);
				text = HTTPResponse.ReadTo(stream, 58, 10);
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00097334 File Offset: 0x00095534
		protected void AddHeader(string name, string value)
		{
			name = name.ToLower();
			if (this.Headers == null)
			{
				this.Headers = new Dictionary<string, List<string>>();
			}
			List<string> list;
			if (!this.Headers.TryGetValue(name, out list))
			{
				this.Headers.Add(name, list = new List<string>(1));
			}
			list.Add(value);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00097388 File Offset: 0x00095588
		public List<string> GetHeaderValues(string name)
		{
			if (this.Headers == null)
			{
				return null;
			}
			name = name.ToLower();
			List<string> list;
			if (!this.Headers.TryGetValue(name, out list) || list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000973C4 File Offset: 0x000955C4
		public string GetFirstHeaderValue(string name)
		{
			if (this.Headers == null)
			{
				return null;
			}
			name = name.ToLower();
			List<string> list;
			if (!this.Headers.TryGetValue(name, out list) || list.Count == 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00097404 File Offset: 0x00095604
		public bool HasHeaderWithValue(string headerName, string value)
		{
			List<string> headerValues = this.GetHeaderValues(headerName);
			if (headerValues == null)
			{
				return false;
			}
			for (int i = 0; i < headerValues.Count; i++)
			{
				if (string.Compare(headerValues[i], value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00097442 File Offset: 0x00095642
		public bool HasHeader(string headerName)
		{
			return this.GetHeaderValues(headerName) != null;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00097450 File Offset: 0x00095650
		public HTTPRange GetRange()
		{
			List<string> headerValues = this.GetHeaderValues("content-range");
			if (headerValues == null)
			{
				return null;
			}
			string[] array = headerValues[0].Split(new char[]
			{
				' ',
				'-',
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array[1] == "*")
			{
				return new HTTPRange(int.Parse(array[2]));
			}
			return new HTTPRange(int.Parse(array[1]), int.Parse(array[2]), (array[3] != "*") ? int.Parse(array[3]) : -1);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000974DC File Offset: 0x000956DC
		public static string ReadTo(Stream stream, byte blocker)
		{
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			string @string;
			try
			{
				int num = 0;
				for (int num2 = stream.ReadByte(); num2 != (int)blocker; num2 = stream.ReadByte())
				{
					if (num2 == -1)
					{
						break;
					}
					if (num2 > 127)
					{
						num2 = 63;
					}
					if (array.Length <= num)
					{
						VariableSizedBufferPool.Resize(ref array, array.Length * 2, true);
					}
					if (num > 0 || !char.IsWhiteSpace((char)num2))
					{
						array[num++] = (byte)num2;
					}
				}
				while (num > 0 && char.IsWhiteSpace((char)array[num - 1]))
				{
					num--;
				}
				@string = Encoding.UTF8.GetString(array, 0, num);
			}
			finally
			{
				VariableSizedBufferPool.Release(array);
			}
			return @string;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00097580 File Offset: 0x00095780
		public static string ReadTo(Stream stream, byte blocker1, byte blocker2)
		{
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			string @string;
			try
			{
				int num = 0;
				int num2 = stream.ReadByte();
				while (num2 != (int)blocker1 && num2 != (int)blocker2)
				{
					if (num2 == -1)
					{
						break;
					}
					if (num2 > 127)
					{
						num2 = 63;
					}
					if (array.Length <= num)
					{
						VariableSizedBufferPool.Resize(ref array, array.Length * 2, true);
					}
					if (num > 0 || !char.IsWhiteSpace((char)num2))
					{
						array[num++] = (byte)num2;
					}
					num2 = stream.ReadByte();
				}
				while (num > 0 && char.IsWhiteSpace((char)array[num - 1]))
				{
					num--;
				}
				@string = Encoding.UTF8.GetString(array, 0, num);
			}
			finally
			{
				VariableSizedBufferPool.Release(array);
			}
			return @string;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00097628 File Offset: 0x00095828
		public static string NoTrimReadTo(Stream stream, byte blocker1, byte blocker2)
		{
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			string @string;
			try
			{
				int num = 0;
				int num2 = stream.ReadByte();
				while (num2 != (int)blocker1 && num2 != (int)blocker2 && num2 != -1)
				{
					if (num2 > 127)
					{
						num2 = 63;
					}
					if (array.Length <= num)
					{
						VariableSizedBufferPool.Resize(ref array, array.Length * 2, true);
					}
					if (num > 0 || !char.IsWhiteSpace((char)num2))
					{
						array[num++] = (byte)num2;
					}
					num2 = stream.ReadByte();
				}
				@string = Encoding.UTF8.GetString(array, 0, num);
			}
			finally
			{
				VariableSizedBufferPool.Release(array);
			}
			return @string;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000976BC File Offset: 0x000958BC
		protected int ReadChunkLength(Stream stream)
		{
			string text = HTTPResponse.ReadTo(stream, 10).Split(new char[]
			{
				';'
			})[0];
			int result;
			if (int.TryParse(text, NumberStyles.AllowHexSpecifier, null, out result))
			{
				return result;
			}
			throw new Exception(string.Format("Can't parse '{0}' as a hex number!", text));
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00097708 File Offset: 0x00095908
		protected void ReadChunked(Stream stream)
		{
			this.BeginReceiveStreamFragments();
			string firstHeaderValue = this.GetFirstHeaderValue("Content-Length");
			bool flag = !string.IsNullOrEmpty(firstHeaderValue);
			int num = 0;
			if (flag)
			{
				flag = int.TryParse(firstHeaderValue, out num);
			}
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging(string.Format("ReadChunked - hasContentLengthHeader: {0}, contentLengthHeader: {1} realLength: {2:N0}", flag.ToString(), firstHeaderValue, num));
			}
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream())
			{
				int num2 = this.ReadChunkLength(stream);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("chunkLength: {0:N0}", num2));
				}
				byte[] array = VariableSizedBufferPool.Get((long)Mathf.NextPowerOfTwo(num2), true);
				int num3 = 0;
				this.baseRequest.DownloadLength = (long)(flag ? num : num2);
				this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
				string text = this.IsFromCache ? null : this.GetFirstHeaderValue("content-encoding");
				bool flag2 = !string.IsNullOrEmpty(text) && text == "gzip";
				while (num2 != 0)
				{
					if (array.Length < num2)
					{
						VariableSizedBufferPool.Resize(ref array, num2, true);
					}
					int num4 = 0;
					do
					{
						int num5 = stream.Read(array, num4, num2 - num4);
						if (num5 <= 0)
						{
							goto Block_11;
						}
						num4 += num5;
						this.baseRequest.Downloaded += (long)num5;
						this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
					}
					while (num4 < num2);
					if (this.baseRequest.UseStreaming)
					{
						if (flag2)
						{
							byte[] array2 = this.Decompress(array, 0, num4, false);
							if (array2 != null)
							{
								this.FeedStreamFragment(array2, 0, array2.Length);
							}
						}
						else
						{
							this.FeedStreamFragment(array, 0, num4);
						}
					}
					else
					{
						bufferPoolMemoryStream.Write(array, 0, num4);
					}
					HTTPResponse.ReadTo(stream, 10);
					num3 += num4;
					num2 = this.ReadChunkLength(stream);
					if (HTTPManager.Logger.Level == Loglevels.All)
					{
						this.VerboseLogging(string.Format("chunkLength: {0:N0}", num2));
					}
					if (!flag)
					{
						this.baseRequest.DownloadLength += (long)num2;
					}
					this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
					continue;
					Block_11:
					throw ExceptionHelper.ServerClosedTCPStream();
				}
				VariableSizedBufferPool.Release(array);
				if (this.baseRequest.UseStreaming)
				{
					if (flag2)
					{
						byte[] array3 = this.Decompress(null, 0, 0, true);
						if (array3 != null)
						{
							this.FeedStreamFragment(array3, 0, array3.Length);
						}
					}
					this.FlushRemainingFragmentBuffer();
				}
				this.ReadHeaders(stream);
				if (!this.baseRequest.UseStreaming)
				{
					this.Data = this.DecodeStream(bufferPoolMemoryStream);
				}
			}
			this.CloseDecompressors();
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000979D0 File Offset: 0x00095BD0
		internal void ReadRaw(Stream stream, long contentLength)
		{
			this.BeginReceiveStreamFragments();
			this.baseRequest.DownloadLength = contentLength;
			this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging(string.Format("ReadRaw - contentLength: {0:N0}", contentLength));
			}
			string text = this.IsFromCache ? null : this.GetFirstHeaderValue("content-encoding");
			bool flag = !string.IsNullOrEmpty(text) && text == "gzip";
			if (!this.baseRequest.UseStreaming && contentLength > 2147483646L)
			{
				throw new OverflowException("You have to use STREAMING to download files bigger than 2GB!");
			}
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream(this.baseRequest.UseStreaming ? 0 : ((int)contentLength)))
			{
				byte[] array = VariableSizedBufferPool.Get((long)Math.Max(this.baseRequest.StreamFragmentSize, 4096), true);
				while (contentLength > 0L)
				{
					int num = 0;
					do
					{
						int val = (int)Math.Min(2147483646U, (uint)contentLength);
						int num2 = stream.Read(array, num, Math.Min(val, array.Length - num));
						if (num2 <= 0)
						{
							goto Block_10;
						}
						num += num2;
						contentLength -= (long)num2;
						this.baseRequest.Downloaded += (long)num2;
						this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
					}
					while (num < array.Length && contentLength > 0L);
					if (!this.baseRequest.UseStreaming)
					{
						bufferPoolMemoryStream.Write(array, 0, num);
						continue;
					}
					if (!flag)
					{
						this.FeedStreamFragment(array, 0, num);
						continue;
					}
					byte[] array2 = this.Decompress(array, 0, num, false);
					if (array2 != null)
					{
						this.FeedStreamFragment(array2, 0, array2.Length);
						continue;
					}
					continue;
					Block_10:
					throw ExceptionHelper.ServerClosedTCPStream();
				}
				VariableSizedBufferPool.Release(array);
				if (this.baseRequest.UseStreaming)
				{
					if (flag)
					{
						byte[] array3 = this.Decompress(null, 0, 0, true);
						if (array3 != null)
						{
							this.FeedStreamFragment(array3, 0, array3.Length);
						}
					}
					this.FlushRemainingFragmentBuffer();
				}
				if (!this.baseRequest.UseStreaming)
				{
					this.Data = this.DecodeStream(bufferPoolMemoryStream);
				}
			}
			this.CloseDecompressors();
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00097C08 File Offset: 0x00095E08
		protected void ReadUnknownSize(Stream stream)
		{
			string text = this.IsFromCache ? null : this.GetFirstHeaderValue("content-encoding");
			bool flag = !string.IsNullOrEmpty(text) && text == "gzip";
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream())
			{
				byte[] array = VariableSizedBufferPool.Get((long)Math.Max(this.baseRequest.StreamFragmentSize, 4096), false);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("ReadUnknownSize - buffer size: {0:N0}", array.Length));
				}
				int num2;
				do
				{
					int num = 0;
					do
					{
						num2 = 0;
						NetworkStream networkStream = stream as NetworkStream;
						if (networkStream != null && this.baseRequest.EnableSafeReadOnUnknownContentLength)
						{
							for (int i = num; i < array.Length; i++)
							{
								if (!networkStream.DataAvailable)
								{
									break;
								}
								int num3 = stream.ReadByte();
								if (num3 < 0)
								{
									break;
								}
								array[i] = (byte)num3;
								num2++;
							}
						}
						else
						{
							num2 = stream.Read(array, num, array.Length - num);
						}
						num += num2;
						this.baseRequest.Downloaded += (long)num2;
						this.baseRequest.DownloadLength = this.baseRequest.Downloaded;
						this.baseRequest.DownloadProgressChanged = (this.IsSuccess || this.IsFromCache);
					}
					while (num < array.Length && num2 > 0);
					if (this.baseRequest.UseStreaming)
					{
						if (flag)
						{
							byte[] array2 = this.Decompress(array, 0, num, false);
							if (array2 != null)
							{
								this.FeedStreamFragment(array2, 0, array2.Length);
							}
						}
						else
						{
							this.FeedStreamFragment(array, 0, num);
						}
					}
					else
					{
						bufferPoolMemoryStream.Write(array, 0, num);
					}
				}
				while (num2 > 0);
				VariableSizedBufferPool.Release(array);
				if (this.baseRequest.UseStreaming)
				{
					if (flag)
					{
						byte[] array3 = this.Decompress(null, 0, 0, true);
						if (array3 != null)
						{
							this.FeedStreamFragment(array3, 0, array3.Length);
						}
					}
					this.FlushRemainingFragmentBuffer();
				}
				if (!this.baseRequest.UseStreaming)
				{
					this.Data = this.DecodeStream(bufferPoolMemoryStream);
				}
			}
			this.CloseDecompressors();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00097E24 File Offset: 0x00096024
		protected byte[] DecodeStream(BufferPoolMemoryStream streamToDecode)
		{
			streamToDecode.Seek(0L, SeekOrigin.Begin);
			List<string> list = this.IsFromCache ? null : this.GetHeaderValues("content-encoding");
			if (list == null)
			{
				return streamToDecode.ToArray();
			}
			string a = list[0];
			Stream stream;
			if (!(a == "gzip"))
			{
				if (!(a == "deflate"))
				{
					return streamToDecode.ToArray();
				}
				stream = new DeflateStream(streamToDecode, CompressionMode.Decompress);
			}
			else
			{
				stream = new GZipStream(streamToDecode, CompressionMode.Decompress);
			}
			byte[] result;
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream((int)streamToDecode.Length))
			{
				byte[] array = VariableSizedBufferPool.Get(1024L, true);
				int count;
				while ((count = stream.Read(array, 0, array.Length)) > 0)
				{
					bufferPoolMemoryStream.Write(array, 0, count);
				}
				VariableSizedBufferPool.Release(array);
				stream.Dispose();
				result = bufferPoolMemoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00097F10 File Offset: 0x00096110
		private void CloseDecompressors()
		{
			if (this.decompressorGZipStream != null)
			{
				this.decompressorGZipStream.Dispose();
			}
			this.decompressorGZipStream = null;
			if (this.decompressorInputStream != null)
			{
				this.decompressorInputStream.Dispose();
			}
			this.decompressorInputStream = null;
			if (this.decompressorOutputStream != null)
			{
				this.decompressorOutputStream.Dispose();
			}
			this.decompressorOutputStream = null;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00097F6C File Offset: 0x0009616C
		private byte[] Decompress(byte[] data, int offset, int count, bool forceDecompress = false)
		{
			if (this.decompressorInputStream == null)
			{
				this.decompressorInputStream = new BufferPoolMemoryStream(count);
			}
			if (data != null)
			{
				this.decompressorInputStream.Write(data, offset, count);
			}
			if (!forceDecompress && this.decompressorInputStream.Length < 256L)
			{
				return null;
			}
			this.decompressorInputStream.Position = 0L;
			if (this.decompressorGZipStream == null)
			{
				this.decompressorGZipStream = new GZipStream(this.decompressorInputStream, CompressionMode.Decompress, CompressionLevel.Default, true);
				this.decompressorGZipStream.FlushMode = FlushType.Sync;
			}
			if (this.decompressorOutputStream == null)
			{
				this.decompressorOutputStream = new BufferPoolMemoryStream();
			}
			this.decompressorOutputStream.SetLength(0L);
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			int count2;
			while ((count2 = this.decompressorGZipStream.Read(array, 0, array.Length)) != 0)
			{
				this.decompressorOutputStream.Write(array, 0, count2);
			}
			VariableSizedBufferPool.Release(array);
			this.decompressorGZipStream.SetLength(0L);
			return this.decompressorOutputStream.ToArray();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0009805C File Offset: 0x0009625C
		protected void BeginReceiveStreamFragments()
		{
			if (!this.baseRequest.DisableCache && this.baseRequest.UseStreaming && !this.IsFromCache && HTTPCacheService.IsCacheble(this.baseRequest.CurrentUri, this.baseRequest.MethodType, this))
			{
				this.cacheStream = HTTPCacheService.PrepareStreamed(this.baseRequest.CurrentUri, this);
			}
			this.allFragmentSize = 0;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000980C8 File Offset: 0x000962C8
		protected void FeedStreamFragment(byte[] buffer, int pos, int length)
		{
			if (buffer == null || length == 0)
			{
				return;
			}
			this.WaitWhileFragmentQueueIsFull();
			if (this.fragmentBuffer == null)
			{
				this.fragmentBuffer = VariableSizedBufferPool.Get((long)this.baseRequest.StreamFragmentSize, false);
				this.fragmentBufferDataLength = 0;
			}
			if (this.fragmentBufferDataLength + length <= this.baseRequest.StreamFragmentSize)
			{
				Array.Copy(buffer, pos, this.fragmentBuffer, this.fragmentBufferDataLength, length);
				this.fragmentBufferDataLength += length;
				if (this.fragmentBufferDataLength == this.baseRequest.StreamFragmentSize)
				{
					this.AddStreamedFragment(this.fragmentBuffer);
					this.fragmentBuffer = null;
					this.fragmentBufferDataLength = 0;
					return;
				}
			}
			else
			{
				int num = this.baseRequest.StreamFragmentSize - this.fragmentBufferDataLength;
				this.FeedStreamFragment(buffer, pos, num);
				this.FeedStreamFragment(buffer, pos + num, length - num);
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00098198 File Offset: 0x00096398
		protected void FlushRemainingFragmentBuffer()
		{
			if (this.fragmentBuffer != null)
			{
				VariableSizedBufferPool.Resize(ref this.fragmentBuffer, this.fragmentBufferDataLength, false);
				this.AddStreamedFragment(this.fragmentBuffer);
				this.fragmentBuffer = null;
				this.fragmentBufferDataLength = 0;
			}
			if (this.cacheStream != null)
			{
				this.cacheStream.Dispose();
				this.cacheStream = null;
				HTTPCacheService.SetBodyLength(this.baseRequest.CurrentUri, this.allFragmentSize);
			}
			AutoResetEvent autoResetEvent = this.fragmentWaitEvent;
			this.fragmentWaitEvent = null;
			if (autoResetEvent != null)
			{
				((IDisposable)autoResetEvent).Dispose();
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00098228 File Offset: 0x00096428
		protected void AddStreamedFragment(byte[] buffer)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsCacheOnly)
				{
					if (this.streamedFragments == null)
					{
						this.streamedFragments = new List<byte[]>();
					}
					this.streamedFragments.Add(buffer);
				}
				if (HTTPManager.Logger.Level == Loglevels.All && buffer != null && this.streamedFragments != null)
				{
					this.VerboseLogging(string.Format("AddStreamedFragment buffer length: {0:N0} streamedFragments: {1:N0}", buffer.Length, this.streamedFragments.Count));
				}
				if (this.cacheStream != null)
				{
					this.cacheStream.Write(buffer, 0, buffer.Length);
					this.allFragmentSize += buffer.Length;
				}
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000982F0 File Offset: 0x000964F0
		protected void WaitWhileFragmentQueueIsFull()
		{
			while (this.baseRequest.UseStreaming && this.FragmentQueueIsFull())
			{
				this.VerboseLogging("WaitWhileFragmentQueueIsFull");
				if (this.fragmentWaitEvent == null)
				{
					this.fragmentWaitEvent = new AutoResetEvent(false);
				}
				this.fragmentWaitEvent.WaitOne(16);
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00098348 File Offset: 0x00096548
		private bool FragmentQueueIsFull()
		{
			object syncRoot = this.SyncRoot;
			bool result;
			lock (syncRoot)
			{
				bool flag2 = this.streamedFragments != null && this.streamedFragments.Count >= this.baseRequest.MaxFragmentQueueLength;
				if (flag2 && HTTPManager.Logger.Level == Loglevels.All)
				{
					this.VerboseLogging(string.Format("HasFragmentsInQueue - {0} / {1}", this.streamedFragments.Count, this.baseRequest.MaxFragmentQueueLength));
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000983EC File Offset: 0x000965EC
		public List<byte[]> GetStreamedFragments()
		{
			object syncRoot = this.SyncRoot;
			List<byte[]> result;
			lock (syncRoot)
			{
				if (this.streamedFragments == null || this.streamedFragments.Count == 0)
				{
					if (HTTPManager.Logger.Level == Loglevels.All)
					{
						this.VerboseLogging("GetStreamedFragments - no fragments, returning with null");
					}
					result = null;
				}
				else
				{
					List<byte[]> list = new List<byte[]>(this.streamedFragments);
					this.streamedFragments.Clear();
					if (this.fragmentWaitEvent != null)
					{
						this.fragmentWaitEvent.Set();
					}
					if (HTTPManager.Logger.Level == Loglevels.All)
					{
						this.VerboseLogging(string.Format("GetStreamedFragments - returning with {0:N0} fragments", list.Count.ToString()));
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000984B4 File Offset: 0x000966B4
		internal bool HasStreamedFragments()
		{
			object syncRoot = this.SyncRoot;
			bool result;
			lock (syncRoot)
			{
				result = (this.streamedFragments != null && this.streamedFragments.Count >= this.baseRequest.MaxFragmentQueueLength);
			}
			return result;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00098518 File Offset: 0x00096718
		internal void FinishStreaming()
		{
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				this.VerboseLogging("FinishStreaming");
			}
			this.IsStreamingFinished = true;
			this.Dispose();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0009853E File Offset: 0x0009673E
		private void VerboseLogging(string str)
		{
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("HTTPResponse", "'" + this.baseRequest.CurrentUri.ToString() + "' - " + str);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0009857C File Offset: 0x0009677C
		public void Dispose()
		{
			if (this.Stream != null && this.Stream is ReadOnlyBufferedStream)
			{
				((IDisposable)this.Stream).Dispose();
			}
			this.Stream = null;
			if (this.cacheStream != null)
			{
				this.cacheStream.Dispose();
				this.cacheStream = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001210 RID: 4624
		internal const byte CR = 13;

		// Token: 0x04001211 RID: 4625
		internal const byte LF = 10;

		// Token: 0x04001212 RID: 4626
		public const int MinBufferSize = 4096;

		// Token: 0x04001220 RID: 4640
		protected string dataAsText;

		// Token: 0x04001221 RID: 4641
		protected Texture2D texture;

		// Token: 0x04001223 RID: 4643
		internal HTTPRequest baseRequest;

		// Token: 0x04001224 RID: 4644
		protected Stream Stream;

		// Token: 0x04001225 RID: 4645
		protected List<byte[]> streamedFragments;

		// Token: 0x04001226 RID: 4646
		protected object SyncRoot = new object();

		// Token: 0x04001227 RID: 4647
		protected byte[] fragmentBuffer;

		// Token: 0x04001228 RID: 4648
		protected int fragmentBufferDataLength;

		// Token: 0x04001229 RID: 4649
		protected Stream cacheStream;

		// Token: 0x0400122A RID: 4650
		protected int allFragmentSize;

		// Token: 0x0400122B RID: 4651
		private BufferPoolMemoryStream decompressorInputStream;

		// Token: 0x0400122C RID: 4652
		private BufferPoolMemoryStream decompressorOutputStream;

		// Token: 0x0400122D RID: 4653
		private GZipStream decompressorGZipStream;

		// Token: 0x0400122E RID: 4654
		private const int MinLengthToDecompress = 256;

		// Token: 0x0400122F RID: 4655
		private volatile AutoResetEvent fragmentWaitEvent;
	}
}
