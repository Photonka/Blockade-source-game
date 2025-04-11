using System;
using System.Collections.Generic;
using BestHTTP.Decompression.Zlib;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket.Extensions
{
	// Token: 0x020001B2 RID: 434
	public sealed class PerMessageCompression : IExtension
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x000A0908 File Offset: 0x0009EB08
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x000A0910 File Offset: 0x0009EB10
		public bool ClientNoContextTakeover { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000A0919 File Offset: 0x0009EB19
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x000A0921 File Offset: 0x0009EB21
		public bool ServerNoContextTakeover { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000A092A File Offset: 0x0009EB2A
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x000A0932 File Offset: 0x0009EB32
		public int ClientMaxWindowBits { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000A093B File Offset: 0x0009EB3B
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x000A0943 File Offset: 0x0009EB43
		public int ServerMaxWindowBits { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000A094C File Offset: 0x0009EB4C
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x000A0954 File Offset: 0x0009EB54
		public CompressionLevel Level { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x000A095D File Offset: 0x0009EB5D
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x000A0965 File Offset: 0x0009EB65
		public int MinimumDataLegthToCompress { get; set; }

		// Token: 0x0600104D RID: 4173 RVA: 0x000A096E File Offset: 0x0009EB6E
		public PerMessageCompression() : this(CompressionLevel.Default, false, false, 15, 15, 256)
		{
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000A0982 File Offset: 0x0009EB82
		public PerMessageCompression(CompressionLevel level, bool clientNoContextTakeover, bool serverNoContextTakeover, int desiredClientMaxWindowBits, int desiredServerMaxWindowBits, int minDatalengthToCompress)
		{
			this.Level = level;
			this.ClientNoContextTakeover = clientNoContextTakeover;
			this.ServerNoContextTakeover = this.ServerNoContextTakeover;
			this.ClientMaxWindowBits = desiredClientMaxWindowBits;
			this.ServerMaxWindowBits = desiredServerMaxWindowBits;
			this.MinimumDataLegthToCompress = minDatalengthToCompress;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000A09BC File Offset: 0x0009EBBC
		public void AddNegotiation(HTTPRequest request)
		{
			string text = "permessage-deflate";
			if (this.ServerNoContextTakeover)
			{
				text += "; server_no_context_takeover";
			}
			if (this.ClientNoContextTakeover)
			{
				text += "; client_no_context_takeover";
			}
			if (this.ServerMaxWindowBits != 15)
			{
				text = text + "; server_max_window_bits=" + this.ServerMaxWindowBits.ToString();
			}
			else
			{
				this.ServerMaxWindowBits = 15;
			}
			if (this.ClientMaxWindowBits != 15)
			{
				text = text + "; client_max_window_bits=" + this.ClientMaxWindowBits.ToString();
			}
			else
			{
				text += "; client_max_window_bits";
				this.ClientMaxWindowBits = 15;
			}
			request.AddHeader("Sec-WebSocket-Extensions", text);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000A0A6C File Offset: 0x0009EC6C
		public bool ParseNegotiation(WebSocketResponse resp)
		{
			List<string> headerValues = resp.GetHeaderValues("Sec-WebSocket-Extensions");
			if (headerValues == null)
			{
				return false;
			}
			for (int i = 0; i < headerValues.Count; i++)
			{
				HeaderParser headerParser = new HeaderParser(headerValues[i]);
				for (int j = 0; j < headerParser.Values.Count; j++)
				{
					HeaderValue headerValue = headerParser.Values[i];
					if (!string.IsNullOrEmpty(headerValue.Key) && headerValue.Key.StartsWith("permessage-deflate", StringComparison.OrdinalIgnoreCase))
					{
						HTTPManager.Logger.Information("PerMessageCompression", "Enabled with header: " + headerValues[i]);
						HeaderValue headerValue2;
						if (headerValue.TryGetOption("client_no_context_takeover", out headerValue2))
						{
							this.ClientNoContextTakeover = true;
						}
						if (headerValue.TryGetOption("server_no_context_takeover", out headerValue2))
						{
							this.ServerNoContextTakeover = true;
						}
						int clientMaxWindowBits;
						if (headerValue.TryGetOption("client_max_window_bits", out headerValue2) && headerValue2.HasValue && int.TryParse(headerValue2.Value, out clientMaxWindowBits))
						{
							this.ClientMaxWindowBits = clientMaxWindowBits;
						}
						int serverMaxWindowBits;
						if (headerValue.TryGetOption("server_max_window_bits", out headerValue2) && headerValue2.HasValue && int.TryParse(headerValue2.Value, out serverMaxWindowBits))
						{
							this.ServerMaxWindowBits = serverMaxWindowBits;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000A0BB3 File Offset: 0x0009EDB3
		public byte GetFrameHeader(WebSocketFrame writer, byte inFlag)
		{
			if ((writer.Type == WebSocketFrameTypes.Binary || writer.Type == WebSocketFrameTypes.Text) && writer.Data != null && writer.Data.Length >= this.MinimumDataLegthToCompress)
			{
				return inFlag | 64;
			}
			return inFlag;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000A0BE6 File Offset: 0x0009EDE6
		public byte[] Encode(WebSocketFrame writer)
		{
			if (writer.Data == null)
			{
				return VariableSizedBufferPool.NoData;
			}
			if ((writer.Header & 64) != 0)
			{
				return this.Compress(writer.Data, writer.DataLength);
			}
			return writer.Data;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000A0C1A File Offset: 0x0009EE1A
		public byte[] Decode(byte header, byte[] data, int length)
		{
			if ((header & 64) != 0)
			{
				return this.Decompress(data, length);
			}
			return data;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000A0C2C File Offset: 0x0009EE2C
		private byte[] Compress(byte[] data, int length)
		{
			if (this.compressorOutputStream == null)
			{
				this.compressorOutputStream = new BufferPoolMemoryStream();
			}
			this.compressorOutputStream.SetLength(0L);
			if (this.compressorDeflateStream == null)
			{
				this.compressorDeflateStream = new DeflateStream(this.compressorOutputStream, CompressionMode.Compress, this.Level, true, this.ClientMaxWindowBits);
				this.compressorDeflateStream.FlushMode = FlushType.Sync;
			}
			byte[] result = null;
			try
			{
				this.compressorDeflateStream.Write(data, 0, length);
				this.compressorDeflateStream.Flush();
				this.compressorOutputStream.Position = 0L;
				this.compressorOutputStream.SetLength(this.compressorOutputStream.Length - 4L);
				result = this.compressorOutputStream.ToArray();
			}
			finally
			{
				if (this.ClientNoContextTakeover)
				{
					this.compressorDeflateStream.Dispose();
					this.compressorDeflateStream = null;
				}
			}
			return result;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000A0D08 File Offset: 0x0009EF08
		private byte[] Decompress(byte[] data, int length)
		{
			if (this.decompressorInputStream == null)
			{
				this.decompressorInputStream = new BufferPoolMemoryStream(data.Length + 4);
			}
			this.decompressorInputStream.Write(data, 0, length);
			this.decompressorInputStream.Write(PerMessageCompression.Trailer, 0, PerMessageCompression.Trailer.Length);
			this.decompressorInputStream.Position = 0L;
			if (this.decompressorDeflateStream == null)
			{
				this.decompressorDeflateStream = new DeflateStream(this.decompressorInputStream, CompressionMode.Decompress, CompressionLevel.Default, true, this.ServerMaxWindowBits);
				this.decompressorDeflateStream.FlushMode = FlushType.Sync;
			}
			if (this.decompressorOutputStream == null)
			{
				this.decompressorOutputStream = new BufferPoolMemoryStream();
			}
			this.decompressorOutputStream.SetLength(0L);
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			int count;
			while ((count = this.decompressorDeflateStream.Read(array, 0, array.Length)) != 0)
			{
				this.decompressorOutputStream.Write(array, 0, count);
			}
			VariableSizedBufferPool.Release(array);
			this.decompressorDeflateStream.SetLength(0L);
			byte[] result = this.decompressorOutputStream.ToArray();
			if (this.ServerNoContextTakeover)
			{
				this.decompressorDeflateStream.Dispose();
				this.decompressorDeflateStream = null;
			}
			return result;
		}

		// Token: 0x0400132A RID: 4906
		public const int MinDataLengthToCompressDefault = 256;

		// Token: 0x0400132B RID: 4907
		private static readonly byte[] Trailer = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x04001332 RID: 4914
		private BufferPoolMemoryStream compressorOutputStream;

		// Token: 0x04001333 RID: 4915
		private DeflateStream compressorDeflateStream;

		// Token: 0x04001334 RID: 4916
		private BufferPoolMemoryStream decompressorInputStream;

		// Token: 0x04001335 RID: 4917
		private BufferPoolMemoryStream decompressorOutputStream;

		// Token: 0x04001336 RID: 4918
		private DeflateStream decompressorDeflateStream;
	}
}
