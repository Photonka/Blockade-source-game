using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000787 RID: 1927
	public class RecipientInfo : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600453E RID: 17726 RVA: 0x001927D1 File Offset: 0x001909D1
		public RecipientInfo(KeyTransRecipientInfo info)
		{
			this.info = info;
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x001927E0 File Offset: 0x001909E0
		public RecipientInfo(KeyAgreeRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 1, info);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x001927F6 File Offset: 0x001909F6
		public RecipientInfo(KekRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 2, info);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x0019280C File Offset: 0x00190A0C
		public RecipientInfo(PasswordRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 3, info);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x00192822 File Offset: 0x00190A22
		public RecipientInfo(OtherRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 4, info);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x001927D1 File Offset: 0x001909D1
		public RecipientInfo(Asn1Object info)
		{
			this.info = info;
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x00192838 File Offset: 0x00190A38
		public static RecipientInfo GetInstance(object o)
		{
			if (o == null || o is RecipientInfo)
			{
				return (RecipientInfo)o;
			}
			if (o is Asn1Sequence)
			{
				return new RecipientInfo((Asn1Sequence)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new RecipientInfo((Asn1TaggedObject)o);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x00192894 File Offset: 0x00190A94
		public DerInteger Version
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info).Version;
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 2:
					return this.GetKekInfo(asn1TaggedObject).Version;
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 4:
					return new DerInteger(0);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x00192924 File Offset: 0x00190B24
		public bool IsTagged
		{
			get
			{
				return this.info is Asn1TaggedObject;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x00192934 File Offset: 0x00190B34
		public Asn1Encodable Info
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info);
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 2:
					return this.GetKekInfo(asn1TaggedObject);
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 4:
					return OtherRecipientInfo.GetInstance(asn1TaggedObject, false);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x001929B1 File Offset: 0x00190BB1
		private KekRecipientInfo GetKekInfo(Asn1TaggedObject o)
		{
			return KekRecipientInfo.GetInstance(o, o.IsExplicit());
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x001929BF File Offset: 0x00190BBF
		public override Asn1Object ToAsn1Object()
		{
			return this.info.ToAsn1Object();
		}

		// Token: 0x04002C34 RID: 11316
		internal Asn1Encodable info;
	}
}
