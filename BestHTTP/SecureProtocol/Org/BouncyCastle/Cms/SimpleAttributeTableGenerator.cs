using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x0200060B RID: 1547
	public class SimpleAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06003ACD RID: 15053 RVA: 0x0016F952 File Offset: 0x0016DB52
		public SimpleAttributeTableGenerator(AttributeTable attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0016F961 File Offset: 0x0016DB61
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return this.attributes;
		}

		// Token: 0x04002558 RID: 9560
		private readonly AttributeTable attributes;
	}
}
