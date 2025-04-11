using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000750 RID: 1872
	public class CertReqMessages : Asn1Encodable
	{
		// Token: 0x060043A1 RID: 17313 RVA: 0x0018E49C File Offset: 0x0018C69C
		private CertReqMessages(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x0018E4AB File Offset: 0x0018C6AB
		public static CertReqMessages GetInstance(object obj)
		{
			if (obj is CertReqMessages)
			{
				return (CertReqMessages)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertReqMessages((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x0018E4EC File Offset: 0x0018C6EC
		public CertReqMessages(params CertReqMsg[] msgs)
		{
			this.content = new DerSequence(msgs);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0018E510 File Offset: 0x0018C710
		public virtual CertReqMsg[] ToCertReqMsgArray()
		{
			CertReqMsg[] array = new CertReqMsg[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertReqMsg.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x0018E551 File Offset: 0x0018C751
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002B6F RID: 11119
		private readonly Asn1Sequence content;
	}
}
