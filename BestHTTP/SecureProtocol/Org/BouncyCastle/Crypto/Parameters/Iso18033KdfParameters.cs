using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D4 RID: 1236
	public class Iso18033KdfParameters : IDerivationParameters
	{
		// Token: 0x06002FF4 RID: 12276 RVA: 0x00128E4F File Offset: 0x0012704F
		public Iso18033KdfParameters(byte[] seed)
		{
			this.seed = seed;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00128E5E File Offset: 0x0012705E
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x04001ECA RID: 7882
		private byte[] seed;
	}
}
