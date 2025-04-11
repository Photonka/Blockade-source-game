using System;
using System.IO;
using BestHTTP.Extensions;
using BestHTTP.ServerSentEvents;
using BestHTTP.WebSocket;

namespace BestHTTP
{
	// Token: 0x02000174 RID: 372
	public static class HTTPProtocolFactory
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x00094FDC File Offset: 0x000931DC
		public static HTTPResponse Get(SupportedProtocols protocol, HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
		{
			if (protocol == SupportedProtocols.WebSocket)
			{
				return new WebSocketResponse(request, stream, isStreamed, isFromCache);
			}
			if (protocol != SupportedProtocols.ServerSentEvents)
			{
				return new HTTPResponse(request, new ReadOnlyBufferedStream(stream), isStreamed, isFromCache);
			}
			return new EventSourceResponse(request, stream, isStreamed, isFromCache);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00095010 File Offset: 0x00093210
		public static SupportedProtocols GetProtocolFromUri(Uri uri)
		{
			if (uri == null || uri.Scheme == null)
			{
				throw new Exception("Malformed URI in GetProtocolFromUri");
			}
			string a = uri.Scheme.ToLowerInvariant();
			if (a == "ws" || a == "wss")
			{
				return SupportedProtocols.WebSocket;
			}
			return SupportedProtocols.HTTP;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00095064 File Offset: 0x00093264
		public static bool IsSecureProtocol(Uri uri)
		{
			if (uri == null || uri.Scheme == null)
			{
				throw new Exception("Malformed URI in IsSecureProtocol");
			}
			string a = uri.Scheme.ToLowerInvariant();
			return a == "https" || a == "wss";
		}
	}
}
