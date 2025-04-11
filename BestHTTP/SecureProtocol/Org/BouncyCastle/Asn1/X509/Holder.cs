using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000689 RID: 1673
	public class Holder : Asn1Encodable
	{
		// Token: 0x06003E40 RID: 15936 RVA: 0x001796B0 File Offset: 0x001778B0
		public static Holder GetInstance(object obj)
		{
			if (obj is Holder)
			{
				return (Holder)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Holder((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new Holder((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00179710 File Offset: 0x00177910
		public Holder(Asn1TaggedObject tagObj)
		{
			int tagNo = tagObj.TagNo;
			if (tagNo != 0)
			{
				if (tagNo != 1)
				{
					throw new ArgumentException("unknown tag in Holder");
				}
				this.entityName = GeneralNames.GetInstance(tagObj, false);
			}
			else
			{
				this.baseCertificateID = IssuerSerial.GetInstance(tagObj, false);
			}
			this.version = 0;
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x00179764 File Offset: 0x00177964
		private Holder(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
					break;
				case 1:
					this.entityName = GeneralNames.GetInstance(instance, false);
					break;
				case 2:
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
					break;
				default:
					throw new ArgumentException("unknown tag in Holder");
				}
			}
			this.version = 1;
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00179813 File Offset: 0x00177A13
		public Holder(IssuerSerial baseCertificateID) : this(baseCertificateID, 1)
		{
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0017981D File Offset: 0x00177A1D
		public Holder(IssuerSerial baseCertificateID, int version)
		{
			this.baseCertificateID = baseCertificateID;
			this.version = version;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x00179833 File Offset: 0x00177A33
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0017983B File Offset: 0x00177A3B
		public Holder(GeneralNames entityName) : this(entityName, 1)
		{
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00179845 File Offset: 0x00177A45
		public Holder(GeneralNames entityName, int version)
		{
			this.entityName = entityName;
			this.version = version;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x0017985B File Offset: 0x00177A5B
		public Holder(ObjectDigestInfo objectDigestInfo)
		{
			this.objectDigestInfo = objectDigestInfo;
			this.version = 1;
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06003E49 RID: 15945 RVA: 0x00179871 File Offset: 0x00177A71
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x00179879 File Offset: 0x00177A79
		public GeneralNames EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x00179881 File Offset: 0x00177A81
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x0017988C File Offset: 0x00177A8C
		public override Asn1Object ToAsn1Object()
		{
			if (this.version == 1)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				if (this.baseCertificateID != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 0, this.baseCertificateID)
					});
				}
				if (this.entityName != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 1, this.entityName)
					});
				}
				if (this.objectDigestInfo != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 2, this.objectDigestInfo)
					});
				}
				return new DerSequence(asn1EncodableVector);
			}
			if (this.entityName != null)
			{
				return new DerTaggedObject(false, 1, this.entityName);
			}
			return new DerTaggedObject(false, 0, this.baseCertificateID);
		}

		// Token: 0x0400268B RID: 9867
		internal readonly IssuerSerial baseCertificateID;

		// Token: 0x0400268C RID: 9868
		internal readonly GeneralNames entityName;

		// Token: 0x0400268D RID: 9869
		internal readonly ObjectDigestInfo objectDigestInfo;

		// Token: 0x0400268E RID: 9870
		private readonly int version;
	}
}
