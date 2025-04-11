using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B4 RID: 1972
	public class PopoDecKeyRespContent : Asn1Encodable
	{
		// Token: 0x0600468C RID: 18060 RVA: 0x00196291 File Offset: 0x00194491
		private PopoDecKeyRespContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001962A0 File Offset: 0x001944A0
		public static PopoDecKeyRespContent GetInstance(object obj)
		{
			if (obj is PopoDecKeyRespContent)
			{
				return (PopoDecKeyRespContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoDecKeyRespContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001962E0 File Offset: 0x001944E0
		public virtual DerInteger[] ToDerIntegerArray()
		{
			DerInteger[] array = new DerInteger[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DerInteger.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00196321 File Offset: 0x00194521
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002D19 RID: 11545
		private readonly Asn1Sequence content;
	}
}
