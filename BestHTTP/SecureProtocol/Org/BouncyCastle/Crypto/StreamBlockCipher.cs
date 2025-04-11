using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D8 RID: 984
	public class StreamBlockCipher : IStreamCipher
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x0010E3F4 File Offset: 0x0010C5F4
		public StreamBlockCipher(IBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			if (cipher.GetBlockSize() != 1)
			{
				throw new ArgumentException("block cipher block size != 1.", "cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0010E441 File Offset: 0x0010C641
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0010E450 File Offset: 0x0010C650
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0010E45D File Offset: 0x0010C65D
		public byte ReturnByte(byte input)
		{
			this.oneByte[0] = input;
			this.cipher.ProcessBlock(this.oneByte, 0, this.oneByte, 0);
			return this.oneByte[0];
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0010E48C File Offset: 0x0010C68C
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (outOff + length > output.Length)
			{
				throw new DataLengthException("output buffer too small in ProcessBytes()");
			}
			for (int num = 0; num != length; num++)
			{
				this.cipher.ProcessBlock(input, inOff + num, output, outOff + num);
			}
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0010E4D0 File Offset: 0x0010C6D0
		public void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x040019CF RID: 6607
		private readonly IBlockCipher cipher;

		// Token: 0x040019D0 RID: 6608
		private readonly byte[] oneByte = new byte[1];
	}
}
