using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075A RID: 1882
	public class OptionalValidity : Asn1Encodable
	{
		// Token: 0x060043F2 RID: 17394 RVA: 0x0018F030 File Offset: 0x0018D230
		private OptionalValidity(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.notBefore = Time.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.notAfter = Time.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x0018F0A8 File Offset: 0x0018D2A8
		public static OptionalValidity GetInstance(object obj)
		{
			if (obj == null || obj is OptionalValidity)
			{
				return (OptionalValidity)obj;
			}
			return new OptionalValidity(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0018F0C7 File Offset: 0x0018D2C7
		public virtual Time NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0018F0CF File Offset: 0x0018D2CF
		public virtual Time NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x0018F0D8 File Offset: 0x0018D2D8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.notBefore != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.notBefore)
				});
			}
			if (this.notAfter != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.notAfter)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B9E RID: 11166
		private readonly Time notBefore;

		// Token: 0x04002B9F RID: 11167
		private readonly Time notAfter;
	}
}
