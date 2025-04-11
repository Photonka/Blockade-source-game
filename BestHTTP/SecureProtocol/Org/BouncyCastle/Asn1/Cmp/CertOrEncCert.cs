using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000797 RID: 1943
	public class CertOrEncCert : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060045BE RID: 17854 RVA: 0x0019407C File Offset: 0x0019227C
		private CertOrEncCert(Asn1TaggedObject tagged)
		{
			if (tagged.TagNo == 0)
			{
				this.certificate = CmpCertificate.GetInstance(tagged.GetObject());
				return;
			}
			if (tagged.TagNo == 1)
			{
				this.encryptedCert = EncryptedValue.GetInstance(tagged.GetObject());
				return;
			}
			throw new ArgumentException("unknown tag: " + tagged.TagNo, "tagged");
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x001940E3 File Offset: 0x001922E3
		public static CertOrEncCert GetInstance(object obj)
		{
			if (obj is CertOrEncCert)
			{
				return (CertOrEncCert)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new CertOrEncCert((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00194122 File Offset: 0x00192322
		public CertOrEncCert(CmpCertificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this.certificate = certificate;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x0019413F File Offset: 0x0019233F
		public CertOrEncCert(EncryptedValue encryptedCert)
		{
			if (encryptedCert == null)
			{
				throw new ArgumentNullException("encryptedCert");
			}
			this.encryptedCert = encryptedCert;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x0019415C File Offset: 0x0019235C
		public virtual CmpCertificate Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x00194164 File Offset: 0x00192364
		public virtual EncryptedValue EncryptedCert
		{
			get
			{
				return this.encryptedCert;
			}
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x0019416C File Offset: 0x0019236C
		public override Asn1Object ToAsn1Object()
		{
			if (this.certificate != null)
			{
				return new DerTaggedObject(true, 0, this.certificate);
			}
			return new DerTaggedObject(true, 1, this.encryptedCert);
		}

		// Token: 0x04002C6B RID: 11371
		private readonly CmpCertificate certificate;

		// Token: 0x04002C6C RID: 11372
		private readonly EncryptedValue encryptedCert;
	}
}
