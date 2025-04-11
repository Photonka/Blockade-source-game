using System;
using System.Collections;
using System.Collections.Generic;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000442 RID: 1090
	public interface TlsClient : TlsPeer
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002B06 RID: 11014
		// (set) Token: 0x06002B07 RID: 11015
		List<string> HostNames { get; set; }

		// Token: 0x06002B08 RID: 11016
		void Init(TlsClientContext context);

		// Token: 0x06002B09 RID: 11017
		TlsSession GetSessionToResume();

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002B0A RID: 11018
		ProtocolVersion ClientHelloRecordLayerVersion { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002B0B RID: 11019
		ProtocolVersion ClientVersion { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002B0C RID: 11020
		bool IsFallback { get; }

		// Token: 0x06002B0D RID: 11021
		int[] GetCipherSuites();

		// Token: 0x06002B0E RID: 11022
		byte[] GetCompressionMethods();

		// Token: 0x06002B0F RID: 11023
		IDictionary GetClientExtensions();

		// Token: 0x06002B10 RID: 11024
		void NotifyServerVersion(ProtocolVersion selectedVersion);

		// Token: 0x06002B11 RID: 11025
		void NotifySessionID(byte[] sessionID);

		// Token: 0x06002B12 RID: 11026
		void NotifySelectedCipherSuite(int selectedCipherSuite);

		// Token: 0x06002B13 RID: 11027
		void NotifySelectedCompressionMethod(byte selectedCompressionMethod);

		// Token: 0x06002B14 RID: 11028
		void ProcessServerExtensions(IDictionary serverExtensions);

		// Token: 0x06002B15 RID: 11029
		void ProcessServerSupplementalData(IList serverSupplementalData);

		// Token: 0x06002B16 RID: 11030
		TlsKeyExchange GetKeyExchange();

		// Token: 0x06002B17 RID: 11031
		TlsAuthentication GetAuthentication();

		// Token: 0x06002B18 RID: 11032
		IList GetClientSupplementalData();

		// Token: 0x06002B19 RID: 11033
		void NotifyNewSessionTicket(NewSessionTicket newSessionTicket);
	}
}
