using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200065D RID: 1629
	public class OidTokenizer
	{
		// Token: 0x06003CF0 RID: 15600 RVA: 0x001752DE File Offset: 0x001734DE
		public OidTokenizer(string oid)
		{
			this.oid = oid;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x001752ED File Offset: 0x001734ED
		public bool HasMoreTokens
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x001752FC File Offset: 0x001734FC
		public string NextToken()
		{
			if (this.index == -1)
			{
				return null;
			}
			int num = this.oid.IndexOf('.', this.index);
			if (num == -1)
			{
				string result = this.oid.Substring(this.index);
				this.index = -1;
				return result;
			}
			string result2 = this.oid.Substring(this.index, num - this.index);
			this.index = num + 1;
			return result2;
		}

		// Token: 0x040025D1 RID: 9681
		private string oid;

		// Token: 0x040025D2 RID: 9682
		private int index;
	}
}
