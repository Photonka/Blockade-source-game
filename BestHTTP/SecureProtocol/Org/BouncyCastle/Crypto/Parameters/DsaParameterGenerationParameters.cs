using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B6 RID: 1206
	public class DsaParameterGenerationParameters
	{
		// Token: 0x06002F3F RID: 12095 RVA: 0x0012780B File Offset: 0x00125A0B
		public DsaParameterGenerationParameters(int L, int N, int certainty, SecureRandom random) : this(L, N, certainty, random, -1)
		{
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x00127819 File Offset: 0x00125A19
		public DsaParameterGenerationParameters(int L, int N, int certainty, SecureRandom random, int usageIndex)
		{
			this.l = L;
			this.n = N;
			this.certainty = certainty;
			this.random = random;
			this.usageIndex = usageIndex;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x00127846 File Offset: 0x00125A46
		public virtual int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x0012784E File Offset: 0x00125A4E
		public virtual int N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x00127856 File Offset: 0x00125A56
		public virtual int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0012785E File Offset: 0x00125A5E
		public virtual int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x00127866 File Offset: 0x00125A66
		public virtual SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x04001E85 RID: 7813
		public const int DigitalSignatureUsage = 1;

		// Token: 0x04001E86 RID: 7814
		public const int KeyEstablishmentUsage = 2;

		// Token: 0x04001E87 RID: 7815
		private readonly int l;

		// Token: 0x04001E88 RID: 7816
		private readonly int n;

		// Token: 0x04001E89 RID: 7817
		private readonly int certainty;

		// Token: 0x04001E8A RID: 7818
		private readonly SecureRandom random;

		// Token: 0x04001E8B RID: 7819
		private readonly int usageIndex;
	}
}
