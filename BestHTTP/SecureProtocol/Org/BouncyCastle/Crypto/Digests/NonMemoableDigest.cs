using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000599 RID: 1433
	public class NonMemoableDigest : IDigest
	{
		// Token: 0x06003722 RID: 14114 RVA: 0x0015AC7D File Offset: 0x00158E7D
		public NonMemoableDigest(IDigest baseDigest)
		{
			if (baseDigest == null)
			{
				throw new ArgumentNullException("baseDigest");
			}
			this.mBaseDigest = baseDigest;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x0015AC9A File Offset: 0x00158E9A
		public virtual string AlgorithmName
		{
			get
			{
				return this.mBaseDigest.AlgorithmName;
			}
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0015ACA7 File Offset: 0x00158EA7
		public virtual int GetDigestSize()
		{
			return this.mBaseDigest.GetDigestSize();
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0015ACB4 File Offset: 0x00158EB4
		public virtual void Update(byte input)
		{
			this.mBaseDigest.Update(input);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0015ACC2 File Offset: 0x00158EC2
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.mBaseDigest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0015ACD2 File Offset: 0x00158ED2
		public virtual int DoFinal(byte[] output, int outOff)
		{
			return this.mBaseDigest.DoFinal(output, outOff);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0015ACE1 File Offset: 0x00158EE1
		public virtual void Reset()
		{
			this.mBaseDigest.Reset();
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x0015ACEE File Offset: 0x00158EEE
		public virtual int GetByteLength()
		{
			return this.mBaseDigest.GetByteLength();
		}

		// Token: 0x04002322 RID: 8994
		protected readonly IDigest mBaseDigest;
	}
}
