using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000626 RID: 1574
	public class BerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06003B71 RID: 15217 RVA: 0x00171480 File Offset: 0x0016F680
		internal BerOctetStringParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x0017148F File Offset: 0x0016F68F
		public Stream GetOctetStream()
		{
			return new ConstructedOctetStream(this._parser);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x0017149C File Offset: 0x0016F69C
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new BerOctetString(Streams.ReadAll(this.GetOctetStream()));
			}
			catch (IOException ex)
			{
				throw new Asn1ParsingException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0400258C RID: 9612
		private readonly Asn1StreamParser _parser;
	}
}
