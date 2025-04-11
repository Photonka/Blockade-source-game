using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000715 RID: 1813
	public class ProcurationSyntax : Asn1Encodable
	{
		// Token: 0x06004228 RID: 16936 RVA: 0x001884D4 File Offset: 0x001866D4
		public static ProcurationSyntax GetInstance(object obj)
		{
			if (obj == null || obj is ProcurationSyntax)
			{
				return (ProcurationSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProcurationSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00188524 File Offset: 0x00186724
		private ProcurationSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 1:
					this.country = DerPrintableString.GetInstance(instance, true).GetString();
					break;
				case 2:
					this.typeOfSubstitution = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
				{
					Asn1Object @object = instance.GetObject();
					if (@object is Asn1TaggedObject)
					{
						this.thirdPerson = GeneralName.GetInstance(@object);
					}
					else
					{
						this.certRef = IssuerSerial.GetInstance(@object);
					}
					break;
				}
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0018860D File Offset: 0x0018680D
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, IssuerSerial certRef)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = null;
			this.certRef = certRef;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x00188631 File Offset: 0x00186831
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, GeneralName thirdPerson)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = thirdPerson;
			this.certRef = null;
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x00188655 File Offset: 0x00186855
		public virtual string Country
		{
			get
			{
				return this.country;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x0018865D File Offset: 0x0018685D
		public virtual DirectoryString TypeOfSubstitution
		{
			get
			{
				return this.typeOfSubstitution;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x00188665 File Offset: 0x00186865
		public virtual GeneralName ThirdPerson
		{
			get
			{
				return this.thirdPerson;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0018866D File Offset: 0x0018686D
		public virtual IssuerSerial CertRef
		{
			get
			{
				return this.certRef;
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x00188678 File Offset: 0x00186878
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.country != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, new DerPrintableString(this.country, true))
				});
			}
			if (this.typeOfSubstitution != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.typeOfSubstitution)
				});
			}
			if (this.thirdPerson != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.thirdPerson)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.certRef)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002A35 RID: 10805
		private readonly string country;

		// Token: 0x04002A36 RID: 10806
		private readonly DirectoryString typeOfSubstitution;

		// Token: 0x04002A37 RID: 10807
		private readonly GeneralName thirdPerson;

		// Token: 0x04002A38 RID: 10808
		private readonly IssuerSerial certRef;
	}
}
