using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000584 RID: 1412
	public class VmpcEngine : IStreamCipher
	{
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x00152133 File Offset: 0x00150333
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC";
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0015213C File Offset: 0x0015033C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC Init parameters must include an IV");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC Init parameters must include a key");
			}
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC requires 1 to 768 bytes of IV");
			}
			this.workingKey = keyParameter.GetKey();
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x001521E0 File Offset: 0x001503E0
		protected virtual void InitKey(byte[] keyBytes, byte[] ivBytes)
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

		// Token: 0x060035F0 RID: 13808 RVA: 0x00152334 File Offset: 0x00150534
		public virtual void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			for (int i = 0; i < len; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b2 = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
				this.n = (this.n + 1 & byte.MaxValue);
				output[i + outOff] = (input[i + inOff] ^ b);
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0015243E File Offset: 0x0015063E
		public virtual void Reset()
		{
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x00152454 File Offset: 0x00150654
		public virtual byte ReturnByte(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
			return input ^ b;
		}

		// Token: 0x04002255 RID: 8789
		protected byte n;

		// Token: 0x04002256 RID: 8790
		protected byte[] P;

		// Token: 0x04002257 RID: 8791
		protected byte s;

		// Token: 0x04002258 RID: 8792
		protected byte[] workingIV;

		// Token: 0x04002259 RID: 8793
		protected byte[] workingKey;
	}
}
