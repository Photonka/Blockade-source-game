using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000237 RID: 567
	public class X509V2AttributeCertificate : X509ExtensionBase, IX509AttributeCertificate, IX509Extension
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x000AF69C File Offset: 0x000AD89C
		private static AttributeCertificate GetObject(Stream input)
		{
			AttributeCertificate instance;
			try
			{
				instance = AttributeCertificate.GetInstance(Asn1Object.FromStream(input));
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception innerException)
			{
				throw new IOException("exception decoding certificate structure", innerException);
			}
			return instance;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000AF6E4 File Offset: 0x000AD8E4
		public X509V2AttributeCertificate(Stream encIn) : this(X509V2AttributeCertificate.GetObject(encIn))
		{
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000AF6F2 File Offset: 0x000AD8F2
		public X509V2AttributeCertificate(byte[] encoded) : this(new MemoryStream(encoded, false))
		{
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x000AF704 File Offset: 0x000AD904
		internal X509V2AttributeCertificate(AttributeCertificate cert)
		{
			this.cert = cert;
			try
			{
				this.notAfter = cert.ACInfo.AttrCertValidityPeriod.NotAfterTime.ToDateTime();
				this.notBefore = cert.ACInfo.AttrCertValidityPeriod.NotBeforeTime.ToDateTime();
			}
			catch (Exception innerException)
			{
				throw new IOException("invalid data structure in certificate!", innerException);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000AF774 File Offset: 0x000AD974
		public virtual int Version
		{
			get
			{
				return this.cert.ACInfo.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x000AF792 File Offset: 0x000AD992
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.cert.ACInfo.SerialNumber.Value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000AF7A9 File Offset: 0x000AD9A9
		public virtual AttributeCertificateHolder Holder
		{
			get
			{
				return new AttributeCertificateHolder((Asn1Sequence)this.cert.ACInfo.Holder.ToAsn1Object());
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000AF7CA File Offset: 0x000AD9CA
		public virtual AttributeCertificateIssuer Issuer
		{
			get
			{
				return new AttributeCertificateIssuer(this.cert.ACInfo.Issuer);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000AF7E1 File Offset: 0x000AD9E1
		public virtual DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000AF7E9 File Offset: 0x000AD9E9
		public virtual DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000AF7F4 File Offset: 0x000AD9F4
		public virtual bool[] GetIssuerUniqueID()
		{
			DerBitString issuerUniqueID = this.cert.ACInfo.IssuerUniqueID;
			if (issuerUniqueID != null)
			{
				byte[] bytes = issuerUniqueID.GetBytes();
				bool[] array = new bool[bytes.Length * 8 - issuerUniqueID.PadBits];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (((int)bytes[num / 8] & 128 >> num % 8) != 0);
				}
				return array;
			}
			return null;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000AF857 File Offset: 0x000ADA57
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000AF864 File Offset: 0x000ADA64
		public virtual bool IsValid(DateTime date)
		{
			return date.CompareTo(this.NotBefore) >= 0 && date.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000AF88B File Offset: 0x000ADA8B
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000AF898 File Offset: 0x000ADA98
		public virtual void CheckValidity(DateTime date)
		{
			if (date.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.NotAfter);
			}
			if (date.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.NotBefore);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000AF8FB File Offset: 0x000ADAFB
		public virtual AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.cert.SignatureAlgorithm;
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x000AF908 File Offset: 0x000ADB08
		public virtual byte[] GetSignature()
		{
			return this.cert.GetSignatureOctets();
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000AF915 File Offset: 0x000ADB15
		public virtual void Verify(AsymmetricKeyParameter key)
		{
			this.CheckSignature(new Asn1VerifierFactory(this.cert.SignatureAlgorithm, key));
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000AF92E File Offset: 0x000ADB2E
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.cert.SignatureAlgorithm));
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000AF948 File Offset: 0x000ADB48
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!this.cert.SignatureAlgorithm.Equals(this.cert.ACInfo.Signature))
			{
				throw new CertificateException("Signature algorithm in certificate info not same as outer certificate");
			}
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			try
			{
				byte[] encoded = this.cert.ACInfo.GetEncoded();
				streamCalculator.Stream.Write(encoded, 0, encoded.Length);
				Platform.Dispose(streamCalculator.Stream);
			}
			catch (IOException exception)
			{
				throw new SignatureException("Exception encoding certificate info object", exception);
			}
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000AF9F8 File Offset: 0x000ADBF8
		public virtual byte[] GetEncoded()
		{
			return this.cert.GetEncoded();
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000AFA05 File Offset: 0x000ADC05
		protected override X509Extensions GetX509Extensions()
		{
			return this.cert.ACInfo.Extensions;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000AFA18 File Offset: 0x000ADC18
		public virtual X509Attribute[] GetAttributes()
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			X509Attribute[] array = new X509Attribute[attributes.Count];
			for (int num = 0; num != attributes.Count; num++)
			{
				array[num] = new X509Attribute(attributes[num]);
			}
			return array;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000AFA64 File Offset: 0x000ADC64
		public virtual X509Attribute[] GetAttributes(string oid)
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != attributes.Count; num++)
			{
				X509Attribute x509Attribute = new X509Attribute(attributes[num]);
				if (x509Attribute.Oid.Equals(oid))
				{
					list.Add(x509Attribute);
				}
			}
			if (list.Count < 1)
			{
				return null;
			}
			X509Attribute[] array = new X509Attribute[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = (X509Attribute)list[i];
			}
			return array;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000AFB00 File Offset: 0x000ADD00
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509V2AttributeCertificate x509V2AttributeCertificate = obj as X509V2AttributeCertificate;
			return x509V2AttributeCertificate != null && this.cert.Equals(x509V2AttributeCertificate.cert);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000AFB30 File Offset: 0x000ADD30
		public override int GetHashCode()
		{
			return this.cert.GetHashCode();
		}

		// Token: 0x0400150F RID: 5391
		private readonly AttributeCertificate cert;

		// Token: 0x04001510 RID: 5392
		private readonly DateTime notBefore;

		// Token: 0x04001511 RID: 5393
		private readonly DateTime notAfter;
	}
}
