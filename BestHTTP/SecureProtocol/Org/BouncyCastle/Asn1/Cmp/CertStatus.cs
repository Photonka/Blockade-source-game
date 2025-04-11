using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079A RID: 1946
	public class CertStatus : Asn1Encodable
	{
		// Token: 0x060045D3 RID: 17875 RVA: 0x001944E0 File Offset: 0x001926E0
		private CertStatus(Asn1Sequence seq)
		{
			this.certHash = Asn1OctetString.GetInstance(seq[0]);
			this.certReqId = DerInteger.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.statusInfo = PkiStatusInfo.GetInstance(seq[2]);
			}
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00194532 File Offset: 0x00192732
		public CertStatus(byte[] certHash, BigInteger certReqId)
		{
			this.certHash = new DerOctetString(certHash);
			this.certReqId = new DerInteger(certReqId);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x00194552 File Offset: 0x00192752
		public CertStatus(byte[] certHash, BigInteger certReqId, PkiStatusInfo statusInfo)
		{
			this.certHash = new DerOctetString(certHash);
			this.certReqId = new DerInteger(certReqId);
			this.statusInfo = statusInfo;
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x00194579 File Offset: 0x00192779
		public static CertStatus GetInstance(object obj)
		{
			if (obj is CertStatus)
			{
				return (CertStatus)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertStatus((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x001945B8 File Offset: 0x001927B8
		public virtual Asn1OctetString CertHash
		{
			get
			{
				return this.certHash;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x001945C0 File Offset: 0x001927C0
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x001945C8 File Offset: 0x001927C8
		public virtual PkiStatusInfo StatusInfo
		{
			get
			{
				return this.statusInfo;
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x001945D0 File Offset: 0x001927D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certHash,
				this.certReqId
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.statusInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C73 RID: 11379
		private readonly Asn1OctetString certHash;

		// Token: 0x04002C74 RID: 11380
		private readonly DerInteger certReqId;

		// Token: 0x04002C75 RID: 11381
		private readonly PkiStatusInfo statusInfo;
	}
}
