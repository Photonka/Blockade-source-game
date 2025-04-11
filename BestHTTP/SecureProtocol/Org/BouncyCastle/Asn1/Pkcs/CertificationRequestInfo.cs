using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D8 RID: 1752
	public class CertificationRequestInfo : Asn1Encodable
	{
		// Token: 0x06004096 RID: 16534 RVA: 0x00182E0F File Offset: 0x0018100F
		public static CertificationRequestInfo GetInstance(object obj)
		{
			if (obj is CertificationRequestInfo)
			{
				return (CertificationRequestInfo)obj;
			}
			if (obj != null)
			{
				return new CertificationRequestInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x00182E30 File Offset: 0x00181030
		public CertificationRequestInfo(X509Name subject, SubjectPublicKeyInfo pkInfo, Asn1Set attributes)
		{
			this.subject = subject;
			this.subjectPKInfo = pkInfo;
			this.attributes = attributes;
			CertificationRequestInfo.ValidateAttributes(attributes);
			if (subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x00182E88 File Offset: 0x00181088
		private CertificationRequestInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.subject = X509Name.GetInstance(seq[1]);
			this.subjectPKInfo = SubjectPublicKeyInfo.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				DerTaggedObject obj = (DerTaggedObject)seq[3];
				this.attributes = Asn1Set.GetInstance(obj, false);
			}
			CertificationRequestInfo.ValidateAttributes(this.attributes);
			if (this.subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x00182F2E File Offset: 0x0018112E
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x00182F36 File Offset: 0x00181136
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x00182F3E File Offset: 0x0018113E
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPKInfo;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x00182F46 File Offset: 0x00181146
		public Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x00182F50 File Offset: 0x00181150
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.subject,
				this.subjectPKInfo
			});
			if (this.attributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.attributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x00182FB0 File Offset: 0x001811B0
		private static void ValidateAttributes(Asn1Set attributes)
		{
			if (attributes == null)
			{
				return;
			}
			foreach (object obj in attributes)
			{
				AttributePkcs instance = AttributePkcs.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
				if (instance.AttrType.Equals(PkcsObjectIdentifiers.Pkcs9AtChallengePassword) && instance.AttrValues.Count != 1)
				{
					throw new ArgumentException("challengePassword attribute must have one value");
				}
			}
		}

		// Token: 0x0400287B RID: 10363
		internal DerInteger version = new DerInteger(0);

		// Token: 0x0400287C RID: 10364
		internal X509Name subject;

		// Token: 0x0400287D RID: 10365
		internal SubjectPublicKeyInfo subjectPKInfo;

		// Token: 0x0400287E RID: 10366
		internal Asn1Set attributes;
	}
}
