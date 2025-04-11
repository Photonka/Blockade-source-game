using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000522 RID: 1314
	public class ISO9797Alg3Mac : IMac
	{
		// Token: 0x06003226 RID: 12838 RVA: 0x0013213E File Offset: 0x0013033E
		public ISO9797Alg3Mac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8, null)
		{
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x00132150 File Offset: 0x00130350
		public ISO9797Alg3Mac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8, padding)
		{
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00132162 File Offset: 0x00130362
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x00132170 File Offset: 0x00130370
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (!(cipher is DesEngine))
			{
				throw new ArgumentException("cipher must be instance of DesEngine");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x001321EB File Offset: 0x001303EB
		public string AlgorithmName
		{
			get
			{
				return "ISO9797Alg3";
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x001321F4 File Offset: 0x001303F4
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			if (!(parameters is KeyParameter) && !(parameters is ParametersWithIV))
			{
				throw new ArgumentException("parameters must be an instance of KeyParameter or ParametersWithIV");
			}
			KeyParameter keyParameter;
			if (parameters is KeyParameter)
			{
				keyParameter = (KeyParameter)parameters;
			}
			else
			{
				keyParameter = (KeyParameter)((ParametersWithIV)parameters).Parameters;
			}
			byte[] key = keyParameter.GetKey();
			KeyParameter parameters2;
			if (key.Length == 16)
			{
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = parameters2;
			}
			else
			{
				if (key.Length != 24)
				{
					throw new ArgumentException("Key must be either 112 or 168 bit long");
				}
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = new KeyParameter(key, 16, 8);
			}
			if (parameters is ParametersWithIV)
			{
				this.cipher.Init(true, new ParametersWithIV(parameters2, ((ParametersWithIV)parameters).GetIV()));
				return;
			}
			this.cipher.Init(true, parameters2);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x001322DF File Offset: 0x001304DF
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x001322E8 File Offset: 0x001304E8
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			byte[] array = this.buf;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00132340 File Offset: 0x00130540
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x00132404 File Offset: 0x00130604
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					byte[] array = this.buf;
					int num = this.bufOff;
					this.bufOff = num + 1;
					array[num] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			DesEngine desEngine = new DesEngine();
			desEngine.Init(false, this.lastKey2);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			desEngine.Init(true, this.lastKey3);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x0013250F File Offset: 0x0013070F
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001FF3 RID: 8179
		private byte[] mac;

		// Token: 0x04001FF4 RID: 8180
		private byte[] buf;

		// Token: 0x04001FF5 RID: 8181
		private int bufOff;

		// Token: 0x04001FF6 RID: 8182
		private IBlockCipher cipher;

		// Token: 0x04001FF7 RID: 8183
		private IBlockCipherPadding padding;

		// Token: 0x04001FF8 RID: 8184
		private int macSize;

		// Token: 0x04001FF9 RID: 8185
		private KeyParameter lastKey2;

		// Token: 0x04001FFA RID: 8186
		private KeyParameter lastKey3;
	}
}
