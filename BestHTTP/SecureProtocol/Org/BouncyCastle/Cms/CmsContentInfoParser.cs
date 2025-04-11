using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D7 RID: 1495
	public class CmsContentInfoParser
	{
		// Token: 0x06003943 RID: 14659 RVA: 0x00169260 File Offset: 0x00167460
		protected CmsContentInfoParser(Stream data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
			try
			{
				Asn1StreamParser asn1StreamParser = new Asn1StreamParser(data);
				this.contentInfo = new ContentInfoParser((Asn1SequenceParser)asn1StreamParser.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Unexpected object reading content.", e2);
			}
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x001692E0 File Offset: 0x001674E0
		public void Close()
		{
			Platform.Dispose(this.data);
		}

		// Token: 0x0400249D RID: 9373
		protected ContentInfoParser contentInfo;

		// Token: 0x0400249E RID: 9374
		protected Stream data;
	}
}
