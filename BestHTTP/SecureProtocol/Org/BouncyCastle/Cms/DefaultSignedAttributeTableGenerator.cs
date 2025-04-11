using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005F2 RID: 1522
	public class DefaultSignedAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06003A3C RID: 14908 RVA: 0x0016D26E File Offset: 0x0016B46E
		public DefaultSignedAttributeTableGenerator()
		{
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0016D281 File Offset: 0x0016B481
		public DefaultSignedAttributeTableGenerator(AttributeTable attributeTable)
		{
			if (attributeTable != null)
			{
				this.table = attributeTable.ToDictionary();
				return;
			}
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0016D2A4 File Offset: 0x0016B4A4
		protected virtual Hashtable createStandardAttributeTable(IDictionary parameters)
		{
			Hashtable hashtable = new Hashtable(this.table);
			this.DoCreateStandardAttributeTable(parameters, hashtable);
			return hashtable;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x0016D2C8 File Offset: 0x0016B4C8
		private void DoCreateStandardAttributeTable(IDictionary parameters, IDictionary std)
		{
			if (parameters.Contains(CmsAttributeTableParameter.ContentType) && !std.Contains(CmsAttributes.ContentType))
			{
				DerObjectIdentifier obj = (DerObjectIdentifier)parameters[CmsAttributeTableParameter.ContentType];
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, new DerSet(obj));
				std[attribute.AttrType] = attribute;
			}
			if (!std.Contains(CmsAttributes.SigningTime))
			{
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.SigningTime, new DerSet(new Time(DateTime.UtcNow)));
				std[attribute2.AttrType] = attribute2;
			}
			if (!std.Contains(CmsAttributes.MessageDigest))
			{
				byte[] str = (byte[])parameters[CmsAttributeTableParameter.Digest];
				BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute attribute3 = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, new DerSet(new DerOctetString(str)));
				std[attribute3.AttrType] = attribute3;
			}
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x0016D399 File Offset: 0x0016B599
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return new AttributeTable(this.createStandardAttributeTable(parameters));
		}

		// Token: 0x0400251B RID: 9499
		private readonly IDictionary table;
	}
}
