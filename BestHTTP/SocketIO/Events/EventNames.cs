using System;

namespace BestHTTP.SocketIO.Events
{
	// Token: 0x020001CB RID: 459
	public static class EventNames
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x000A43BC File Offset: 0x000A25BC
		public static string GetNameFor(SocketIOEventTypes type)
		{
			return EventNames.SocketIONames[(int)(type + 1)];
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000A43C7 File Offset: 0x000A25C7
		public static string GetNameFor(TransportEventTypes transEvent)
		{
			return EventNames.TransportNames[(int)(transEvent + 1)];
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000A43D4 File Offset: 0x000A25D4
		public static bool IsBlacklisted(string eventName)
		{
			for (int i = 0; i < EventNames.BlacklistedEvents.Length; i++)
			{
				if (string.Compare(EventNames.BlacklistedEvents[i], eventName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040013B1 RID: 5041
		public const string Connect = "connect";

		// Token: 0x040013B2 RID: 5042
		public const string Disconnect = "disconnect";

		// Token: 0x040013B3 RID: 5043
		public const string Event = "event";

		// Token: 0x040013B4 RID: 5044
		public const string Ack = "ack";

		// Token: 0x040013B5 RID: 5045
		public const string Error = "error";

		// Token: 0x040013B6 RID: 5046
		public const string BinaryEvent = "binaryevent";

		// Token: 0x040013B7 RID: 5047
		public const string BinaryAck = "binaryack";

		// Token: 0x040013B8 RID: 5048
		private static string[] SocketIONames = new string[]
		{
			"unknown",
			"connect",
			"disconnect",
			"event",
			"ack",
			"error",
			"binaryevent",
			"binaryack"
		};

		// Token: 0x040013B9 RID: 5049
		private static string[] TransportNames = new string[]
		{
			"unknown",
			"open",
			"close",
			"ping",
			"pong",
			"message",
			"upgrade",
			"noop"
		};

		// Token: 0x040013BA RID: 5050
		private static string[] BlacklistedEvents = new string[]
		{
			"connect",
			"connect_error",
			"connect_timeout",
			"disconnect",
			"error",
			"reconnect",
			"reconnect_attempt",
			"reconnect_failed",
			"reconnect_error",
			"reconnecting"
		};
	}
}
