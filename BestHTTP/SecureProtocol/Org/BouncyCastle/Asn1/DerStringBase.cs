using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064C RID: 1612
	public abstract class DerStringBase : Asn1Object, IAsn1String
	{
		// Token: 0x06003C95 RID: 15509
		public abstract string GetString();

		// Token: 0x06003C96 RID: 15510 RVA: 0x00174685 File Offset: 0x00172885
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x0017468D File Offset: 0x0017288D
		protected override int Asn1GetHashCode()
		{
			return this.GetString().GetHashCode();
		}
	}
}
