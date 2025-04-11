using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003BA RID: 954
	public interface IBasicAgreement
	{
		// Token: 0x060027AF RID: 10159
		void Init(ICipherParameters parameters);

		// Token: 0x060027B0 RID: 10160
		int GetFieldSize();

		// Token: 0x060027B1 RID: 10161
		BigInteger CalculateAgreement(ICipherParameters pubKey);
	}
}
