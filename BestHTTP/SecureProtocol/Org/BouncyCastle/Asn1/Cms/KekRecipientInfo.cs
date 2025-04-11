using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000779 RID: 1913
	public class KekRecipientInfo : Asn1Encodable
	{
		// Token: 0x060044CC RID: 17612 RVA: 0x00191846 File Offset: 0x0018FA46
		public KekRecipientInfo(KekIdentifier kekID, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(4);
			this.kekID = kekID;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x00191870 File Offset: 0x0018FA70
		public KekRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.kekID = KekIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001918CB File Offset: 0x0018FACB
		public static KekRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001918D9 File Offset: 0x0018FAD9
		public static KekRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KekRecipientInfo)
			{
				return (KekRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x00191916 File Offset: 0x0018FB16
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x0019191E File Offset: 0x0018FB1E
		public KekIdentifier KekID
		{
			get
			{
				return this.kekID;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x00191926 File Offset: 0x0018FB26
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x0019192E File Offset: 0x0018FB2E
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x00191936 File Offset: 0x0018FB36
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.kekID,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x04002C0F RID: 11279
		private DerInteger version;

		// Token: 0x04002C10 RID: 11280
		private KekIdentifier kekID;

		// Token: 0x04002C11 RID: 11281
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04002C12 RID: 11282
		private Asn1OctetString encryptedKey;
	}
}
