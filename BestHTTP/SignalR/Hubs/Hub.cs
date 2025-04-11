using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Hubs
{
	// Token: 0x02000211 RID: 529
	public class Hub : IHub
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x000AA2E9 File Offset: 0x000A84E9
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x000AA2F1 File Offset: 0x000A84F1
		public string Name { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x000AA2FA File Offset: 0x000A84FA
		public Dictionary<string, object> State
		{
			get
			{
				if (this.state == null)
				{
					this.state = new Dictionary<string, object>();
				}
				return this.state;
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600136C RID: 4972 RVA: 0x000AA318 File Offset: 0x000A8518
		// (remove) Token: 0x0600136D RID: 4973 RVA: 0x000AA350 File Offset: 0x000A8550
		public event OnMethodCallDelegate OnMethodCall;

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000AA385 File Offset: 0x000A8585
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x000AA38D File Offset: 0x000A858D
		Connection IHub.Connection { get; set; }

		// Token: 0x06001370 RID: 4976 RVA: 0x000AA396 File Offset: 0x000A8596
		public Hub(string name) : this(name, null)
		{
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000AA3A0 File Offset: 0x000A85A0
		public Hub(string name, Connection manager)
		{
			this.Name = name;
			((IHub)this).Connection = manager;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000AA3D7 File Offset: 0x000A85D7
		public void On(string method, OnMethodCallCallbackDelegate callback)
		{
			this.MethodTable[method] = callback;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000AA3E6 File Offset: 0x000A85E6
		public void Off(string method)
		{
			this.MethodTable[method] = null;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000AA3F5 File Offset: 0x000A85F5
		public bool Call(string method, params object[] args)
		{
			return this.Call(method, null, null, null, args);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000AA402 File Offset: 0x000A8602
		public bool Call(string method, OnMethodResultDelegate onResult, params object[] args)
		{
			return this.Call(method, onResult, null, null, args);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000AA40F File Offset: 0x000A860F
		public bool Call(string method, OnMethodResultDelegate onResult, OnMethodFailedDelegate onResultError, params object[] args)
		{
			return this.Call(method, onResult, onResultError, null, args);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000AA41D File Offset: 0x000A861D
		public bool Call(string method, OnMethodResultDelegate onResult, OnMethodProgressDelegate onProgress, params object[] args)
		{
			return this.Call(method, onResult, null, onProgress, args);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000AA42C File Offset: 0x000A862C
		public bool Call(string method, OnMethodResultDelegate onResult, OnMethodFailedDelegate onResultError, OnMethodProgressDelegate onProgress, params object[] args)
		{
			object syncRoot = ((IHub)this).Connection.SyncRoot;
			bool result;
			lock (syncRoot)
			{
				((IHub)this).Connection.ClientMessageCounter %= ulong.MaxValue;
				Connection connection = ((IHub)this).Connection;
				ulong clientMessageCounter = connection.ClientMessageCounter;
				connection.ClientMessageCounter = clientMessageCounter + 1UL;
				result = ((IHub)this).Call(new ClientMessage(this, method, args, clientMessageCounter, onResult, onResultError, onProgress));
			}
			return result;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000AA4B0 File Offset: 0x000A86B0
		bool IHub.Call(ClientMessage msg)
		{
			object syncRoot = ((IHub)this).Connection.SyncRoot;
			lock (syncRoot)
			{
				if (!((IHub)this).Connection.SendJson(this.BuildMessage(msg)))
				{
					return false;
				}
				this.SentMessages.Add(msg.CallIdx, msg);
			}
			return true;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000AA520 File Offset: 0x000A8720
		bool IHub.HasSentMessageId(ulong id)
		{
			return this.SentMessages.ContainsKey(id);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000AA52E File Offset: 0x000A872E
		void IHub.Close()
		{
			this.SentMessages.Clear();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000AA53C File Offset: 0x000A873C
		void IHub.OnMethod(MethodCallMessage msg)
		{
			this.MergeState(msg.State);
			if (this.OnMethodCall != null)
			{
				try
				{
					this.OnMethodCall(this, msg.Method, msg.Arguments);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("Hub - " + this.Name, "IHub.OnMethod - OnMethodCall", ex);
				}
			}
			OnMethodCallCallbackDelegate onMethodCallCallbackDelegate;
			if (this.MethodTable.TryGetValue(msg.Method, out onMethodCallCallbackDelegate) && onMethodCallCallbackDelegate != null)
			{
				try
				{
					onMethodCallCallbackDelegate(this, msg);
					return;
				}
				catch (Exception ex2)
				{
					HTTPManager.Logger.Exception("Hub - " + this.Name, "IHub.OnMethod - callback", ex2);
					return;
				}
			}
			if (this.OnMethodCall == null)
			{
				HTTPManager.Logger.Warning("Hub - " + this.Name, string.Format("[Client] {0}.{1} (args: {2})", this.Name, msg.Method, msg.Arguments.Length));
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000AA640 File Offset: 0x000A8840
		void IHub.OnMessage(IServerMessage msg)
		{
			ulong invocationId = (msg as IHubMessage).InvocationId;
			ClientMessage clientMessage;
			if (!this.SentMessages.TryGetValue(invocationId, out clientMessage))
			{
				HTTPManager.Logger.Warning("Hub - " + this.Name, "OnMessage - Sent message not found with id: " + invocationId.ToString());
				return;
			}
			switch (msg.Type)
			{
			case MessageTypes.Result:
			{
				ResultMessage resultMessage = msg as ResultMessage;
				this.MergeState(resultMessage.State);
				if (clientMessage.ResultCallback != null)
				{
					try
					{
						clientMessage.ResultCallback(this, clientMessage, resultMessage);
					}
					catch (Exception ex)
					{
						HTTPManager.Logger.Exception("Hub " + this.Name, "IHub.OnMessage - ResultCallback", ex);
					}
				}
				this.SentMessages.Remove(invocationId);
				return;
			}
			case MessageTypes.Failure:
			{
				FailureMessage failureMessage = msg as FailureMessage;
				this.MergeState(failureMessage.State);
				if (clientMessage.ResultErrorCallback != null)
				{
					try
					{
						clientMessage.ResultErrorCallback(this, clientMessage, failureMessage);
					}
					catch (Exception ex2)
					{
						HTTPManager.Logger.Exception("Hub " + this.Name, "IHub.OnMessage - ResultErrorCallback", ex2);
					}
				}
				this.SentMessages.Remove(invocationId);
				return;
			}
			case MessageTypes.MethodCall:
				break;
			case MessageTypes.Progress:
				if (clientMessage.ProgressCallback != null)
				{
					try
					{
						clientMessage.ProgressCallback(this, clientMessage, msg as ProgressMessage);
					}
					catch (Exception ex3)
					{
						HTTPManager.Logger.Exception("Hub " + this.Name, "IHub.OnMessage - ProgressCallback", ex3);
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000AA7E0 File Offset: 0x000A89E0
		private void MergeState(IDictionary<string, object> state)
		{
			if (state != null && state.Count > 0)
			{
				foreach (KeyValuePair<string, object> keyValuePair in state)
				{
					this.State[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000AA848 File Offset: 0x000A8A48
		private string BuildMessage(ClientMessage msg)
		{
			string result;
			try
			{
				this.builder.Append("{\"H\":\"");
				this.builder.Append(this.Name);
				this.builder.Append("\",\"M\":\"");
				this.builder.Append(msg.Method);
				this.builder.Append("\",\"A\":");
				string value = string.Empty;
				if (msg.Args != null && msg.Args.Length != 0)
				{
					value = ((IHub)this).Connection.JsonEncoder.Encode(msg.Args);
				}
				else
				{
					value = "[]";
				}
				this.builder.Append(value);
				this.builder.Append(",\"I\":\"");
				this.builder.Append(msg.CallIdx.ToString());
				this.builder.Append("\"");
				if (msg.Hub.state != null && msg.Hub.state.Count > 0)
				{
					this.builder.Append(",\"S\":");
					value = ((IHub)this).Connection.JsonEncoder.Encode(msg.Hub.state);
					this.builder.Append(value);
				}
				this.builder.Append("}");
				result = this.builder.ToString();
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("Hub - " + this.Name, "Send", ex);
				result = null;
			}
			finally
			{
				this.builder.Length = 0;
			}
			return result;
		}

		// Token: 0x040014A5 RID: 5285
		private Dictionary<string, object> state;

		// Token: 0x040014A7 RID: 5287
		private Dictionary<ulong, ClientMessage> SentMessages = new Dictionary<ulong, ClientMessage>();

		// Token: 0x040014A8 RID: 5288
		private Dictionary<string, OnMethodCallCallbackDelegate> MethodTable = new Dictionary<string, OnMethodCallCallbackDelegate>();

		// Token: 0x040014A9 RID: 5289
		private StringBuilder builder = new StringBuilder();
	}
}
