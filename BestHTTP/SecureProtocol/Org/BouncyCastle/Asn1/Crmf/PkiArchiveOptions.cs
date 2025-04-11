using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075B RID: 1883
	public class PkiArchiveOptions : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060043F7 RID: 17399 RVA: 0x0018F13E File Offset: 0x0018D33E
		public static PkiArchiveOptions GetInstance(object obj)
		{
			if (obj is PkiArchiveOptions)
			{
				return (PkiArchiveOptions)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new PkiArchiveOptions((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x0018F180 File Offset: 0x0018D380
		private PkiArchiveOptions(Asn1TaggedObject tagged)
		{
			switch (tagged.TagNo)
			{
			case 0:
				this.value = EncryptedKey.GetInstance(tagged.GetObject());
				return;
			case 1:
				this.value = Asn1OctetString.GetInstance(tagged, false);
				return;
			case 2:
				this.value = DerBoolean.GetInstance(tagged, false);
				return;
			default:
				throw new ArgumentException("unknown tag number: " + tagged.TagNo, "tagged");
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x0018F1FB File Offset: 0x0018D3FB
		public PkiArchiveOptions(EncryptedKey encKey)
		{
			this.value = encKey;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0018F1FB File Offset: 0x0018D3FB
		public PkiArchiveOptions(Asn1OctetString keyGenParameters)
		{
			this.value = keyGenParameters;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x0018F20A File Offset: 0x0018D40A
		public PkiArchiveOptions(bool archiveRemGenPrivKey)
		{
			this.value = DerBoolean.GetInstance(archiveRemGenPrivKey);
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x0018F21E File Offset: 0x0018D41E
		public virtual int Type
		{
			get
			{
				if (this.value is EncryptedKey)
				{
					return 0;
				}
				if (this.value is Asn1OctetString)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x0018F23F File Offset: 0x0018D43F
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x0018F248 File Offset: 0x0018D448
		public override Asn1Object ToAsn1Object()
		{
			if (this.value is EncryptedKey)
			{
				return new DerTaggedObject(true, 0, this.value);
			}
			if (this.value is Asn1OctetString)
			{
				return new DerTaggedObject(false, 1, this.value);
			}
			return new DerTaggedObject(false, 2, this.value);
		}

		// Token: 0x04002BA0 RID: 11168
		public const int encryptedPrivKey = 0;

		// Token: 0x04002BA1 RID: 11169
		public const int keyGenParameters = 1;

		// Token: 0x04002BA2 RID: 11170
		public const int archiveRemGenPrivKey = 2;

		// Token: 0x04002BA3 RID: 11171
		private readonly Asn1Encodable value;
	}
}
