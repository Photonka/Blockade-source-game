using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x0200026D RID: 621
	public class PemObject : PemObjectGenerator
	{
		// Token: 0x06001737 RID: 5943 RVA: 0x000B9AEB File Offset: 0x000B7CEB
		public PemObject(string type, byte[] content) : this(type, Platform.CreateArrayList(), content)
		{
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000B9AFA File Offset: 0x000B7CFA
		public PemObject(string type, IList headers, byte[] content)
		{
			this.type = type;
			this.headers = Platform.CreateArrayList(headers);
			this.content = content;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000B9B1C File Offset: 0x000B7D1C
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000B9B24 File Offset: 0x000B7D24
		public IList Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x000B9B2C File Offset: 0x000B7D2C
		public byte[] Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public PemObject Generate()
		{
			return this;
		}

		// Token: 0x040016D4 RID: 5844
		private string type;

		// Token: 0x040016D5 RID: 5845
		private IList headers;

		// Token: 0x040016D6 RID: 5846
		private byte[] content;
	}
}
