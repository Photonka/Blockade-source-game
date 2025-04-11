using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000659 RID: 1625
	public class LazyAsn1InputStream : Asn1InputStream
	{
		// Token: 0x06003CDD RID: 15581 RVA: 0x0017508B File Offset: 0x0017328B
		public LazyAsn1InputStream(byte[] input) : base(input)
		{
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x00175094 File Offset: 0x00173294
		public LazyAsn1InputStream(Stream inputStream) : base(inputStream)
		{
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0017509D File Offset: 0x0017329D
		internal override DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSequence(dIn.ToArray());
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x001750AA File Offset: 0x001732AA
		internal override DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSet(dIn.ToArray());
		}
	}
}
