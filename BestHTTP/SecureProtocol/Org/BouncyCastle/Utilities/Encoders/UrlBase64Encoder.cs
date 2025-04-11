using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x0200027C RID: 636
	public class UrlBase64Encoder : Base64Encoder
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x000BAEB6 File Offset: 0x000B90B6
		public UrlBase64Encoder()
		{
			this.encodingTable[this.encodingTable.Length - 2] = 45;
			this.encodingTable[this.encodingTable.Length - 1] = 95;
			this.padding = 46;
			base.InitialiseDecodingTable();
		}
	}
}
