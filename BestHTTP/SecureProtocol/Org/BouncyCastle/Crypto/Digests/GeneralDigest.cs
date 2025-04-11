using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200058F RID: 1423
	public abstract class GeneralDigest : IDigest, IMemoable
	{
		// Token: 0x0600367A RID: 13946 RVA: 0x001563BD File Offset: 0x001545BD
		internal GeneralDigest()
		{
			this.xBuf = new byte[4];
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x001563D1 File Offset: 0x001545D1
		internal GeneralDigest(GeneralDigest t)
		{
			this.xBuf = new byte[t.xBuf.Length];
			this.CopyIn(t);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x001563F3 File Offset: 0x001545F3
		protected void CopyIn(GeneralDigest t)
		{
			Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);
			this.xBufOff = t.xBufOff;
			this.byteCount = t.byteCount;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x00156428 File Offset: 0x00154628
		public void Update(byte input)
		{
			byte[] array = this.xBuf;
			int num = this.xBufOff;
			this.xBufOff = num + 1;
			array[num] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.ProcessWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1L;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x00156484 File Offset: 0x00154684
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			length = Math.Max(0, length);
			int i = 0;
			if (this.xBufOff != 0)
			{
				while (i < length)
				{
					byte[] array = this.xBuf;
					int num = this.xBufOff;
					this.xBufOff = num + 1;
					array[num] = input[inOff + i++];
					if (this.xBufOff == 4)
					{
						this.ProcessWord(this.xBuf, 0);
						this.xBufOff = 0;
						break;
					}
				}
			}
			int num2 = (length - i & -4) + i;
			while (i < num2)
			{
				this.ProcessWord(input, inOff + i);
				i += 4;
			}
			while (i < length)
			{
				byte[] array2 = this.xBuf;
				int num = this.xBufOff;
				this.xBufOff = num + 1;
				array2[num] = input[inOff + i++];
			}
			this.byteCount += (long)length;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x00156540 File Offset: 0x00154740
		public void Finish()
		{
			long bitLength = this.byteCount << 3;
			this.Update(128);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.ProcessLength(bitLength);
			this.ProcessBlock();
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0015657F File Offset: 0x0015477F
		public virtual void Reset()
		{
			this.byteCount = 0L;
			this.xBufOff = 0;
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x001554A4 File Offset: 0x001536A4
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x06003682 RID: 13954
		internal abstract void ProcessWord(byte[] input, int inOff);

		// Token: 0x06003683 RID: 13955
		internal abstract void ProcessLength(long bitLength);

		// Token: 0x06003684 RID: 13956
		internal abstract void ProcessBlock();

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06003685 RID: 13957
		public abstract string AlgorithmName { get; }

		// Token: 0x06003686 RID: 13958
		public abstract int GetDigestSize();

		// Token: 0x06003687 RID: 13959
		public abstract int DoFinal(byte[] output, int outOff);

		// Token: 0x06003688 RID: 13960
		public abstract IMemoable Copy();

		// Token: 0x06003689 RID: 13961
		public abstract void Reset(IMemoable t);

		// Token: 0x040022B2 RID: 8882
		private const int BYTE_LENGTH = 64;

		// Token: 0x040022B3 RID: 8883
		private byte[] xBuf;

		// Token: 0x040022B4 RID: 8884
		private int xBufOff;

		// Token: 0x040022B5 RID: 8885
		private long byteCount;
	}
}
