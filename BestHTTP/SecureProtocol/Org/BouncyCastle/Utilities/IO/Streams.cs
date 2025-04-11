using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000268 RID: 616
	public sealed class Streams
	{
		// Token: 0x0600171B RID: 5915 RVA: 0x00023EF4 File Offset: 0x000220F4
		private Streams()
		{
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000B97B8 File Offset: 0x000B79B8
		public static void Drain(Stream inStr)
		{
			byte[] array = new byte[512];
			while (inStr.Read(array, 0, array.Length) > 0)
			{
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000B97E0 File Offset: 0x000B79E0
		public static byte[] ReadAll(Stream inStr)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAll(inStr, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000B9800 File Offset: 0x000B7A00
		public static byte[] ReadAllLimited(Stream inStr, int limit)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAllLimited(inStr, (long)limit, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000B9823 File Offset: 0x000B7A23
		public static int ReadFully(Stream inStr, byte[] buf)
		{
			return Streams.ReadFully(inStr, buf, 0, buf.Length);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000B9830 File Offset: 0x000B7A30
		public static int ReadFully(Stream inStr, byte[] buf, int off, int len)
		{
			int i;
			int num;
			for (i = 0; i < len; i += num)
			{
				num = inStr.Read(buf, off + i, len - i);
				if (num < 1)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000B985C File Offset: 0x000B7A5C
		public static void PipeAll(Stream inStr, Stream outStr)
		{
			byte[] array = new byte[512];
			int count;
			while ((count = inStr.Read(array, 0, array.Length)) > 0)
			{
				outStr.Write(array, 0, count);
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000B9890 File Offset: 0x000B7A90
		public static long PipeAllLimited(Stream inStr, long limit, Stream outStr)
		{
			byte[] array = new byte[512];
			long num = 0L;
			int num2;
			while ((num2 = inStr.Read(array, 0, array.Length)) > 0)
			{
				if (limit - num < (long)num2)
				{
					throw new StreamOverflowException("Data Overflow");
				}
				num += (long)num2;
				outStr.Write(array, 0, num2);
			}
			return num;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000B98DE File Offset: 0x000B7ADE
		public static void WriteBufTo(MemoryStream buf, Stream output)
		{
			buf.WriteTo(output);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000B98E8 File Offset: 0x000B7AE8
		public static int WriteBufTo(MemoryStream buf, byte[] output, int offset)
		{
			int num = (int)buf.Length;
			buf.WriteTo(new MemoryStream(output, offset, num, true));
			return num;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000B9910 File Offset: 0x000B7B10
		public static void WriteZeroes(Stream outStr, long count)
		{
			byte[] buffer = new byte[512];
			while (count > 512L)
			{
				outStr.Write(buffer, 0, 512);
				count -= 512L;
			}
			outStr.Write(buffer, 0, (int)count);
		}

		// Token: 0x040016CD RID: 5837
		private const int BufferSize = 512;
	}
}
