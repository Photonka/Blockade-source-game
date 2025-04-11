using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020006B5 RID: 1717
	public class PersonalData : Asn1Encodable
	{
		// Token: 0x06003FB5 RID: 16309 RVA: 0x0017EFF8 File Offset: 0x0017D1F8
		public static PersonalData GetInstance(object obj)
		{
			if (obj == null || obj is PersonalData)
			{
				return (PersonalData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PersonalData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0017F048 File Offset: 0x0017D248
		private PersonalData(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.nameOrPseudonym = NameOrPseudonym.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 0:
					this.nameDistinguisher = DerInteger.GetInstance(instance, false).Value;
					break;
				case 1:
					this.dateOfBirth = DerGeneralizedTime.GetInstance(instance, false);
					break;
				case 2:
					this.placeOfBirth = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
					this.gender = DerPrintableString.GetInstance(instance, false).GetString();
					break;
				case 4:
					this.postalAddress = DirectoryString.GetInstance(instance, true);
					break;
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0017F14D File Offset: 0x0017D34D
		public PersonalData(NameOrPseudonym nameOrPseudonym, BigInteger nameDistinguisher, DerGeneralizedTime dateOfBirth, DirectoryString placeOfBirth, string gender, DirectoryString postalAddress)
		{
			this.nameOrPseudonym = nameOrPseudonym;
			this.dateOfBirth = dateOfBirth;
			this.gender = gender;
			this.nameDistinguisher = nameDistinguisher;
			this.postalAddress = postalAddress;
			this.placeOfBirth = placeOfBirth;
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x0017F182 File Offset: 0x0017D382
		public NameOrPseudonym NameOrPseudonym
		{
			get
			{
				return this.nameOrPseudonym;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x0017F18A File Offset: 0x0017D38A
		public BigInteger NameDistinguisher
		{
			get
			{
				return this.nameDistinguisher;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x0017F192 File Offset: 0x0017D392
		public DerGeneralizedTime DateOfBirth
		{
			get
			{
				return this.dateOfBirth;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x0017F19A File Offset: 0x0017D39A
		public DirectoryString PlaceOfBirth
		{
			get
			{
				return this.placeOfBirth;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0017F1A2 File Offset: 0x0017D3A2
		public string Gender
		{
			get
			{
				return this.gender;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x0017F1AA File Offset: 0x0017D3AA
		public DirectoryString PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x0017F1B4 File Offset: 0x0017D3B4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.nameOrPseudonym
			});
			if (this.nameDistinguisher != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, new DerInteger(this.nameDistinguisher))
				});
			}
			if (this.dateOfBirth != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.dateOfBirth)
				});
			}
			if (this.placeOfBirth != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.placeOfBirth)
				});
			}
			if (this.gender != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 3, new DerPrintableString(this.gender, true))
				});
			}
			if (this.postalAddress != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 4, this.postalAddress)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002795 RID: 10133
		private readonly NameOrPseudonym nameOrPseudonym;

		// Token: 0x04002796 RID: 10134
		private readonly BigInteger nameDistinguisher;

		// Token: 0x04002797 RID: 10135
		private readonly DerGeneralizedTime dateOfBirth;

		// Token: 0x04002798 RID: 10136
		private readonly DirectoryString placeOfBirth;

		// Token: 0x04002799 RID: 10137
		private readonly string gender;

		// Token: 0x0400279A RID: 10138
		private readonly DirectoryString postalAddress;
	}
}
