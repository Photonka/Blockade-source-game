using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000673 RID: 1651
	public class AttributeCertificate : Asn1Encodable
	{
		// Token: 0x06003D87 RID: 15751 RVA: 0x001771C5 File Offset: 0x001753C5
		public static AttributeCertificate GetInstance(object obj)
		{
			if (obj is AttributeCertificate)
			{
				return (AttributeCertificate)obj;
			}
			if (obj != null)
			{
				return new AttributeCertificate(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x001771E6 File Offset: 0x001753E6
		public AttributeCertificate(AttributeCertificateInfo acinfo, AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue)
		{
			this.acinfo = acinfo;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x00177204 File Offset: 0x00175404
		private AttributeCertificate(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.acinfo = AttributeCertificateInfo.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signatureValue = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x00177271 File Offset: 0x00175471
		public AttributeCertificateInfo ACInfo
		{
			get
			{
				return this.acinfo;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x00177279 File Offset: 0x00175479
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x00177281 File Offset: 0x00175481
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x00177289 File Offset: 0x00175489
		public byte[] GetSignatureOctets()
		{
			return this.signatureValue.GetOctets();
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x00177296 File Offset: 0x00175496
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.acinfo,
				this.signatureAlgorithm,
				this.signatureValue
			});
		}

		// Token: 0x04002642 RID: 9794
		private readonly AttributeCertificateInfo acinfo;

		// Token: 0x04002643 RID: 9795
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04002644 RID: 9796
		private readonly DerBitString signatureValue;
	}
}
