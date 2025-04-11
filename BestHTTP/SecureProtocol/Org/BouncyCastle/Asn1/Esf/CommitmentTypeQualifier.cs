using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200072E RID: 1838
	public class CommitmentTypeQualifier : Asn1Encodable
	{
		// Token: 0x060042C6 RID: 17094 RVA: 0x0018ADD1 File Offset: 0x00188FD1
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier) : this(commitmentTypeIdentifier, null)
		{
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0018ADDB File Offset: 0x00188FDB
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier, Asn1Encodable qualifier)
		{
			if (commitmentTypeIdentifier == null)
			{
				throw new ArgumentNullException("commitmentTypeIdentifier");
			}
			this.commitmentTypeIdentifier = commitmentTypeIdentifier;
			if (qualifier != null)
			{
				this.qualifier = qualifier.ToAsn1Object();
			}
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0018AE08 File Offset: 0x00189008
		public CommitmentTypeQualifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.qualifier = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0018AE90 File Offset: 0x00189090
		public static CommitmentTypeQualifier GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeQualifier)
			{
				return (CommitmentTypeQualifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeQualifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeQualifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060042CA RID: 17098 RVA: 0x0018AEDD File Offset: 0x001890DD
		public DerObjectIdentifier CommitmentTypeIdentifier
		{
			get
			{
				return this.commitmentTypeIdentifier;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x0018AEE5 File Offset: 0x001890E5
		public Asn1Object Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0018AEF0 File Offset: 0x001890F0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeIdentifier
			});
			if (this.qualifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.qualifier
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AE8 RID: 10984
		private readonly DerObjectIdentifier commitmentTypeIdentifier;

		// Token: 0x04002AE9 RID: 10985
		private readonly Asn1Object qualifier;
	}
}
