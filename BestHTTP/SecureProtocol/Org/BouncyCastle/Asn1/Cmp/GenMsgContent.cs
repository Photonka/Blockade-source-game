using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A0 RID: 1952
	public class GenMsgContent : Asn1Encodable
	{
		// Token: 0x060045F4 RID: 17908 RVA: 0x00194B5E File Offset: 0x00192D5E
		private GenMsgContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x00194B6D File Offset: 0x00192D6D
		public static GenMsgContent GetInstance(object obj)
		{
			if (obj is GenMsgContent)
			{
				return (GenMsgContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GenMsgContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x00194BAC File Offset: 0x00192DAC
		public GenMsgContent(params InfoTypeAndValue[] itv)
		{
			this.content = new DerSequence(itv);
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00194BD0 File Offset: 0x00192DD0
		public virtual InfoTypeAndValue[] ToInfoTypeAndValueArray()
		{
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = InfoTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00194C11 File Offset: 0x00192E11
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002C99 RID: 11417
		private readonly Asn1Sequence content;
	}
}
