using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077B RID: 1915
	public class KeyAgreeRecipientInfo : Asn1Encodable
	{
		// Token: 0x060044DC RID: 17628 RVA: 0x00191A3D File Offset: 0x0018FC3D
		public KeyAgreeRecipientInfo(OriginatorIdentifierOrKey originator, Asn1OctetString ukm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1Sequence recipientEncryptedKeys)
		{
			this.version = new DerInteger(3);
			this.originator = originator;
			this.ukm = ukm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.recipientEncryptedKeys = recipientEncryptedKeys;
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x00191A70 File Offset: 0x0018FC70
		public KeyAgreeRecipientInfo(Asn1Sequence seq)
		{
			int index = 0;
			this.version = (DerInteger)seq[index++];
			this.originator = OriginatorIdentifierOrKey.GetInstance((Asn1TaggedObject)seq[index++], true);
			if (seq[index] is Asn1TaggedObject)
			{
				this.ukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[index++], true);
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[index++]);
			this.recipientEncryptedKeys = (Asn1Sequence)seq[index++];
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x00191B0D File Offset: 0x0018FD0D
		public static KeyAgreeRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KeyAgreeRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x00191B1B File Offset: 0x0018FD1B
		public static KeyAgreeRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientInfo)
			{
				return (KeyAgreeRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyAgreeRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x00191B58 File Offset: 0x0018FD58
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x00191B60 File Offset: 0x0018FD60
		public OriginatorIdentifierOrKey Originator
		{
			get
			{
				return this.originator;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x00191B68 File Offset: 0x0018FD68
		public Asn1OctetString UserKeyingMaterial
		{
			get
			{
				return this.ukm;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x00191B70 File Offset: 0x0018FD70
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00191B78 File Offset: 0x0018FD78
		public Asn1Sequence RecipientEncryptedKeys
		{
			get
			{
				return this.recipientEncryptedKeys;
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x00191B80 File Offset: 0x0018FD80
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				new DerTaggedObject(true, 0, this.originator)
			});
			if (this.ukm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ukm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.recipientEncryptedKeys
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C15 RID: 11285
		private DerInteger version;

		// Token: 0x04002C16 RID: 11286
		private OriginatorIdentifierOrKey originator;

		// Token: 0x04002C17 RID: 11287
		private Asn1OctetString ukm;

		// Token: 0x04002C18 RID: 11288
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04002C19 RID: 11289
		private Asn1Sequence recipientEncryptedKeys;
	}
}
