using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200071C RID: 1820
	public abstract class IcaoObjectIdentifiers
	{
		// Token: 0x04002A5B RID: 10843
		public static readonly DerObjectIdentifier IdIcao = new DerObjectIdentifier("2.23.136");

		// Token: 0x04002A5C RID: 10844
		public static readonly DerObjectIdentifier IdIcaoMrtd = IcaoObjectIdentifiers.IdIcao.Branch("1");

		// Token: 0x04002A5D RID: 10845
		public static readonly DerObjectIdentifier IdIcaoMrtdSecurity = IcaoObjectIdentifiers.IdIcaoMrtd.Branch("1");

		// Token: 0x04002A5E RID: 10846
		public static readonly DerObjectIdentifier IdIcaoLdsSecurityObject = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("1");

		// Token: 0x04002A5F RID: 10847
		public static readonly DerObjectIdentifier IdIcaoCscaMasterList = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("2");

		// Token: 0x04002A60 RID: 10848
		public static readonly DerObjectIdentifier IdIcaoCscaMasterListSigningKey = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("3");

		// Token: 0x04002A61 RID: 10849
		public static readonly DerObjectIdentifier IdIcaoDocumentTypeList = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("4");

		// Token: 0x04002A62 RID: 10850
		public static readonly DerObjectIdentifier IdIcaoAAProtocolObject = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("5");

		// Token: 0x04002A63 RID: 10851
		public static readonly DerObjectIdentifier IdIcaoExtensions = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("6");

		// Token: 0x04002A64 RID: 10852
		public static readonly DerObjectIdentifier IdIcaoExtensionsNamechangekeyrollover = IcaoObjectIdentifiers.IdIcaoExtensions.Branch("1");
	}
}
