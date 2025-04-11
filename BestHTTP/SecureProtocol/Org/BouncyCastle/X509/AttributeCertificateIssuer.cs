using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000223 RID: 547
	public class AttributeCertificateIssuer : IX509Selector, ICloneable
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x000AC3E6 File Offset: 0x000AA5E6
		public AttributeCertificateIssuer(AttCertIssuer issuer)
		{
			this.form = issuer.Issuer;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000AC3FA File Offset: 0x000AA5FA
		public AttributeCertificateIssuer(X509Name principal)
		{
			this.form = new V2Form(new GeneralNames(new GeneralName(principal)));
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000AC418 File Offset: 0x000AA618
		private object[] GetNames()
		{
			GeneralNames generalNames;
			if (this.form is V2Form)
			{
				generalNames = ((V2Form)this.form).IssuerName;
			}
			else
			{
				generalNames = (GeneralNames)this.form;
			}
			GeneralName[] names = generalNames.GetNames();
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

		// Token: 0x0600141C RID: 5148 RVA: 0x000AC4BC File Offset: 0x000AA6BC
		public X509Name[] GetPrincipals()
		{
			object[] names = this.GetNames();
			int num = 0;
			for (int num2 = 0; num2 != names.Length; num2++)
			{
				if (names[num2] is X509Name)
				{
					num++;
				}
			}
			X509Name[] array = new X509Name[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names.Length; num4++)
			{
				if (names[num4] is X509Name)
				{
					array[num3++] = (X509Name)names[num4];
				}
			}
			return array;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000AC52C File Offset: 0x000AA72C
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

		// Token: 0x0600141E RID: 5150 RVA: 0x000AC58C File Offset: 0x000AA78C
		public object Clone()
		{
			return new AttributeCertificateIssuer(AttCertIssuer.GetInstance(this.form));
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000AC5A0 File Offset: 0x000AA7A0
		public bool Match(X509Certificate x509Cert)
		{
			if (!(this.form is V2Form))
			{
				return this.MatchesDN(x509Cert.SubjectDN, (GeneralNames)this.form);
			}
			V2Form v2Form = (V2Form)this.form;
			if (v2Form.BaseCertificateID != null)
			{
				return v2Form.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(x509Cert.IssuerDN, v2Form.BaseCertificateID.Issuer);
			}
			return this.MatchesDN(x509Cert.SubjectDN, v2Form.IssuerName);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000AC630 File Offset: 0x000AA830
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateIssuer))
			{
				return false;
			}
			AttributeCertificateIssuer attributeCertificateIssuer = (AttributeCertificateIssuer)obj;
			return this.form.Equals(attributeCertificateIssuer.form);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000AC665 File Offset: 0x000AA865
		public override int GetHashCode()
		{
			return this.form.GetHashCode();
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000AC672 File Offset: 0x000AA872
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x040014DA RID: 5338
		internal readonly Asn1Encodable form;
	}
}
