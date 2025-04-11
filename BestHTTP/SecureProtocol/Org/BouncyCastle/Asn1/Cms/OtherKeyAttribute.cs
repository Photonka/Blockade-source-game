using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000781 RID: 1921
	public class OtherKeyAttribute : Asn1Encodable
	{
		// Token: 0x06004512 RID: 17682 RVA: 0x00192278 File Offset: 0x00190478
		public static OtherKeyAttribute GetInstance(object obj)
		{
			if (obj == null || obj is OtherKeyAttribute)
			{
				return (OtherKeyAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherKeyAttribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x001922C5 File Offset: 0x001904C5
		public OtherKeyAttribute(Asn1Sequence seq)
		{
			this.keyAttrId = (DerObjectIdentifier)seq[0];
			this.keyAttr = seq[1];
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x001922EC File Offset: 0x001904EC
		public OtherKeyAttribute(DerObjectIdentifier keyAttrId, Asn1Encodable keyAttr)
		{
			this.keyAttrId = keyAttrId;
			this.keyAttr = keyAttr;
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x00192302 File Offset: 0x00190502
		public DerObjectIdentifier KeyAttrId
		{
			get
			{
				return this.keyAttrId;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x0019230A File Offset: 0x0019050A
		public Asn1Encodable KeyAttr
		{
			get
			{
				return this.keyAttr;
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x00192312 File Offset: 0x00190512
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.keyAttrId,
				this.keyAttr
			});
		}

		// Token: 0x04002C27 RID: 11303
		private DerObjectIdentifier keyAttrId;

		// Token: 0x04002C28 RID: 11304
		private Asn1Encodable keyAttr;
	}
}
