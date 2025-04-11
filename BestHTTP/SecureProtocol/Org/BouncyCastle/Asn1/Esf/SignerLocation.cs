using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000743 RID: 1859
	public class SignerLocation : Asn1Encodable
	{
		// Token: 0x06004351 RID: 17233 RVA: 0x0018CF34 File Offset: 0x0018B134
		public SignerLocation(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.countryName = DirectoryString.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.localityName = DirectoryString.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
				{
					bool explicitly = asn1TaggedObject.IsExplicit();
					this.postalAddress = Asn1Sequence.GetInstance(asn1TaggedObject, explicitly);
					if (this.postalAddress != null && this.postalAddress.Count > 6)
					{
						throw new ArgumentException("postal address must contain less than 6 strings");
					}
					break;
				}
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0018D00C File Offset: 0x0018B20C
		private SignerLocation(DirectoryString countryName, DirectoryString localityName, Asn1Sequence postalAddress)
		{
			if (postalAddress != null && postalAddress.Count > 6)
			{
				throw new ArgumentException("postal address must contain less than 6 strings");
			}
			this.countryName = countryName;
			this.localityName = localityName;
			this.postalAddress = postalAddress;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x0018D040 File Offset: 0x0018B240
		public SignerLocation(DirectoryString countryName, DirectoryString localityName, DirectoryString[] postalAddress) : this(countryName, localityName, new DerSequence(postalAddress))
		{
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x0018D05D File Offset: 0x0018B25D
		public SignerLocation(DerUtf8String countryName, DerUtf8String localityName, Asn1Sequence postalAddress) : this(DirectoryString.GetInstance(countryName), DirectoryString.GetInstance(localityName), postalAddress)
		{
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x0018D072 File Offset: 0x0018B272
		public static SignerLocation GetInstance(object obj)
		{
			if (obj == null || obj is SignerLocation)
			{
				return (SignerLocation)obj;
			}
			return new SignerLocation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x0018D091 File Offset: 0x0018B291
		public DirectoryString Country
		{
			get
			{
				return this.countryName;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x0018D099 File Offset: 0x0018B299
		public DirectoryString Locality
		{
			get
			{
				return this.localityName;
			}
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x0018D0A4 File Offset: 0x0018B2A4
		public DirectoryString[] GetPostal()
		{
			if (this.postalAddress == null)
			{
				return null;
			}
			DirectoryString[] array = new DirectoryString[this.postalAddress.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DirectoryString.GetInstance(this.postalAddress[num]);
			}
			return array;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x0018D0EF File Offset: 0x0018B2EF
		[Obsolete("Use 'Country' property instead")]
		public DerUtf8String CountryName
		{
			get
			{
				if (this.countryName != null)
				{
					return new DerUtf8String(this.countryName.GetString());
				}
				return null;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x0018D10B File Offset: 0x0018B30B
		[Obsolete("Use 'Locality' property instead")]
		public DerUtf8String LocalityName
		{
			get
			{
				if (this.localityName != null)
				{
					return new DerUtf8String(this.localityName.GetString());
				}
				return null;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x0018D127 File Offset: 0x0018B327
		public Asn1Sequence PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x0018D130 File Offset: 0x0018B330
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.countryName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.countryName)
				});
			}
			if (this.localityName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.localityName)
				});
			}
			if (this.postalAddress != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.postalAddress)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B1D RID: 11037
		private DirectoryString countryName;

		// Token: 0x04002B1E RID: 11038
		private DirectoryString localityName;

		// Token: 0x04002B1F RID: 11039
		private Asn1Sequence postalAddress;
	}
}
