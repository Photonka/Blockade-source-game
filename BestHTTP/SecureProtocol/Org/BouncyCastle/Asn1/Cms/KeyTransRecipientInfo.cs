using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077C RID: 1916
	public class KeyTransRecipientInfo : Asn1Encodable
	{
		// Token: 0x060044E6 RID: 17638 RVA: 0x00191BFC File Offset: 0x0018FDFC
		public KeyTransRecipientInfo(RecipientIdentifier rid, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			if (rid.ToAsn1Object() is Asn1TaggedObject)
			{
				this.version = new DerInteger(2);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.rid = rid;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x00191C4C File Offset: 0x0018FE4C
		public KeyTransRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.rid = RecipientIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x00191CA7 File Offset: 0x0018FEA7
		public static KeyTransRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyTransRecipientInfo)
			{
				return (KeyTransRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyTransRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyTransRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x00191CE4 File Offset: 0x0018FEE4
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x00191CEC File Offset: 0x0018FEEC
		public RecipientIdentifier RecipientIdentifier
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x00191CF4 File Offset: 0x0018FEF4
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x00191CFC File Offset: 0x0018FEFC
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00191D04 File Offset: 0x0018FF04
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.rid,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x04002C1A RID: 11290
		private DerInteger version;

		// Token: 0x04002C1B RID: 11291
		private RecipientIdentifier rid;

		// Token: 0x04002C1C RID: 11292
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04002C1D RID: 11293
		private Asn1OctetString encryptedKey;
	}
}
