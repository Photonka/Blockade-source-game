using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FC RID: 1788
	public class ResponseData : Asn1Encodable
	{
		// Token: 0x06004197 RID: 16791 RVA: 0x00186287 File Offset: 0x00184487
		public static ResponseData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x00186298 File Offset: 0x00184498
		public static ResponseData GetInstance(object obj)
		{
			if (obj == null || obj is ResponseData)
			{
				return (ResponseData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x001862E5 File Offset: 0x001844E5
		public ResponseData(DerInteger version, ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions)
		{
			this.version = version;
			this.responderID = responderID;
			this.producedAt = producedAt;
			this.responses = responses;
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x00186312 File Offset: 0x00184512
		public ResponseData(ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions) : this(ResponseData.V1, responderID, producedAt, responses, responseExtensions)
		{
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x00186324 File Offset: 0x00184524
		private ResponseData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionPresent = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = ResponseData.V1;
				}
			}
			else
			{
				this.version = ResponseData.V1;
			}
			this.responderID = ResponderID.GetInstance(seq[num++]);
			this.producedAt = (DerGeneralizedTime)seq[num++];
			this.responses = (Asn1Sequence)seq[num++];
			if (seq.Count > num)
			{
				this.responseExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x001863ED File Offset: 0x001845ED
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x001863F5 File Offset: 0x001845F5
		public ResponderID ResponderID
		{
			get
			{
				return this.responderID;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x001863FD File Offset: 0x001845FD
		public DerGeneralizedTime ProducedAt
		{
			get
			{
				return this.producedAt;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x00186405 File Offset: 0x00184605
		public Asn1Sequence Responses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x0018640D File Offset: 0x0018460D
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.responseExtensions;
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x00186418 File Offset: 0x00184618
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.versionPresent || !this.version.Equals(ResponseData.V1))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.version)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.responderID,
				this.producedAt,
				this.responses
			});
			if (this.responseExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.responseExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002989 RID: 10633
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x0400298A RID: 10634
		private readonly bool versionPresent;

		// Token: 0x0400298B RID: 10635
		private readonly DerInteger version;

		// Token: 0x0400298C RID: 10636
		private readonly ResponderID responderID;

		// Token: 0x0400298D RID: 10637
		private readonly DerGeneralizedTime producedAt;

		// Token: 0x0400298E RID: 10638
		private readonly Asn1Sequence responses;

		// Token: 0x0400298F RID: 10639
		private readonly X509Extensions responseExtensions;
	}
}
