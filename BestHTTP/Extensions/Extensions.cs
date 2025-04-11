using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D5 RID: 2005
	public static class Extensions
	{
		// Token: 0x060047AE RID: 18350 RVA: 0x00199820 File Offset: 0x00197A20
		public static string AsciiToString(this byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder(bytes.Length);
			foreach (byte b in bytes)
			{
				stringBuilder.Append((char)((b <= 127) ? b : 63));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00199864 File Offset: 0x00197A64
		public static byte[] GetASCIIBytes(this string str)
		{
			byte[] array = VariableSizedBufferPool.Get((long)str.Length, false);
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				array[i] = (byte)((c < '\u0080') ? c : '?');
			}
			return array;
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x001998AC File Offset: 0x00197AAC
		public static void SendAsASCII(this BinaryWriter stream, string str)
		{
			foreach (char c in str)
			{
				stream.Write((byte)((c < '\u0080') ? c : '?'));
			}
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x001998E6 File Offset: 0x00197AE6
		public static void WriteLine(this Stream fs)
		{
			fs.Write(HTTPRequest.EOL, 0, 2);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x001998F8 File Offset: 0x00197AF8
		public static void WriteLine(this Stream fs, string line)
		{
			byte[] asciibytes = line.GetASCIIBytes();
			fs.Write(asciibytes, 0, asciibytes.Length);
			fs.WriteLine();
			VariableSizedBufferPool.Release(asciibytes);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00199924 File Offset: 0x00197B24
		public static void WriteLine(this Stream fs, string format, params object[] values)
		{
			byte[] asciibytes = string.Format(format, values).GetASCIIBytes();
			fs.Write(asciibytes, 0, asciibytes.Length);
			fs.WriteLine();
			VariableSizedBufferPool.Release(asciibytes);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00199958 File Offset: 0x00197B58
		public static string GetRequestPathAndQueryURL(this Uri uri)
		{
			string text = uri.GetComponents(UriComponents.PathAndQuery, UriFormat.UriEscaped);
			if (string.IsNullOrEmpty(text))
			{
				text = "/";
			}
			return text;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00199980 File Offset: 0x00197B80
		public static string[] FindOption(this string str, string option)
		{
			string[] array = str.ToLower().Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			option = option.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(option))
				{
					return array[i].Split(new char[]
					{
						'='
					}, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			return null;
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000B961E File Offset: 0x000B781E
		public static void WriteArray(this Stream stream, byte[] array)
		{
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x001999DC File Offset: 0x00197BDC
		public static bool IsHostIsAnIPAddress(this Uri uri)
		{
			return !(uri == null) && (Extensions.IsIpV4AddressValid(uri.Host) || Extensions.IsIpV6AddressValid(uri.Host));
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00199A03 File Offset: 0x00197C03
		public static bool IsIpV4AddressValid(string address)
		{
			return !string.IsNullOrEmpty(address) && Extensions.validIpV4AddressRegex.IsMatch(address.Trim());
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x00199A20 File Offset: 0x00197C20
		public static bool IsIpV6AddressValid(string address)
		{
			IPAddress ipaddress;
			return !string.IsNullOrEmpty(address) && IPAddress.TryParse(address, out ipaddress) && ipaddress.AddressFamily == AddressFamily.InterNetworkV6;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00199A4C File Offset: 0x00197C4C
		public static int ToInt32(this string str, int defaultValue = 0)
		{
			if (str == null)
			{
				return defaultValue;
			}
			int result;
			try
			{
				result = int.Parse(str);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x00199A80 File Offset: 0x00197C80
		public static long ToInt64(this string str, long defaultValue = 0L)
		{
			if (str == null)
			{
				return defaultValue;
			}
			long result;
			try
			{
				result = long.Parse(str);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x00199AB4 File Offset: 0x00197CB4
		public static DateTime ToDateTime(this string str, DateTime defaultValue = default(DateTime))
		{
			if (str == null)
			{
				return defaultValue;
			}
			DateTime result;
			try
			{
				DateTime.TryParse(str, out defaultValue);
				result = defaultValue.ToUniversalTime();
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x00199AF0 File Offset: 0x00197CF0
		public static string ToStrOrEmpty(this string str)
		{
			if (str == null)
			{
				return string.Empty;
			}
			return str;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x00199AFC File Offset: 0x00197CFC
		public static string CalculateMD5Hash(this string input)
		{
			byte[] asciibytes = input.GetASCIIBytes();
			string result = asciibytes.CalculateMD5Hash();
			VariableSizedBufferPool.Release(asciibytes);
			return result;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x00199B1C File Offset: 0x00197D1C
		public static string CalculateMD5Hash(this byte[] input)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] array = md.ComputeHash(input);
				StringBuilder stringBuilder = new StringBuilder(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				VariableSizedBufferPool.Release(array);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00199B94 File Offset: 0x00197D94
		internal static string Read(this string str, ref int pos, char block, bool needResult = true)
		{
			return str.Read(ref pos, (char ch) => ch != block, needResult);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00199BC4 File Offset: 0x00197DC4
		internal static string Read(this string str, ref int pos, Func<char, bool> block, bool needResult = true)
		{
			if (pos >= str.Length)
			{
				return string.Empty;
			}
			str.SkipWhiteSpace(ref pos);
			int num = pos;
			while (pos < str.Length && block(str[pos]))
			{
				pos++;
			}
			string result = needResult ? str.Substring(num, pos - num) : null;
			pos++;
			return result;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00199C24 File Offset: 0x00197E24
		internal static string ReadPossibleQuotedText(this string str, ref int pos)
		{
			string result = string.Empty;
			if (str == null)
			{
				return result;
			}
			if (str[pos] == '"')
			{
				str.Read(ref pos, '"', false);
				result = str.Read(ref pos, '"', true);
				str.Read(ref pos, ',', false);
			}
			else
			{
				result = str.Read(ref pos, (char ch) => ch != ',' && ch != ';', true);
			}
			return result;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00199C94 File Offset: 0x00197E94
		internal static void SkipWhiteSpace(this string str, ref int pos)
		{
			if (pos >= str.Length)
			{
				return;
			}
			while (pos < str.Length && char.IsWhiteSpace(str[pos]))
			{
				pos++;
			}
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00199CC0 File Offset: 0x00197EC0
		internal static string TrimAndLower(this string str)
		{
			if (str == null)
			{
				return null;
			}
			char[] array = new char[str.Length];
			int length = 0;
			foreach (char c in str)
			{
				if (!char.IsWhiteSpace(c) && !char.IsControl(c))
				{
					array[length++] = char.ToLowerInvariant(c);
				}
			}
			return new string(array, 0, length);
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00199D20 File Offset: 0x00197F20
		internal static char? Peek(this string str, int pos)
		{
			if (pos < 0 || pos >= str.Length)
			{
				return null;
			}
			return new char?(str[pos]);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x00199D50 File Offset: 0x00197F50
		internal static List<HeaderValue> ParseOptionalHeader(this string str)
		{
			List<HeaderValue> list = new List<HeaderValue>();
			if (str == null)
			{
				return list;
			}
			int i = 0;
			while (i < str.Length)
			{
				HeaderValue headerValue = new HeaderValue(str.Read(ref i, (char ch) => ch != '=' && ch != ',', true).TrimAndLower());
				if (str[i - 1] == '=')
				{
					headerValue.Value = str.ReadPossibleQuotedText(ref i);
				}
				list.Add(headerValue);
			}
			return list;
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x00199DCC File Offset: 0x00197FCC
		internal static List<HeaderValue> ParseQualityParams(this string str)
		{
			List<HeaderValue> list = new List<HeaderValue>();
			if (str == null)
			{
				return list;
			}
			int i = 0;
			while (i < str.Length)
			{
				HeaderValue headerValue = new HeaderValue(str.Read(ref i, (char ch) => ch != ',' && ch != ';', true).TrimAndLower());
				if (str[i - 1] == ';')
				{
					str.Read(ref i, '=', false);
					headerValue.Value = str.Read(ref i, ',', true);
				}
				list.Add(headerValue);
			}
			return list;
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x00199E58 File Offset: 0x00198058
		public static void ReadBuffer(this Stream stream, byte[] buffer)
		{
			int num = 0;
			for (;;)
			{
				int num2 = stream.Read(buffer, num, buffer.Length - num);
				if (num2 <= 0)
				{
					break;
				}
				num += num2;
				if (num >= buffer.Length)
				{
					return;
				}
			}
			throw ExceptionHelper.ServerClosedTCPStream();
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x00199E8C File Offset: 0x0019808C
		public static void ReadBuffer(this Stream stream, byte[] buffer, int length)
		{
			int num = 0;
			for (;;)
			{
				int num2 = stream.Read(buffer, num, length - num);
				if (num2 <= 0)
				{
					break;
				}
				num += num2;
				if (num >= length)
				{
					return;
				}
			}
			throw ExceptionHelper.ServerClosedTCPStream();
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x00199EBC File Offset: 0x001980BC
		public static void WriteString(this BufferPoolMemoryStream ms, string str)
		{
			int byteCount = Encoding.UTF8.GetByteCount(str);
			byte[] array = VariableSizedBufferPool.Get((long)byteCount, true);
			Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
			ms.Write(array, 0, byteCount);
			VariableSizedBufferPool.Release(array);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x00199F02 File Offset: 0x00198102
		public static void WriteLine(this BufferPoolMemoryStream ms)
		{
			ms.Write(HTTPRequest.EOL, 0, HTTPRequest.EOL.Length);
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x00199F17 File Offset: 0x00198117
		public static void WriteLine(this BufferPoolMemoryStream ms, string str)
		{
			ms.WriteString(str);
			ms.Write(HTTPRequest.EOL, 0, HTTPRequest.EOL.Length);
		}

		// Token: 0x04002DC2 RID: 11714
		private static readonly Regex validIpV4AddressRegex = new Regex("\\b(?:\\d{1,3}\\.){3}\\d{1,3}\\b", RegexOptions.IgnoreCase);
	}
}
