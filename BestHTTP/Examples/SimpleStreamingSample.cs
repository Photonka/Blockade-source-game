using System;
using BestHTTP.SignalR;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000198 RID: 408
	public sealed class SimpleStreamingSample : MonoBehaviour
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0009C1E4 File Offset: 0x0009A3E4
		private void Start()
		{
			this.signalRConnection = new Connection(this.URI);
			this.signalRConnection.OnNonHubMessage += this.signalRConnection_OnNonHubMessage;
			this.signalRConnection.OnStateChanged += this.signalRConnection_OnStateChanged;
			this.signalRConnection.OnError += this.signalRConnection_OnError;
			this.signalRConnection.Open();
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0009C252 File Offset: 0x0009A452
		private void OnDestroy()
		{
			this.signalRConnection.Close();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0009C25F File Offset: 0x0009A45F
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.Label("Messages", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(20f);
				this.messages.Draw((float)(Screen.width - 20), 0f);
				GUILayout.EndHorizontal();
			});
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0009C278 File Offset: 0x0009A478
		private void signalRConnection_OnNonHubMessage(Connection connection, object data)
		{
			this.messages.Add("[Server Message] " + data.ToString());
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0009C295 File Offset: 0x0009A495
		private void signalRConnection_OnStateChanged(Connection connection, ConnectionStates oldState, ConnectionStates newState)
		{
			this.messages.Add(string.Format("[State Change] {0} => {1}", oldState, newState));
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0009C2B8 File Offset: 0x0009A4B8
		private void signalRConnection_OnError(Connection connection, string error)
		{
			this.messages.Add("[Error] " + error);
		}

		// Token: 0x040012A4 RID: 4772
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/streaming-connection");

		// Token: 0x040012A5 RID: 4773
		private Connection signalRConnection;

		// Token: 0x040012A6 RID: 4774
		private GUIMessageList messages = new GUIMessageList();
	}
}
