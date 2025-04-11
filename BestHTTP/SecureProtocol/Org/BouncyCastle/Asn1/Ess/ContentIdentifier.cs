using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000724 RID: 1828
	public class ContentIdentifier : Asn1Encodable
	{
		// Token: 0x06004284 RID: 17028 RVA: 0x00189F88 File Offset: 0x00188188
		public static ContentIdentifier GetInstance(object o)
		{
			if (o == null || o is ContentIdentifier)
			{
				return (ContentIdentifier)o;
			}
			if (o is Asn1OctetString)
			{
				return new ContentIdentifier((Asn1OctetString)o);
			}
			throw new ArgumentException("unknown object in 'ContentIdentifier' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x00189FD5 File Offset: 0x001881D5
		public ContentIdentifier(Asn1OctetString value)
		{
			this.value = value;
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x00189FE4 File Offset: 0x001881E4
		public ContentIdentifier(byte[] value) : this(new DerOctetString(value))
		{
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x00189FF2 File Offset: 0x001881F2
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x00189FF2 File Offset: 0x001881F2
		public override Asn1Object ToAsn1Object()
		{
			return this.value;
		}

		// Token: 0x04002AD0 RID: 10960
		private Asn1OctetString value;
	}
}
