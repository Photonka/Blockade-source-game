using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006AB RID: 1707
	public class X509CertificateStructure : Asn1Encodable
	{
		// Token: 0x06003F4F RID: 16207 RVA: 0x0017C8F5 File Offset: 0x0017AAF5
		public static X509CertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509CertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x0017C903 File Offset: 0x0017AB03
		public static X509CertificateStructure GetInstance(object obj)
		{
			if (obj is X509CertificateStructure)
			{
				return (X509CertificateStructure)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new X509CertificateStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x0017C924 File Offset: 0x0017AB24
		public X509CertificateStructure(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlgID, DerBitString sig)
		{
			if (tbsCert == null)
			{
				throw new ArgumentNullException("tbsCert");
			}
			if (sigAlgID == null)
			{
				throw new ArgumentNullException("sigAlgID");
			}
			if (sig == null)
			{
				throw new ArgumentNullException("sig");
			}
			this.tbsCert = tbsCert;
			this.sigAlgID = sigAlgID;
			this.sig = sig;
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x0017C978 File Offset: 0x0017AB78
		private X509CertificateStructure(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for a certificate", "seq");
			}
			this.tbsCert = TbsCertificateStructure.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x0017C9DA File Offset: 0x0017ABDA
		public TbsCertificateStructure TbsCertificate
		{
			get
			{
				return this.tbsCert;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0017C9E2 File Offset: 0x0017ABE2
		public int Version
		{
			get
			{
				return this.tbsCert.Version;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x0017C9EF File Offset: 0x0017ABEF
		public DerInteger SerialNumber
		{
			get
			{
				return this.tbsCert.SerialNumber;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0017C9FC File Offset: 0x0017ABFC
		public X509Name Issuer
		{
			get
			{
				return this.tbsCert.Issuer;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x0017CA09 File Offset: 0x0017AC09
		public Time StartDate
		{
			get
			{
				return this.tbsCert.StartDate;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x0017CA16 File Offset: 0x0017AC16
		public Time EndDate
		{
			get
			{
				return this.tbsCert.EndDate;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x0017CA23 File Offset: 0x0017AC23
		public X509Name Subject
		{
			get
			{
				return this.tbsCert.Subject;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x0017CA30 File Offset: 0x0017AC30
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.tbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x0017CA3D File Offset: 0x0017AC3D
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x0017CA45 File Offset: 0x0017AC45
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x0017CA4D File Offset: 0x0017AC4D
		public byte[] GetSignatureOctets()
		{
			return this.sig.GetOctets();
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0017CA5A File Offset: 0x0017AC5A
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCert,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04002723 RID: 10019
		private readonly TbsCertificateStructure tbsCert;

		// Token: 0x04002724 RID: 10020
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04002725 RID: 10021
		private readonly DerBitString sig;
	}
}
