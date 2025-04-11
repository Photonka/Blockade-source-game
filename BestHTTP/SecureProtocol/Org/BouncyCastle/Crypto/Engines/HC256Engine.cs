using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000564 RID: 1380
	public class HC256Engine : IStreamCipher
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x001446AC File Offset: 0x001428AC
		private uint Step()
		{
			uint num = this.cnt & 1023U;
			uint result;
			if (this.cnt < 1024U)
			{
				uint num2 = this.p[(int)(num - 3U & 1023U)];
				uint num3 = this.p[(int)(num - 1023U & 1023U)];
				this.p[(int)num] += this.p[(int)(num - 10U & 1023U)] + (HC256Engine.RotateRight(num2, 10) ^ HC256Engine.RotateRight(num3, 23)) + this.q[(int)((num2 ^ num3) & 1023U)];
				num2 = this.p[(int)(num - 12U & 1023U)];
				result = (this.q[(int)(num2 & 255U)] + this.q[(int)((num2 >> 8 & 255U) + 256U)] + this.q[(int)((num2 >> 16 & 255U) + 512U)] + this.q[(int)((num2 >> 24 & 255U) + 768U)] ^ this.p[(int)num]);
			}
			else
			{
				uint num4 = this.q[(int)(num - 3U & 1023U)];
				uint num5 = this.q[(int)(num - 1023U & 1023U)];
				this.q[(int)num] += this.q[(int)(num - 10U & 1023U)] + (HC256Engine.RotateRight(num4, 10) ^ HC256Engine.RotateRight(num5, 23)) + this.p[(int)((num4 ^ num5) & 1023U)];
				num4 = this.q[(int)(num - 12U & 1023U)];
				result = (this.p[(int)(num4 & 255U)] + this.p[(int)((num4 >> 8 & 255U) + 256U)] + this.p[(int)((num4 >> 16 & 255U) + 512U)] + this.p[(int)((num4 >> 24 & 255U) + 768U)] ^ this.q[(int)num]);
			}
			this.cnt = (this.cnt + 1U & 2047U);
			return result;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x001448B4 File Offset: 0x00142AB4
		private void Init()
		{
			if (this.key.Length != 32 && this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128/256 bits long");
			}
			if (this.iv.Length < 16)
			{
				throw new ArgumentException("The IV must be at least 128 bits long");
			}
			if (this.key.Length != 32)
			{
				byte[] destinationArray = new byte[32];
				Array.Copy(this.key, 0, destinationArray, 0, this.key.Length);
				Array.Copy(this.key, 0, destinationArray, 16, this.key.Length);
				this.key = destinationArray;
			}
			if (this.iv.Length < 32)
			{
				byte[] array = new byte[32];
				Array.Copy(this.iv, 0, array, 0, this.iv.Length);
				Array.Copy(this.iv, 0, array, this.iv.Length, array.Length - this.iv.Length);
				this.iv = array;
			}
			this.idx = 0;
			this.cnt = 0U;
			uint[] array2 = new uint[2560];
			for (int i = 0; i < 32; i++)
			{
				array2[i >> 2] |= (uint)((uint)this.key[i] << 8 * (i & 3));
			}
			for (int j = 0; j < 32; j++)
			{
				array2[(j >> 2) + 8] |= (uint)((uint)this.iv[j] << 8 * (j & 3));
			}
			for (uint num = 16U; num < 2560U; num += 1U)
			{
				uint num2 = array2[(int)(num - 2U)];
				uint num3 = array2[(int)(num - 15U)];
				array2[(int)num] = (HC256Engine.RotateRight(num2, 17) ^ HC256Engine.RotateRight(num2, 19) ^ num2 >> 10) + array2[(int)(num - 7U)] + (HC256Engine.RotateRight(num3, 7) ^ HC256Engine.RotateRight(num3, 18) ^ num3 >> 3) + array2[(int)(num - 16U)] + num;
			}
			Array.Copy(array2, 512, this.p, 0, 1024);
			Array.Copy(array2, 1536, this.q, 0, 1024);
			for (int k = 0; k < 4096; k++)
			{
				this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x00144ACA File Offset: 0x00142CCA
		public virtual string AlgorithmName
		{
			get
			{
				return "HC-256";
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x00144AD4 File Offset: 0x00142CD4
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ICipherParameters cipherParameters = parameters;
			if (parameters is ParametersWithIV)
			{
				this.iv = ((ParametersWithIV)parameters).GetIV();
				cipherParameters = ((ParametersWithIV)parameters).Parameters;
			}
			else
			{
				this.iv = new byte[0];
			}
			if (cipherParameters is KeyParameter)
			{
				this.key = ((KeyParameter)cipherParameters).GetKey();
				this.Init();
				this.initialised = true;
				return;
			}
			throw new ArgumentException("Invalid parameter passed to HC256 init - " + Platform.GetTypeName(parameters), "parameters");
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00144B59 File Offset: 0x00142D59
		private byte GetByte()
		{
			if (this.idx == 0)
			{
				Pack.UInt32_To_LE(this.Step(), this.buf);
			}
			byte result = this.buf[this.idx];
			this.idx = (this.idx + 1 & 3);
			return result;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00144B94 File Offset: 0x00142D94
		public virtual void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			for (int i = 0; i < len; i++)
			{
				output[outOff + i] = (input[inOff + i] ^ this.GetByte());
			}
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00144BFB File Offset: 0x00142DFB
		public virtual void Reset()
		{
			this.Init();
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00144C03 File Offset: 0x00142E03
		public virtual byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x0014424C File Offset: 0x0014244C
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x0400215D RID: 8541
		private uint[] p = new uint[1024];

		// Token: 0x0400215E RID: 8542
		private uint[] q = new uint[1024];

		// Token: 0x0400215F RID: 8543
		private uint cnt;

		// Token: 0x04002160 RID: 8544
		private byte[] key;

		// Token: 0x04002161 RID: 8545
		private byte[] iv;

		// Token: 0x04002162 RID: 8546
		private bool initialised;

		// Token: 0x04002163 RID: 8547
		private byte[] buf = new byte[4];

		// Token: 0x04002164 RID: 8548
		private int idx;
	}
}
