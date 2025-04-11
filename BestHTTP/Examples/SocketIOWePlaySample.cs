using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x020001A0 RID: 416
	public sealed class SocketIOWePlaySample : MonoBehaviour
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x0009DC70 File Offset: 0x0009BE70
		private void Start()
		{
			SocketOptions socketOptions = new SocketOptions();
			socketOptions.AutoConnect = false;
			SocketManager socketManager = new SocketManager(new Uri("http://io.weplay.io/socket.io/"), socketOptions);
			this.Socket = socketManager.Socket;
			this.Socket.On(SocketIOEventTypes.Connect, new SocketIOCallback(this.OnConnected));
			this.Socket.On("joined", new SocketIOCallback(this.OnJoined));
			this.Socket.On("connections", new SocketIOCallback(this.OnConnections));
			this.Socket.On("join", new SocketIOCallback(this.OnJoin));
			this.Socket.On("move", new SocketIOCallback(this.OnMove));
			this.Socket.On("message", new SocketIOCallback(this.OnMessage));
			this.Socket.On("reload", new SocketIOCallback(this.OnReload));
			this.Socket.On("frame", new SocketIOCallback(this.OnFrame), false);
			this.Socket.On(SocketIOEventTypes.Error, new SocketIOCallback(this.OnError));
			socketManager.Open();
			this.State = SocketIOWePlaySample.States.Connecting;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0009DDA9 File Offset: 0x0009BFA9
		private void OnDestroy()
		{
			this.Socket.Manager.Close();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0009DDBB File Offset: 0x0009BFBB
		private void Update()
		{
			if (Input.GetKeyDown(27))
			{
				SampleSelector.SelectedSample.DestroyUnityObject();
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0009DDD0 File Offset: 0x0009BFD0
		private void OnGUI()
		{
			switch (this.State)
			{
			case SocketIOWePlaySample.States.Connecting:
				GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
				{
					GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
					GUILayout.FlexibleSpace();
					GUIHelper.DrawCenteredText("Connecting to the server...");
					GUILayout.FlexibleSpace();
					GUILayout.EndVertical();
				});
				return;
			case SocketIOWePlaySample.States.WaitForNick:
				GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
				{
					this.DrawLoginScreen();
				});
				return;
			case SocketIOWePlaySample.States.Joined:
				GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
				{
					if (this.FrameTexture != null)
					{
						GUILayout.Box(this.FrameTexture, Array.Empty<GUILayoutOption>());
					}
					this.DrawControls();
					this.DrawChat(true);
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0009DE54 File Offset: 0x0009C054
		private void DrawLoginScreen()
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUIHelper.DrawCenteredText("What's your nickname?");
			this.Nick = GUILayout.TextField(this.Nick, Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("Join", Array.Empty<GUILayoutOption>()))
			{
				this.Join();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0009DEB4 File Offset: 0x0009C0B4
		private void DrawControls()
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label("Controls:", Array.Empty<GUILayoutOption>());
			for (int i = 0; i < this.controls.Length; i++)
			{
				if (GUILayout.Button(this.controls[i], Array.Empty<GUILayoutOption>()))
				{
					this.Socket.Emit("move", new object[]
					{
						this.controls[i]
					});
				}
			}
			GUILayout.Label(" Connections: " + this.connections, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0009DF48 File Offset: 0x0009C148
		private void DrawChat(bool withInput = true)
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, Array.Empty<GUILayoutOption>());
			for (int i = 0; i < this.messages.Count; i++)
			{
				GUILayout.Label(this.messages[i], new GUILayoutOption[]
				{
					GUILayout.MinWidth((float)Screen.width)
				});
			}
			GUILayout.EndScrollView();
			if (withInput)
			{
				GUILayout.Label("Your message: ", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				this.messageToSend = GUILayout.TextField(this.messageToSend, Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Send", new GUILayoutOption[]
				{
					GUILayout.MaxWidth(100f)
				}))
				{
					this.SendMessage();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0009E018 File Offset: 0x0009C218
		private void AddMessage(string msg)
		{
			this.messages.Insert(0, msg);
			if (this.messages.Count > this.MaxMessages)
			{
				this.messages.RemoveRange(this.MaxMessages, this.messages.Count - this.MaxMessages);
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0009E068 File Offset: 0x0009C268
		private void SendMessage()
		{
			if (string.IsNullOrEmpty(this.messageToSend))
			{
				return;
			}
			this.Socket.Emit("message", new object[]
			{
				this.messageToSend
			});
			this.AddMessage(string.Format("{0}: {1}", this.Nick, this.messageToSend));
			this.messageToSend = string.Empty;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0009E0CA File Offset: 0x0009C2CA
		private void Join()
		{
			PlayerPrefs.SetString("Nick", this.Nick);
			this.Socket.Emit("join", new object[]
			{
				this.Nick
			});
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0009E0FC File Offset: 0x0009C2FC
		private void Reload()
		{
			this.FrameTexture = null;
			if (this.Socket != null)
			{
				this.Socket.Manager.Close();
				this.Socket = null;
				this.Start();
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0009E12A File Offset: 0x0009C32A
		private void OnConnected(Socket socket, Packet packet, params object[] args)
		{
			if (PlayerPrefs.HasKey("Nick"))
			{
				this.Nick = PlayerPrefs.GetString("Nick", "NickName");
				this.Join();
			}
			else
			{
				this.State = SocketIOWePlaySample.States.WaitForNick;
			}
			this.AddMessage("connected");
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0009E167 File Offset: 0x0009C367
		private void OnJoined(Socket socket, Packet packet, params object[] args)
		{
			this.State = SocketIOWePlaySample.States.Joined;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0009E170 File Offset: 0x0009C370
		private void OnReload(Socket socket, Packet packet, params object[] args)
		{
			this.Reload();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0009E178 File Offset: 0x0009C378
		private void OnMessage(Socket socket, Packet packet, params object[] args)
		{
			if (args.Length == 1)
			{
				this.AddMessage(args[0] as string);
				return;
			}
			this.AddMessage(string.Format("{0}: {1}", args[1], args[0]));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0009E1A5 File Offset: 0x0009C3A5
		private void OnMove(Socket socket, Packet packet, params object[] args)
		{
			this.AddMessage(string.Format("{0} pressed {1}", args[1], args[0]));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0009E1C0 File Offset: 0x0009C3C0
		private void OnJoin(Socket socket, Packet packet, params object[] args)
		{
			string arg = (args.Length > 1) ? string.Format("({0})", args[1]) : string.Empty;
			this.AddMessage(string.Format("{0} joined {1}", args[0], arg));
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0009E1FC File Offset: 0x0009C3FC
		private void OnConnections(Socket socket, Packet packet, params object[] args)
		{
			this.connections = Convert.ToInt32(args[0]);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0009E20C File Offset: 0x0009C40C
		private void OnFrame(Socket socket, Packet packet, params object[] args)
		{
			if (this.State != SocketIOWePlaySample.States.Joined)
			{
				return;
			}
			if (this.FrameTexture == null)
			{
				this.FrameTexture = new Texture2D(0, 0, 4, false);
				this.FrameTexture.filterMode = 0;
			}
			byte[] array = packet.Attachments[0];
			ImageConversion.LoadImage(this.FrameTexture, array);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0009E266 File Offset: 0x0009C466
		private void OnError(Socket socket, Packet packet, params object[] args)
		{
			this.AddMessage(string.Format("--ERROR - {0}", args[0].ToString()));
		}

		// Token: 0x040012C9 RID: 4809
		private string[] controls = new string[]
		{
			"left",
			"right",
			"a",
			"b",
			"up",
			"down",
			"select",
			"start"
		};

		// Token: 0x040012CA RID: 4810
		private const float ratio = 1.5f;

		// Token: 0x040012CB RID: 4811
		private int MaxMessages = 50;

		// Token: 0x040012CC RID: 4812
		private SocketIOWePlaySample.States State;

		// Token: 0x040012CD RID: 4813
		private Socket Socket;

		// Token: 0x040012CE RID: 4814
		private string Nick = string.Empty;

		// Token: 0x040012CF RID: 4815
		private string messageToSend = string.Empty;

		// Token: 0x040012D0 RID: 4816
		private int connections;

		// Token: 0x040012D1 RID: 4817
		private List<string> messages = new List<string>();

		// Token: 0x040012D2 RID: 4818
		private Vector2 scrollPos;

		// Token: 0x040012D3 RID: 4819
		private Texture2D FrameTexture;

		// Token: 0x020008C5 RID: 2245
		private enum States
		{
			// Token: 0x04003351 RID: 13137
			Connecting,
			// Token: 0x04003352 RID: 13138
			WaitForNick,
			// Token: 0x04003353 RID: 13139
			Joined
		}
	}
}
