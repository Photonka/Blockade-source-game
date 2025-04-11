using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000272 RID: 626
	public sealed class Base64
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x00023EF4 File Offset: 0x000220F4
		private Base64()
		{
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000B9F1F File Offset: 0x000B811F
		public static string ToBase64String(byte[] data)
		{
			return Convert.ToBase64String(data, 0, data.Length);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000B9F2B File Offset: 0x000B812B
		public static string ToBase64String(byte[] data, int off, int length)
		{
			return Convert.ToBase64String(data, off, length);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000B9F35 File Offset: 0x000B8135
		public static byte[] Encode(byte[] data)
		{
			return Base64.Encode(data, 0, data.Length);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000B9F41 File Offset: 0x000B8141
		public static byte[] Encode(byte[] data, int off, int length)
		{
			return Strings.ToAsciiByteArray(Convert.ToBase64String(data, off, length));
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000B9F50 File Offset: 0x000B8150
		public static int Encode(byte[] data, Stream outStream)
		{
			byte[] array = Base64.Encode(data);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000B9F74 File Offset: 0x000B8174
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			byte[] array = Base64.Encode(data, off, length);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000B9F98 File Offset: 0x000B8198
		public static byte[] Decode(byte[] data)
		{
			return Convert.FromBase64String(Strings.FromAsciiByteArray(data));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000B9FA5 File Offset: 0x000B81A5
		public static byte[] Decode(string data)
		{
			return Convert.FromBase64String(data);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000B9FB0 File Offset: 0x000B81B0
		public static int Decode(string data, Stream outStream)
		{
			byte[] array = Base64.Decode(data);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}
	}
}
