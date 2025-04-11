using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CF RID: 1743
	public class SmimeEncryptionKeyPreferenceAttribute : AttributeX509
	{
		// Token: 0x0600405F RID: 16479 RVA: 0x00181FE5 File Offset: 0x001801E5
		public SmimeEncryptionKeyPreferenceAttribute(IssuerAndSerialNumber issAndSer) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 0, issAndSer)))
		{
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x00181FFF File Offset: 0x001801FF
		public SmimeEncryptionKeyPreferenceAttribute(RecipientKeyIdentifier rKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 1, rKeyID)))
		{
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x00182019 File Offset: 0x00180219
		public SmimeEncryptionKeyPreferenceAttribute(Asn1OctetString sKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 2, sKeyID)))
		{
		}
	}
}
