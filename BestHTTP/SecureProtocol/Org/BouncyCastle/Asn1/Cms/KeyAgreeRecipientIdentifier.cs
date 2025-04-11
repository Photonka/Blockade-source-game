using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077A RID: 1914
	public class KeyAgreeRecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060044D5 RID: 17621 RVA: 0x00191967 File Offset: 0x0018FB67
		public static KeyAgreeRecipientIdentifier GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return KeyAgreeRecipientIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00191978 File Offset: 0x0018FB78
		public static KeyAgreeRecipientIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientIdentifier)
			{
				return (KeyAgreeRecipientIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientIdentifier(IssuerAndSerialNumber.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject && ((Asn1TaggedObject)obj).TagNo == 0)
			{
				return new KeyAgreeRecipientIdentifier(RecipientKeyIdentifier.GetInstance((Asn1TaggedObject)obj, false));
			}
			throw new ArgumentException("Invalid KeyAgreeRecipientIdentifier: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x001919EC File Offset: 0x0018FBEC
		public KeyAgreeRecipientIdentifier(IssuerAndSerialNumber issuerSerial)
		{
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x001919FB File Offset: 0x0018FBFB
		public KeyAgreeRecipientIdentifier(RecipientKeyIdentifier rKeyID)
		{
			this.rKeyID = rKeyID;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x00191A0A File Offset: 0x0018FC0A
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x00191A12 File Offset: 0x0018FC12
		public RecipientKeyIdentifier RKeyID
		{
			get
			{
				return this.rKeyID;
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x00191A1A File Offset: 0x0018FC1A
		public override Asn1Object ToAsn1Object()
		{
			if (this.issuerSerial != null)
			{
				return this.issuerSerial.ToAsn1Object();
			}
			return new DerTaggedObject(false, 0, this.rKeyID);
		}

		// Token: 0x04002C13 RID: 11283
		private readonly IssuerAndSerialNumber issuerSerial;

		// Token: 0x04002C14 RID: 11284
		private readonly RecipientKeyIdentifier rKeyID;
	}
}
