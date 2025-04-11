using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F1 RID: 1521
	public class DefaultAuthenticatedAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06003A38 RID: 14904 RVA: 0x0016D18D File Offset: 0x0016B38D
		public DefaultAuthenticatedAttributeTableGenerator()
		{
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x0016D1A0 File Offset: 0x0016B3A0
		public DefaultAuthenticatedAttributeTableGenerator(AttributeTable attributeTable)
		{
			if (attributeTable != null)
			{
				this.table = attributeTable.ToDictionary();
				return;
			}
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x0016D1C4 File Offset: 0x0016B3C4
		protected virtual IDictionary CreateStandardAttributeTable(IDictionary parameters)
		{
			IDictionary dictionary = Platform.CreateHashtable(this.table);
			if (!dictionary.Contains(CmsAttributes.ContentType))
			{
				DerObjectIdentifier obj = (DerObjectIdentifier)parameters[CmsAttributeTableParameter.ContentType];
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, new DerSet(obj));
				dictionary[attribute.AttrType] = attribute;
			}
			if (!dictionary.Contains(CmsAttributes.MessageDigest))
			{
				byte[] str = (byte[])parameters[CmsAttributeTableParameter.Digest];
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, new DerSet(new DerOctetString(str)));
				dictionary[attribute2.AttrType] = attribute2;
			}
			return dictionary;
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x0016D260 File Offset: 0x0016B460
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return new AttributeTable(this.CreateStandardAttributeTable(parameters));
		}

		// Token: 0x0400251A RID: 9498
		private readonly IDictionary table;
	}
}
