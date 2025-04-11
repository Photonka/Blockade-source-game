using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using BestHTTP.SocketIO.Transports;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200019F RID: 415
	public sealed class SocketIOChatSample : MonoBehaviour
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0009D548 File Offset: 0x0009B748
		private void Start()
		{
			this.State = SocketIOChatSample.ChatStates.Login;
			SocketOptions socketOptions = new SocketOptions();
			socketOptions.AutoConnect = false;
			socketOptions.ConnectWith = TransportTypes.WebSocket;
			this.Manager = new SocketManager(new Uri("https://socket-io-chat.now.sh/socket.io/"), socketOptions);
			this.Manager.Socket.On("login", new SocketIOCallback(this.OnLogin));
			this.Manager.Socket.On("new message", new SocketIOCallback(this.OnNewMessage));
			this.Manager.Socket.On("user joined", new SocketIOCallback(this.OnUserJoined));
			this.Manager.Socket.On("user left", new SocketIOCallback(this.OnUserLeft));
			this.Manager.Socket.On("typing", new SocketIOCallback(this.OnTyping));
			this.Manager.Socket.On("stop typing", new SocketIOCallback(this.OnStopTyping));
			this.Manager.Socket.On(SocketIOEventTypes.Error, delegate(Socket socket, Packet packet, object[] args)
			{
				Debug.LogError(string.Format("Error: {0}", args[0].ToString()));
			});
			this.Manager.Open();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0009D687 File Offset: 0x0009B887
		private void OnDestroy()
		{
			this.Manager.Close();
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0009D694 File Offset: 0x0009B894
		private void Update()
		{
			if (Input.GetKeyDown(27))
			{
				SampleSelector.SelectedSample.DestroyUnityObject();
			}
			if (this.typing && DateTime.UtcNow - this.lastTypingTime >= this.TYPING_TIMER_LENGTH)
			{
				this.Manager.Socket.Emit("stop typing", Array.Empty<object>());
				this.typing = false;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0009D6FC File Offset: 0x0009B8FC
		private void OnGUI()
		{
			SocketIOChatSample.ChatStates state = this.State;
			if (state == SocketIOChatSample.ChatStates.Login)
			{
				this.DrawLoginScreen();
				return;
			}
			if (state != SocketIOChatSample.ChatStates.Chat)
			{
				return;
			}
			this.DrawChatScreen();
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0009D725 File Offset: 0x0009B925
		private void DrawLoginScreen()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				GUIHelper.DrawCenteredText("What's your nickname?");
				this.userName = GUILayout.TextField(this.userName, Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Join", Array.Empty<GUILayoutOption>()))
				{
					this.SetUserName();
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
			});
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0009D73E File Offset: 0x0009B93E
		private void DrawChatScreen()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, Array.Empty<GUILayoutOption>());
				GUILayout.Label(this.chatLog, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true),
					GUILayout.ExpandHeight(true)
				});
				GUILayout.EndScrollView();
				string text = string.Empty;
				if (this.typingUsers.Count > 0)
				{
					text += string.Format("{0}", this.typingUsers[0]);
					for (int i = 1; i < this.typingUsers.Count; i++)
					{
						text += string.Format(", {0}", this.typingUsers[i]);
					}
					if (this.typingUsers.Count == 1)
					{
						text += " is typing!";
					}
					else
					{
						text += " are typing!";
					}
				}
				GUILayout.Label(text, Array.Empty<GUILayoutOption>());
				GUILayout.Label("Type here:", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				this.message = GUILayout.TextField(this.message, Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Send", new GUILayoutOption[]
				{
					GUILayout.MaxWidth(100f)
				}))
				{
					this.SendMessage();
				}
				GUILayout.EndHorizontal();
				if (GUI.changed)
				{
					this.UpdateTyping();
				}
				GUILayout.EndVertical();
			});
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0009D757 File Offset: 0x0009B957
		private void SetUserName()
		{
			if (string.IsNullOrEmpty(this.userName))
			{
				return;
			}
			this.State = SocketIOChatSample.ChatStates.Chat;
			this.Manager.Socket.Emit("add user", new object[]
			{
				this.userName
			});
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0009D794 File Offset: 0x0009B994
		private void SendMessage()
		{
			if (string.IsNullOrEmpty(this.message))
			{
				return;
			}
			this.Manager.Socket.Emit("new message", new object[]
			{
				this.message
			});
			this.chatLog += string.Format("{0}: {1}\n", this.userName, this.message);
			this.message = string.Empty;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0009D806 File Offset: 0x0009BA06
		private void UpdateTyping()
		{
			if (!this.typing)
			{
				this.typing = true;
				this.Manager.Socket.Emit("typing", Array.Empty<object>());
			}
			this.lastTypingTime = DateTime.UtcNow;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0009D840 File Offset: 0x0009BA40
		private void addParticipantsMessage(Dictionary<string, object> data)
		{
			int num = Convert.ToInt32(data["numUsers"]);
			if (num == 1)
			{
				this.chatLog += "there's 1 participant\n";
				return;
			}
			this.chatLog = string.Concat(new object[]
			{
				this.chatLog,
				"there are ",
				num,
				" participants\n"
			});
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0009D8AC File Offset: 0x0009BAAC
		private void addChatMessage(Dictionary<string, object> data)
		{
			string arg = data["username"] as string;
			string arg2 = data["message"] as string;
			this.chatLog += string.Format("{0}: {1}\n", arg, arg2);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0009D8F8 File Offset: 0x0009BAF8
		private void AddChatTyping(Dictionary<string, object> data)
		{
			string item = data["username"] as string;
			this.typingUsers.Add(item);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0009D924 File Offset: 0x0009BB24
		private void RemoveChatTyping(Dictionary<string, object> data)
		{
			string username = data["username"] as string;
			int num = this.typingUsers.FindIndex((string name) => name.Equals(username));
			if (num != -1)
			{
				this.typingUsers.RemoveAt(num);
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0009D975 File Offset: 0x0009BB75
		private void OnLogin(Socket socket, Packet packet, params object[] args)
		{
			this.chatLog = "Welcome to Socket.IO Chat — \n";
			this.addParticipantsMessage(args[0] as Dictionary<string, object>);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0009D990 File Offset: 0x0009BB90
		private void OnNewMessage(Socket socket, Packet packet, params object[] args)
		{
			this.addChatMessage(args[0] as Dictionary<string, object>);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0009D9A0 File Offset: 0x0009BBA0
		private void OnUserJoined(Socket socket, Packet packet, params object[] args)
		{
			Dictionary<string, object> dictionary = args[0] as Dictionary<string, object>;
			string arg = dictionary["username"] as string;
			this.chatLog += string.Format("{0} joined\n", arg);
			this.addParticipantsMessage(dictionary);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0009D9EC File Offset: 0x0009BBEC
		private void OnUserLeft(Socket socket, Packet packet, params object[] args)
		{
			Dictionary<string, object> dictionary = args[0] as Dictionary<string, object>;
			string arg = dictionary["username"] as string;
			this.chatLog += string.Format("{0} left\n", arg);
			this.addParticipantsMessage(dictionary);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0009DA36 File Offset: 0x0009BC36
		private void OnTyping(Socket socket, Packet packet, params object[] args)
		{
			this.AddChatTyping(args[0] as Dictionary<string, object>);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0009DA46 File Offset: 0x0009BC46
		private void OnStopTyping(Socket socket, Packet packet, params object[] args)
		{
			this.RemoveChatTyping(args[0] as Dictionary<string, object>);
		}

		// Token: 0x040012BF RID: 4799
		private readonly TimeSpan TYPING_TIMER_LENGTH = TimeSpan.FromMilliseconds(700.0);

		// Token: 0x040012C0 RID: 4800
		private SocketManager Manager;

		// Token: 0x040012C1 RID: 4801
		private SocketIOChatSample.ChatStates State;

		// Token: 0x040012C2 RID: 4802
		private string userName = string.Empty;

		// Token: 0x040012C3 RID: 4803
		private string message = string.Empty;

		// Token: 0x040012C4 RID: 4804
		private string chatLog = string.Empty;

		// Token: 0x040012C5 RID: 4805
		private Vector2 scrollPos;

		// Token: 0x040012C6 RID: 4806
		private bool typing;

		// Token: 0x040012C7 RID: 4807
		private DateTime lastTypingTime = DateTime.MinValue;

		// Token: 0x040012C8 RID: 4808
		private List<string> typingUsers = new List<string>();

		// Token: 0x020008C2 RID: 2242
		private enum ChatStates
		{
			// Token: 0x0400334B RID: 13131
			Login,
			// Token: 0x0400334C RID: 13132
			Chat
		}
	}
}
