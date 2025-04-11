using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D3 RID: 1491
	public class CmsCompressedData
	{
		// Token: 0x06003933 RID: 14643 RVA: 0x00168F87 File Offset: 0x00167187
		public CmsCompressedData(byte[] compressedData) : this(CmsUtilities.ReadContentInfo(compressedData))
		{
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00168F95 File Offset: 0x00167195
		public CmsCompressedData(Stream compressedDataStream) : this(CmsUtilities.ReadContentInfo(compressedDataStream))
		{
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x00168FA3 File Offset: 0x001671A3
		public CmsCompressedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x00168FB4 File Offset: 0x001671B4
		public byte[] GetContent()
		{
			ZInputStream zinputStream = new ZInputStream(((Asn1OctetString)CompressedData.GetInstance(this.contentInfo.Content).EncapContentInfo.Content).GetOctetStream());
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(zinputStream);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			finally
			{
				Platform.Dispose(zinputStream);
			}
			return result;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x00169028 File Offset: 0x00167228
		public byte[] GetContent(int limit)
		{
			ZInputStream inStream = new ZInputStream(new MemoryStream(((Asn1OctetString)CompressedData.GetInstance(this.contentInfo.Content).EncapContentInfo.Content).GetOctets(), false));
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(inStream, limit);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			return result;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x00169090 File Offset: 0x00167290
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x00169098 File Offset: 0x00167298
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04002499 RID: 9369
		internal ContentInfo contentInfo;
	}
}
