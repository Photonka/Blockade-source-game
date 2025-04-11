using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D7 RID: 1239
	public class MgfParameters : IDerivationParameters
	{
		// Token: 0x06002FFC RID: 12284 RVA: 0x00128F2E File Offset: 0x0012712E
		public MgfParameters(byte[] seed) : this(seed, 0, seed.Length)
		{
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00128F3B File Offset: 0x0012713B
		public MgfParameters(byte[] seed, int off, int len)
		{
			this.seed = new byte[len];
			Array.Copy(seed, off, this.seed, 0, len);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00128F5E File Offset: 0x0012715E
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x04001ECE RID: 7886
		private readonly byte[] seed;
	}
}
