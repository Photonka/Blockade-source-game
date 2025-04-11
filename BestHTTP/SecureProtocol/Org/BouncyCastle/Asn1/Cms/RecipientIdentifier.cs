using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000786 RID: 1926
	public class RecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004537 RID: 17719 RVA: 0x001926F2 File Offset: 0x001908F2
		public RecipientIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00192701 File Offset: 0x00190901
		public RecipientIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x001926F2 File Offset: 0x001908F2
		public RecipientIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00192718 File Offset: 0x00190918
		public static RecipientIdentifier GetInstance(object o)
		{
			if (o == null || o is RecipientIdentifier)
			{
				return (RecipientIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new RecipientIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new RecipientIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new RecipientIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in RecipientIdentifier: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00192788 File Offset: 0x00190988
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x00192798 File Offset: 0x00190998
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return IssuerAndSerialNumber.GetInstance(this.id);
			}
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x001927C4 File Offset: 0x001909C4
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04002C33 RID: 11315
		private Asn1Encodable id;
	}
}
