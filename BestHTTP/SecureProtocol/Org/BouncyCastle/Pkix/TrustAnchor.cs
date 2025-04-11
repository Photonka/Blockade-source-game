using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002AF RID: 687
	public class TrustAnchor
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x000C5598 File Offset: 0x000C3798
		public TrustAnchor(X509Certificate trustedCert, byte[] nameConstraints)
		{
			if (trustedCert == null)
			{
				throw new ArgumentNullException("trustedCert");
			}
			this.trustedCert = trustedCert;
			this.pubKey = null;
			this.caName = null;
			this.caPrincipal = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000C55D4 File Offset: 0x000C37D4
		public TrustAnchor(X509Name caPrincipal, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caPrincipal == null)
			{
				throw new ArgumentNullException("caPrincipal");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			this.trustedCert = null;
			this.caPrincipal = caPrincipal;
			this.caName = caPrincipal.ToString();
			this.pubKey = pubKey;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000C562C File Offset: 0x000C382C
		public TrustAnchor(string caName, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caName == null)
			{
				throw new ArgumentNullException("caName");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			if (caName.Length == 0)
			{
				throw new ArgumentException("caName can not be an empty string");
			}
			this.caPrincipal = new X509Name(caName);
			this.pubKey = pubKey;
			this.caName = caName;
			this.trustedCert = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x000C5696 File Offset: 0x000C3896
		public X509Certificate TrustedCert
		{
			get
			{
				return this.trustedCert;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x000C569E File Offset: 0x000C389E
		public X509Name CA
		{
			get
			{
				return this.caPrincipal;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x000C56A6 File Offset: 0x000C38A6
		public string CAName
		{
			get
			{
				return this.caName;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000C56AE File Offset: 0x000C38AE
		public AsymmetricKeyParameter CAPublicKey
		{
			get
			{
				return this.pubKey;
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000C56B6 File Offset: 0x000C38B6
		private void setNameConstraints(byte[] bytes)
		{
			if (bytes == null)
			{
				this.ncBytes = null;
				this.nc = null;
				return;
			}
			this.ncBytes = (byte[])bytes.Clone();
			this.nc = NameConstraints.GetInstance(Asn1Object.FromByteArray(bytes));
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000C56EC File Offset: 0x000C38EC
		public byte[] GetNameConstraints
		{
			get
			{
				return Arrays.Clone(this.ncBytes);
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000C56FC File Offset: 0x000C38FC
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(newLine);
			if (this.pubKey != null)
			{
				stringBuilder.Append("  Trusted CA Public Key: ").Append(this.pubKey).Append(newLine);
				stringBuilder.Append("  Trusted CA Issuer Name: ").Append(this.caName).Append(newLine);
			}
			else
			{
				stringBuilder.Append("  Trusted CA cert: ").Append(this.TrustedCert).Append(newLine);
			}
			if (this.nc != null)
			{
				stringBuilder.Append("  Name Constraints: ").Append(this.nc).Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001774 RID: 6004
		private readonly AsymmetricKeyParameter pubKey;

		// Token: 0x04001775 RID: 6005
		private readonly string caName;

		// Token: 0x04001776 RID: 6006
		private readonly X509Name caPrincipal;

		// Token: 0x04001777 RID: 6007
		private readonly X509Certificate trustedCert;

		// Token: 0x04001778 RID: 6008
		private byte[] ncBytes;

		// Token: 0x04001779 RID: 6009
		private NameConstraints nc;
	}
}
