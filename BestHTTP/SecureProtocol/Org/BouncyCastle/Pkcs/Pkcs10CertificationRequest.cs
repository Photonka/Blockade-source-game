using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B2 RID: 690
	public class Pkcs10CertificationRequest : CertificationRequest
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x000C58B0 File Offset: 0x000C3AB0
		static Pkcs10CertificationRequest()
		{
			Pkcs10CertificationRequest.algorithms.Add("MD2WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD2WITHRSA", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD5WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD5WITHRSA", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("RSAWITHMD5", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSA", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSA", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSA", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSA", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSA", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("RSAWITHSHA1", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD128WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD128WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD160WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD160WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD256WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD256WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHDSA", X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("DSAWITHSHA1", X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHDSA", NistObjectIdentifiers.DsaWithSha224);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHDSA", NistObjectIdentifiers.DsaWithSha256);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHDSA", NistObjectIdentifiers.DsaWithSha384);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHDSA", NistObjectIdentifiers.DsaWithSha512);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha224);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha256);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha384);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha512);
			Pkcs10CertificationRequest.algorithms.Add("ECDSAWITHSHA1", X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.algorithms.Add("GOST3410WITHGOST3411", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHECGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHECGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption, "SHA1WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption, "SHA224WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption, "SHA256WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption, "SHA384WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption, "SHA512WITHRSA");
			Pkcs10CertificationRequest.oids.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94, "GOST3411WITHGOST3410");
			Pkcs10CertificationRequest.oids.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001, "GOST3411WITHECGOST3410");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.MD5WithRsaEncryption, "MD5WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.MD2WithRsaEncryption, "MD2WITHRSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.IdDsaWithSha1, "SHA1WITHDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha1, "SHA1WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha224, "SHA224WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha256, "SHA256WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha384, "SHA384WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha512, "SHA512WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.MD5WithRsa, "MD5WITHRSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.Sha1WithRsa, "SHA1WITHRSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.DsaWithSha1, "SHA1WITHDSA");
			Pkcs10CertificationRequest.oids.Add(NistObjectIdentifiers.DsaWithSha224, "SHA224WITHDSA");
			Pkcs10CertificationRequest.oids.Add(NistObjectIdentifiers.DsaWithSha256, "SHA256WITHDSA");
			Pkcs10CertificationRequest.keyAlgorithms.Add(PkcsObjectIdentifiers.RsaEncryption, "RSA");
			Pkcs10CertificationRequest.keyAlgorithms.Add(X9ObjectIdentifiers.IdDsa, "DSA");
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			Pkcs10CertificationRequest.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
			Pkcs10CertificationRequest.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			AlgorithmIdentifier hashAlgId = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA1WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId, 20));
			AlgorithmIdentifier hashAlgId2 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha224, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA224WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId2, 28));
			AlgorithmIdentifier hashAlgId3 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA256WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId3, 32));
			AlgorithmIdentifier hashAlgId4 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha384, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA384WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId4, 48));
			AlgorithmIdentifier hashAlgId5 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha512, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA512WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId5, 64));
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000AF394 File Offset: 0x000AD594
		private static RsassaPssParameters CreatePssParams(AlgorithmIdentifier hashAlgId, int saltSize)
		{
			return new RsassaPssParameters(hashAlgId, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, hashAlgId), new DerInteger(saltSize), new DerInteger(1));
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000C5F72 File Offset: 0x000C4172
		protected Pkcs10CertificationRequest()
		{
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000C5F7A File Offset: 0x000C417A
		public Pkcs10CertificationRequest(byte[] encoded) : base((Asn1Sequence)Asn1Object.FromByteArray(encoded))
		{
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000C5F8D File Offset: 0x000C418D
		public Pkcs10CertificationRequest(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000C5F96 File Offset: 0x000C4196
		public Pkcs10CertificationRequest(Stream input) : base((Asn1Sequence)Asn1Object.FromStream(input))
		{
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000C5FA9 File Offset: 0x000C41A9
		public Pkcs10CertificationRequest(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : this(new Asn1SignatureFactory(signatureAlgorithm, signingKey), subject, publicKey, attributes)
		{
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000C5FBD File Offset: 0x000C41BD
		[Obsolete("Use constructor without 'signingKey' parameter (ignored here)")]
		public Pkcs10CertificationRequest(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : this(signatureFactory, subject, publicKey, attributes)
		{
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000C5FCC File Offset: 0x000C41CC
		public Pkcs10CertificationRequest(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			if (signatureFactory == null)
			{
				throw new ArgumentNullException("signatureFactory");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("expected public key", "publicKey");
			}
			this.Init(signatureFactory, subject, publicKey, attributes);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000C602C File Offset: 0x000C422C
		private void Init(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			this.sigAlgId = (AlgorithmIdentifier)signatureFactory.AlgorithmDetails;
			SubjectPublicKeyInfo pkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			this.reqInfo = new CertificationRequestInfo(subject, pkInfo, attributes);
			IStreamCalculator streamCalculator = signatureFactory.CreateCalculator();
			byte[] derEncoded = this.reqInfo.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			this.sigBits = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000C60A9 File Offset: 0x000C42A9
		public AsymmetricKeyParameter GetPublicKey()
		{
			return PublicKeyFactory.CreateKey(this.reqInfo.SubjectPublicKeyInfo);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000C60BB File Offset: 0x000C42BB
		public bool Verify()
		{
			return this.Verify(this.GetPublicKey());
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000C60C9 File Offset: 0x000C42C9
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			return this.Verify(new Asn1VerifierFactoryProvider(publicKey));
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000C60D7 File Offset: 0x000C42D7
		public bool Verify(IVerifierFactoryProvider verifierProvider)
		{
			return this.Verify(verifierProvider.CreateVerifierFactory(this.sigAlgId));
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000C60EC File Offset: 0x000C42EC
		public bool Verify(IVerifierFactory verifier)
		{
			bool result;
			try
			{
				byte[] derEncoded = this.reqInfo.GetDerEncoded();
				IStreamCalculator streamCalculator = verifier.CreateCalculator();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
				Platform.Dispose(streamCalculator.Stream);
				result = ((IVerifier)streamCalculator.GetResult()).IsVerified(this.sigBits.GetOctets());
			}
			catch (Exception exception)
			{
				throw new SignatureException("exception encoding TBS cert request", exception);
			}
			return result;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000C6164 File Offset: 0x000C4364
		private void SetSignatureParameters(ISigner signature, Asn1Encodable asn1Params)
		{
			if (asn1Params != null && !(asn1Params is Asn1Null) && Platform.EndsWith(signature.AlgorithmName, "MGF1"))
			{
				throw Platform.CreateNotImplementedException("signature algorithm with MGF1");
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000C6190 File Offset: 0x000C4390
		internal static string GetSignatureName(AlgorithmIdentifier sigAlgId)
		{
			Asn1Encodable parameters = sigAlgId.Parameters;
			if (parameters != null && !(parameters is Asn1Null) && sigAlgId.Algorithm.Equals(PkcsObjectIdentifiers.IdRsassaPss))
			{
				return Pkcs10CertificationRequest.GetDigestAlgName(RsassaPssParameters.GetInstance(parameters).HashAlgorithm.Algorithm) + "withRSAandMGF1";
			}
			return sigAlgId.Algorithm.Id;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000C61EC File Offset: 0x000C43EC
		private static string GetDigestAlgName(DerObjectIdentifier digestAlgOID)
		{
			if (PkcsObjectIdentifiers.MD5.Equals(digestAlgOID))
			{
				return "MD5";
			}
			if (OiwObjectIdentifiers.IdSha1.Equals(digestAlgOID))
			{
				return "SHA1";
			}
			if (NistObjectIdentifiers.IdSha224.Equals(digestAlgOID))
			{
				return "SHA224";
			}
			if (NistObjectIdentifiers.IdSha256.Equals(digestAlgOID))
			{
				return "SHA256";
			}
			if (NistObjectIdentifiers.IdSha384.Equals(digestAlgOID))
			{
				return "SHA384";
			}
			if (NistObjectIdentifiers.IdSha512.Equals(digestAlgOID))
			{
				return "SHA512";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD128.Equals(digestAlgOID))
			{
				return "RIPEMD128";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD160.Equals(digestAlgOID))
			{
				return "RIPEMD160";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD256.Equals(digestAlgOID))
			{
				return "RIPEMD256";
			}
			if (CryptoProObjectIdentifiers.GostR3411.Equals(digestAlgOID))
			{
				return "GOST3411";
			}
			return digestAlgOID.Id;
		}

		// Token: 0x0400177B RID: 6011
		protected static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x0400177C RID: 6012
		protected static readonly IDictionary exParams = Platform.CreateHashtable();

		// Token: 0x0400177D RID: 6013
		protected static readonly IDictionary keyAlgorithms = Platform.CreateHashtable();

		// Token: 0x0400177E RID: 6014
		protected static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x0400177F RID: 6015
		protected static readonly ISet noParams = new HashSet();
	}
}
