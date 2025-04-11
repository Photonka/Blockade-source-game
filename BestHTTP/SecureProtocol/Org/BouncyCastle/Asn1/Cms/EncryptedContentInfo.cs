using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000771 RID: 1905
	public class EncryptedContentInfo : Asn1Encodable
	{
		// Token: 0x06004493 RID: 17555 RVA: 0x00190EA6 File Offset: 0x0018F0A6
		public EncryptedContentInfo(DerObjectIdentifier contentType, AlgorithmIdentifier contentEncryptionAlgorithm, Asn1OctetString encryptedContent)
		{
			this.contentType = contentType;
			this.contentEncryptionAlgorithm = contentEncryptionAlgorithm;
			this.encryptedContent = encryptedContent;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x00190EC4 File Offset: 0x0018F0C4
		public EncryptedContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			this.contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.encryptedContent = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[2], false);
			}
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x00190F1C File Offset: 0x0018F11C
		public static EncryptedContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is EncryptedContentInfo)
			{
				return (EncryptedContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedContentInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x00190F59 File Offset: 0x0018F159
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x00190F61 File Offset: 0x0018F161
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this.contentEncryptionAlgorithm;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x00190F69 File Offset: 0x0018F169
		public Asn1OctetString EncryptedContent
		{
			get
			{
				return this.encryptedContent;
			}
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00190F74 File Offset: 0x0018F174
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType,
				this.contentEncryptionAlgorithm
			});
			if (this.encryptedContent != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(false, 0, this.encryptedContent)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BF7 RID: 11255
		private DerObjectIdentifier contentType;

		// Token: 0x04002BF8 RID: 11256
		private AlgorithmIdentifier contentEncryptionAlgorithm;

		// Token: 0x04002BF9 RID: 11257
		private Asn1OctetString encryptedContent;
	}
}
