using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000733 RID: 1843
	public class CrlOcspRef : Asn1Encodable
	{
		// Token: 0x060042E7 RID: 17127 RVA: 0x0018B600 File Offset: 0x00189800
		public static CrlOcspRef GetInstance(object obj)
		{
			if (obj == null || obj is CrlOcspRef)
			{
				return (CrlOcspRef)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlOcspRef((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlOcspRef' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0018B650 File Offset: 0x00189850
		private CrlOcspRef(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlids = CrlListID.GetInstance(@object);
					break;
				case 1:
					this.ocspids = OcspListID.GetInstance(@object);
					break;
				case 2:
					this.otherRev = OtherRevRefs.GetInstance(@object);
					break;
				default:
					throw new ArgumentException("Illegal tag in CrlOcspRef", "seq");
				}
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0018B70C File Offset: 0x0018990C
		public CrlOcspRef(CrlListID crlids, OcspListID ocspids, OtherRevRefs otherRev)
		{
			this.crlids = crlids;
			this.ocspids = ocspids;
			this.otherRev = otherRev;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x0018B729 File Offset: 0x00189929
		public CrlListID CrlIDs
		{
			get
			{
				return this.crlids;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x0018B731 File Offset: 0x00189931
		public OcspListID OcspIDs
		{
			get
			{
				return this.ocspids;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x0018B739 File Offset: 0x00189939
		public OtherRevRefs OtherRev
		{
			get
			{
				return this.otherRev;
			}
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0018B744 File Offset: 0x00189944
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.crlids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlids.ToAsn1Object())
				});
			}
			if (this.ocspids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ocspids.ToAsn1Object())
				});
			}
			if (this.otherRev != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.otherRev.ToAsn1Object())
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AF0 RID: 10992
		private readonly CrlListID crlids;

		// Token: 0x04002AF1 RID: 10993
		private readonly OcspListID ocspids;

		// Token: 0x04002AF2 RID: 10994
		private readonly OtherRevRefs otherRev;
	}
}
