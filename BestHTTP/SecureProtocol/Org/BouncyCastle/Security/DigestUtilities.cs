using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.GM;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Rosstandart;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.UA;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C5 RID: 709
	public sealed class DigestUtilities
	{
		// Token: 0x06001A47 RID: 6727 RVA: 0x00023EF4 File Offset: 0x000220F4
		private DigestUtilities()
		{
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000CA8A0 File Offset: 0x000C8AA0
		static DigestUtilities()
		{
			((DigestUtilities.DigestAlgorithm)Enums.GetArbitraryValue(typeof(DigestUtilities.DigestAlgorithm))).ToString();
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD2.Id] = "MD2";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD4.Id] = "MD4";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD5.Id] = "MD5";
			DigestUtilities.algorithms["SHA1"] = "SHA-1";
			DigestUtilities.algorithms[OiwObjectIdentifiers.IdSha1.Id] = "SHA-1";
			DigestUtilities.algorithms["SHA224"] = "SHA-224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha224.Id] = "SHA-224";
			DigestUtilities.algorithms["SHA256"] = "SHA-256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha256.Id] = "SHA-256";
			DigestUtilities.algorithms["SHA384"] = "SHA-384";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha384.Id] = "SHA-384";
			DigestUtilities.algorithms["SHA512"] = "SHA-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512.Id] = "SHA-512";
			DigestUtilities.algorithms["SHA512/224"] = "SHA-512/224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512_224.Id] = "SHA-512/224";
			DigestUtilities.algorithms["SHA512/256"] = "SHA-512/256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512_256.Id] = "SHA-512/256";
			DigestUtilities.algorithms["RIPEMD-128"] = "RIPEMD128";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD128.Id] = "RIPEMD128";
			DigestUtilities.algorithms["RIPEMD-160"] = "RIPEMD160";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD160.Id] = "RIPEMD160";
			DigestUtilities.algorithms["RIPEMD-256"] = "RIPEMD256";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD256.Id] = "RIPEMD256";
			DigestUtilities.algorithms["RIPEMD-320"] = "RIPEMD320";
			DigestUtilities.algorithms[CryptoProObjectIdentifiers.GostR3411.Id] = "GOST3411";
			DigestUtilities.algorithms["KECCAK224"] = "KECCAK-224";
			DigestUtilities.algorithms["KECCAK256"] = "KECCAK-256";
			DigestUtilities.algorithms["KECCAK288"] = "KECCAK-288";
			DigestUtilities.algorithms["KECCAK384"] = "KECCAK-384";
			DigestUtilities.algorithms["KECCAK512"] = "KECCAK-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_224.Id] = "SHA3-224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_256.Id] = "SHA3-256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_384.Id] = "SHA3-384";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_512.Id] = "SHA3-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdShake128.Id] = "SHAKE128";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdShake256.Id] = "SHAKE256";
			DigestUtilities.algorithms[GMObjectIdentifiers.sm3.Id] = "SM3";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b160.Id] = "BLAKE2B-160";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b256.Id] = "BLAKE2B-256";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b384.Id] = "BLAKE2B-384";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b512.Id] = "BLAKE2B-512";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s128.Id] = "BLAKE2S-128";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s160.Id] = "BLAKE2S-160";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s224.Id] = "BLAKE2S-224";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s256.Id] = "BLAKE2S-256";
			DigestUtilities.algorithms[RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id] = "GOST3411-2012-256";
			DigestUtilities.algorithms[RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id] = "GOST3411-2012-512";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_256.Id] = "DSTU7564-256";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_384.Id] = "DSTU7564-384";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_512.Id] = "DSTU7564-512";
			DigestUtilities.oids["MD2"] = PkcsObjectIdentifiers.MD2;
			DigestUtilities.oids["MD4"] = PkcsObjectIdentifiers.MD4;
			DigestUtilities.oids["MD5"] = PkcsObjectIdentifiers.MD5;
			DigestUtilities.oids["SHA-1"] = OiwObjectIdentifiers.IdSha1;
			DigestUtilities.oids["SHA-224"] = NistObjectIdentifiers.IdSha224;
			DigestUtilities.oids["SHA-256"] = NistObjectIdentifiers.IdSha256;
			DigestUtilities.oids["SHA-384"] = NistObjectIdentifiers.IdSha384;
			DigestUtilities.oids["SHA-512"] = NistObjectIdentifiers.IdSha512;
			DigestUtilities.oids["SHA-512/224"] = NistObjectIdentifiers.IdSha512_224;
			DigestUtilities.oids["SHA-512/256"] = NistObjectIdentifiers.IdSha512_256;
			DigestUtilities.oids["SHA3-224"] = NistObjectIdentifiers.IdSha3_224;
			DigestUtilities.oids["SHA3-256"] = NistObjectIdentifiers.IdSha3_256;
			DigestUtilities.oids["SHA3-384"] = NistObjectIdentifiers.IdSha3_384;
			DigestUtilities.oids["SHA3-512"] = NistObjectIdentifiers.IdSha3_512;
			DigestUtilities.oids["SHAKE128"] = NistObjectIdentifiers.IdShake128;
			DigestUtilities.oids["SHAKE256"] = NistObjectIdentifiers.IdShake256;
			DigestUtilities.oids["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			DigestUtilities.oids["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			DigestUtilities.oids["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			DigestUtilities.oids["GOST3411"] = CryptoProObjectIdentifiers.GostR3411;
			DigestUtilities.oids["SM3"] = GMObjectIdentifiers.sm3;
			DigestUtilities.oids["BLAKE2B-160"] = MiscObjectIdentifiers.id_blake2b160;
			DigestUtilities.oids["BLAKE2B-256"] = MiscObjectIdentifiers.id_blake2b256;
			DigestUtilities.oids["BLAKE2B-384"] = MiscObjectIdentifiers.id_blake2b384;
			DigestUtilities.oids["BLAKE2B-512"] = MiscObjectIdentifiers.id_blake2b512;
			DigestUtilities.oids["BLAKE2S-128"] = MiscObjectIdentifiers.id_blake2s128;
			DigestUtilities.oids["BLAKE2S-160"] = MiscObjectIdentifiers.id_blake2s160;
			DigestUtilities.oids["BLAKE2S-224"] = MiscObjectIdentifiers.id_blake2s224;
			DigestUtilities.oids["BLAKE2S-256"] = MiscObjectIdentifiers.id_blake2s256;
			DigestUtilities.oids["GOST3411-2012-256"] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256;
			DigestUtilities.oids["GOST3411-2012-512"] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512;
			DigestUtilities.oids["DSTU7564-256"] = UAObjectIdentifiers.dstu7564digest_256;
			DigestUtilities.oids["DSTU7564-384"] = UAObjectIdentifiers.dstu7564digest_384;
			DigestUtilities.oids["DSTU7564-512"] = UAObjectIdentifiers.dstu7564digest_512;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000CB020 File Offset: 0x000C9220
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = Platform.ToUpperInvariant(mechanism);
			string text = (string)DigestUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)DigestUtilities.oids[mechanism];
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x000CB06A File Offset: 0x000C926A
		public static ICollection Algorithms
		{
			get
			{
				return DigestUtilities.oids.Keys;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000CB076 File Offset: 0x000C9276
		public static IDigest GetDigest(DerObjectIdentifier id)
		{
			return DigestUtilities.GetDigest(id.Id);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000CB084 File Offset: 0x000C9284
		public static IDigest GetDigest(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)DigestUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			try
			{
				switch ((DigestUtilities.DigestAlgorithm)Enums.GetEnumValue(typeof(DigestUtilities.DigestAlgorithm), text2))
				{
				case DigestUtilities.DigestAlgorithm.BLAKE2B_160:
					return new Blake2bDigest(160);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_256:
					return new Blake2bDigest(256);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_384:
					return new Blake2bDigest(384);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_512:
					return new Blake2bDigest(512);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_128:
					return new Blake2sDigest(128);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_160:
					return new Blake2sDigest(160);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_224:
					return new Blake2sDigest(224);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_256:
					return new Blake2sDigest(256);
				case DigestUtilities.DigestAlgorithm.DSTU7564_256:
					return new Dstu7564Digest(256);
				case DigestUtilities.DigestAlgorithm.DSTU7564_384:
					return new Dstu7564Digest(384);
				case DigestUtilities.DigestAlgorithm.DSTU7564_512:
					return new Dstu7564Digest(512);
				case DigestUtilities.DigestAlgorithm.GOST3411:
					return new Gost3411Digest();
				case DigestUtilities.DigestAlgorithm.GOST3411_2012_256:
					return new GOST3411_2012_256Digest();
				case DigestUtilities.DigestAlgorithm.GOST3411_2012_512:
					return new GOST3411_2012_512Digest();
				case DigestUtilities.DigestAlgorithm.KECCAK_224:
					return new KeccakDigest(224);
				case DigestUtilities.DigestAlgorithm.KECCAK_256:
					return new KeccakDigest(256);
				case DigestUtilities.DigestAlgorithm.KECCAK_288:
					return new KeccakDigest(288);
				case DigestUtilities.DigestAlgorithm.KECCAK_384:
					return new KeccakDigest(384);
				case DigestUtilities.DigestAlgorithm.KECCAK_512:
					return new KeccakDigest(512);
				case DigestUtilities.DigestAlgorithm.MD2:
					return new MD2Digest();
				case DigestUtilities.DigestAlgorithm.MD4:
					return new MD4Digest();
				case DigestUtilities.DigestAlgorithm.MD5:
					return new MD5Digest();
				case DigestUtilities.DigestAlgorithm.NONE:
					return new NullDigest();
				case DigestUtilities.DigestAlgorithm.RIPEMD128:
					return new RipeMD128Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD160:
					return new RipeMD160Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD256:
					return new RipeMD256Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD320:
					return new RipeMD320Digest();
				case DigestUtilities.DigestAlgorithm.SHA_1:
					return new Sha1Digest();
				case DigestUtilities.DigestAlgorithm.SHA_224:
					return new Sha224Digest();
				case DigestUtilities.DigestAlgorithm.SHA_256:
					return new Sha256Digest();
				case DigestUtilities.DigestAlgorithm.SHA_384:
					return new Sha384Digest();
				case DigestUtilities.DigestAlgorithm.SHA_512:
					return new Sha512Digest();
				case DigestUtilities.DigestAlgorithm.SHA_512_224:
					return new Sha512tDigest(224);
				case DigestUtilities.DigestAlgorithm.SHA_512_256:
					return new Sha512tDigest(256);
				case DigestUtilities.DigestAlgorithm.SHA3_224:
					return new Sha3Digest(224);
				case DigestUtilities.DigestAlgorithm.SHA3_256:
					return new Sha3Digest(256);
				case DigestUtilities.DigestAlgorithm.SHA3_384:
					return new Sha3Digest(384);
				case DigestUtilities.DigestAlgorithm.SHA3_512:
					return new Sha3Digest(512);
				case DigestUtilities.DigestAlgorithm.SHAKE128:
					return new ShakeDigest(128);
				case DigestUtilities.DigestAlgorithm.SHAKE256:
					return new ShakeDigest(256);
				case DigestUtilities.DigestAlgorithm.SM3:
					return new SM3Digest();
				case DigestUtilities.DigestAlgorithm.TIGER:
					return new TigerDigest();
				case DigestUtilities.DigestAlgorithm.WHIRLPOOL:
					return new WhirlpoolDigest();
				}
			}
			catch (ArgumentException)
			{
			}
			throw new SecurityUtilityException("Digest " + text2 + " not recognised.");
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000CB3EC File Offset: 0x000C95EC
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)DigestUtilities.algorithms[oid.Id];
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000CB403 File Offset: 0x000C9603
		public static byte[] CalculateDigest(string algorithm, byte[] input)
		{
			IDigest digest = DigestUtilities.GetDigest(algorithm);
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000CB41C File Offset: 0x000C961C
		public static byte[] DoFinal(IDigest digest)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000CB43F File Offset: 0x000C963F
		public static byte[] DoFinal(IDigest digest, byte[] input)
		{
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x040017A3 RID: 6051
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x040017A4 RID: 6052
		private static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x020008E0 RID: 2272
		private enum DigestAlgorithm
		{
			// Token: 0x040033EA RID: 13290
			BLAKE2B_160,
			// Token: 0x040033EB RID: 13291
			BLAKE2B_256,
			// Token: 0x040033EC RID: 13292
			BLAKE2B_384,
			// Token: 0x040033ED RID: 13293
			BLAKE2B_512,
			// Token: 0x040033EE RID: 13294
			BLAKE2S_128,
			// Token: 0x040033EF RID: 13295
			BLAKE2S_160,
			// Token: 0x040033F0 RID: 13296
			BLAKE2S_224,
			// Token: 0x040033F1 RID: 13297
			BLAKE2S_256,
			// Token: 0x040033F2 RID: 13298
			DSTU7564_256,
			// Token: 0x040033F3 RID: 13299
			DSTU7564_384,
			// Token: 0x040033F4 RID: 13300
			DSTU7564_512,
			// Token: 0x040033F5 RID: 13301
			GOST3411,
			// Token: 0x040033F6 RID: 13302
			GOST3411_2012_256,
			// Token: 0x040033F7 RID: 13303
			GOST3411_2012_512,
			// Token: 0x040033F8 RID: 13304
			KECCAK_224,
			// Token: 0x040033F9 RID: 13305
			KECCAK_256,
			// Token: 0x040033FA RID: 13306
			KECCAK_288,
			// Token: 0x040033FB RID: 13307
			KECCAK_384,
			// Token: 0x040033FC RID: 13308
			KECCAK_512,
			// Token: 0x040033FD RID: 13309
			MD2,
			// Token: 0x040033FE RID: 13310
			MD4,
			// Token: 0x040033FF RID: 13311
			MD5,
			// Token: 0x04003400 RID: 13312
			NONE,
			// Token: 0x04003401 RID: 13313
			RIPEMD128,
			// Token: 0x04003402 RID: 13314
			RIPEMD160,
			// Token: 0x04003403 RID: 13315
			RIPEMD256,
			// Token: 0x04003404 RID: 13316
			RIPEMD320,
			// Token: 0x04003405 RID: 13317
			SHA_1,
			// Token: 0x04003406 RID: 13318
			SHA_224,
			// Token: 0x04003407 RID: 13319
			SHA_256,
			// Token: 0x04003408 RID: 13320
			SHA_384,
			// Token: 0x04003409 RID: 13321
			SHA_512,
			// Token: 0x0400340A RID: 13322
			SHA_512_224,
			// Token: 0x0400340B RID: 13323
			SHA_512_256,
			// Token: 0x0400340C RID: 13324
			SHA3_224,
			// Token: 0x0400340D RID: 13325
			SHA3_256,
			// Token: 0x0400340E RID: 13326
			SHA3_384,
			// Token: 0x0400340F RID: 13327
			SHA3_512,
			// Token: 0x04003410 RID: 13328
			SHAKE128,
			// Token: 0x04003411 RID: 13329
			SHAKE256,
			// Token: 0x04003412 RID: 13330
			SM3,
			// Token: 0x04003413 RID: 13331
			TIGER,
			// Token: 0x04003414 RID: 13332
			WHIRLPOOL
		}
	}
}
