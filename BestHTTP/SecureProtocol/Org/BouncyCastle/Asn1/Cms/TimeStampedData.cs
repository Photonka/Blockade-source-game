using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000790 RID: 1936
	public class TimeStampedData : Asn1Encodable
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x001938F9 File Offset: 0x00191AF9
		public TimeStampedData(DerIA5String dataUri, MetaData metaData, Asn1OctetString content, Evidence temporalEvidence)
		{
			this.version = new DerInteger(1);
			this.dataUri = dataUri;
			this.metaData = metaData;
			this.content = content;
			this.temporalEvidence = temporalEvidence;
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x0019392C File Offset: 0x00191B2C
		private TimeStampedData(Asn1Sequence seq)
		{
			this.version = DerInteger.GetInstance(seq[0]);
			int index = 1;
			if (seq[index] is DerIA5String)
			{
				this.dataUri = DerIA5String.GetInstance(seq[index++]);
			}
			if (seq[index] is MetaData || seq[index] is Asn1Sequence)
			{
				this.metaData = MetaData.GetInstance(seq[index++]);
			}
			if (seq[index] is Asn1OctetString)
			{
				this.content = Asn1OctetString.GetInstance(seq[index++]);
			}
			this.temporalEvidence = Evidence.GetInstance(seq[index]);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x001939DF File Offset: 0x00191BDF
		public static TimeStampedData GetInstance(object obj)
		{
			if (obj is TimeStampedData)
			{
				return (TimeStampedData)obj;
			}
			if (obj != null)
			{
				return new TimeStampedData(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00193A00 File Offset: 0x00191C00
		public virtual DerIA5String DataUri
		{
			get
			{
				return this.dataUri;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x00193A08 File Offset: 0x00191C08
		public MetaData MetaData
		{
			get
			{
				return this.metaData;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004595 RID: 17813 RVA: 0x00193A10 File Offset: 0x00191C10
		public Asn1OctetString Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x00193A18 File Offset: 0x00191C18
		public Evidence TemporalEvidence
		{
			get
			{
				return this.temporalEvidence;
			}
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00193A20 File Offset: 0x00191C20
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.dataUri,
				this.metaData,
				this.content
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.temporalEvidence
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C56 RID: 11350
		private DerInteger version;

		// Token: 0x04002C57 RID: 11351
		private DerIA5String dataUri;

		// Token: 0x04002C58 RID: 11352
		private MetaData metaData;

		// Token: 0x04002C59 RID: 11353
		private Asn1OctetString content;

		// Token: 0x04002C5A RID: 11354
		private Evidence temporalEvidence;
	}
}
