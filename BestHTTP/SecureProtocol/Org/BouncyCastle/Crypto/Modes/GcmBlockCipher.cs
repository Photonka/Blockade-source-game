using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000508 RID: 1288
	public sealed class GcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06003108 RID: 12552 RVA: 0x0012C439 File Offset: 0x0012A639
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x0012C444 File Offset: 0x0012A644
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16 + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x0012C4A2 File Offset: 0x0012A6A2
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCM";
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0012C4B9 File Offset: 0x0012A6B9
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0012C4C8 File Offset: 0x0012A6C8
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			this.initialised = true;
			byte[] iv;
			KeyParameter keyParameter;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				iv = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 32 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num);
				}
				this.macSize = num / 8;
				keyParameter = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to GCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				iv = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = 16;
				keyParameter = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (iv == null || iv.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (forEncryption && this.nonce != null && Arrays.AreEqual(this.nonce, iv))
			{
				if (keyParameter == null)
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
				if (this.lastKey != null && Arrays.AreEqual(this.lastKey, keyParameter.GetKey()))
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
			}
			this.nonce = iv;
			if (keyParameter != null)
			{
				this.lastKey = keyParameter.GetKey();
			}
			if (keyParameter != null)
			{
				this.cipher.Init(true, keyParameter);
				this.H = new byte[16];
				this.cipher.ProcessBlock(this.H, 0, this.H, 0);
				this.multiplier.Init(this.H);
				this.exp = null;
			}
			else if (this.H == null)
			{
				throw new ArgumentException("Key must be specified in initial init");
			}
			this.J0 = new byte[16];
			if (this.nonce.Length == 12)
			{
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.gHASH(this.J0, this.nonce, this.nonce.Length);
				byte[] array = new byte[16];
				Pack.UInt64_To_BE((ulong)((long)this.nonce.Length * 8L), array, 8);
				this.gHASHBlock(this.J0, array);
			}
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0012C79E File Offset: 0x0012A99E
		public byte[] GetMac()
		{
			if (this.macBlock != null)
			{
				return Arrays.Clone(this.macBlock);
			}
			return new byte[this.macSize];
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x0012C7C0 File Offset: 0x0012A9C0
		public int GetOutputSize(int len)
		{
			int num = len + this.bufOff;
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

		// Token: 0x06003110 RID: 12560 RVA: 0x0012C7FC File Offset: 0x0012A9FC
		public int GetUpdateOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % 16;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0012C834 File Offset: 0x0012AA34
		public void ProcessAadByte(byte input)
		{
			this.CheckStatus();
			this.atBlock[this.atBlockPos] = input;
			int num = this.atBlockPos + 1;
			this.atBlockPos = num;
			if (num == 16)
			{
				this.gHASHBlock(this.S_at, this.atBlock);
				this.atBlockPos = 0;
				this.atLength += 16UL;
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0012C894 File Offset: 0x0012AA94
		public void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			this.CheckStatus();
			for (int i = 0; i < len; i++)
			{
				this.atBlock[this.atBlockPos] = inBytes[inOff + i];
				int num = this.atBlockPos + 1;
				this.atBlockPos = num;
				if (num == 16)
				{
					this.gHASHBlock(this.S_at, this.atBlock);
					this.atBlockPos = 0;
					this.atLength += 16UL;
				}
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0012C904 File Offset: 0x0012AB04
		private void InitCipher()
		{
			if (this.atLength > 0UL)
			{
				Array.Copy(this.S_at, 0, this.S_atPre, 0, 16);
				this.atLengthPre = this.atLength;
			}
			if (this.atBlockPos > 0)
			{
				this.gHASHPartial(this.S_atPre, this.atBlock, 0, this.atBlockPos);
				this.atLengthPre += (ulong)this.atBlockPos;
			}
			if (this.atLengthPre > 0UL)
			{
				Array.Copy(this.S_atPre, 0, this.S, 0, 16);
			}
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0012C994 File Offset: 0x0012AB94
		public int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.CheckStatus();
			this.bufBlock[this.bufOff] = input;
			int num = this.bufOff + 1;
			this.bufOff = num;
			if (num == this.bufBlock.Length)
			{
				this.ProcessBlock(this.bufBlock, 0, output, outOff);
				if (this.forEncryption)
				{
					this.bufOff = 0;
				}
				else
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					this.bufOff = this.macSize;
				}
				return 16;
			}
			return 0;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0012CA1C File Offset: 0x0012AC1C
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			this.CheckStatus();
			Check.DataLength(input, inOff, len, "input buffer too short");
			int num = 0;
			if (this.forEncryption)
			{
				if (this.bufOff != 0)
				{
					while (len > 0)
					{
						len--;
						this.bufBlock[this.bufOff] = input[inOff++];
						int num2 = this.bufOff + 1;
						this.bufOff = num2;
						if (num2 == 16)
						{
							this.ProcessBlock(this.bufBlock, 0, output, outOff);
							this.bufOff = 0;
							num += 16;
							break;
						}
					}
				}
				while (len >= 16)
				{
					this.ProcessBlock(input, inOff, output, outOff + num);
					inOff += 16;
					len -= 16;
					num += 16;
				}
				if (len > 0)
				{
					Array.Copy(input, inOff, this.bufBlock, 0, len);
					this.bufOff = len;
				}
			}
			else
			{
				for (int i = 0; i < len; i++)
				{
					this.bufBlock[this.bufOff] = input[inOff + i];
					int num2 = this.bufOff + 1;
					this.bufOff = num2;
					if (num2 == this.bufBlock.Length)
					{
						this.ProcessBlock(this.bufBlock, 0, output, outOff + num);
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
						this.bufOff = this.macSize;
						num += 16;
					}
				}
			}
			return num;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0012CB64 File Offset: 0x0012AD64
		public int DoFinal(byte[] output, int outOff)
		{
			this.CheckStatus();
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			int num = this.bufOff;
			if (this.forEncryption)
			{
				Check.OutputLength(output, outOff, num + this.macSize, "Output buffer too short");
			}
			else
			{
				if (num < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num -= this.macSize;
				Check.OutputLength(output, outOff, num, "Output buffer too short");
			}
			if (num > 0)
			{
				this.ProcessPartial(this.bufBlock, 0, num, output, outOff);
			}
			this.atLength += (ulong)this.atBlockPos;
			if (this.atLength > this.atLengthPre)
			{
				if (this.atBlockPos > 0)
				{
					this.gHASHPartial(this.S_at, this.atBlock, 0, this.atBlockPos);
				}
				if (this.atLengthPre > 0UL)
				{
					GcmUtilities.Xor(this.S_at, this.S_atPre);
				}
				long pow = (long)(this.totalLength * 8UL + 127UL >> 7);
				byte[] array = new byte[16];
				if (this.exp == null)
				{
					this.exp = new Tables1kGcmExponentiator();
					this.exp.Init(this.H);
				}
				this.exp.ExponentiateX(pow, array);
				GcmUtilities.Multiply(this.S_at, array);
				GcmUtilities.Xor(this.S, this.S_at);
			}
			byte[] array2 = new byte[16];
			Pack.UInt64_To_BE(this.atLength * 8UL, array2, 0);
			Pack.UInt64_To_BE(this.totalLength * 8UL, array2, 8);
			this.gHASHBlock(this.S, array2);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new InvalidCipherTextException("mac check in GCM failed");
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0012CDAC File Offset: 0x0012AFAC
		public void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0012CDB8 File Offset: 0x0012AFB8
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Arrays.Fill(this.bufBlock, 0);
			}
			if (clearMac)
			{
				this.macBlock = null;
			}
			if (this.forEncryption)
			{
				this.initialised = false;
				return;
			}
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x0012CE90 File Offset: 0x0012B090
		private void ProcessBlock(byte[] buf, int bufOff, byte[] output, int outOff)
		{
			Check.OutputLength(output, outOff, 16, "Output buffer too short");
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			this.GetNextCtrBlock(this.ctrBlock);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(this.ctrBlock, buf, bufOff);
				this.gHASHBlock(this.S, this.ctrBlock);
				Array.Copy(this.ctrBlock, 0, output, outOff, 16);
			}
			else
			{
				this.gHASHBlock(this.S, buf, bufOff);
				GcmUtilities.Xor(this.ctrBlock, 0, buf, bufOff, output, outOff);
			}
			this.totalLength += 16UL;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x0012CF30 File Offset: 0x0012B130
		private void ProcessPartial(byte[] buf, int off, int len, byte[] output, int outOff)
		{
			this.GetNextCtrBlock(this.ctrBlock);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(buf, off, this.ctrBlock, 0, len);
				this.gHASHPartial(this.S, buf, off, len);
			}
			else
			{
				this.gHASHPartial(this.S, buf, off, len);
				GcmUtilities.Xor(buf, off, this.ctrBlock, 0, len);
			}
			Array.Copy(buf, off, output, outOff, len);
			this.totalLength += (ulong)len;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x0012CFAC File Offset: 0x0012B1AC
		private void gHASH(byte[] Y, byte[] b, int len)
		{
			for (int i = 0; i < len; i += 16)
			{
				int len2 = Math.Min(len - i, 16);
				this.gHASHPartial(Y, b, i, len2);
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0012CFDB File Offset: 0x0012B1DB
		private void gHASHBlock(byte[] Y, byte[] b)
		{
			GcmUtilities.Xor(Y, b);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x0012CFF0 File Offset: 0x0012B1F0
		private void gHASHBlock(byte[] Y, byte[] b, int off)
		{
			GcmUtilities.Xor(Y, b, off);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x0012D006 File Offset: 0x0012B206
		private void gHASHPartial(byte[] Y, byte[] b, int off, int len)
		{
			GcmUtilities.Xor(Y, b, off, len);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x0012D020 File Offset: 0x0012B220
		private void GetNextCtrBlock(byte[] block)
		{
			if (this.blocksRemaining == 0U)
			{
				throw new InvalidOperationException("Attempt to process too many blocks");
			}
			this.blocksRemaining -= 1U;
			uint num = 1U;
			num += (uint)this.counter[15];
			this.counter[15] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[14];
			this.counter[14] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[13];
			this.counter[13] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[12];
			this.counter[12] = (byte)num;
			this.cipher.ProcessBlock(this.counter, 0, block, 0);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x0012D0CD File Offset: 0x0012B2CD
		private void CheckStatus()
		{
			if (this.initialised)
			{
				return;
			}
			if (this.forEncryption)
			{
				throw new InvalidOperationException("GCM cipher cannot be reused for encryption");
			}
			throw new InvalidOperationException("GCM cipher needs to be initialised");
		}

		// Token: 0x04001F47 RID: 8007
		private const int BlockSize = 16;

		// Token: 0x04001F48 RID: 8008
		private byte[] ctrBlock = new byte[16];

		// Token: 0x04001F49 RID: 8009
		private readonly IBlockCipher cipher;

		// Token: 0x04001F4A RID: 8010
		private readonly IGcmMultiplier multiplier;

		// Token: 0x04001F4B RID: 8011
		private IGcmExponentiator exp;

		// Token: 0x04001F4C RID: 8012
		private bool forEncryption;

		// Token: 0x04001F4D RID: 8013
		private bool initialised;

		// Token: 0x04001F4E RID: 8014
		private int macSize;

		// Token: 0x04001F4F RID: 8015
		private byte[] lastKey;

		// Token: 0x04001F50 RID: 8016
		private byte[] nonce;

		// Token: 0x04001F51 RID: 8017
		private byte[] initialAssociatedText;

		// Token: 0x04001F52 RID: 8018
		private byte[] H;

		// Token: 0x04001F53 RID: 8019
		private byte[] J0;

		// Token: 0x04001F54 RID: 8020
		private byte[] bufBlock;

		// Token: 0x04001F55 RID: 8021
		private byte[] macBlock;

		// Token: 0x04001F56 RID: 8022
		private byte[] S;

		// Token: 0x04001F57 RID: 8023
		private byte[] S_at;

		// Token: 0x04001F58 RID: 8024
		private byte[] S_atPre;

		// Token: 0x04001F59 RID: 8025
		private byte[] counter;

		// Token: 0x04001F5A RID: 8026
		private uint blocksRemaining;

		// Token: 0x04001F5B RID: 8027
		private int bufOff;

		// Token: 0x04001F5C RID: 8028
		private ulong totalLength;

		// Token: 0x04001F5D RID: 8029
		private byte[] atBlock;

		// Token: 0x04001F5E RID: 8030
		private int atBlockPos;

		// Token: 0x04001F5F RID: 8031
		private ulong atLength;

		// Token: 0x04001F60 RID: 8032
		private ulong atLengthPre;
	}
}
