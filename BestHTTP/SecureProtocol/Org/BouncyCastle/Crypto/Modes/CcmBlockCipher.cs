using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000504 RID: 1284
	public class CcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x060030D1 RID: 12497 RVA: 0x0012AF44 File Offset: 0x00129144
		public CcmBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.macBlock = new byte[CcmBlockCipher.BlockSize];
			if (cipher.GetBlockSize() != CcmBlockCipher.BlockSize)
			{
				throw new ArgumentException("cipher required with a block size of " + CcmBlockCipher.BlockSize + ".");
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0012AFB0 File Offset: 0x001291B0
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0012AFB8 File Offset: 0x001291B8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			ICipherParameters cipherParameters;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				this.macSize = aeadParameters.MacSize / 8;
				cipherParameters = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to CCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = this.macBlock.Length / 2;
				cipherParameters = parametersWithIV.Parameters;
			}
			if (cipherParameters != null)
			{
				this.keyParam = cipherParameters;
			}
			if (this.nonce == null || this.nonce.Length < 7 || this.nonce.Length > 13)
			{
				throw new ArgumentException("nonce must have length from 7 to 13 octets");
			}
			this.Reset();
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x0012B08A File Offset: 0x0012928A
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CCM";
			}
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0012B0A1 File Offset: 0x001292A1
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0012B0AE File Offset: 0x001292AE
		public virtual void ProcessAadByte(byte input)
		{
			this.associatedText.WriteByte(input);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0012B0BC File Offset: 0x001292BC
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			this.associatedText.Write(inBytes, inOff, len);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0012B0CC File Offset: 0x001292CC
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.data.WriteByte(input);
			return 0;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0012B0DB File Offset: 0x001292DB
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int inLen, byte[] outBytes, int outOff)
		{
			Check.DataLength(inBytes, inOff, inLen, "Input buffer too short");
			this.data.Write(inBytes, inOff, inLen);
			return 0;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0012B0FC File Offset: 0x001292FC
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			byte[] buffer = this.data.GetBuffer();
			int inLen = (int)this.data.Position;
			int result = this.ProcessPacket(buffer, 0, inLen, outBytes, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0012B133 File Offset: 0x00129333
		public virtual void Reset()
		{
			this.cipher.Reset();
			this.associatedText.SetLength(0L);
			this.data.SetLength(0L);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0012B15A File Offset: 0x0012935A
		public virtual byte[] GetMac()
		{
			return Arrays.CopyOfRange(this.macBlock, 0, this.macSize);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int GetUpdateOutputSize(int len)
		{
			return 0;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0012B170 File Offset: 0x00129370
		public virtual int GetOutputSize(int len)
		{
			int num = (int)this.data.Length + len;
			if (this.forEncryption)
			{
				return num + this.macSize;
			}
			if (num >= this.macSize)
			{
				return num - this.macSize;
			}
			return 0;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0012B1B0 File Offset: 0x001293B0
		public virtual byte[] ProcessPacket(byte[] input, int inOff, int inLen)
		{
			byte[] array;
			if (this.forEncryption)
			{
				array = new byte[inLen + this.macSize];
			}
			else
			{
				if (inLen < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				array = new byte[inLen - this.macSize];
			}
			this.ProcessPacket(input, inOff, inLen, array, 0);
			return array;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0012B204 File Offset: 0x00129404
		public virtual int ProcessPacket(byte[] input, int inOff, int inLen, byte[] output, int outOff)
		{
			if (this.keyParam == null)
			{
				throw new InvalidOperationException("CCM cipher unitialized.");
			}
			int num = this.nonce.Length;
			int num2 = 15 - num;
			if (num2 < 4)
			{
				int num3 = 1 << 8 * num2;
				if (inLen >= num3)
				{
					throw new InvalidOperationException("CCM packet too large for choice of q.");
				}
			}
			byte[] array = new byte[CcmBlockCipher.BlockSize];
			array[0] = (byte)(num2 - 1 & 7);
			this.nonce.CopyTo(array, 1);
			IBlockCipher blockCipher = new SicBlockCipher(this.cipher);
			blockCipher.Init(this.forEncryption, new ParametersWithIV(this.keyParam, array));
			int i = inOff;
			int num4 = outOff;
			int num5;
			if (this.forEncryption)
			{
				num5 = inLen + this.macSize;
				Check.OutputLength(output, outOff, num5, "Output buffer too short.");
				this.CalculateMac(input, inOff, inLen, this.macBlock);
				byte[] array2 = new byte[CcmBlockCipher.BlockSize];
				blockCipher.ProcessBlock(this.macBlock, 0, array2, 0);
				while (i < inOff + inLen - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, i, output, num4);
					num4 += CcmBlockCipher.BlockSize;
					i += CcmBlockCipher.BlockSize;
				}
				byte[] array3 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, i, array3, 0, inLen + inOff - i);
				blockCipher.ProcessBlock(array3, 0, array3, 0);
				Array.Copy(array3, 0, output, num4, inLen + inOff - i);
				Array.Copy(array2, 0, output, outOff + inLen, this.macSize);
			}
			else
			{
				if (inLen < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num5 = inLen - this.macSize;
				Check.OutputLength(output, outOff, num5, "Output buffer too short.");
				Array.Copy(input, inOff + num5, this.macBlock, 0, this.macSize);
				blockCipher.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				for (int num6 = this.macSize; num6 != this.macBlock.Length; num6++)
				{
					this.macBlock[num6] = 0;
				}
				while (i < inOff + num5 - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, i, output, num4);
					num4 += CcmBlockCipher.BlockSize;
					i += CcmBlockCipher.BlockSize;
				}
				byte[] array4 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, i, array4, 0, num5 - (i - inOff));
				blockCipher.ProcessBlock(array4, 0, array4, 0);
				Array.Copy(array4, 0, output, num4, num5 - (i - inOff));
				byte[] b = new byte[CcmBlockCipher.BlockSize];
				this.CalculateMac(output, outOff, num5, b);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, b))
				{
					throw new InvalidCipherTextException("mac check in CCM failed");
				}
			}
			return num5;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0012B49C File Offset: 0x0012969C
		private int CalculateMac(byte[] data, int dataOff, int dataLen, byte[] macBlock)
		{
			IMac mac = new CbcBlockCipherMac(this.cipher, this.macSize * 8);
			mac.Init(this.keyParam);
			byte[] array = new byte[16];
			if (this.HasAssociatedText())
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] |= 64;
			}
			byte[] array3 = array;
			int num2 = 0;
			array3[num2] |= (byte)(((mac.GetMacSize() - 2) / 2 & 7) << 3);
			byte[] array4 = array;
			int num3 = 0;
			array4[num3] |= (byte)(15 - this.nonce.Length - 1 & 7);
			Array.Copy(this.nonce, 0, array, 1, this.nonce.Length);
			int i = dataLen;
			int num4 = 1;
			while (i > 0)
			{
				array[array.Length - num4] = (byte)(i & 255);
				i >>= 8;
				num4++;
			}
			mac.BlockUpdate(array, 0, array.Length);
			if (this.HasAssociatedText())
			{
				int associatedTextLength = this.GetAssociatedTextLength();
				int num5;
				if (associatedTextLength < 65280)
				{
					mac.Update((byte)(associatedTextLength >> 8));
					mac.Update((byte)associatedTextLength);
					num5 = 2;
				}
				else
				{
					mac.Update(byte.MaxValue);
					mac.Update(254);
					mac.Update((byte)(associatedTextLength >> 24));
					mac.Update((byte)(associatedTextLength >> 16));
					mac.Update((byte)(associatedTextLength >> 8));
					mac.Update((byte)associatedTextLength);
					num5 = 6;
				}
				if (this.initialAssociatedText != null)
				{
					mac.BlockUpdate(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
				}
				if (this.associatedText.Position > 0L)
				{
					byte[] buffer = this.associatedText.GetBuffer();
					int len = (int)this.associatedText.Position;
					mac.BlockUpdate(buffer, 0, len);
				}
				num5 = (num5 + associatedTextLength) % 16;
				if (num5 != 0)
				{
					for (int j = num5; j < 16; j++)
					{
						mac.Update(0);
					}
				}
			}
			mac.BlockUpdate(data, dataOff, dataLen);
			return mac.DoFinal(macBlock, 0);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0012B665 File Offset: 0x00129865
		private int GetAssociatedTextLength()
		{
			return (int)this.associatedText.Length + ((this.initialAssociatedText == null) ? 0 : this.initialAssociatedText.Length);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0012B687 File Offset: 0x00129887
		private bool HasAssociatedText()
		{
			return this.GetAssociatedTextLength() > 0;
		}

		// Token: 0x04001F2A RID: 7978
		private static readonly int BlockSize = 16;

		// Token: 0x04001F2B RID: 7979
		private readonly IBlockCipher cipher;

		// Token: 0x04001F2C RID: 7980
		private readonly byte[] macBlock;

		// Token: 0x04001F2D RID: 7981
		private bool forEncryption;

		// Token: 0x04001F2E RID: 7982
		private byte[] nonce;

		// Token: 0x04001F2F RID: 7983
		private byte[] initialAssociatedText;

		// Token: 0x04001F30 RID: 7984
		private int macSize;

		// Token: 0x04001F31 RID: 7985
		private ICipherParameters keyParam;

		// Token: 0x04001F32 RID: 7986
		private readonly MemoryStream associatedText = new MemoryStream();

		// Token: 0x04001F33 RID: 7987
		private readonly MemoryStream data = new MemoryStream();
	}
}
