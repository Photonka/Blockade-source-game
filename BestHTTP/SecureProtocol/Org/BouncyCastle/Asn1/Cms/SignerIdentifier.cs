using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078C RID: 1932
	public class SignerIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004570 RID: 17776 RVA: 0x001932D4 File Offset: 0x001914D4
		public SignerIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x001932E3 File Offset: 0x001914E3
		public SignerIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x001932D4 File Offset: 0x001914D4
		public SignerIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x001932FC File Offset: 0x001914FC
		public static SignerIdentifier GetInstance(object o)
		{
			if (o == null || o is SignerIdentifier)
			{
				return (SignerIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new SignerIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new SignerIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new SignerIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in SignerIdentifier: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x0019336C File Offset: 0x0019156C
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x0019337C File Offset: 0x0019157C
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return this.id;
			}
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x001933A3 File Offset: 0x001915A3
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04002C4B RID: 11339
		private Asn1Encodable id;
	}
}
