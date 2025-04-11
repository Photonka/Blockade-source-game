using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E3 RID: 1251
	public class RC5Parameters : KeyParameter
	{
		// Token: 0x0600302E RID: 12334 RVA: 0x00129327 File Offset: 0x00127527
		public RC5Parameters(byte[] key, int rounds) : base(key)
		{
			if (key.Length > 255)
			{
				throw new ArgumentException("RC5 key length can be no greater than 255");
			}
			this.rounds = rounds;
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x0012934C File Offset: 0x0012754C
		public int Rounds
		{
			get
			{
				return this.rounds;
			}
		}

		// Token: 0x04001EE6 RID: 7910
		private readonly int rounds;
	}
}
