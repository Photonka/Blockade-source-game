using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000711 RID: 1809
	public class AdmissionSyntax : Asn1Encodable
	{
		// Token: 0x06004208 RID: 16904 RVA: 0x00187DF0 File Offset: 0x00185FF0
		public static AdmissionSyntax GetInstance(object obj)
		{
			if (obj == null || obj is AdmissionSyntax)
			{
				return (AdmissionSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AdmissionSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x00187E40 File Offset: 0x00186040
		private AdmissionSyntax(Asn1Sequence seq)
		{
			int count = seq.Count;
			if (count == 1)
			{
				this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[0]);
				return;
			}
			if (count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.admissionAuthority = GeneralName.GetInstance(seq[0]);
			this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x00187EB6 File Offset: 0x001860B6
		public AdmissionSyntax(GeneralName admissionAuthority, Asn1Sequence contentsOfAdmissions)
		{
			this.admissionAuthority = admissionAuthority;
			this.contentsOfAdmissions = contentsOfAdmissions;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x00187ECC File Offset: 0x001860CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.admissionAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.admissionAuthority
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.contentsOfAdmissions
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x00187F1C File Offset: 0x0018611C
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x00187F24 File Offset: 0x00186124
		public virtual Admissions[] GetContentsOfAdmissions()
		{
			Admissions[] array = new Admissions[this.contentsOfAdmissions.Count];
			for (int i = 0; i < this.contentsOfAdmissions.Count; i++)
			{
				array[i] = Admissions.GetInstance(this.contentsOfAdmissions[i]);
			}
			return array;
		}

		// Token: 0x04002A2B RID: 10795
		private readonly GeneralName admissionAuthority;

		// Token: 0x04002A2C RID: 10796
		private readonly Asn1Sequence contentsOfAdmissions;
	}
}
