using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B3 RID: 947
	public class BufferedStreamCipher : BufferedCipherBase
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x0010E052 File Offset: 0x0010C252
		public BufferedStreamCipher(IStreamCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0010E06F File Offset: 0x0010C26F
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0010E07C File Offset: 0x0010C27C
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000A6AED File Offset: 0x000A4CED
		public override int GetOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000A6AED File Offset: 0x000A4CED
		public override int GetUpdateOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0010E0A0 File Offset: 0x0010C2A0
		public override byte[] ProcessByte(byte input)
		{
			return new byte[]
			{
				this.cipher.ReturnByte(input)
			};
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x0010E0B7 File Offset: 0x0010C2B7
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			if (outOff >= output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			output[outOff] = this.cipher.ReturnByte(input);
			return 1;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0010E0DC File Offset: 0x0010C2DC
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			byte[] array = new byte[length];
			this.cipher.ProcessBytes(input, inOff, length, array, 0);
			return array;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0010E107 File Offset: 0x0010C307
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 1)
			{
				return 0;
			}
			if (length > 0)
			{
				this.cipher.ProcessBytes(input, inOff, length, output, outOff);
			}
			return length;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0010E126 File Offset: 0x0010C326
		public override byte[] DoFinal()
		{
			this.Reset();
			return BufferedCipherBase.EmptyBuffer;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0010E133 File Offset: 0x0010C333
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return BufferedCipherBase.EmptyBuffer;
			}
			byte[] result = this.ProcessBytes(input, inOff, length);
			this.Reset();
			return result;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0010E14E File Offset: 0x0010C34E
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x040019C5 RID: 6597
		private readonly IStreamCipher cipher;
	}
}
