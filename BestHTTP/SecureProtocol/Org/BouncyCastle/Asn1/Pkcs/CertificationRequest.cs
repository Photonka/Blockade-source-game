using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D7 RID: 1751
	public class CertificationRequest : Asn1Encodable
	{
		// Token: 0x0600408D RID: 16525 RVA: 0x00182D20 File Offset: 0x00180F20
		public static CertificationRequest GetInstance(object obj)
		{
			if (obj is CertificationRequest)
			{
				return (CertificationRequest)obj;
			}
			if (obj != null)
			{
				return new CertificationRequest((Asn1Sequence)obj);
			}
			return null;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0017018D File Offset: 0x0016E38D
		protected CertificationRequest()
		{
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x00182D41 File Offset: 0x00180F41
		public CertificationRequest(CertificationRequestInfo requestInfo, AlgorithmIdentifier algorithm, DerBitString signature)
		{
			this.reqInfo = requestInfo;
			this.sigAlgId = algorithm;
			this.sigBits = signature;
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x00182D60 File Offset: 0x00180F60
		[Obsolete("Use 'GetInstance' instead")]
		public CertificationRequest(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.reqInfo = CertificationRequestInfo.GetInstance(seq[0]);
			this.sigAlgId = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sigBits = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x00182DC2 File Offset: 0x00180FC2
		public CertificationRequestInfo GetCertificationRequestInfo()
		{
			return this.reqInfo;
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x00182DCA File Offset: 0x00180FCA
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgId;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x00182DD2 File Offset: 0x00180FD2
		public DerBitString Signature
		{
			get
			{
				return this.sigBits;
			}
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x00182DDA File Offset: 0x00180FDA
		public byte[] GetSignatureOctets()
		{
			return this.sigBits.GetOctets();
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x00182DE7 File Offset: 0x00180FE7
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.reqInfo,
				this.sigAlgId,
				this.sigBits
			});
		}

		// Token: 0x04002878 RID: 10360
		protected CertificationRequestInfo reqInfo;

		// Token: 0x04002879 RID: 10361
		protected AlgorithmIdentifier sigAlgId;

		// Token: 0x0400287A RID: 10362
		protected DerBitString sigBits;
	}
}
