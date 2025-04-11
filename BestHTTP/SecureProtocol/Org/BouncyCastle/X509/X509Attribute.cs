using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022A RID: 554
	public class X509Attribute : Asn1Encodable
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x000ACF36 File Offset: 0x000AB136
		internal X509Attribute(Asn1Encodable at)
		{
			this.attr = AttributeX509.GetInstance(at);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000ACF4A File Offset: 0x000AB14A
		public X509Attribute(string oid, Asn1Encodable value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000ACF69 File Offset: 0x000AB169
		public X509Attribute(string oid, Asn1EncodableVector value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x000ACF88 File Offset: 0x000AB188
		public string Oid
		{
			get
			{
				return this.attr.AttrType.Id;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000ACF9C File Offset: 0x000AB19C
		public Asn1Encodable[] GetValues()
		{
			Asn1Set attrValues = this.attr.AttrValues;
			Asn1Encodable[] array = new Asn1Encodable[attrValues.Count];
			for (int num = 0; num != attrValues.Count; num++)
			{
				array[num] = attrValues[num];
			}
			return array;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000ACFDD File Offset: 0x000AB1DD
		public override Asn1Object ToAsn1Object()
		{
			return this.attr.ToAsn1Object();
		}

		// Token: 0x040014E3 RID: 5347
		private readonly AttributeX509 attr;
	}
}
