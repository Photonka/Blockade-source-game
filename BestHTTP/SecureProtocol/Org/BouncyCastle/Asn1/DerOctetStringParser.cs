using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000643 RID: 1603
	public class DerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06003C5C RID: 15452 RVA: 0x00173EBE File Offset: 0x001720BE
		internal DerOctetStringParser(DefiniteLengthInputStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x00173ECD File Offset: 0x001720CD
		public Stream GetOctetStream()
		{
			return this.stream;
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x00173ED8 File Offset: 0x001720D8
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new DerOctetString(this.stream.ToArray());
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x040025BC RID: 9660
		private readonly DefiniteLengthInputStream stream;
	}
}
