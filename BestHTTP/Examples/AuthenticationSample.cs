using System;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Authentication;
using BestHTTP.SignalR.Hubs;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000191 RID: 401
	public class AuthenticationSample : MonoBehaviour
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x0009A668 File Offset: 0x00098868
		private void Start()
		{
			this.signalRConnection = new Connection(this.URI, new Hub[]
			{
				new BaseHub("noauthhub", "Messages"),
				new BaseHub("invokeauthhub", "Messages Invoked By Admin or Invoker"),
				new BaseHub("authhub", "Messages Requiring Authentication to Send or Receive"),
				new BaseHub("inheritauthhub", "Messages Requiring Authentication to Send or Receive Because of Inheritance"),
				new BaseHub("incomingauthhub", "Messages Requiring Authentication to Send"),
				new BaseHub("adminauthhub", "Messages Requiring Admin Membership to Send or Receive"),
				new BaseHub("userandroleauthhub", "Messages Requiring Name to be \"User\" and Role to be \"Admin\" to Send or Receive")
			});
			if (!string.IsNullOrEmpty(this.userName) && !string.IsNullOrEmpty(this.role))
			{
				this.signalRConnection.AuthenticationProvider = new HeaderAuthenticator(this.userName, this.role);
			}
			this.signalRConnection.OnConnected += this.signalRConnection_OnConnected;
			this.signalRConnection.Open();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0009A762 File Offset: 0x00098962
		private void OnDestroy()
		{
			this.signalRConnection.Close();
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0009A76F File Offset: 0x0009896F
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				if (this.signalRConnection.AuthenticationProvider == null)
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label("Username (Enter 'User'):", Array.Empty<GUILayoutOption>());
					this.userName = GUILayout.TextField(this.userName, new GUILayoutOption[]
					{
						GUILayout.MinWidth(100f)
					});
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label("Roles (Enter 'Invoker' or 'Admin'):", Array.Empty<GUILayoutOption>());
					this.role = GUILayout.TextField(this.role, new GUILayoutOption[]
					{
						GUILayout.MinWidth(100f)
					});
					GUILayout.EndHorizontal();
					if (GUILayout.Button("Log in", Array.Empty<GUILayoutOption>()))
					{
						this.Restart();
					}
				}
				for (int i = 0; i < this.signalRConnection.Hubs.Length; i++)
				{
					(this.signalRConnection.Hubs[i] as BaseHub).Draw();
				}
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			});
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0009A788 File Offset: 0x00098988
		private void signalRConnection_OnConnected(Connection manager)
		{
			for (int i = 0; i < this.signalRConnection.Hubs.Length; i++)
			{
				(this.signalRConnection.Hubs[i] as BaseHub).InvokedFromClient();
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0009A7C4 File Offset: 0x000989C4
		private void Restart()
		{
			this.signalRConnection.OnConnected -= this.signalRConnection_OnConnected;
			this.signalRConnection.Close();
			this.signalRConnection = null;
			this.Start();
		}

		// Token: 0x04001277 RID: 4727
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/signalr");

		// Token: 0x04001278 RID: 4728
		private Connection signalRConnection;

		// Token: 0x04001279 RID: 4729
		private string userName = string.Empty;

		// Token: 0x0400127A RID: 4730
		private string role = string.Empty;

		// Token: 0x0400127B RID: 4731
		private Vector2 scrollPos;
	}
}
