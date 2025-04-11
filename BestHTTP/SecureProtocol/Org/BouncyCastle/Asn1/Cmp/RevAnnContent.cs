using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B6 RID: 1974
	public class RevAnnContent : Asn1Encodable
	{
		// Token: 0x06004696 RID: 18070 RVA: 0x001963DC File Offset: 0x001945DC
		private RevAnnContent(Asn1Sequence seq)
		{
			this.status = PkiStatusEncodable.GetInstance(seq[0]);
			this.certId = CertId.GetInstance(seq[1]);
			this.willBeRevokedAt = DerGeneralizedTime.GetInstance(seq[2]);
			this.badSinceDate = DerGeneralizedTime.GetInstance(seq[3]);
			if (seq.Count > 4)
			{
				this.crlDetails = X509Extensions.GetInstance(seq[4]);
			}
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00196452 File Offset: 0x00194652
		public static RevAnnContent GetInstance(object obj)
		{
			if (obj is RevAnnContent)
			{
				return (RevAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x00196491 File Offset: 0x00194691
		public virtual PkiStatusEncodable Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00196499 File Offset: 0x00194699
		public virtual CertId CertID
		{
			get
			{
				return this.certId;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x001964A1 File Offset: 0x001946A1
		public virtual DerGeneralizedTime WillBeRevokedAt
		{
			get
			{
				return this.willBeRevokedAt;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x001964A9 File Offset: 0x001946A9
		public virtual DerGeneralizedTime BadSinceDate
		{
			get
			{
				return this.badSinceDate;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x001964B1 File Offset: 0x001946B1
		public virtual X509Extensions CrlDetails
		{
			get
			{
				return this.crlDetails;
			}
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001964BC File Offset: 0x001946BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status,
				this.certId,
				this.willBeRevokedAt,
				this.badSinceDate
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crlDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002D1C RID: 11548
		private readonly PkiStatusEncodable status;

		// Token: 0x04002D1D RID: 11549
		private readonly CertId certId;

		// Token: 0x04002D1E RID: 11550
		private readonly DerGeneralizedTime willBeRevokedAt;

		// Token: 0x04002D1F RID: 11551
		private readonly DerGeneralizedTime badSinceDate;

		// Token: 0x04002D20 RID: 11552
		private readonly X509Extensions crlDetails;
	}
}
