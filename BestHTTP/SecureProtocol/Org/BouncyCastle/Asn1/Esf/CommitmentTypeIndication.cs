using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200072D RID: 1837
	public class CommitmentTypeIndication : Asn1Encodable
	{
		// Token: 0x060042BF RID: 17087 RVA: 0x0018AC70 File Offset: 0x00188E70
		public static CommitmentTypeIndication GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeIndication)
			{
				return (CommitmentTypeIndication)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeIndication((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeIndication' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0018ACC0 File Offset: 0x00188EC0
		public CommitmentTypeIndication(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.commitmentTypeQualifier = (Asn1Sequence)seq[1].ToAsn1Object();
			}
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x0018AD4A File Offset: 0x00188F4A
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId) : this(commitmentTypeId, null)
		{
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0018AD54 File Offset: 0x00188F54
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId, Asn1Sequence commitmentTypeQualifier)
		{
			if (commitmentTypeId == null)
			{
				throw new ArgumentNullException("commitmentTypeId");
			}
			this.commitmentTypeId = commitmentTypeId;
			if (commitmentTypeQualifier != null)
			{
				this.commitmentTypeQualifier = commitmentTypeQualifier;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x0018AD7B File Offset: 0x00188F7B
		public DerObjectIdentifier CommitmentTypeID
		{
			get
			{
				return this.commitmentTypeId;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x0018AD83 File Offset: 0x00188F83
		public Asn1Sequence CommitmentTypeQualifier
		{
			get
			{
				return this.commitmentTypeQualifier;
			}
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0018AD8C File Offset: 0x00188F8C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeId
			});
			if (this.commitmentTypeQualifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.commitmentTypeQualifier
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AE6 RID: 10982
		private readonly DerObjectIdentifier commitmentTypeId;

		// Token: 0x04002AE7 RID: 10983
		private readonly Asn1Sequence commitmentTypeQualifier;
	}
}
