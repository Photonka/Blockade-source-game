using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000783 RID: 1923
	public class OtherRevocationInfoFormat : Asn1Encodable
	{
		// Token: 0x0600451F RID: 17695 RVA: 0x001923D4 File Offset: 0x001905D4
		public OtherRevocationInfoFormat(DerObjectIdentifier otherRevInfoFormat, Asn1Encodable otherRevInfo)
		{
			this.otherRevInfoFormat = otherRevInfoFormat;
			this.otherRevInfo = otherRevInfo;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001923EA File Offset: 0x001905EA
		private OtherRevocationInfoFormat(Asn1Sequence seq)
		{
			this.otherRevInfoFormat = DerObjectIdentifier.GetInstance(seq[0]);
			this.otherRevInfo = seq[1];
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00192411 File Offset: 0x00190611
		public static OtherRevocationInfoFormat GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return OtherRevocationInfoFormat.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0019241F File Offset: 0x0019061F
		public static OtherRevocationInfoFormat GetInstance(object obj)
		{
			if (obj is OtherRevocationInfoFormat)
			{
				return (OtherRevocationInfoFormat)obj;
			}
			if (obj != null)
			{
				return new OtherRevocationInfoFormat(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x00192440 File Offset: 0x00190640
		public virtual DerObjectIdentifier InfoFormat
		{
			get
			{
				return this.otherRevInfoFormat;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x00192448 File Offset: 0x00190648
		public virtual Asn1Encodable Info
		{
			get
			{
				return this.otherRevInfo;
			}
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x00192450 File Offset: 0x00190650
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevInfoFormat,
				this.otherRevInfo
			});
		}

		// Token: 0x04002C2B RID: 11307
		private readonly DerObjectIdentifier otherRevInfoFormat;

		// Token: 0x04002C2C RID: 11308
		private readonly Asn1Encodable otherRevInfo;
	}
}
