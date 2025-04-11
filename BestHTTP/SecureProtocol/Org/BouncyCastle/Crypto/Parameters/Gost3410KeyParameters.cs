using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CC RID: 1228
	public abstract class Gost3410KeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002FCA RID: 12234 RVA: 0x0012899C File Offset: 0x00126B9C
		protected Gost3410KeyParameters(bool isPrivate, Gost3410Parameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x001289AC File Offset: 0x00126BAC
		protected Gost3410KeyParameters(bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			this.parameters = Gost3410KeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x001289C8 File Offset: 0x00126BC8
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x001289D0 File Offset: 0x00126BD0
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x001289D8 File Offset: 0x00126BD8
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

		// Token: 0x04001EB6 RID: 7862
		private readonly Gost3410Parameters parameters;

		// Token: 0x04001EB7 RID: 7863
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
