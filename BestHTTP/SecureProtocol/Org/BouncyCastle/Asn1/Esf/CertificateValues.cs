using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200072B RID: 1835
	public class CertificateValues : Asn1Encodable
	{
		// Token: 0x060042B7 RID: 17079 RVA: 0x0018AA98 File Offset: 0x00188C98
		public static CertificateValues GetInstance(object obj)
		{
			if (obj == null || obj is CertificateValues)
			{
				return (CertificateValues)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificateValues((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CertificateValues' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0018AAE8 File Offset: 0x00188CE8
		private CertificateValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				X509CertificateStructure.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
			}
			this.certificates = seq;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0018AB5C File Offset: 0x00188D5C
		public CertificateValues(params X509CertificateStructure[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			this.certificates = new DerSequence(certificates);
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0018AB8C File Offset: 0x00188D8C
		public CertificateValues(IEnumerable certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certificates, typeof(X509CertificateStructure)))
			{
				throw new ArgumentException("Must contain only 'X509CertificateStructure' objects", "certificates");
			}
			this.certificates = new DerSequence(Asn1EncodableVector.FromEnumerable(certificates));
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0018ABE0 File Offset: 0x00188DE0
		public X509CertificateStructure[] GetCertificates()
		{
			X509CertificateStructure[] array = new X509CertificateStructure[this.certificates.Count];
			for (int i = 0; i < this.certificates.Count; i++)
			{
				array[i] = X509CertificateStructure.GetInstance(this.certificates[i]);
			}
			return array;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0018AC29 File Offset: 0x00188E29
		public override Asn1Object ToAsn1Object()
		{
			return this.certificates;
		}

		// Token: 0x04002ADF RID: 10975
		private readonly Asn1Sequence certificates;
	}
}
