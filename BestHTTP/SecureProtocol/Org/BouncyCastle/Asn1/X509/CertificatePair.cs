using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200067A RID: 1658
	public class CertificatePair : Asn1Encodable
	{
		// Token: 0x06003DCE RID: 15822 RVA: 0x00177DA8 File Offset: 0x00175FA8
		public static CertificatePair GetInstance(object obj)
		{
			if (obj == null || obj is CertificatePair)
			{
				return (CertificatePair)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificatePair((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00177DF8 File Offset: 0x00175FF8
		private CertificatePair(Asn1Sequence seq)
		{
			if (seq.Count != 1 && seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				if (instance.TagNo == 0)
				{
					this.forward = X509CertificateStructure.GetInstance(instance, true);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.reverse = X509CertificateStructure.GetInstance(instance, true);
				}
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x00177EC8 File Offset: 0x001760C8
		public CertificatePair(X509CertificateStructure forward, X509CertificateStructure reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x00177EE0 File Offset: 0x001760E0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.forward != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.forward)
				});
			}
			if (this.reverse != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(1, this.reverse)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x00177F44 File Offset: 0x00176144
		public X509CertificateStructure Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x00177F4C File Offset: 0x0017614C
		public X509CertificateStructure Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x04002658 RID: 9816
		private X509CertificateStructure forward;

		// Token: 0x04002659 RID: 9817
		private X509CertificateStructure reverse;
	}
}
