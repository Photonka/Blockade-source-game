using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000679 RID: 1657
	public class CertificateList : Asn1Encodable
	{
		// Token: 0x06003DC0 RID: 15808 RVA: 0x00177C79 File Offset: 0x00175E79
		public static CertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00177C87 File Offset: 0x00175E87
		public static CertificateList GetInstance(object obj)
		{
			if (obj is CertificateList)
			{
				return (CertificateList)obj;
			}
			if (obj != null)
			{
				return new CertificateList(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00177CA8 File Offset: 0x00175EA8
		private CertificateList(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for CertificateList", "seq");
			}
			this.tbsCertList = TbsCertificateList.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x00177D0A File Offset: 0x00175F0A
		public TbsCertificateList TbsCertList
		{
			get
			{
				return this.tbsCertList;
			}
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x00177D12 File Offset: 0x00175F12
		public CrlEntry[] GetRevokedCertificates()
		{
			return this.tbsCertList.GetRevokedCertificates();
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00177D1F File Offset: 0x00175F1F
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			return this.tbsCertList.GetRevokedCertificateEnumeration();
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x00177D2C File Offset: 0x00175F2C
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x00177D34 File Offset: 0x00175F34
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x00177D3C File Offset: 0x00175F3C
		public byte[] GetSignatureOctets()
		{
			return this.sig.GetOctets();
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x00177D49 File Offset: 0x00175F49
		public int Version
		{
			get
			{
				return this.tbsCertList.Version;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x00177D56 File Offset: 0x00175F56
		public X509Name Issuer
		{
			get
			{
				return this.tbsCertList.Issuer;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x00177D63 File Offset: 0x00175F63
		public Time ThisUpdate
		{
			get
			{
				return this.tbsCertList.ThisUpdate;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x00177D70 File Offset: 0x00175F70
		public Time NextUpdate
		{
			get
			{
				return this.tbsCertList.NextUpdate;
			}
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00177D7D File Offset: 0x00175F7D
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCertList,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04002655 RID: 9813
		private readonly TbsCertificateList tbsCertList;

		// Token: 0x04002656 RID: 9814
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04002657 RID: 9815
		private readonly DerBitString sig;
	}
}
