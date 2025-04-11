using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A7 RID: 1447
	public class ShortenedDigest : IDigest
	{
		// Token: 0x060037EE RID: 14318 RVA: 0x0016318C File Offset: 0x0016138C
		public ShortenedDigest(IDigest baseDigest, int length)
		{
			if (baseDigest == null)
			{
				throw new ArgumentNullException("baseDigest");
			}
			if (length > baseDigest.GetDigestSize())
			{
				throw new ArgumentException("baseDigest output not large enough to support length");
			}
			this.baseDigest = baseDigest;
			this.length = length;
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x001631C4 File Offset: 0x001613C4
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					this.baseDigest.AlgorithmName,
					"(",
					this.length * 8,
					")"
				});
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x001631FF File Offset: 0x001613FF
		public int GetDigestSize()
		{
			return this.length;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x00163207 File Offset: 0x00161407
		public void Update(byte input)
		{
			this.baseDigest.Update(input);
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x00163215 File Offset: 0x00161415
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.baseDigest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x00163228 File Offset: 0x00161428
		public int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.baseDigest.GetDigestSize()];
			this.baseDigest.DoFinal(array, 0);
			Array.Copy(array, 0, output, outOff, this.length);
			return this.length;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x00163269 File Offset: 0x00161469
		public void Reset()
		{
			this.baseDigest.Reset();
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x00163276 File Offset: 0x00161476
		public int GetByteLength()
		{
			return this.baseDigest.GetByteLength();
		}

		// Token: 0x0400237B RID: 9083
		private IDigest baseDigest;

		// Token: 0x0400237C RID: 9084
		private int length;
	}
}
