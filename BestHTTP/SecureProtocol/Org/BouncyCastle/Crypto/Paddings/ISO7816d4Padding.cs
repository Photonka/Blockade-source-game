using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F5 RID: 1269
	public class ISO7816d4Padding : IBlockCipherPadding
	{
		// Token: 0x06003086 RID: 12422 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x00129E07 File Offset: 0x00128007
		public string PaddingName
		{
			get
			{
				return "ISO7816-4";
			}
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x00129E10 File Offset: 0x00128010
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			input[inOff] = 128;
			for (inOff++; inOff < input.Length; inOff++)
			{
				input[inOff] = 0;
			}
			return result;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x00129E44 File Offset: 0x00128044
		public int PadCount(byte[] input)
		{
			int num = input.Length - 1;
			while (num > 0 && input[num] == 0)
			{
				num--;
			}
			if (input[num] != 128)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return input.Length - num;
		}
	}
}
