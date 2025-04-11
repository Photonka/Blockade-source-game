using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200056D RID: 1389
	public class RC4Engine : IStreamCipher
	{
		// Token: 0x060034CB RID: 13515 RVA: 0x0014748A File Offset: 0x0014568A
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.workingKey = ((KeyParameter)parameters).GetKey();
				this.SetKey(this.workingKey);
				return;
			}
			throw new ArgumentException("invalid parameter passed to RC4 init - " + Platform.GetTypeName(parameters));
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x001474C7 File Offset: 0x001456C7
		public virtual string AlgorithmName
		{
			get
			{
				return "RC4";
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x001474D0 File Offset: 0x001456D0
		public virtual byte ReturnByte(byte input)
		{
			this.x = (this.x + 1 & 255);
			this.y = ((int)this.engineState[this.x] + this.y & 255);
			byte b = this.engineState[this.x];
			this.engineState[this.x] = this.engineState[this.y];
			this.engineState[this.y] = b;
			return input ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)];
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x00147574 File Offset: 0x00145774
		public virtual void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, length, "input buffer too short");
			Check.OutputLength(output, outOff, length, "output buffer too short");
			for (int i = 0; i < length; i++)
			{
				this.x = (this.x + 1 & 255);
				this.y = ((int)this.engineState[this.x] + this.y & 255);
				byte b = this.engineState[this.x];
				this.engineState[this.x] = this.engineState[this.y];
				this.engineState[this.y] = b;
				output[i + outOff] = (input[i + inOff] ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)]);
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x0014764F File Offset: 0x0014584F
		public virtual void Reset()
		{
			this.SetKey(this.workingKey);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x00147660 File Offset: 0x00145860
		private void SetKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			this.x = 0;
			this.y = 0;
			if (this.engineState == null)
			{
				this.engineState = new byte[RC4Engine.STATE_LENGTH];
			}
			for (int i = 0; i < RC4Engine.STATE_LENGTH; i++)
			{
				this.engineState[i] = (byte)i;
			}
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < RC4Engine.STATE_LENGTH; j++)
			{
				num2 = ((int)((keyBytes[num] & byte.MaxValue) + this.engineState[j]) + num2 & 255);
				byte b = this.engineState[j];
				this.engineState[j] = this.engineState[num2];
				this.engineState[num2] = b;
				num = (num + 1) % keyBytes.Length;
			}
		}

		// Token: 0x04002197 RID: 8599
		private static readonly int STATE_LENGTH = 256;

		// Token: 0x04002198 RID: 8600
		private byte[] engineState;

		// Token: 0x04002199 RID: 8601
		private int x;

		// Token: 0x0400219A RID: 8602
		private int y;

		// Token: 0x0400219B RID: 8603
		private byte[] workingKey;
	}
}
