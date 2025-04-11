using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020005BD RID: 1469
	public class ConcatenationKdfGenerator : IDerivationFunction
	{
		// Token: 0x060038B5 RID: 14517 RVA: 0x001672B0 File Offset: 0x001654B0
		public ConcatenationKdfGenerator(IDigest digest)
		{
			this.mDigest = digest;
			this.mHLen = digest.GetDigestSize();
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x001672CC File Offset: 0x001654CC
		public virtual void Init(IDerivationParameters param)
		{
			if (!(param is KdfParameters))
			{
				throw new ArgumentException("KDF parameters required for ConcatenationKdfGenerator");
			}
			KdfParameters kdfParameters = (KdfParameters)param;
			this.mShared = kdfParameters.GetSharedSecret();
			this.mOtherInfo = kdfParameters.GetIV();
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x0016730B File Offset: 0x0016550B
		public virtual IDigest Digest
		{
			get
			{
				return this.mDigest;
			}
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00167314 File Offset: 0x00165514
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			if (outBytes.Length - len < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			byte[] array = new byte[this.mHLen];
			byte[] array2 = new byte[4];
			uint n = 1U;
			int num = 0;
			this.mDigest.Reset();
			if (len > this.mHLen)
			{
				do
				{
					Pack.UInt32_To_BE(n, array2);
					this.mDigest.BlockUpdate(array2, 0, array2.Length);
					this.mDigest.BlockUpdate(this.mShared, 0, this.mShared.Length);
					this.mDigest.BlockUpdate(this.mOtherInfo, 0, this.mOtherInfo.Length);
					this.mDigest.DoFinal(array, 0);
					Array.Copy(array, 0, outBytes, outOff + num, this.mHLen);
					num += this.mHLen;
				}
				while ((ulong)n++ < (ulong)((long)(len / this.mHLen)));
			}
			if (num < len)
			{
				Pack.UInt32_To_BE(n, array2);
				this.mDigest.BlockUpdate(array2, 0, array2.Length);
				this.mDigest.BlockUpdate(this.mShared, 0, this.mShared.Length);
				this.mDigest.BlockUpdate(this.mOtherInfo, 0, this.mOtherInfo.Length);
				this.mDigest.DoFinal(array, 0);
				Array.Copy(array, 0, outBytes, outOff + num, len - num);
			}
			return len;
		}

		// Token: 0x0400243B RID: 9275
		private readonly IDigest mDigest;

		// Token: 0x0400243C RID: 9276
		private byte[] mShared;

		// Token: 0x0400243D RID: 9277
		private byte[] mOtherInfo;

		// Token: 0x0400243E RID: 9278
		private int mHLen;
	}
}
