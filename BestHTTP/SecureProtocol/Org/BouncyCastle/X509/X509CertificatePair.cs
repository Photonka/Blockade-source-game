using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022C RID: 556
	public class X509CertificatePair
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x000AD97E File Offset: 0x000ABB7E
		public X509CertificatePair(X509Certificate forward, X509Certificate reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000AD994 File Offset: 0x000ABB94
		public X509CertificatePair(CertificatePair pair)
		{
			if (pair.Forward != null)
			{
				this.forward = new X509Certificate(pair.Forward);
			}
			if (pair.Reverse != null)
			{
				this.reverse = new X509Certificate(pair.Reverse);
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000AD9D0 File Offset: 0x000ABBD0
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				X509CertificateStructure x509CertificateStructure = null;
				X509CertificateStructure x509CertificateStructure2 = null;
				if (this.forward != null)
				{
					x509CertificateStructure = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.forward.GetEncoded()));
					if (x509CertificateStructure == null)
					{
						throw new CertificateEncodingException("unable to get encoding for forward");
					}
				}
				if (this.reverse != null)
				{
					x509CertificateStructure2 = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.reverse.GetEncoded()));
					if (x509CertificateStructure2 == null)
					{
						throw new CertificateEncodingException("unable to get encoding for reverse");
					}
				}
				derEncoded = new CertificatePair(x509CertificateStructure, x509CertificateStructure2).GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CertificateEncodingException(ex.Message, ex);
			}
			return derEncoded;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x000ADA68 File Offset: 0x000ABC68
		public X509Certificate Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000ADA70 File Offset: 0x000ABC70
		public X509Certificate Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000ADA78 File Offset: 0x000ABC78
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && object.Equals(this.forward, x509CertificatePair.forward) && object.Equals(this.reverse, x509CertificatePair.reverse);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000ADAC0 File Offset: 0x000ABCC0
		public override int GetHashCode()
		{
			int num = -1;
			if (this.forward != null)
			{
				num ^= this.forward.GetHashCode();
			}
			if (this.reverse != null)
			{
				num *= 17;
				num ^= this.reverse.GetHashCode();
			}
			return num;
		}

		// Token: 0x040014E9 RID: 5353
		private readonly X509Certificate forward;

		// Token: 0x040014EA RID: 5354
		private readonly X509Certificate reverse;
	}
}
