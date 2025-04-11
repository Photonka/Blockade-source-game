using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F9 RID: 1273
	public class X923Padding : IBlockCipherPadding
	{
		// Token: 0x0600309D RID: 12445 RVA: 0x0012A2EE File Offset: 0x001284EE
		public void Init(SecureRandom random)
		{
			this.random = random;
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0012A2F7 File Offset: 0x001284F7
		public string PaddingName
		{
			get
			{
				return "X9.23";
			}
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0012A300 File Offset: 0x00128500
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				if (this.random == null)
				{
					input[inOff] = 0;
				}
				else
				{
					input[inOff] = (byte)this.random.NextInt();
				}
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x00129DE7 File Offset: 0x00127FE7
		public int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1] & byte.MaxValue;
			if ((int)b > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return (int)b;
		}

		// Token: 0x04001F14 RID: 7956
		private SecureRandom random;
	}
}
