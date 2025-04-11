using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000526 RID: 1318
	public class VmpcMac : IMac
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x0013343C File Offset: 0x0013163C
		public virtual int DoFinal(byte[] output, int outOff)
		{
			for (int i = 1; i < 25; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				this.x4 = this.P[(int)(this.x4 + this.x3) + i & 255];
				this.x3 = this.P[(int)(this.x3 + this.x2) + i & 255];
				this.x2 = this.P[(int)(this.x2 + this.x1) + i & 255];
				this.x1 = this.P[(int)(this.x1 + this.s) + i & 255];
				this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
				this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
				this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
				this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
				this.g = (this.g + 4 & 31);
				byte b = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
				this.n = (this.n + 1 & byte.MaxValue);
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + this.T[j & 31] & byte.MaxValue)];
				byte b2 = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			byte[] array = new byte[20];
			for (int k = 0; k < 20; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] & byte.MaxValue)];
				array[k] = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b3 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			Array.Copy(array, 0, output, outOff, array.Length);
			this.Reset();
			return array.Length;
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x00133797 File Offset: 0x00131997
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC-MAC";
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x0013379E File Offset: 0x0013199E
		public virtual int GetMacSize()
		{
			return 20;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001337A4 File Offset: 0x001319A4
		public virtual void Init(ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include an IV", "parameters");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include a key", "parameters");
			}
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC-MAC requires 1 to 768 bytes of IV", "parameters");
			}
			this.workingKey = keyParameter.GetKey();
			this.Reset();
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0013384C File Offset: 0x00131A4C
		private void initKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			this.n = 0;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x001339A0 File Offset: 0x00131BA0
		public virtual void Reset()
		{
			this.initKey(this.workingKey, this.workingIV);
			this.g = (this.x1 = (this.x2 = (this.x3 = (this.x4 = (this.n = 0)))));
			this.T = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				this.T[i] = 0;
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00133A18 File Offset: 0x00131C18
		public virtual void Update(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = input ^ this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			this.x4 = this.P[(int)(this.x4 + this.x3 & byte.MaxValue)];
			this.x3 = this.P[(int)(this.x3 + this.x2 & byte.MaxValue)];
			this.x2 = this.P[(int)(this.x2 + this.x1 & byte.MaxValue)];
			this.x1 = this.P[(int)(this.x1 + this.s + b & byte.MaxValue)];
			this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
			this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
			this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
			this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
			this.g = (this.g + 4 & 31);
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x00133C28 File Offset: 0x00131E28
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < len; i++)
			{
				this.Update(input[inOff + i]);
			}
		}

		// Token: 0x04002021 RID: 8225
		private byte g;

		// Token: 0x04002022 RID: 8226
		private byte n;

		// Token: 0x04002023 RID: 8227
		private byte[] P;

		// Token: 0x04002024 RID: 8228
		private byte s;

		// Token: 0x04002025 RID: 8229
		private byte[] T;

		// Token: 0x04002026 RID: 8230
		private byte[] workingIV;

		// Token: 0x04002027 RID: 8231
		private byte[] workingKey;

		// Token: 0x04002028 RID: 8232
		private byte x1;

		// Token: 0x04002029 RID: 8233
		private byte x2;

		// Token: 0x0400202A RID: 8234
		private byte x3;

		// Token: 0x0400202B RID: 8235
		private byte x4;
	}
}
