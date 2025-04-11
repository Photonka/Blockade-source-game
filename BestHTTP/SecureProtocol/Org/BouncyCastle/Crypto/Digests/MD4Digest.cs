using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000597 RID: 1431
	public class MD4Digest : GeneralDigest
	{
		// Token: 0x060036FF RID: 14079 RVA: 0x00159687 File Offset: 0x00157887
		public MD4Digest()
		{
			this.Reset();
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x001596A2 File Offset: 0x001578A2
		public MD4Digest(MD4Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x001596C0 File Offset: 0x001578C0
		private void CopyIn(MD4Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x0015972B File Offset: 0x0015792B
		public override string AlgorithmName
		{
			get
			{
				return "MD4";
			}
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x00159734 File Offset: 0x00157934
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

		// Token: 0x06003705 RID: 14085 RVA: 0x0015979E File Offset: 0x0015799E
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x001597CC File Offset: 0x001579CC
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x001597F0 File Offset: 0x001579F0
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H1, output, outOff);
			this.UnpackWord(this.H2, output, outOff + 4);
			this.UnpackWord(this.H3, output, outOff + 8);
			this.UnpackWord(this.H4, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x0015984C File Offset: 0x00157A4C
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193;
			this.H2 = -271733879;
			this.H3 = -1732584194;
			this.H4 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x00146642 File Offset: 0x00144842
		private int RotateLeft(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x001598AE File Offset: 0x00157AAE
		private int F(int u, int v, int w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x001598B8 File Offset: 0x00157AB8
		private int G(int u, int v, int w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x001598C5 File Offset: 0x00157AC5
		private int H(int u, int v, int w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x001598CC File Offset: 0x00157ACC
		internal override void ProcessBlock()
		{
			int num = this.H1;
			int num2 = this.H2;
			int num3 = this.H3;
			int num4 = this.H4;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[0], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[1], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[2], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[3], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[4], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[5], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[6], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[7], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[8], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[9], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[10], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[11], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[12], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[13], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[14], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[15], 19);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[0] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[4] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[8] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[12] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[1] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[5] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[9] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[13] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[2] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[6] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[10] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[14] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[3] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[7] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[11] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[15] + 1518500249, 13);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[0] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[8] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[4] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[12] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[2] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[10] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[6] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[14] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[1] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[9] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[5] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[13] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[3] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[11] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[7] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[15] + 1859775393, 15);
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.xOff = 0;
			for (int num5 = 0; num5 != this.X.Length; num5++)
			{
				this.X[num5] = 0;
			}
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x00159F86 File Offset: 0x00158186
		public override IMemoable Copy()
		{
			return new MD4Digest(this);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x00159F90 File Offset: 0x00158190
		public override void Reset(IMemoable other)
		{
			MD4Digest t = (MD4Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x040022F8 RID: 8952
		private const int DigestLength = 16;

		// Token: 0x040022F9 RID: 8953
		private int H1;

		// Token: 0x040022FA RID: 8954
		private int H2;

		// Token: 0x040022FB RID: 8955
		private int H3;

		// Token: 0x040022FC RID: 8956
		private int H4;

		// Token: 0x040022FD RID: 8957
		private int[] X = new int[16];

		// Token: 0x040022FE RID: 8958
		private int xOff;

		// Token: 0x040022FF RID: 8959
		private const int S11 = 3;

		// Token: 0x04002300 RID: 8960
		private const int S12 = 7;

		// Token: 0x04002301 RID: 8961
		private const int S13 = 11;

		// Token: 0x04002302 RID: 8962
		private const int S14 = 19;

		// Token: 0x04002303 RID: 8963
		private const int S21 = 3;

		// Token: 0x04002304 RID: 8964
		private const int S22 = 5;

		// Token: 0x04002305 RID: 8965
		private const int S23 = 9;

		// Token: 0x04002306 RID: 8966
		private const int S24 = 13;

		// Token: 0x04002307 RID: 8967
		private const int S31 = 3;

		// Token: 0x04002308 RID: 8968
		private const int S32 = 9;

		// Token: 0x04002309 RID: 8969
		private const int S33 = 11;

		// Token: 0x0400230A RID: 8970
		private const int S34 = 15;
	}
}
