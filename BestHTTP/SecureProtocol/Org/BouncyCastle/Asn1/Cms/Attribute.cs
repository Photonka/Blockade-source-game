using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000764 RID: 1892
	public class Attribute : Asn1Encodable
	{
		// Token: 0x06004431 RID: 17457 RVA: 0x0018FA74 File Offset: 0x0018DC74
		public static Attribute GetInstance(object obj)
		{
			if (obj == null || obj is Attribute)
			{
				return (Attribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Attribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x0018FAC1 File Offset: 0x0018DCC1
		public Attribute(Asn1Sequence seq)
		{
			this.attrType = (DerObjectIdentifier)seq[0];
			this.attrValues = (Asn1Set)seq[1];
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x0018FAED File Offset: 0x0018DCED
		public Attribute(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x0018FB03 File Offset: 0x0018DD03
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x0018FB0B File Offset: 0x0018DD0B
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x0018FB13 File Offset: 0x0018DD13
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x04002BBF RID: 11199
		private DerObjectIdentifier attrType;

		// Token: 0x04002BC0 RID: 11200
		private Asn1Set attrValues;
	}
}
