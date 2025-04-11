using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005AA RID: 1450
	public class SM3Digest : GeneralDigest
	{
		// Token: 0x06003819 RID: 14361 RVA: 0x00163AB8 File Offset: 0x00161CB8
		static SM3Digest()
		{
			for (int i = 0; i < 16; i++)
			{
				uint num = 2043430169U;
				SM3Digest.T[i] = (num << i | num >> 32 - i);
			}
			for (int j = 16; j < 64; j++)
			{
				int num2 = j % 32;
				uint num3 = 2055708042U;
				SM3Digest.T[j] = (num3 << num2 | num3 >> 32 - num2);
			}
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00163B2E File Offset: 0x00161D2E
		public SM3Digest()
		{
			this.Reset();
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x00163B62 File Offset: 0x00161D62
		public SM3Digest(SM3Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x00163B98 File Offset: 0x00161D98
		private void CopyIn(SM3Digest t)
		{
			Array.Copy(t.V, 0, this.V, 0, this.V.Length);
			Array.Copy(t.inwords, 0, this.inwords, 0, this.inwords.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x00163BE7 File Offset: 0x00161DE7
		public override string AlgorithmName
		{
			get
			{
				return "SM3";
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x001565CC File Offset: 0x001547CC
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x00163BEE File Offset: 0x00161DEE
		public override IMemoable Copy()
		{
			return new SM3Digest(this);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x00163BF8 File Offset: 0x00161DF8
		public override void Reset(IMemoable other)
		{
			SM3Digest t = (SM3Digest)other;
			base.CopyIn(t);
			this.CopyIn(t);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x00163C1C File Offset: 0x00161E1C
		public override void Reset()
		{
			base.Reset();
			this.V[0] = 1937774191U;
			this.V[1] = 1226093241U;
			this.V[2] = 388252375U;
			this.V[3] = 3666478592U;
			this.V[4] = 2842636476U;
			this.V[5] = 372324522U;
			this.V[6] = 3817729613U;
			this.V[7] = 2969243214U;
			this.xOff = 0;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x00163C9E File Offset: 0x00161E9E
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.V, output, outOff);
			this.Reset();
			return 32;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x00163CBC File Offset: 0x00161EBC
		internal override void ProcessWord(byte[] input, int inOff)
		{
			uint num = Pack.BE_To_UInt32(input, inOff);
			this.inwords[this.xOff] = num;
			this.xOff++;
			if (this.xOff >= 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x00163D00 File Offset: 0x00161F00
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.inwords[this.xOff] = 0U;
				this.xOff++;
				this.ProcessBlock();
			}
			while (this.xOff < 14)
			{
				this.inwords[this.xOff] = 0U;
				this.xOff++;
			}
			uint[] array = this.inwords;
			int num = this.xOff;
			this.xOff = num + 1;
			array[num] = (uint)(bitLength >> 32);
			uint[] array2 = this.inwords;
			num = this.xOff;
			this.xOff = num + 1;
			array2[num] = (uint)bitLength;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00163D98 File Offset: 0x00161F98
		private uint P0(uint x)
		{
			uint num = x << 9 | x >> 23;
			uint num2 = x << 17 | x >> 15;
			return x ^ num ^ num2;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x00163DC0 File Offset: 0x00161FC0
		private uint P1(uint x)
		{
			uint num = x << 15 | x >> 17;
			uint num2 = x << 23 | x >> 9;
			return x ^ num ^ num2;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x001598C5 File Offset: 0x00157AC5
		private uint FF0(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x001598B8 File Offset: 0x00157AB8
		private uint FF1(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x001598C5 File Offset: 0x00157AC5
		private uint GG0(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x001598AE File Offset: 0x00157AAE
		private uint GG1(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00163DE8 File Offset: 0x00161FE8
		internal override void ProcessBlock()
		{
			for (int i = 0; i < 16; i++)
			{
				this.W[i] = this.inwords[i];
			}
			for (int j = 16; j < 68; j++)
			{
				uint num = this.W[j - 3];
				uint num2 = num << 15 | num >> 17;
				uint num3 = this.W[j - 13];
				uint num4 = num3 << 7 | num3 >> 25;
				this.W[j] = (this.P1(this.W[j - 16] ^ this.W[j - 9] ^ num2) ^ num4 ^ this.W[j - 6]);
			}
			uint num5 = this.V[0];
			uint num6 = this.V[1];
			uint num7 = this.V[2];
			uint num8 = this.V[3];
			uint num9 = this.V[4];
			uint num10 = this.V[5];
			uint num11 = this.V[6];
			uint num12 = this.V[7];
			for (int k = 0; k < 16; k++)
			{
				uint num13 = num5 << 12 | num5 >> 20;
				uint num14 = num13 + num9 + SM3Digest.T[k];
				uint num15 = num14 << 7 | num14 >> 25;
				uint num16 = num15 ^ num13;
				uint num17 = this.W[k];
				uint num18 = num17 ^ this.W[k + 4];
				uint num19 = this.FF0(num5, num6, num7) + num8 + num16 + num18;
				uint x = this.GG0(num9, num10, num11) + num12 + num15 + num17;
				num8 = num7;
				num7 = (num6 << 9 | num6 >> 23);
				num6 = num5;
				num5 = num19;
				num12 = num11;
				num11 = (num10 << 19 | num10 >> 13);
				num10 = num9;
				num9 = this.P0(x);
			}
			for (int l = 16; l < 64; l++)
			{
				uint num20 = num5 << 12 | num5 >> 20;
				uint num21 = num20 + num9 + SM3Digest.T[l];
				uint num22 = num21 << 7 | num21 >> 25;
				uint num23 = num22 ^ num20;
				uint num24 = this.W[l];
				uint num25 = num24 ^ this.W[l + 4];
				uint num26 = this.FF1(num5, num6, num7) + num8 + num23 + num25;
				uint x2 = this.GG1(num9, num10, num11) + num12 + num22 + num24;
				num8 = num7;
				num7 = (num6 << 9 | num6 >> 23);
				num6 = num5;
				num5 = num26;
				num12 = num11;
				num11 = (num10 << 19 | num10 >> 13);
				num10 = num9;
				num9 = this.P0(x2);
			}
			this.V[0] ^= num5;
			this.V[1] ^= num6;
			this.V[2] ^= num7;
			this.V[3] ^= num8;
			this.V[4] ^= num9;
			this.V[5] ^= num10;
			this.V[6] ^= num11;
			this.V[7] ^= num12;
			this.xOff = 0;
		}

		// Token: 0x04002392 RID: 9106
		private const int DIGEST_LENGTH = 32;

		// Token: 0x04002393 RID: 9107
		private const int BLOCK_SIZE = 16;

		// Token: 0x04002394 RID: 9108
		private uint[] V = new uint[8];

		// Token: 0x04002395 RID: 9109
		private uint[] inwords = new uint[16];

		// Token: 0x04002396 RID: 9110
		private int xOff;

		// Token: 0x04002397 RID: 9111
		private uint[] W = new uint[68];

		// Token: 0x04002398 RID: 9112
		private static readonly uint[] T = new uint[64];
	}
}
