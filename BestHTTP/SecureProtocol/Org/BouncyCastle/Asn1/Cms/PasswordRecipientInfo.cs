using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000784 RID: 1924
	public class PasswordRecipientInfo : Asn1Encodable
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x0019246F File Offset: 0x0019066F
		public PasswordRecipientInfo(AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x00192491 File Offset: 0x00190691
		public PasswordRecipientInfo(AlgorithmIdentifier keyDerivationAlgorithm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyDerivationAlgorithm = keyDerivationAlgorithm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x001924BC File Offset: 0x001906BC
		public PasswordRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			if (seq[1] is Asn1TaggedObject)
			{
				this.keyDerivationAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)seq[1], false);
				this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
				this.encryptedKey = (Asn1OctetString)seq[3];
				return;
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encryptedKey = (Asn1OctetString)seq[2];
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x00192550 File Offset: 0x00190750
		public static PasswordRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return PasswordRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x0019255E File Offset: 0x0019075E
		public static PasswordRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is PasswordRecipientInfo)
			{
				return (PasswordRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PasswordRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid PasswordRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0019259B File Offset: 0x0019079B
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x001925A3 File Offset: 0x001907A3
		public AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.keyDerivationAlgorithm;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x001925AB File Offset: 0x001907AB
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x001925B3 File Offset: 0x001907B3
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x001925BC File Offset: 0x001907BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			if (this.keyDerivationAlgorithm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.keyDerivationAlgorithm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C2D RID: 11309
		private readonly DerInteger version;

		// Token: 0x04002C2E RID: 11310
		private readonly AlgorithmIdentifier keyDerivationAlgorithm;

		// Token: 0x04002C2F RID: 11311
		private readonly AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04002C30 RID: 11312
		private readonly Asn1OctetString encryptedKey;
	}
}
