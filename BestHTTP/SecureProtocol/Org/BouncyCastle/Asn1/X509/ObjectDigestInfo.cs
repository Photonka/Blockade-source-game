using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000691 RID: 1681
	public class ObjectDigestInfo : Asn1Encodable
	{
		// Token: 0x06003E7E RID: 15998 RVA: 0x0017A628 File Offset: 0x00178828
		public static ObjectDigestInfo GetInstance(object obj)
		{
			if (obj == null || obj is ObjectDigestInfo)
			{
				return (ObjectDigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ObjectDigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x0017A675 File Offset: 0x00178875
		public static ObjectDigestInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ObjectDigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0017A683 File Offset: 0x00178883
		public ObjectDigestInfo(int digestedObjectType, string otherObjectTypeID, AlgorithmIdentifier digestAlgorithm, byte[] objectDigest)
		{
			this.digestedObjectType = new DerEnumerated(digestedObjectType);
			if (digestedObjectType == 2)
			{
				this.otherObjectTypeID = new DerObjectIdentifier(otherObjectTypeID);
			}
			this.digestAlgorithm = digestAlgorithm;
			this.objectDigest = new DerBitString(objectDigest);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0017A6BC File Offset: 0x001788BC
		private ObjectDigestInfo(Asn1Sequence seq)
		{
			if (seq.Count > 4 || seq.Count < 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.digestedObjectType = DerEnumerated.GetInstance(seq[0]);
			int num = 0;
			if (seq.Count == 4)
			{
				this.otherObjectTypeID = DerObjectIdentifier.GetInstance(seq[1]);
				num++;
			}
			this.digestAlgorithm = AlgorithmIdentifier.GetInstance(seq[1 + num]);
			this.objectDigest = DerBitString.GetInstance(seq[2 + num]);
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x0017A757 File Offset: 0x00178957
		public DerEnumerated DigestedObjectType
		{
			get
			{
				return this.digestedObjectType;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003E83 RID: 16003 RVA: 0x0017A75F File Offset: 0x0017895F
		public DerObjectIdentifier OtherObjectTypeID
		{
			get
			{
				return this.otherObjectTypeID;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x0017A767 File Offset: 0x00178967
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x0017A76F File Offset: 0x0017896F
		public DerBitString ObjectDigest
		{
			get
			{
				return this.objectDigest;
			}
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0017A778 File Offset: 0x00178978
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digestedObjectType
			});
			if (this.otherObjectTypeID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.otherObjectTypeID
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digestAlgorithm,
				this.objectDigest
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040026B9 RID: 9913
		public const int PublicKey = 0;

		// Token: 0x040026BA RID: 9914
		public const int PublicKeyCert = 1;

		// Token: 0x040026BB RID: 9915
		public const int OtherObjectDigest = 2;

		// Token: 0x040026BC RID: 9916
		internal readonly DerEnumerated digestedObjectType;

		// Token: 0x040026BD RID: 9917
		internal readonly DerObjectIdentifier otherObjectTypeID;

		// Token: 0x040026BE RID: 9918
		internal readonly AlgorithmIdentifier digestAlgorithm;

		// Token: 0x040026BF RID: 9919
		internal readonly DerBitString objectDigest;
	}
}
