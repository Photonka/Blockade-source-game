using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F2 RID: 1778
	public class CertID : Asn1Encodable
	{
		// Token: 0x0600415A RID: 16730 RVA: 0x0018589A File Offset: 0x00183A9A
		public static CertID GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertID.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x001858A8 File Offset: 0x00183AA8
		public static CertID GetInstance(object obj)
		{
			if (obj == null || obj is CertID)
			{
				return (CertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x001858F5 File Offset: 0x00183AF5
		public CertID(AlgorithmIdentifier hashAlgorithm, Asn1OctetString issuerNameHash, Asn1OctetString issuerKeyHash, DerInteger serialNumber)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.issuerNameHash = issuerNameHash;
			this.issuerKeyHash = issuerKeyHash;
			this.serialNumber = serialNumber;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0018591C File Offset: 0x00183B1C
		private CertID(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.issuerNameHash = Asn1OctetString.GetInstance(seq[1]);
			this.issuerKeyHash = Asn1OctetString.GetInstance(seq[2]);
			this.serialNumber = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x00185990 File Offset: 0x00183B90
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x00185998 File Offset: 0x00183B98
		public Asn1OctetString IssuerNameHash
		{
			get
			{
				return this.issuerNameHash;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x001859A0 File Offset: 0x00183BA0
		public Asn1OctetString IssuerKeyHash
		{
			get
			{
				return this.issuerKeyHash;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x001859A8 File Offset: 0x00183BA8
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x001859B0 File Offset: 0x00183BB0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.issuerNameHash,
				this.issuerKeyHash,
				this.serialNumber
			});
		}

		// Token: 0x04002968 RID: 10600
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002969 RID: 10601
		private readonly Asn1OctetString issuerNameHash;

		// Token: 0x0400296A RID: 10602
		private readonly Asn1OctetString issuerKeyHash;

		// Token: 0x0400296B RID: 10603
		private readonly DerInteger serialNumber;
	}
}
