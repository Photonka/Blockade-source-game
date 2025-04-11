using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AD RID: 1965
	public class PkiMessages : Asn1Encodable
	{
		// Token: 0x06004666 RID: 18022 RVA: 0x00195CB9 File Offset: 0x00193EB9
		private PkiMessages(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x00195CC8 File Offset: 0x00193EC8
		public static PkiMessages GetInstance(object obj)
		{
			if (obj is PkiMessages)
			{
				return (PkiMessages)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiMessages((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x00195D08 File Offset: 0x00193F08
		public PkiMessages(params PkiMessage[] msgs)
		{
			this.content = new DerSequence(msgs);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x00195D2C File Offset: 0x00193F2C
		public virtual PkiMessage[] ToPkiMessageArray()
		{
			PkiMessage[] array = new PkiMessage[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PkiMessage.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x00195D6D File Offset: 0x00193F6D
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002D00 RID: 11520
		private Asn1Sequence content;
	}
}
