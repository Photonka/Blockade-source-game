using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.GM;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Rosstandart;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000295 RID: 661
	public abstract class TspAlgorithms
	{
		// Token: 0x06001880 RID: 6272 RVA: 0x000BC94C File Offset: 0x000BAB4C
		static TspAlgorithms()
		{
			string[] array = new string[]
			{
				TspAlgorithms.Gost3411,
				TspAlgorithms.Gost3411_2012_256,
				TspAlgorithms.Gost3411_2012_512,
				TspAlgorithms.MD5,
				TspAlgorithms.RipeMD128,
				TspAlgorithms.RipeMD160,
				TspAlgorithms.RipeMD256,
				TspAlgorithms.Sha1,
				TspAlgorithms.Sha224,
				TspAlgorithms.Sha256,
				TspAlgorithms.Sha384,
				TspAlgorithms.Sha512,
				TspAlgorithms.SM3
			};
			TspAlgorithms.Allowed = Platform.CreateArrayList();
			foreach (string value in array)
			{
				TspAlgorithms.Allowed.Add(value);
			}
		}

		// Token: 0x0400171C RID: 5916
		public static readonly string MD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x0400171D RID: 5917
		public static readonly string Sha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x0400171E RID: 5918
		public static readonly string Sha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x0400171F RID: 5919
		public static readonly string Sha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x04001720 RID: 5920
		public static readonly string Sha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x04001721 RID: 5921
		public static readonly string Sha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x04001722 RID: 5922
		public static readonly string RipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x04001723 RID: 5923
		public static readonly string RipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x04001724 RID: 5924
		public static readonly string RipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x04001725 RID: 5925
		public static readonly string Gost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x04001726 RID: 5926
		public static readonly string Gost3411_2012_256 = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id;

		// Token: 0x04001727 RID: 5927
		public static readonly string Gost3411_2012_512 = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id;

		// Token: 0x04001728 RID: 5928
		public static readonly string SM3 = GMObjectIdentifiers.sm3.Id;

		// Token: 0x04001729 RID: 5929
		public static readonly IList Allowed;
	}
}
