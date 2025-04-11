using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F5 RID: 1013
	public abstract class CipherSuite
	{
		// Token: 0x06002937 RID: 10551 RVA: 0x00110684 File Offset: 0x0010E884
		public static bool IsScsv(int cipherSuite)
		{
			return cipherSuite == 255 || cipherSuite == 22016;
		}

		// Token: 0x04001A41 RID: 6721
		public const int TLS_NULL_WITH_NULL_NULL = 0;

		// Token: 0x04001A42 RID: 6722
		public const int TLS_RSA_WITH_NULL_MD5 = 1;

		// Token: 0x04001A43 RID: 6723
		public const int TLS_RSA_WITH_NULL_SHA = 2;

		// Token: 0x04001A44 RID: 6724
		public const int TLS_RSA_EXPORT_WITH_RC4_40_MD5 = 3;

		// Token: 0x04001A45 RID: 6725
		public const int TLS_RSA_WITH_RC4_128_MD5 = 4;

		// Token: 0x04001A46 RID: 6726
		public const int TLS_RSA_WITH_RC4_128_SHA = 5;

		// Token: 0x04001A47 RID: 6727
		public const int TLS_RSA_EXPORT_WITH_RC2_CBC_40_MD5 = 6;

		// Token: 0x04001A48 RID: 6728
		public const int TLS_RSA_WITH_IDEA_CBC_SHA = 7;

		// Token: 0x04001A49 RID: 6729
		public const int TLS_RSA_EXPORT_WITH_DES40_CBC_SHA = 8;

		// Token: 0x04001A4A RID: 6730
		public const int TLS_RSA_WITH_DES_CBC_SHA = 9;

		// Token: 0x04001A4B RID: 6731
		public const int TLS_RSA_WITH_3DES_EDE_CBC_SHA = 10;

		// Token: 0x04001A4C RID: 6732
		public const int TLS_DH_DSS_EXPORT_WITH_DES40_CBC_SHA = 11;

		// Token: 0x04001A4D RID: 6733
		public const int TLS_DH_DSS_WITH_DES_CBC_SHA = 12;

		// Token: 0x04001A4E RID: 6734
		public const int TLS_DH_DSS_WITH_3DES_EDE_CBC_SHA = 13;

		// Token: 0x04001A4F RID: 6735
		public const int TLS_DH_RSA_EXPORT_WITH_DES40_CBC_SHA = 14;

		// Token: 0x04001A50 RID: 6736
		public const int TLS_DH_RSA_WITH_DES_CBC_SHA = 15;

		// Token: 0x04001A51 RID: 6737
		public const int TLS_DH_RSA_WITH_3DES_EDE_CBC_SHA = 16;

		// Token: 0x04001A52 RID: 6738
		public const int TLS_DHE_DSS_EXPORT_WITH_DES40_CBC_SHA = 17;

		// Token: 0x04001A53 RID: 6739
		public const int TLS_DHE_DSS_WITH_DES_CBC_SHA = 18;

		// Token: 0x04001A54 RID: 6740
		public const int TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA = 19;

		// Token: 0x04001A55 RID: 6741
		public const int TLS_DHE_RSA_EXPORT_WITH_DES40_CBC_SHA = 20;

		// Token: 0x04001A56 RID: 6742
		public const int TLS_DHE_RSA_WITH_DES_CBC_SHA = 21;

		// Token: 0x04001A57 RID: 6743
		public const int TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA = 22;

		// Token: 0x04001A58 RID: 6744
		public const int TLS_DH_anon_EXPORT_WITH_RC4_40_MD5 = 23;

		// Token: 0x04001A59 RID: 6745
		public const int TLS_DH_anon_WITH_RC4_128_MD5 = 24;

		// Token: 0x04001A5A RID: 6746
		public const int TLS_DH_anon_EXPORT_WITH_DES40_CBC_SHA = 25;

		// Token: 0x04001A5B RID: 6747
		public const int TLS_DH_anon_WITH_DES_CBC_SHA = 26;

		// Token: 0x04001A5C RID: 6748
		public const int TLS_DH_anon_WITH_3DES_EDE_CBC_SHA = 27;

		// Token: 0x04001A5D RID: 6749
		public const int TLS_RSA_WITH_AES_128_CBC_SHA = 47;

		// Token: 0x04001A5E RID: 6750
		public const int TLS_DH_DSS_WITH_AES_128_CBC_SHA = 48;

		// Token: 0x04001A5F RID: 6751
		public const int TLS_DH_RSA_WITH_AES_128_CBC_SHA = 49;

		// Token: 0x04001A60 RID: 6752
		public const int TLS_DHE_DSS_WITH_AES_128_CBC_SHA = 50;

		// Token: 0x04001A61 RID: 6753
		public const int TLS_DHE_RSA_WITH_AES_128_CBC_SHA = 51;

		// Token: 0x04001A62 RID: 6754
		public const int TLS_DH_anon_WITH_AES_128_CBC_SHA = 52;

		// Token: 0x04001A63 RID: 6755
		public const int TLS_RSA_WITH_AES_256_CBC_SHA = 53;

		// Token: 0x04001A64 RID: 6756
		public const int TLS_DH_DSS_WITH_AES_256_CBC_SHA = 54;

		// Token: 0x04001A65 RID: 6757
		public const int TLS_DH_RSA_WITH_AES_256_CBC_SHA = 55;

		// Token: 0x04001A66 RID: 6758
		public const int TLS_DHE_DSS_WITH_AES_256_CBC_SHA = 56;

		// Token: 0x04001A67 RID: 6759
		public const int TLS_DHE_RSA_WITH_AES_256_CBC_SHA = 57;

		// Token: 0x04001A68 RID: 6760
		public const int TLS_DH_anon_WITH_AES_256_CBC_SHA = 58;

		// Token: 0x04001A69 RID: 6761
		public const int TLS_RSA_WITH_CAMELLIA_128_CBC_SHA = 65;

		// Token: 0x04001A6A RID: 6762
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA = 66;

		// Token: 0x04001A6B RID: 6763
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA = 67;

		// Token: 0x04001A6C RID: 6764
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA = 68;

		// Token: 0x04001A6D RID: 6765
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA = 69;

		// Token: 0x04001A6E RID: 6766
		public const int TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA = 70;

		// Token: 0x04001A6F RID: 6767
		public const int TLS_RSA_WITH_CAMELLIA_256_CBC_SHA = 132;

		// Token: 0x04001A70 RID: 6768
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA = 133;

		// Token: 0x04001A71 RID: 6769
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA = 134;

		// Token: 0x04001A72 RID: 6770
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA = 135;

		// Token: 0x04001A73 RID: 6771
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA = 136;

		// Token: 0x04001A74 RID: 6772
		public const int TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA = 137;

		// Token: 0x04001A75 RID: 6773
		public const int TLS_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 186;

		// Token: 0x04001A76 RID: 6774
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 187;

		// Token: 0x04001A77 RID: 6775
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 188;

		// Token: 0x04001A78 RID: 6776
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 189;

		// Token: 0x04001A79 RID: 6777
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 190;

		// Token: 0x04001A7A RID: 6778
		public const int TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA256 = 191;

		// Token: 0x04001A7B RID: 6779
		public const int TLS_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 192;

		// Token: 0x04001A7C RID: 6780
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 193;

		// Token: 0x04001A7D RID: 6781
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 194;

		// Token: 0x04001A7E RID: 6782
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 195;

		// Token: 0x04001A7F RID: 6783
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 196;

		// Token: 0x04001A80 RID: 6784
		public const int TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA256 = 197;

		// Token: 0x04001A81 RID: 6785
		public const int TLS_RSA_WITH_SEED_CBC_SHA = 150;

		// Token: 0x04001A82 RID: 6786
		public const int TLS_DH_DSS_WITH_SEED_CBC_SHA = 151;

		// Token: 0x04001A83 RID: 6787
		public const int TLS_DH_RSA_WITH_SEED_CBC_SHA = 152;

		// Token: 0x04001A84 RID: 6788
		public const int TLS_DHE_DSS_WITH_SEED_CBC_SHA = 153;

		// Token: 0x04001A85 RID: 6789
		public const int TLS_DHE_RSA_WITH_SEED_CBC_SHA = 154;

		// Token: 0x04001A86 RID: 6790
		public const int TLS_DH_anon_WITH_SEED_CBC_SHA = 155;

		// Token: 0x04001A87 RID: 6791
		public const int TLS_PSK_WITH_RC4_128_SHA = 138;

		// Token: 0x04001A88 RID: 6792
		public const int TLS_PSK_WITH_3DES_EDE_CBC_SHA = 139;

		// Token: 0x04001A89 RID: 6793
		public const int TLS_PSK_WITH_AES_128_CBC_SHA = 140;

		// Token: 0x04001A8A RID: 6794
		public const int TLS_PSK_WITH_AES_256_CBC_SHA = 141;

		// Token: 0x04001A8B RID: 6795
		public const int TLS_DHE_PSK_WITH_RC4_128_SHA = 142;

		// Token: 0x04001A8C RID: 6796
		public const int TLS_DHE_PSK_WITH_3DES_EDE_CBC_SHA = 143;

		// Token: 0x04001A8D RID: 6797
		public const int TLS_DHE_PSK_WITH_AES_128_CBC_SHA = 144;

		// Token: 0x04001A8E RID: 6798
		public const int TLS_DHE_PSK_WITH_AES_256_CBC_SHA = 145;

		// Token: 0x04001A8F RID: 6799
		public const int TLS_RSA_PSK_WITH_RC4_128_SHA = 146;

		// Token: 0x04001A90 RID: 6800
		public const int TLS_RSA_PSK_WITH_3DES_EDE_CBC_SHA = 147;

		// Token: 0x04001A91 RID: 6801
		public const int TLS_RSA_PSK_WITH_AES_128_CBC_SHA = 148;

		// Token: 0x04001A92 RID: 6802
		public const int TLS_RSA_PSK_WITH_AES_256_CBC_SHA = 149;

		// Token: 0x04001A93 RID: 6803
		public const int TLS_ECDH_ECDSA_WITH_NULL_SHA = 49153;

		// Token: 0x04001A94 RID: 6804
		public const int TLS_ECDH_ECDSA_WITH_RC4_128_SHA = 49154;

		// Token: 0x04001A95 RID: 6805
		public const int TLS_ECDH_ECDSA_WITH_3DES_EDE_CBC_SHA = 49155;

		// Token: 0x04001A96 RID: 6806
		public const int TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA = 49156;

		// Token: 0x04001A97 RID: 6807
		public const int TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA = 49157;

		// Token: 0x04001A98 RID: 6808
		public const int TLS_ECDHE_ECDSA_WITH_NULL_SHA = 49158;

		// Token: 0x04001A99 RID: 6809
		public const int TLS_ECDHE_ECDSA_WITH_RC4_128_SHA = 49159;

		// Token: 0x04001A9A RID: 6810
		public const int TLS_ECDHE_ECDSA_WITH_3DES_EDE_CBC_SHA = 49160;

		// Token: 0x04001A9B RID: 6811
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA = 49161;

		// Token: 0x04001A9C RID: 6812
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA = 49162;

		// Token: 0x04001A9D RID: 6813
		public const int TLS_ECDH_RSA_WITH_NULL_SHA = 49163;

		// Token: 0x04001A9E RID: 6814
		public const int TLS_ECDH_RSA_WITH_RC4_128_SHA = 49164;

		// Token: 0x04001A9F RID: 6815
		public const int TLS_ECDH_RSA_WITH_3DES_EDE_CBC_SHA = 49165;

		// Token: 0x04001AA0 RID: 6816
		public const int TLS_ECDH_RSA_WITH_AES_128_CBC_SHA = 49166;

		// Token: 0x04001AA1 RID: 6817
		public const int TLS_ECDH_RSA_WITH_AES_256_CBC_SHA = 49167;

		// Token: 0x04001AA2 RID: 6818
		public const int TLS_ECDHE_RSA_WITH_NULL_SHA = 49168;

		// Token: 0x04001AA3 RID: 6819
		public const int TLS_ECDHE_RSA_WITH_RC4_128_SHA = 49169;

		// Token: 0x04001AA4 RID: 6820
		public const int TLS_ECDHE_RSA_WITH_3DES_EDE_CBC_SHA = 49170;

		// Token: 0x04001AA5 RID: 6821
		public const int TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA = 49171;

		// Token: 0x04001AA6 RID: 6822
		public const int TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA = 49172;

		// Token: 0x04001AA7 RID: 6823
		public const int TLS_ECDH_anon_WITH_NULL_SHA = 49173;

		// Token: 0x04001AA8 RID: 6824
		public const int TLS_ECDH_anon_WITH_RC4_128_SHA = 49174;

		// Token: 0x04001AA9 RID: 6825
		public const int TLS_ECDH_anon_WITH_3DES_EDE_CBC_SHA = 49175;

		// Token: 0x04001AAA RID: 6826
		public const int TLS_ECDH_anon_WITH_AES_128_CBC_SHA = 49176;

		// Token: 0x04001AAB RID: 6827
		public const int TLS_ECDH_anon_WITH_AES_256_CBC_SHA = 49177;

		// Token: 0x04001AAC RID: 6828
		public const int TLS_PSK_WITH_NULL_SHA = 44;

		// Token: 0x04001AAD RID: 6829
		public const int TLS_DHE_PSK_WITH_NULL_SHA = 45;

		// Token: 0x04001AAE RID: 6830
		public const int TLS_RSA_PSK_WITH_NULL_SHA = 46;

		// Token: 0x04001AAF RID: 6831
		public const int TLS_SRP_SHA_WITH_3DES_EDE_CBC_SHA = 49178;

		// Token: 0x04001AB0 RID: 6832
		public const int TLS_SRP_SHA_RSA_WITH_3DES_EDE_CBC_SHA = 49179;

		// Token: 0x04001AB1 RID: 6833
		public const int TLS_SRP_SHA_DSS_WITH_3DES_EDE_CBC_SHA = 49180;

		// Token: 0x04001AB2 RID: 6834
		public const int TLS_SRP_SHA_WITH_AES_128_CBC_SHA = 49181;

		// Token: 0x04001AB3 RID: 6835
		public const int TLS_SRP_SHA_RSA_WITH_AES_128_CBC_SHA = 49182;

		// Token: 0x04001AB4 RID: 6836
		public const int TLS_SRP_SHA_DSS_WITH_AES_128_CBC_SHA = 49183;

		// Token: 0x04001AB5 RID: 6837
		public const int TLS_SRP_SHA_WITH_AES_256_CBC_SHA = 49184;

		// Token: 0x04001AB6 RID: 6838
		public const int TLS_SRP_SHA_RSA_WITH_AES_256_CBC_SHA = 49185;

		// Token: 0x04001AB7 RID: 6839
		public const int TLS_SRP_SHA_DSS_WITH_AES_256_CBC_SHA = 49186;

		// Token: 0x04001AB8 RID: 6840
		public const int TLS_RSA_WITH_NULL_SHA256 = 59;

		// Token: 0x04001AB9 RID: 6841
		public const int TLS_RSA_WITH_AES_128_CBC_SHA256 = 60;

		// Token: 0x04001ABA RID: 6842
		public const int TLS_RSA_WITH_AES_256_CBC_SHA256 = 61;

		// Token: 0x04001ABB RID: 6843
		public const int TLS_DH_DSS_WITH_AES_128_CBC_SHA256 = 62;

		// Token: 0x04001ABC RID: 6844
		public const int TLS_DH_RSA_WITH_AES_128_CBC_SHA256 = 63;

		// Token: 0x04001ABD RID: 6845
		public const int TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 = 64;

		// Token: 0x04001ABE RID: 6846
		public const int TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 = 103;

		// Token: 0x04001ABF RID: 6847
		public const int TLS_DH_DSS_WITH_AES_256_CBC_SHA256 = 104;

		// Token: 0x04001AC0 RID: 6848
		public const int TLS_DH_RSA_WITH_AES_256_CBC_SHA256 = 105;

		// Token: 0x04001AC1 RID: 6849
		public const int TLS_DHE_DSS_WITH_AES_256_CBC_SHA256 = 106;

		// Token: 0x04001AC2 RID: 6850
		public const int TLS_DHE_RSA_WITH_AES_256_CBC_SHA256 = 107;

		// Token: 0x04001AC3 RID: 6851
		public const int TLS_DH_anon_WITH_AES_128_CBC_SHA256 = 108;

		// Token: 0x04001AC4 RID: 6852
		public const int TLS_DH_anon_WITH_AES_256_CBC_SHA256 = 109;

		// Token: 0x04001AC5 RID: 6853
		public const int TLS_RSA_WITH_AES_128_GCM_SHA256 = 156;

		// Token: 0x04001AC6 RID: 6854
		public const int TLS_RSA_WITH_AES_256_GCM_SHA384 = 157;

		// Token: 0x04001AC7 RID: 6855
		public const int TLS_DHE_RSA_WITH_AES_128_GCM_SHA256 = 158;

		// Token: 0x04001AC8 RID: 6856
		public const int TLS_DHE_RSA_WITH_AES_256_GCM_SHA384 = 159;

		// Token: 0x04001AC9 RID: 6857
		public const int TLS_DH_RSA_WITH_AES_128_GCM_SHA256 = 160;

		// Token: 0x04001ACA RID: 6858
		public const int TLS_DH_RSA_WITH_AES_256_GCM_SHA384 = 161;

		// Token: 0x04001ACB RID: 6859
		public const int TLS_DHE_DSS_WITH_AES_128_GCM_SHA256 = 162;

		// Token: 0x04001ACC RID: 6860
		public const int TLS_DHE_DSS_WITH_AES_256_GCM_SHA384 = 163;

		// Token: 0x04001ACD RID: 6861
		public const int TLS_DH_DSS_WITH_AES_128_GCM_SHA256 = 164;

		// Token: 0x04001ACE RID: 6862
		public const int TLS_DH_DSS_WITH_AES_256_GCM_SHA384 = 165;

		// Token: 0x04001ACF RID: 6863
		public const int TLS_DH_anon_WITH_AES_128_GCM_SHA256 = 166;

		// Token: 0x04001AD0 RID: 6864
		public const int TLS_DH_anon_WITH_AES_256_GCM_SHA384 = 167;

		// Token: 0x04001AD1 RID: 6865
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 = 49187;

		// Token: 0x04001AD2 RID: 6866
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384 = 49188;

		// Token: 0x04001AD3 RID: 6867
		public const int TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 = 49189;

		// Token: 0x04001AD4 RID: 6868
		public const int TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA384 = 49190;

		// Token: 0x04001AD5 RID: 6869
		public const int TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 = 49191;

		// Token: 0x04001AD6 RID: 6870
		public const int TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384 = 49192;

		// Token: 0x04001AD7 RID: 6871
		public const int TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 = 49193;

		// Token: 0x04001AD8 RID: 6872
		public const int TLS_ECDH_RSA_WITH_AES_256_CBC_SHA384 = 49194;

		// Token: 0x04001AD9 RID: 6873
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256 = 49195;

		// Token: 0x04001ADA RID: 6874
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384 = 49196;

		// Token: 0x04001ADB RID: 6875
		public const int TLS_ECDH_ECDSA_WITH_AES_128_GCM_SHA256 = 49197;

		// Token: 0x04001ADC RID: 6876
		public const int TLS_ECDH_ECDSA_WITH_AES_256_GCM_SHA384 = 49198;

		// Token: 0x04001ADD RID: 6877
		public const int TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256 = 49199;

		// Token: 0x04001ADE RID: 6878
		public const int TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384 = 49200;

		// Token: 0x04001ADF RID: 6879
		public const int TLS_ECDH_RSA_WITH_AES_128_GCM_SHA256 = 49201;

		// Token: 0x04001AE0 RID: 6880
		public const int TLS_ECDH_RSA_WITH_AES_256_GCM_SHA384 = 49202;

		// Token: 0x04001AE1 RID: 6881
		public const int TLS_PSK_WITH_AES_128_GCM_SHA256 = 168;

		// Token: 0x04001AE2 RID: 6882
		public const int TLS_PSK_WITH_AES_256_GCM_SHA384 = 169;

		// Token: 0x04001AE3 RID: 6883
		public const int TLS_DHE_PSK_WITH_AES_128_GCM_SHA256 = 170;

		// Token: 0x04001AE4 RID: 6884
		public const int TLS_DHE_PSK_WITH_AES_256_GCM_SHA384 = 171;

		// Token: 0x04001AE5 RID: 6885
		public const int TLS_RSA_PSK_WITH_AES_128_GCM_SHA256 = 172;

		// Token: 0x04001AE6 RID: 6886
		public const int TLS_RSA_PSK_WITH_AES_256_GCM_SHA384 = 173;

		// Token: 0x04001AE7 RID: 6887
		public const int TLS_PSK_WITH_AES_128_CBC_SHA256 = 174;

		// Token: 0x04001AE8 RID: 6888
		public const int TLS_PSK_WITH_AES_256_CBC_SHA384 = 175;

		// Token: 0x04001AE9 RID: 6889
		public const int TLS_PSK_WITH_NULL_SHA256 = 176;

		// Token: 0x04001AEA RID: 6890
		public const int TLS_PSK_WITH_NULL_SHA384 = 177;

		// Token: 0x04001AEB RID: 6891
		public const int TLS_DHE_PSK_WITH_AES_128_CBC_SHA256 = 178;

		// Token: 0x04001AEC RID: 6892
		public const int TLS_DHE_PSK_WITH_AES_256_CBC_SHA384 = 179;

		// Token: 0x04001AED RID: 6893
		public const int TLS_DHE_PSK_WITH_NULL_SHA256 = 180;

		// Token: 0x04001AEE RID: 6894
		public const int TLS_DHE_PSK_WITH_NULL_SHA384 = 181;

		// Token: 0x04001AEF RID: 6895
		public const int TLS_RSA_PSK_WITH_AES_128_CBC_SHA256 = 182;

		// Token: 0x04001AF0 RID: 6896
		public const int TLS_RSA_PSK_WITH_AES_256_CBC_SHA384 = 183;

		// Token: 0x04001AF1 RID: 6897
		public const int TLS_RSA_PSK_WITH_NULL_SHA256 = 184;

		// Token: 0x04001AF2 RID: 6898
		public const int TLS_RSA_PSK_WITH_NULL_SHA384 = 185;

		// Token: 0x04001AF3 RID: 6899
		public const int TLS_ECDHE_PSK_WITH_RC4_128_SHA = 49203;

		// Token: 0x04001AF4 RID: 6900
		public const int TLS_ECDHE_PSK_WITH_3DES_EDE_CBC_SHA = 49204;

		// Token: 0x04001AF5 RID: 6901
		public const int TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA = 49205;

		// Token: 0x04001AF6 RID: 6902
		public const int TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA = 49206;

		// Token: 0x04001AF7 RID: 6903
		public const int TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA256 = 49207;

		// Token: 0x04001AF8 RID: 6904
		public const int TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA384 = 49208;

		// Token: 0x04001AF9 RID: 6905
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA = 49209;

		// Token: 0x04001AFA RID: 6906
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA256 = 49210;

		// Token: 0x04001AFB RID: 6907
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA384 = 49211;

		// Token: 0x04001AFC RID: 6908
		public const int TLS_EMPTY_RENEGOTIATION_INFO_SCSV = 255;

		// Token: 0x04001AFD RID: 6909
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49266;

		// Token: 0x04001AFE RID: 6910
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49267;

		// Token: 0x04001AFF RID: 6911
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49268;

		// Token: 0x04001B00 RID: 6912
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49269;

		// Token: 0x04001B01 RID: 6913
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49270;

		// Token: 0x04001B02 RID: 6914
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49271;

		// Token: 0x04001B03 RID: 6915
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49272;

		// Token: 0x04001B04 RID: 6916
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49273;

		// Token: 0x04001B05 RID: 6917
		public const int TLS_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49274;

		// Token: 0x04001B06 RID: 6918
		public const int TLS_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49275;

		// Token: 0x04001B07 RID: 6919
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49276;

		// Token: 0x04001B08 RID: 6920
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49277;

		// Token: 0x04001B09 RID: 6921
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49278;

		// Token: 0x04001B0A RID: 6922
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49279;

		// Token: 0x04001B0B RID: 6923
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49280;

		// Token: 0x04001B0C RID: 6924
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49281;

		// Token: 0x04001B0D RID: 6925
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49282;

		// Token: 0x04001B0E RID: 6926
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49283;

		// Token: 0x04001B0F RID: 6927
		public const int TLS_DH_anon_WITH_CAMELLIA_128_GCM_SHA256 = 49284;

		// Token: 0x04001B10 RID: 6928
		public const int TLS_DH_anon_WITH_CAMELLIA_256_GCM_SHA384 = 49285;

		// Token: 0x04001B11 RID: 6929
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49286;

		// Token: 0x04001B12 RID: 6930
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49287;

		// Token: 0x04001B13 RID: 6931
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49288;

		// Token: 0x04001B14 RID: 6932
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49289;

		// Token: 0x04001B15 RID: 6933
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49290;

		// Token: 0x04001B16 RID: 6934
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49291;

		// Token: 0x04001B17 RID: 6935
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49292;

		// Token: 0x04001B18 RID: 6936
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49293;

		// Token: 0x04001B19 RID: 6937
		public const int TLS_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49294;

		// Token: 0x04001B1A RID: 6938
		public const int TLS_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49295;

		// Token: 0x04001B1B RID: 6939
		public const int TLS_DHE_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49296;

		// Token: 0x04001B1C RID: 6940
		public const int TLS_DHE_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49297;

		// Token: 0x04001B1D RID: 6941
		public const int TLS_RSA_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49298;

		// Token: 0x04001B1E RID: 6942
		public const int TLS_RSA_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49299;

		// Token: 0x04001B1F RID: 6943
		public const int TLS_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49300;

		// Token: 0x04001B20 RID: 6944
		public const int TLS_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49301;

		// Token: 0x04001B21 RID: 6945
		public const int TLS_DHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49302;

		// Token: 0x04001B22 RID: 6946
		public const int TLS_DHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49303;

		// Token: 0x04001B23 RID: 6947
		public const int TLS_RSA_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49304;

		// Token: 0x04001B24 RID: 6948
		public const int TLS_RSA_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49305;

		// Token: 0x04001B25 RID: 6949
		public const int TLS_ECDHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49306;

		// Token: 0x04001B26 RID: 6950
		public const int TLS_ECDHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49307;

		// Token: 0x04001B27 RID: 6951
		public const int TLS_RSA_WITH_AES_128_CCM = 49308;

		// Token: 0x04001B28 RID: 6952
		public const int TLS_RSA_WITH_AES_256_CCM = 49309;

		// Token: 0x04001B29 RID: 6953
		public const int TLS_DHE_RSA_WITH_AES_128_CCM = 49310;

		// Token: 0x04001B2A RID: 6954
		public const int TLS_DHE_RSA_WITH_AES_256_CCM = 49311;

		// Token: 0x04001B2B RID: 6955
		public const int TLS_RSA_WITH_AES_128_CCM_8 = 49312;

		// Token: 0x04001B2C RID: 6956
		public const int TLS_RSA_WITH_AES_256_CCM_8 = 49313;

		// Token: 0x04001B2D RID: 6957
		public const int TLS_DHE_RSA_WITH_AES_128_CCM_8 = 49314;

		// Token: 0x04001B2E RID: 6958
		public const int TLS_DHE_RSA_WITH_AES_256_CCM_8 = 49315;

		// Token: 0x04001B2F RID: 6959
		public const int TLS_PSK_WITH_AES_128_CCM = 49316;

		// Token: 0x04001B30 RID: 6960
		public const int TLS_PSK_WITH_AES_256_CCM = 49317;

		// Token: 0x04001B31 RID: 6961
		public const int TLS_DHE_PSK_WITH_AES_128_CCM = 49318;

		// Token: 0x04001B32 RID: 6962
		public const int TLS_DHE_PSK_WITH_AES_256_CCM = 49319;

		// Token: 0x04001B33 RID: 6963
		public const int TLS_PSK_WITH_AES_128_CCM_8 = 49320;

		// Token: 0x04001B34 RID: 6964
		public const int TLS_PSK_WITH_AES_256_CCM_8 = 49321;

		// Token: 0x04001B35 RID: 6965
		public const int TLS_PSK_DHE_WITH_AES_128_CCM_8 = 49322;

		// Token: 0x04001B36 RID: 6966
		public const int TLS_PSK_DHE_WITH_AES_256_CCM_8 = 49323;

		// Token: 0x04001B37 RID: 6967
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CCM = 49324;

		// Token: 0x04001B38 RID: 6968
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CCM = 49325;

		// Token: 0x04001B39 RID: 6969
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CCM_8 = 49326;

		// Token: 0x04001B3A RID: 6970
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CCM_8 = 49327;

		// Token: 0x04001B3B RID: 6971
		public const int TLS_FALLBACK_SCSV = 22016;

		// Token: 0x04001B3C RID: 6972
		public const int DRAFT_TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52392;

		// Token: 0x04001B3D RID: 6973
		public const int DRAFT_TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305_SHA256 = 52393;

		// Token: 0x04001B3E RID: 6974
		public const int DRAFT_TLS_DHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52394;

		// Token: 0x04001B3F RID: 6975
		public const int DRAFT_TLS_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52395;

		// Token: 0x04001B40 RID: 6976
		public const int DRAFT_TLS_ECDHE_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52396;

		// Token: 0x04001B41 RID: 6977
		public const int DRAFT_TLS_DHE_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52397;

		// Token: 0x04001B42 RID: 6978
		public const int DRAFT_TLS_RSA_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52398;

		// Token: 0x04001B43 RID: 6979
		public const int DRAFT_TLS_DHE_RSA_WITH_AES_128_OCB = 65280;

		// Token: 0x04001B44 RID: 6980
		public const int DRAFT_TLS_DHE_RSA_WITH_AES_256_OCB = 65281;

		// Token: 0x04001B45 RID: 6981
		public const int DRAFT_TLS_ECDHE_RSA_WITH_AES_128_OCB = 65282;

		// Token: 0x04001B46 RID: 6982
		public const int DRAFT_TLS_ECDHE_RSA_WITH_AES_256_OCB = 65283;

		// Token: 0x04001B47 RID: 6983
		public const int DRAFT_TLS_ECDHE_ECDSA_WITH_AES_128_OCB = 65284;

		// Token: 0x04001B48 RID: 6984
		public const int DRAFT_TLS_ECDHE_ECDSA_WITH_AES_256_OCB = 65285;

		// Token: 0x04001B49 RID: 6985
		public const int DRAFT_TLS_PSK_WITH_AES_128_OCB = 65296;

		// Token: 0x04001B4A RID: 6986
		public const int DRAFT_TLS_PSK_WITH_AES_256_OCB = 65297;

		// Token: 0x04001B4B RID: 6987
		public const int DRAFT_TLS_DHE_PSK_WITH_AES_128_OCB = 65298;

		// Token: 0x04001B4C RID: 6988
		public const int DRAFT_TLS_DHE_PSK_WITH_AES_256_OCB = 65299;

		// Token: 0x04001B4D RID: 6989
		public const int DRAFT_TLS_ECDHE_PSK_WITH_AES_128_OCB = 65300;

		// Token: 0x04001B4E RID: 6990
		public const int DRAFT_TLS_ECDHE_PSK_WITH_AES_256_OCB = 65301;
	}
}
