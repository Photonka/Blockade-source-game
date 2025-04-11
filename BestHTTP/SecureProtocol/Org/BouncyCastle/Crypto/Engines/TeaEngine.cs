using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000580 RID: 1408
	public class TeaEngine : IBlockCipher
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x0014E63C File Offset: 0x0014C83C
		public TeaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x0014E64B File Offset: 0x0014C84B
		public virtual string AlgorithmName
		{
			get
			{
				return "TEA";
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0014E654 File Offset: 0x0014C854
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to TEA init - " + Platform.GetTypeName(parameters));
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0014E6A0 File Offset: 0x0014C8A0
		public virtual int ProcessBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(inBytes, inOff, 8, "input buffer too short");
			Check.OutputLength(outBytes, outOff, 8, "output buffer too short");
			if (!this._forEncryption)
			{
				return this.decryptBlock(inBytes, inOff, outBytes, outOff);
			}
			return this.encryptBlock(inBytes, inOff, outBytes, outOff);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0014E705 File Offset: 0x0014C905
		private void setKey(byte[] key)
		{
			this._a = Pack.BE_To_UInt32(key, 0);
			this._b = Pack.BE_To_UInt32(key, 4);
			this._c = Pack.BE_To_UInt32(key, 8);
			this._d = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0014E73C File Offset: 0x0014C93C
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 0U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num3 += 2654435769U;
				num += ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num2 += ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0014E7C0 File Offset: 0x0014C9C0
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 3337565984U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num2 -= ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
				num -= ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num3 -= 2654435769U;
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x04002209 RID: 8713
		private const int rounds = 32;

		// Token: 0x0400220A RID: 8714
		private const int block_size = 8;

		// Token: 0x0400220B RID: 8715
		private const uint delta = 2654435769U;

		// Token: 0x0400220C RID: 8716
		private const uint d_sum = 3337565984U;

		// Token: 0x0400220D RID: 8717
		private uint _a;

		// Token: 0x0400220E RID: 8718
		private uint _b;

		// Token: 0x0400220F RID: 8719
		private uint _c;

		// Token: 0x04002210 RID: 8720
		private uint _d;

		// Token: 0x04002211 RID: 8721
		private bool _initialised;

		// Token: 0x04002212 RID: 8722
		private bool _forEncryption;
	}
}
