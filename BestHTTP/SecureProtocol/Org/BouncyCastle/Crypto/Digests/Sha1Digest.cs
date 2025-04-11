using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200059F RID: 1439
	public class Sha1Digest : GeneralDigest
	{
		// Token: 0x0600378C RID: 14220 RVA: 0x0016140F File Offset: 0x0015F60F
		public Sha1Digest()
		{
			this.Reset();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x0016142A File Offset: 0x0015F62A
		public Sha1Digest(Sha1Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x00161448 File Offset: 0x0015F648
		private void CopyIn(Sha1Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x001614BF File Offset: 0x0015F6BF
		public override string AlgorithmName
		{
			get
			{
				return "SHA-1";
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0013379E File Offset: 0x0013199E
		public override int GetDigestSize()
		{
			return 20;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x001614C8 File Offset: 0x0015F6C8
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.BE_To_UInt32(input, inOff);
			int num = this.xOff + 1;
			this.xOff = num;
			if (num == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x00161504 File Offset: 0x0015F704
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (uint)((ulong)bitLength >> 32);
			this.X[15] = (uint)bitLength;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x00161530 File Offset: 0x0015F730
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.H1, output, outOff);
			Pack.UInt32_To_BE(this.H2, output, outOff + 4);
			Pack.UInt32_To_BE(this.H3, output, outOff + 8);
			Pack.UInt32_To_BE(this.H4, output, outOff + 12);
			Pack.UInt32_To_BE(this.H5, output, outOff + 16);
			this.Reset();
			return 20;
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x00161598 File Offset: 0x0015F798
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193U;
			this.H2 = 4023233417U;
			this.H3 = 2562383102U;
			this.H4 = 271733878U;
			this.H5 = 3285377520U;
			this.xOff = 0;
			Array.Clear(this.X, 0, this.X.Length);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0015A1B6 File Offset: 0x001583B6
		private static uint F(uint u, uint v, uint w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0015A1CA File Offset: 0x001583CA
		private static uint H(uint u, uint v, uint w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x001615FD File Offset: 0x0015F7FD
		private static uint G(uint u, uint v, uint w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0016160C File Offset: 0x0015F80C
		internal override void ProcessBlock()
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = this.X[i - 3] ^ this.X[i - 8] ^ this.X[i - 14] ^ this.X[i - 16];
				this.X[i] = (num << 1 | num >> 31);
			}
			uint num2 = this.H1;
			uint num3 = this.H2;
			uint num4 = this.H3;
			uint num5 = this.H4;
			uint num6 = this.H5;
			int num7 = 0;
			for (int j = 0; j < 4; j++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.F(num3, num4, num5) + this.X[num7++] + 1518500249U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.F(num2, num3, num4) + this.X[num7++] + 1518500249U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.F(num6, num2, num3) + this.X[num7++] + 1518500249U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.F(num5, num6, num2) + this.X[num7++] + 1518500249U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.F(num4, num5, num6) + this.X[num7++] + 1518500249U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int k = 0; k < 4; k++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 1859775393U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 1859775393U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 1859775393U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 1859775393U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 1859775393U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int l = 0; l < 4; l++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.G(num3, num4, num5) + this.X[num7++] + 2400959708U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.G(num2, num3, num4) + this.X[num7++] + 2400959708U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.G(num6, num2, num3) + this.X[num7++] + 2400959708U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.G(num5, num6, num2) + this.X[num7++] + 2400959708U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.G(num4, num5, num6) + this.X[num7++] + 2400959708U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int m = 0; m < 4; m++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 3395469782U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 3395469782U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 3395469782U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 3395469782U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 3395469782U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			this.H1 += num2;
			this.H2 += num3;
			this.H3 += num4;
			this.H4 += num5;
			this.H5 += num6;
			this.xOff = 0;
			Array.Clear(this.X, 0, 16);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x00161B5C File Offset: 0x0015FD5C
		public override IMemoable Copy()
		{
			return new Sha1Digest(this);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x00161B64 File Offset: 0x0015FD64
		public override void Reset(IMemoable other)
		{
			Sha1Digest t = (Sha1Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x0400234B RID: 9035
		private const int DigestLength = 20;

		// Token: 0x0400234C RID: 9036
		private uint H1;

		// Token: 0x0400234D RID: 9037
		private uint H2;

		// Token: 0x0400234E RID: 9038
		private uint H3;

		// Token: 0x0400234F RID: 9039
		private uint H4;

		// Token: 0x04002350 RID: 9040
		private uint H5;

		// Token: 0x04002351 RID: 9041
		private uint[] X = new uint[80];

		// Token: 0x04002352 RID: 9042
		private int xOff;

		// Token: 0x04002353 RID: 9043
		private const uint Y1 = 1518500249U;

		// Token: 0x04002354 RID: 9044
		private const uint Y2 = 1859775393U;

		// Token: 0x04002355 RID: 9045
		private const uint Y3 = 2400959708U;

		// Token: 0x04002356 RID: 9046
		private const uint Y4 = 3395469782U;
	}
}
