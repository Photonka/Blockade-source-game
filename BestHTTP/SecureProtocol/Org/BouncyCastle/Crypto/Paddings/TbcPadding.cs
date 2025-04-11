using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F8 RID: 1272
	public class TbcPadding : IBlockCipherPadding
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0012A25E File Offset: 0x0012845E
		public string PaddingName
		{
			get
			{
				return "TBC";
			}
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Init(SecureRandom random)
		{
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0012A268 File Offset: 0x00128468
		public virtual int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			byte b;
			if (inOff > 0)
			{
				b = (((input[inOff - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			else
			{
				b = (((input[input.Length - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return result;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0012A2BC File Offset: 0x001284BC
		public virtual int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1];
			int num = input.Length - 1;
			while (num > 0 && input[num - 1] == b)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
