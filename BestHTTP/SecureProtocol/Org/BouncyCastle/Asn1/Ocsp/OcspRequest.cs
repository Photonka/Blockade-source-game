using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F6 RID: 1782
	public class OcspRequest : Asn1Encodable
	{
		// Token: 0x06004172 RID: 16754 RVA: 0x00185D29 File Offset: 0x00183F29
		public static OcspRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x00185D38 File Offset: 0x00183F38
		public static OcspRequest GetInstance(object obj)
		{
			if (obj == null || obj is OcspRequest)
			{
				return (OcspRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00185D85 File Offset: 0x00183F85
		public OcspRequest(TbsRequest tbsRequest, Signature optionalSignature)
		{
			if (tbsRequest == null)
			{
				throw new ArgumentNullException("tbsRequest");
			}
			this.tbsRequest = tbsRequest;
			this.optionalSignature = optionalSignature;
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00185DA9 File Offset: 0x00183FA9
		private OcspRequest(Asn1Sequence seq)
		{
			this.tbsRequest = TbsRequest.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.optionalSignature = Signature.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x00185DE4 File Offset: 0x00183FE4
		public TbsRequest TbsRequest
		{
			get
			{
				return this.tbsRequest;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x00185DEC File Offset: 0x00183FEC
		public Signature OptionalSignature
		{
			get
			{
				return this.optionalSignature;
			}
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00185DF4 File Offset: 0x00183FF4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsRequest
			});
			if (this.optionalSignature != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.optionalSignature)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400297A RID: 10618
		private readonly TbsRequest tbsRequest;

		// Token: 0x0400297B RID: 10619
		private readonly Signature optionalSignature;
	}
}
