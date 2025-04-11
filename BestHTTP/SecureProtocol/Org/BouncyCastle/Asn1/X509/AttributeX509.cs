using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000672 RID: 1650
	public class AttributeX509 : Asn1Encodable
	{
		// Token: 0x06003D80 RID: 15744 RVA: 0x001770C8 File Offset: 0x001752C8
		public static AttributeX509 GetInstance(object obj)
		{
			if (obj == null || obj is AttributeX509)
			{
				return (AttributeX509)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeX509((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x00177118 File Offset: 0x00175318
		private AttributeX509(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x00177173 File Offset: 0x00175373
		public AttributeX509(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x00177189 File Offset: 0x00175389
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x00177191 File Offset: 0x00175391
		public Asn1Encodable[] GetAttributeValues()
		{
			return this.attrValues.ToArray();
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x0017719E File Offset: 0x0017539E
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x001771A6 File Offset: 0x001753A6
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x04002640 RID: 9792
		private readonly DerObjectIdentifier attrType;

		// Token: 0x04002641 RID: 9793
		private readonly Asn1Set attrValues;
	}
}
