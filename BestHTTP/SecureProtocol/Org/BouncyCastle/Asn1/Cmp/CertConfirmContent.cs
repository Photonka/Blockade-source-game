using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000795 RID: 1941
	public class CertConfirmContent : Asn1Encodable
	{
		// Token: 0x060045B2 RID: 17842 RVA: 0x00193E45 File Offset: 0x00192045
		private CertConfirmContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00193E54 File Offset: 0x00192054
		public static CertConfirmContent GetInstance(object obj)
		{
			if (obj is CertConfirmContent)
			{
				return (CertConfirmContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertConfirmContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00193E94 File Offset: 0x00192094
		public virtual CertStatus[] ToCertStatusArray()
		{
			CertStatus[] array = new CertStatus[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertStatus.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00193ED5 File Offset: 0x001920D5
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002C67 RID: 11367
		private readonly Asn1Sequence content;
	}
}
