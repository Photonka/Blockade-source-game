using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000591 RID: 1425
	public class GOST3411_2012_512Digest : GOST3411_2012Digest
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x0015661D File Offset: 0x0015481D
		public override string AlgorithmName
		{
			get
			{
				return "GOST3411-2012-512";
			}
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x00156624 File Offset: 0x00154824
		public GOST3411_2012_512Digest() : base(GOST3411_2012_512Digest.IV)
		{
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x00156631 File Offset: 0x00154831
		public GOST3411_2012_512Digest(GOST3411_2012_512Digest other) : base(GOST3411_2012_512Digest.IV)
		{
			base.Reset(other);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x001554A4 File Offset: 0x001536A4
		public override int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x00156645 File Offset: 0x00154845
		public override IMemoable Copy()
		{
			return new GOST3411_2012_512Digest(this);
		}

		// Token: 0x040022B7 RID: 8887
		private static readonly byte[] IV = new byte[64];
	}
}
