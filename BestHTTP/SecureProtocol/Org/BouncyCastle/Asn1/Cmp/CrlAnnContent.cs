using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079E RID: 1950
	public class CrlAnnContent : Asn1Encodable
	{
		// Token: 0x060045E8 RID: 17896 RVA: 0x0019498B File Offset: 0x00192B8B
		private CrlAnnContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0019499A File Offset: 0x00192B9A
		public static CrlAnnContent GetInstance(object obj)
		{
			if (obj is CrlAnnContent)
			{
				return (CrlAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x001949DC File Offset: 0x00192BDC
		public virtual CertificateList[] ToCertificateListArray()
		{
			CertificateList[] array = new CertificateList[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertificateList.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00194A1D File Offset: 0x00192C1D
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002C95 RID: 11413
		private readonly Asn1Sequence content;
	}
}
