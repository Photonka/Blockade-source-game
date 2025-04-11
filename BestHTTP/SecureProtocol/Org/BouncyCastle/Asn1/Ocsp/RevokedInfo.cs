using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FD RID: 1789
	public class RevokedInfo : Asn1Encodable
	{
		// Token: 0x060041A3 RID: 16803 RVA: 0x001864C4 File Offset: 0x001846C4
		public static RevokedInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RevokedInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x001864D4 File Offset: 0x001846D4
		public static RevokedInfo GetInstance(object obj)
		{
			if (obj == null || obj is RevokedInfo)
			{
				return (RevokedInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevokedInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00186521 File Offset: 0x00184721
		public RevokedInfo(DerGeneralizedTime revocationTime) : this(revocationTime, null)
		{
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x0018652B File Offset: 0x0018472B
		public RevokedInfo(DerGeneralizedTime revocationTime, CrlReason revocationReason)
		{
			if (revocationTime == null)
			{
				throw new ArgumentNullException("revocationTime");
			}
			this.revocationTime = revocationTime;
			this.revocationReason = revocationReason;
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0018654F File Offset: 0x0018474F
		private RevokedInfo(Asn1Sequence seq)
		{
			this.revocationTime = (DerGeneralizedTime)seq[0];
			if (seq.Count > 1)
			{
				this.revocationReason = new CrlReason(DerEnumerated.GetInstance((Asn1TaggedObject)seq[1], true));
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x0018658F File Offset: 0x0018478F
		public DerGeneralizedTime RevocationTime
		{
			get
			{
				return this.revocationTime;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x00186597 File Offset: 0x00184797
		public CrlReason RevocationReason
		{
			get
			{
				return this.revocationReason;
			}
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x001865A0 File Offset: 0x001847A0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.revocationTime
			});
			if (this.revocationReason != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.revocationReason)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002990 RID: 10640
		private readonly DerGeneralizedTime revocationTime;

		// Token: 0x04002991 RID: 10641
		private readonly CrlReason revocationReason;
	}
}
