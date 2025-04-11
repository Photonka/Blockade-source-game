using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075E RID: 1886
	public class PopoPrivKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600440C RID: 17420 RVA: 0x0018F448 File Offset: 0x0018D648
		private PopoPrivKey(Asn1TaggedObject obj)
		{
			this.tagNo = obj.TagNo;
			switch (this.tagNo)
			{
			case 0:
				this.obj = DerBitString.GetInstance(obj, false);
				return;
			case 1:
				this.obj = SubsequentMessage.ValueOf(DerInteger.GetInstance(obj, false).Value.IntValue);
				return;
			case 2:
				this.obj = DerBitString.GetInstance(obj, false);
				return;
			case 3:
				this.obj = PKMacValue.GetInstance(obj, false);
				return;
			case 4:
				this.obj = EnvelopedData.GetInstance(obj, false);
				return;
			default:
				throw new ArgumentException("unknown tag in PopoPrivKey", "obj");
			}
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x0018F4EE File Offset: 0x0018D6EE
		public static PopoPrivKey GetInstance(Asn1TaggedObject tagged, bool isExplicit)
		{
			return new PopoPrivKey(Asn1TaggedObject.GetInstance(tagged.GetObject()));
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x0018F500 File Offset: 0x0018D700
		public PopoPrivKey(SubsequentMessage msg)
		{
			this.tagNo = 1;
			this.obj = msg;
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x0018F516 File Offset: 0x0018D716
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x0018F51E File Offset: 0x0018D71E
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0018F526 File Offset: 0x0018D726
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.obj);
		}

		// Token: 0x04002BA8 RID: 11176
		public const int thisMessage = 0;

		// Token: 0x04002BA9 RID: 11177
		public const int subsequentMessage = 1;

		// Token: 0x04002BAA RID: 11178
		public const int dhMAC = 2;

		// Token: 0x04002BAB RID: 11179
		public const int agreeMAC = 3;

		// Token: 0x04002BAC RID: 11180
		public const int encryptedKey = 4;

		// Token: 0x04002BAD RID: 11181
		private readonly int tagNo;

		// Token: 0x04002BAE RID: 11182
		private readonly Asn1Encodable obj;
	}
}
