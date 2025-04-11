using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000276 RID: 630
	public sealed class Hex
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x00023EF4 File Offset: 0x000220F4
		private Hex()
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000BA8D4 File Offset: 0x000B8AD4
		public static string ToHexString(byte[] data)
		{
			return Hex.ToHexString(data, 0, data.Length);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		public static string ToHexString(byte[] data, int off, int length)
		{
			return Strings.FromAsciiByteArray(Hex.Encode(data, off, length));
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000BA8EF File Offset: 0x000B8AEF
		public static byte[] Encode(byte[] data)
		{
			return Hex.Encode(data, 0, data.Length);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000BA8FC File Offset: 0x000B8AFC
		public static byte[] Encode(byte[] data, int off, int length)
		{
			MemoryStream memoryStream = new MemoryStream(length * 2);
			Hex.encoder.Encode(data, off, length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000BA927 File Offset: 0x000B8B27
		public static int Encode(byte[] data, Stream outStream)
		{
			return Hex.encoder.Encode(data, 0, data.Length, outStream);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000BA939 File Offset: 0x000B8B39
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			return Hex.encoder.Encode(data, off, length, outStream);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000BA94C File Offset: 0x000B8B4C
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.Decode(data, 0, data.Length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000BA980 File Offset: 0x000B8B80
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.DecodeString(data, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000BA9B0 File Offset: 0x000B8BB0
		public static int Decode(string data, Stream outStream)
		{
			return Hex.encoder.DecodeString(data, outStream);
		}

		// Token: 0x040016E7 RID: 5863
		private static readonly IEncoder encoder = new HexEncoder();
	}
}
