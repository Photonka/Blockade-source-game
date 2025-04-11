using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005CB RID: 1483
	public interface CmsAttributeTableGenerator
	{
		// Token: 0x0600390B RID: 14603
		AttributeTable GetAttributes(IDictionary parameters);
	}
}
