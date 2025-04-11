using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B6 RID: 694
	public class Pkcs12StoreBuilder
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x000C7D26 File Offset: 0x000C5F26
		public Pkcs12Store Build()
		{
			return new Pkcs12Store(this.keyAlgorithm, this.certAlgorithm, this.useDerEncoding);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000C7D3F File Offset: 0x000C5F3F
		public Pkcs12StoreBuilder SetCertAlgorithm(DerObjectIdentifier certAlgorithm)
		{
			this.certAlgorithm = certAlgorithm;
			return this;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000C7D49 File Offset: 0x000C5F49
		public Pkcs12StoreBuilder SetKeyAlgorithm(DerObjectIdentifier keyAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			return this;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000C7D53 File Offset: 0x000C5F53
		public Pkcs12StoreBuilder SetUseDerEncoding(bool useDerEncoding)
		{
			this.useDerEncoding = useDerEncoding;
			return this;
		}

		// Token: 0x0400178C RID: 6028
		private DerObjectIdentifier keyAlgorithm = PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc;

		// Token: 0x0400178D RID: 6029
		private DerObjectIdentifier certAlgorithm = PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc;

		// Token: 0x0400178E RID: 6030
		private bool useDerEncoding;
	}
}
