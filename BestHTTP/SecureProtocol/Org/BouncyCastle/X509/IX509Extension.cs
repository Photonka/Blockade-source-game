using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000225 RID: 549
	public interface IX509Extension
	{
		// Token: 0x06001433 RID: 5171
		ISet GetCriticalExtensionOids();

		// Token: 0x06001434 RID: 5172
		ISet GetNonCriticalExtensionOids();

		// Token: 0x06001435 RID: 5173
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		Asn1OctetString GetExtensionValue(string oid);

		// Token: 0x06001436 RID: 5174
		Asn1OctetString GetExtensionValue(DerObjectIdentifier oid);
	}
}
