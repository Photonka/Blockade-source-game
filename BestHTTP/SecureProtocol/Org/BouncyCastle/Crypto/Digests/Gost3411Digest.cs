using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000593 RID: 1427
	public class Gost3411Digest : IDigest, IMemoable
	{
		// Token: 0x060036A9 RID: 13993 RVA: 0x001575A0 File Offset: 0x001557A0
		private static byte[][] MakeC()
		{
			byte[][] array = new byte[4][];
			for (int i = 0; i < 4; i++)
			{
				array[i] = new byte[32];
			}
			return array;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x001575CC File Offset: 0x001557CC
		public Gost3411Digest()
		{
			this.sBox = Gost28147Engine.GetSBox("D-A");
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x001576CC File Offset: 0x001558CC
		public Gost3411Digest(byte[] sBoxParam)
		{
			this.sBox = Arrays.Clone(sBoxParam);
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x001577C8 File Offset: 0x001559C8
		public Gost3411Digest(Gost3411Digest t)
		{
			this.Reset(t);
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x001578A0 File Offset: 0x00155AA0
		public string AlgorithmName
		{
			get
			{
				return "Gost3411";
			}
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x001565CC File Offset: 0x001547CC
		public int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x001578A8 File Offset: 0x00155AA8
		public void Update(byte input)
		{
			byte[] array = this.xBuf;
			int num = this.xBufOff;
			this.xBufOff = num + 1;
			array[num] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1UL;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x00157910 File Offset: 0x00155B10
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.xBufOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > this.xBuf.Length)
			{
				Array.Copy(input, inOff, this.xBuf, 0, this.xBuf.Length);
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				inOff += this.xBuf.Length;
				length -= this.xBuf.Length;
				this.byteCount += (ulong)this.xBuf.Length;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x001579C4 File Offset: 0x00155BC4
		private byte[] P(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				this.K[num++] = input[i];
				this.K[num++] = input[8 + i];
				this.K[num++] = input[16 + i];
				this.K[num++] = input[24 + i];
			}
			return this.K;
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x00157A2C File Offset: 0x00155C2C
		private byte[] A(byte[] input)
		{
			for (int i = 0; i < 8; i++)
			{
				this.a[i] = (input[i] ^ input[i + 8]);
			}
			Array.Copy(input, 8, input, 0, 24);
			Array.Copy(this.a, 0, input, 24, 8);
			return input;
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x00157A73 File Offset: 0x00155C73
		private void E(byte[] key, byte[] s, int sOff, byte[] input, int inOff)
		{
			this.cipher.Init(true, new KeyParameter(key));
			this.cipher.ProcessBlock(input, inOff, s, sOff);
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x00157A9C File Offset: 0x00155C9C
		private void fw(byte[] input)
		{
			Gost3411Digest.cpyBytesToShort(input, this.wS);
			this.w_S[15] = (this.wS[0] ^ this.wS[1] ^ this.wS[2] ^ this.wS[3] ^ this.wS[12] ^ this.wS[15]);
			Array.Copy(this.wS, 1, this.w_S, 0, 15);
			Gost3411Digest.cpyShortToBytes(this.w_S, input);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x00157B18 File Offset: 0x00155D18
		private void processBlock(byte[] input, int inOff)
		{
			Array.Copy(input, inOff, this.M, 0, 32);
			this.H.CopyTo(this.U, 0);
			this.M.CopyTo(this.V, 0);
			for (int i = 0; i < 32; i++)
			{
				this.W[i] = (this.U[i] ^ this.V[i]);
			}
			this.E(this.P(this.W), this.S, 0, this.H, 0);
			for (int j = 1; j < 4; j++)
			{
				byte[] array = this.A(this.U);
				for (int k = 0; k < 32; k++)
				{
					this.U[k] = (array[k] ^ this.C[j][k]);
				}
				this.V = this.A(this.A(this.V));
				for (int l = 0; l < 32; l++)
				{
					this.W[l] = (this.U[l] ^ this.V[l]);
				}
				this.E(this.P(this.W), this.S, j * 8, this.H, j * 8);
			}
			for (int m = 0; m < 12; m++)
			{
				this.fw(this.S);
			}
			for (int n = 0; n < 32; n++)
			{
				this.S[n] = (this.S[n] ^ this.M[n]);
			}
			this.fw(this.S);
			for (int num = 0; num < 32; num++)
			{
				this.S[num] = (this.H[num] ^ this.S[num]);
			}
			for (int num2 = 0; num2 < 61; num2++)
			{
				this.fw(this.S);
			}
			Array.Copy(this.S, 0, this.H, 0, this.H.Length);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00157D0C File Offset: 0x00155F0C
		private void finish()
		{
			Pack.UInt64_To_LE(this.byteCount * 8UL, this.L);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.processBlock(this.L, 0);
			this.processBlock(this.Sum, 0);
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00157D58 File Offset: 0x00155F58
		public int DoFinal(byte[] output, int outOff)
		{
			this.finish();
			this.H.CopyTo(output, outOff);
			this.Reset();
			return 32;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x00157D78 File Offset: 0x00155F78
		public void Reset()
		{
			this.byteCount = 0UL;
			this.xBufOff = 0;
			Array.Clear(this.H, 0, this.H.Length);
			Array.Clear(this.L, 0, this.L.Length);
			Array.Clear(this.M, 0, this.M.Length);
			Array.Clear(this.C[1], 0, this.C[1].Length);
			Array.Clear(this.C[3], 0, this.C[3].Length);
			Array.Clear(this.Sum, 0, this.Sum.Length);
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
			Gost3411Digest.C2.CopyTo(this.C[2], 0);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x00157E3C File Offset: 0x0015603C
		private void sumByteArray(byte[] input)
		{
			int num = 0;
			for (int num2 = 0; num2 != this.Sum.Length; num2++)
			{
				int num3 = (int)((this.Sum[num2] & byte.MaxValue) + (input[num2] & byte.MaxValue)) + num;
				this.Sum[num2] = (byte)num3;
				num = num3 >> 8;
			}
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x00157E88 File Offset: 0x00156088
		private static void cpyBytesToShort(byte[] S, short[] wS)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				wS[i] = (short)(((int)S[i * 2 + 1] << 8 & 65280) | (int)(S[i * 2] & byte.MaxValue));
			}
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x00157EC4 File Offset: 0x001560C4
		private static void cpyShortToBytes(short[] wS, byte[] S)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				S[i * 2 + 1] = (byte)(wS[i] >> 8);
				S[i * 2] = (byte)wS[i];
			}
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x001565CC File Offset: 0x001547CC
		public int GetByteLength()
		{
			return 32;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x00157EF7 File Offset: 0x001560F7
		public IMemoable Copy()
		{
			return new Gost3411Digest(this);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x00157F00 File Offset: 0x00156100
		public void Reset(IMemoable other)
		{
			Gost3411Digest gost3411Digest = (Gost3411Digest)other;
			this.sBox = gost3411Digest.sBox;
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
			Array.Copy(gost3411Digest.H, 0, this.H, 0, gost3411Digest.H.Length);
			Array.Copy(gost3411Digest.L, 0, this.L, 0, gost3411Digest.L.Length);
			Array.Copy(gost3411Digest.M, 0, this.M, 0, gost3411Digest.M.Length);
			Array.Copy(gost3411Digest.Sum, 0, this.Sum, 0, gost3411Digest.Sum.Length);
			Array.Copy(gost3411Digest.C[1], 0, this.C[1], 0, gost3411Digest.C[1].Length);
			Array.Copy(gost3411Digest.C[2], 0, this.C[2], 0, gost3411Digest.C[2].Length);
			Array.Copy(gost3411Digest.C[3], 0, this.C[3], 0, gost3411Digest.C[3].Length);
			Array.Copy(gost3411Digest.xBuf, 0, this.xBuf, 0, gost3411Digest.xBuf.Length);
			this.xBufOff = gost3411Digest.xBufOff;
			this.byteCount = gost3411Digest.byteCount;
		}

		// Token: 0x040022C4 RID: 8900
		private const int DIGEST_LENGTH = 32;

		// Token: 0x040022C5 RID: 8901
		private byte[] H = new byte[32];

		// Token: 0x040022C6 RID: 8902
		private byte[] L = new byte[32];

		// Token: 0x040022C7 RID: 8903
		private byte[] M = new byte[32];

		// Token: 0x040022C8 RID: 8904
		private byte[] Sum = new byte[32];

		// Token: 0x040022C9 RID: 8905
		private byte[][] C = Gost3411Digest.MakeC();

		// Token: 0x040022CA RID: 8906
		private byte[] xBuf = new byte[32];

		// Token: 0x040022CB RID: 8907
		private int xBufOff;

		// Token: 0x040022CC RID: 8908
		private ulong byteCount;

		// Token: 0x040022CD RID: 8909
		private readonly IBlockCipher cipher = new Gost28147Engine();

		// Token: 0x040022CE RID: 8910
		private byte[] sBox;

		// Token: 0x040022CF RID: 8911
		private byte[] K = new byte[32];

		// Token: 0x040022D0 RID: 8912
		private byte[] a = new byte[8];

		// Token: 0x040022D1 RID: 8913
		internal short[] wS = new short[16];

		// Token: 0x040022D2 RID: 8914
		internal short[] w_S = new short[16];

		// Token: 0x040022D3 RID: 8915
		internal byte[] S = new byte[32];

		// Token: 0x040022D4 RID: 8916
		internal byte[] U = new byte[32];

		// Token: 0x040022D5 RID: 8917
		internal byte[] V = new byte[32];

		// Token: 0x040022D6 RID: 8918
		internal byte[] W = new byte[32];

		// Token: 0x040022D7 RID: 8919
		private static readonly byte[] C2 = new byte[]
		{
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue
		};
	}
}
