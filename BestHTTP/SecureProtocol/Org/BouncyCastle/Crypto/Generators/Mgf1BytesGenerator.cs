using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000543 RID: 1347
	public class Mgf1BytesGenerator : IDerivationFunction
	{
		// Token: 0x06003311 RID: 13073 RVA: 0x00136CF8 File Offset: 0x00134EF8
		public Mgf1BytesGenerator(IDigest digest)
		{
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x00136D14 File Offset: 0x00134F14
		public void Init(IDerivationParameters parameters)
		{
			if (!typeof(MgfParameters).IsInstanceOfType(parameters))
			{
				throw new ArgumentException("MGF parameters required for MGF1Generator");
			}
			MgfParameters mgfParameters = (MgfParameters)parameters;
			this.seed = mgfParameters.GetSeed();
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x00136D51 File Offset: 0x00134F51
		public IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x00122A78 File Offset: 0x00120C78
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x00136D5C File Offset: 0x00134F5C
		public int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			byte[] array = new byte[this.hLen];
			byte[] array2 = new byte[4];
			int num = 0;
			this.digest.Reset();
			if (length > this.hLen)
			{
				do
				{
					this.ItoOSP(num, array2);
					this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
					this.digest.BlockUpdate(array2, 0, array2.Length);
					this.digest.DoFinal(array, 0);
					Array.Copy(array, 0, output, outOff + num * this.hLen, this.hLen);
				}
				while (++num < length / this.hLen);
			}
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array2);
				this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
				this.digest.BlockUpdate(array2, 0, array2.Length);
				this.digest.DoFinal(array, 0);
				Array.Copy(array, 0, output, outOff + num * this.hLen, length - num * this.hLen);
			}
			return length;
		}

		// Token: 0x0400207A RID: 8314
		private IDigest digest;

		// Token: 0x0400207B RID: 8315
		private byte[] seed;

		// Token: 0x0400207C RID: 8316
		private int hLen;
	}
}
