using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x0200027B RID: 635
	public class UrlBase64
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x000BAD7C File Offset: 0x000B8F7C
		public static byte[] Encode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Encode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception encoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000BADD0 File Offset: 0x000B8FD0
		public static int Encode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Encode(data, 0, data.Length, outStr);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000BADE4 File Offset: 0x000B8FE4
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Decode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000BAE38 File Offset: 0x000B9038
		public static int Decode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Decode(data, 0, data.Length, outStr);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000BAE4C File Offset: 0x000B904C
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.DecodeString(data, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000BAE9C File Offset: 0x000B909C
		public static int Decode(string data, Stream outStr)
		{
			return UrlBase64.encoder.DecodeString(data, outStr);
		}

		// Token: 0x040016EB RID: 5867
		private static readonly IEncoder encoder = new UrlBase64Encoder();
	}
}
