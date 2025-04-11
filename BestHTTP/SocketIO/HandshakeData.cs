using System;
using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001B9 RID: 441
	public sealed class HandshakeData
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000A0E9D File Offset: 0x0009F09D
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x000A0EA5 File Offset: 0x0009F0A5
		public string Sid { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x000A0EAE File Offset: 0x0009F0AE
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x000A0EB6 File Offset: 0x0009F0B6
		public List<string> Upgrades { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000A0EBF File Offset: 0x0009F0BF
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x000A0EC7 File Offset: 0x0009F0C7
		public TimeSpan PingInterval { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x000A0ED0 File Offset: 0x0009F0D0
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x000A0ED8 File Offset: 0x0009F0D8
		public TimeSpan PingTimeout { get; private set; }

		// Token: 0x06001065 RID: 4197 RVA: 0x000A0EE4 File Offset: 0x0009F0E4
		public bool Parse(string str)
		{
			bool flag = false;
			Dictionary<string, object> from = Json.Decode(str, ref flag) as Dictionary<string, object>;
			if (!flag)
			{
				return false;
			}
			try
			{
				this.Sid = HandshakeData.GetString(from, "sid");
				this.Upgrades = HandshakeData.GetStringList(from, "upgrades");
				this.PingInterval = TimeSpan.FromMilliseconds((double)HandshakeData.GetInt(from, "pingInterval"));
				this.PingTimeout = TimeSpan.FromMilliseconds((double)HandshakeData.GetInt(from, "pingTimeout"));
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("HandshakeData", "Parse", ex);
				return false;
			}
			return true;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000A0F88 File Offset: 0x0009F188
		private static object Get(Dictionary<string, object> from, string key)
		{
			object result;
			if (!from.TryGetValue(key, out result))
			{
				throw new Exception(string.Format("Can't get {0} from Handshake data!", key));
			}
			return result;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000A0FB2 File Offset: 0x0009F1B2
		private static string GetString(Dictionary<string, object> from, string key)
		{
			return HandshakeData.Get(from, key) as string;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000A0FC0 File Offset: 0x0009F1C0
		private static List<string> GetStringList(Dictionary<string, object> from, string key)
		{
			List<object> list = HandshakeData.Get(from, key) as List<object>;
			List<string> list2 = new List<string>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i] as string;
				if (text != null)
				{
					list2.Add(text);
				}
			}
			return list2;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000A100F File Offset: 0x0009F20F
		private static int GetInt(Dictionary<string, object> from, string key)
		{
			return (int)((double)HandshakeData.Get(from, key));
		}
	}
}
