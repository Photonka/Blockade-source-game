using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D5 RID: 1493
	public class CmsCompressedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600393C RID: 14652 RVA: 0x0016912C File Offset: 0x0016732C
		public CmsCompressedDataParser(byte[] compressedData) : this(new MemoryStream(compressedData, false))
		{
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x0016913B File Offset: 0x0016733B
		public CmsCompressedDataParser(Stream compressedData) : base(compressedData)
		{
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x00169144 File Offset: 0x00167344
		public CmsTypedStream GetContent()
		{
			CmsTypedStream result;
			try
			{
				ContentInfoParser encapContentInfo = new CompressedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16)).GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				result = new CmsTypedStream(encapContentInfo.ContentType.ToString(), new ZInputStream(asn1OctetStringParser.GetOctetStream()));
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading compressed content.", e);
			}
			return result;
		}
	}
}
