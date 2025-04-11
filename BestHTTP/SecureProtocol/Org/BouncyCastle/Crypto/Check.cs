using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B4 RID: 948
	internal class Check
	{
		// Token: 0x06002797 RID: 10135 RVA: 0x0010E15B File Offset: 0x0010C35B
		internal static void DataLength(bool condition, string msg)
		{
			if (condition)
			{
				throw new DataLengthException(msg);
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0010E167 File Offset: 0x0010C367
		internal static void DataLength(byte[] buf, int off, int len, string msg)
		{
			if (off + len > buf.Length)
			{
				throw new DataLengthException(msg);
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x0010E178 File Offset: 0x0010C378
		internal static void OutputLength(byte[] buf, int off, int len, string msg)
		{
			if (off + len > buf.Length)
			{
				throw new OutputLengthException(msg);
			}
		}
	}
}
