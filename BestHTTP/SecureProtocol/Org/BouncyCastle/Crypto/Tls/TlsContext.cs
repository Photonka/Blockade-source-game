using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000447 RID: 1095
	public interface TlsContext
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002B2D RID: 11053
		IRandomGenerator NonceRandomGenerator { get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002B2E RID: 11054
		SecureRandom SecureRandom { get; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002B2F RID: 11055
		SecurityParameters SecurityParameters { get; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002B30 RID: 11056
		bool IsServer { get; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002B31 RID: 11057
		ProtocolVersion ClientVersion { get; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002B32 RID: 11058
		ProtocolVersion ServerVersion { get; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002B33 RID: 11059
		TlsSession ResumableSession { get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002B34 RID: 11060
		// (set) Token: 0x06002B35 RID: 11061
		object UserObject { get; set; }

		// Token: 0x06002B36 RID: 11062
		byte[] ExportKeyingMaterial(string asciiLabel, byte[] context_value, int length);
	}
}
