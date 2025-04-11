using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200076D RID: 1901
	public class CompressedData : Asn1Encodable
	{
		// Token: 0x0600447D RID: 17533 RVA: 0x00190BB0 File Offset: 0x0018EDB0
		public CompressedData(AlgorithmIdentifier compressionAlgorithm, ContentInfo encapContentInfo)
		{
			this.version = new DerInteger(0);
			this.compressionAlgorithm = compressionAlgorithm;
			this.encapContentInfo = encapContentInfo;
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x00190BD2 File Offset: 0x0018EDD2
		public CompressedData(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encapContentInfo = ContentInfo.GetInstance(seq[2]);
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x00190C10 File Offset: 0x0018EE10
		public static CompressedData GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return CompressedData.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00190C1E File Offset: 0x0018EE1E
		public static CompressedData GetInstance(object obj)
		{
			if (obj == null || obj is CompressedData)
			{
				return (CompressedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompressedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid CompressedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x00190C5B File Offset: 0x0018EE5B
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x00190C63 File Offset: 0x0018EE63
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this.compressionAlgorithm;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x00190C6B File Offset: 0x0018EE6B
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.encapContentInfo;
			}
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x00190C73 File Offset: 0x0018EE73
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				this.version,
				this.compressionAlgorithm,
				this.encapContentInfo
			});
		}

		// Token: 0x04002BED RID: 11245
		private DerInteger version;

		// Token: 0x04002BEE RID: 11246
		private AlgorithmIdentifier compressionAlgorithm;

		// Token: 0x04002BEF RID: 11247
		private ContentInfo encapContentInfo;
	}
}
