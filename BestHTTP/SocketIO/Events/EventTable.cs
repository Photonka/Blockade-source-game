using System;
using System.Collections.Generic;
using BestHTTP.Logger;

namespace BestHTTP.SocketIO.Events
{
	// Token: 0x020001CC RID: 460
	internal sealed class EventTable
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000A4508 File Offset: 0x000A2708
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x000A4510 File Offset: 0x000A2710
		private Socket Socket { get; set; }

		// Token: 0x0600115C RID: 4444 RVA: 0x000A4519 File Offset: 0x000A2719
		public EventTable(Socket socket)
		{
			this.Socket = socket;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000A4534 File Offset: 0x000A2734
		public void Register(string eventName, SocketIOCallback callback, bool onlyOnce, bool autoDecodePayload)
		{
			List<EventDescriptor> list;
			if (!this.Table.TryGetValue(eventName, out list))
			{
				this.Table.Add(eventName, list = new List<EventDescriptor>(1));
			}
			EventDescriptor eventDescriptor = list.Find((EventDescriptor d) => d.OnlyOnce == onlyOnce && d.AutoDecodePayload == autoDecodePayload);
			if (eventDescriptor == null)
			{
				list.Add(new EventDescriptor(onlyOnce, autoDecodePayload, callback));
				return;
			}
			eventDescriptor.Callbacks.Add(callback);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000A45B5 File Offset: 0x000A27B5
		public void Unregister(string eventName)
		{
			this.Table.Remove(eventName);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000A45C4 File Offset: 0x000A27C4
		public void Unregister(string eventName, SocketIOCallback callback)
		{
			List<EventDescriptor> list;
			if (this.Table.TryGetValue(eventName, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Callbacks.Remove(callback);
				}
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000A4608 File Offset: 0x000A2808
		public void Call(string eventName, Packet packet, params object[] args)
		{
			if (HTTPManager.Logger.Level <= Loglevels.All)
			{
				HTTPManager.Logger.Verbose("EventTable", "Call - " + eventName);
			}
			List<EventDescriptor> list;
			if (this.Table.TryGetValue(eventName, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Call(this.Socket, packet, args);
				}
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000A4674 File Offset: 0x000A2874
		public void Call(Packet packet)
		{
			string text = packet.DecodeEventName();
			string text2 = (packet.SocketIOEvent != SocketIOEventTypes.Unknown) ? EventNames.GetNameFor(packet.SocketIOEvent) : EventNames.GetNameFor(packet.TransportEvent);
			object[] args = null;
			if (!this.HasSubsciber(text) && !this.HasSubsciber(text2))
			{
				return;
			}
			if (packet.TransportEvent == TransportEventTypes.Message && (packet.SocketIOEvent == SocketIOEventTypes.Event || packet.SocketIOEvent == SocketIOEventTypes.BinaryEvent) && this.ShouldDecodePayload(text))
			{
				args = packet.Decode(this.Socket.Manager.Encoder);
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.Call(text, packet, args);
			}
			if (!packet.IsDecoded && this.ShouldDecodePayload(text2))
			{
				args = packet.Decode(this.Socket.Manager.Encoder);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				this.Call(text2, packet, args);
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000A4744 File Offset: 0x000A2944
		public void Clear()
		{
			this.Table.Clear();
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000A4754 File Offset: 0x000A2954
		private bool ShouldDecodePayload(string eventName)
		{
			List<EventDescriptor> list;
			if (this.Table.TryGetValue(eventName, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].AutoDecodePayload && list[i].Callbacks.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000A47A7 File Offset: 0x000A29A7
		private bool HasSubsciber(string eventName)
		{
			return this.Table.ContainsKey(eventName);
		}

		// Token: 0x040013BC RID: 5052
		private Dictionary<string, List<EventDescriptor>> Table = new Dictionary<string, List<EventDescriptor>>();
	}
}
