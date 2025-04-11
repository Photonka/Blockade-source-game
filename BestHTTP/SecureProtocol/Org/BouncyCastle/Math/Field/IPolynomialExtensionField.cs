using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000304 RID: 772
	public interface IPolynomialExtensionField : IExtensionField, IFiniteField
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001D8F RID: 7567
		IPolynomial MinimalPolynomial { get; }
	}
}
