using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.GM
{
	// Token: 0x02000722 RID: 1826
	public abstract class GMObjectIdentifiers
	{
		// Token: 0x04002A8D RID: 10893
		public static readonly DerObjectIdentifier sm_scheme = new DerObjectIdentifier("1.2.156.10197.1");

		// Token: 0x04002A8E RID: 10894
		public static readonly DerObjectIdentifier sm6_ecb = GMObjectIdentifiers.sm_scheme.Branch("101.1");

		// Token: 0x04002A8F RID: 10895
		public static readonly DerObjectIdentifier sm6_cbc = GMObjectIdentifiers.sm_scheme.Branch("101.2");

		// Token: 0x04002A90 RID: 10896
		public static readonly DerObjectIdentifier sm6_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("101.3");

		// Token: 0x04002A91 RID: 10897
		public static readonly DerObjectIdentifier sm6_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("101.4");

		// Token: 0x04002A92 RID: 10898
		public static readonly DerObjectIdentifier sm1_ecb = GMObjectIdentifiers.sm_scheme.Branch("102.1");

		// Token: 0x04002A93 RID: 10899
		public static readonly DerObjectIdentifier sm1_cbc = GMObjectIdentifiers.sm_scheme.Branch("102.2");

		// Token: 0x04002A94 RID: 10900
		public static readonly DerObjectIdentifier sm1_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("102.3");

		// Token: 0x04002A95 RID: 10901
		public static readonly DerObjectIdentifier sm1_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("102.4");

		// Token: 0x04002A96 RID: 10902
		public static readonly DerObjectIdentifier sm1_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("102.5");

		// Token: 0x04002A97 RID: 10903
		public static readonly DerObjectIdentifier sm1_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("102.6");

		// Token: 0x04002A98 RID: 10904
		public static readonly DerObjectIdentifier ssf33_ecb = GMObjectIdentifiers.sm_scheme.Branch("103.1");

		// Token: 0x04002A99 RID: 10905
		public static readonly DerObjectIdentifier ssf33_cbc = GMObjectIdentifiers.sm_scheme.Branch("103.2");

		// Token: 0x04002A9A RID: 10906
		public static readonly DerObjectIdentifier ssf33_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("103.3");

		// Token: 0x04002A9B RID: 10907
		public static readonly DerObjectIdentifier ssf33_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("103.4");

		// Token: 0x04002A9C RID: 10908
		public static readonly DerObjectIdentifier ssf33_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("103.5");

		// Token: 0x04002A9D RID: 10909
		public static readonly DerObjectIdentifier ssf33_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("103.6");

		// Token: 0x04002A9E RID: 10910
		public static readonly DerObjectIdentifier sms4_ecb = GMObjectIdentifiers.sm_scheme.Branch("104.1");

		// Token: 0x04002A9F RID: 10911
		public static readonly DerObjectIdentifier sms4_cbc = GMObjectIdentifiers.sm_scheme.Branch("104.2");

		// Token: 0x04002AA0 RID: 10912
		public static readonly DerObjectIdentifier sms4_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("104.3");

		// Token: 0x04002AA1 RID: 10913
		public static readonly DerObjectIdentifier sms4_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("104.4");

		// Token: 0x04002AA2 RID: 10914
		public static readonly DerObjectIdentifier sms4_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("104.5");

		// Token: 0x04002AA3 RID: 10915
		public static readonly DerObjectIdentifier sms4_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("104.6");

		// Token: 0x04002AA4 RID: 10916
		public static readonly DerObjectIdentifier sms4_ctr = GMObjectIdentifiers.sm_scheme.Branch("104.7");

		// Token: 0x04002AA5 RID: 10917
		public static readonly DerObjectIdentifier sms4_gcm = GMObjectIdentifiers.sm_scheme.Branch("104.8");

		// Token: 0x04002AA6 RID: 10918
		public static readonly DerObjectIdentifier sms4_ccm = GMObjectIdentifiers.sm_scheme.Branch("104.9");

		// Token: 0x04002AA7 RID: 10919
		public static readonly DerObjectIdentifier sms4_xts = GMObjectIdentifiers.sm_scheme.Branch("104.10");

		// Token: 0x04002AA8 RID: 10920
		public static readonly DerObjectIdentifier sms4_wrap = GMObjectIdentifiers.sm_scheme.Branch("104.11");

		// Token: 0x04002AA9 RID: 10921
		public static readonly DerObjectIdentifier sms4_wrap_pad = GMObjectIdentifiers.sm_scheme.Branch("104.12");

		// Token: 0x04002AAA RID: 10922
		public static readonly DerObjectIdentifier sms4_ocb = GMObjectIdentifiers.sm_scheme.Branch("104.100");

		// Token: 0x04002AAB RID: 10923
		public static readonly DerObjectIdentifier sm5 = GMObjectIdentifiers.sm_scheme.Branch("201");

		// Token: 0x04002AAC RID: 10924
		public static readonly DerObjectIdentifier sm2p256v1 = GMObjectIdentifiers.sm_scheme.Branch("301");

		// Token: 0x04002AAD RID: 10925
		public static readonly DerObjectIdentifier sm2sign = GMObjectIdentifiers.sm_scheme.Branch("301.1");

		// Token: 0x04002AAE RID: 10926
		public static readonly DerObjectIdentifier sm2exchange = GMObjectIdentifiers.sm_scheme.Branch("301.2");

		// Token: 0x04002AAF RID: 10927
		public static readonly DerObjectIdentifier sm2encrypt = GMObjectIdentifiers.sm_scheme.Branch("301.3");

		// Token: 0x04002AB0 RID: 10928
		public static readonly DerObjectIdentifier wapip192v1 = GMObjectIdentifiers.sm_scheme.Branch("301.101");

		// Token: 0x04002AB1 RID: 10929
		public static readonly DerObjectIdentifier sm2encrypt_recommendedParameters = GMObjectIdentifiers.sm2encrypt.Branch("1");

		// Token: 0x04002AB2 RID: 10930
		public static readonly DerObjectIdentifier sm2encrypt_specifiedParameters = GMObjectIdentifiers.sm2encrypt.Branch("2");

		// Token: 0x04002AB3 RID: 10931
		public static readonly DerObjectIdentifier sm2encrypt_with_sm3 = GMObjectIdentifiers.sm2encrypt.Branch("2.1");

		// Token: 0x04002AB4 RID: 10932
		public static readonly DerObjectIdentifier sm2encrypt_with_sha1 = GMObjectIdentifiers.sm2encrypt.Branch("2.2");

		// Token: 0x04002AB5 RID: 10933
		public static readonly DerObjectIdentifier sm2encrypt_with_sha224 = GMObjectIdentifiers.sm2encrypt.Branch("2.3");

		// Token: 0x04002AB6 RID: 10934
		public static readonly DerObjectIdentifier sm2encrypt_with_sha256 = GMObjectIdentifiers.sm2encrypt.Branch("2.4");

		// Token: 0x04002AB7 RID: 10935
		public static readonly DerObjectIdentifier sm2encrypt_with_sha384 = GMObjectIdentifiers.sm2encrypt.Branch("2.5");

		// Token: 0x04002AB8 RID: 10936
		public static readonly DerObjectIdentifier sm2encrypt_with_sha512 = GMObjectIdentifiers.sm2encrypt.Branch("2.6");

		// Token: 0x04002AB9 RID: 10937
		public static readonly DerObjectIdentifier sm2encrypt_with_rmd160 = GMObjectIdentifiers.sm2encrypt.Branch("2.7");

		// Token: 0x04002ABA RID: 10938
		public static readonly DerObjectIdentifier sm2encrypt_with_whirlpool = GMObjectIdentifiers.sm2encrypt.Branch("2.8");

		// Token: 0x04002ABB RID: 10939
		public static readonly DerObjectIdentifier sm2encrypt_with_blake2b512 = GMObjectIdentifiers.sm2encrypt.Branch("2.9");

		// Token: 0x04002ABC RID: 10940
		public static readonly DerObjectIdentifier sm2encrypt_with_blake2s256 = GMObjectIdentifiers.sm2encrypt.Branch("2.10");

		// Token: 0x04002ABD RID: 10941
		public static readonly DerObjectIdentifier sm2encrypt_with_md5 = GMObjectIdentifiers.sm2encrypt.Branch("2.11");

		// Token: 0x04002ABE RID: 10942
		public static readonly DerObjectIdentifier id_sm9PublicKey = GMObjectIdentifiers.sm_scheme.Branch("302");

		// Token: 0x04002ABF RID: 10943
		public static readonly DerObjectIdentifier sm9sign = GMObjectIdentifiers.sm_scheme.Branch("302.1");

		// Token: 0x04002AC0 RID: 10944
		public static readonly DerObjectIdentifier sm9keyagreement = GMObjectIdentifiers.sm_scheme.Branch("302.2");

		// Token: 0x04002AC1 RID: 10945
		public static readonly DerObjectIdentifier sm9encrypt = GMObjectIdentifiers.sm_scheme.Branch("302.3");

		// Token: 0x04002AC2 RID: 10946
		public static readonly DerObjectIdentifier sm3 = GMObjectIdentifiers.sm_scheme.Branch("401");

		// Token: 0x04002AC3 RID: 10947
		public static readonly DerObjectIdentifier hmac_sm3 = GMObjectIdentifiers.sm3.Branch("2");

		// Token: 0x04002AC4 RID: 10948
		public static readonly DerObjectIdentifier sm2sign_with_sm3 = GMObjectIdentifiers.sm_scheme.Branch("501");

		// Token: 0x04002AC5 RID: 10949
		public static readonly DerObjectIdentifier sm2sign_with_sha1 = GMObjectIdentifiers.sm_scheme.Branch("502");

		// Token: 0x04002AC6 RID: 10950
		public static readonly DerObjectIdentifier sm2sign_with_sha256 = GMObjectIdentifiers.sm_scheme.Branch("503");

		// Token: 0x04002AC7 RID: 10951
		public static readonly DerObjectIdentifier sm2sign_with_sha512 = GMObjectIdentifiers.sm_scheme.Branch("504");

		// Token: 0x04002AC8 RID: 10952
		public static readonly DerObjectIdentifier sm2sign_with_sha224 = GMObjectIdentifiers.sm_scheme.Branch("505");

		// Token: 0x04002AC9 RID: 10953
		public static readonly DerObjectIdentifier sm2sign_with_sha384 = GMObjectIdentifiers.sm_scheme.Branch("506");

		// Token: 0x04002ACA RID: 10954
		public static readonly DerObjectIdentifier sm2sign_with_rmd160 = GMObjectIdentifiers.sm_scheme.Branch("507");

		// Token: 0x04002ACB RID: 10955
		public static readonly DerObjectIdentifier sm2sign_with_whirlpool = GMObjectIdentifiers.sm_scheme.Branch("520");

		// Token: 0x04002ACC RID: 10956
		public static readonly DerObjectIdentifier sm2sign_with_blake2b512 = GMObjectIdentifiers.sm_scheme.Branch("521");

		// Token: 0x04002ACD RID: 10957
		public static readonly DerObjectIdentifier sm2sign_with_blake2s256 = GMObjectIdentifiers.sm_scheme.Branch("522");
	}
}
