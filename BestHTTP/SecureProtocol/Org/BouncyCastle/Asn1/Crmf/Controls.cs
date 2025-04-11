using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000755 RID: 1877
	public class Controls : Asn1Encodable
	{
		// Token: 0x060043D0 RID: 17360 RVA: 0x0018EB08 File Offset: 0x0018CD08
		private Controls(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x0018EB17 File Offset: 0x0018CD17
		public static Controls GetInstance(object obj)
		{
			if (obj is Controls)
			{
				return (Controls)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Controls((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x0018EB58 File Offset: 0x0018CD58
		public Controls(params AttributeTypeAndValue[] atvs)
		{
			this.content = new DerSequence(atvs);
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0018EB7C File Offset: 0x0018CD7C
		public virtual AttributeTypeAndValue[] ToAttributeTypeAndValueArray()
		{
			AttributeTypeAndValue[] array = new AttributeTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = AttributeTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x0018EBBD File Offset: 0x0018CDBD
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002B8B RID: 11147
		private readonly Asn1Sequence content;
	}
}
