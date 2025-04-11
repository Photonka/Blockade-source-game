using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200052E RID: 1326
	public class BaseKdfBytesGenerator : IDerivationFunction
	{
		// Token: 0x060032AE RID: 12974 RVA: 0x00134463 File Offset: 0x00132663
		public BaseKdfBytesGenerator(int counterStart, IDigest digest)
		{
			this.counterStart = counterStart;
			this.digest = digest;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x0013447C File Offset: 0x0013267C
		public virtual void Init(IDerivationParameters parameters)
		{
			if (parameters is KdfParameters)
			{
				KdfParameters kdfParameters = (KdfParameters)parameters;
				this.shared = kdfParameters.GetSharedSecret();
				this.iv = kdfParameters.GetIV();
				return;
			}
			if (parameters is Iso18033KdfParameters)
			{
				Iso18033KdfParameters iso18033KdfParameters = (Iso18033KdfParameters)parameters;
				this.shared = iso18033KdfParameters.GetSeed();
				this.iv = null;
				return;
			}
			throw new ArgumentException("KDF parameters required for KDF Generator");
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x001344DE File Offset: 0x001326DE
		public virtual IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x001344E8 File Offset: 0x001326E8
		public virtual int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)length;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			byte[] array2 = new byte[4];
			Pack.UInt32_To_BE((uint)this.counterStart, array2, 0);
			uint num3 = (uint)(this.counterStart & -256);
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.shared, 0, this.shared.Length);
				this.digest.BlockUpdate(array2, 0, 4);
				if (this.iv != null)
				{
					this.digest.BlockUpdate(this.iv, 0, this.iv.Length);
				}
				this.digest.DoFinal(array, 0);
				if (length > digestSize)
				{
					Array.Copy(array, 0, output, outOff, digestSize);
					outOff += digestSize;
					length -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, output, outOff, length);
				}
				byte[] array3 = array2;
				int num4 = 3;
				byte b = array3[num4] + 1;
				array3[num4] = b;
				if (b == 0)
				{
					num3 += 256U;
					Pack.UInt32_To_BE(num3, array2, 0);
				}
			}
			this.digest.Reset();
			return (int)num;
		}

		// Token: 0x0400203E RID: 8254
		private int counterStart;

		// Token: 0x0400203F RID: 8255
		private IDigest digest;

		// Token: 0x04002040 RID: 8256
		private byte[] shared;

		// Token: 0x04002041 RID: 8257
		private byte[] iv;
	}
}
