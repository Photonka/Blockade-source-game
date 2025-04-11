using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x020004A2 RID: 1186
	internal class X931Rng
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x0012564C File Offset: 0x0012384C
		internal X931Rng(IBlockCipher engine, byte[] dateTimeVector, IEntropySource entropySource)
		{
			this.mEngine = engine;
			this.mEntropySource = entropySource;
			this.mDT = new byte[engine.GetBlockSize()];
			Array.Copy(dateTimeVector, 0, this.mDT, 0, this.mDT.Length);
			this.mI = new byte[engine.GetBlockSize()];
			this.mR = new byte[engine.GetBlockSize()];
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x001256C0 File Offset: 0x001238C0
		internal int Generate(byte[] output, bool predictionResistant)
		{
			if (this.mR.Length == 8)
			{
				if (this.mReseedCounter > 32768L)
				{
					return -1;
				}
				if (X931Rng.IsTooLarge(output, 512))
				{
					throw new ArgumentException("Number of bits per request limited to " + 4096, "output");
				}
			}
			else
			{
				if (this.mReseedCounter > 8388608L)
				{
					return -1;
				}
				if (X931Rng.IsTooLarge(output, 32768))
				{
					throw new ArgumentException("Number of bits per request limited to " + 262144, "output");
				}
			}
			if (predictionResistant || this.mV == null)
			{
				this.mV = this.mEntropySource.GetEntropy();
				if (this.mV.Length != this.mEngine.GetBlockSize())
				{
					throw new InvalidOperationException("Insufficient entropy returned");
				}
			}
			int num = output.Length / this.mR.Length;
			for (int i = 0; i < num; i++)
			{
				this.mEngine.ProcessBlock(this.mDT, 0, this.mI, 0);
				this.Process(this.mR, this.mI, this.mV);
				this.Process(this.mV, this.mR, this.mI);
				Array.Copy(this.mR, 0, output, i * this.mR.Length, this.mR.Length);
				this.Increment(this.mDT);
			}
			int num2 = output.Length - num * this.mR.Length;
			if (num2 > 0)
			{
				this.mEngine.ProcessBlock(this.mDT, 0, this.mI, 0);
				this.Process(this.mR, this.mI, this.mV);
				this.Process(this.mV, this.mR, this.mI);
				Array.Copy(this.mR, 0, output, num * this.mR.Length, num2);
				this.Increment(this.mDT);
			}
			this.mReseedCounter += 1L;
			return output.Length;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x001258AD File Offset: 0x00123AAD
		internal void Reseed()
		{
			this.mV = this.mEntropySource.GetEntropy();
			if (this.mV.Length != this.mEngine.GetBlockSize())
			{
				throw new InvalidOperationException("Insufficient entropy returned");
			}
			this.mReseedCounter = 1L;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x001258E8 File Offset: 0x00123AE8
		internal IEntropySource EntropySource
		{
			get
			{
				return this.mEntropySource;
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x001258F0 File Offset: 0x00123AF0
		private void Process(byte[] res, byte[] a, byte[] b)
		{
			for (int num = 0; num != res.Length; num++)
			{
				res[num] = (a[num] ^ b[num]);
			}
			this.mEngine.ProcessBlock(res, 0, res, 0);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00125928 File Offset: 0x00123B28
		private void Increment(byte[] val)
		{
			for (int i = val.Length - 1; i >= 0; i--)
			{
				int num = i;
				byte b = val[num] + 1;
				val[num] = b;
				if (b != 0)
				{
					break;
				}
			}
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x00125957 File Offset: 0x00123B57
		private static bool IsTooLarge(byte[] bytes, int maxBytes)
		{
			return bytes != null && bytes.Length > maxBytes;
		}

		// Token: 0x04001E39 RID: 7737
		private const long BLOCK64_RESEED_MAX = 32768L;

		// Token: 0x04001E3A RID: 7738
		private const long BLOCK128_RESEED_MAX = 8388608L;

		// Token: 0x04001E3B RID: 7739
		private const int BLOCK64_MAX_BITS_REQUEST = 4096;

		// Token: 0x04001E3C RID: 7740
		private const int BLOCK128_MAX_BITS_REQUEST = 262144;

		// Token: 0x04001E3D RID: 7741
		private readonly IBlockCipher mEngine;

		// Token: 0x04001E3E RID: 7742
		private readonly IEntropySource mEntropySource;

		// Token: 0x04001E3F RID: 7743
		private readonly byte[] mDT;

		// Token: 0x04001E40 RID: 7744
		private readonly byte[] mI;

		// Token: 0x04001E41 RID: 7745
		private readonly byte[] mR;

		// Token: 0x04001E42 RID: 7746
		private byte[] mV;

		// Token: 0x04001E43 RID: 7747
		private long mReseedCounter = 1L;
	}
}
