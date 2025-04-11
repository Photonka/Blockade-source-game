using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000540 RID: 1344
	public class HkdfBytesGenerator : IDerivationFunction
	{
		// Token: 0x06003309 RID: 13065 RVA: 0x00136A55 File Offset: 0x00134C55
		public HkdfBytesGenerator(IDigest hash)
		{
			this.hMacHash = new HMac(hash);
			this.hashLen = hash.GetDigestSize();
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x00136A78 File Offset: 0x00134C78
		public virtual void Init(IDerivationParameters parameters)
		{
			if (!(parameters is HkdfParameters))
			{
				throw new ArgumentException("HKDF parameters required for HkdfBytesGenerator", "parameters");
			}
			HkdfParameters hkdfParameters = (HkdfParameters)parameters;
			if (hkdfParameters.SkipExtract)
			{
				this.hMacHash.Init(new KeyParameter(hkdfParameters.GetIkm()));
			}
			else
			{
				this.hMacHash.Init(this.Extract(hkdfParameters.GetSalt(), hkdfParameters.GetIkm()));
			}
			this.info = hkdfParameters.GetInfo();
			this.generatedBytes = 0;
			this.currentT = new byte[this.hashLen];
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x00136B08 File Offset: 0x00134D08
		private KeyParameter Extract(byte[] salt, byte[] ikm)
		{
			if (salt == null)
			{
				this.hMacHash.Init(new KeyParameter(new byte[this.hashLen]));
			}
			else
			{
				this.hMacHash.Init(new KeyParameter(salt));
			}
			this.hMacHash.BlockUpdate(ikm, 0, ikm.Length);
			byte[] array = new byte[this.hashLen];
			this.hMacHash.DoFinal(array, 0);
			return new KeyParameter(array);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x00136B78 File Offset: 0x00134D78
		private void ExpandNext()
		{
			int num = this.generatedBytes / this.hashLen + 1;
			if (num >= 256)
			{
				throw new DataLengthException("HKDF cannot generate more than 255 blocks of HashLen size");
			}
			if (this.generatedBytes != 0)
			{
				this.hMacHash.BlockUpdate(this.currentT, 0, this.hashLen);
			}
			this.hMacHash.BlockUpdate(this.info, 0, this.info.Length);
			this.hMacHash.Update((byte)num);
			this.hMacHash.DoFinal(this.currentT, 0);
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x00136C02 File Offset: 0x00134E02
		public virtual IDigest Digest
		{
			get
			{
				return this.hMacHash.GetUnderlyingDigest();
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x00136C10 File Offset: 0x00134E10
		public virtual int GenerateBytes(byte[] output, int outOff, int len)
		{
			if (this.generatedBytes + len > 255 * this.hashLen)
			{
				throw new DataLengthException("HKDF may only be used for 255 * HashLen bytes of output");
			}
			if (this.generatedBytes % this.hashLen == 0)
			{
				this.ExpandNext();
			}
			int sourceIndex = this.generatedBytes % this.hashLen;
			int num = Math.Min(this.hashLen - this.generatedBytes % this.hashLen, len);
			Array.Copy(this.currentT, sourceIndex, output, outOff, num);
			this.generatedBytes += num;
			int i = len - num;
			outOff += num;
			while (i > 0)
			{
				this.ExpandNext();
				num = Math.Min(this.hashLen, i);
				Array.Copy(this.currentT, 0, output, outOff, num);
				this.generatedBytes += num;
				i -= num;
				outOff += num;
			}
			return len;
		}

		// Token: 0x04002075 RID: 8309
		private HMac hMacHash;

		// Token: 0x04002076 RID: 8310
		private int hashLen;

		// Token: 0x04002077 RID: 8311
		private byte[] info;

		// Token: 0x04002078 RID: 8312
		private byte[] currentT;

		// Token: 0x04002079 RID: 8313
		private int generatedBytes;
	}
}
