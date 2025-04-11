using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004FA RID: 1274
	public class ZeroBytePadding : IBlockCipherPadding
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x0012A345 File Offset: 0x00128545
		public string PaddingName
		{
			get
			{
				return "ZeroBytePadding";
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0012A34C File Offset: 0x0012854C
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			while (inOff < input.Length)
			{
				input[inOff] = 0;
				inOff++;
			}
			return result;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0012A374 File Offset: 0x00128574
		public int PadCount(byte[] input)
		{
			int num = input.Length;
			while (num > 0 && input[num - 1] == 0)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
