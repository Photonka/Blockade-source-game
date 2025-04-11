using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200059C RID: 1436
	public class RipeMD160Digest : GeneralDigest
	{
		// Token: 0x0600374C RID: 14156 RVA: 0x0015BD03 File Offset: 0x00159F03
		public RipeMD160Digest()
		{
			this.Reset();
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0015BD1E File Offset: 0x00159F1E
		public RipeMD160Digest(RipeMD160Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x0015BD3C File Offset: 0x00159F3C
		private void CopyIn(RipeMD160Digest t)
		{
			base.CopyIn(t);
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x0015BDB3 File Offset: 0x00159FB3
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD160";
			}
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x0013379E File Offset: 0x0013199E
		public override int GetDigestSize()
		{
			return 20;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x0015BDBC File Offset: 0x00159FBC
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

		// Token: 0x06003752 RID: 14162 RVA: 0x0015BE26 File Offset: 0x0015A026
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x001597CC File Offset: 0x001579CC
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0015BE54 File Offset: 0x0015A054
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.UnpackWord(this.H4, output, outOff + 16);
			this.Reset();
			return 20;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0015BEC0 File Offset: 0x0015A0C0
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.H4 = -1009589776;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x00146642 File Offset: 0x00144842
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x001598C5 File Offset: 0x00157AC5
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x001598AE File Offset: 0x00157AAE
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0015AF8E File Offset: 0x0015918E
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x0015AF96 File Offset: 0x00159196
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x0015BF2D File Offset: 0x0015A12D
		private int F5(int x, int y, int z)
		{
			return x ^ (y | ~z);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x0015BF38 File Offset: 0x0015A138
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
			int num10;
			int num9 = num10 = this.H4;
			num2 = this.RL(num2 + this.F1(num4, num6, num8) + this.X[0], 11) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F1(num2, num4, num6) + this.X[1], 14) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F1(num10, num2, num4) + this.X[2], 15) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F1(num8, num10, num2) + this.X[3], 12) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F1(num6, num8, num10) + this.X[4], 5) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F1(num4, num6, num8) + this.X[5], 8) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F1(num2, num4, num6) + this.X[6], 7) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F1(num10, num2, num4) + this.X[7], 9) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F1(num8, num10, num2) + this.X[8], 11) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F1(num6, num8, num10) + this.X[9], 13) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F1(num4, num6, num8) + this.X[10], 14) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F1(num2, num4, num6) + this.X[11], 15) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F1(num10, num2, num4) + this.X[12], 6) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F1(num8, num10, num2) + this.X[13], 7) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F1(num6, num8, num10) + this.X[14], 9) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F1(num4, num6, num8) + this.X[15], 8) + num10;
			num6 = this.RL(num6, 10);
			num = this.RL(num + this.F5(num3, num5, num7) + this.X[5] + 1352829926, 8) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F5(num, num3, num5) + this.X[14] + 1352829926, 9) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F5(num9, num, num3) + this.X[7] + 1352829926, 9) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F5(num7, num9, num) + this.X[0] + 1352829926, 11) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F5(num5, num7, num9) + this.X[9] + 1352829926, 13) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F5(num3, num5, num7) + this.X[2] + 1352829926, 15) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F5(num, num3, num5) + this.X[11] + 1352829926, 15) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F5(num9, num, num3) + this.X[4] + 1352829926, 5) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F5(num7, num9, num) + this.X[13] + 1352829926, 7) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F5(num5, num7, num9) + this.X[6] + 1352829926, 7) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F5(num3, num5, num7) + this.X[15] + 1352829926, 8) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F5(num, num3, num5) + this.X[8] + 1352829926, 11) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F5(num9, num, num3) + this.X[1] + 1352829926, 14) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F5(num7, num9, num) + this.X[10] + 1352829926, 14) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F5(num5, num7, num9) + this.X[3] + 1352829926, 12) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F5(num3, num5, num7) + this.X[12] + 1352829926, 6) + num9;
			num5 = this.RL(num5, 10);
			num10 = this.RL(num10 + this.F2(num2, num4, num6) + this.X[7] + 1518500249, 7) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F2(num10, num2, num4) + this.X[4] + 1518500249, 6) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F2(num8, num10, num2) + this.X[13] + 1518500249, 8) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F2(num6, num8, num10) + this.X[1] + 1518500249, 13) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F2(num4, num6, num8) + this.X[10] + 1518500249, 11) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F2(num2, num4, num6) + this.X[6] + 1518500249, 9) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F2(num10, num2, num4) + this.X[15] + 1518500249, 7) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F2(num8, num10, num2) + this.X[3] + 1518500249, 15) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F2(num6, num8, num10) + this.X[12] + 1518500249, 7) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F2(num4, num6, num8) + this.X[0] + 1518500249, 12) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F2(num2, num4, num6) + this.X[9] + 1518500249, 15) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F2(num10, num2, num4) + this.X[5] + 1518500249, 9) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F2(num8, num10, num2) + this.X[2] + 1518500249, 11) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F2(num6, num8, num10) + this.X[14] + 1518500249, 7) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F2(num4, num6, num8) + this.X[11] + 1518500249, 13) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F2(num2, num4, num6) + this.X[8] + 1518500249, 12) + num8;
			num4 = this.RL(num4, 10);
			num9 = this.RL(num9 + this.F4(num, num3, num5) + this.X[6] + 1548603684, 9) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F4(num9, num, num3) + this.X[11] + 1548603684, 13) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F4(num7, num9, num) + this.X[3] + 1548603684, 15) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F4(num5, num7, num9) + this.X[7] + 1548603684, 7) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F4(num3, num5, num7) + this.X[0] + 1548603684, 12) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F4(num, num3, num5) + this.X[13] + 1548603684, 8) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F4(num9, num, num3) + this.X[5] + 1548603684, 9) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F4(num7, num9, num) + this.X[10] + 1548603684, 11) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F4(num5, num7, num9) + this.X[14] + 1548603684, 7) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F4(num3, num5, num7) + this.X[15] + 1548603684, 7) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F4(num, num3, num5) + this.X[8] + 1548603684, 12) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F4(num9, num, num3) + this.X[12] + 1548603684, 7) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F4(num7, num9, num) + this.X[4] + 1548603684, 6) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F4(num5, num7, num9) + this.X[9] + 1548603684, 15) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F4(num3, num5, num7) + this.X[1] + 1548603684, 13) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F4(num, num3, num5) + this.X[2] + 1548603684, 11) + num7;
			num3 = this.RL(num3, 10);
			num8 = this.RL(num8 + this.F3(num10, num2, num4) + this.X[3] + 1859775393, 11) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F3(num8, num10, num2) + this.X[10] + 1859775393, 13) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F3(num6, num8, num10) + this.X[14] + 1859775393, 6) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F3(num4, num6, num8) + this.X[4] + 1859775393, 7) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F3(num2, num4, num6) + this.X[9] + 1859775393, 14) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F3(num10, num2, num4) + this.X[15] + 1859775393, 9) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F3(num8, num10, num2) + this.X[8] + 1859775393, 13) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F3(num6, num8, num10) + this.X[1] + 1859775393, 15) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F3(num4, num6, num8) + this.X[2] + 1859775393, 14) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F3(num2, num4, num6) + this.X[7] + 1859775393, 8) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F3(num10, num2, num4) + this.X[0] + 1859775393, 13) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F3(num8, num10, num2) + this.X[6] + 1859775393, 6) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F3(num6, num8, num10) + this.X[13] + 1859775393, 5) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F3(num4, num6, num8) + this.X[11] + 1859775393, 12) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F3(num2, num4, num6) + this.X[5] + 1859775393, 7) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F3(num10, num2, num4) + this.X[12] + 1859775393, 5) + num6;
			num2 = this.RL(num2, 10);
			num7 = this.RL(num7 + this.F3(num9, num, num3) + this.X[15] + 1836072691, 9) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F3(num7, num9, num) + this.X[5] + 1836072691, 7) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F3(num5, num7, num9) + this.X[1] + 1836072691, 15) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F3(num3, num5, num7) + this.X[3] + 1836072691, 11) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F3(num, num3, num5) + this.X[7] + 1836072691, 8) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F3(num9, num, num3) + this.X[14] + 1836072691, 6) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F3(num7, num9, num) + this.X[6] + 1836072691, 6) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F3(num5, num7, num9) + this.X[9] + 1836072691, 14) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F3(num3, num5, num7) + this.X[11] + 1836072691, 12) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F3(num, num3, num5) + this.X[8] + 1836072691, 13) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F3(num9, num, num3) + this.X[12] + 1836072691, 5) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F3(num7, num9, num) + this.X[2] + 1836072691, 14) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F3(num5, num7, num9) + this.X[10] + 1836072691, 13) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F3(num3, num5, num7) + this.X[0] + 1836072691, 13) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F3(num, num3, num5) + this.X[4] + 1836072691, 7) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F3(num9, num, num3) + this.X[13] + 1836072691, 5) + num5;
			num = this.RL(num, 10);
			num6 = this.RL(num6 + this.F4(num8, num10, num2) + this.X[1] + -1894007588, 11) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F4(num6, num8, num10) + this.X[9] + -1894007588, 12) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F4(num4, num6, num8) + this.X[11] + -1894007588, 14) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F4(num2, num4, num6) + this.X[10] + -1894007588, 15) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F4(num10, num2, num4) + this.X[0] + -1894007588, 14) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F4(num8, num10, num2) + this.X[8] + -1894007588, 15) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F4(num6, num8, num10) + this.X[12] + -1894007588, 9) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F4(num4, num6, num8) + this.X[4] + -1894007588, 8) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F4(num2, num4, num6) + this.X[13] + -1894007588, 9) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F4(num10, num2, num4) + this.X[3] + -1894007588, 14) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F4(num8, num10, num2) + this.X[7] + -1894007588, 5) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F4(num6, num8, num10) + this.X[15] + -1894007588, 6) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F4(num4, num6, num8) + this.X[14] + -1894007588, 8) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F4(num2, num4, num6) + this.X[5] + -1894007588, 6) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F4(num10, num2, num4) + this.X[6] + -1894007588, 5) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F4(num8, num10, num2) + this.X[2] + -1894007588, 12) + num4;
			num10 = this.RL(num10, 10);
			num5 = this.RL(num5 + this.F2(num7, num9, num) + this.X[8] + 2053994217, 15) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F2(num5, num7, num9) + this.X[6] + 2053994217, 5) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F2(num3, num5, num7) + this.X[4] + 2053994217, 8) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F2(num, num3, num5) + this.X[1] + 2053994217, 11) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F2(num9, num, num3) + this.X[3] + 2053994217, 14) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F2(num7, num9, num) + this.X[11] + 2053994217, 14) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F2(num5, num7, num9) + this.X[15] + 2053994217, 6) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F2(num3, num5, num7) + this.X[0] + 2053994217, 14) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F2(num, num3, num5) + this.X[5] + 2053994217, 6) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F2(num9, num, num3) + this.X[12] + 2053994217, 9) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F2(num7, num9, num) + this.X[2] + 2053994217, 12) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F2(num5, num7, num9) + this.X[13] + 2053994217, 9) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F2(num3, num5, num7) + this.X[9] + 2053994217, 12) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F2(num, num3, num5) + this.X[7] + 2053994217, 5) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F2(num9, num, num3) + this.X[10] + 2053994217, 15) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F2(num7, num9, num) + this.X[14] + 2053994217, 8) + num3;
			num9 = this.RL(num9, 10);
			num4 = this.RL(num4 + this.F5(num6, num8, num10) + this.X[4] + -1454113458, 9) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F5(num4, num6, num8) + this.X[0] + -1454113458, 15) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F5(num2, num4, num6) + this.X[5] + -1454113458, 5) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F5(num10, num2, num4) + this.X[9] + -1454113458, 11) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F5(num8, num10, num2) + this.X[7] + -1454113458, 6) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F5(num6, num8, num10) + this.X[12] + -1454113458, 8) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F5(num4, num6, num8) + this.X[2] + -1454113458, 13) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F5(num2, num4, num6) + this.X[10] + -1454113458, 12) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F5(num10, num2, num4) + this.X[14] + -1454113458, 5) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F5(num8, num10, num2) + this.X[1] + -1454113458, 12) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F5(num6, num8, num10) + this.X[3] + -1454113458, 13) + num2;
			num8 = this.RL(num8, 10);
			num2 = this.RL(num2 + this.F5(num4, num6, num8) + this.X[8] + -1454113458, 14) + num10;
			num6 = this.RL(num6, 10);
			num10 = this.RL(num10 + this.F5(num2, num4, num6) + this.X[11] + -1454113458, 11) + num8;
			num4 = this.RL(num4, 10);
			num8 = this.RL(num8 + this.F5(num10, num2, num4) + this.X[6] + -1454113458, 8) + num6;
			num2 = this.RL(num2, 10);
			num6 = this.RL(num6 + this.F5(num8, num10, num2) + this.X[15] + -1454113458, 5) + num4;
			num10 = this.RL(num10, 10);
			num4 = this.RL(num4 + this.F5(num6, num8, num10) + this.X[13] + -1454113458, 6) + num2;
			num8 = this.RL(num8, 10);
			num3 = this.RL(num3 + this.F1(num5, num7, num9) + this.X[12], 8) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F1(num3, num5, num7) + this.X[15], 5) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F1(num, num3, num5) + this.X[10], 12) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F1(num9, num, num3) + this.X[4], 9) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F1(num7, num9, num) + this.X[1], 12) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F1(num5, num7, num9) + this.X[5], 5) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F1(num3, num5, num7) + this.X[8], 14) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F1(num, num3, num5) + this.X[7], 6) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F1(num9, num, num3) + this.X[6], 8) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F1(num7, num9, num) + this.X[2], 13) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F1(num5, num7, num9) + this.X[13], 6) + num;
			num7 = this.RL(num7, 10);
			num = this.RL(num + this.F1(num3, num5, num7) + this.X[14], 5) + num9;
			num5 = this.RL(num5, 10);
			num9 = this.RL(num9 + this.F1(num, num3, num5) + this.X[0], 15) + num7;
			num3 = this.RL(num3, 10);
			num7 = this.RL(num7 + this.F1(num9, num, num3) + this.X[3], 13) + num5;
			num = this.RL(num, 10);
			num5 = this.RL(num5 + this.F1(num7, num9, num) + this.X[9], 11) + num3;
			num9 = this.RL(num9, 10);
			num3 = this.RL(num3 + this.F1(num5, num7, num9) + this.X[11], 11) + num;
			num7 = this.RL(num7, 10);
			num7 += num6 + this.H1;
			this.H1 = this.H2 + num8 + num9;
			this.H2 = this.H3 + num10 + num;
			this.H3 = this.H4 + num2 + num3;
			this.H4 = this.H0 + num4 + num5;
			this.H0 = num7;
			this.xOff = 0;
			for (int num11 = 0; num11 != this.X.Length; num11++)
			{
				this.X[num11] = 0;
			}
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x0015DFA0 File Offset: 0x0015C1A0
		public override IMemoable Copy()
		{
			return new RipeMD160Digest(this);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0015DFA8 File Offset: 0x0015C1A8
		public override void Reset(IMemoable other)
		{
			RipeMD160Digest t = (RipeMD160Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x0400232B RID: 9003
		private const int DigestLength = 20;

		// Token: 0x0400232C RID: 9004
		private int H0;

		// Token: 0x0400232D RID: 9005
		private int H1;

		// Token: 0x0400232E RID: 9006
		private int H2;

		// Token: 0x0400232F RID: 9007
		private int H3;

		// Token: 0x04002330 RID: 9008
		private int H4;

		// Token: 0x04002331 RID: 9009
		private int[] X = new int[16];

		// Token: 0x04002332 RID: 9010
		private int xOff;
	}
}
