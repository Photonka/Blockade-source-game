using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000587 RID: 1415
	public class XteaEngine : IBlockCipher
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x00152858 File Offset: 0x00150A58
		public XteaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x0015288D File Offset: 0x00150A8D
		public virtual string AlgorithmName
		{
			get
			{
				return "XTEA";
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x00152894 File Offset: 0x00150A94
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

		// Token: 0x06003600 RID: 13824 RVA: 0x001528E0 File Offset: 0x00150AE0
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

		// Token: 0x06003601 RID: 13825 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x00152948 File Offset: 0x00150B48
		private void setKey(byte[] key)
		{
			int i;
			int num = i = 0;
			while (i < 4)
			{
				this._S[i] = Pack.BE_To_UInt32(key, num);
				i++;
				num += 4;
			}
			num = (i = 0);
			while (i < 32)
			{
				this._sum0[i] = (uint)(num + (int)this._S[num & 3]);
				num += -1640531527;
				this._sum1[i] = (uint)(num + (int)this._S[num >> 11 & 3]);
				i++;
			}
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x001529B8 File Offset: 0x00150BB8
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 0; i < 32; i++)
			{
				num += ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
				num2 += ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x00152A24 File Offset: 0x00150C24
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 31; i >= 0; i--)
			{
				num2 -= ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
				num -= ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x0400225A RID: 8794
		private const int rounds = 32;

		// Token: 0x0400225B RID: 8795
		private const int block_size = 8;

		// Token: 0x0400225C RID: 8796
		private const int delta = -1640531527;

		// Token: 0x0400225D RID: 8797
		private uint[] _S = new uint[4];

		// Token: 0x0400225E RID: 8798
		private uint[] _sum0 = new uint[32];

		// Token: 0x0400225F RID: 8799
		private uint[] _sum1 = new uint[32];

		// Token: 0x04002260 RID: 8800
		private bool _initialised;

		// Token: 0x04002261 RID: 8801
		private bool _forEncryption;
	}
}
