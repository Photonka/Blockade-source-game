using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062C RID: 1580
	public class BerSetGenerator : BerGenerator
	{
		// Token: 0x06003B8A RID: 15242 RVA: 0x00171754 File Offset: 0x0016F954
		public BerSetGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(49);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00171765 File Offset: 0x0016F965
		public BerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(49);
		}
	}
}
