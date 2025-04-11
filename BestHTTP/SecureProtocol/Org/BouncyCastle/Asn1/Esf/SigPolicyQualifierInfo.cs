using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000744 RID: 1860
	public class SigPolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x0600435D RID: 17245 RVA: 0x0018D1BC File Offset: 0x0018B3BC
		public static SigPolicyQualifierInfo GetInstance(object obj)
		{
			if (obj == null || obj is SigPolicyQualifierInfo)
			{
				return (SigPolicyQualifierInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SigPolicyQualifierInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SigPolicyQualifierInfo' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x0018D20C File Offset: 0x0018B40C
		private SigPolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyQualifierId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigQualifier = seq[1].ToAsn1Object();
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x0018D27F File Offset: 0x0018B47F
		public SigPolicyQualifierInfo(DerObjectIdentifier sigPolicyQualifierId, Asn1Encodable sigQualifier)
		{
			this.sigPolicyQualifierId = sigPolicyQualifierId;
			this.sigQualifier = sigQualifier.ToAsn1Object();
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x0018D29A File Offset: 0x0018B49A
		public DerObjectIdentifier SigPolicyQualifierId
		{
			get
			{
				return this.sigPolicyQualifierId;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x0018D2A2 File Offset: 0x0018B4A2
		public Asn1Object SigQualifier
		{
			get
			{
				return this.sigQualifier;
			}
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x0018D2AA File Offset: 0x0018B4AA
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.sigPolicyQualifierId,
				this.sigQualifier
			});
		}

		// Token: 0x04002B20 RID: 11040
		private readonly DerObjectIdentifier sigPolicyQualifierId;

		// Token: 0x04002B21 RID: 11041
		private readonly Asn1Object sigQualifier;
	}
}
