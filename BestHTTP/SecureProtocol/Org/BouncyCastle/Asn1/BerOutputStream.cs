using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000627 RID: 1575
	public class BerOutputStream : DerOutputStream
	{
		// Token: 0x06003B74 RID: 15220 RVA: 0x001702ED File Offset: 0x0016E4ED
		public BerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x001714E8 File Offset: 0x0016F6E8
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public override void WriteObject(object obj)
		{
			if (obj == null)
			{
				base.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not BerEncodable");
		}
	}
}
