using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000413 RID: 1043
	public abstract class ECBasisType
	{
		// Token: 0x06002A27 RID: 10791 RVA: 0x001148B1 File Offset: 0x00112AB1
		public static bool IsValid(byte ecBasisType)
		{
			return ecBasisType >= 1 && ecBasisType <= 2;
		}

		// Token: 0x04001BB2 RID: 7090
		public const byte ec_basis_trinomial = 1;

		// Token: 0x04001BB3 RID: 7091
		public const byte ec_basis_pentanomial = 2;
	}
}
