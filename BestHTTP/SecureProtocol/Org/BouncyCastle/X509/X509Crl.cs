using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022F RID: 559
	public class X509Crl : X509ExtensionBase
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x000ADE64 File Offset: 0x000AC064
		public X509Crl(CertificateList c)
		{
			this.c = c;
			try
			{
				this.sigAlgName = X509SignatureUtilities.GetSignatureName(c.SignatureAlgorithm);
				if (c.SignatureAlgorithm.Parameters != null)
				{
					this.sigAlgParams = c.SignatureAlgorithm.Parameters.GetDerEncoded();
				}
				else
				{
					this.sigAlgParams = null;
				}
				this.isIndirect = this.IsIndirectCrl;
			}
			catch (Exception arg)
			{
				throw new CrlException("CRL contents invalid: " + arg);
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000ADEEC File Offset: 0x000AC0EC
		protected override X509Extensions GetX509Extensions()
		{
			if (this.c.Version < 2)
			{
				return null;
			}
			return this.c.TbsCertList.Extensions;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000ADF10 File Offset: 0x000AC110
		public virtual byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000ADF48 File Offset: 0x000AC148
		public virtual void Verify(AsymmetricKeyParameter publicKey)
		{
			this.Verify(new Asn1VerifierFactoryProvider(publicKey));
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000ADF56 File Offset: 0x000AC156
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.c.SignatureAlgorithm));
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x000ADF70 File Offset: 0x000AC170
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!this.c.SignatureAlgorithm.Equals(this.c.TbsCertList.Signature))
			{
				throw new CrlException("Signature algorithm on CertificateList does not match TbsCertList.");
			}
			Asn1Encodable parameters = this.c.SignatureAlgorithm.Parameters;
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			byte[] tbsCertList = this.GetTbsCertList();
			streamCalculator.Stream.Write(tbsCertList, 0, tbsCertList.Length);
			Platform.Dispose(streamCalculator.Stream);
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("CRL does not verify with supplied public key.");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000AE005 File Offset: 0x000AC205
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x000AE012 File Offset: 0x000AC212
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000AE01F File Offset: 0x000AC21F
		public virtual DateTime ThisUpdate
		{
			get
			{
				return this.c.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x000AE031 File Offset: 0x000AC231
		public virtual DateTimeObject NextUpdate
		{
			get
			{
				if (this.c.NextUpdate != null)
				{
					return new DateTimeObject(this.c.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000AE058 File Offset: 0x000AC258
		private ISet LoadCrlEntries()
		{
			ISet set = new HashSet();
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				X509CrlEntry x509CrlEntry = new X509CrlEntry((CrlEntry)obj, this.isIndirect, previousCertificateIssuer);
				set.Add(x509CrlEntry);
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return set;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000AE0DC File Offset: 0x000AC2DC
		public virtual X509CrlEntry GetRevokedCertificate(BigInteger serialNumber)
		{
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				CrlEntry crlEntry = (CrlEntry)obj;
				X509CrlEntry x509CrlEntry = new X509CrlEntry(crlEntry, this.isIndirect, previousCertificateIssuer);
				if (serialNumber.Equals(crlEntry.UserCertificate.Value))
				{
					return x509CrlEntry;
				}
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return null;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000AE170 File Offset: 0x000AC370
		public virtual ISet GetRevokedCertificates()
		{
			ISet set = this.LoadCrlEntries();
			if (set.Count > 0)
			{
				return set;
			}
			return null;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000AE190 File Offset: 0x000AC390
		public virtual byte[] GetTbsCertList()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.TbsCertList.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000AE1CC File Offset: 0x000AC3CC
		public virtual byte[] GetSignature()
		{
			return this.c.GetSignatureOctets();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x000AE1D9 File Offset: 0x000AC3D9
		public virtual string SigAlgName
		{
			get
			{
				return this.sigAlgName;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000AE1E1 File Offset: 0x000AC3E1
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000AE1F8 File Offset: 0x000AC3F8
		public virtual byte[] GetSigAlgParams()
		{
			return Arrays.Clone(this.sigAlgParams);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000AE208 File Offset: 0x000AC408
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509Crl x509Crl = obj as X509Crl;
			return x509Crl != null && this.c.Equals(x509Crl.c);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000AE238 File Offset: 0x000AC438
		public override int GetHashCode()
		{
			return this.c.GetHashCode();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000AE248 File Offset: 0x000AC448
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("              Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("          This update: ").Append(this.ThisUpdate).Append(newLine);
			stringBuilder.Append("          Next update: ").Append(this.NextUpdate).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ");
			stringBuilder.Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ");
				stringBuilder.Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertList.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("           Extensions: ").Append(newLine);
				}
				for (;;)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
					X509Extension extension = extensions.GetExtension(derObjectIdentifier);
					if (extension.Value != null)
					{
						Asn1Object asn1Object = X509ExtensionUtilities.FromExtensionValue(extension.Value);
						stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
						try
						{
							if (derObjectIdentifier.Equals(X509Extensions.CrlNumber))
							{
								stringBuilder.Append(new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.DeltaCrlIndicator))
							{
								stringBuilder.Append("Base CRL: " + new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.IssuingDistributionPoint))
							{
								stringBuilder.Append(IssuingDistributionPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.CrlDistributionPoints))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.FreshestCrl))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object)).Append(newLine);
							}
							goto IL_2EE;
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****").Append(newLine);
							goto IL_2EE;
						}
						goto IL_2E6;
					}
					goto IL_2E6;
					IL_2EE:
					if (!enumerator.MoveNext())
					{
						break;
					}
					continue;
					IL_2E6:
					stringBuilder.Append(newLine);
					goto IL_2EE;
				}
			}
			ISet revokedCertificates = this.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry value = (X509CrlEntry)obj;
					stringBuilder.Append(value);
					stringBuilder.Append(newLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000AE5E0 File Offset: 0x000AC7E0
		public virtual bool IsRevoked(X509Certificate cert)
		{
			CrlEntry[] revokedCertificates = this.c.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				BigInteger serialNumber = cert.SerialNumber;
				for (int i = 0; i < revokedCertificates.Length; i++)
				{
					if (revokedCertificates[i].UserCertificate.Value.Equals(serialNumber))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000AE62C File Offset: 0x000AC82C
		protected virtual bool IsIndirectCrl
		{
			get
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				bool result = false;
				try
				{
					if (extensionValue != null)
					{
						result = IssuingDistributionPoint.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).IsIndirectCrl;
					}
				}
				catch (Exception arg)
				{
					throw new CrlException("Exception reading IssuingDistributionPoint" + arg);
				}
				return result;
			}
		}

		// Token: 0x040014F0 RID: 5360
		private readonly CertificateList c;

		// Token: 0x040014F1 RID: 5361
		private readonly string sigAlgName;

		// Token: 0x040014F2 RID: 5362
		private readonly byte[] sigAlgParams;

		// Token: 0x040014F3 RID: 5363
		private readonly bool isIndirect;
	}
}
