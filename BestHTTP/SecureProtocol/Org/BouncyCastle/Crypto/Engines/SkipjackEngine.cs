﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200057D RID: 1405
	public class SkipjackEngine : IBlockCipher
	{
		// Token: 0x0600358E RID: 13710 RVA: 0x0014D870 File Offset: 0x0014BA70
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to SKIPJACK init - " + Platform.GetTypeName(parameters));
			}
			byte[] key = ((KeyParameter)parameters).GetKey();
			this.encrypting = forEncryption;
			this.key0 = new int[32];
			this.key1 = new int[32];
			this.key2 = new int[32];
			this.key3 = new int[32];
			for (int i = 0; i < 32; i++)
			{
				this.key0[i] = (int)(key[i * 4 % 10] & byte.MaxValue);
				this.key1[i] = (int)(key[(i * 4 + 1) % 10] & byte.MaxValue);
				this.key2[i] = (int)(key[(i * 4 + 2) % 10] & byte.MaxValue);
				this.key3[i] = (int)(key[(i * 4 + 3) % 10] & byte.MaxValue);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x0014D94D File Offset: 0x0014BB4D
		public virtual string AlgorithmName
		{
			get
			{
				return "SKIPJACK";
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x0014D954 File Offset: 0x0014BB54
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.key1 == null)
			{
				throw new InvalidOperationException("SKIPJACK engine not initialised");
			}
			Check.DataLength(input, inOff, 8, "input buffer too short");
			Check.OutputLength(output, outOff, 8, "output buffer too short");
			if (this.encrypting)
			{
				this.EncryptBlock(input, inOff, output, outOff);
			}
			else
			{
				this.DecryptBlock(input, inOff, output, outOff);
			}
			return 8;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x0014D9B4 File Offset: 0x0014BBB4
		private int G(int k, int w)
		{
			int num = w >> 8 & 255;
			int num2 = w & 255;
			int num3 = (int)SkipjackEngine.ftable[num2 ^ this.key0[k]] ^ num;
			int num4 = (int)SkipjackEngine.ftable[num3 ^ this.key1[k]] ^ num2;
			int num5 = (int)SkipjackEngine.ftable[num4 ^ this.key2[k]] ^ num3;
			int num6 = (int)SkipjackEngine.ftable[num5 ^ this.key3[k]] ^ num4;
			return (num5 << 8) + num6;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0014DA2C File Offset: 0x0014BC2C
		public virtual int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = ((int)input[inOff] << 8) + (int)(input[inOff + 1] & byte.MaxValue);
			int num2 = ((int)input[inOff + 2] << 8) + (int)(input[inOff + 3] & byte.MaxValue);
			int num3 = ((int)input[inOff + 4] << 8) + (int)(input[inOff + 5] & byte.MaxValue);
			int num4 = ((int)input[inOff + 6] << 8) + (int)(input[inOff + 7] & byte.MaxValue);
			int num5 = 0;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num6 = num4;
					num4 = num3;
					num3 = num2;
					num2 = this.G(num5, num);
					num = (num2 ^ num6 ^ num5 + 1);
					num5++;
				}
				for (int k = 0; k < 8; k++)
				{
					int num7 = num4;
					num4 = num3;
					num3 = (num ^ num2 ^ num5 + 1);
					num2 = this.G(num5, num);
					num = num7;
					num5++;
				}
			}
			outBytes[outOff] = (byte)(num >> 8);
			outBytes[outOff + 1] = (byte)num;
			outBytes[outOff + 2] = (byte)(num2 >> 8);
			outBytes[outOff + 3] = (byte)num2;
			outBytes[outOff + 4] = (byte)(num3 >> 8);
			outBytes[outOff + 5] = (byte)num3;
			outBytes[outOff + 6] = (byte)(num4 >> 8);
			outBytes[outOff + 7] = (byte)num4;
			return 8;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x0014DB40 File Offset: 0x0014BD40
		private int H(int k, int w)
		{
			int num = w & 255;
			int num2 = w >> 8 & 255;
			int num3 = (int)SkipjackEngine.ftable[num2 ^ this.key3[k]] ^ num;
			int num4 = (int)SkipjackEngine.ftable[num3 ^ this.key2[k]] ^ num2;
			int num5 = (int)SkipjackEngine.ftable[num4 ^ this.key1[k]] ^ num3;
			return (((int)SkipjackEngine.ftable[num5 ^ this.key0[k]] ^ num4) << 8) + num5;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0014DBB4 File Offset: 0x0014BDB4
		public virtual int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = ((int)input[inOff] << 8) + (int)(input[inOff + 1] & byte.MaxValue);
			int num2 = ((int)input[inOff + 2] << 8) + (int)(input[inOff + 3] & byte.MaxValue);
			int num3 = ((int)input[inOff + 4] << 8) + (int)(input[inOff + 5] & byte.MaxValue);
			int num4 = ((int)input[inOff + 6] << 8) + (int)(input[inOff + 7] & byte.MaxValue);
			int num5 = 31;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num6 = num3;
					num3 = num4;
					num4 = num;
					num = this.H(num5, num2);
					num2 = (num ^ num6 ^ num5 + 1);
					num5--;
				}
				for (int k = 0; k < 8; k++)
				{
					int num7 = num3;
					num3 = num4;
					num4 = (num2 ^ num ^ num5 + 1);
					num = this.H(num5, num2);
					num2 = num7;
					num5--;
				}
			}
			outBytes[outOff] = (byte)(num >> 8);
			outBytes[outOff + 1] = (byte)num;
			outBytes[outOff + 2] = (byte)(num2 >> 8);
			outBytes[outOff + 3] = (byte)num2;
			outBytes[outOff + 4] = (byte)(num3 >> 8);
			outBytes[outOff + 5] = (byte)num3;
			outBytes[outOff + 6] = (byte)(num4 >> 8);
			outBytes[outOff + 7] = (byte)num4;
			return 8;
		}

		// Token: 0x040021F7 RID: 8695
		private const int BLOCK_SIZE = 8;

		// Token: 0x040021F8 RID: 8696
		private static readonly short[] ftable = new short[]
		{
			163,
			215,
			9,
			131,
			248,
			72,
			246,
			244,
			179,
			33,
			21,
			120,
			153,
			177,
			175,
			249,
			231,
			45,
			77,
			138,
			206,
			76,
			202,
			46,
			82,
			149,
			217,
			30,
			78,
			56,
			68,
			40,
			10,
			223,
			2,
			160,
			23,
			241,
			96,
			104,
			18,
			183,
			122,
			195,
			233,
			250,
			61,
			83,
			150,
			132,
			107,
			186,
			242,
			99,
			154,
			25,
			124,
			174,
			229,
			245,
			247,
			22,
			106,
			162,
			57,
			182,
			123,
			15,
			193,
			147,
			129,
			27,
			238,
			180,
			26,
			234,
			208,
			145,
			47,
			184,
			85,
			185,
			218,
			133,
			63,
			65,
			191,
			224,
			90,
			88,
			128,
			95,
			102,
			11,
			216,
			144,
			53,
			213,
			192,
			167,
			51,
			6,
			101,
			105,
			69,
			0,
			148,
			86,
			109,
			152,
			155,
			118,
			151,
			252,
			178,
			194,
			176,
			254,
			219,
			32,
			225,
			235,
			214,
			228,
			221,
			71,
			74,
			29,
			66,
			237,
			158,
			110,
			73,
			60,
			205,
			67,
			39,
			210,
			7,
			212,
			222,
			199,
			103,
			24,
			137,
			203,
			48,
			31,
			141,
			198,
			143,
			170,
			200,
			116,
			220,
			201,
			93,
			92,
			49,
			164,
			112,
			136,
			97,
			44,
			159,
			13,
			43,
			135,
			80,
			130,
			84,
			100,
			38,
			125,
			3,
			64,
			52,
			75,
			28,
			115,
			209,
			196,
			253,
			59,
			204,
			251,
			127,
			171,
			230,
			62,
			91,
			165,
			173,
			4,
			35,
			156,
			20,
			81,
			34,
			240,
			41,
			121,
			113,
			126,
			255,
			140,
			14,
			226,
			12,
			239,
			188,
			114,
			117,
			111,
			55,
			161,
			236,
			211,
			142,
			98,
			139,
			134,
			16,
			232,
			8,
			119,
			17,
			190,
			146,
			79,
			36,
			197,
			50,
			54,
			157,
			207,
			243,
			166,
			187,
			172,
			94,
			108,
			169,
			19,
			87,
			37,
			181,
			227,
			189,
			168,
			58,
			1,
			5,
			89,
			42,
			70
		};

		// Token: 0x040021F9 RID: 8697
		private int[] key0;

		// Token: 0x040021FA RID: 8698
		private int[] key1;

		// Token: 0x040021FB RID: 8699
		private int[] key2;

		// Token: 0x040021FC RID: 8700
		private int[] key3;

		// Token: 0x040021FD RID: 8701
		private bool encrypting;
	}
}
