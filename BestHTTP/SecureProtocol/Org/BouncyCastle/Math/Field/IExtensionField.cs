using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000301 RID: 769
	public interface IExtensionField : IFiniteField
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001D89 RID: 7561
		IFiniteField Subfield { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001D8A RID: 7562
		int Degree { get; }
	}
}
