using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022B RID: 555
	public class X509Certificate : X509ExtensionBase
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x000ACFEA File Offset: 0x000AB1EA
		protected X509Certificate()
		{
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000ACFF4 File Offset: 0x000AB1F4
		public X509Certificate(X509CertificateStructure c)
		{
			this.c = c;
			try
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.19"));
				if (extensionValue != null)
				{
					this.basicConstraints = BasicConstraints.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				}
			}
			catch (Exception arg)
			{
				throw new CertificateParsingException("cannot construct BasicConstraints: " + arg);
			}
			try
			{
				Asn1OctetString extensionValue2 = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.15"));
				if (extensionValue2 != null)
				{
					DerBitString instance = DerBitString.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
					byte[] bytes = instance.GetBytes();
					int num = bytes.Length * 8 - instance.PadBits;
					this.keyUsage = new bool[(num < 9) ? 9 : num];
					for (int num2 = 0; num2 != num; num2++)
					{
						this.keyUsage[num2] = (((int)bytes[num2 / 8] & 128 >> num2 % 8) != 0);
					}
				}
				else
				{
					this.keyUsage = null;
				}
			}
			catch (Exception arg2)
			{
				throw new CertificateParsingException("cannot construct KeyUsage: " + arg2);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000AD104 File Offset: 0x000AB304
		public virtual X509CertificateStructure CertificateStructure
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000AD10C File Offset: 0x000AB30C
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000AD119 File Offset: 0x000AB319
		public virtual bool IsValid(DateTime time)
		{
			return time.CompareTo(this.NotBefore) >= 0 && time.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000AD140 File Offset: 0x000AB340
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000AD150 File Offset: 0x000AB350
		public virtual void CheckValidity(DateTime time)
		{
			if (time.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.c.EndDate.GetTime());
			}
			if (time.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.c.StartDate.GetTime());
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000AD1BD File Offset: 0x000AB3BD
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000AD1CA File Offset: 0x000AB3CA
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.c.SerialNumber.Value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000AD1DC File Offset: 0x000AB3DC
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x000AD1E9 File Offset: 0x000AB3E9
		public virtual X509Name SubjectDN
		{
			get
			{
				return this.c.Subject;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x000AD1F6 File Offset: 0x000AB3F6
		public virtual DateTime NotBefore
		{
			get
			{
				return this.c.StartDate.ToDateTime();
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x000AD208 File Offset: 0x000AB408
		public virtual DateTime NotAfter
		{
			get
			{
				return this.c.EndDate.ToDateTime();
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000AD21A File Offset: 0x000AB41A
		public virtual byte[] GetTbsCertificate()
		{
			return this.c.TbsCertificate.GetDerEncoded();
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000AD22C File Offset: 0x000AB42C
		public virtual byte[] GetSignature()
		{
			return this.c.GetSignatureOctets();
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000AD239 File Offset: 0x000AB439
		public virtual string SigAlgName
		{
			get
			{
				return SignerUtilities.GetEncodingName(this.c.SignatureAlgorithm.Algorithm);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000AD250 File Offset: 0x000AB450
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000AD267 File Offset: 0x000AB467
		public virtual byte[] GetSigAlgParams()
		{
			if (this.c.SignatureAlgorithm.Parameters != null)
			{
				return this.c.SignatureAlgorithm.Parameters.GetDerEncoded();
			}
			return null;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x000AD292 File Offset: 0x000AB492
		public virtual DerBitString IssuerUniqueID
		{
			get
			{
				return this.c.TbsCertificate.IssuerUniqueID;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		public virtual DerBitString SubjectUniqueID
		{
			get
			{
				return this.c.TbsCertificate.SubjectUniqueID;
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000AD2B6 File Offset: 0x000AB4B6
		public virtual bool[] GetKeyUsage()
		{
			if (this.keyUsage != null)
			{
				return (bool[])this.keyUsage.Clone();
			}
			return null;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000AD2D4 File Offset: 0x000AB4D4
		public virtual IList GetExtendedKeyUsage()
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.37"));
			if (extensionValue == null)
			{
				return null;
			}
			IList result;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				IList list = Platform.CreateArrayList();
				foreach (object obj in instance)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					list.Add(derObjectIdentifier.Id);
				}
				result = list;
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("error processing extended key usage extension", exception);
			}
			return result;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000AD37C File Offset: 0x000AB57C
		public virtual int GetBasicConstraints()
		{
			if (this.basicConstraints == null || !this.basicConstraints.IsCA())
			{
				return -1;
			}
			if (this.basicConstraints.PathLenConstraint == null)
			{
				return int.MaxValue;
			}
			return this.basicConstraints.PathLenConstraint.IntValue;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000AD3B8 File Offset: 0x000AB5B8
		public virtual ICollection GetSubjectAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.17");
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000AD3C5 File Offset: 0x000AB5C5
		public virtual ICollection GetIssuerAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.18");
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
		protected virtual ICollection GetAlternativeNames(string oid)
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier(oid));
			if (extensionValue == null)
			{
				return null;
			}
			GeneralNames instance = GeneralNames.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
			IList list = Platform.CreateArrayList();
			foreach (GeneralName generalName in instance.GetNames())
			{
				IList list2 = Platform.CreateArrayList();
				list2.Add(generalName.TagNo);
				list2.Add(generalName.Name.ToString());
				list.Add(list2);
			}
			return list;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000AD456 File Offset: 0x000AB656
		protected override X509Extensions GetX509Extensions()
		{
			if (this.c.Version < 3)
			{
				return null;
			}
			return this.c.TbsCertificate.Extensions;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000AD478 File Offset: 0x000AB678
		public virtual AsymmetricKeyParameter GetPublicKey()
		{
			return PublicKeyFactory.CreateKey(this.c.SubjectPublicKeyInfo);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000AD48A File Offset: 0x000AB68A
		public virtual byte[] GetEncoded()
		{
			return this.c.GetDerEncoded();
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000AD498 File Offset: 0x000AB698
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.c.Equals(x509Certificate.c);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000AD4C8 File Offset: 0x000AB6C8
		public override int GetHashCode()
		{
			lock (this)
			{
				if (!this.hashValueSet)
				{
					this.hashValue = this.c.GetHashCode();
					this.hashValueSet = true;
				}
			}
			return this.hashValue;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x000AD524 File Offset: 0x000AB724
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("  [0]         Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("         SerialNumber: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("           Start Date: ").Append(this.NotBefore).Append(newLine);
			stringBuilder.Append("           Final Date: ").Append(this.NotAfter).Append(newLine);
			stringBuilder.Append("            SubjectDN: ").Append(this.SubjectDN).Append(newLine);
			stringBuilder.Append("           Public Key: ").Append(this.GetPublicKey()).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ").Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ").Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("       Extensions: \n");
				}
				do
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
					X509Extension extension = extensions.GetExtension(derObjectIdentifier);
					if (extension.Value != null)
					{
						Asn1Object asn1Object = Asn1Object.FromByteArray(extension.Value.GetOctets());
						stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
						try
						{
							if (derObjectIdentifier.Equals(X509Extensions.BasicConstraints))
							{
								stringBuilder.Append(BasicConstraints.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(X509Extensions.KeyUsage))
							{
								stringBuilder.Append(KeyUsage.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeCertType))
							{
								stringBuilder.Append(new NetscapeCertType((DerBitString)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeRevocationUrl))
							{
								stringBuilder.Append(new NetscapeRevocationUrl((DerIA5String)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.VerisignCzagExtension))
							{
								stringBuilder.Append(new VerisignCzagExtension((DerIA5String)asn1Object));
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
							}
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****");
						}
					}
					stringBuilder.Append(newLine);
				}
				while (enumerator.MoveNext());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000AD850 File Offset: 0x000ABA50
		public virtual void Verify(AsymmetricKeyParameter key)
		{
			this.CheckSignature(new Asn1VerifierFactory(this.c.SignatureAlgorithm, key));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000AD869 File Offset: 0x000ABA69
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.c.SignatureAlgorithm));
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000AD884 File Offset: 0x000ABA84
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!X509Certificate.IsAlgIDEqual(this.c.SignatureAlgorithm, this.c.TbsCertificate.Signature))
			{
				throw new CertificateException("signature algorithm in TBS cert not same as outer cert");
			}
			Asn1Encodable parameters = this.c.SignatureAlgorithm.Parameters;
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			byte[] tbsCertificate = this.GetTbsCertificate();
			streamCalculator.Stream.Write(tbsCertificate, 0, tbsCertificate.Length);
			Platform.Dispose(streamCalculator.Stream);
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000AD91C File Offset: 0x000ABB1C
		private static bool IsAlgIDEqual(AlgorithmIdentifier id1, AlgorithmIdentifier id2)
		{
			if (!id1.Algorithm.Equals(id2.Algorithm))
			{
				return false;
			}
			Asn1Encodable parameters = id1.Parameters;
			Asn1Encodable parameters2 = id2.Parameters;
			if (parameters == null == (parameters2 == null))
			{
				return object.Equals(parameters, parameters2);
			}
			if (parameters != null)
			{
				return parameters.ToAsn1Object() is Asn1Null;
			}
			return parameters2.ToAsn1Object() is Asn1Null;
		}

		// Token: 0x040014E4 RID: 5348
		private readonly X509CertificateStructure c;

		// Token: 0x040014E5 RID: 5349
		private readonly BasicConstraints basicConstraints;

		// Token: 0x040014E6 RID: 5350
		private readonly bool[] keyUsage;

		// Token: 0x040014E7 RID: 5351
		private bool hashValueSet;

		// Token: 0x040014E8 RID: 5352
		private int hashValue;
	}
}
