using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048E RID: 1166
	public class IsoTrailers
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x001234CC File Offset: 0x001216CC
		private static IDictionary CreateTrailerMap()
		{
			IDictionary dictionary = Platform.CreateHashtable();
			dictionary.Add("RIPEMD128", 13004);
			dictionary.Add("RIPEMD160", 12748);
			dictionary.Add("SHA-1", 13260);
			dictionary.Add("SHA-224", 14540);
			dictionary.Add("SHA-256", 13516);
			dictionary.Add("SHA-384", 14028);
			dictionary.Add("SHA-512", 13772);
			dictionary.Add("SHA-512/224", 14796);
			dictionary.Add("SHA-512/256", 16588);
			dictionary.Add("Whirlpool", 14284);
			return CollectionUtilities.ReadOnly(dictionary);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x001235B5 File Offset: 0x001217B5
		public static int GetTrailer(IDigest digest)
		{
			return (int)IsoTrailers.trailerMap[digest.AlgorithmName];
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x001235CC File Offset: 0x001217CC
		public static bool NoTrailerAvailable(IDigest digest)
		{
			return !IsoTrailers.trailerMap.Contains(digest.AlgorithmName);
		}

		// Token: 0x04001DE4 RID: 7652
		public const int TRAILER_IMPLICIT = 188;

		// Token: 0x04001DE5 RID: 7653
		public const int TRAILER_RIPEMD160 = 12748;

		// Token: 0x04001DE6 RID: 7654
		public const int TRAILER_RIPEMD128 = 13004;

		// Token: 0x04001DE7 RID: 7655
		public const int TRAILER_SHA1 = 13260;

		// Token: 0x04001DE8 RID: 7656
		public const int TRAILER_SHA256 = 13516;

		// Token: 0x04001DE9 RID: 7657
		public const int TRAILER_SHA512 = 13772;

		// Token: 0x04001DEA RID: 7658
		public const int TRAILER_SHA384 = 14028;

		// Token: 0x04001DEB RID: 7659
		public const int TRAILER_WHIRLPOOL = 14284;

		// Token: 0x04001DEC RID: 7660
		public const int TRAILER_SHA224 = 14540;

		// Token: 0x04001DED RID: 7661
		public const int TRAILER_SHA512_224 = 14796;

		// Token: 0x04001DEE RID: 7662
		public const int TRAILER_SHA512_256 = 16588;

		// Token: 0x04001DEF RID: 7663
		private static readonly IDictionary trailerMap = IsoTrailers.CreateTrailerMap();
	}
}
