using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200055C RID: 1372
	public class DesEdeEngine : DesEngine
	{
		// Token: 0x06003402 RID: 13314 RVA: 0x0014105C File Offset: 0x0013F25C
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to DESede init - " + Platform.GetTypeName(parameters));
			}
			byte[] key = ((KeyParameter)parameters).GetKey();
			if (key.Length != 24 && key.Length != 16)
			{
				throw new ArgumentException("key size must be 16 or 24 bytes.");
			}
			this.forEncryption = forEncryption;
			byte[] array = new byte[8];
			Array.Copy(key, 0, array, 0, array.Length);
			this.workingKey1 = DesEngine.GenerateWorkingKey(forEncryption, array);
			byte[] array2 = new byte[8];
			Array.Copy(key, 8, array2, 0, array2.Length);
			this.workingKey2 = DesEngine.GenerateWorkingKey(!forEncryption, array2);
			if (key.Length == 24)
			{
				byte[] array3 = new byte[8];
				Array.Copy(key, 16, array3, 0, array3.Length);
				this.workingKey3 = DesEngine.GenerateWorkingKey(forEncryption, array3);
				return;
			}
			this.workingKey3 = this.workingKey1;
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x0014112B File Offset: 0x0013F32B
		public override string AlgorithmName
		{
			get
			{
				return "DESede";
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000FE681 File Offset: 0x000FC881
		public override int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00141134 File Offset: 0x0013F334
		public override int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey1 == null)
			{
				throw new InvalidOperationException("DESede engine not initialised");
			}
			Check.DataLength(input, inOff, 8, "input buffer too short");
			Check.OutputLength(output, outOff, 8, "output buffer too short");
			byte[] array = new byte[8];
			if (this.forEncryption)
			{
				DesEngine.DesFunc(this.workingKey1, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey3, array, 0, output, outOff);
			}
			else
			{
				DesEngine.DesFunc(this.workingKey3, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey1, array, 0, output, outOff);
			}
			return 8;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Reset()
		{
		}

		// Token: 0x04002111 RID: 8465
		private int[] workingKey1;

		// Token: 0x04002112 RID: 8466
		private int[] workingKey2;

		// Token: 0x04002113 RID: 8467
		private int[] workingKey3;

		// Token: 0x04002114 RID: 8468
		private bool forEncryption;
	}
}
