using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A0 RID: 1696
	public class TbsCertificateStructure : Asn1Encodable
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x0017B641 File Offset: 0x00179841
		public static TbsCertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x0017B64F File Offset: 0x0017984F
		public static TbsCertificateStructure GetInstance(object obj)
		{
			if (obj is TbsCertificateStructure)
			{
				return (TbsCertificateStructure)obj;
			}
			if (obj != null)
			{
				return new TbsCertificateStructure(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0017B670 File Offset: 0x00179870
		internal TbsCertificateStructure(Asn1Sequence seq)
		{
			int num = 0;
			this.seq = seq;
			if (seq[0] is DerTaggedObject)
			{
				this.version = DerInteger.GetInstance((Asn1TaggedObject)seq[0], true);
			}
			else
			{
				num = -1;
				this.version = new DerInteger(0);
			}
			bool flag = false;
			bool flag2 = false;
			if (this.version.Value.Equals(BigInteger.Zero))
			{
				flag = true;
			}
			else if (this.version.Value.Equals(BigInteger.One))
			{
				flag2 = true;
			}
			else if (!this.version.Value.Equals(BigInteger.Two))
			{
				throw new ArgumentException("version number not recognised");
			}
			this.serialNumber = DerInteger.GetInstance(seq[num + 1]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[num + 2]);
			this.issuer = X509Name.GetInstance(seq[num + 3]);
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[num + 4];
			this.startDate = Time.GetInstance(asn1Sequence[0]);
			this.endDate = Time.GetInstance(asn1Sequence[1]);
			this.subject = X509Name.GetInstance(seq[num + 5]);
			this.subjectPublicKeyInfo = SubjectPublicKeyInfo.GetInstance(seq[num + 6]);
			int i = seq.Count - (num + 6) - 1;
			if (i != 0 && flag)
			{
				throw new ArgumentException("version 1 certificate contains extra data");
			}
			while (i > 0)
			{
				DerTaggedObject derTaggedObject = (DerTaggedObject)seq[num + 6 + i];
				switch (derTaggedObject.TagNo)
				{
				case 1:
					this.issuerUniqueID = DerBitString.GetInstance(derTaggedObject, false);
					break;
				case 2:
					this.subjectUniqueID = DerBitString.GetInstance(derTaggedObject, false);
					break;
				case 3:
					if (flag2)
					{
						throw new ArgumentException("version 2 certificate cannot contain extensions");
					}
					this.extensions = X509Extensions.GetInstance(Asn1Sequence.GetInstance(derTaggedObject, true));
					break;
				default:
					throw new ArgumentException("Unknown tag encountered in structure: " + derTaggedObject.TagNo);
				}
				i--;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x0017B87A File Offset: 0x00179A7A
		public int Version
		{
			get
			{
				return this.version.Value.IntValue + 1;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x0017B88E File Offset: 0x00179A8E
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x0017B896 File Offset: 0x00179A96
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x0017B89E File Offset: 0x00179A9E
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x0017B8A6 File Offset: 0x00179AA6
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x0017B8AE File Offset: 0x00179AAE
		public Time StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x0017B8B6 File Offset: 0x00179AB6
		public Time EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x0017B8BE File Offset: 0x00179ABE
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x0017B8C6 File Offset: 0x00179AC6
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPublicKeyInfo;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x0017B8CE File Offset: 0x00179ACE
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x0017B8D6 File Offset: 0x00179AD6
		public DerBitString SubjectUniqueID
		{
			get
			{
				return this.subjectUniqueID;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0017B8DE File Offset: 0x00179ADE
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0017B8E6 File Offset: 0x00179AE6
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x040026DF RID: 9951
		internal Asn1Sequence seq;

		// Token: 0x040026E0 RID: 9952
		internal DerInteger version;

		// Token: 0x040026E1 RID: 9953
		internal DerInteger serialNumber;

		// Token: 0x040026E2 RID: 9954
		internal AlgorithmIdentifier signature;

		// Token: 0x040026E3 RID: 9955
		internal X509Name issuer;

		// Token: 0x040026E4 RID: 9956
		internal Time startDate;

		// Token: 0x040026E5 RID: 9957
		internal Time endDate;

		// Token: 0x040026E6 RID: 9958
		internal X509Name subject;

		// Token: 0x040026E7 RID: 9959
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x040026E8 RID: 9960
		internal DerBitString issuerUniqueID;

		// Token: 0x040026E9 RID: 9961
		internal DerBitString subjectUniqueID;

		// Token: 0x040026EA RID: 9962
		internal X509Extensions extensions;
	}
}
