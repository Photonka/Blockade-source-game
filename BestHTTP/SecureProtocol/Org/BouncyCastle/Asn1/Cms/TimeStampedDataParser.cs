using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000791 RID: 1937
	public class TimeStampedDataParser
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x00193A84 File Offset: 0x00191C84
		private TimeStampedDataParser(Asn1SequenceParser parser)
		{
			this.parser = parser;
			this.version = DerInteger.GetInstance(parser.ReadObject());
			Asn1Object asn1Object = parser.ReadObject().ToAsn1Object();
			if (asn1Object is DerIA5String)
			{
				this.dataUri = DerIA5String.GetInstance(asn1Object);
				asn1Object = parser.ReadObject().ToAsn1Object();
			}
			if (asn1Object is Asn1SequenceParser)
			{
				this.metaData = MetaData.GetInstance(asn1Object.ToAsn1Object());
				asn1Object = parser.ReadObject().ToAsn1Object();
			}
			if (asn1Object is Asn1OctetStringParser)
			{
				this.content = (Asn1OctetStringParser)asn1Object;
			}
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00193B14 File Offset: 0x00191D14
		public static TimeStampedDataParser GetInstance(object obj)
		{
			if (obj is Asn1Sequence)
			{
				return new TimeStampedDataParser(((Asn1Sequence)obj).Parser);
			}
			if (obj is Asn1SequenceParser)
			{
				return new TimeStampedDataParser((Asn1SequenceParser)obj);
			}
			return null;
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x00193B44 File Offset: 0x00191D44
		public virtual DerIA5String DataUri
		{
			get
			{
				return this.dataUri;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x00193B4C File Offset: 0x00191D4C
		public virtual MetaData MetaData
		{
			get
			{
				return this.metaData;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x00193B54 File Offset: 0x00191D54
		public virtual Asn1OctetStringParser Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x00193B5C File Offset: 0x00191D5C
		public virtual Evidence GetTemporalEvidence()
		{
			if (this.temporalEvidence == null)
			{
				this.temporalEvidence = Evidence.GetInstance(this.parser.ReadObject().ToAsn1Object());
			}
			return this.temporalEvidence;
		}

		// Token: 0x04002C5B RID: 11355
		private DerInteger version;

		// Token: 0x04002C5C RID: 11356
		private DerIA5String dataUri;

		// Token: 0x04002C5D RID: 11357
		private MetaData metaData;

		// Token: 0x04002C5E RID: 11358
		private Asn1OctetStringParser content;

		// Token: 0x04002C5F RID: 11359
		private Evidence temporalEvidence;

		// Token: 0x04002C60 RID: 11360
		private Asn1SequenceParser parser;
	}
}
