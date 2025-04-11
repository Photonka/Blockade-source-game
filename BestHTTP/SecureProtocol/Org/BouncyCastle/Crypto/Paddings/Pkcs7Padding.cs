using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F7 RID: 1271
	public class Pkcs7Padding : IBlockCipherPadding
	{
		// Token: 0x06003093 RID: 12435 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x0012A1E0 File Offset: 0x001283E0
		public string PaddingName
		{
			get
			{
				return "PKCS7";
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0012A1E8 File Offset: 0x001283E8
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return (int)b;
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0012A210 File Offset: 0x00128410
		public int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1];
			int num = (int)b;
			if (num < 1 || num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			for (int i = 2; i <= num; i++)
			{
				if (input[input.Length - i] != b)
				{
					throw new InvalidCipherTextException("pad block corrupted");
				}
			}
			return num;
		}
	}
}
