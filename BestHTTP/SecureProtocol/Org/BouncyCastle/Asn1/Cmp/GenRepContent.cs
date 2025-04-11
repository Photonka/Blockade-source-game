using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A1 RID: 1953
	public class GenRepContent : Asn1Encodable
	{
		// Token: 0x060045F9 RID: 17913 RVA: 0x00194C19 File Offset: 0x00192E19
		private GenRepContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x00194C28 File Offset: 0x00192E28
		public static GenRepContent GetInstance(object obj)
		{
			if (obj is GenRepContent)
			{
				return (GenRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GenRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00194C68 File Offset: 0x00192E68
		public GenRepContent(params InfoTypeAndValue[] itv)
		{
			this.content = new DerSequence(itv);
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00194C8C File Offset: 0x00192E8C
		public virtual InfoTypeAndValue[] ToInfoTypeAndValueArray()
		{
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = InfoTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00194CCD File Offset: 0x00192ECD
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002C9A RID: 11418
		private readonly Asn1Sequence content;
	}
}
