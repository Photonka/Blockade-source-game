using System;
using System.IO;
using System.Text;
using BestHTTP.Authentication;
using BestHTTP.Extensions;
using BestHTTP.Logger;

namespace BestHTTP
{
	// Token: 0x02000176 RID: 374
	public sealed class HTTPProxy : Proxy
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000950EE File Offset: 0x000932EE
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x000950F6 File Offset: 0x000932F6
		public bool IsTransparent { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x000950FF File Offset: 0x000932FF
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00095107 File Offset: 0x00093307
		public bool SendWholeUri { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00095110 File Offset: 0x00093310
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00095118 File Offset: 0x00093318
		public bool NonTransparentForHTTPS { get; set; }

		// Token: 0x06000D69 RID: 3433 RVA: 0x00095121 File Offset: 0x00093321
		public HTTPProxy(Uri address) : this(address, null, false)
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0009512C File Offset: 0x0009332C
		public HTTPProxy(Uri address, Credentials credentials) : this(address, credentials, false)
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00095137 File Offset: 0x00093337
		public HTTPProxy(Uri address, Credentials credentials, bool isTransparent) : this(address, credentials, isTransparent, true)
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00095143 File Offset: 0x00093343
		public HTTPProxy(Uri address, Credentials credentials, bool isTransparent, bool sendWholeUri) : this(address, credentials, isTransparent, sendWholeUri, true)
		{
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00095151 File Offset: 0x00093351
		public HTTPProxy(Uri address, Credentials credentials, bool isTransparent, bool sendWholeUri, bool nonTransparentForHTTPS) : base(address, credentials)
		{
			this.IsTransparent = isTransparent;
			this.SendWholeUri = sendWholeUri;
			this.NonTransparentForHTTPS = nonTransparentForHTTPS;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00095172 File Offset: 0x00093372
		internal override string GetRequestPath(Uri uri)
		{
			if (!this.SendWholeUri)
			{
				return uri.GetRequestPathAndQueryURL();
			}
			return uri.OriginalString;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0009518C File Offset: 0x0009338C
		internal override void Connect(Stream stream, HTTPRequest request)
		{
			bool flag = HTTPProtocolFactory.IsSecureProtocol(request.CurrentUri);
			if (!this.IsTransparent || (flag && this.NonTransparentForHTTPS))
			{
				using (WriteOnlyBufferedStream writeOnlyBufferedStream = new WriteOnlyBufferedStream(stream, HTTPRequest.UploadChunkSize))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(writeOnlyBufferedStream, Encoding.UTF8))
					{
						for (;;)
						{
							bool flag2 = false;
							string text = string.Format("CONNECT {0}:{1} HTTP/1.1", request.CurrentUri.Host, request.CurrentUri.Port.ToString());
							HTTPManager.Logger.Information("HTTPConnection", "Sending " + text);
							binaryWriter.SendAsASCII(text);
							binaryWriter.Write(HTTPRequest.EOL);
							binaryWriter.SendAsASCII("Proxy-Connection: Keep-Alive");
							binaryWriter.Write(HTTPRequest.EOL);
							binaryWriter.SendAsASCII("Connection: Keep-Alive");
							binaryWriter.Write(HTTPRequest.EOL);
							binaryWriter.SendAsASCII(string.Format("Host: {0}:{1}", request.CurrentUri.Host, request.CurrentUri.Port.ToString()));
							binaryWriter.Write(HTTPRequest.EOL);
							if (base.Credentials != null)
							{
								switch (base.Credentials.Type)
								{
								case AuthenticationTypes.Unknown:
								case AuthenticationTypes.Digest:
								{
									Digest digest = DigestStore.Get(base.Address);
									if (digest != null)
									{
										string text2 = digest.GenerateResponseHeader(request, base.Credentials, true);
										if (!string.IsNullOrEmpty(text2))
										{
											string text3 = string.Format("Proxy-Authorization: {0}", text2);
											if (HTTPManager.Logger.Level <= Loglevels.Information)
											{
												HTTPManager.Logger.Information("HTTPConnection", "Sending proxy authorization header: " + text3);
											}
											byte[] asciibytes = text3.GetASCIIBytes();
											binaryWriter.Write(asciibytes);
											binaryWriter.Write(HTTPRequest.EOL);
											VariableSizedBufferPool.Release(asciibytes);
										}
									}
									break;
								}
								case AuthenticationTypes.Basic:
									binaryWriter.Write(string.Format("Proxy-Authorization: {0}", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(base.Credentials.UserName + ":" + base.Credentials.Password))).GetASCIIBytes());
									binaryWriter.Write(HTTPRequest.EOL);
									break;
								}
							}
							binaryWriter.Write(HTTPRequest.EOL);
							binaryWriter.Flush();
							request.ProxyResponse = new HTTPResponse(request, stream, false, false);
							if (!request.ProxyResponse.Receive(-1, true))
							{
								break;
							}
							if (HTTPManager.Logger.Level <= Loglevels.Information)
							{
								HTTPManager.Logger.Information("HTTPConnection", string.Concat(new object[]
								{
									"Proxy returned - status code: ",
									request.ProxyResponse.StatusCode,
									" message: ",
									request.ProxyResponse.Message,
									" Body: ",
									request.ProxyResponse.DataAsText
								}));
							}
							int statusCode = request.ProxyResponse.StatusCode;
							if (statusCode == 407)
							{
								string text4 = DigestStore.FindBest(request.ProxyResponse.GetHeaderValues("proxy-authenticate"));
								if (!string.IsNullOrEmpty(text4))
								{
									Digest orCreate = DigestStore.GetOrCreate(base.Address);
									orCreate.ParseChallange(text4);
									if (base.Credentials != null && orCreate.IsUriProtected(base.Address) && (!request.HasHeader("Proxy-Authorization") || orCreate.Stale))
									{
										flag2 = true;
									}
								}
								if (!flag2)
								{
									goto Block_18;
								}
							}
							else if (!request.ProxyResponse.IsSuccess)
							{
								goto Block_19;
							}
							if (!flag2)
							{
								goto Block_20;
							}
						}
						throw new Exception("Connection to the Proxy Server failed!");
						Block_18:
						throw new Exception(string.Format("Can't authenticate Proxy! Status Code: \"{0}\", Message: \"{1}\" and Response: {2}", request.ProxyResponse.StatusCode, request.ProxyResponse.Message, request.ProxyResponse.DataAsText));
						Block_19:
						throw new Exception(string.Format("Proxy returned Status Code: \"{0}\", Message: \"{1}\" and Response: {2}", request.ProxyResponse.StatusCode, request.ProxyResponse.Message, request.ProxyResponse.DataAsText));
						Block_20:;
					}
				}
			}
		}
	}
}
