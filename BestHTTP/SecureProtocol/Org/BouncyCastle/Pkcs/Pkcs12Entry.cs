using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B4 RID: 692
	public abstract class Pkcs12Entry
	{
		// Token: 0x060019D8 RID: 6616 RVA: 0x000C6418 File Offset: 0x000C4618
		protected internal Pkcs12Entry(IDictionary attributes)
		{
			this.attributes = attributes;
			foreach (object obj in attributes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!(dictionaryEntry.Key is string))
				{
					throw new ArgumentException("Attribute keys must be of type: " + typeof(string).FullName, "attributes");
				}
				if (!(dictionaryEntry.Value is Asn1Encodable))
				{
					throw new ArgumentException("Attribute values must be of type: " + typeof(Asn1Encodable).FullName, "attributes");
				}
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000C64D8 File Offset: 0x000C46D8
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(DerObjectIdentifier oid)
		{
			return (Asn1Encodable)this.attributes[oid.Id];
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000C64F0 File Offset: 0x000C46F0
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(string oid)
		{
			return (Asn1Encodable)this.attributes[oid];
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x000C6503 File Offset: 0x000C4703
		[Obsolete("Use 'BagAttributeKeys' property")]
		public IEnumerator GetBagAttributeKeys()
		{
			return this.attributes.Keys.GetEnumerator();
		}

		// Token: 0x17000352 RID: 850
		public Asn1Encodable this[DerObjectIdentifier oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid.Id];
			}
		}

		// Token: 0x17000353 RID: 851
		public Asn1Encodable this[string oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid];
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x000C6515 File Offset: 0x000C4715
		public IEnumerable BagAttributeKeys
		{
			get
			{
				return new EnumerableProxy(this.attributes.Keys);
			}
		}

		// Token: 0x04001780 RID: 6016
		private readonly IDictionary attributes;
	}
}
