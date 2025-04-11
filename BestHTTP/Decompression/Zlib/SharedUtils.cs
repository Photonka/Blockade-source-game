using System;
using System.IO;
using System.Text;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F1 RID: 2033
	internal class SharedUtils
	{
		// Token: 0x06004899 RID: 18585 RVA: 0x001A11DE File Offset: 0x0019F3DE
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x001A11E8 File Offset: 0x0019F3E8
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x000B25EC File Offset: 0x000B07EC
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x001A1229 File Offset: 0x0019F429
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
