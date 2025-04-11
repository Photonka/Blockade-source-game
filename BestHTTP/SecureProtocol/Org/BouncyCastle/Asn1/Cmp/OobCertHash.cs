using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A4 RID: 1956
	public class OobCertHash : Asn1Encodable
	{
		// Token: 0x0600460D RID: 17933 RVA: 0x00194FDC File Offset: 0x001931DC
		private OobCertHash(Asn1Sequence seq)
		{
			int num = seq.Count - 1;
			this.hashVal = DerBitString.GetInstance(seq[num--]);
			for (int i = num; i >= 0; i--)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
				if (asn1TaggedObject.TagNo == 0)
				{
					this.hashAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.certId = CertId.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x0019504B File Offset: 0x0019324B
		public static OobCertHash GetInstance(object obj)
		{
			if (obj is OobCertHash)
			{
				return (OobCertHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OobCertHash((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x0019508A File Offset: 0x0019328A
		public virtual AlgorithmIdentifier HashAlg
		{
			get
			{
				return this.hashAlg;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x00195092 File Offset: 0x00193292
		public virtual CertId CertID
		{
			get
			{
				return this.certId;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x0019509C File Offset: 0x0019329C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			this.AddOptional(asn1EncodableVector, 0, this.hashAlg);
			this.AddOptional(asn1EncodableVector, 1, this.certId);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.hashVal
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00194FBE File Offset: 0x001931BE
		private void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, tagNo, obj)
				});
			}
		}

		// Token: 0x04002CA1 RID: 11425
		private readonly AlgorithmIdentifier hashAlg;

		// Token: 0x04002CA2 RID: 11426
		private readonly CertId certId;

		// Token: 0x04002CA3 RID: 11427
		private readonly DerBitString hashVal;
	}
}
