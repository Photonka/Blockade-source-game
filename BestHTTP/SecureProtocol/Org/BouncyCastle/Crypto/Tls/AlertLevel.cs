using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E6 RID: 998
	public abstract class AlertLevel
	{
		// Token: 0x060028DA RID: 10458 RVA: 0x0010F67E File Offset: 0x0010D87E
		public static string GetName(byte alertDescription)
		{
			if (alertDescription == 1)
			{
				return "warning";
			}
			if (alertDescription != 2)
			{
				return "UNKNOWN";
			}
			return "fatal";
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0010F69B File Offset: 0x0010D89B
		public static string GetText(byte alertDescription)
		{
			return string.Concat(new object[]
			{
				AlertLevel.GetName(alertDescription),
				"(",
				alertDescription,
				")"
			});
		}

		// Token: 0x04001A17 RID: 6679
		public const byte warning = 1;

		// Token: 0x04001A18 RID: 6680
		public const byte fatal = 2;
	}
}
