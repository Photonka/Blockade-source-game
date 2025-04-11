using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200059B RID: 1435
	public class RipeMD128Digest : GeneralDigest
	{
		// Token: 0x06003732 RID: 14130 RVA: 0x0015AD8A File Offset: 0x00158F8A
		public RipeMD128Digest()
		{
			this.Reset();
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0015ADA5 File Offset: 0x00158FA5
		public RipeMD128Digest(RipeMD128Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0015ADC4 File Offset: 0x00158FC4
		private void CopyIn(RipeMD128Digest t)
		{
			base.CopyIn(t);
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x0015AE2F File Offset: 0x0015902F
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD128";
			}
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x0015AE38 File Offset: 0x00159038
		internal override void ProcessWord(byte[] input, int inOff)
		{
			int[] x = this.X;
			int num = this.xOff;
			this.xOff = num + 1;
			x[num] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0015AEA2 File Offset: 0x001590A2
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x001597CC File Offset: 0x001579CC
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x0015AED0 File Offset: 0x001590D0
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0015AF2C File Offset: 0x0015912C
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x00146642 File Offset: 0x00144842
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x001598C5 File Offset: 0x00157AC5
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x001598AE File Offset: 0x00157AAE
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0015AF8E File Offset: 0x0015918E
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0015AF96 File Offset: 0x00159196
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x0015AFA0 File Offset: 0x001591A0
		private int F1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0015AFB9 File Offset: 0x001591B9
		private int F2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1518500249, s);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0015AFD8 File Offset: 0x001591D8
		private int F3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1859775393, s);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0015AFF7 File Offset: 0x001591F7
		private int F4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + -1894007588, s);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0015AFA0 File Offset: 0x001591A0
		private int FF1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0015B016 File Offset: 0x00159216
		private int FF2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1836072691, s);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0015B035 File Offset: 0x00159235
		private int FF3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1548603684, s);
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x0015B054 File Offset: 0x00159254
		private int FF4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + 1352829926, s);
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0015B074 File Offset: 0x00159274
		internal override void ProcessBlock()
		{
			int num2;
			int num = num2 = this.H0;
			int num4;
			int num3 = num4 = this.H1;
			int num6;
			int num5 = num6 = this.H2;
			int num8;
			int num7 = num8 = this.H3;
			num2 = this.F1(num2, num4, num6, num8, this.X[0], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[1], 14);
			num6 = this.F1(num6, num8, num2, num4, this.X[2], 15);
			num4 = this.F1(num4, num6, num8, num2, this.X[3], 12);
			num2 = this.F1(num2, num4, num6, num8, this.X[4], 5);
			num8 = this.F1(num8, num2, num4, num6, this.X[5], 8);
			num6 = this.F1(num6, num8, num2, num4, this.X[6], 7);
			num4 = this.F1(num4, num6, num8, num2, this.X[7], 9);
			num2 = this.F1(num2, num4, num6, num8, this.X[8], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[9], 13);
			num6 = this.F1(num6, num8, num2, num4, this.X[10], 14);
			num4 = this.F1(num4, num6, num8, num2, this.X[11], 15);
			num2 = this.F1(num2, num4, num6, num8, this.X[12], 6);
			num8 = this.F1(num8, num2, num4, num6, this.X[13], 7);
			num6 = this.F1(num6, num8, num2, num4, this.X[14], 9);
			num4 = this.F1(num4, num6, num8, num2, this.X[15], 8);
			num2 = this.F2(num2, num4, num6, num8, this.X[7], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[4], 6);
			num6 = this.F2(num6, num8, num2, num4, this.X[13], 8);
			num4 = this.F2(num4, num6, num8, num2, this.X[1], 13);
			num2 = this.F2(num2, num4, num6, num8, this.X[10], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[6], 9);
			num6 = this.F2(num6, num8, num2, num4, this.X[15], 7);
			num4 = this.F2(num4, num6, num8, num2, this.X[3], 15);
			num2 = this.F2(num2, num4, num6, num8, this.X[12], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[0], 12);
			num6 = this.F2(num6, num8, num2, num4, this.X[9], 15);
			num4 = this.F2(num4, num6, num8, num2, this.X[5], 9);
			num2 = this.F2(num2, num4, num6, num8, this.X[2], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[14], 7);
			num6 = this.F2(num6, num8, num2, num4, this.X[11], 13);
			num4 = this.F2(num4, num6, num8, num2, this.X[8], 12);
			num2 = this.F3(num2, num4, num6, num8, this.X[3], 11);
			num8 = this.F3(num8, num2, num4, num6, this.X[10], 13);
			num6 = this.F3(num6, num8, num2, num4, this.X[14], 6);
			num4 = this.F3(num4, num6, num8, num2, this.X[4], 7);
			num2 = this.F3(num2, num4, num6, num8, this.X[9], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[15], 9);
			num6 = this.F3(num6, num8, num2, num4, this.X[8], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[1], 15);
			num2 = this.F3(num2, num4, num6, num8, this.X[2], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[7], 8);
			num6 = this.F3(num6, num8, num2, num4, this.X[0], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[6], 6);
			num2 = this.F3(num2, num4, num6, num8, this.X[13], 5);
			num8 = this.F3(num8, num2, num4, num6, this.X[11], 12);
			num6 = this.F3(num6, num8, num2, num4, this.X[5], 7);
			num4 = this.F3(num4, num6, num8, num2, this.X[12], 5);
			num2 = this.F4(num2, num4, num6, num8, this.X[1], 11);
			num8 = this.F4(num8, num2, num4, num6, this.X[9], 12);
			num6 = this.F4(num6, num8, num2, num4, this.X[11], 14);
			num4 = this.F4(num4, num6, num8, num2, this.X[10], 15);
			num2 = this.F4(num2, num4, num6, num8, this.X[0], 14);
			num8 = this.F4(num8, num2, num4, num6, this.X[8], 15);
			num6 = this.F4(num6, num8, num2, num4, this.X[12], 9);
			num4 = this.F4(num4, num6, num8, num2, this.X[4], 8);
			num2 = this.F4(num2, num4, num6, num8, this.X[13], 9);
			num8 = this.F4(num8, num2, num4, num6, this.X[3], 14);
			num6 = this.F4(num6, num8, num2, num4, this.X[7], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[15], 6);
			num2 = this.F4(num2, num4, num6, num8, this.X[14], 8);
			num8 = this.F4(num8, num2, num4, num6, this.X[5], 6);
			num6 = this.F4(num6, num8, num2, num4, this.X[6], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[2], 12);
			num = this.FF4(num, num3, num5, num7, this.X[5], 8);
			num7 = this.FF4(num7, num, num3, num5, this.X[14], 9);
			num5 = this.FF4(num5, num7, num, num3, this.X[7], 9);
			num3 = this.FF4(num3, num5, num7, num, this.X[0], 11);
			num = this.FF4(num, num3, num5, num7, this.X[9], 13);
			num7 = this.FF4(num7, num, num3, num5, this.X[2], 15);
			num5 = this.FF4(num5, num7, num, num3, this.X[11], 15);
			num3 = this.FF4(num3, num5, num7, num, this.X[4], 5);
			num = this.FF4(num, num3, num5, num7, this.X[13], 7);
			num7 = this.FF4(num7, num, num3, num5, this.X[6], 7);
			num5 = this.FF4(num5, num7, num, num3, this.X[15], 8);
			num3 = this.FF4(num3, num5, num7, num, this.X[8], 11);
			num = this.FF4(num, num3, num5, num7, this.X[1], 14);
			num7 = this.FF4(num7, num, num3, num5, this.X[10], 14);
			num5 = this.FF4(num5, num7, num, num3, this.X[3], 12);
			num3 = this.FF4(num3, num5, num7, num, this.X[12], 6);
			num = this.FF3(num, num3, num5, num7, this.X[6], 9);
			num7 = this.FF3(num7, num, num3, num5, this.X[11], 13);
			num5 = this.FF3(num5, num7, num, num3, this.X[3], 15);
			num3 = this.FF3(num3, num5, num7, num, this.X[7], 7);
			num = this.FF3(num, num3, num5, num7, this.X[0], 12);
			num7 = this.FF3(num7, num, num3, num5, this.X[13], 8);
			num5 = this.FF3(num5, num7, num, num3, this.X[5], 9);
			num3 = this.FF3(num3, num5, num7, num, this.X[10], 11);
			num = this.FF3(num, num3, num5, num7, this.X[14], 7);
			num7 = this.FF3(num7, num, num3, num5, this.X[15], 7);
			num5 = this.FF3(num5, num7, num, num3, this.X[8], 12);
			num3 = this.FF3(num3, num5, num7, num, this.X[12], 7);
			num = this.FF3(num, num3, num5, num7, this.X[4], 6);
			num7 = this.FF3(num7, num, num3, num5, this.X[9], 15);
			num5 = this.FF3(num5, num7, num, num3, this.X[1], 13);
			num3 = this.FF3(num3, num5, num7, num, this.X[2], 11);
			num = this.FF2(num, num3, num5, num7, this.X[15], 9);
			num7 = this.FF2(num7, num, num3, num5, this.X[5], 7);
			num5 = this.FF2(num5, num7, num, num3, this.X[1], 15);
			num3 = this.FF2(num3, num5, num7, num, this.X[3], 11);
			num = this.FF2(num, num3, num5, num7, this.X[7], 8);
			num7 = this.FF2(num7, num, num3, num5, this.X[14], 6);
			num5 = this.FF2(num5, num7, num, num3, this.X[6], 6);
			num3 = this.FF2(num3, num5, num7, num, this.X[9], 14);
			num = this.FF2(num, num3, num5, num7, this.X[11], 12);
			num7 = this.FF2(num7, num, num3, num5, this.X[8], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[12], 5);
			num3 = this.FF2(num3, num5, num7, num, this.X[2], 14);
			num = this.FF2(num, num3, num5, num7, this.X[10], 13);
			num7 = this.FF2(num7, num, num3, num5, this.X[0], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[4], 7);
			num3 = this.FF2(num3, num5, num7, num, this.X[13], 5);
			num = this.FF1(num, num3, num5, num7, this.X[8], 15);
			num7 = this.FF1(num7, num, num3, num5, this.X[6], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[4], 8);
			num3 = this.FF1(num3, num5, num7, num, this.X[1], 11);
			num = this.FF1(num, num3, num5, num7, this.X[3], 14);
			num7 = this.FF1(num7, num, num3, num5, this.X[11], 14);
			num5 = this.FF1(num5, num7, num, num3, this.X[15], 6);
			num3 = this.FF1(num3, num5, num7, num, this.X[0], 14);
			num = this.FF1(num, num3, num5, num7, this.X[5], 6);
			num7 = this.FF1(num7, num, num3, num5, this.X[12], 9);
			num5 = this.FF1(num5, num7, num, num3, this.X[2], 12);
			num3 = this.FF1(num3, num5, num7, num, this.X[13], 9);
			num = this.FF1(num, num3, num5, num7, this.X[9], 12);
			num7 = this.FF1(num7, num, num3, num5, this.X[7], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[10], 15);
			num3 = this.FF1(num3, num5, num7, num, this.X[14], 8);
			num7 += num6 + this.H1;
			this.H1 = this.H2 + num8 + num;
			this.H2 = this.H3 + num2 + num3;
			this.H3 = this.H0 + num4 + num5;
			this.H0 = num7;
			this.xOff = 0;
			for (int num9 = 0; num9 != this.X.Length; num9++)
			{
				this.X[num9] = 0;
			}
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x0015BCDF File Offset: 0x00159EDF
		public override IMemoable Copy()
		{
			return new RipeMD128Digest(this);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0015BCE8 File Offset: 0x00159EE8
		public override void Reset(IMemoable other)
		{
			RipeMD128Digest t = (RipeMD128Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x04002324 RID: 8996
		private const int DigestLength = 16;

		// Token: 0x04002325 RID: 8997
		private int H0;

		// Token: 0x04002326 RID: 8998
		private int H1;

		// Token: 0x04002327 RID: 8999
		private int H2;

		// Token: 0x04002328 RID: 9000
		private int H3;

		// Token: 0x04002329 RID: 9001
		private int[] X = new int[16];

		// Token: 0x0400232A RID: 9002
		private int xOff;
	}
}
