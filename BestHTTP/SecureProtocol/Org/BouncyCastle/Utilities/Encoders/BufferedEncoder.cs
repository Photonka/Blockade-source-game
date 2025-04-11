using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000275 RID: 629
	public class BufferedEncoder
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x000BA774 File Offset: 0x000B8974
		public BufferedEncoder(ITranslator translator, int bufferSize)
		{
			this.translator = translator;
			if (bufferSize % translator.GetEncodedBlockSize() != 0)
			{
				throw new ArgumentException("buffer size not multiple of input block size");
			}
			this.Buffer = new byte[bufferSize];
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000BA7A4 File Offset: 0x000B89A4
		public int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			int result = 0;
			byte[] buffer = this.Buffer;
			int num = this.bufOff;
			this.bufOff = num + 1;
			buffer[num] = input;
			if (this.bufOff == this.Buffer.Length)
			{
				result = this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
			}
			return result;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000BA804 File Offset: 0x000B8A04
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 0;
			int num2 = this.Buffer.Length - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				outOff += num;
				int num3 = len - len % this.Buffer.Length;
				num += this.translator.Encode(input, inOff, num3, outBytes, outOff);
				len -= num3;
				inOff += num3;
			}
			if (len != 0)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
				this.bufOff += len;
			}
			return num;
		}

		// Token: 0x040016E4 RID: 5860
		internal byte[] Buffer;

		// Token: 0x040016E5 RID: 5861
		internal int bufOff;

		// Token: 0x040016E6 RID: 5862
		internal ITranslator translator;
	}
}
