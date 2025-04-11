using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F0 RID: 1520
	internal class CounterSignatureDigestCalculator : IDigestCalculator
	{
		// Token: 0x06003A36 RID: 14902 RVA: 0x0016D15A File Offset: 0x0016B35A
		internal CounterSignatureDigestCalculator(string alg, byte[] data)
		{
			this.alg = alg;
			this.data = data;
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x0016D170 File Offset: 0x0016B370
		public byte[] GetDigest()
		{
			return DigestUtilities.DoFinal(CmsSignedHelper.Instance.GetDigestInstance(this.alg), this.data);
		}

		// Token: 0x04002518 RID: 9496
		private readonly string alg;

		// Token: 0x04002519 RID: 9497
		private readonly byte[] data;
	}
}
