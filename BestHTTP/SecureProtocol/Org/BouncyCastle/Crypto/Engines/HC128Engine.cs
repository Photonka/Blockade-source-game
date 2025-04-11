using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000563 RID: 1379
	public class HC128Engine : IStreamCipher
	{
		// Token: 0x06003455 RID: 13397 RVA: 0x001441D8 File Offset: 0x001423D8
		private static uint F1(uint x)
		{
			return HC128Engine.RotateRight(x, 7) ^ HC128Engine.RotateRight(x, 18) ^ x >> 3;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x001441EE File Offset: 0x001423EE
		private static uint F2(uint x)
		{
			return HC128Engine.RotateRight(x, 17) ^ HC128Engine.RotateRight(x, 19) ^ x >> 10;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00144206 File Offset: 0x00142406
		private uint G1(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateRight(x, 10) ^ HC128Engine.RotateRight(z, 23)) + HC128Engine.RotateRight(y, 8);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00144221 File Offset: 0x00142421
		private uint G2(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateLeft(x, 10) ^ HC128Engine.RotateLeft(z, 23)) + HC128Engine.RotateLeft(y, 8);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x0014423C File Offset: 0x0014243C
		private static uint RotateLeft(uint x, int bits)
		{
			return x << bits | x >> -bits;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x0014424C File Offset: 0x0014244C
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x0014425C File Offset: 0x0014245C
		private uint H1(uint x)
		{
			return this.q[(int)(x & 255U)] + this.q[(int)((x >> 16 & 255U) + 256U)];
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00144284 File Offset: 0x00142484
		private uint H2(uint x)
		{
			return this.p[(int)(x & 255U)] + this.p[(int)((x >> 16 & 255U) + 256U)];
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x001442AC File Offset: 0x001424AC
		private static uint Mod1024(uint x)
		{
			return x & 1023U;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x001442B5 File Offset: 0x001424B5
		private static uint Mod512(uint x)
		{
			return x & 511U;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x001442BE File Offset: 0x001424BE
		private static uint Dim(uint x, uint y)
		{
			return HC128Engine.Mod512(x - y);
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x001442C8 File Offset: 0x001424C8
		private uint Step()
		{
			uint num = HC128Engine.Mod512(this.cnt);
			uint result;
			if (this.cnt < 512U)
			{
				this.p[(int)num] += this.G1(this.p[(int)HC128Engine.Dim(num, 3U)], this.p[(int)HC128Engine.Dim(num, 10U)], this.p[(int)HC128Engine.Dim(num, 511U)]);
				result = (this.H1(this.p[(int)HC128Engine.Dim(num, 12U)]) ^ this.p[(int)num]);
			}
			else
			{
				this.q[(int)num] += this.G2(this.q[(int)HC128Engine.Dim(num, 3U)], this.q[(int)HC128Engine.Dim(num, 10U)], this.q[(int)HC128Engine.Dim(num, 511U)]);
				result = (this.H2(this.q[(int)HC128Engine.Dim(num, 12U)]) ^ this.q[(int)num]);
			}
			this.cnt = HC128Engine.Mod1024(this.cnt + 1U);
			return result;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x001443CC File Offset: 0x001425CC
		private void Init()
		{
			if (this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128 bits long");
			}
			this.idx = 0;
			this.cnt = 0U;
			uint[] array = new uint[1280];
			for (int i = 0; i < 16; i++)
			{
				array[i >> 2] |= (uint)((uint)this.key[i] << 8 * (i & 3));
			}
			Array.Copy(array, 0, array, 4, 4);
			int num = 0;
			while (num < this.iv.Length && num < 16)
			{
				array[(num >> 2) + 8] |= (uint)((uint)this.iv[num] << 8 * (num & 3));
				num++;
			}
			Array.Copy(array, 8, array, 12, 4);
			for (uint num2 = 16U; num2 < 1280U; num2 += 1U)
			{
				array[(int)num2] = HC128Engine.F2(array[(int)(num2 - 2U)]) + array[(int)(num2 - 7U)] + HC128Engine.F1(array[(int)(num2 - 15U)]) + array[(int)(num2 - 16U)] + num2;
			}
			Array.Copy(array, 256, this.p, 0, 512);
			Array.Copy(array, 768, this.q, 0, 512);
			for (int j = 0; j < 512; j++)
			{
				this.p[j] = this.Step();
			}
			for (int k = 0; k < 512; k++)
			{
				this.q[k] = this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x00144535 File Offset: 0x00142735
		public virtual string AlgorithmName
		{
			get
			{
				return "HC-128";
			}
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x0014453C File Offset: 0x0014273C
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
			throw new ArgumentException("Invalid parameter passed to HC128 init - " + Platform.GetTypeName(parameters), "parameters");
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x001445C1 File Offset: 0x001427C1
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

		// Token: 0x06003465 RID: 13413 RVA: 0x001445FC File Offset: 0x001427FC
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

		// Token: 0x06003466 RID: 13414 RVA: 0x00144663 File Offset: 0x00142863
		public virtual void Reset()
		{
			this.Init();
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x0014466B File Offset: 0x0014286B
		public virtual byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x04002155 RID: 8533
		private uint[] p = new uint[512];

		// Token: 0x04002156 RID: 8534
		private uint[] q = new uint[512];

		// Token: 0x04002157 RID: 8535
		private uint cnt;

		// Token: 0x04002158 RID: 8536
		private byte[] key;

		// Token: 0x04002159 RID: 8537
		private byte[] iv;

		// Token: 0x0400215A RID: 8538
		private bool initialised;

		// Token: 0x0400215B RID: 8539
		private byte[] buf = new byte[4];

		// Token: 0x0400215C RID: 8540
		private int idx;
	}
}
