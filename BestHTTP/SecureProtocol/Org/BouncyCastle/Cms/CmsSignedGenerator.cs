using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005EB RID: 1515
	public class CmsSignedGenerator
	{
		// Token: 0x060039FE RID: 14846 RVA: 0x0016BECF File Offset: 0x0016A0CF
		protected CmsSignedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x0016BEDC File Offset: 0x0016A0DC
		protected CmsSignedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0016BF18 File Offset: 0x0016A118
		protected internal virtual IDictionary GetBaseParameters(DerObjectIdentifier contentType, AlgorithmIdentifier digAlgId, byte[] hash)
		{
			IDictionary dictionary = Platform.CreateHashtable();
			if (contentType != null)
			{
				dictionary[CmsAttributeTableParameter.ContentType] = contentType;
			}
			dictionary[CmsAttributeTableParameter.DigestAlgorithmIdentifier] = digAlgId;
			dictionary[CmsAttributeTableParameter.Digest] = hash.Clone();
			return dictionary;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0016BF5B File Offset: 0x0016A15B
		protected internal virtual Asn1Set GetAttributeSet(BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable attr)
		{
			if (attr != null)
			{
				return new DerSet(attr.ToAsn1EncodableVector());
			}
			return null;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x0016BF6D File Offset: 0x0016A16D
		public void AddCertificates(IX509Store certStore)
		{
			CollectionUtilities.AddRange(this._certs, CmsUtilities.GetCertificatesFromStore(certStore));
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x0016BF80 File Offset: 0x0016A180
		public void AddCrls(IX509Store crlStore)
		{
			CollectionUtilities.AddRange(this._crls, CmsUtilities.GetCrlsFromStore(crlStore));
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x0016BF94 File Offset: 0x0016A194
		public void AddAttributeCertificates(IX509Store store)
		{
			try
			{
				foreach (object obj in store.GetMatches(null))
				{
					IX509AttributeCertificate ix509AttributeCertificate = (IX509AttributeCertificate)obj;
					this._certs.Add(new DerTaggedObject(false, 2, AttributeCertificate.GetInstance(Asn1Object.FromByteArray(ix509AttributeCertificate.GetEncoded()))));
				}
			}
			catch (Exception e)
			{
				throw new CmsException("error processing attribute certs", e);
			}
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x0016C028 File Offset: 0x0016A228
		public void AddSigners(SignerInformationStore signerStore)
		{
			foreach (object obj in signerStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				this._signers.Add(signerInformation);
				this.AddSignerCallback(signerInformation);
			}
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0016C090 File Offset: 0x0016A290
		public IDictionary GetGeneratedDigests()
		{
			return Platform.CreateHashtable(this._digests);
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x0016C09D File Offset: 0x0016A29D
		// (set) Token: 0x06003A08 RID: 14856 RVA: 0x0016C0A5 File Offset: 0x0016A2A5
		public bool UseDerForCerts
		{
			get
			{
				return this._useDerForCerts;
			}
			set
			{
				this._useDerForCerts = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x0016C0AE File Offset: 0x0016A2AE
		// (set) Token: 0x06003A0A RID: 14858 RVA: 0x0016C0B6 File Offset: 0x0016A2B6
		public bool UseDerForCrls
		{
			get
			{
				return this._useDerForCrls;
			}
			set
			{
				this._useDerForCrls = value;
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x00002B75 File Offset: 0x00000D75
		internal virtual void AddSignerCallback(SignerInformation si)
		{
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0016C0BF File Offset: 0x0016A2BF
		internal static SignerIdentifier GetSignerIdentifier(X509Certificate cert)
		{
			return new SignerIdentifier(CmsUtilities.GetIssuerAndSerialNumber(cert));
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0016C0CC File Offset: 0x0016A2CC
		internal static SignerIdentifier GetSignerIdentifier(byte[] subjectKeyIdentifier)
		{
			return new SignerIdentifier(new DerOctetString(subjectKeyIdentifier));
		}

		// Token: 0x040024F2 RID: 9458
		public static readonly string Data = CmsObjectIdentifiers.Data.Id;

		// Token: 0x040024F3 RID: 9459
		public static readonly string DigestSha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x040024F4 RID: 9460
		public static readonly string DigestSha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x040024F5 RID: 9461
		public static readonly string DigestSha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x040024F6 RID: 9462
		public static readonly string DigestSha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x040024F7 RID: 9463
		public static readonly string DigestSha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x040024F8 RID: 9464
		public static readonly string DigestMD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x040024F9 RID: 9465
		public static readonly string DigestGost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x040024FA RID: 9466
		public static readonly string DigestRipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x040024FB RID: 9467
		public static readonly string DigestRipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x040024FC RID: 9468
		public static readonly string DigestRipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x040024FD RID: 9469
		public static readonly string EncryptionRsa = PkcsObjectIdentifiers.RsaEncryption.Id;

		// Token: 0x040024FE RID: 9470
		public static readonly string EncryptionDsa = X9ObjectIdentifiers.IdDsaWithSha1.Id;

		// Token: 0x040024FF RID: 9471
		public static readonly string EncryptionECDsa = X9ObjectIdentifiers.ECDsaWithSha1.Id;

		// Token: 0x04002500 RID: 9472
		public static readonly string EncryptionRsaPss = PkcsObjectIdentifiers.IdRsassaPss.Id;

		// Token: 0x04002501 RID: 9473
		public static readonly string EncryptionGost3410 = CryptoProObjectIdentifiers.GostR3410x94.Id;

		// Token: 0x04002502 RID: 9474
		public static readonly string EncryptionECGost3410 = CryptoProObjectIdentifiers.GostR3410x2001.Id;

		// Token: 0x04002503 RID: 9475
		internal IList _certs = Platform.CreateArrayList();

		// Token: 0x04002504 RID: 9476
		internal IList _crls = Platform.CreateArrayList();

		// Token: 0x04002505 RID: 9477
		internal IList _signers = Platform.CreateArrayList();

		// Token: 0x04002506 RID: 9478
		internal IDictionary _digests = Platform.CreateHashtable();

		// Token: 0x04002507 RID: 9479
		internal bool _useDerForCerts;

		// Token: 0x04002508 RID: 9480
		internal bool _useDerForCrls;

		// Token: 0x04002509 RID: 9481
		protected readonly SecureRandom rand;
	}
}
