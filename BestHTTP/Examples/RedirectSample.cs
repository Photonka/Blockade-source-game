using System;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using BestHTTP.SignalRCore.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200019C RID: 412
	public sealed class RedirectSample : MonoBehaviour
	{
		// Token: 0x06000F55 RID: 3925 RVA: 0x0009CA68 File Offset: 0x0009AC68
		private void Start()
		{
			this.hub = new HubConnection(this.URI, new JsonProtocol(new LitJsonEncoder()));
			this.hub.AuthenticationProvider = new RedirectLoggerAccessTokenAuthenticator(this.hub);
			this.hub.OnConnected += this.Hub_OnConnected;
			this.hub.OnError += this.Hub_OnError;
			this.hub.OnClosed += this.Hub_OnClosed;
			this.hub.OnMessage += this.Hub_OnMessage;
			this.hub.OnRedirected += this.Hub_Redirected;
			this.hub.StartConnect();
			this.uiText = "StartConnect called\n";
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0009CB2F File Offset: 0x0009AD2F
		private void OnDestroy()
		{
			if (this.hub != null)
			{
				this.hub.StartClose();
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0009CB44 File Offset: 0x0009AD44
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

		// Token: 0x06000F58 RID: 3928 RVA: 0x0009CB5D File Offset: 0x0009AD5D
		private void Hub_Redirected(HubConnection hub, Uri oldUri, Uri newUri)
		{
			this.uiText += string.Format("Hub connection redirected to '<color=green>{0}</color>'!\n", hub.Uri);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0009CB80 File Offset: 0x0009AD80
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

		// Token: 0x06000F5A RID: 3930 RVA: 0x0006CF70 File Offset: 0x0006B170
		private bool Hub_OnMessage(HubConnection hub, Message message)
		{
			return true;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0009CBCE File Offset: 0x0009ADCE
		private void Hub_OnClosed(HubConnection hub)
		{
			this.uiText += "Hub Closed\n";
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0009CBE6 File Offset: 0x0009ADE6
		private void Hub_OnError(HubConnection hub, string error)
		{
			this.uiText = this.uiText + "Hub Error: " + error + "\n";
		}

		// Token: 0x040012B4 RID: 4788
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/redirect_sample");

		// Token: 0x040012B5 RID: 4789
		public HubConnection hub;

		// Token: 0x040012B6 RID: 4790
		private Vector2 scrollPos;

		// Token: 0x040012B7 RID: 4791
		public string uiText;
	}
}
