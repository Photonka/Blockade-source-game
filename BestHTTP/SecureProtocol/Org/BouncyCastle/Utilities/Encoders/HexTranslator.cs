using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000278 RID: 632
	public class HexTranslator : ITranslator
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public int GetEncodedBlockSize()
		{
			return 2;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000BAC84 File Offset: 0x000B8E84
		public int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int i = 0;
			int num = 0;
			while (i < length)
			{
				outBytes[outOff + num] = HexTranslator.hexTable[input[inOff] >> 4 & 15];
				outBytes[outOff + num + 1] = HexTranslator.hexTable[(int)(input[inOff] & 15)];
				inOff++;
				i++;
				num += 2;
			}
			return length * 2;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0006CF70 File Offset: 0x0006B170
		public int GetDecodedBlockSize()
		{
			return 1;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000BACD8 File Offset: 0x000B8ED8
		public int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int num = length / 2;
			for (int i = 0; i < num; i++)
			{
				byte b = input[inOff + i * 2];
				byte b2 = input[inOff + i * 2 + 1];
				if (b < 97)
				{
					outBytes[outOff] = (byte)(b - 48 << 4);
				}
				else
				{
					outBytes[outOff] = (byte)(b - 97 + 10 << 4);
				}
				if (b2 < 97)
				{
					int num2 = outOff;
					outBytes[num2] += b2 - 48;
				}
				else
				{
					int num3 = outOff;
					outBytes[num3] += b2 - 97 + 10;
				}
				outOff++;
			}
			return num;
		}

		// Token: 0x040016EA RID: 5866
		private static readonly byte[] hexTable = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};
	}
}
