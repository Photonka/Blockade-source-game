using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003AF RID: 943
	public class BufferedAsymmetricBlockCipher : BufferedCipherBase
	{
		// Token: 0x06002750 RID: 10064 RVA: 0x0010D804 File Offset: 0x0010BA04
		public BufferedAsymmetricBlockCipher(IAsymmetricBlockCipher cipher)
		{
			this.cipher = cipher;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0010D813 File Offset: 0x0010BA13
		internal int GetBufferPosition()
		{
			return this.bufOff;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x0010D81B File Offset: 0x0010BA1B
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0010D828 File Offset: 0x0010BA28
		public override int GetBlockSize()
		{
			return this.cipher.GetInputBlockSize();
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0010D835 File Offset: 0x0010BA35
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputBlockSize();
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int GetUpdateOutputSize(int length)
		{
			return 0;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0010D842 File Offset: 0x0010BA42
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(forEncryption, parameters);
			this.buffer = new byte[this.cipher.GetInputBlockSize() + (forEncryption ? 1 : 0)];
			this.bufOff = 0;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0010D87C File Offset: 0x0010BA7C
		public override byte[] ProcessByte(byte input)
		{
			if (this.bufOff >= this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			byte[] array = this.buffer;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
			return null;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0010D8C0 File Offset: 0x0010BAC0
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (this.bufOff + length > this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			Array.Copy(input, inOff, this.buffer, this.bufOff, length);
			this.bufOff += length;
			return null;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0010D921 File Offset: 0x0010BB21
		public override byte[] DoFinal()
		{
			byte[] result = (this.bufOff > 0) ? this.cipher.ProcessBlock(this.buffer, 0, this.bufOff) : BufferedCipherBase.EmptyBuffer;
			this.Reset();
			return result;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0010D951 File Offset: 0x0010BB51
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0010D963 File Offset: 0x0010BB63
		public override void Reset()
		{
			if (this.buffer != null)
			{
				Array.Clear(this.buffer, 0, this.buffer.Length);
				this.bufOff = 0;
			}
		}

		// Token: 0x040019BA RID: 6586
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x040019BB RID: 6587
		private byte[] buffer;

		// Token: 0x040019BC RID: 6588
		private int bufOff;
	}
}
