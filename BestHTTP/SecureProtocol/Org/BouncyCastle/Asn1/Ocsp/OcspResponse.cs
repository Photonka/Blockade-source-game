using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F7 RID: 1783
	public class OcspResponse : Asn1Encodable
	{
		// Token: 0x06004179 RID: 16761 RVA: 0x00185E40 File Offset: 0x00184040
		public static OcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x00185E50 File Offset: 0x00184050
		public static OcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponse)
			{
				return (OcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x00185E9D File Offset: 0x0018409D
		public OcspResponse(OcspResponseStatus responseStatus, ResponseBytes responseBytes)
		{
			if (responseStatus == null)
			{
				throw new ArgumentNullException("responseStatus");
			}
			this.responseStatus = responseStatus;
			this.responseBytes = responseBytes;
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x00185EC1 File Offset: 0x001840C1
		private OcspResponse(Asn1Sequence seq)
		{
			this.responseStatus = new OcspResponseStatus(DerEnumerated.GetInstance(seq[0]));
			if (seq.Count == 2)
			{
				this.responseBytes = ResponseBytes.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x00185F01 File Offset: 0x00184101
		public OcspResponseStatus ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x00185F09 File Offset: 0x00184109
		public ResponseBytes ResponseBytes
		{
			get
			{
				return this.responseBytes;
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x00185F14 File Offset: 0x00184114
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.responseStatus
			});
			if (this.responseBytes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.responseBytes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400297C RID: 10620
		private readonly OcspResponseStatus responseStatus;

		// Token: 0x0400297D RID: 10621
		private readonly ResponseBytes responseBytes;
	}
}
