using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069B RID: 1691
	public class SubjectKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x0017B165 File Offset: 0x00179365
		public static SubjectKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectKeyIdentifier.GetInstance(Asn1OctetString.GetInstance(obj, explicitly));
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0017B174 File Offset: 0x00179374
		public static SubjectKeyIdentifier GetInstance(object obj)
		{
			if (obj is SubjectKeyIdentifier)
			{
				return (SubjectKeyIdentifier)obj;
			}
			if (obj is SubjectPublicKeyInfo)
			{
				return new SubjectKeyIdentifier((SubjectPublicKeyInfo)obj);
			}
			if (obj is Asn1OctetString)
			{
				return new SubjectKeyIdentifier((Asn1OctetString)obj);
			}
			if (obj is X509Extension)
			{
				return SubjectKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("Invalid SubjectKeyIdentifier: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0017B1E6 File Offset: 0x001793E6
		public SubjectKeyIdentifier(byte[] keyID)
		{
			if (keyID == null)
			{
				throw new ArgumentNullException("keyID");
			}
			this.keyIdentifier = keyID;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x0017B203 File Offset: 0x00179403
		public SubjectKeyIdentifier(Asn1OctetString keyID)
		{
			this.keyIdentifier = keyID.GetOctets();
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x0017B217 File Offset: 0x00179417
		public SubjectKeyIdentifier(SubjectPublicKeyInfo spki)
		{
			this.keyIdentifier = SubjectKeyIdentifier.GetDigest(spki);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x0017B22B File Offset: 0x0017942B
		public byte[] GetKeyIdentifier()
		{
			return this.keyIdentifier;
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0017B233 File Offset: 0x00179433
		public override Asn1Object ToAsn1Object()
		{
			return new DerOctetString(this.keyIdentifier);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0017B240 File Offset: 0x00179440
		public static SubjectKeyIdentifier CreateSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			return new SubjectKeyIdentifier(keyInfo);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0017B248 File Offset: 0x00179448
		public static SubjectKeyIdentifier CreateTruncatedSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			byte[] digest = SubjectKeyIdentifier.GetDigest(keyInfo);
			byte[] array = new byte[8];
			Array.Copy(digest, digest.Length - 8, array, 0, array.Length);
			byte[] array2 = array;
			int num = 0;
			array2[num] &= 15;
			byte[] array3 = array;
			int num2 = 0;
			array3[num2] |= 64;
			return new SubjectKeyIdentifier(array);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0017B298 File Offset: 0x00179498
		private static byte[] GetDigest(SubjectPublicKeyInfo spki)
		{
			Sha1Digest sha1Digest = new Sha1Digest();
			byte[] array = new byte[((IDigest)sha1Digest).GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			((IDigest)sha1Digest).BlockUpdate(bytes, 0, bytes.Length);
			((IDigest)sha1Digest).DoFinal(array, 0);
			return array;
		}

		// Token: 0x040026D8 RID: 9944
		private readonly byte[] keyIdentifier;
	}
}
