using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006AF RID: 1711
	public class X509ExtensionsGenerator
	{
		// Token: 0x06003F7D RID: 16253 RVA: 0x0017D4E9 File Offset: 0x0017B6E9
		public void Reset()
		{
			this.extensions = Platform.CreateHashtable();
			this.extOrdering = Platform.CreateArrayList();
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0017D504 File Offset: 0x0017B704
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extValue)
		{
			byte[] derEncoded;
			try
			{
				derEncoded = extValue.GetDerEncoded();
			}
			catch (Exception arg)
			{
				throw new ArgumentException("error encoding value: " + arg);
			}
			this.AddExtension(oid, critical, derEncoded);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0017D548 File Offset: 0x0017B748
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extValue)
		{
			if (this.extensions.Contains(oid))
			{
				throw new ArgumentException("extension " + oid + " already added");
			}
			this.extOrdering.Add(oid);
			this.extensions.Add(oid, new X509Extension(critical, new DerOctetString(extValue)));
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x0017D59E File Offset: 0x0017B79E
		public bool IsEmpty
		{
			get
			{
				return this.extOrdering.Count < 1;
			}
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0017D5AE File Offset: 0x0017B7AE
		public X509Extensions Generate()
		{
			return new X509Extensions(this.extOrdering, this.extensions);
		}

		// Token: 0x0400274A RID: 10058
		private IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x0400274B RID: 10059
		private IList extOrdering = Platform.CreateArrayList();
	}
}
