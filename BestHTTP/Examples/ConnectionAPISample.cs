using System;
using BestHTTP.Cookies;
using BestHTTP.JSON;
using BestHTTP.SignalR;
using BestHTTP.SignalR.JsonEncoders;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000193 RID: 403
	public sealed class ConnectionAPISample : MonoBehaviour
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x0009AB64 File Offset: 0x00098D64
		private void Start()
		{
			if (PlayerPrefs.HasKey("userName"))
			{
				CookieJar.Set(this.URI, new Cookie("user", PlayerPrefs.GetString("userName")));
			}
			this.signalRConnection = new Connection(this.URI);
			this.signalRConnection.JsonEncoder = new LitJsonEncoder();
			this.signalRConnection.OnStateChanged += this.signalRConnection_OnStateChanged;
			this.signalRConnection.OnNonHubMessage += this.signalRConnection_OnGeneralMessage;
			this.signalRConnection.Open();
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0009ABF6 File Offset: 0x00098DF6
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.Label("To Everybody", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				this.ToEveryBodyText = GUILayout.TextField(this.ToEveryBodyText, new GUILayoutOption[]
				{
					GUILayout.MinWidth(100f)
				});
				if (GUILayout.Button("Broadcast", Array.Empty<GUILayoutOption>()))
				{
					this.Broadcast(this.ToEveryBodyText);
				}
				if (GUILayout.Button("Broadcast (All Except Me)", Array.Empty<GUILayoutOption>()))
				{
					this.BroadcastExceptMe(this.ToEveryBodyText);
				}
				if (GUILayout.Button("Enter Name", Array.Empty<GUILayoutOption>()))
				{
					this.EnterName(this.ToEveryBodyText);
				}
				if (GUILayout.Button("Join Group", Array.Empty<GUILayoutOption>()))
				{
					this.JoinGroup(this.ToEveryBodyText);
				}
				if (GUILayout.Button("Leave Group", Array.Empty<GUILayoutOption>()))
				{
					this.LeaveGroup(this.ToEveryBodyText);
				}
				GUILayout.EndHorizontal();
				GUILayout.Label("To Me", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				this.ToMeText = GUILayout.TextField(this.ToMeText, new GUILayoutOption[]
				{
					GUILayout.MinWidth(100f)
				});
				if (GUILayout.Button("Send to me", Array.Empty<GUILayoutOption>()))
				{
					this.SendToMe(this.ToMeText);
				}
				GUILayout.EndHorizontal();
				GUILayout.Label("Private Message", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label("Message:", Array.Empty<GUILayoutOption>());
				this.PrivateMessageText = GUILayout.TextField(this.PrivateMessageText, new GUILayoutOption[]
				{
					GUILayout.MinWidth(100f)
				});
				GUILayout.Label("User or Group name:", Array.Empty<GUILayoutOption>());
				this.PrivateMessageUserOrGroupName = GUILayout.TextField(this.PrivateMessageUserOrGroupName, new GUILayoutOption[]
				{
					GUILayout.MinWidth(100f)
				});
				if (GUILayout.Button("Send to user", Array.Empty<GUILayoutOption>()))
				{
					this.SendToUser(this.PrivateMessageUserOrGroupName, this.PrivateMessageText);
				}
				if (GUILayout.Button("Send to group", Array.Empty<GUILayoutOption>()))
				{
					this.SendToGroup(this.PrivateMessageUserOrGroupName, this.PrivateMessageText);
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(20f);
				if (this.signalRConnection.State == ConnectionStates.Closed)
				{
					if (GUILayout.Button("Start Connection", Array.Empty<GUILayoutOption>()))
					{
						this.signalRConnection.Open();
					}
				}
				else if (GUILayout.Button("Stop Connection", Array.Empty<GUILayoutOption>()))
				{
					this.signalRConnection.Close();
				}
				GUILayout.Space(20f);
				GUILayout.Label("Messages", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(20f);
				this.messages.Draw((float)(Screen.width - 20), 0f);
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			});
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0009AC0F File Offset: 0x00098E0F
		private void OnDestroy()
		{
			this.signalRConnection.Close();
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0009AC1C File Offset: 0x00098E1C
		private void signalRConnection_OnGeneralMessage(Connection manager, object data)
		{
			string str = Json.Encode(data);
			this.messages.Add("[Server Message] " + str);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0009AC46 File Offset: 0x00098E46
		private void signalRConnection_OnStateChanged(Connection manager, ConnectionStates oldState, ConnectionStates newState)
		{
			this.messages.Add(string.Format("[State Change] {0} => {1}", oldState.ToString(), newState.ToString()));
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0009AC77 File Offset: 0x00098E77
		private void Broadcast(string text)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.Broadcast,
				Value = text
			});
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0009AC8C File Offset: 0x00098E8C
		private void BroadcastExceptMe(string text)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.BroadcastExceptMe,
				Value = text
			});
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0009ACA1 File Offset: 0x00098EA1
		private void EnterName(string name)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.Join,
				Value = name
			});
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0009ACB6 File Offset: 0x00098EB6
		private void JoinGroup(string groupName)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.AddToGroup,
				Value = groupName
			});
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0009ACCB File Offset: 0x00098ECB
		private void LeaveGroup(string groupName)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.RemoveFromGroup,
				Value = groupName
			});
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0009ACE0 File Offset: 0x00098EE0
		private void SendToMe(string text)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.Send,
				Value = text
			});
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0009ACF5 File Offset: 0x00098EF5
		private void SendToUser(string userOrGroupName, string text)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.PrivateMessage,
				Value = string.Format("{0}|{1}", userOrGroupName, text)
			});
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0009AD15 File Offset: 0x00098F15
		private void SendToGroup(string userOrGroupName, string text)
		{
			this.signalRConnection.Send(new
			{
				Type = ConnectionAPISample.MessageTypes.SendToGroup,
				Value = string.Format("{0}|{1}", userOrGroupName, text)
			});
		}

		// Token: 0x0400127E RID: 4734
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/raw-connection/");

		// Token: 0x0400127F RID: 4735
		private Connection signalRConnection;

		// Token: 0x04001280 RID: 4736
		private string ToEveryBodyText = string.Empty;

		// Token: 0x04001281 RID: 4737
		private string ToMeText = string.Empty;

		// Token: 0x04001282 RID: 4738
		private string PrivateMessageText = string.Empty;

		// Token: 0x04001283 RID: 4739
		private string PrivateMessageUserOrGroupName = string.Empty;

		// Token: 0x04001284 RID: 4740
		private GUIMessageList messages = new GUIMessageList();

		// Token: 0x020008C0 RID: 2240
		private enum MessageTypes
		{
			// Token: 0x04003340 RID: 13120
			Send,
			// Token: 0x04003341 RID: 13121
			Broadcast,
			// Token: 0x04003342 RID: 13122
			Join,
			// Token: 0x04003343 RID: 13123
			PrivateMessage,
			// Token: 0x04003344 RID: 13124
			AddToGroup,
			// Token: 0x04003345 RID: 13125
			RemoveFromGroup,
			// Token: 0x04003346 RID: 13126
			SendToGroup,
			// Token: 0x04003347 RID: 13127
			BroadcastExceptMe
		}
	}
}
