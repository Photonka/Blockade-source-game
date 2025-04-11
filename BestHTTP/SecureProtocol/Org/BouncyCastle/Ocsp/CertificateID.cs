using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002DF RID: 735
	public class CertificateID
	{
		// Token: 0x06001B28 RID: 6952 RVA: 0x000D2024 File Offset: 0x000D0224
		public CertificateID(CertID id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000D2044 File Offset: 0x000D0244
		public CertificateID(string hashAlgorithm, X509Certificate issuerCert, BigInteger serialNumber)
		{
			AlgorithmIdentifier hashAlg = new AlgorithmIdentifier(new DerObjectIdentifier(hashAlgorithm), DerNull.Instance);
			this.id = CertificateID.CreateCertID(hashAlg, issuerCert, new DerInteger(serialNumber));
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000D207B File Offset: 0x000D027B
		public string HashAlgOid
		{
			get
			{
				return this.id.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000D2092 File Offset: 0x000D0292
		public byte[] GetIssuerNameHash()
		{
			return this.id.IssuerNameHash.GetOctets();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000D20A4 File Offset: 0x000D02A4
		public byte[] GetIssuerKeyHash()
		{
			return this.id.IssuerKeyHash.GetOctets();
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000D20B6 File Offset: 0x000D02B6
		public BigInteger SerialNumber
		{
			get
			{
				return this.id.SerialNumber.Value;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000D20C8 File Offset: 0x000D02C8
		public bool MatchesIssuer(X509Certificate issuerCert)
		{
			return CertificateID.CreateCertID(this.id.HashAlgorithm, issuerCert, this.id.SerialNumber).Equals(this.id);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000D20F1 File Offset: 0x000D02F1
		public CertID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000D20FC File Offset: 0x000D02FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			CertificateID certificateID = obj as CertificateID;
			return certificateID != null && this.id.ToAsn1Object().Equals(certificateID.id.ToAsn1Object());
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000D2136 File Offset: 0x000D0336
		public override int GetHashCode()
		{
			return this.id.ToAsn1Object().GetHashCode();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000D2148 File Offset: 0x000D0348
		public static CertificateID DeriveCertificateID(CertificateID original, BigInteger newSerialNumber)
		{
			return new CertificateID(new CertID(original.id.HashAlgorithm, original.id.IssuerNameHash, original.id.IssuerKeyHash, new DerInteger(newSerialNumber)));
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000D217C File Offset: 0x000D037C
		private static CertID CreateCertID(AlgorithmIdentifier hashAlg, X509Certificate issuerCert, DerInteger serialNumber)
		{
			CertID result;
			try
			{
				string algorithm = hashAlg.Algorithm.Id;
				X509Name subjectX509Principal = PrincipalUtilities.GetSubjectX509Principal(issuerCert);
				byte[] str = DigestUtilities.CalculateDigest(algorithm, subjectX509Principal.GetEncoded());
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(issuerCert.GetPublicKey());
				byte[] str2 = DigestUtilities.CalculateDigest(algorithm, subjectPublicKeyInfo.PublicKeyData.GetBytes());
				result = new CertID(hashAlg, new DerOctetString(str), new DerOctetString(str2), serialNumber);
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
			return result;
		}

		// Token: 0x040017BE RID: 6078
		public const string HashSha1 = "1.3.14.3.2.26";

		// Token: 0x040017BF RID: 6079
		private readonly CertID id;
	}
}
