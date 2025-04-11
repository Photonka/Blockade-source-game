using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000753 RID: 1875
	public class CertTemplate : Asn1Encodable
	{
		// Token: 0x060043B6 RID: 17334 RVA: 0x0018E7D8 File Offset: 0x0018C9D8
		private CertTemplate(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.version = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.serialNumber = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.signingAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 3:
					this.issuer = X509Name.GetInstance(asn1TaggedObject, true);
					break;
				case 4:
					this.validity = OptionalValidity.GetInstance(Asn1Sequence.GetInstance(asn1TaggedObject, false));
					break;
				case 5:
					this.subject = X509Name.GetInstance(asn1TaggedObject, true);
					break;
				case 6:
					this.publicKey = SubjectPublicKeyInfo.GetInstance(asn1TaggedObject, false);
					break;
				case 7:
					this.issuerUID = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 8:
					this.subjectUID = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 9:
					this.extensions = X509Extensions.GetInstance(asn1TaggedObject, false);
					break;
				default:
					throw new ArgumentException("unknown tag: " + asn1TaggedObject.TagNo, "seq");
				}
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0018E944 File Offset: 0x0018CB44
		public static CertTemplate GetInstance(object obj)
		{
			if (obj is CertTemplate)
			{
				return (CertTemplate)obj;
			}
			if (obj != null)
			{
				return new CertTemplate(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x0018E965 File Offset: 0x0018CB65
		public virtual int Version
		{
			get
			{
				return this.version.Value.IntValue;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x0018E977 File Offset: 0x0018CB77
		public virtual DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x0018E97F File Offset: 0x0018CB7F
		public virtual AlgorithmIdentifier SigningAlg
		{
			get
			{
				return this.signingAlg;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x0018E987 File Offset: 0x0018CB87
		public virtual X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x0018E98F File Offset: 0x0018CB8F
		public virtual OptionalValidity Validity
		{
			get
			{
				return this.validity;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x0018E997 File Offset: 0x0018CB97
		public virtual X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x0018E99F File Offset: 0x0018CB9F
		public virtual SubjectPublicKeyInfo PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x0018E9A7 File Offset: 0x0018CBA7
		public virtual DerBitString IssuerUID
		{
			get
			{
				return this.issuerUID;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x0018E9AF File Offset: 0x0018CBAF
		public virtual DerBitString SubjectUID
		{
			get
			{
				return this.subjectUID;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x0018E9B7 File Offset: 0x0018CBB7
		public virtual X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x0018E9BF File Offset: 0x0018CBBF
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04002B76 RID: 11126
		private readonly Asn1Sequence seq;

		// Token: 0x04002B77 RID: 11127
		private readonly DerInteger version;

		// Token: 0x04002B78 RID: 11128
		private readonly DerInteger serialNumber;

		// Token: 0x04002B79 RID: 11129
		private readonly AlgorithmIdentifier signingAlg;

		// Token: 0x04002B7A RID: 11130
		private readonly X509Name issuer;

		// Token: 0x04002B7B RID: 11131
		private readonly OptionalValidity validity;

		// Token: 0x04002B7C RID: 11132
		private readonly X509Name subject;

		// Token: 0x04002B7D RID: 11133
		private readonly SubjectPublicKeyInfo publicKey;

		// Token: 0x04002B7E RID: 11134
		private readonly DerBitString issuerUID;

		// Token: 0x04002B7F RID: 11135
		private readonly DerBitString subjectUID;

		// Token: 0x04002B80 RID: 11136
		private readonly X509Extensions extensions;
	}
}
