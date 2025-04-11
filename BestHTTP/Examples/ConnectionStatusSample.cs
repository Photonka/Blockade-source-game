using System;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Hubs;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000194 RID: 404
	public sealed class ConnectionStatusSample : MonoBehaviour
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0009B054 File Offset: 0x00099254
		private void Start()
		{
			this.signalRConnection = new Connection(this.URI, new string[]
			{
				"StatusHub"
			});
			this.signalRConnection.OnNonHubMessage += this.signalRConnection_OnNonHubMessage;
			this.signalRConnection.OnError += this.signalRConnection_OnError;
			this.signalRConnection.OnStateChanged += this.signalRConnection_OnStateChanged;
			this.signalRConnection["StatusHub"].OnMethodCall += this.statusHub_OnMethodCall;
			this.signalRConnection.Open();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0009B0F1 File Offset: 0x000992F1
		private void OnDestroy()
		{
			this.signalRConnection.Close();
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0009B0FE File Offset: 0x000992FE
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("START", Array.Empty<GUILayoutOption>()) && this.signalRConnection.State != ConnectionStates.Connected)
				{
					this.signalRConnection.Open();
				}
				if (GUILayout.Button("STOP", Array.Empty<GUILayoutOption>()) && this.signalRConnection.State == ConnectionStates.Connected)
				{
					this.signalRConnection.Close();
					this.messages.Clear();
				}
				if (GUILayout.Button("PING", Array.Empty<GUILayoutOption>()) && this.signalRConnection.State == ConnectionStates.Connected)
				{
					this.signalRConnection["StatusHub"].Call("Ping", Array.Empty<object>());
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(20f);
				GUILayout.Label("Connection Status Messages", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(20f);
				this.messages.Draw((float)(Screen.width - 20), 0f);
				GUILayout.EndHorizontal();
			});
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0009B117 File Offset: 0x00099317
		private void signalRConnection_OnNonHubMessage(Connection manager, object data)
		{
			this.messages.Add("[Server Message] " + data.ToString());
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0009B134 File Offset: 0x00099334
		private void signalRConnection_OnStateChanged(Connection manager, ConnectionStates oldState, ConnectionStates newState)
		{
			this.messages.Add(string.Format("[State Change] {0} => {1}", oldState, newState));
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0009B157 File Offset: 0x00099357
		private void signalRConnection_OnError(Connection manager, string error)
		{
			this.messages.Add("[Error] " + error);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0009B170 File Offset: 0x00099370
		private void statusHub_OnMethodCall(Hub hub, string method, params object[] args)
		{
			string arg = (args.Length != 0) ? (args[0] as string) : string.Empty;
			string arg2 = (args.Length > 1) ? args[1].ToString() : string.Empty;
			if (method == "joined")
			{
				this.messages.Add(string.Format("[{0}] {1} joined at {2}", hub.Name, arg, arg2));
				return;
			}
			if (method == "rejoined")
			{
				this.messages.Add(string.Format("[{0}] {1} reconnected at {2}", hub.Name, arg, arg2));
				return;
			}
			if (!(method == "leave"))
			{
				this.messages.Add(string.Format("[{0}] {1}", hub.Name, method));
				return;
			}
			this.messages.Add(string.Format("[{0}] {1} leaved at {2}", hub.Name, arg, arg2));
		}

		// Token: 0x04001285 RID: 4741
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/signalr");

		// Token: 0x04001286 RID: 4742
		private Connection signalRConnection;

		// Token: 0x04001287 RID: 4743
		private GUIMessageList messages = new GUIMessageList();
	}
}
