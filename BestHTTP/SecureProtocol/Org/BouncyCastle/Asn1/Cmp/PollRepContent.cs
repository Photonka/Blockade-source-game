using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B1 RID: 1969
	public class PollRepContent : Asn1Encodable
	{
		// Token: 0x0600467B RID: 18043 RVA: 0x00195FF8 File Offset: 0x001941F8
		private PollRepContent(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.checkAfter = DerInteger.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.reason = PkiFreeText.GetInstance(seq[2]);
			}
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x0019604A File Offset: 0x0019424A
		public static PollRepContent GetInstance(object obj)
		{
			if (obj is PollRepContent)
			{
				return (PollRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PollRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00196089 File Offset: 0x00194289
		public PollRepContent(DerInteger certReqId, DerInteger checkAfter)
		{
			this.certReqId = certReqId;
			this.checkAfter = checkAfter;
			this.reason = null;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001960A6 File Offset: 0x001942A6
		public PollRepContent(DerInteger certReqId, DerInteger checkAfter, PkiFreeText reason)
		{
			this.certReqId = certReqId;
			this.checkAfter = checkAfter;
			this.reason = reason;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x001960C3 File Offset: 0x001942C3
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x001960CB File Offset: 0x001942CB
		public virtual DerInteger CheckAfter
		{
			get
			{
				return this.checkAfter;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x001960D3 File Offset: 0x001942D3
		public virtual PkiFreeText Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001960DC File Offset: 0x001942DC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.checkAfter
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.reason
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002D14 RID: 11540
		private readonly DerInteger certReqId;

		// Token: 0x04002D15 RID: 11541
		private readonly DerInteger checkAfter;

		// Token: 0x04002D16 RID: 11542
		private readonly PkiFreeText reason;
	}
}
