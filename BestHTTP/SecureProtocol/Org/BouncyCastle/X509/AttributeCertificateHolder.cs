using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000222 RID: 546
	public class AttributeCertificateHolder : IX509Selector, ICloneable
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x000ABEF0 File Offset: 0x000AA0F0
		internal AttributeCertificateHolder(Asn1Sequence seq)
		{
			this.holder = Holder.GetInstance(seq);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x000ABF04 File Offset: 0x000AA104
		public AttributeCertificateHolder(X509Name issuerName, BigInteger serialNumber)
		{
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerName), new DerInteger(serialNumber)));
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x000ABF2C File Offset: 0x000AA12C
		public AttributeCertificateHolder(X509Certificate cert)
		{
			X509Name issuerX509Principal;
			try
			{
				issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message);
			}
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerX509Principal), new DerInteger(cert.SerialNumber)));
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000ABF88 File Offset: 0x000AA188
		public AttributeCertificateHolder(X509Name principal)
		{
			this.holder = new Holder(this.GenerateGeneralNames(principal));
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000ABFA2 File Offset: 0x000AA1A2
		public AttributeCertificateHolder(int digestedObjectType, string digestAlgorithm, string otherObjectTypeID, byte[] objectDigest)
		{
			this.holder = new Holder(new ObjectDigestInfo(digestedObjectType, otherObjectTypeID, new AlgorithmIdentifier(new DerObjectIdentifier(digestAlgorithm)), Arrays.Clone(objectDigest)));
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000ABFD0 File Offset: 0x000AA1D0
		public int DigestedObjectType
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestedObjectType.Value.IntValue;
				}
				return -1;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000AC000 File Offset: 0x000AA200
		public string DigestAlgorithm
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestAlgorithm.Algorithm.Id;
				}
				return null;
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x000AC030 File Offset: 0x000AA230
		public byte[] GetObjectDigest()
		{
			ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
			if (objectDigestInfo != null)
			{
				return objectDigestInfo.ObjectDigest.GetBytes();
			}
			return null;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000AC05C File Offset: 0x000AA25C
		public string OtherObjectTypeID
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.OtherObjectTypeID.Id;
				}
				return null;
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000AC085 File Offset: 0x000AA285
		private GeneralNames GenerateGeneralNames(X509Name principal)
		{
			return new GeneralNames(new GeneralName(principal));
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000AC094 File Offset: 0x000AA294
		private bool MatchesDN(X509Name subject, GeneralNames targets)
		{
			GeneralName[] names = targets.GetNames();
			for (int num = 0; num != names.Length; num++)
			{
				GeneralName generalName = names[num];
				if (generalName.TagNo == 4)
				{
					try
					{
						if (X509Name.GetInstance(generalName.Name).Equivalent(subject))
						{
							return true;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return false;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x000AC0F4 File Offset: 0x000AA2F4
		private object[] GetNames(GeneralName[] names)
		{
			int num = 0;
			for (int num2 = 0; num2 != names.Length; num2++)
			{
				if (names[num2].TagNo == 4)
				{
					num++;
				}
			}
			object[] array = new object[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names.Length; num4++)
			{
				if (names[num4].TagNo == 4)
				{
					array[num3++] = X509Name.GetInstance(names[num4].Name);
				}
			}
			return array;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000AC160 File Offset: 0x000AA360
		private X509Name[] GetPrincipals(GeneralNames names)
		{
			object[] names2 = this.GetNames(names.GetNames());
			int num = 0;
			for (int num2 = 0; num2 != names2.Length; num2++)
			{
				if (names2[num2] is X509Name)
				{
					num++;
				}
			}
			X509Name[] array = new X509Name[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names2.Length; num4++)
			{
				if (names2[num4] is X509Name)
				{
					array[num3++] = (X509Name)names2[num4];
				}
			}
			return array;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000AC1D4 File Offset: 0x000AA3D4
		public X509Name[] GetEntityNames()
		{
			if (this.holder.EntityName != null)
			{
				return this.GetPrincipals(this.holder.EntityName);
			}
			return null;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000AC1F6 File Offset: 0x000AA3F6
		public X509Name[] GetIssuer()
		{
			if (this.holder.BaseCertificateID != null)
			{
				return this.GetPrincipals(this.holder.BaseCertificateID.Issuer);
			}
			return null;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000AC21D File Offset: 0x000AA41D
		public BigInteger SerialNumber
		{
			get
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value;
				}
				return null;
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000AC243 File Offset: 0x000AA443
		public object Clone()
		{
			return new AttributeCertificateHolder((Asn1Sequence)this.holder.ToAsn1Object());
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000AC25C File Offset: 0x000AA45C
		public bool Match(X509Certificate x509Cert)
		{
			try
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(PrincipalUtilities.GetIssuerX509Principal(x509Cert), this.holder.BaseCertificateID.Issuer);
				}
				if (this.holder.EntityName != null && this.MatchesDN(PrincipalUtilities.GetSubjectX509Principal(x509Cert), this.holder.EntityName))
				{
					return true;
				}
				if (this.holder.ObjectDigestInfo != null)
				{
					IDigest digest = null;
					try
					{
						digest = DigestUtilities.GetDigest(this.DigestAlgorithm);
					}
					catch (Exception)
					{
						return false;
					}
					int digestedObjectType = this.DigestedObjectType;
					if (digestedObjectType != 0)
					{
						if (digestedObjectType == 1)
						{
							byte[] encoded = x509Cert.GetEncoded();
							digest.BlockUpdate(encoded, 0, encoded.Length);
						}
					}
					else
					{
						byte[] encoded2 = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Cert.GetPublicKey()).GetEncoded();
						digest.BlockUpdate(encoded2, 0, encoded2.Length);
					}
					if (!Arrays.AreEqual(DigestUtilities.DoFinal(digest), this.GetObjectDigest()))
					{
						return false;
					}
				}
			}
			catch (CertificateEncodingException)
			{
				return false;
			}
			return false;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000AC38C File Offset: 0x000AA58C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateHolder))
			{
				return false;
			}
			AttributeCertificateHolder attributeCertificateHolder = (AttributeCertificateHolder)obj;
			return this.holder.Equals(attributeCertificateHolder.holder);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000AC3C1 File Offset: 0x000AA5C1
		public override int GetHashCode()
		{
			return this.holder.GetHashCode();
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x000AC3CE File Offset: 0x000AA5CE
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x040014D9 RID: 5337
		internal readonly Holder holder;
	}
}
