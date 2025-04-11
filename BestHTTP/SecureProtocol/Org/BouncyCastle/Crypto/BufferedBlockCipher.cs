using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B0 RID: 944
	public class BufferedBlockCipher : BufferedCipherBase
	{
		// Token: 0x0600275C RID: 10076 RVA: 0x0010D988 File Offset: 0x0010BB88
		protected BufferedBlockCipher()
		{
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0010D990 File Offset: 0x0010BB90
		public BufferedBlockCipher(IBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0010D9C5 File Offset: 0x0010BBC5
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0010D9D4 File Offset: 0x0010BBD4
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			ParametersWithRandom parametersWithRandom = parameters as ParametersWithRandom;
			if (parametersWithRandom != null)
			{
				parameters = parametersWithRandom.Parameters;
			}
			this.Reset();
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0010DA0D File Offset: 0x0010BC0D
		public override int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0010DA1C File Offset: 0x0010BC1C
		public override int GetUpdateOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			return num - num2;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0010DA3E File Offset: 0x0010BC3E
		public override int GetOutputSize(int length)
		{
			return length + this.bufOff;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0010DA48 File Offset: 0x0010BC48
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			byte[] array = this.buf;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
			if (this.bufOff != this.buf.Length)
			{
				return 0;
			}
			if (outOff + this.buf.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.bufOff = 0;
			return this.cipher.ProcessBlock(this.buf, 0, output, outOff);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0010DAB8 File Offset: 0x0010BCB8
		public override byte[] ProcessByte(byte input)
		{
			int updateOutputSize = this.GetUpdateOutputSize(1);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessByte(input, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0010DB04 File Offset: 0x0010BD04
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (length < 1)
			{
				return null;
			}
			int updateOutputSize = this.GetUpdateOutputSize(length);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessBytes(input, inOff, length, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0010DB64 File Offset: 0x0010BD64
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length >= 1)
			{
				int blockSize = this.GetBlockSize();
				int updateOutputSize = this.GetUpdateOutputSize(length);
				if (updateOutputSize > 0)
				{
					Check.OutputLength(output, outOff, updateOutputSize, "output buffer too short");
				}
				int num = 0;
				int num2 = this.buf.Length - this.bufOff;
				if (length > num2)
				{
					Array.Copy(input, inOff, this.buf, this.bufOff, num2);
					num += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
					this.bufOff = 0;
					length -= num2;
					inOff += num2;
					while (length > this.buf.Length)
					{
						num += this.cipher.ProcessBlock(input, inOff, output, outOff + num);
						length -= blockSize;
						inOff += blockSize;
					}
				}
				Array.Copy(input, inOff, this.buf, this.bufOff, length);
				this.bufOff += length;
				if (this.bufOff == this.buf.Length)
				{
					num += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num);
					this.bufOff = 0;
				}
				return num;
			}
			if (length < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			return 0;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0010DC80 File Offset: 0x0010BE80
		public override byte[] DoFinal()
		{
			byte[] array = BufferedCipherBase.EmptyBuffer;
			int outputSize = this.GetOutputSize(0);
			if (outputSize > 0)
			{
				array = new byte[outputSize];
				int num = this.DoFinal(array, 0);
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Array.Copy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			else
			{
				this.Reset();
			}
			return array;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0010DCD4 File Offset: 0x0010BED4
		public override byte[] DoFinal(byte[] input, int inOff, int inLen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int outputSize = this.GetOutputSize(inLen);
			byte[] array = BufferedCipherBase.EmptyBuffer;
			if (outputSize > 0)
			{
				array = new byte[outputSize];
				int num = (inLen > 0) ? this.ProcessBytes(input, inOff, inLen, array, 0) : 0;
				num += this.DoFinal(array, num);
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Array.Copy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			else
			{
				this.Reset();
			}
			return array;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0010DD48 File Offset: 0x0010BF48
		public override int DoFinal(byte[] output, int outOff)
		{
			int result;
			try
			{
				if (this.bufOff != 0)
				{
					Check.DataLength(!this.cipher.IsPartialBlockOkay, "data not block size aligned");
					Check.OutputLength(output, outOff, this.bufOff, "output buffer too short for DoFinal()");
					this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
					Array.Copy(this.buf, 0, output, outOff, this.bufOff);
				}
				result = this.bufOff;
			}
			finally
			{
				this.Reset();
			}
			return result;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0010DDD8 File Offset: 0x0010BFD8
		public override void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x040019BD RID: 6589
		internal byte[] buf;

		// Token: 0x040019BE RID: 6590
		internal int bufOff;

		// Token: 0x040019BF RID: 6591
		internal bool forEncryption;

		// Token: 0x040019C0 RID: 6592
		internal IBlockCipher cipher;
	}
}
