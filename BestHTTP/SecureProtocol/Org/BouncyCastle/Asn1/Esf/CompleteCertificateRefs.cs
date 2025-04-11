using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200072F RID: 1839
	public class CompleteCertificateRefs : Asn1Encodable
	{
		// Token: 0x060042CD RID: 17101 RVA: 0x0018AF38 File Offset: 0x00189138
		public static CompleteCertificateRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteCertificateRefs)
			{
				return (CompleteCertificateRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteCertificateRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteCertificateRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0018AF88 File Offset: 0x00189188
		private CompleteCertificateRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				OtherCertID.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
			}
			this.otherCertIDs = seq;
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0018AFFC File Offset: 0x001891FC
		public CompleteCertificateRefs(params OtherCertID[] otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(otherCertIDs);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0018B02C File Offset: 0x0018922C
		public CompleteCertificateRefs(IEnumerable otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(otherCertIDs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(Asn1EncodableVector.FromEnumerable(otherCertIDs));
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0018B080 File Offset: 0x00189280
		public OtherCertID[] GetOtherCertIDs()
		{
			OtherCertID[] array = new OtherCertID[this.otherCertIDs.Count];
			for (int i = 0; i < this.otherCertIDs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.otherCertIDs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0018B0CE File Offset: 0x001892CE
		public override Asn1Object ToAsn1Object()
		{
			return this.otherCertIDs;
		}

		// Token: 0x04002AEA RID: 10986
		private readonly Asn1Sequence otherCertIDs;
	}
}
