using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200074E RID: 1870
	public class AttributeTypeAndValue : Asn1Encodable
	{
		// Token: 0x06004394 RID: 17300 RVA: 0x0018E33A File Offset: 0x0018C53A
		private AttributeTypeAndValue(Asn1Sequence seq)
		{
			this.type = (DerObjectIdentifier)seq[0];
			this.value = seq[1];
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x0018E361 File Offset: 0x0018C561
		public static AttributeTypeAndValue GetInstance(object obj)
		{
			if (obj is AttributeTypeAndValue)
			{
				return (AttributeTypeAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeTypeAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x0018E3A0 File Offset: 0x0018C5A0
		public AttributeTypeAndValue(string oid, Asn1Encodable value) : this(new DerObjectIdentifier(oid), value)
		{
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0018E3AF File Offset: 0x0018C5AF
		public AttributeTypeAndValue(DerObjectIdentifier type, Asn1Encodable value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x0018E3C5 File Offset: 0x0018C5C5
		public virtual DerObjectIdentifier Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x0018E3CD File Offset: 0x0018C5CD
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x0018E3D5 File Offset: 0x0018C5D5
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.type,
				this.value
			});
		}

		// Token: 0x04002B6B RID: 11115
		private readonly DerObjectIdentifier type;

		// Token: 0x04002B6C RID: 11116
		private readonly Asn1Encodable value;
	}
}
