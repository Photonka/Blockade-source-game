using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000782 RID: 1922
	public class OtherRecipientInfo : Asn1Encodable
	{
		// Token: 0x06004518 RID: 17688 RVA: 0x00192331 File Offset: 0x00190531
		public OtherRecipientInfo(DerObjectIdentifier oriType, Asn1Encodable oriValue)
		{
			this.oriType = oriType;
			this.oriValue = oriValue;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00192347 File Offset: 0x00190547
		[Obsolete("Use GetInstance() instead")]
		public OtherRecipientInfo(Asn1Sequence seq)
		{
			this.oriType = DerObjectIdentifier.GetInstance(seq[0]);
			this.oriValue = seq[1];
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0019236E File Offset: 0x0019056E
		public static OtherRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OtherRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x0019237C File Offset: 0x0019057C
		public static OtherRecipientInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			OtherRecipientInfo otherRecipientInfo = obj as OtherRecipientInfo;
			if (otherRecipientInfo != null)
			{
				return otherRecipientInfo;
			}
			return new OtherRecipientInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x001923A5 File Offset: 0x001905A5
		public virtual DerObjectIdentifier OriType
		{
			get
			{
				return this.oriType;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x001923AD File Offset: 0x001905AD
		public virtual Asn1Encodable OriValue
		{
			get
			{
				return this.oriValue;
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x001923B5 File Offset: 0x001905B5
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.oriType,
				this.oriValue
			});
		}

		// Token: 0x04002C29 RID: 11305
		private readonly DerObjectIdentifier oriType;

		// Token: 0x04002C2A RID: 11306
		private readonly Asn1Encodable oriValue;
	}
}
