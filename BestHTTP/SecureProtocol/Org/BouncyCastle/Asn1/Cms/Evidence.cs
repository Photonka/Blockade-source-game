using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000776 RID: 1910
	public class Evidence : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060044B8 RID: 17592 RVA: 0x0019158F File Offset: 0x0018F78F
		public Evidence(TimeStampTokenEvidence tstEvidence)
		{
			this.tstEvidence = tstEvidence;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x0019159E File Offset: 0x0018F79E
		private Evidence(Asn1TaggedObject tagged)
		{
			if (tagged.TagNo == 0)
			{
				this.tstEvidence = TimeStampTokenEvidence.GetInstance(tagged, false);
			}
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x001915BB File Offset: 0x0018F7BB
		public static Evidence GetInstance(object obj)
		{
			if (obj is Evidence)
			{
				return (Evidence)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new Evidence(Asn1TaggedObject.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x001915FA File Offset: 0x0018F7FA
		public virtual TimeStampTokenEvidence TstEvidence
		{
			get
			{
				return this.tstEvidence;
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x00191602 File Offset: 0x0018F802
		public override Asn1Object ToAsn1Object()
		{
			if (this.tstEvidence != null)
			{
				return new DerTaggedObject(false, 0, this.tstEvidence);
			}
			return null;
		}

		// Token: 0x04002C09 RID: 11273
		private TimeStampTokenEvidence tstEvidence;
	}
}
