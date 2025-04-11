using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F4 RID: 1268
	public class ISO10126d2Padding : IBlockCipherPadding
	{
		// Token: 0x06003081 RID: 12417 RVA: 0x00129D94 File Offset: 0x00127F94
		public void Init(SecureRandom random)
		{
			this.random = ((random != null) ? random : new SecureRandom());
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x00129DA7 File Offset: 0x00127FA7
		public string PaddingName
		{
			get
			{
				return "ISO10126-2";
			}
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00129DB0 File Offset: 0x00127FB0
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				input[inOff] = (byte)this.random.NextInt();
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x00129DE7 File Offset: 0x00127FE7
		public int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1] & byte.MaxValue;
			if ((int)b > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return (int)b;
		}

		// Token: 0x04001F12 RID: 7954
		private SecureRandom random;
	}
}
