using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000675 RID: 1653
	public class AttributeTable
	{
		// Token: 0x06003D9C RID: 15772 RVA: 0x00177504 File Offset: 0x00175704
		public AttributeTable(IDictionary attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00177504 File Offset: 0x00175704
		[Obsolete]
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00177518 File Offset: 0x00175718
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = Platform.CreateHashtable(v.Count);
			for (int num = 0; num != v.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(v[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x0017756C File Offset: 0x0017576C
		public AttributeTable(Asn1Set s)
		{
			this.attributes = Platform.CreateHashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(s[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x001775C0 File Offset: 0x001757C0
		public AttributeX509 Get(DerObjectIdentifier oid)
		{
			return (AttributeX509)this.attributes[oid];
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x001775D3 File Offset: 0x001757D3
		[Obsolete("Use 'ToDictionary' instead")]
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x001775E0 File Offset: 0x001757E0
		public IDictionary ToDictionary()
		{
			return Platform.CreateHashtable(this.attributes);
		}

		// Token: 0x0400264E RID: 9806
		private readonly IDictionary attributes;
	}
}
