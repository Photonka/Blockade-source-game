using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073E RID: 1854
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x0600432B RID: 17195 RVA: 0x0018C464 File Offset: 0x0018A664
		public static OtherSigningCertificate GetInstance(object obj)
		{
			if (obj == null || obj is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherSigningCertificate' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0018C4B4 File Offset: 0x0018A6B4
		private OtherSigningCertificate(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.certs = Asn1Sequence.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0018C53E File Offset: 0x0018A73E
		public OtherSigningCertificate(params OtherCertID[] certs) : this(certs, null)
		{
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0018C548 File Offset: 0x0018A748
		public OtherSigningCertificate(OtherCertID[] certs, params PolicyInformation[] policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0018C588 File Offset: 0x0018A788
		public OtherSigningCertificate(IEnumerable certs) : this(certs, null)
		{
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x0018C594 File Offset: 0x0018A794
		public OtherSigningCertificate(IEnumerable certs, IEnumerable policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "certs");
			}
			this.certs = new DerSequence(Asn1EncodableVector.FromEnumerable(certs));
			if (policies != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(policies, typeof(PolicyInformation)))
				{
					throw new ArgumentException("Must contain only 'PolicyInformation' objects", "policies");
				}
				this.policies = new DerSequence(Asn1EncodableVector.FromEnumerable(policies));
			}
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0018C620 File Offset: 0x0018A820
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int i = 0; i < this.certs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.certs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0018C670 File Offset: 0x0018A870
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int i = 0; i < this.policies.Count; i++)
			{
				array[i] = PolicyInformation.GetInstance(this.policies[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x0018C6C8 File Offset: 0x0018A8C8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certs
			});
			if (this.policies != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.policies
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B12 RID: 11026
		private readonly Asn1Sequence certs;

		// Token: 0x04002B13 RID: 11027
		private readonly Asn1Sequence policies;
	}
}
