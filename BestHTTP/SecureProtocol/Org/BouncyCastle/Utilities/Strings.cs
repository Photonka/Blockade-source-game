using System;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x02000251 RID: 593
	public abstract class Strings
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x000B24D4 File Offset: 0x000B06D4
		internal static bool IsOneOf(string s, params string[] candidates)
		{
			foreach (string b in candidates)
			{
				if (s == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000B2504 File Offset: 0x000B0704
		public static string FromByteArray(byte[] bs)
		{
			char[] array = new char[bs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToChar(bs[i]);
			}
			return new string(array);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000B253C File Offset: 0x000B073C
		public static byte[] ToByteArray(char[] cs)
		{
			byte[] array = new byte[cs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(cs[i]);
			}
			return array;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000B256C File Offset: 0x000B076C
		public static byte[] ToByteArray(string s)
		{
			byte[] array = new byte[s.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(s[i]);
			}
			return array;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000B25A3 File Offset: 0x000B07A3
		public static string FromAsciiByteArray(byte[] bytes)
		{
			return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000B25B4 File Offset: 0x000B07B4
		public static byte[] ToAsciiByteArray(char[] cs)
		{
			return Encoding.ASCII.GetBytes(cs);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000B25C1 File Offset: 0x000B07C1
		public static byte[] ToAsciiByteArray(string s)
		{
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000B25CE File Offset: 0x000B07CE
		public static string FromUtf8ByteArray(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000B25DF File Offset: 0x000B07DF
		public static byte[] ToUtf8ByteArray(char[] cs)
		{
			return Encoding.UTF8.GetBytes(cs);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000B25EC File Offset: 0x000B07EC
		public static byte[] ToUtf8ByteArray(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}
	}
}
