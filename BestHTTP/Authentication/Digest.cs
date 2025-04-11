using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.Extensions;

namespace BestHTTP.Authentication
{
	// Token: 0x02000804 RID: 2052
	public sealed class Digest
	{
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x001A58CF File Offset: 0x001A3ACF
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x001A58D7 File Offset: 0x001A3AD7
		public Uri Uri { get; private set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x001A58E0 File Offset: 0x001A3AE0
		// (set) Token: 0x06004985 RID: 18821 RVA: 0x001A58E8 File Offset: 0x001A3AE8
		public AuthenticationTypes Type { get; private set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x001A58F1 File Offset: 0x001A3AF1
		// (set) Token: 0x06004987 RID: 18823 RVA: 0x001A58F9 File Offset: 0x001A3AF9
		public string Realm { get; private set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x001A5902 File Offset: 0x001A3B02
		// (set) Token: 0x06004989 RID: 18825 RVA: 0x001A590A File Offset: 0x001A3B0A
		public bool Stale { get; private set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x001A5913 File Offset: 0x001A3B13
		// (set) Token: 0x0600498B RID: 18827 RVA: 0x001A591B File Offset: 0x001A3B1B
		private string Nonce { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x001A5924 File Offset: 0x001A3B24
		// (set) Token: 0x0600498D RID: 18829 RVA: 0x001A592C File Offset: 0x001A3B2C
		private string Opaque { get; set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x001A5935 File Offset: 0x001A3B35
		// (set) Token: 0x0600498F RID: 18831 RVA: 0x001A593D File Offset: 0x001A3B3D
		private string Algorithm { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x001A5946 File Offset: 0x001A3B46
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x001A594E File Offset: 0x001A3B4E
		public List<string> ProtectedUris { get; private set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x001A5957 File Offset: 0x001A3B57
		// (set) Token: 0x06004993 RID: 18835 RVA: 0x001A595F File Offset: 0x001A3B5F
		private string QualityOfProtections { get; set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x001A5968 File Offset: 0x001A3B68
		// (set) Token: 0x06004995 RID: 18837 RVA: 0x001A5970 File Offset: 0x001A3B70
		private int NonceCount { get; set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x001A5979 File Offset: 0x001A3B79
		// (set) Token: 0x06004997 RID: 18839 RVA: 0x001A5981 File Offset: 0x001A3B81
		private string HA1Sess { get; set; }

		// Token: 0x06004998 RID: 18840 RVA: 0x001A598A File Offset: 0x001A3B8A
		internal Digest(Uri uri)
		{
			this.Uri = uri;
			this.Algorithm = "md5";
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x001A59A4 File Offset: 0x001A3BA4
		public void ParseChallange(string header)
		{
			this.Type = AuthenticationTypes.Unknown;
			this.Stale = false;
			this.Opaque = null;
			this.HA1Sess = null;
			this.NonceCount = 0;
			this.QualityOfProtections = null;
			if (this.ProtectedUris != null)
			{
				this.ProtectedUris.Clear();
			}
			foreach (HeaderValue headerValue in new WWWAuthenticateHeaderParser(header).Values)
			{
				string key = headerValue.Key;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
				if (num <= 1863671838U)
				{
					if (num <= 474311018U)
					{
						if (num != 87360061U)
						{
							if (num == 474311018U)
							{
								if (key == "algorithm")
								{
									this.Algorithm = headerValue.Value;
								}
							}
						}
						else if (key == "basic")
						{
							this.Type = AuthenticationTypes.Basic;
						}
					}
					else if (num != 1749328254U)
					{
						if (num == 1863671838U)
						{
							if (key == "opaque")
							{
								this.Opaque = headerValue.Value;
							}
						}
					}
					else if (key == "stale")
					{
						this.Stale = bool.Parse(headerValue.Value);
					}
				}
				else if (num <= 3885209585U)
				{
					if (num != 1914854288U)
					{
						if (num == 3885209585U)
						{
							if (key == "domain")
							{
								if (!string.IsNullOrEmpty(headerValue.Value) && headerValue.Value.Length != 0)
								{
									if (this.ProtectedUris == null)
									{
										this.ProtectedUris = new List<string>();
									}
									int num2 = 0;
									string item = headerValue.Value.Read(ref num2, ' ', true);
									do
									{
										this.ProtectedUris.Add(item);
										item = headerValue.Value.Read(ref num2, ' ', true);
									}
									while (num2 < headerValue.Value.Length);
								}
							}
						}
					}
					else if (key == "realm")
					{
						this.Realm = headerValue.Value;
					}
				}
				else if (num != 4143537083U)
				{
					if (num != 4178082296U)
					{
						if (num == 4179908061U)
						{
							if (key == "digest")
							{
								this.Type = AuthenticationTypes.Digest;
							}
						}
					}
					else if (key == "nonce")
					{
						this.Nonce = headerValue.Value;
					}
				}
				else if (key == "qop")
				{
					this.QualityOfProtections = headerValue.Value;
				}
			}
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x001A5C78 File Offset: 0x001A3E78
		public string GenerateResponseHeader(HTTPRequest request, Credentials credentials, bool isProxy = false)
		{
			try
			{
				AuthenticationTypes type = this.Type;
				if (type == AuthenticationTypes.Basic)
				{
					return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", credentials.UserName, credentials.Password)));
				}
				if (type == AuthenticationTypes.Digest)
				{
					int nonceCount = this.NonceCount;
					this.NonceCount = nonceCount + 1;
					string text = string.Empty;
					string text2 = new Random(request.GetHashCode()).Next(int.MinValue, int.MaxValue).ToString("X8");
					string text3 = this.NonceCount.ToString("X8");
					string a = this.Algorithm.TrimAndLower();
					if (!(a == "md5"))
					{
						if (!(a == "md5-sess"))
						{
							return string.Empty;
						}
						if (string.IsNullOrEmpty(this.HA1Sess))
						{
							this.HA1Sess = string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
							{
								credentials.UserName,
								this.Realm,
								credentials.Password,
								this.Nonce,
								text3
							}).CalculateMD5Hash();
						}
						text = this.HA1Sess;
					}
					else
					{
						text = string.Format("{0}:{1}:{2}", credentials.UserName, this.Realm, credentials.Password).CalculateMD5Hash();
					}
					string text4 = string.Empty;
					string text5 = (this.QualityOfProtections != null) ? this.QualityOfProtections.TrimAndLower() : null;
					string text6 = isProxy ? "CONNECT" : request.MethodType.ToString().ToUpper();
					string text7 = isProxy ? (request.CurrentUri.Host + ":" + request.CurrentUri.Port) : request.CurrentUri.GetRequestPathAndQueryURL();
					if (text5 == null)
					{
						string arg = (request.MethodType.ToString().ToUpper() + ":" + request.CurrentUri.GetRequestPathAndQueryURL()).CalculateMD5Hash();
						text4 = string.Format("{0}:{1}:{2}", text, this.Nonce, arg).CalculateMD5Hash();
					}
					else if (text5.Contains("auth-int"))
					{
						text5 = "auth-int";
						byte[] array = request.GetEntityBody();
						if (array == null)
						{
							array = VariableSizedBufferPool.NoData;
						}
						string text8 = string.Format("{0}:{1}:{2}", text6, text7, array.CalculateMD5Hash()).CalculateMD5Hash();
						text4 = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
						{
							text,
							this.Nonce,
							text3,
							text2,
							text5,
							text8
						}).CalculateMD5Hash();
					}
					else
					{
						if (!text5.Contains("auth"))
						{
							return string.Empty;
						}
						text5 = "auth";
						string text9 = (text6 + ":" + text7).CalculateMD5Hash();
						text4 = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
						{
							text,
							this.Nonce,
							text3,
							text2,
							text5,
							text9
						}).CalculateMD5Hash();
					}
					string text10 = string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", cnonce=\"{4}\", response=\"{5}\"", new object[]
					{
						credentials.UserName,
						this.Realm,
						this.Nonce,
						text7,
						text2,
						text4
					});
					if (text5 != null)
					{
						text10 = string.Concat(new string[]
						{
							text10,
							", qop=\"",
							text5,
							"\", nc=",
							text3
						});
					}
					if (!string.IsNullOrEmpty(this.Opaque))
					{
						text10 = text10 + ", opaque=\"" + this.Opaque + "\"";
					}
					return text10;
				}
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x001A6058 File Offset: 0x001A4258
		public bool IsUriProtected(Uri uri)
		{
			if (string.CompareOrdinal(uri.Host, this.Uri.Host) != 0)
			{
				return false;
			}
			string text = uri.ToString();
			if (this.ProtectedUris != null && this.ProtectedUris.Count > 0)
			{
				for (int i = 0; i < this.ProtectedUris.Count; i++)
				{
					if (text.Contains(this.ProtectedUris[i]))
					{
						return true;
					}
				}
			}
			return true;
		}
	}
}
