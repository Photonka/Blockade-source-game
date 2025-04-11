using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000789 RID: 1929
	public class ScvpReqRes : Asn1Encodable
	{
		// Token: 0x06004554 RID: 17748 RVA: 0x00192B6A File Offset: 0x00190D6A
		public static ScvpReqRes GetInstance(object obj)
		{
			if (obj is ScvpReqRes)
			{
				return (ScvpReqRes)obj;
			}
			if (obj != null)
			{
				return new ScvpReqRes(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00192B8C File Offset: 0x00190D8C
		private ScvpReqRes(Asn1Sequence seq)
		{
			if (seq[0] is Asn1TaggedObject)
			{
				this.request = ContentInfo.GetInstance(Asn1TaggedObject.GetInstance(seq[0]), true);
				this.response = ContentInfo.GetInstance(seq[1]);
				return;
			}
			this.request = null;
			this.response = ContentInfo.GetInstance(seq[0]);
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x00192BF1 File Offset: 0x00190DF1
		public ScvpReqRes(ContentInfo response) : this(null, response)
		{
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x00192BFB File Offset: 0x00190DFB
		public ScvpReqRes(ContentInfo request, ContentInfo response)
		{
			this.request = request;
			this.response = response;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x00192C11 File Offset: 0x00190E11
		public virtual ContentInfo Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x00192C19 File Offset: 0x00190E19
		public virtual ContentInfo Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00192C24 File Offset: 0x00190E24
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.request != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.request)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.response
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C38 RID: 11320
		private readonly ContentInfo request;

		// Token: 0x04002C39 RID: 11321
		private readonly ContentInfo response;
	}
}
