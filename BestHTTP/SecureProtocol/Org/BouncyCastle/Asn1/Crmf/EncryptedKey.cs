using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000758 RID: 1880
	public class EncryptedKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060043E1 RID: 17377 RVA: 0x0018ED88 File Offset: 0x0018CF88
		public static EncryptedKey GetInstance(object o)
		{
			if (o is EncryptedKey)
			{
				return (EncryptedKey)o;
			}
			if (o is Asn1TaggedObject)
			{
				return new EncryptedKey(EnvelopedData.GetInstance((Asn1TaggedObject)o, false));
			}
			if (o is EncryptedValue)
			{
				return new EncryptedKey((EncryptedValue)o);
			}
			return new EncryptedKey(EncryptedValue.GetInstance(o));
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x0018EDDD File Offset: 0x0018CFDD
		public EncryptedKey(EnvelopedData envelopedData)
		{
			this.envelopedData = envelopedData;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0018EDEC File Offset: 0x0018CFEC
		public EncryptedKey(EncryptedValue encryptedValue)
		{
			this.encryptedValue = encryptedValue;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x0018EDFB File Offset: 0x0018CFFB
		public virtual bool IsEncryptedValue
		{
			get
			{
				return this.encryptedValue != null;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x0018EE06 File Offset: 0x0018D006
		public virtual Asn1Encodable Value
		{
			get
			{
				if (this.encryptedValue != null)
				{
					return this.encryptedValue;
				}
				return this.envelopedData;
			}
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x0018EE1D File Offset: 0x0018D01D
		public override Asn1Object ToAsn1Object()
		{
			if (this.encryptedValue != null)
			{
				return this.encryptedValue.ToAsn1Object();
			}
			return new DerTaggedObject(false, 0, this.envelopedData);
		}

		// Token: 0x04002B96 RID: 11158
		private readonly EnvelopedData envelopedData;

		// Token: 0x04002B97 RID: 11159
		private readonly EncryptedValue encryptedValue;
	}
}
