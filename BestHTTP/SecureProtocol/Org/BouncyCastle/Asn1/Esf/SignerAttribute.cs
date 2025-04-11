using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000742 RID: 1858
	public class SignerAttribute : Asn1Encodable
	{
		// Token: 0x0600434A RID: 17226 RVA: 0x0018CE06 File Offset: 0x0018B006
		public static SignerAttribute GetInstance(object obj)
		{
			if (obj == null || obj is SignerAttribute)
			{
				return (SignerAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerAttribute(obj);
			}
			throw new ArgumentException("Unknown object in 'SignerAttribute' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x0018CE44 File Offset: 0x0018B044
		private SignerAttribute(object obj)
		{
			DerTaggedObject derTaggedObject = (DerTaggedObject)((Asn1Sequence)obj)[0];
			if (derTaggedObject.TagNo == 0)
			{
				this.claimedAttributes = Asn1Sequence.GetInstance(derTaggedObject, true);
				return;
			}
			if (derTaggedObject.TagNo == 1)
			{
				this.certifiedAttributes = AttributeCertificate.GetInstance(derTaggedObject);
				return;
			}
			throw new ArgumentException("illegal tag.", "obj");
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x0018CEA4 File Offset: 0x0018B0A4
		public SignerAttribute(Asn1Sequence claimedAttributes)
		{
			this.claimedAttributes = claimedAttributes;
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0018CEB3 File Offset: 0x0018B0B3
		public SignerAttribute(AttributeCertificate certifiedAttributes)
		{
			this.certifiedAttributes = certifiedAttributes;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x0018CEC2 File Offset: 0x0018B0C2
		public virtual Asn1Sequence ClaimedAttributes
		{
			get
			{
				return this.claimedAttributes;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x0018CECA File Offset: 0x0018B0CA
		public virtual AttributeCertificate CertifiedAttributes
		{
			get
			{
				return this.certifiedAttributes;
			}
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x0018CED4 File Offset: 0x0018B0D4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.claimedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.claimedAttributes)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(1, this.certifiedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B1B RID: 11035
		private Asn1Sequence claimedAttributes;

		// Token: 0x04002B1C RID: 11036
		private AttributeCertificate certifiedAttributes;
	}
}
