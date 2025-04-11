using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004EB RID: 1259
	public sealed class Srp6GroupParameters
	{
		// Token: 0x0600305B RID: 12379 RVA: 0x001299D7 File Offset: 0x00127BD7
		public Srp6GroupParameters(BigInteger N, BigInteger g)
		{
			this.n = N;
			this.g = g;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x001299ED File Offset: 0x00127BED
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x001299F5 File Offset: 0x00127BF5
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x04001F04 RID: 7940
		private readonly BigInteger n;

		// Token: 0x04001F05 RID: 7941
		private readonly BigInteger g;
	}
}
