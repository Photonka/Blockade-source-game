using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200066D RID: 1645
	public abstract class X9ObjectIdentifiers
	{
		// Token: 0x040025F4 RID: 9716
		internal const string AnsiX962 = "1.2.840.10045";

		// Token: 0x040025F5 RID: 9717
		public static readonly DerObjectIdentifier ansi_X9_62 = new DerObjectIdentifier("1.2.840.10045");

		// Token: 0x040025F6 RID: 9718
		public static readonly DerObjectIdentifier IdFieldType = X9ObjectIdentifiers.ansi_X9_62.Branch("1");

		// Token: 0x040025F7 RID: 9719
		public static readonly DerObjectIdentifier PrimeField = X9ObjectIdentifiers.IdFieldType.Branch("1");

		// Token: 0x040025F8 RID: 9720
		public static readonly DerObjectIdentifier CharacteristicTwoField = X9ObjectIdentifiers.IdFieldType.Branch("2");

		// Token: 0x040025F9 RID: 9721
		public static readonly DerObjectIdentifier GNBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.1");

		// Token: 0x040025FA RID: 9722
		public static readonly DerObjectIdentifier TPBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.2");

		// Token: 0x040025FB RID: 9723
		public static readonly DerObjectIdentifier PPBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.3");

		// Token: 0x040025FC RID: 9724
		[Obsolete("Use 'id_ecSigType' instead")]
		public const string IdECSigType = "1.2.840.10045.4";

		// Token: 0x040025FD RID: 9725
		public static readonly DerObjectIdentifier id_ecSigType = X9ObjectIdentifiers.ansi_X9_62.Branch("4");

		// Token: 0x040025FE RID: 9726
		public static readonly DerObjectIdentifier ECDsaWithSha1 = X9ObjectIdentifiers.id_ecSigType.Branch("1");

		// Token: 0x040025FF RID: 9727
		[Obsolete("Use 'id_publicKeyType' instead")]
		public const string IdPublicKeyType = "1.2.840.10045.2";

		// Token: 0x04002600 RID: 9728
		public static readonly DerObjectIdentifier id_publicKeyType = X9ObjectIdentifiers.ansi_X9_62.Branch("2");

		// Token: 0x04002601 RID: 9729
		public static readonly DerObjectIdentifier IdECPublicKey = X9ObjectIdentifiers.id_publicKeyType.Branch("1");

		// Token: 0x04002602 RID: 9730
		public static readonly DerObjectIdentifier ECDsaWithSha2 = X9ObjectIdentifiers.id_ecSigType.Branch("3");

		// Token: 0x04002603 RID: 9731
		public static readonly DerObjectIdentifier ECDsaWithSha224 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("1");

		// Token: 0x04002604 RID: 9732
		public static readonly DerObjectIdentifier ECDsaWithSha256 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("2");

		// Token: 0x04002605 RID: 9733
		public static readonly DerObjectIdentifier ECDsaWithSha384 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("3");

		// Token: 0x04002606 RID: 9734
		public static readonly DerObjectIdentifier ECDsaWithSha512 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("4");

		// Token: 0x04002607 RID: 9735
		public static readonly DerObjectIdentifier EllipticCurve = X9ObjectIdentifiers.ansi_X9_62.Branch("3");

		// Token: 0x04002608 RID: 9736
		public static readonly DerObjectIdentifier CTwoCurve = X9ObjectIdentifiers.EllipticCurve.Branch("0");

		// Token: 0x04002609 RID: 9737
		public static readonly DerObjectIdentifier C2Pnb163v1 = X9ObjectIdentifiers.CTwoCurve.Branch("1");

		// Token: 0x0400260A RID: 9738
		public static readonly DerObjectIdentifier C2Pnb163v2 = X9ObjectIdentifiers.CTwoCurve.Branch("2");

		// Token: 0x0400260B RID: 9739
		public static readonly DerObjectIdentifier C2Pnb163v3 = X9ObjectIdentifiers.CTwoCurve.Branch("3");

		// Token: 0x0400260C RID: 9740
		public static readonly DerObjectIdentifier C2Pnb176w1 = X9ObjectIdentifiers.CTwoCurve.Branch("4");

		// Token: 0x0400260D RID: 9741
		public static readonly DerObjectIdentifier C2Tnb191v1 = X9ObjectIdentifiers.CTwoCurve.Branch("5");

		// Token: 0x0400260E RID: 9742
		public static readonly DerObjectIdentifier C2Tnb191v2 = X9ObjectIdentifiers.CTwoCurve.Branch("6");

		// Token: 0x0400260F RID: 9743
		public static readonly DerObjectIdentifier C2Tnb191v3 = X9ObjectIdentifiers.CTwoCurve.Branch("7");

		// Token: 0x04002610 RID: 9744
		public static readonly DerObjectIdentifier C2Onb191v4 = X9ObjectIdentifiers.CTwoCurve.Branch("8");

		// Token: 0x04002611 RID: 9745
		public static readonly DerObjectIdentifier C2Onb191v5 = X9ObjectIdentifiers.CTwoCurve.Branch("9");

		// Token: 0x04002612 RID: 9746
		public static readonly DerObjectIdentifier C2Pnb208w1 = X9ObjectIdentifiers.CTwoCurve.Branch("10");

		// Token: 0x04002613 RID: 9747
		public static readonly DerObjectIdentifier C2Tnb239v1 = X9ObjectIdentifiers.CTwoCurve.Branch("11");

		// Token: 0x04002614 RID: 9748
		public static readonly DerObjectIdentifier C2Tnb239v2 = X9ObjectIdentifiers.CTwoCurve.Branch("12");

		// Token: 0x04002615 RID: 9749
		public static readonly DerObjectIdentifier C2Tnb239v3 = X9ObjectIdentifiers.CTwoCurve.Branch("13");

		// Token: 0x04002616 RID: 9750
		public static readonly DerObjectIdentifier C2Onb239v4 = X9ObjectIdentifiers.CTwoCurve.Branch("14");

		// Token: 0x04002617 RID: 9751
		public static readonly DerObjectIdentifier C2Onb239v5 = X9ObjectIdentifiers.CTwoCurve.Branch("15");

		// Token: 0x04002618 RID: 9752
		public static readonly DerObjectIdentifier C2Pnb272w1 = X9ObjectIdentifiers.CTwoCurve.Branch("16");

		// Token: 0x04002619 RID: 9753
		public static readonly DerObjectIdentifier C2Pnb304w1 = X9ObjectIdentifiers.CTwoCurve.Branch("17");

		// Token: 0x0400261A RID: 9754
		public static readonly DerObjectIdentifier C2Tnb359v1 = X9ObjectIdentifiers.CTwoCurve.Branch("18");

		// Token: 0x0400261B RID: 9755
		public static readonly DerObjectIdentifier C2Pnb368w1 = X9ObjectIdentifiers.CTwoCurve.Branch("19");

		// Token: 0x0400261C RID: 9756
		public static readonly DerObjectIdentifier C2Tnb431r1 = X9ObjectIdentifiers.CTwoCurve.Branch("20");

		// Token: 0x0400261D RID: 9757
		public static readonly DerObjectIdentifier PrimeCurve = X9ObjectIdentifiers.EllipticCurve.Branch("1");

		// Token: 0x0400261E RID: 9758
		public static readonly DerObjectIdentifier Prime192v1 = X9ObjectIdentifiers.PrimeCurve.Branch("1");

		// Token: 0x0400261F RID: 9759
		public static readonly DerObjectIdentifier Prime192v2 = X9ObjectIdentifiers.PrimeCurve.Branch("2");

		// Token: 0x04002620 RID: 9760
		public static readonly DerObjectIdentifier Prime192v3 = X9ObjectIdentifiers.PrimeCurve.Branch("3");

		// Token: 0x04002621 RID: 9761
		public static readonly DerObjectIdentifier Prime239v1 = X9ObjectIdentifiers.PrimeCurve.Branch("4");

		// Token: 0x04002622 RID: 9762
		public static readonly DerObjectIdentifier Prime239v2 = X9ObjectIdentifiers.PrimeCurve.Branch("5");

		// Token: 0x04002623 RID: 9763
		public static readonly DerObjectIdentifier Prime239v3 = X9ObjectIdentifiers.PrimeCurve.Branch("6");

		// Token: 0x04002624 RID: 9764
		public static readonly DerObjectIdentifier Prime256v1 = X9ObjectIdentifiers.PrimeCurve.Branch("7");

		// Token: 0x04002625 RID: 9765
		public static readonly DerObjectIdentifier IdDsa = new DerObjectIdentifier("1.2.840.10040.4.1");

		// Token: 0x04002626 RID: 9766
		public static readonly DerObjectIdentifier IdDsaWithSha1 = new DerObjectIdentifier("1.2.840.10040.4.3");

		// Token: 0x04002627 RID: 9767
		public static readonly DerObjectIdentifier X9x63Scheme = new DerObjectIdentifier("1.3.133.16.840.63.0");

		// Token: 0x04002628 RID: 9768
		public static readonly DerObjectIdentifier DHSinglePassStdDHSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("2");

		// Token: 0x04002629 RID: 9769
		public static readonly DerObjectIdentifier DHSinglePassCofactorDHSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("3");

		// Token: 0x0400262A RID: 9770
		public static readonly DerObjectIdentifier MqvSinglePassSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("16");

		// Token: 0x0400262B RID: 9771
		public static readonly DerObjectIdentifier ansi_x9_42 = new DerObjectIdentifier("1.2.840.10046");

		// Token: 0x0400262C RID: 9772
		public static readonly DerObjectIdentifier DHPublicNumber = X9ObjectIdentifiers.ansi_x9_42.Branch("2.1");

		// Token: 0x0400262D RID: 9773
		public static readonly DerObjectIdentifier X9x42Schemes = X9ObjectIdentifiers.ansi_x9_42.Branch("2.3");

		// Token: 0x0400262E RID: 9774
		public static readonly DerObjectIdentifier DHStatic = X9ObjectIdentifiers.X9x42Schemes.Branch("1");

		// Token: 0x0400262F RID: 9775
		public static readonly DerObjectIdentifier DHEphem = X9ObjectIdentifiers.X9x42Schemes.Branch("2");

		// Token: 0x04002630 RID: 9776
		public static readonly DerObjectIdentifier DHOneFlow = X9ObjectIdentifiers.X9x42Schemes.Branch("3");

		// Token: 0x04002631 RID: 9777
		public static readonly DerObjectIdentifier DHHybrid1 = X9ObjectIdentifiers.X9x42Schemes.Branch("4");

		// Token: 0x04002632 RID: 9778
		public static readonly DerObjectIdentifier DHHybrid2 = X9ObjectIdentifiers.X9x42Schemes.Branch("5");

		// Token: 0x04002633 RID: 9779
		public static readonly DerObjectIdentifier DHHybridOneFlow = X9ObjectIdentifiers.X9x42Schemes.Branch("6");

		// Token: 0x04002634 RID: 9780
		public static readonly DerObjectIdentifier Mqv2 = X9ObjectIdentifiers.X9x42Schemes.Branch("7");

		// Token: 0x04002635 RID: 9781
		public static readonly DerObjectIdentifier Mqv1 = X9ObjectIdentifiers.X9x42Schemes.Branch("8");
	}
}
