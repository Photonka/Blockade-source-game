using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051F RID: 1311
	public class GMac : IMac
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x00131739 File Offset: 0x0012F939
		public GMac(GcmBlockCipher cipher) : this(cipher, 128)
		{
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00131747 File Offset: 0x0012F947
		public GMac(GcmBlockCipher cipher, int macSizeBits)
		{
			this.cipher = cipher;
			this.macSizeBits = macSizeBits;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00131760 File Offset: 0x0012F960
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				KeyParameter key = (KeyParameter)parametersWithIV.Parameters;
				this.cipher.Init(true, new AeadParameters(key, this.macSizeBits, iv));
				return;
			}
			throw new ArgumentException("GMAC requires ParametersWithIV");
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x001317B1 File Offset: 0x0012F9B1
		public string AlgorithmName
		{
			get
			{
				return this.cipher.GetUnderlyingCipher().AlgorithmName + "-GMAC";
			}
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x001317CD File Offset: 0x0012F9CD
		public int GetMacSize()
		{
			return this.macSizeBits / 8;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x001317D7 File Offset: 0x0012F9D7
		public void Update(byte input)
		{
			this.cipher.ProcessAadByte(input);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x001317E5 File Offset: 0x0012F9E5
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.cipher.ProcessAadBytes(input, inOff, len);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x001317F8 File Offset: 0x0012F9F8
		public int DoFinal(byte[] output, int outOff)
		{
			int result;
			try
			{
				result = this.cipher.DoFinal(output, outOff);
			}
			catch (InvalidCipherTextException ex)
			{
				throw new InvalidOperationException(ex.ToString());
			}
			return result;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00131834 File Offset: 0x0012FA34
		public void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001FDF RID: 8159
		private readonly GcmBlockCipher cipher;

		// Token: 0x04001FE0 RID: 8160
		private readonly int macSizeBits;
	}
}
