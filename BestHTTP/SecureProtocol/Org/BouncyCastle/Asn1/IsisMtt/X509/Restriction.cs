using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000717 RID: 1815
	public class Restriction : Asn1Encodable
	{
		// Token: 0x0600423B RID: 16955 RVA: 0x00188CD8 File Offset: 0x00186ED8
		public static Restriction GetInstance(object obj)
		{
			if (obj is Restriction)
			{
				return (Restriction)obj;
			}
			if (obj is IAsn1String)
			{
				return new Restriction(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00188D17 File Offset: 0x00186F17
		private Restriction(DirectoryString restriction)
		{
			this.restriction = restriction;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x00188D26 File Offset: 0x00186F26
		public Restriction(string restriction)
		{
			this.restriction = new DirectoryString(restriction);
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x00188D3A File Offset: 0x00186F3A
		public virtual DirectoryString RestrictionString
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00188D42 File Offset: 0x00186F42
		public override Asn1Object ToAsn1Object()
		{
			return this.restriction.ToAsn1Object();
		}

		// Token: 0x04002A51 RID: 10833
		private readonly DirectoryString restriction;
	}
}
