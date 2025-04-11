using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000792 RID: 1938
	public class TimeStampTokenEvidence : Asn1Encodable
	{
		// Token: 0x0600459E RID: 17822 RVA: 0x00193B87 File Offset: 0x00191D87
		public TimeStampTokenEvidence(TimeStampAndCrl[] timeStampAndCrls)
		{
			this.timeStampAndCrls = timeStampAndCrls;
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x00193B96 File Offset: 0x00191D96
		public TimeStampTokenEvidence(TimeStampAndCrl timeStampAndCrl)
		{
			this.timeStampAndCrls = new TimeStampAndCrl[]
			{
				timeStampAndCrl
			};
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00193BB0 File Offset: 0x00191DB0
		private TimeStampTokenEvidence(Asn1Sequence seq)
		{
			this.timeStampAndCrls = new TimeStampAndCrl[seq.Count];
			int num = 0;
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				this.timeStampAndCrls[num++] = TimeStampAndCrl.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00193C30 File Offset: 0x00191E30
		public static TimeStampTokenEvidence GetInstance(Asn1TaggedObject tagged, bool isExplicit)
		{
			return TimeStampTokenEvidence.GetInstance(Asn1Sequence.GetInstance(tagged, isExplicit));
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00193C3E File Offset: 0x00191E3E
		public static TimeStampTokenEvidence GetInstance(object obj)
		{
			if (obj is TimeStampTokenEvidence)
			{
				return (TimeStampTokenEvidence)obj;
			}
			if (obj != null)
			{
				return new TimeStampTokenEvidence(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x00193C5F File Offset: 0x00191E5F
		public virtual TimeStampAndCrl[] ToTimeStampAndCrlArray()
		{
			return (TimeStampAndCrl[])this.timeStampAndCrls.Clone();
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00193C74 File Offset: 0x00191E74
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.timeStampAndCrls;
			return new DerSequence(v);
		}

		// Token: 0x04002C61 RID: 11361
		private TimeStampAndCrl[] timeStampAndCrls;
	}
}
