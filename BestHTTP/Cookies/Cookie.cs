using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP.Extensions;

namespace BestHTTP.Cookies
{
	// Token: 0x020007FB RID: 2043
	public sealed class Cookie : IComparable<Cookie>, IEquatable<Cookie>
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x001A2FB6 File Offset: 0x001A11B6
		// (set) Token: 0x060048EC RID: 18668 RVA: 0x001A2FBE File Offset: 0x001A11BE
		public string Name { get; private set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x001A2FC7 File Offset: 0x001A11C7
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x001A2FCF File Offset: 0x001A11CF
		public string Value { get; private set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x001A2FD8 File Offset: 0x001A11D8
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x001A2FE0 File Offset: 0x001A11E0
		public DateTime Date { get; internal set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x001A2FE9 File Offset: 0x001A11E9
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x001A2FF1 File Offset: 0x001A11F1
		public DateTime LastAccess { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x001A2FFA File Offset: 0x001A11FA
		// (set) Token: 0x060048F4 RID: 18676 RVA: 0x001A3002 File Offset: 0x001A1202
		public DateTime Expires { get; private set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x001A300B File Offset: 0x001A120B
		// (set) Token: 0x060048F6 RID: 18678 RVA: 0x001A3013 File Offset: 0x001A1213
		public long MaxAge { get; private set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x001A301C File Offset: 0x001A121C
		// (set) Token: 0x060048F8 RID: 18680 RVA: 0x001A3024 File Offset: 0x001A1224
		public bool IsSession { get; private set; }

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x001A302D File Offset: 0x001A122D
		// (set) Token: 0x060048FA RID: 18682 RVA: 0x001A3035 File Offset: 0x001A1235
		public string Domain { get; private set; }

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060048FB RID: 18683 RVA: 0x001A303E File Offset: 0x001A123E
		// (set) Token: 0x060048FC RID: 18684 RVA: 0x001A3046 File Offset: 0x001A1246
		public string Path { get; private set; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x001A304F File Offset: 0x001A124F
		// (set) Token: 0x060048FE RID: 18686 RVA: 0x001A3057 File Offset: 0x001A1257
		public bool IsSecure { get; private set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x001A3060 File Offset: 0x001A1260
		// (set) Token: 0x06004900 RID: 18688 RVA: 0x001A3068 File Offset: 0x001A1268
		public bool IsHttpOnly { get; private set; }

		// Token: 0x06004901 RID: 18689 RVA: 0x001A3071 File Offset: 0x001A1271
		public Cookie(string name, string value) : this(name, value, "/", string.Empty)
		{
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x001A3085 File Offset: 0x001A1285
		public Cookie(string name, string value, string path) : this(name, value, path, string.Empty)
		{
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x001A3095 File Offset: 0x001A1295
		public Cookie(string name, string value, string path, string domain) : this()
		{
			this.Name = name;
			this.Value = value;
			this.Path = path;
			this.Domain = domain;
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x001A30BA File Offset: 0x001A12BA
		public Cookie(Uri uri, string name, string value, DateTime expires, bool isSession = true) : this(name, value, uri.AbsolutePath, uri.Host)
		{
			this.Expires = expires;
			this.IsSession = isSession;
			this.Date = DateTime.UtcNow;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x001A30EB File Offset: 0x001A12EB
		public Cookie(Uri uri, string name, string value, long maxAge = -1L, bool isSession = true) : this(name, value, uri.AbsolutePath, uri.Host)
		{
			this.MaxAge = maxAge;
			this.IsSession = isSession;
			this.Date = DateTime.UtcNow;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x001A311C File Offset: 0x001A131C
		internal Cookie()
		{
			this.IsSession = true;
			this.MaxAge = -1L;
			this.LastAccess = DateTime.UtcNow;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x001A3140 File Offset: 0x001A1340
		public bool WillExpireInTheFuture()
		{
			if (this.IsSession)
			{
				return true;
			}
			if (this.MaxAge == -1L)
			{
				return this.Expires > DateTime.UtcNow;
			}
			return Math.Max(0L, (long)(DateTime.UtcNow - this.Date).TotalSeconds) < this.MaxAge;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x001A319C File Offset: 0x001A139C
		public uint GuessSize()
		{
			return (uint)(((this.Name != null) ? (this.Name.Length * 2) : 0) + ((this.Value != null) ? (this.Value.Length * 2) : 0) + ((this.Domain != null) ? (this.Domain.Length * 2) : 0) + ((this.Path != null) ? (this.Path.Length * 2) : 0) + 32 + 3);
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x001A3214 File Offset: 0x001A1414
		public static Cookie Parse(string header, Uri defaultDomain)
		{
			Cookie cookie = new Cookie();
			try
			{
				foreach (HeaderValue headerValue in Cookie.ParseCookieHeader(header))
				{
					string a = headerValue.Key.ToLowerInvariant();
					if (!(a == "path"))
					{
						if (!(a == "domain"))
						{
							if (!(a == "expires"))
							{
								if (!(a == "max-age"))
								{
									if (!(a == "secure"))
									{
										if (!(a == "httponly"))
										{
											cookie.Name = headerValue.Key;
											cookie.Value = headerValue.Value;
										}
										else
										{
											cookie.IsHttpOnly = true;
										}
									}
									else
									{
										cookie.IsSecure = true;
									}
								}
								else
								{
									cookie.MaxAge = headerValue.Value.ToInt64(-1L);
									cookie.IsSession = false;
								}
							}
							else
							{
								cookie.Expires = headerValue.Value.ToDateTime(DateTime.FromBinary(0L));
								cookie.IsSession = false;
							}
						}
						else
						{
							if (string.IsNullOrEmpty(headerValue.Value))
							{
								return null;
							}
							cookie.Domain = (headerValue.Value.StartsWith(".") ? headerValue.Value.Substring(1) : headerValue.Value);
						}
					}
					else
					{
						cookie.Path = ((string.IsNullOrEmpty(headerValue.Value) || !headerValue.Value.StartsWith("/")) ? "/" : (cookie.Path = headerValue.Value));
					}
				}
				if (HTTPManager.EnablePrivateBrowsing)
				{
					cookie.IsSession = true;
				}
				if (string.IsNullOrEmpty(cookie.Domain))
				{
					cookie.Domain = defaultDomain.Host;
				}
				if (string.IsNullOrEmpty(cookie.Path))
				{
					cookie.Path = defaultDomain.AbsolutePath;
				}
				cookie.Date = (cookie.LastAccess = DateTime.UtcNow);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Warning("Cookie", string.Concat(new string[]
				{
					"Parse - Couldn't parse header: ",
					header,
					" exception: ",
					ex.ToString(),
					" ",
					ex.StackTrace
				}));
			}
			return cookie;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x001A3494 File Offset: 0x001A1694
		internal void SaveTo(BinaryWriter stream)
		{
			stream.Write(1);
			stream.Write(this.Name ?? string.Empty);
			stream.Write(this.Value ?? string.Empty);
			stream.Write(this.Date.ToBinary());
			stream.Write(this.LastAccess.ToBinary());
			stream.Write(this.Expires.ToBinary());
			stream.Write(this.MaxAge);
			stream.Write(this.IsSession);
			stream.Write(this.Domain ?? string.Empty);
			stream.Write(this.Path ?? string.Empty);
			stream.Write(this.IsSecure);
			stream.Write(this.IsHttpOnly);
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x001A3568 File Offset: 0x001A1768
		internal void LoadFrom(BinaryReader stream)
		{
			stream.ReadInt32();
			this.Name = stream.ReadString();
			this.Value = stream.ReadString();
			this.Date = DateTime.FromBinary(stream.ReadInt64());
			this.LastAccess = DateTime.FromBinary(stream.ReadInt64());
			this.Expires = DateTime.FromBinary(stream.ReadInt64());
			this.MaxAge = stream.ReadInt64();
			this.IsSession = stream.ReadBoolean();
			this.Domain = stream.ReadString();
			this.Path = stream.ReadString();
			this.IsSecure = stream.ReadBoolean();
			this.IsHttpOnly = stream.ReadBoolean();
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x001A360F File Offset: 0x001A180F
		public override string ToString()
		{
			return this.Name + "=" + this.Value;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x001A3627 File Offset: 0x001A1827
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals(obj as Cookie);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x001A363C File Offset: 0x001A183C
		public bool Equals(Cookie cookie)
		{
			return cookie != null && (this == cookie || (this.Name.Equals(cookie.Name, StringComparison.Ordinal) && ((this.Domain == null && cookie.Domain == null) || this.Domain.Equals(cookie.Domain, StringComparison.Ordinal)) && ((this.Path == null && cookie.Path == null) || this.Path.Equals(cookie.Path, StringComparison.Ordinal))));
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x001A36B2 File Offset: 0x001A18B2
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x001A36C0 File Offset: 0x001A18C0
		private static string ReadValue(string str, ref int pos)
		{
			string empty = string.Empty;
			if (str == null)
			{
				return empty;
			}
			return str.Read(ref pos, ';', true);
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x001A36E4 File Offset: 0x001A18E4
		private static List<HeaderValue> ParseCookieHeader(string str)
		{
			List<HeaderValue> list = new List<HeaderValue>();
			if (str == null)
			{
				return list;
			}
			int i = 0;
			while (i < str.Length)
			{
				HeaderValue headerValue = new HeaderValue(str.Read(ref i, (char ch) => ch != '=' && ch != ';', true).Trim());
				if (i < str.Length && str[i - 1] == '=')
				{
					headerValue.Value = Cookie.ReadValue(str, ref i);
				}
				list.Add(headerValue);
			}
			return list;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x001A3768 File Offset: 0x001A1968
		public int CompareTo(Cookie other)
		{
			return this.LastAccess.CompareTo(other.LastAccess);
		}

		// Token: 0x04002F10 RID: 12048
		private const int Version = 1;
	}
}
