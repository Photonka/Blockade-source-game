using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CB RID: 1227
	public class Gost3410KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002FC5 RID: 12229 RVA: 0x0012890C File Offset: 0x00126B0C
		public Gost3410KeyGenerationParameters(SecureRandom random, Gost3410Parameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00128929 File Offset: 0x00126B29
		public Gost3410KeyGenerationParameters(SecureRandom random, DerObjectIdentifier publicKeyParamSet) : this(random, Gost3410KeyGenerationParameters.LookupParameters(publicKeyParamSet))
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x0012893F File Offset: 0x00126B3F
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x00128947 File Offset: 0x00126B47
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00128950 File Offset: 0x00126B50
		private static Gost3410Parameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			Gost3410ParamSetParameters byOid = Gost3410NamedParameters.GetByOid(publicKeyParamSet);
			if (byOid == null)
			{
				throw new ArgumentException("OID is not a valid CryptoPro public key parameter set", "publicKeyParamSet");
			}
			return new Gost3410Parameters(byOid.P, byOid.Q, byOid.A);
		}

		// Token: 0x04001EB4 RID: 7860
		private readonly Gost3410Parameters parameters;

		// Token: 0x04001EB5 RID: 7861
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
