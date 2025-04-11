using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200076E RID: 1902
	public class CompressedDataParser
	{
		// Token: 0x06004485 RID: 17541 RVA: 0x00190C9C File Offset: 0x0018EE9C
		public CompressedDataParser(Asn1SequenceParser seq)
		{
			this._version = (DerInteger)seq.ReadObject();
			this._compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encapContentInfo = new ContentInfoParser((Asn1SequenceParser)seq.ReadObject());
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x00190CEC File Offset: 0x0018EEEC
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x00190CF4 File Offset: 0x0018EEF4
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this._compressionAlgorithm;
			}
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x00190CFC File Offset: 0x0018EEFC
		public ContentInfoParser GetEncapContentInfo()
		{
			return this._encapContentInfo;
		}

		// Token: 0x04002BF0 RID: 11248
		private DerInteger _version;

		// Token: 0x04002BF1 RID: 11249
		private AlgorithmIdentifier _compressionAlgorithm;

		// Token: 0x04002BF2 RID: 11250
		private ContentInfoParser _encapContentInfo;
	}
}
