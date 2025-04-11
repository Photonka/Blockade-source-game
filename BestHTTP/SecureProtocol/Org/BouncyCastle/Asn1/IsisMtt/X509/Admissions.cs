using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000710 RID: 1808
	public class Admissions : Asn1Encodable
	{
		// Token: 0x06004201 RID: 16897 RVA: 0x00187B14 File Offset: 0x00185D14
		public static Admissions GetInstance(object obj)
		{
			if (obj == null || obj is Admissions)
			{
				return (Admissions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Admissions((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x00187B64 File Offset: 0x00185D64
		private Admissions(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				int tagNo = ((Asn1TaggedObject)asn1Encodable).TagNo;
				if (tagNo != 0)
				{
					if (tagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
					}
					this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
				}
				else
				{
					this.admissionAuthority = GeneralName.GetInstance((Asn1TaggedObject)asn1Encodable, true);
				}
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			if (asn1Encodable is Asn1TaggedObject)
			{
				int tagNo = ((Asn1TaggedObject)asn1Encodable).TagNo;
				if (tagNo != 1)
				{
					throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionInfos = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(enumerator.Current));
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x00187CBC File Offset: 0x00185EBC
		public Admissions(GeneralName admissionAuthority, NamingAuthority namingAuthority, ProfessionInfo[] professionInfos)
		{
			this.admissionAuthority = admissionAuthority;
			this.namingAuthority = namingAuthority;
			this.professionInfos = new DerSequence(professionInfos);
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x00187CEB File Offset: 0x00185EEB
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00187CF3 File Offset: 0x00185EF3
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x00187CFC File Offset: 0x00185EFC
		public ProfessionInfo[] GetProfessionInfos()
		{
			ProfessionInfo[] array = new ProfessionInfo[this.professionInfos.Count];
			int num = 0;
			foreach (object obj in this.professionInfos)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				array[num++] = ProfessionInfo.GetInstance(obj2);
			}
			return array;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x00187D74 File Offset: 0x00185F74
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.admissionAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.admissionAuthority)
				});
			}
			if (this.namingAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.namingAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.professionInfos
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002A28 RID: 10792
		private readonly GeneralName admissionAuthority;

		// Token: 0x04002A29 RID: 10793
		private readonly NamingAuthority namingAuthority;

		// Token: 0x04002A2A RID: 10794
		private readonly Asn1Sequence professionInfos;
	}
}
