using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D4 RID: 1748
	public class AttributePkcs : Asn1Encodable
	{
		// Token: 0x0600407E RID: 16510 RVA: 0x00182B0C File Offset: 0x00180D0C
		public static AttributePkcs GetInstance(object obj)
		{
			AttributePkcs attributePkcs = obj as AttributePkcs;
			if (obj == null || attributePkcs != null)
			{
				return attributePkcs;
			}
			Asn1Sequence asn1Sequence = obj as Asn1Sequence;
			if (asn1Sequence != null)
			{
				return new AttributePkcs(asn1Sequence);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x00182B54 File Offset: 0x00180D54
		private AttributePkcs(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x00182BA4 File Offset: 0x00180DA4
		public AttributePkcs(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x00182BBA File Offset: 0x00180DBA
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x00182BC2 File Offset: 0x00180DC2
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x00182BCA File Offset: 0x00180DCA
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x04002873 RID: 10355
		private readonly DerObjectIdentifier attrType;

		// Token: 0x04002874 RID: 10356
		private readonly Asn1Set attrValues;
	}
}
