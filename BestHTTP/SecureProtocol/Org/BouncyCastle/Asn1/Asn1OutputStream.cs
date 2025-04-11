using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000615 RID: 1557
	public class Asn1OutputStream : DerOutputStream
	{
		// Token: 0x06003B0B RID: 15115 RVA: 0x001702ED File Offset: 0x0016E4ED
		public Asn1OutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x001702F8 File Offset: 0x0016E4F8
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
			throw new IOException("object not Asn1Encodable");
		}
	}
}
