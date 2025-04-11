using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B9 RID: 697
	public class X509CertificateEntry : Pkcs12Entry
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x000C8379 File Offset: 0x000C6579
		public X509CertificateEntry(X509Certificate cert) : base(Platform.CreateHashtable())
		{
			this.cert = cert;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000C838D File Offset: 0x000C658D
		[Obsolete]
		public X509CertificateEntry(X509Certificate cert, Hashtable attributes) : base(attributes)
		{
			this.cert = cert;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000C838D File Offset: 0x000C658D
		public X509CertificateEntry(X509Certificate cert, IDictionary attributes) : base(attributes)
		{
			this.cert = cert;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000C839D File Offset: 0x000C659D
		public X509Certificate Certificate
		{
			get
			{
				return this.cert;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000C83A8 File Offset: 0x000C65A8
		public override bool Equals(object obj)
		{
			X509CertificateEntry x509CertificateEntry = obj as X509CertificateEntry;
			return x509CertificateEntry != null && this.cert.Equals(x509CertificateEntry.cert);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000C83D2 File Offset: 0x000C65D2
		public override int GetHashCode()
		{
			return ~this.cert.GetHashCode();
		}

		// Token: 0x0400178F RID: 6031
		private readonly X509Certificate cert;
	}
}
