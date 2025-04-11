using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000766 RID: 1894
	public class AttributeTable
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x0018FBC1 File Offset: 0x0018DDC1
		[Obsolete]
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x0018FBC1 File Offset: 0x0018DDC1
		public AttributeTable(IDictionary attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x0018FBD8 File Offset: 0x0018DDD8
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = Platform.CreateHashtable(v.Count);
			foreach (object obj in v)
			{
				Attribute instance = Attribute.GetInstance((Asn1Encodable)obj);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x0018FC48 File Offset: 0x0018DE48
		public AttributeTable(Asn1Set s)
		{
			this.attributes = Platform.CreateHashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				Attribute instance = Attribute.GetInstance(s[num]);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x0018FC91 File Offset: 0x0018DE91
		public AttributeTable(Attributes attrs) : this(Asn1Set.GetInstance(attrs.ToAsn1Object()))
		{
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x0018FCA4 File Offset: 0x0018DEA4
		private void AddAttribute(Attribute a)
		{
			DerObjectIdentifier attrType = a.AttrType;
			object obj = this.attributes[attrType];
			if (obj == null)
			{
				this.attributes[attrType] = a;
				return;
			}
			IList list;
			if (obj is Attribute)
			{
				list = Platform.CreateArrayList();
				list.Add(obj);
				list.Add(a);
			}
			else
			{
				list = (IList)obj;
				list.Add(a);
			}
			this.attributes[attrType] = list;
		}

		// Token: 0x170009AD RID: 2477
		public Attribute this[DerObjectIdentifier oid]
		{
			get
			{
				object obj = this.attributes[oid];
				if (obj is IList)
				{
					return (Attribute)((IList)obj)[0];
				}
				return (Attribute)obj;
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x0018FD4E File Offset: 0x0018DF4E
		[Obsolete("Use 'object[oid]' syntax instead")]
		public Attribute Get(DerObjectIdentifier oid)
		{
			return this[oid];
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x0018FD58 File Offset: 0x0018DF58
		public Asn1EncodableVector GetAll(DerObjectIdentifier oid)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			object obj = this.attributes[oid];
			if (obj is IList)
			{
				using (IEnumerator enumerator = ((IList)obj).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						Attribute attribute = (Attribute)obj2;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							attribute
						});
					}
					return asn1EncodableVector;
				}
			}
			if (obj != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					(Attribute)obj
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x0018FDF8 File Offset: 0x0018DFF8
		public int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this.attributes.Values)
				{
					if (obj is IList)
					{
						num += ((IList)obj).Count;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x0018FE6C File Offset: 0x0018E06C
		public IDictionary ToDictionary()
		{
			return Platform.CreateHashtable(this.attributes);
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x0018FE79 File Offset: 0x0018E079
		[Obsolete("Use 'ToDictionary' instead")]
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x0018FE88 File Offset: 0x0018E088
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.attributes.Values)
			{
				if (obj is IList)
				{
					using (IEnumerator enumerator2 = ((IList)obj).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							asn1EncodableVector.Add(new Asn1Encodable[]
							{
								Attribute.GetInstance(obj2)
							});
						}
						continue;
					}
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					Attribute.GetInstance(obj)
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x0018FF5C File Offset: 0x0018E15C
		public Attributes ToAttributes()
		{
			return new Attributes(this.ToAsn1EncodableVector());
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x0018FF69 File Offset: 0x0018E169
		public AttributeTable Add(DerObjectIdentifier attrType, Asn1Encodable attrValue)
		{
			AttributeTable attributeTable = new AttributeTable(this.attributes);
			attributeTable.AddAttribute(new Attribute(attrType, new DerSet(attrValue)));
			return attributeTable;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x0018FF88 File Offset: 0x0018E188
		public AttributeTable Remove(DerObjectIdentifier attrType)
		{
			AttributeTable attributeTable = new AttributeTable(this.attributes);
			attributeTable.attributes.Remove(attrType);
			return attributeTable;
		}

		// Token: 0x04002BC2 RID: 11202
		private readonly IDictionary attributes;
	}
}
