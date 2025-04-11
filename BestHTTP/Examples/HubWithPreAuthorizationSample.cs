using System;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using BestHTTP.SignalRCore.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200019A RID: 410
	public sealed class HubWithPreAuthorizationSample : MonoBehaviour
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x0009C51C File Offset: 0x0009A71C
		private void Start()
		{
			this.hub = new HubConnection(this.URI, new JsonProtocol(new LitJsonEncoder()));
			this.hub.AuthenticationProvider = new PreAuthAccessTokenAuthenticator(this.AuthURI);
			this.hub.AuthenticationProvider.OnAuthenticationSucceded += this.AuthenticationProvider_OnAuthenticationSucceded;
			this.hub.AuthenticationProvider.OnAuthenticationFailed += this.AuthenticationProvider_OnAuthenticationFailed;
			this.hub.OnConnected += this.Hub_OnConnected;
			this.hub.OnError += this.Hub_OnError;
			this.hub.OnClosed += this.Hub_OnClosed;
			this.hub.OnMessage += this.Hub_OnMessage;
			this.hub.StartConnect();
			this.uiText = "StartConnect called\n";
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0009C604 File Offset: 0x0009A804
		private void AuthenticationProvider_OnAuthenticationSucceded(IAuthenticationProvider provider)
		{
			string text = string.Format("Pre-Authentication Succeded! Token: '{0}' \n", (this.hub.AuthenticationProvider as PreAuthAccessTokenAuthenticator).Token);
			Debug.Log(text);
			this.uiText += text;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0009C649 File Offset: 0x0009A849
		private void AuthenticationProvider_OnAuthenticationFailed(IAuthenticationProvider provider, string reason)
		{
			this.uiText += string.Format("Authentication Failed! Reason: '{0}'\n", reason);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0009C667 File Offset: 0x0009A867
		private void OnDestroy()
		{
			if (this.hub != null)
			{
				this.hub.StartClose();
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0009C67C File Offset: 0x0009A87C
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

		// Token: 0x06000F41 RID: 3905 RVA: 0x0009C698 File Offset: 0x0009A898
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

		// Token: 0x06000F42 RID: 3906 RVA: 0x0006CF70 File Offset: 0x0006B170
		private bool Hub_OnMessage(HubConnection hub, Message message)
		{
			return true;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0009C6E6 File Offset: 0x0009A8E6
		private void Hub_OnClosed(HubConnection hub)
		{
			this.uiText += "Hub Closed\n";
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0009C6FE File Offset: 0x0009A8FE
		private void Hub_OnError(HubConnection hub, string error)
		{
			this.uiText = this.uiText + "Hub Error: " + error + "\n";
		}

		// Token: 0x040012AB RID: 4779
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/HubWithAuthorization");

		// Token: 0x040012AC RID: 4780
		private readonly Uri AuthURI = new Uri(GUIHelper.BaseURL + "/generateJwtToken");

		// Token: 0x040012AD RID: 4781
		private HubConnection hub;

		// Token: 0x040012AE RID: 4782
		private Vector2 scrollPos;

		// Token: 0x040012AF RID: 4783
		private string uiText;
	}
}
