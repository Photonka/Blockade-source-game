using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073A RID: 1850
	public class OtherHash : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004311 RID: 17169 RVA: 0x0018BFB7 File Offset: 0x0018A1B7
		public static OtherHash GetInstance(object obj)
		{
			if (obj == null || obj is OtherHash)
			{
				return (OtherHash)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new OtherHash((Asn1OctetString)obj);
			}
			return new OtherHash(OtherHashAlgAndValue.GetInstance(obj));
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x0018BFEA File Offset: 0x0018A1EA
		public OtherHash(byte[] sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = new DerOctetString(sha1Hash);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x0018C00C File Offset: 0x0018A20C
		public OtherHash(Asn1OctetString sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = sha1Hash;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x0018C029 File Offset: 0x0018A229
		public OtherHash(OtherHashAlgAndValue otherHash)
		{
			if (otherHash == null)
			{
				throw new ArgumentNullException("otherHash");
			}
			this.otherHash = otherHash;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x0018C046 File Offset: 0x0018A246
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				if (this.otherHash != null)
				{
					return this.otherHash.HashAlgorithm;
				}
				return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
			}
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x0018C066 File Offset: 0x0018A266
		public byte[] GetHashValue()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.GetHashValue();
			}
			return this.sha1Hash.GetOctets();
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x0018C087 File Offset: 0x0018A287
		public override Asn1Object ToAsn1Object()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.ToAsn1Object();
			}
			return this.sha1Hash;
		}

		// Token: 0x04002B0A RID: 11018
		private readonly Asn1OctetString sha1Hash;

		// Token: 0x04002B0B RID: 11019
		private readonly OtherHashAlgAndValue otherHash;
	}
}
