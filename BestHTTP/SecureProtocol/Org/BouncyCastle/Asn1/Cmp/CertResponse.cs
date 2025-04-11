using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000799 RID: 1945
	public class CertResponse : Asn1Encodable
	{
		// Token: 0x060045CB RID: 17867 RVA: 0x00194348 File Offset: 0x00192548
		private CertResponse(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.status = PkiStatusInfo.GetInstance(seq[1]);
			if (seq.Count >= 3)
			{
				if (seq.Count == 3)
				{
					Asn1Encodable asn1Encodable = seq[2];
					if (asn1Encodable is Asn1OctetString)
					{
						this.rspInfo = Asn1OctetString.GetInstance(asn1Encodable);
						return;
					}
					this.certifiedKeyPair = CertifiedKeyPair.GetInstance(asn1Encodable);
					return;
				}
				else
				{
					this.certifiedKeyPair = CertifiedKeyPair.GetInstance(seq[2]);
					this.rspInfo = Asn1OctetString.GetInstance(seq[3]);
				}
			}
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x001943DF File Offset: 0x001925DF
		public static CertResponse GetInstance(object obj)
		{
			if (obj is CertResponse)
			{
				return (CertResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x0019441E File Offset: 0x0019261E
		public CertResponse(DerInteger certReqId, PkiStatusInfo status) : this(certReqId, status, null, null)
		{
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x0019442C File Offset: 0x0019262C
		public CertResponse(DerInteger certReqId, PkiStatusInfo status, CertifiedKeyPair certifiedKeyPair, Asn1OctetString rspInfo)
		{
			if (certReqId == null)
			{
				throw new ArgumentNullException("certReqId");
			}
			if (status == null)
			{
				throw new ArgumentNullException("status");
			}
			this.certReqId = certReqId;
			this.status = status;
			this.certifiedKeyPair = certifiedKeyPair;
			this.rspInfo = rspInfo;
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x00194478 File Offset: 0x00192678
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x00194480 File Offset: 0x00192680
		public virtual PkiStatusInfo Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x00194488 File Offset: 0x00192688
		public virtual CertifiedKeyPair CertifiedKeyPair
		{
			get
			{
				return this.certifiedKeyPair;
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x00194490 File Offset: 0x00192690
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.status
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.certifiedKeyPair,
				this.rspInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C6F RID: 11375
		private readonly DerInteger certReqId;

		// Token: 0x04002C70 RID: 11376
		private readonly PkiStatusInfo status;

		// Token: 0x04002C71 RID: 11377
		private readonly CertifiedKeyPair certifiedKeyPair;

		// Token: 0x04002C72 RID: 11378
		private readonly Asn1OctetString rspInfo;
	}
}
