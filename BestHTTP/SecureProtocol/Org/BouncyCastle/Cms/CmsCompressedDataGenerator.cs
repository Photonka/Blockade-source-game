using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D4 RID: 1492
	public class CmsCompressedDataGenerator
	{
		// Token: 0x0600393B RID: 14651 RVA: 0x001690A8 File Offset: 0x001672A8
		public CmsCompressedData Generate(CmsProcessable content, string compressionOid)
		{
			AlgorithmIdentifier compressionAlgorithm;
			Asn1OctetString content2;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				ZOutputStream zoutputStream = new ZOutputStream(memoryStream, -1);
				content.Write(zoutputStream);
				Platform.Dispose(zoutputStream);
				compressionAlgorithm = new AlgorithmIdentifier(new DerObjectIdentifier(compressionOid));
				content2 = new BerOctetString(memoryStream.ToArray());
			}
			catch (IOException e)
			{
				throw new CmsException("exception encoding data.", e);
			}
			ContentInfo encapContentInfo = new ContentInfo(CmsObjectIdentifiers.Data, content2);
			return new CmsCompressedData(new ContentInfo(CmsObjectIdentifiers.CompressedData, new CompressedData(compressionAlgorithm, encapContentInfo)));
		}

		// Token: 0x0400249A RID: 9370
		public const string ZLib = "1.2.840.113549.1.9.16.3.8";
	}
}
