using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000629 RID: 1577
	public class BerSequenceGenerator : BerGenerator
	{
		// Token: 0x06003B7D RID: 15229 RVA: 0x00171610 File Offset: 0x0016F810
		public BerSequenceGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(48);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x00171621 File Offset: 0x0016F821
		public BerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(48);
		}
	}
}
