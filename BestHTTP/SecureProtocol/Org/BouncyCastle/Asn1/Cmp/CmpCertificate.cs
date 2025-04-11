using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079C RID: 1948
	public class CmpCertificate : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x00194719 File Offset: 0x00192919
		public CmpCertificate(AttributeCertificate x509v2AttrCert)
		{
			this.x509v2AttrCert = x509v2AttrCert;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x00194728 File Offset: 0x00192928
		public CmpCertificate(X509CertificateStructure x509v3PKCert)
		{
			if (x509v3PKCert.Version != 3)
			{
				throw new ArgumentException("only version 3 certificates allowed", "x509v3PKCert");
			}
			this.x509v3PKCert = x509v3PKCert;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x00194750 File Offset: 0x00192950
		public static CmpCertificate GetInstance(object obj)
		{
			if (obj is CmpCertificate)
			{
				return (CmpCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CmpCertificate(X509CertificateStructure.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject)
			{
				return new CmpCertificate(AttributeCertificate.GetInstance(((Asn1TaggedObject)obj).GetObject()));
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x001947B8 File Offset: 0x001929B8
		public virtual bool IsX509v3PKCert
		{
			get
			{
				return this.x509v3PKCert != null;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x001947C3 File Offset: 0x001929C3
		public virtual X509CertificateStructure X509v3PKCert
		{
			get
			{
				return this.x509v3PKCert;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x001947CB File Offset: 0x001929CB
		public virtual AttributeCertificate X509v2AttrCert
		{
			get
			{
				return this.x509v2AttrCert;
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x001947D3 File Offset: 0x001929D3
		public override Asn1Object ToAsn1Object()
		{
			if (this.x509v2AttrCert != null)
			{
				return new DerTaggedObject(true, 1, this.x509v2AttrCert);
			}
			return this.x509v3PKCert.ToAsn1Object();
		}

		// Token: 0x04002C79 RID: 11385
		private readonly X509CertificateStructure x509v3PKCert;

		// Token: 0x04002C7A RID: 11386
		private readonly AttributeCertificate x509v2AttrCert;
	}
}
