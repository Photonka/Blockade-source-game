using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.UA
{
	// Token: 0x020006C2 RID: 1730
	public abstract class UAObjectIdentifiers
	{
		// Token: 0x040027C1 RID: 10177
		public static readonly DerObjectIdentifier UaOid = new DerObjectIdentifier("1.2.804.2.1.1.1");

		// Token: 0x040027C2 RID: 10178
		public static readonly DerObjectIdentifier dstu4145le = UAObjectIdentifiers.UaOid.Branch("1.3.1.1");

		// Token: 0x040027C3 RID: 10179
		public static readonly DerObjectIdentifier dstu4145be = UAObjectIdentifiers.UaOid.Branch("1.3.1.1.1.1");

		// Token: 0x040027C4 RID: 10180
		public static readonly DerObjectIdentifier dstu7564digest_256 = UAObjectIdentifiers.UaOid.Branch("1.2.2.1");

		// Token: 0x040027C5 RID: 10181
		public static readonly DerObjectIdentifier dstu7564digest_384 = UAObjectIdentifiers.UaOid.Branch("1.2.2.2");

		// Token: 0x040027C6 RID: 10182
		public static readonly DerObjectIdentifier dstu7564digest_512 = UAObjectIdentifiers.UaOid.Branch("1.2.2.3");

		// Token: 0x040027C7 RID: 10183
		public static readonly DerObjectIdentifier dstu7564mac_256 = UAObjectIdentifiers.UaOid.Branch("1.2.2.4");

		// Token: 0x040027C8 RID: 10184
		public static readonly DerObjectIdentifier dstu7564mac_384 = UAObjectIdentifiers.UaOid.Branch("1.2.2.5");

		// Token: 0x040027C9 RID: 10185
		public static readonly DerObjectIdentifier dstu7564mac_512 = UAObjectIdentifiers.UaOid.Branch("1.2.2.6");

		// Token: 0x040027CA RID: 10186
		public static readonly DerObjectIdentifier dstu7624ecb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.1");

		// Token: 0x040027CB RID: 10187
		public static readonly DerObjectIdentifier dstu7624ecb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.2");

		// Token: 0x040027CC RID: 10188
		public static readonly DerObjectIdentifier dstu7624ecb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.3");

		// Token: 0x040027CD RID: 10189
		public static readonly DerObjectIdentifier dstu7624ctr_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.1");

		// Token: 0x040027CE RID: 10190
		public static readonly DerObjectIdentifier dstu7624ctr_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.2");

		// Token: 0x040027CF RID: 10191
		public static readonly DerObjectIdentifier dstu7624ctr_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.3");

		// Token: 0x040027D0 RID: 10192
		public static readonly DerObjectIdentifier dstu7624cfb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.1");

		// Token: 0x040027D1 RID: 10193
		public static readonly DerObjectIdentifier dstu7624cfb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.2");

		// Token: 0x040027D2 RID: 10194
		public static readonly DerObjectIdentifier dstu7624cfb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.3");

		// Token: 0x040027D3 RID: 10195
		public static readonly DerObjectIdentifier dstu7624cmac_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.1");

		// Token: 0x040027D4 RID: 10196
		public static readonly DerObjectIdentifier dstu7624cmac_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.2");

		// Token: 0x040027D5 RID: 10197
		public static readonly DerObjectIdentifier dstu7624cmac_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.3");

		// Token: 0x040027D6 RID: 10198
		public static readonly DerObjectIdentifier dstu7624cbc_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.1");

		// Token: 0x040027D7 RID: 10199
		public static readonly DerObjectIdentifier dstu7624cbc_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.2");

		// Token: 0x040027D8 RID: 10200
		public static readonly DerObjectIdentifier dstu7624cbc_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.3");

		// Token: 0x040027D9 RID: 10201
		public static readonly DerObjectIdentifier dstu7624ofb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.1");

		// Token: 0x040027DA RID: 10202
		public static readonly DerObjectIdentifier dstu7624ofb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.2");

		// Token: 0x040027DB RID: 10203
		public static readonly DerObjectIdentifier dstu7624ofb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.3");

		// Token: 0x040027DC RID: 10204
		public static readonly DerObjectIdentifier dstu7624gmac_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.1");

		// Token: 0x040027DD RID: 10205
		public static readonly DerObjectIdentifier dstu7624gmac_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.2");

		// Token: 0x040027DE RID: 10206
		public static readonly DerObjectIdentifier dstu7624gmac_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.3");

		// Token: 0x040027DF RID: 10207
		public static readonly DerObjectIdentifier dstu7624ccm_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.1");

		// Token: 0x040027E0 RID: 10208
		public static readonly DerObjectIdentifier dstu7624ccm_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.2");

		// Token: 0x040027E1 RID: 10209
		public static readonly DerObjectIdentifier dstu7624ccm_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.3");

		// Token: 0x040027E2 RID: 10210
		public static readonly DerObjectIdentifier dstu7624xts_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.1");

		// Token: 0x040027E3 RID: 10211
		public static readonly DerObjectIdentifier dstu7624xts_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.2");

		// Token: 0x040027E4 RID: 10212
		public static readonly DerObjectIdentifier dstu7624xts_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.3");

		// Token: 0x040027E5 RID: 10213
		public static readonly DerObjectIdentifier dstu7624kw_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.1");

		// Token: 0x040027E6 RID: 10214
		public static readonly DerObjectIdentifier dstu7624kw_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.2");

		// Token: 0x040027E7 RID: 10215
		public static readonly DerObjectIdentifier dstu7624kw_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.3");
	}
}
