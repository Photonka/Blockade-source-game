using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005C8 RID: 1480
	internal class BaseDigestCalculator : IDigestCalculator
	{
		// Token: 0x06003906 RID: 14598 RVA: 0x001685FA File Offset: 0x001667FA
		internal BaseDigestCalculator(byte[] digest)
		{
			this.digest = digest;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x00168609 File Offset: 0x00166809
		public byte[] GetDigest()
		{
			return Arrays.Clone(this.digest);
		}

		// Token: 0x04002476 RID: 9334
		private readonly byte[] digest;
	}
}
