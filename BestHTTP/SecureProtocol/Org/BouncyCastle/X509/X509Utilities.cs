﻿using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000235 RID: 565
	internal class X509Utilities
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
		static X509Utilities()
		{
			X509Utilities.algorithms.Add("MD2WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			X509Utilities.algorithms.Add("MD2WITHRSA", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			X509Utilities.algorithms.Add("MD5WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			X509Utilities.algorithms.Add("MD5WITHRSA", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSA", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA224WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA224WITHRSA", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA256WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA256WITHRSA", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA384WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA384WITHRSA", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA512WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA512WITHRSA", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA224WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA256WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA384WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA512WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("RIPEMD160WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			X509Utilities.algorithms.Add("RIPEMD160WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			X509Utilities.algorithms.Add("RIPEMD128WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			X509Utilities.algorithms.Add("RIPEMD128WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			X509Utilities.algorithms.Add("RIPEMD256WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			X509Utilities.algorithms.Add("RIPEMD256WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			X509Utilities.algorithms.Add("SHA1WITHDSA", X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.algorithms.Add("DSAWITHSHA1", X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.algorithms.Add("SHA224WITHDSA", NistObjectIdentifiers.DsaWithSha224);
			X509Utilities.algorithms.Add("SHA256WITHDSA", NistObjectIdentifiers.DsaWithSha256);
			X509Utilities.algorithms.Add("SHA384WITHDSA", NistObjectIdentifiers.DsaWithSha384);
			X509Utilities.algorithms.Add("SHA512WITHDSA", NistObjectIdentifiers.DsaWithSha512);
			X509Utilities.algorithms.Add("SHA1WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.algorithms.Add("ECDSAWITHSHA1", X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.algorithms.Add("SHA224WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha224);
			X509Utilities.algorithms.Add("SHA256WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha256);
			X509Utilities.algorithms.Add("SHA384WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha384);
			X509Utilities.algorithms.Add("SHA512WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha512);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410-94", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.algorithms.Add("GOST3411WITHECGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.algorithms.Add("GOST3411WITHECGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha384);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha512);
			X509Utilities.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			AlgorithmIdentifier hashAlgId = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			X509Utilities.exParams.Add("SHA1WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId, 20));
			AlgorithmIdentifier hashAlgId2 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha224, DerNull.Instance);
			X509Utilities.exParams.Add("SHA224WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId2, 28));
			AlgorithmIdentifier hashAlgId3 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256, DerNull.Instance);
			X509Utilities.exParams.Add("SHA256WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId3, 32));
			AlgorithmIdentifier hashAlgId4 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha384, DerNull.Instance);
			X509Utilities.exParams.Add("SHA384WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId4, 48));
			AlgorithmIdentifier hashAlgId5 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha512, DerNull.Instance);
			X509Utilities.exParams.Add("SHA512WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId5, 64));
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000AF394 File Offset: 0x000AD594
		private static RsassaPssParameters CreatePssParams(AlgorithmIdentifier hashAlgId, int saltSize)
		{
			return new RsassaPssParameters(hashAlgId, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, hashAlgId), new DerInteger(saltSize), new DerInteger(1));
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000AF3B3 File Offset: 0x000AD5B3
		internal static DerObjectIdentifier GetAlgorithmOid(string algorithmName)
		{
			algorithmName = Platform.ToUpperInvariant(algorithmName);
			if (X509Utilities.algorithms.Contains(algorithmName))
			{
				return (DerObjectIdentifier)X509Utilities.algorithms[algorithmName];
			}
			return new DerObjectIdentifier(algorithmName);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000AF3E4 File Offset: 0x000AD5E4
		internal static AlgorithmIdentifier GetSigAlgID(DerObjectIdentifier sigOid, string algorithmName)
		{
			if (X509Utilities.noParams.Contains(sigOid))
			{
				return new AlgorithmIdentifier(sigOid);
			}
			algorithmName = Platform.ToUpperInvariant(algorithmName);
			if (X509Utilities.exParams.Contains(algorithmName))
			{
				return new AlgorithmIdentifier(sigOid, (Asn1Encodable)X509Utilities.exParams[algorithmName]);
			}
			return new AlgorithmIdentifier(sigOid, DerNull.Instance);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x000AF43C File Offset: 0x000AD63C
		internal static IEnumerable GetAlgNames()
		{
			return new EnumerableProxy(X509Utilities.algorithms.Keys);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000AF450 File Offset: 0x000AD650
		internal static byte[] GetSignatureForObject(DerObjectIdentifier sigOid, string sigName, AsymmetricKeyParameter privateKey, SecureRandom random, Asn1Encodable ae)
		{
			if (sigOid == null)
			{
				throw new ArgumentNullException("sigOid");
			}
			ISigner signer = SignerUtilities.GetSigner(sigName);
			if (random != null)
			{
				signer.Init(true, new ParametersWithRandom(privateKey, random));
			}
			else
			{
				signer.Init(true, privateKey);
			}
			byte[] derEncoded = ae.GetDerEncoded();
			signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
			return signer.GenerateSignature();
		}

		// Token: 0x04001508 RID: 5384
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001509 RID: 5385
		private static readonly IDictionary exParams = Platform.CreateHashtable();

		// Token: 0x0400150A RID: 5386
		private static readonly ISet noParams = new HashSet();
	}
}
