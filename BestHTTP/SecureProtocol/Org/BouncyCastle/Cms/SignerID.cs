using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000605 RID: 1541
	public class SignerID : X509CertStoreSelector
	{
		// Token: 0x06003A9A RID: 15002 RVA: 0x0016E924 File Offset: 0x0016CB24
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(base.SubjectKeyIdentifier);
			BigInteger serialNumber = base.SerialNumber;
			if (serialNumber != null)
			{
				num ^= serialNumber.GetHashCode();
			}
			X509Name issuer = base.Issuer;
			if (issuer != null)
			{
				num ^= issuer.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0016E964 File Offset: 0x0016CB64
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return false;
			}
			SignerID signerID = obj as SignerID;
			return signerID != null && (Arrays.AreEqual(base.SubjectKeyIdentifier, signerID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, signerID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, signerID.Issuer);
		}
	}
}
