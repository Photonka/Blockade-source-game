using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020006B6 RID: 1718
	public sealed class SigIObjectIdentifiers
	{
		// Token: 0x06003FBF RID: 16319 RVA: 0x00023EF4 File Offset: 0x000220F4
		private SigIObjectIdentifiers()
		{
		}

		// Token: 0x0400279B RID: 10139
		public static readonly DerObjectIdentifier IdSigI = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x0400279C RID: 10140
		public static readonly DerObjectIdentifier IdSigIKP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".2");

		// Token: 0x0400279D RID: 10141
		public static readonly DerObjectIdentifier IdSigICP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".1");

		// Token: 0x0400279E RID: 10142
		public static readonly DerObjectIdentifier IdSigION = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".4");

		// Token: 0x0400279F RID: 10143
		public static readonly DerObjectIdentifier IdSigIKPDirectoryService = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigIKP + ".1");

		// Token: 0x040027A0 RID: 10144
		public static readonly DerObjectIdentifier IdSigIONPersonalData = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigION + ".1");

		// Token: 0x040027A1 RID: 10145
		public static readonly DerObjectIdentifier IdSigICPSigConform = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigICP + ".1");
	}
}
