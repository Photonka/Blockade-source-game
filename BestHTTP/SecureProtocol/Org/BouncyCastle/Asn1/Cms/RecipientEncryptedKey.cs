using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000785 RID: 1925
	public class RecipientEncryptedKey : Asn1Encodable
	{
		// Token: 0x06004530 RID: 17712 RVA: 0x00192626 File Offset: 0x00190826
		private RecipientEncryptedKey(Asn1Sequence seq)
		{
			this.identifier = KeyAgreeRecipientIdentifier.GetInstance(seq[0]);
			this.encryptedKey = (Asn1OctetString)seq[1];
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x00192652 File Offset: 0x00190852
		public static RecipientEncryptedKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return RecipientEncryptedKey.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00192660 File Offset: 0x00190860
		public static RecipientEncryptedKey GetInstance(object obj)
		{
			if (obj == null || obj is RecipientEncryptedKey)
			{
				return (RecipientEncryptedKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientEncryptedKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientEncryptedKey: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001926AD File Offset: 0x001908AD
		public RecipientEncryptedKey(KeyAgreeRecipientIdentifier id, Asn1OctetString encryptedKey)
		{
			this.identifier = id;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x001926C3 File Offset: 0x001908C3
		public KeyAgreeRecipientIdentifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x001926CB File Offset: 0x001908CB
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001926D3 File Offset: 0x001908D3
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.identifier,
				this.encryptedKey
			});
		}

		// Token: 0x04002C31 RID: 11313
		private readonly KeyAgreeRecipientIdentifier identifier;

		// Token: 0x04002C32 RID: 11314
		private readonly Asn1OctetString encryptedKey;
	}
}
