using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B3 RID: 1971
	public class PopoDecKeyChallContent : Asn1Encodable
	{
		// Token: 0x06004688 RID: 18056 RVA: 0x001961F7 File Offset: 0x001943F7
		private PopoDecKeyChallContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00196206 File Offset: 0x00194406
		public static PopoDecKeyChallContent GetInstance(object obj)
		{
			if (obj is PopoDecKeyChallContent)
			{
				return (PopoDecKeyChallContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoDecKeyChallContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00196248 File Offset: 0x00194448
		public virtual Challenge[] ToChallengeArray()
		{
			Challenge[] array = new Challenge[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = Challenge.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00196289 File Offset: 0x00194489
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002D18 RID: 11544
		private readonly Asn1Sequence content;
	}
}
