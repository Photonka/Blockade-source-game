using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BC RID: 1212
	public class ECKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002F6F RID: 12143 RVA: 0x00127DF7 File Offset: 0x00125FF7
		public ECKeyGenerationParameters(ECDomainParameters domainParameters, SecureRandom random) : base(random, domainParameters.N.BitLength)
		{
			this.domainParams = domainParameters;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x00127E12 File Offset: 0x00126012
		public ECKeyGenerationParameters(DerObjectIdentifier publicKeyParamSet, SecureRandom random) : this(ECKeyParameters.LookupParameters(publicKeyParamSet), random)
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x00127E28 File Offset: 0x00126028
		public ECDomainParameters DomainParameters
		{
			get
			{
				return this.domainParams;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x00127E30 File Offset: 0x00126030
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x04001E9B RID: 7835
		private readonly ECDomainParameters domainParams;

		// Token: 0x04001E9C RID: 7836
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
