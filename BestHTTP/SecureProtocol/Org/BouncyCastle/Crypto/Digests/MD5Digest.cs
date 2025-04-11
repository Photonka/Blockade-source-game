using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000598 RID: 1432
	public class MD5Digest : GeneralDigest
	{
		// Token: 0x06003710 RID: 14096 RVA: 0x00159FAB File Offset: 0x001581AB
		public MD5Digest()
		{
			this.Reset();
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x00159FC6 File Offset: 0x001581C6
		public MD5Digest(MD5Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x00159FE4 File Offset: 0x001581E4
		private void CopyIn(MD5Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x0015A04F File Offset: 0x0015824F
		public override string AlgorithmName
		{
			get
			{
				return "MD5";
			}
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x0015A058 File Offset: 0x00158258
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.LE_To_UInt32(input, inOff);
			int num = this.xOff + 1;
			this.xOff = num;
			if (num == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0015A094 File Offset: 0x00158294
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				if (this.xOff == 15)
				{
					this.X[15] = 0U;
				}
				this.ProcessBlock();
			}
			for (int i = this.xOff; i < 14; i++)
			{
				this.X[i] = 0U;
			}
			this.X[14] = (uint)bitLength;
			this.X[15] = (uint)((ulong)bitLength >> 32);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0015A0FC File Offset: 0x001582FC
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_LE(this.H1, output, outOff);
			Pack.UInt32_To_LE(this.H2, output, outOff + 4);
			Pack.UInt32_To_LE(this.H3, output, outOff + 8);
			Pack.UInt32_To_LE(this.H4, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x0015A154 File Offset: 0x00158354
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193U;
			this.H2 = 4023233417U;
			this.H3 = 2562383102U;
			this.H4 = 271733878U;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0U;
			}
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x0014A672 File Offset: 0x00148872
		private static uint RotateLeft(uint x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x0015A1B6 File Offset: 0x001583B6
		private static uint F(uint u, uint v, uint w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x0015A1C0 File Offset: 0x001583C0
		private static uint G(uint u, uint v, uint w)
		{
			return (u & w) | (v & ~w);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x0015A1CA File Offset: 0x001583CA
		private static uint H(uint u, uint v, uint w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0015A1D1 File Offset: 0x001583D1
		private static uint K(uint u, uint v, uint w)
		{
			return v ^ (u | ~w);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0015A1DC File Offset: 0x001583DC
		internal override void ProcessBlock()
		{
			uint num = this.H1;
			uint num2 = this.H2;
			uint num3 = this.H3;
			uint num4 = this.H4;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[0] + 3614090360U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[1] + 3905402710U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[2] + 606105819U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[3] + 3250441966U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[4] + 4118548399U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[5] + 1200080426U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[6] + 2821735955U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[7] + 4249261313U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[8] + 1770035416U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[9] + 2336552879U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[10] + 4294925233U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[11] + 2304563134U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[12] + 1804603682U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[13] + 4254626195U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[14] + 2792965006U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[15] + 1236535329U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[1] + 4129170786U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[6] + 3225465664U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[11] + 643717713U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[0] + 3921069994U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[5] + 3593408605U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[10] + 38016083U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[15] + 3634488961U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[4] + 3889429448U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[9] + 568446438U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[14] + 3275163606U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[3] + 4107603335U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[8] + 1163531501U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[13] + 2850285829U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[2] + 4243563512U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[7] + 1735328473U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[12] + 2368359562U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[5] + 4294588738U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[8] + 2272392833U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[11] + 1839030562U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[14] + 4259657740U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[1] + 2763975236U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[4] + 1272893353U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[7] + 4139469664U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[10] + 3200236656U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[13] + 681279174U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[0] + 3936430074U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[3] + 3572445317U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[6] + 76029189U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[9] + 3654602809U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[12] + 3873151461U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[15] + 530742520U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[2] + 3299628645U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[0] + 4096336452U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[7] + 1126891415U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[14] + 2878612391U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[5] + 4237533241U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[12] + 1700485571U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[3] + 2399980690U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[10] + 4293915773U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[1] + 2240044497U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[8] + 1873313359U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[15] + 4264355552U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[6] + 2734768916U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[13] + 1309151649U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[4] + 4149444226U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[11] + 3174756917U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[2] + 718787259U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[9] + 3951481745U, MD5Digest.S44) + num3;
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.xOff = 0;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0015ABE0 File Offset: 0x00158DE0
		public override IMemoable Copy()
		{
			return new MD5Digest(this);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x0015ABE8 File Offset: 0x00158DE8
		public override void Reset(IMemoable other)
		{
			MD5Digest t = (MD5Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x0400230B RID: 8971
		private const int DigestLength = 16;

		// Token: 0x0400230C RID: 8972
		private uint H1;

		// Token: 0x0400230D RID: 8973
		private uint H2;

		// Token: 0x0400230E RID: 8974
		private uint H3;

		// Token: 0x0400230F RID: 8975
		private uint H4;

		// Token: 0x04002310 RID: 8976
		private uint[] X = new uint[16];

		// Token: 0x04002311 RID: 8977
		private int xOff;

		// Token: 0x04002312 RID: 8978
		private static readonly int S11 = 7;

		// Token: 0x04002313 RID: 8979
		private static readonly int S12 = 12;

		// Token: 0x04002314 RID: 8980
		private static readonly int S13 = 17;

		// Token: 0x04002315 RID: 8981
		private static readonly int S14 = 22;

		// Token: 0x04002316 RID: 8982
		private static readonly int S21 = 5;

		// Token: 0x04002317 RID: 8983
		private static readonly int S22 = 9;

		// Token: 0x04002318 RID: 8984
		private static readonly int S23 = 14;

		// Token: 0x04002319 RID: 8985
		private static readonly int S24 = 20;

		// Token: 0x0400231A RID: 8986
		private static readonly int S31 = 4;

		// Token: 0x0400231B RID: 8987
		private static readonly int S32 = 11;

		// Token: 0x0400231C RID: 8988
		private static readonly int S33 = 16;

		// Token: 0x0400231D RID: 8989
		private static readonly int S34 = 23;

		// Token: 0x0400231E RID: 8990
		private static readonly int S41 = 6;

		// Token: 0x0400231F RID: 8991
		private static readonly int S42 = 10;

		// Token: 0x04002320 RID: 8992
		private static readonly int S43 = 15;

		// Token: 0x04002321 RID: 8993
		private static readonly int S44 = 21;
	}
}
