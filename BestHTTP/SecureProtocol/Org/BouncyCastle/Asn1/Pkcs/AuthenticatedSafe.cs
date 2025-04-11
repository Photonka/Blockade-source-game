using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D5 RID: 1749
	public class AuthenticatedSafe : Asn1Encodable
	{
		// Token: 0x06004084 RID: 16516 RVA: 0x00182BEC File Offset: 0x00180DEC
		public AuthenticatedSafe(Asn1Sequence seq)
		{
			this.info = new ContentInfo[seq.Count];
			for (int num = 0; num != this.info.Length; num++)
			{
				this.info[num] = ContentInfo.GetInstance(seq[num]);
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x00182C37 File Offset: 0x00180E37
		public AuthenticatedSafe(ContentInfo[] info)
		{
			this.info = (ContentInfo[])info.Clone();
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x00182C50 File Offset: 0x00180E50
		public ContentInfo[] GetContentInfo()
		{
			return (ContentInfo[])this.info.Clone();
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x00182C64 File Offset: 0x00180E64
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.info;
			return new BerSequence(v);
		}

		// Token: 0x04002875 RID: 10357
		private readonly ContentInfo[] info;
	}
}
