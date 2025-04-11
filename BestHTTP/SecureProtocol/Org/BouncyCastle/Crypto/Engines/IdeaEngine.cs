using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000565 RID: 1381
	public class IdeaEngine : IBlockCipher
	{
		// Token: 0x06003474 RID: 13428 RVA: 0x00144C42 File Offset: 0x00142E42
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to IDEA init - " + Platform.GetTypeName(parameters));
			}
			this.workingKey = this.GenerateWorkingKey(forEncryption, ((KeyParameter)parameters).GetKey());
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x00144C7A File Offset: 0x00142E7A
		public virtual string AlgorithmName
		{
			get
			{
				return "IDEA";
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00144C84 File Offset: 0x00142E84
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey == null)
			{
				throw new InvalidOperationException("IDEA engine not initialised");
			}
			Check.DataLength(input, inOff, 8, "input buffer too short");
			Check.OutputLength(output, outOff, 8, "output buffer too short");
			this.IdeaFunc(this.workingKey, input, inOff, output, outOff);
			return 8;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00144CD1 File Offset: 0x00142ED1
		private int BytesToWord(byte[] input, int inOff)
		{
			return ((int)input[inOff] << 8 & 65280) + (int)(input[inOff + 1] & byte.MaxValue);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00144CEA File Offset: 0x00142EEA
		private void WordToBytes(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)((uint)word >> 8);
			outBytes[outOff + 1] = (byte)word;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00144CFC File Offset: 0x00142EFC
		private int Mul(int x, int y)
		{
			if (x == 0)
			{
				x = IdeaEngine.BASE - y;
			}
			else if (y == 0)
			{
				x = IdeaEngine.BASE - x;
			}
			else
			{
				int num = x * y;
				y = (num & IdeaEngine.MASK);
				x = (int)((uint)num >> 16);
				x = y - x + ((y < x) ? 1 : 0);
			}
			return x & IdeaEngine.MASK;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00144D4C File Offset: 0x00142F4C
		private void IdeaFunc(int[] workingKey, byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = 0;
			int num2 = this.BytesToWord(input, inOff);
			int num3 = this.BytesToWord(input, inOff + 2);
			int num4 = this.BytesToWord(input, inOff + 4);
			int num5 = this.BytesToWord(input, inOff + 6);
			for (int i = 0; i < 8; i++)
			{
				num2 = this.Mul(num2, workingKey[num++]);
				num3 += workingKey[num++];
				num3 &= IdeaEngine.MASK;
				num4 += workingKey[num++];
				num4 &= IdeaEngine.MASK;
				num5 = this.Mul(num5, workingKey[num++]);
				int num6 = num3;
				int num7 = num4;
				num4 ^= num2;
				num3 ^= num5;
				num4 = this.Mul(num4, workingKey[num++]);
				num3 += num4;
				num3 &= IdeaEngine.MASK;
				num3 = this.Mul(num3, workingKey[num++]);
				num4 += num3;
				num4 &= IdeaEngine.MASK;
				num2 ^= num3;
				num5 ^= num4;
				num3 ^= num7;
				num4 ^= num6;
			}
			this.WordToBytes(this.Mul(num2, workingKey[num++]), outBytes, outOff);
			this.WordToBytes(num4 + workingKey[num++], outBytes, outOff + 2);
			this.WordToBytes(num3 + workingKey[num++], outBytes, outOff + 4);
			this.WordToBytes(this.Mul(num5, workingKey[num]), outBytes, outOff + 6);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x00144EA0 File Offset: 0x001430A0
		private int[] ExpandKey(byte[] uKey)
		{
			int[] array = new int[52];
			if (uKey.Length < 16)
			{
				byte[] array2 = new byte[16];
				Array.Copy(uKey, 0, array2, array2.Length - uKey.Length, uKey.Length);
				uKey = array2;
			}
			for (int i = 0; i < 8; i++)
			{
				array[i] = this.BytesToWord(uKey, i * 2);
			}
			for (int j = 8; j < 52; j++)
			{
				if ((j & 7) < 6)
				{
					array[j] = (((array[j - 7] & 127) << 9 | array[j - 6] >> 7) & IdeaEngine.MASK);
				}
				else if ((j & 7) == 6)
				{
					array[j] = (((array[j - 7] & 127) << 9 | array[j - 14] >> 7) & IdeaEngine.MASK);
				}
				else
				{
					array[j] = (((array[j - 15] & 127) << 9 | array[j - 14] >> 7) & IdeaEngine.MASK);
				}
			}
			return array;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00144F68 File Offset: 0x00143168
		private int MulInv(int x)
		{
			if (x < 2)
			{
				return x;
			}
			int num = 1;
			int num2 = IdeaEngine.BASE / x;
			int num3 = IdeaEngine.BASE % x;
			while (num3 != 1)
			{
				int num4 = x / num3;
				x %= num3;
				num = (num + num2 * num4 & IdeaEngine.MASK);
				if (x == 1)
				{
					return num;
				}
				num4 = num3 / x;
				num3 %= x;
				num2 = (num2 + num * num4 & IdeaEngine.MASK);
			}
			return 1 - num2 & IdeaEngine.MASK;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00144FCB File Offset: 0x001431CB
		private int AddInv(int x)
		{
			return 0 - x & IdeaEngine.MASK;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00144FD8 File Offset: 0x001431D8
		private int[] InvertKey(int[] inKey)
		{
			int num = 52;
			int[] array = new int[52];
			int num2 = 0;
			int num3 = this.MulInv(inKey[num2++]);
			int num4 = this.AddInv(inKey[num2++]);
			int num5 = this.AddInv(inKey[num2++]);
			int num6 = this.MulInv(inKey[num2++]);
			array[--num] = num6;
			array[--num] = num5;
			array[--num] = num4;
			array[--num] = num3;
			for (int i = 1; i < 8; i++)
			{
				num3 = inKey[num2++];
				num4 = inKey[num2++];
				array[--num] = num4;
				array[--num] = num3;
				num3 = this.MulInv(inKey[num2++]);
				num4 = this.AddInv(inKey[num2++]);
				num5 = this.AddInv(inKey[num2++]);
				num6 = this.MulInv(inKey[num2++]);
				array[--num] = num6;
				array[--num] = num4;
				array[--num] = num5;
				array[--num] = num3;
			}
			num3 = inKey[num2++];
			num4 = inKey[num2++];
			array[--num] = num4;
			array[--num] = num3;
			num3 = this.MulInv(inKey[num2++]);
			num4 = this.AddInv(inKey[num2++]);
			num5 = this.AddInv(inKey[num2++]);
			num6 = this.MulInv(inKey[num2]);
			array[--num] = num6;
			array[--num] = num5;
			array[--num] = num4;
			array[num - 1] = num3;
			return array;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x001451A0 File Offset: 0x001433A0
		private int[] GenerateWorkingKey(bool forEncryption, byte[] userKey)
		{
			if (forEncryption)
			{
				return this.ExpandKey(userKey);
			}
			return this.InvertKey(this.ExpandKey(userKey));
		}

		// Token: 0x04002165 RID: 8549
		private const int BLOCK_SIZE = 8;

		// Token: 0x04002166 RID: 8550
		private int[] workingKey;

		// Token: 0x04002167 RID: 8551
		private static readonly int MASK = 65535;

		// Token: 0x04002168 RID: 8552
		private static readonly int BASE = 65537;
	}
}
