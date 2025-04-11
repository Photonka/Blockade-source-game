using System;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using BestHTTP.SignalRCore.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000199 RID: 409
	public sealed class HubWithAuthorizationSample : MonoBehaviour
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0009C350 File Offset: 0x0009A550
		private void Start()
		{
			this.hub = new HubConnection(this.URI, new JsonProtocol(new LitJsonEncoder()));
			this.hub.OnConnected += this.Hub_OnConnected;
			this.hub.OnError += this.Hub_OnError;
			this.hub.OnClosed += this.Hub_OnClosed;
			this.hub.OnMessage += this.Hub_OnMessage;
			this.hub.StartConnect();
			this.uiText = "StartConnect called\n";
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0009C3EA File Offset: 0x0009A5EA
		private void OnDestroy()
		{
			if (this.hub != null)
			{
				this.hub.StartClose();
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0009C3FF File Offset: 0x0009A5FF
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.Label(this.uiText, Array.Empty<GUILayoutOption>());
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			});
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0009C418 File Offset: 0x0009A618
		private void Hub_OnConnected(HubConnection hub)
		{
			this.uiText += "Hub Connected\n";
			hub.Invoke<string>("Echo", new object[]
			{
				"Message from the client"
			}).OnSuccess(delegate(string ret)
			{
				this.uiText += string.Format(" 'Echo' returned: '{0}'\n", ret);
			});
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0006CF70 File Offset: 0x0006B170
		private bool Hub_OnMessage(HubConnection hub, Message message)
		{
			return true;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0009C466 File Offset: 0x0009A666
		private void Hub_OnClosed(HubConnection hub)
		{
			this.uiText += "Hub Closed\n";
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0009C47E File Offset: 0x0009A67E
		private void Hub_OnError(HubConnection hub, string error)
		{
			this.uiText = this.uiText + "Hub Error: " + error + "\n";
		}

		// Token: 0x040012A7 RID: 4775
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/redirect");

		// Token: 0x040012A8 RID: 4776
		private HubConnection hub;

		// Token: 0x040012A9 RID: 4777
		private Vector2 scrollPos;

		// Token: 0x040012AA RID: 4778
		private string uiText;
	}
}
