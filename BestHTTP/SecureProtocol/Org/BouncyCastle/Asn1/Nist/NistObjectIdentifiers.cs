using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x02000704 RID: 1796
	public sealed class NistObjectIdentifiers
	{
		// Token: 0x060041DB RID: 16859 RVA: 0x00023EF4 File Offset: 0x000220F4
		private NistObjectIdentifiers()
		{
		}

		// Token: 0x040029AA RID: 10666
		public static readonly DerObjectIdentifier NistAlgorithm = new DerObjectIdentifier("2.16.840.1.101.3.4");

		// Token: 0x040029AB RID: 10667
		public static readonly DerObjectIdentifier HashAlgs = NistObjectIdentifiers.NistAlgorithm.Branch("2");

		// Token: 0x040029AC RID: 10668
		public static readonly DerObjectIdentifier IdSha256 = NistObjectIdentifiers.HashAlgs.Branch("1");

		// Token: 0x040029AD RID: 10669
		public static readonly DerObjectIdentifier IdSha384 = NistObjectIdentifiers.HashAlgs.Branch("2");

		// Token: 0x040029AE RID: 10670
		public static readonly DerObjectIdentifier IdSha512 = NistObjectIdentifiers.HashAlgs.Branch("3");

		// Token: 0x040029AF RID: 10671
		public static readonly DerObjectIdentifier IdSha224 = NistObjectIdentifiers.HashAlgs.Branch("4");

		// Token: 0x040029B0 RID: 10672
		public static readonly DerObjectIdentifier IdSha512_224 = NistObjectIdentifiers.HashAlgs.Branch("5");

		// Token: 0x040029B1 RID: 10673
		public static readonly DerObjectIdentifier IdSha512_256 = NistObjectIdentifiers.HashAlgs.Branch("6");

		// Token: 0x040029B2 RID: 10674
		public static readonly DerObjectIdentifier IdSha3_224 = NistObjectIdentifiers.HashAlgs.Branch("7");

		// Token: 0x040029B3 RID: 10675
		public static readonly DerObjectIdentifier IdSha3_256 = NistObjectIdentifiers.HashAlgs.Branch("8");

		// Token: 0x040029B4 RID: 10676
		public static readonly DerObjectIdentifier IdSha3_384 = NistObjectIdentifiers.HashAlgs.Branch("9");

		// Token: 0x040029B5 RID: 10677
		public static readonly DerObjectIdentifier IdSha3_512 = NistObjectIdentifiers.HashAlgs.Branch("10");

		// Token: 0x040029B6 RID: 10678
		public static readonly DerObjectIdentifier IdShake128 = NistObjectIdentifiers.HashAlgs.Branch("11");

		// Token: 0x040029B7 RID: 10679
		public static readonly DerObjectIdentifier IdShake256 = NistObjectIdentifiers.HashAlgs.Branch("12");

		// Token: 0x040029B8 RID: 10680
		public static readonly DerObjectIdentifier IdHMacWithSha3_224 = NistObjectIdentifiers.HashAlgs.Branch("13");

		// Token: 0x040029B9 RID: 10681
		public static readonly DerObjectIdentifier IdHMacWithSha3_256 = NistObjectIdentifiers.HashAlgs.Branch("14");

		// Token: 0x040029BA RID: 10682
		public static readonly DerObjectIdentifier IdHMacWithSha3_384 = NistObjectIdentifiers.HashAlgs.Branch("15");

		// Token: 0x040029BB RID: 10683
		public static readonly DerObjectIdentifier IdHMacWithSha3_512 = NistObjectIdentifiers.HashAlgs.Branch("16");

		// Token: 0x040029BC RID: 10684
		public static readonly DerObjectIdentifier Aes = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".1");

		// Token: 0x040029BD RID: 10685
		public static readonly DerObjectIdentifier IdAes128Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".1");

		// Token: 0x040029BE RID: 10686
		public static readonly DerObjectIdentifier IdAes128Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".2");

		// Token: 0x040029BF RID: 10687
		public static readonly DerObjectIdentifier IdAes128Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".3");

		// Token: 0x040029C0 RID: 10688
		public static readonly DerObjectIdentifier IdAes128Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".4");

		// Token: 0x040029C1 RID: 10689
		public static readonly DerObjectIdentifier IdAes128Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".5");

		// Token: 0x040029C2 RID: 10690
		public static readonly DerObjectIdentifier IdAes128Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".6");

		// Token: 0x040029C3 RID: 10691
		public static readonly DerObjectIdentifier IdAes128Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".7");

		// Token: 0x040029C4 RID: 10692
		public static readonly DerObjectIdentifier IdAes192Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".21");

		// Token: 0x040029C5 RID: 10693
		public static readonly DerObjectIdentifier IdAes192Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".22");

		// Token: 0x040029C6 RID: 10694
		public static readonly DerObjectIdentifier IdAes192Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".23");

		// Token: 0x040029C7 RID: 10695
		public static readonly DerObjectIdentifier IdAes192Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".24");

		// Token: 0x040029C8 RID: 10696
		public static readonly DerObjectIdentifier IdAes192Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".25");

		// Token: 0x040029C9 RID: 10697
		public static readonly DerObjectIdentifier IdAes192Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".26");

		// Token: 0x040029CA RID: 10698
		public static readonly DerObjectIdentifier IdAes192Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".27");

		// Token: 0x040029CB RID: 10699
		public static readonly DerObjectIdentifier IdAes256Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".41");

		// Token: 0x040029CC RID: 10700
		public static readonly DerObjectIdentifier IdAes256Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".42");

		// Token: 0x040029CD RID: 10701
		public static readonly DerObjectIdentifier IdAes256Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".43");

		// Token: 0x040029CE RID: 10702
		public static readonly DerObjectIdentifier IdAes256Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".44");

		// Token: 0x040029CF RID: 10703
		public static readonly DerObjectIdentifier IdAes256Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".45");

		// Token: 0x040029D0 RID: 10704
		public static readonly DerObjectIdentifier IdAes256Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".46");

		// Token: 0x040029D1 RID: 10705
		public static readonly DerObjectIdentifier IdAes256Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".47");

		// Token: 0x040029D2 RID: 10706
		public static readonly DerObjectIdentifier IdDsaWithSha2 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".3");

		// Token: 0x040029D3 RID: 10707
		public static readonly DerObjectIdentifier DsaWithSha224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".1");

		// Token: 0x040029D4 RID: 10708
		public static readonly DerObjectIdentifier DsaWithSha256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".2");

		// Token: 0x040029D5 RID: 10709
		public static readonly DerObjectIdentifier DsaWithSha384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".3");

		// Token: 0x040029D6 RID: 10710
		public static readonly DerObjectIdentifier DsaWithSha512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".4");
	}
}
