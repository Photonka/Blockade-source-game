using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068F RID: 1679
	public class NameConstraints : Asn1Encodable
	{
		// Token: 0x06003E6D RID: 15981 RVA: 0x0017A29C File Offset: 0x0017849C
		public static NameConstraints GetInstance(object obj)
		{
			if (obj == null || obj is NameConstraints)
			{
				return (NameConstraints)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NameConstraints((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x0017A2EC File Offset: 0x001784EC
		public NameConstraints(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				int tagNo = asn1TaggedObject.TagNo;
				if (tagNo != 0)
				{
					if (tagNo == 1)
					{
						this.excluded = Asn1Sequence.GetInstance(asn1TaggedObject, false);
					}
				}
				else
				{
					this.permitted = Asn1Sequence.GetInstance(asn1TaggedObject, false);
				}
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0017A36C File Offset: 0x0017856C
		public NameConstraints(ArrayList permitted, ArrayList excluded) : this(permitted, excluded)
		{
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0017A376 File Offset: 0x00178576
		public NameConstraints(IList permitted, IList excluded)
		{
			if (permitted != null)
			{
				this.permitted = this.CreateSequence(permitted);
			}
			if (excluded != null)
			{
				this.excluded = this.CreateSequence(excluded);
			}
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0017A3A0 File Offset: 0x001785A0
		private DerSequence CreateSequence(IList subtrees)
		{
			GeneralSubtree[] array = new GeneralSubtree[subtrees.Count];
			for (int i = 0; i < subtrees.Count; i++)
			{
				array[i] = (GeneralSubtree)subtrees[i];
			}
			Asn1Encodable[] v = array;
			return new DerSequence(v);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x0017A3E1 File Offset: 0x001785E1
		public Asn1Sequence PermittedSubtrees
		{
			get
			{
				return this.permitted;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003E73 RID: 15987 RVA: 0x0017A3E9 File Offset: 0x001785E9
		public Asn1Sequence ExcludedSubtrees
		{
			get
			{
				return this.excluded;
			}
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0017A3F4 File Offset: 0x001785F4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.permitted != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.permitted)
				});
			}
			if (this.excluded != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.excluded)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040026B5 RID: 9909
		private Asn1Sequence permitted;

		// Token: 0x040026B6 RID: 9910
		private Asn1Sequence excluded;
	}
}
