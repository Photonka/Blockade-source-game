using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt
{
	// Token: 0x0200070E RID: 1806
	public abstract class IsisMttObjectIdentifiers
	{
		// Token: 0x04002A13 RID: 10771
		public static readonly DerObjectIdentifier IdIsisMtt = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x04002A14 RID: 10772
		public static readonly DerObjectIdentifier IdIsisMttCP = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".1");

		// Token: 0x04002A15 RID: 10773
		public static readonly DerObjectIdentifier IdIsisMttCPAccredited = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttCP + ".1");

		// Token: 0x04002A16 RID: 10774
		public static readonly DerObjectIdentifier IdIsisMttAT = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".3");

		// Token: 0x04002A17 RID: 10775
		public static readonly DerObjectIdentifier IdIsisMttATDateOfCertGen = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".1");

		// Token: 0x04002A18 RID: 10776
		public static readonly DerObjectIdentifier IdIsisMttATProcuration = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".2");

		// Token: 0x04002A19 RID: 10777
		public static readonly DerObjectIdentifier IdIsisMttATAdmission = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".3");

		// Token: 0x04002A1A RID: 10778
		public static readonly DerObjectIdentifier IdIsisMttATMonetaryLimit = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".4");

		// Token: 0x04002A1B RID: 10779
		public static readonly DerObjectIdentifier IdIsisMttATDeclarationOfMajority = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".5");

		// Token: 0x04002A1C RID: 10780
		public static readonly DerObjectIdentifier IdIsisMttATIccsn = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".6");

		// Token: 0x04002A1D RID: 10781
		public static readonly DerObjectIdentifier IdIsisMttATPKReference = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".7");

		// Token: 0x04002A1E RID: 10782
		public static readonly DerObjectIdentifier IdIsisMttATRestriction = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".8");

		// Token: 0x04002A1F RID: 10783
		public static readonly DerObjectIdentifier IdIsisMttATRetrieveIfAllowed = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".9");

		// Token: 0x04002A20 RID: 10784
		public static readonly DerObjectIdentifier IdIsisMttATRequestedCertificate = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".10");

		// Token: 0x04002A21 RID: 10785
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthorities = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".11");

		// Token: 0x04002A22 RID: 10786
		public static readonly DerObjectIdentifier IdIsisMttATCertInDirSince = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".12");

		// Token: 0x04002A23 RID: 10787
		public static readonly DerObjectIdentifier IdIsisMttATCertHash = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".13");

		// Token: 0x04002A24 RID: 10788
		public static readonly DerObjectIdentifier IdIsisMttATNameAtBirth = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".14");

		// Token: 0x04002A25 RID: 10789
		public static readonly DerObjectIdentifier IdIsisMttATAdditionalInformation = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".15");

		// Token: 0x04002A26 RID: 10790
		public static readonly DerObjectIdentifier IdIsisMttATLiabilityLimitationFlag = new DerObjectIdentifier("0.2.262.1.10.12.0");
	}
}
