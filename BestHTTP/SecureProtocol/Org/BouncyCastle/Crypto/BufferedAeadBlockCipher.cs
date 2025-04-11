using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003AE RID: 942
	public class BufferedAeadBlockCipher : BufferedCipherBase
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x0010D5FF File Offset: 0x0010B7FF
		public BufferedAeadBlockCipher(IAeadBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0010D61C File Offset: 0x0010B81C
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0010D629 File Offset: 0x0010B829
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0010D64D File Offset: 0x0010B84D
		public override int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0010D65A File Offset: 0x0010B85A
		public override int GetUpdateOutputSize(int length)
		{
			return this.cipher.GetUpdateOutputSize(length);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0010D668 File Offset: 0x0010B868
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputSize(length);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0010D676 File Offset: 0x0010B876
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.cipher.ProcessByte(input, output, outOff);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0010D688 File Offset: 0x0010B888
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

		// Token: 0x0600274A RID: 10058 RVA: 0x0010D6D4 File Offset: 0x0010B8D4
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

		// Token: 0x0600274B RID: 10059 RVA: 0x0010D733 File Offset: 0x0010B933
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			return this.cipher.ProcessBytes(input, inOff, length, output, outOff);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0010D748 File Offset: 0x0010B948
		public override byte[] DoFinal()
		{
			byte[] array = new byte[this.GetOutputSize(0)];
			int num = this.DoFinal(array, 0);
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x0010D788 File Offset: 0x0010B988
		public override byte[] DoFinal(byte[] input, int inOff, int inLen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			byte[] array = new byte[this.GetOutputSize(inLen)];
			int num = (inLen > 0) ? this.ProcessBytes(input, inOff, inLen, array, 0) : 0;
			num += this.DoFinal(array, num);
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0010D7E8 File Offset: 0x0010B9E8
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.cipher.DoFinal(output, outOff);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0010D7F7 File Offset: 0x0010B9F7
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x040019B9 RID: 6585
		private readonly IAeadBlockCipher cipher;
	}
}
