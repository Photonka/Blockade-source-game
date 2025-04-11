using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000567 RID: 1383
	public class IsaacEngine : IStreamCipher
	{
		// Token: 0x0600348B RID: 13451 RVA: 0x00145618 File Offset: 0x00143818
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to ISAAC Init - " + Platform.GetTypeName(parameters), "parameters");
			}
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x0014565C File Offset: 0x0014385C
		public virtual byte ReturnByte(byte input)
		{
			if (this.index == 0)
			{
				this.isaac();
				this.keyStream = Pack.UInt32_To_BE(this.results);
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 1023);
			return result;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x001456AC File Offset: 0x001438AC
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
				if (this.index == 0)
				{
					this.isaac();
					this.keyStream = Pack.UInt32_To_BE(this.results);
				}
				output[i + outOff] = (this.keyStream[this.index] ^ input[i + inOff]);
				this.index = (this.index + 1 & 1023);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x0014574D File Offset: 0x0014394D
		public virtual string AlgorithmName
		{
			get
			{
				return "ISAAC";
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00145754 File Offset: 0x00143954
		public virtual void Reset()
		{
			this.setKey(this.workingKey);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00145764 File Offset: 0x00143964
		private void setKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			if (this.engineState == null)
			{
				this.engineState = new uint[IsaacEngine.stateArraySize];
			}
			if (this.results == null)
			{
				this.results = new uint[IsaacEngine.stateArraySize];
			}
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				this.engineState[i] = (this.results[i] = 0U);
			}
			this.a = (this.b = (this.c = 0U));
			this.index = 0;
			byte[] array = new byte[keyBytes.Length + (keyBytes.Length & 3)];
			Array.Copy(keyBytes, 0, array, 0, keyBytes.Length);
			for (int i = 0; i < array.Length; i += 4)
			{
				this.results[i >> 2] = Pack.LE_To_UInt32(array, i);
			}
			uint[] array2 = new uint[IsaacEngine.sizeL];
			for (int i = 0; i < IsaacEngine.sizeL; i++)
			{
				array2[i] = 2654435769U;
			}
			for (int i = 0; i < 4; i++)
			{
				this.mix(array2);
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < IsaacEngine.stateArraySize; j += IsaacEngine.sizeL)
				{
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						array2[k] += ((i < 1) ? this.results[j + k] : this.engineState[j + k]);
					}
					this.mix(array2);
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						this.engineState[j + k] = array2[k];
					}
				}
			}
			this.isaac();
			this.initialised = true;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x001458F0 File Offset: 0x00143AF0
		private void isaac()
		{
			uint num = this.b;
			uint num2 = this.c + 1U;
			this.c = num2;
			this.b = num + num2;
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				uint num3 = this.engineState[i];
				switch (i & 3)
				{
				case 0:
					this.a ^= this.a << 13;
					break;
				case 1:
					this.a ^= this.a >> 6;
					break;
				case 2:
					this.a ^= this.a << 2;
					break;
				case 3:
					this.a ^= this.a >> 16;
					break;
				}
				this.a += this.engineState[i + 128 & 255];
				uint num4 = this.engineState[i] = this.engineState[(int)(num3 >> 2 & 255U)] + this.a + this.b;
				this.results[i] = (this.b = this.engineState[(int)(num4 >> 10 & 255U)] + num3);
			}
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x00145A24 File Offset: 0x00143C24
		private void mix(uint[] x)
		{
			x[0] ^= x[1] << 11;
			x[3] += x[0];
			x[1] += x[2];
			x[1] ^= x[2] >> 2;
			x[4] += x[1];
			x[2] += x[3];
			x[2] ^= x[3] << 8;
			x[5] += x[2];
			x[3] += x[4];
			x[3] ^= x[4] >> 16;
			x[6] += x[3];
			x[4] += x[5];
			x[4] ^= x[5] << 10;
			x[7] += x[4];
			x[5] += x[6];
			x[5] ^= x[6] >> 4;
			x[0] += x[5];
			x[6] += x[7];
			x[6] ^= x[7] << 8;
			x[1] += x[6];
			x[7] += x[0];
			x[7] ^= x[0] >> 9;
			x[2] += x[7];
			x[0] += x[1];
		}

		// Token: 0x04002172 RID: 8562
		private static readonly int sizeL = 8;

		// Token: 0x04002173 RID: 8563
		private static readonly int stateArraySize = IsaacEngine.sizeL << 5;

		// Token: 0x04002174 RID: 8564
		private uint[] engineState;

		// Token: 0x04002175 RID: 8565
		private uint[] results;

		// Token: 0x04002176 RID: 8566
		private uint a;

		// Token: 0x04002177 RID: 8567
		private uint b;

		// Token: 0x04002178 RID: 8568
		private uint c;

		// Token: 0x04002179 RID: 8569
		private int index;

		// Token: 0x0400217A RID: 8570
		private byte[] keyStream = new byte[IsaacEngine.stateArraySize << 2];

		// Token: 0x0400217B RID: 8571
		private byte[] workingKey;

		// Token: 0x0400217C RID: 8572
		private bool initialised;
	}
}
