using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E6 RID: 1766
	public abstract class PkcsObjectIdentifiers
	{
		// Token: 0x04002899 RID: 10393
		public const string Pkcs1 = "1.2.840.113549.1.1";

		// Token: 0x0400289A RID: 10394
		internal static readonly DerObjectIdentifier Pkcs1Oid = new DerObjectIdentifier("1.2.840.113549.1.1");

		// Token: 0x0400289B RID: 10395
		public static readonly DerObjectIdentifier RsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("1");

		// Token: 0x0400289C RID: 10396
		public static readonly DerObjectIdentifier MD2WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("2");

		// Token: 0x0400289D RID: 10397
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("3");

		// Token: 0x0400289E RID: 10398
		public static readonly DerObjectIdentifier MD5WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("4");

		// Token: 0x0400289F RID: 10399
		public static readonly DerObjectIdentifier Sha1WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("5");

		// Token: 0x040028A0 RID: 10400
		public static readonly DerObjectIdentifier SrsaOaepEncryptionSet = PkcsObjectIdentifiers.Pkcs1Oid.Branch("6");

		// Token: 0x040028A1 RID: 10401
		public static readonly DerObjectIdentifier IdRsaesOaep = PkcsObjectIdentifiers.Pkcs1Oid.Branch("7");

		// Token: 0x040028A2 RID: 10402
		public static readonly DerObjectIdentifier IdMgf1 = PkcsObjectIdentifiers.Pkcs1Oid.Branch("8");

		// Token: 0x040028A3 RID: 10403
		public static readonly DerObjectIdentifier IdPSpecified = PkcsObjectIdentifiers.Pkcs1Oid.Branch("9");

		// Token: 0x040028A4 RID: 10404
		public static readonly DerObjectIdentifier IdRsassaPss = PkcsObjectIdentifiers.Pkcs1Oid.Branch("10");

		// Token: 0x040028A5 RID: 10405
		public static readonly DerObjectIdentifier Sha256WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("11");

		// Token: 0x040028A6 RID: 10406
		public static readonly DerObjectIdentifier Sha384WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("12");

		// Token: 0x040028A7 RID: 10407
		public static readonly DerObjectIdentifier Sha512WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("13");

		// Token: 0x040028A8 RID: 10408
		public static readonly DerObjectIdentifier Sha224WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("14");

		// Token: 0x040028A9 RID: 10409
		public static readonly DerObjectIdentifier Sha512_224WithRSAEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("15");

		// Token: 0x040028AA RID: 10410
		public static readonly DerObjectIdentifier Sha512_256WithRSAEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("16");

		// Token: 0x040028AB RID: 10411
		public const string Pkcs3 = "1.2.840.113549.1.3";

		// Token: 0x040028AC RID: 10412
		public static readonly DerObjectIdentifier DhKeyAgreement = new DerObjectIdentifier("1.2.840.113549.1.3.1");

		// Token: 0x040028AD RID: 10413
		public const string Pkcs5 = "1.2.840.113549.1.5";

		// Token: 0x040028AE RID: 10414
		public static readonly DerObjectIdentifier PbeWithMD2AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.1");

		// Token: 0x040028AF RID: 10415
		public static readonly DerObjectIdentifier PbeWithMD2AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.4");

		// Token: 0x040028B0 RID: 10416
		public static readonly DerObjectIdentifier PbeWithMD5AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.3");

		// Token: 0x040028B1 RID: 10417
		public static readonly DerObjectIdentifier PbeWithMD5AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.6");

		// Token: 0x040028B2 RID: 10418
		public static readonly DerObjectIdentifier PbeWithSha1AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.10");

		// Token: 0x040028B3 RID: 10419
		public static readonly DerObjectIdentifier PbeWithSha1AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.11");

		// Token: 0x040028B4 RID: 10420
		public static readonly DerObjectIdentifier IdPbeS2 = new DerObjectIdentifier("1.2.840.113549.1.5.13");

		// Token: 0x040028B5 RID: 10421
		public static readonly DerObjectIdentifier IdPbkdf2 = new DerObjectIdentifier("1.2.840.113549.1.5.12");

		// Token: 0x040028B6 RID: 10422
		public const string EncryptionAlgorithm = "1.2.840.113549.3";

		// Token: 0x040028B7 RID: 10423
		public static readonly DerObjectIdentifier DesEde3Cbc = new DerObjectIdentifier("1.2.840.113549.3.7");

		// Token: 0x040028B8 RID: 10424
		public static readonly DerObjectIdentifier RC2Cbc = new DerObjectIdentifier("1.2.840.113549.3.2");

		// Token: 0x040028B9 RID: 10425
		public const string DigestAlgorithm = "1.2.840.113549.2";

		// Token: 0x040028BA RID: 10426
		public static readonly DerObjectIdentifier MD2 = new DerObjectIdentifier("1.2.840.113549.2.2");

		// Token: 0x040028BB RID: 10427
		public static readonly DerObjectIdentifier MD4 = new DerObjectIdentifier("1.2.840.113549.2.4");

		// Token: 0x040028BC RID: 10428
		public static readonly DerObjectIdentifier MD5 = new DerObjectIdentifier("1.2.840.113549.2.5");

		// Token: 0x040028BD RID: 10429
		public static readonly DerObjectIdentifier IdHmacWithSha1 = new DerObjectIdentifier("1.2.840.113549.2.7");

		// Token: 0x040028BE RID: 10430
		public static readonly DerObjectIdentifier IdHmacWithSha224 = new DerObjectIdentifier("1.2.840.113549.2.8");

		// Token: 0x040028BF RID: 10431
		public static readonly DerObjectIdentifier IdHmacWithSha256 = new DerObjectIdentifier("1.2.840.113549.2.9");

		// Token: 0x040028C0 RID: 10432
		public static readonly DerObjectIdentifier IdHmacWithSha384 = new DerObjectIdentifier("1.2.840.113549.2.10");

		// Token: 0x040028C1 RID: 10433
		public static readonly DerObjectIdentifier IdHmacWithSha512 = new DerObjectIdentifier("1.2.840.113549.2.11");

		// Token: 0x040028C2 RID: 10434
		public const string Pkcs7 = "1.2.840.113549.1.7";

		// Token: 0x040028C3 RID: 10435
		public static readonly DerObjectIdentifier Data = new DerObjectIdentifier("1.2.840.113549.1.7.1");

		// Token: 0x040028C4 RID: 10436
		public static readonly DerObjectIdentifier SignedData = new DerObjectIdentifier("1.2.840.113549.1.7.2");

		// Token: 0x040028C5 RID: 10437
		public static readonly DerObjectIdentifier EnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.3");

		// Token: 0x040028C6 RID: 10438
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.4");

		// Token: 0x040028C7 RID: 10439
		public static readonly DerObjectIdentifier DigestedData = new DerObjectIdentifier("1.2.840.113549.1.7.5");

		// Token: 0x040028C8 RID: 10440
		public static readonly DerObjectIdentifier EncryptedData = new DerObjectIdentifier("1.2.840.113549.1.7.6");

		// Token: 0x040028C9 RID: 10441
		public const string Pkcs9 = "1.2.840.113549.1.9";

		// Token: 0x040028CA RID: 10442
		public static readonly DerObjectIdentifier Pkcs9AtEmailAddress = new DerObjectIdentifier("1.2.840.113549.1.9.1");

		// Token: 0x040028CB RID: 10443
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredName = new DerObjectIdentifier("1.2.840.113549.1.9.2");

		// Token: 0x040028CC RID: 10444
		public static readonly DerObjectIdentifier Pkcs9AtContentType = new DerObjectIdentifier("1.2.840.113549.1.9.3");

		// Token: 0x040028CD RID: 10445
		public static readonly DerObjectIdentifier Pkcs9AtMessageDigest = new DerObjectIdentifier("1.2.840.113549.1.9.4");

		// Token: 0x040028CE RID: 10446
		public static readonly DerObjectIdentifier Pkcs9AtSigningTime = new DerObjectIdentifier("1.2.840.113549.1.9.5");

		// Token: 0x040028CF RID: 10447
		public static readonly DerObjectIdentifier Pkcs9AtCounterSignature = new DerObjectIdentifier("1.2.840.113549.1.9.6");

		// Token: 0x040028D0 RID: 10448
		public static readonly DerObjectIdentifier Pkcs9AtChallengePassword = new DerObjectIdentifier("1.2.840.113549.1.9.7");

		// Token: 0x040028D1 RID: 10449
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredAddress = new DerObjectIdentifier("1.2.840.113549.1.9.8");

		// Token: 0x040028D2 RID: 10450
		public static readonly DerObjectIdentifier Pkcs9AtExtendedCertificateAttributes = new DerObjectIdentifier("1.2.840.113549.1.9.9");

		// Token: 0x040028D3 RID: 10451
		public static readonly DerObjectIdentifier Pkcs9AtSigningDescription = new DerObjectIdentifier("1.2.840.113549.1.9.13");

		// Token: 0x040028D4 RID: 10452
		public static readonly DerObjectIdentifier Pkcs9AtExtensionRequest = new DerObjectIdentifier("1.2.840.113549.1.9.14");

		// Token: 0x040028D5 RID: 10453
		public static readonly DerObjectIdentifier Pkcs9AtSmimeCapabilities = new DerObjectIdentifier("1.2.840.113549.1.9.15");

		// Token: 0x040028D6 RID: 10454
		public static readonly DerObjectIdentifier IdSmime = new DerObjectIdentifier("1.2.840.113549.1.9.16");

		// Token: 0x040028D7 RID: 10455
		public static readonly DerObjectIdentifier Pkcs9AtFriendlyName = new DerObjectIdentifier("1.2.840.113549.1.9.20");

		// Token: 0x040028D8 RID: 10456
		public static readonly DerObjectIdentifier Pkcs9AtLocalKeyID = new DerObjectIdentifier("1.2.840.113549.1.9.21");

		// Token: 0x040028D9 RID: 10457
		[Obsolete("Use X509Certificate instead")]
		public static readonly DerObjectIdentifier X509CertType = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x040028DA RID: 10458
		public const string CertTypes = "1.2.840.113549.1.9.22";

		// Token: 0x040028DB RID: 10459
		public static readonly DerObjectIdentifier X509Certificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x040028DC RID: 10460
		public static readonly DerObjectIdentifier SdsiCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.2");

		// Token: 0x040028DD RID: 10461
		public const string CrlTypes = "1.2.840.113549.1.9.23";

		// Token: 0x040028DE RID: 10462
		public static readonly DerObjectIdentifier X509Crl = new DerObjectIdentifier("1.2.840.113549.1.9.23.1");

		// Token: 0x040028DF RID: 10463
		public static readonly DerObjectIdentifier IdAlg = PkcsObjectIdentifiers.IdSmime.Branch("3");

		// Token: 0x040028E0 RID: 10464
		public static readonly DerObjectIdentifier IdAlgEsdh = PkcsObjectIdentifiers.IdAlg.Branch("5");

		// Token: 0x040028E1 RID: 10465
		public static readonly DerObjectIdentifier IdAlgCms3DesWrap = PkcsObjectIdentifiers.IdAlg.Branch("6");

		// Token: 0x040028E2 RID: 10466
		public static readonly DerObjectIdentifier IdAlgCmsRC2Wrap = PkcsObjectIdentifiers.IdAlg.Branch("7");

		// Token: 0x040028E3 RID: 10467
		public static readonly DerObjectIdentifier IdAlgPwriKek = PkcsObjectIdentifiers.IdAlg.Branch("9");

		// Token: 0x040028E4 RID: 10468
		public static readonly DerObjectIdentifier IdAlgSsdh = PkcsObjectIdentifiers.IdAlg.Branch("10");

		// Token: 0x040028E5 RID: 10469
		public static readonly DerObjectIdentifier IdRsaKem = PkcsObjectIdentifiers.IdAlg.Branch("14");

		// Token: 0x040028E6 RID: 10470
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("1");

		// Token: 0x040028E7 RID: 10471
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("2");

		// Token: 0x040028E8 RID: 10472
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("3");

		// Token: 0x040028E9 RID: 10473
		public static readonly DerObjectIdentifier IdAAReceiptRequest = PkcsObjectIdentifiers.IdSmime.Branch("2.1");

		// Token: 0x040028EA RID: 10474
		public const string IdCT = "1.2.840.113549.1.9.16.1";

		// Token: 0x040028EB RID: 10475
		public static readonly DerObjectIdentifier IdCTAuthData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.2");

		// Token: 0x040028EC RID: 10476
		public static readonly DerObjectIdentifier IdCTTstInfo = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.4");

		// Token: 0x040028ED RID: 10477
		public static readonly DerObjectIdentifier IdCTCompressedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.9");

		// Token: 0x040028EE RID: 10478
		public static readonly DerObjectIdentifier IdCTAuthEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.23");

		// Token: 0x040028EF RID: 10479
		public static readonly DerObjectIdentifier IdCTTimestampedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.31");

		// Token: 0x040028F0 RID: 10480
		public const string IdCti = "1.2.840.113549.1.9.16.6";

		// Token: 0x040028F1 RID: 10481
		public static readonly DerObjectIdentifier IdCtiEtsProofOfOrigin = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.1");

		// Token: 0x040028F2 RID: 10482
		public static readonly DerObjectIdentifier IdCtiEtsProofOfReceipt = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.2");

		// Token: 0x040028F3 RID: 10483
		public static readonly DerObjectIdentifier IdCtiEtsProofOfDelivery = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.3");

		// Token: 0x040028F4 RID: 10484
		public static readonly DerObjectIdentifier IdCtiEtsProofOfSender = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.4");

		// Token: 0x040028F5 RID: 10485
		public static readonly DerObjectIdentifier IdCtiEtsProofOfApproval = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.5");

		// Token: 0x040028F6 RID: 10486
		public static readonly DerObjectIdentifier IdCtiEtsProofOfCreation = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.6");

		// Token: 0x040028F7 RID: 10487
		public const string IdAA = "1.2.840.113549.1.9.16.2";

		// Token: 0x040028F8 RID: 10488
		public static readonly DerObjectIdentifier IdAAOid = new DerObjectIdentifier("1.2.840.113549.1.9.16.2");

		// Token: 0x040028F9 RID: 10489
		public static readonly DerObjectIdentifier IdAAContentHint = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.4");

		// Token: 0x040028FA RID: 10490
		public static readonly DerObjectIdentifier IdAAMsgSigDigest = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.5");

		// Token: 0x040028FB RID: 10491
		public static readonly DerObjectIdentifier IdAAContentReference = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.10");

		// Token: 0x040028FC RID: 10492
		public static readonly DerObjectIdentifier IdAAEncrypKeyPref = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.11");

		// Token: 0x040028FD RID: 10493
		public static readonly DerObjectIdentifier IdAASigningCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.12");

		// Token: 0x040028FE RID: 10494
		public static readonly DerObjectIdentifier IdAASigningCertificateV2 = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47");

		// Token: 0x040028FF RID: 10495
		public static readonly DerObjectIdentifier IdAAContentIdentifier = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.7");

		// Token: 0x04002900 RID: 10496
		public static readonly DerObjectIdentifier IdAASignatureTimeStampToken = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.14");

		// Token: 0x04002901 RID: 10497
		public static readonly DerObjectIdentifier IdAAEtsSigPolicyID = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.15");

		// Token: 0x04002902 RID: 10498
		public static readonly DerObjectIdentifier IdAAEtsCommitmentType = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.16");

		// Token: 0x04002903 RID: 10499
		public static readonly DerObjectIdentifier IdAAEtsSignerLocation = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.17");

		// Token: 0x04002904 RID: 10500
		public static readonly DerObjectIdentifier IdAAEtsSignerAttr = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.18");

		// Token: 0x04002905 RID: 10501
		public static readonly DerObjectIdentifier IdAAEtsOtherSigCert = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.19");

		// Token: 0x04002906 RID: 10502
		public static readonly DerObjectIdentifier IdAAEtsContentTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.20");

		// Token: 0x04002907 RID: 10503
		public static readonly DerObjectIdentifier IdAAEtsCertificateRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.21");

		// Token: 0x04002908 RID: 10504
		public static readonly DerObjectIdentifier IdAAEtsRevocationRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.22");

		// Token: 0x04002909 RID: 10505
		public static readonly DerObjectIdentifier IdAAEtsCertValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.23");

		// Token: 0x0400290A RID: 10506
		public static readonly DerObjectIdentifier IdAAEtsRevocationValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.24");

		// Token: 0x0400290B RID: 10507
		public static readonly DerObjectIdentifier IdAAEtsEscTimeStamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.25");

		// Token: 0x0400290C RID: 10508
		public static readonly DerObjectIdentifier IdAAEtsCertCrlTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.26");

		// Token: 0x0400290D RID: 10509
		public static readonly DerObjectIdentifier IdAAEtsArchiveTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.27");

		// Token: 0x0400290E RID: 10510
		public static readonly DerObjectIdentifier IdAADecryptKeyID = PkcsObjectIdentifiers.IdAAOid.Branch("37");

		// Token: 0x0400290F RID: 10511
		public static readonly DerObjectIdentifier IdAAImplCryptoAlgs = PkcsObjectIdentifiers.IdAAOid.Branch("38");

		// Token: 0x04002910 RID: 10512
		public static readonly DerObjectIdentifier IdAAAsymmDecryptKeyID = PkcsObjectIdentifiers.IdAAOid.Branch("54");

		// Token: 0x04002911 RID: 10513
		public static readonly DerObjectIdentifier IdAAImplCompressAlgs = PkcsObjectIdentifiers.IdAAOid.Branch("43");

		// Token: 0x04002912 RID: 10514
		public static readonly DerObjectIdentifier IdAACommunityIdentifiers = PkcsObjectIdentifiers.IdAAOid.Branch("40");

		// Token: 0x04002913 RID: 10515
		[Obsolete("Use 'IdAAEtsSigPolicyID' instead")]
		public static readonly DerObjectIdentifier IdAASigPolicyID = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x04002914 RID: 10516
		[Obsolete("Use 'IdAAEtsCommitmentType' instead")]
		public static readonly DerObjectIdentifier IdAACommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x04002915 RID: 10517
		[Obsolete("Use 'IdAAEtsSignerLocation' instead")]
		public static readonly DerObjectIdentifier IdAASignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x04002916 RID: 10518
		[Obsolete("Use 'IdAAEtsOtherSigCert' instead")]
		public static readonly DerObjectIdentifier IdAAOtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x04002917 RID: 10519
		public const string IdSpq = "1.2.840.113549.1.9.16.5";

		// Token: 0x04002918 RID: 10520
		public static readonly DerObjectIdentifier IdSpqEtsUri = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.1");

		// Token: 0x04002919 RID: 10521
		public static readonly DerObjectIdentifier IdSpqEtsUNotice = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.2");

		// Token: 0x0400291A RID: 10522
		public const string Pkcs12 = "1.2.840.113549.1.12";

		// Token: 0x0400291B RID: 10523
		public const string BagTypes = "1.2.840.113549.1.12.10.1";

		// Token: 0x0400291C RID: 10524
		public static readonly DerObjectIdentifier KeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.1");

		// Token: 0x0400291D RID: 10525
		public static readonly DerObjectIdentifier Pkcs8ShroudedKeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.2");

		// Token: 0x0400291E RID: 10526
		public static readonly DerObjectIdentifier CertBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.3");

		// Token: 0x0400291F RID: 10527
		public static readonly DerObjectIdentifier CrlBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.4");

		// Token: 0x04002920 RID: 10528
		public static readonly DerObjectIdentifier SecretBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.5");

		// Token: 0x04002921 RID: 10529
		public static readonly DerObjectIdentifier SafeContentsBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.6");

		// Token: 0x04002922 RID: 10530
		public const string Pkcs12PbeIds = "1.2.840.113549.1.12.1";

		// Token: 0x04002923 RID: 10531
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.1");

		// Token: 0x04002924 RID: 10532
		public static readonly DerObjectIdentifier PbeWithShaAnd40BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.2");

		// Token: 0x04002925 RID: 10533
		public static readonly DerObjectIdentifier PbeWithShaAnd3KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.3");

		// Token: 0x04002926 RID: 10534
		public static readonly DerObjectIdentifier PbeWithShaAnd2KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.4");

		// Token: 0x04002927 RID: 10535
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.5");

		// Token: 0x04002928 RID: 10536
		public static readonly DerObjectIdentifier PbewithShaAnd40BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.6");
	}
}
