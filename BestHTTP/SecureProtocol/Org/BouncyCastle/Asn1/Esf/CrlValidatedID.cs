using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000734 RID: 1844
	public class CrlValidatedID : Asn1Encodable
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x0018B7E0 File Offset: 0x001899E0
		public static CrlValidatedID GetInstance(object obj)
		{
			if (obj == null || obj is CrlValidatedID)
			{
				return (CrlValidatedID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlValidatedID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlValidatedID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0018B830 File Offset: 0x00189A30
		private CrlValidatedID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.crlIdentifier = CrlIdentifier.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0018B8BA File Offset: 0x00189ABA
		public CrlValidatedID(OtherHash crlHash) : this(crlHash, null)
		{
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0018B8C4 File Offset: 0x00189AC4
		public CrlValidatedID(OtherHash crlHash, CrlIdentifier crlIdentifier)
		{
			if (crlHash == null)
			{
				throw new ArgumentNullException("crlHash");
			}
			this.crlHash = crlHash;
			this.crlIdentifier = crlIdentifier;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x0018B8E8 File Offset: 0x00189AE8
		public OtherHash CrlHash
		{
			get
			{
				return this.crlHash;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x0018B8F0 File Offset: 0x00189AF0
		public CrlIdentifier CrlIdentifier
		{
			get
			{
				return this.crlIdentifier;
			}
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0018B8F8 File Offset: 0x00189AF8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlHash.ToAsn1Object()
			});
			if (this.crlIdentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.crlIdentifier.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AF3 RID: 10995
		private readonly OtherHash crlHash;

		// Token: 0x04002AF4 RID: 10996
		private readonly CrlIdentifier crlIdentifier;
	}
}
