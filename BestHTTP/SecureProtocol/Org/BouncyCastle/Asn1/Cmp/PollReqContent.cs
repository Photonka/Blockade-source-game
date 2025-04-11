using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B2 RID: 1970
	public class PollReqContent : Asn1Encodable
	{
		// Token: 0x06004683 RID: 18051 RVA: 0x00196122 File Offset: 0x00194322
		private PollReqContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x00196131 File Offset: 0x00194331
		public static PollReqContent GetInstance(object obj)
		{
			if (obj is PollReqContent)
			{
				return (PollReqContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PollReqContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00196170 File Offset: 0x00194370
		public virtual DerInteger[][] GetCertReqIDs()
		{
			DerInteger[][] array = new DerInteger[this.content.Count][];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PollReqContent.SequenceToDerIntegerArray((Asn1Sequence)this.content[num]);
			}
			return array;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x001961B8 File Offset: 0x001943B8
		private static DerInteger[] SequenceToDerIntegerArray(Asn1Sequence seq)
		{
			DerInteger[] array = new DerInteger[seq.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DerInteger.GetInstance(seq[num]);
			}
			return array;
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x001961EF File Offset: 0x001943EF
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002D17 RID: 11543
		private readonly Asn1Sequence content;
	}
}
