using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000765 RID: 1893
	public class Attributes : Asn1Encodable
	{
		// Token: 0x06004437 RID: 17463 RVA: 0x0018FB32 File Offset: 0x0018DD32
		private Attributes(Asn1Set attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x0018FB41 File Offset: 0x0018DD41
		public Attributes(Asn1EncodableVector v)
		{
			this.attributes = new BerSet(v);
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x0018FB55 File Offset: 0x0018DD55
		public static Attributes GetInstance(object obj)
		{
			if (obj is Attributes)
			{
				return (Attributes)obj;
			}
			if (obj != null)
			{
				return new Attributes(Asn1Set.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x0018FB78 File Offset: 0x0018DD78
		public virtual Attribute[] GetAttributes()
		{
			Attribute[] array = new Attribute[this.attributes.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = Attribute.GetInstance(this.attributes[num]);
			}
			return array;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x0018FBB9 File Offset: 0x0018DDB9
		public override Asn1Object ToAsn1Object()
		{
			return this.attributes;
		}

		// Token: 0x04002BC1 RID: 11201
		private readonly Asn1Set attributes;
	}
}
