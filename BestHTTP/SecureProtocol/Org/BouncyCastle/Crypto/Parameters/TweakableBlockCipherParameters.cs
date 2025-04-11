using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004EC RID: 1260
	public class TweakableBlockCipherParameters : ICipherParameters
	{
		// Token: 0x0600305E RID: 12382 RVA: 0x001299FD File Offset: 0x00127BFD
		public TweakableBlockCipherParameters(KeyParameter key, byte[] tweak)
		{
			this.key = key;
			this.tweak = Arrays.Clone(tweak);
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x00129A18 File Offset: 0x00127C18
		public KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x00129A20 File Offset: 0x00127C20
		public byte[] Tweak
		{
			get
			{
				return this.tweak;
			}
		}

		// Token: 0x04001F06 RID: 7942
		private readonly byte[] tweak;

		// Token: 0x04001F07 RID: 7943
		private readonly KeyParameter key;
	}
}
