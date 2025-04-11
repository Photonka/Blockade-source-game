using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000735 RID: 1845
	public abstract class EsfAttributes
	{
		// Token: 0x04002AF5 RID: 10997
		public static readonly DerObjectIdentifier SigPolicyId = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x04002AF6 RID: 10998
		public static readonly DerObjectIdentifier CommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x04002AF7 RID: 10999
		public static readonly DerObjectIdentifier SignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x04002AF8 RID: 11000
		public static readonly DerObjectIdentifier SignerAttr = PkcsObjectIdentifiers.IdAAEtsSignerAttr;

		// Token: 0x04002AF9 RID: 11001
		public static readonly DerObjectIdentifier OtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x04002AFA RID: 11002
		public static readonly DerObjectIdentifier ContentTimestamp = PkcsObjectIdentifiers.IdAAEtsContentTimestamp;

		// Token: 0x04002AFB RID: 11003
		public static readonly DerObjectIdentifier CertificateRefs = PkcsObjectIdentifiers.IdAAEtsCertificateRefs;

		// Token: 0x04002AFC RID: 11004
		public static readonly DerObjectIdentifier RevocationRefs = PkcsObjectIdentifiers.IdAAEtsRevocationRefs;

		// Token: 0x04002AFD RID: 11005
		public static readonly DerObjectIdentifier CertValues = PkcsObjectIdentifiers.IdAAEtsCertValues;

		// Token: 0x04002AFE RID: 11006
		public static readonly DerObjectIdentifier RevocationValues = PkcsObjectIdentifiers.IdAAEtsRevocationValues;

		// Token: 0x04002AFF RID: 11007
		public static readonly DerObjectIdentifier EscTimeStamp = PkcsObjectIdentifiers.IdAAEtsEscTimeStamp;

		// Token: 0x04002B00 RID: 11008
		public static readonly DerObjectIdentifier CertCrlTimestamp = PkcsObjectIdentifiers.IdAAEtsCertCrlTimestamp;

		// Token: 0x04002B01 RID: 11009
		public static readonly DerObjectIdentifier ArchiveTimestamp = PkcsObjectIdentifiers.IdAAEtsArchiveTimestamp;

		// Token: 0x04002B02 RID: 11010
		public static readonly DerObjectIdentifier ArchiveTimestampV2 = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.48");
	}
}
