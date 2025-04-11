using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000601 RID: 1537
	public class RecipientID : X509CertStoreSelector
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x0016E5CB File Offset: 0x0016C7CB
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x0016E5D8 File Offset: 0x0016C7D8
		public byte[] KeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.keyIdentifier);
			}
			set
			{
				this.keyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x0016E5E8 File Offset: 0x0016C7E8
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(this.keyIdentifier) ^ Arrays.GetHashCode(base.SubjectKeyIdentifier);
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

		// Token: 0x06003A87 RID: 14983 RVA: 0x0016E634 File Offset: 0x0016C834
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecipientID recipientID = obj as RecipientID;
			return recipientID != null && (Arrays.AreEqual(this.keyIdentifier, recipientID.keyIdentifier) && Arrays.AreEqual(base.SubjectKeyIdentifier, recipientID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, recipientID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, recipientID.Issuer);
		}

		// Token: 0x04002537 RID: 9527
		private byte[] keyIdentifier;
	}
}
