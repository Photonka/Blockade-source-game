using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000232 RID: 562
	public abstract class X509ExtensionBase : IX509Extension
	{
		// Token: 0x060014B7 RID: 5303
		protected abstract X509Extensions GetX509Extensions();

		// Token: 0x060014B8 RID: 5304 RVA: 0x000AEC38 File Offset: 0x000ACE38
		protected virtual ISet GetExtensionOids(bool critical)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				HashSet hashSet = new HashSet();
				foreach (object obj in x509Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					if (x509Extensions.GetExtension(derObjectIdentifier).IsCritical == critical)
					{
						hashSet.Add(derObjectIdentifier.Id);
					}
				}
				return hashSet;
			}
			return null;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000AECBC File Offset: 0x000ACEBC
		public virtual ISet GetNonCriticalExtensionOids()
		{
			return this.GetExtensionOids(false);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000AECC5 File Offset: 0x000ACEC5
		public virtual ISet GetCriticalExtensionOids()
		{
			return this.GetExtensionOids(true);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000AECCE File Offset: 0x000ACECE
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		public Asn1OctetString GetExtensionValue(string oid)
		{
			return this.GetExtensionValue(new DerObjectIdentifier(oid));
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000AECDC File Offset: 0x000ACEDC
		public virtual Asn1OctetString GetExtensionValue(DerObjectIdentifier oid)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				X509Extension extension = x509Extensions.GetExtension(oid);
				if (extension != null)
				{
					return extension.Value;
				}
			}
			return null;
		}
	}
}
