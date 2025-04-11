using System;
using System.Collections.Generic;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Logger;
using BestHTTP.Statistics;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000190 RID: 400
	public class SampleSelector : MonoBehaviour
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00099FA8 File Offset: 0x000981A8
		private void Awake()
		{
			Application.runInBackground = true;
			HTTPManager.Logger.Level = Loglevels.All;
			this.Samples.Add(new SampleDescriptor(null, "HTTP Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(TextureDownloadSample), "Texture Download", "With HTTPManager.MaxConnectionPerServer you can control how many requests can be processed per server parallel.\n\nFeatures demoed in this example:\n-Parallel requests to the same server\n-Controlling the parallelization\n-Automatic Caching\n-Create a Texture2D from the downloaded data"));
			this.Samples.Add(new SampleDescriptor(typeof(AssetBundleSample), "AssetBundle Download", "A small example that shows a possible way to download an AssetBundle and load a resource from it.\n\nFeatures demoed in this example:\n-Using HTTPRequest without a callback\n-Using HTTPRequest in a Coroutine\n-Loading an AssetBundle from the downloaded bytes\n-Automatic Caching"));
			this.Samples.Add(new SampleDescriptor(typeof(LargeFileDownloadSample), "Large File Download", "This example demonstrates how you can download a (large) file and continue the download after the connection is aborted.\n\nFeatures demoed in this example:\n-Setting up a streamed download\n-How to access the downloaded data while the download is in progress\n-Setting the HTTPRequest's StreamFragmentSize to controll the frequency and size of the fragments\n-How to use the SetRangeHeader to continue a previously disconnected download\n-How to disable the local, automatic caching"));
			this.Samples.Add(new SampleDescriptor(null, "WebSocket Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(WebSocketSample), "Echo", "A WebSocket demonstration that connects to a WebSocket echo service.\n\nFeatures demoed in this example:\n-Basic usage of the WebSocket class"));
			this.Samples.Add(new SampleDescriptor(null, "Socket.IO Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(SocketIOChatSample), "Chat", "This example uses the Socket.IO implementation to connect to the official Chat demo server(http://chat.socket.io/).\n\nFeatures demoed in this example:\n-Instantiating and setting up a SocketManager to connect to a Socket.IO server\n-Changing SocketOptions property\n-Subscribing to Socket.IO events\n-Sending custom events to the server"));
			this.Samples.Add(new SampleDescriptor(typeof(SocketIOWePlaySample), "WePlay", "This example uses the Socket.IO implementation to connect to the official WePlay demo server(http://weplay.io/).\n\nFeatures demoed in this example:\n-Instantiating and setting up a SocketManager to connect to a Socket.IO server\n-Subscribing to Socket.IO events\n-Receiving binary data\n-How to load a texture from the received binary data\n-How to disable payload decoding for fine tune for some speed\n-Sending custom events to the server"));
			this.Samples.Add(new SampleDescriptor(null, "SignalR Core Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(TestHubExample), "Hub Sample", "This sample demonstrates most of the functionalities of the SignalR protocol:\n-How to set up HubConnection to connect to the server\n-Subscribing to server-callable function\n-Calling client-callable function on the server\n-Calling and handling streaming\n"));
			this.Samples.Add(new SampleDescriptor(typeof(HubWithAuthorizationSample), "Hub Authentication Sample", "This sample demonstrates the default access token authentication. The server sends a JWT token to the client with a new url. The client will connect to that new url and sends the JWT token.\n"));
			this.Samples.Add(new SampleDescriptor(typeof(HubWithPreAuthorizationSample), "Hub Pre-Authentication Sample", "This sample demonstrates manual authentication.\n"));
			this.Samples.Add(new SampleDescriptor(typeof(RedirectSample), "Hub Redirect Sample", "This sample demonstrates how the plugin handles redirection through the SignalR Core negotiation data.\n"));
			this.Samples.Add(new SampleDescriptor(null, "SignalR Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(SimpleStreamingSample), "Simple Streaming", "A very simple example of a background thread that broadcasts the server time to all connected clients every two seconds.\n\nFeatures demoed in this example:\n-Subscribing and handling non-hub messages"));
			this.Samples.Add(new SampleDescriptor(typeof(ConnectionAPISample), "Connection API", "Demonstrates all features of the lower-level connection API including starting and stopping, sending and receiving messages, and managing groups.\n\nFeatures demoed in this example:\n-Instantiating and setting up a SignalR Connection to connect to a SignalR server\n-Changing the default Json encoder\n-Subscribing to state changes\n-Receiving and handling of non-hub messages\n-Sending non-hub messages\n-Managing groups"));
			this.Samples.Add(new SampleDescriptor(typeof(ConnectionStatusSample), "Connection Status", "Demonstrates how to handle the events that are raised when connections connect, reconnect and disconnect from the Hub API.\n\nFeatures demoed in this example:\n-Connecting to a Hub\n-Setting up a callback for Hub events\n-Handling server-sent method call requests\n-Calling a Hub-method on the server-side\n-Opening and closing the SignalR Connection"));
			this.Samples.Add(new SampleDescriptor(typeof(DemoHubSample), "Demo Hub", "A contrived example that exploits every feature of the Hub API.\n\nFeatures demoed in this example:\n-Creating and using wrapper Hub classes to encapsulate hub functions and events\n-Handling long running server-side functions by handling progress messages\n-Groups\n-Handling server-side functions with return value\n-Handling server-side functions throwing Exceptions\n-Calling server-side functions with complex type parameters\n-Calling server-side functions with array parameters\n-Calling overloaded server-side functions\n-Changing Hub states\n-Receiving and handling hub state changes\n-Calling server-side functions implemented in VB .NET"));
			this.Samples.Add(new SampleDescriptor(typeof(AuthenticationSample), "Authentication", "Demonstrates how to use the authorization features of the Hub API to restrict certain Hubs and methods to specific users.\n\nFeatures demoed in this example:\n-Creating and using wrapper Hub classes to encapsulate hub functions and events\n-Create and use a Header-based authenticator to access protected APIs\n-SignalR over HTTPS"));
			this.Samples.Add(new SampleDescriptor(null, "Plugin Samples", string.Empty)
			{
				IsLabel = true
			});
			this.Samples.Add(new SampleDescriptor(typeof(CacheMaintenanceSample), "Cache Maintenance", "With this demo you can see how you can use the HTTPCacheService's BeginMaintainence function to delete too old cached entities and keep the cache size under a specified value.\n\nFeatures demoed in this example:\n-How to set up a HTTPCacheMaintananceParams\n-How to call the BeginMaintainence function"));
			SampleSelector.SelectedSample = this.Samples[1];
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0009A2E4 File Offset: 0x000984E4
		private void Update()
		{
			GUIHelper.ClientArea = new Rect(0f, 165f, (float)Screen.width, (float)(Screen.height - 160 - 50));
			if (Input.GetKeyDown(27))
			{
				if (SampleSelector.SelectedSample != null && SampleSelector.SelectedSample.IsRunning)
				{
					SampleSelector.SelectedSample.DestroyUnityObject();
				}
				else
				{
					Application.Quit();
				}
			}
			if ((Input.GetKeyDown(271) || Input.GetKeyDown(13)) && SampleSelector.SelectedSample != null && !SampleSelector.SelectedSample.IsRunning)
			{
				SampleSelector.SelectedSample.CreateUnityObject();
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0009A37C File Offset: 0x0009857C
		private void OnGUI()
		{
			GeneralStatistics stats = HTTPManager.GetGeneralStatistics(StatisticsQueryFlags.All);
			GUIHelper.DrawArea(new Rect(0f, 0f, (float)(Screen.width / 3), 160f), false, delegate
			{
				GUIHelper.DrawCenteredText("Connections");
				GUILayout.Space(5f);
				GUIHelper.DrawRow("Sum:", stats.Connections.ToString());
				GUIHelper.DrawRow("Active:", stats.ActiveConnections.ToString());
				GUIHelper.DrawRow("Free:", stats.FreeConnections.ToString());
				GUIHelper.DrawRow("Recycled:", stats.RecycledConnections.ToString());
				GUIHelper.DrawRow("Requests in queue:", stats.RequestsInQueue.ToString());
			});
			GUIHelper.DrawArea(new Rect((float)(Screen.width / 3), 0f, (float)(Screen.width / 3), 160f), false, delegate
			{
				GUIHelper.DrawCenteredText("Cache");
				if (!HTTPCacheService.IsSupported)
				{
					GUI.color = Color.yellow;
					GUIHelper.DrawCenteredText("Disabled in WebPlayer, WebGL & Samsung Smart TV Builds!");
					GUI.color = Color.white;
					return;
				}
				GUILayout.Space(5f);
				GUIHelper.DrawRow("Cached entities:", stats.CacheEntityCount.ToString());
				GUIHelper.DrawRow("Sum Size (bytes): ", stats.CacheSize.ToString("N0"));
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Clear Cache", Array.Empty<GUILayoutOption>()))
				{
					HTTPCacheService.BeginClear();
				}
				GUILayout.EndVertical();
			});
			GUIHelper.DrawArea(new Rect((float)(Screen.width / 3 * 2), 0f, (float)(Screen.width / 3), 160f), false, delegate
			{
				GUIHelper.DrawCenteredText("Cookies");
				if (!CookieJar.IsSavingSupported)
				{
					GUI.color = Color.yellow;
					GUIHelper.DrawCenteredText("Saving and loading from disk is disabled in WebPlayer, WebGL & Samsung Smart TV Builds!");
					GUI.color = Color.white;
					return;
				}
				GUILayout.Space(5f);
				GUIHelper.DrawRow("Cookies:", stats.CookieCount.ToString());
				GUIHelper.DrawRow("Estimated size (bytes):", stats.CookieJarSize.ToString("N0"));
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Clear Cookies", Array.Empty<GUILayoutOption>()))
				{
					CookieJar.Clear();
				}
				GUILayout.EndVertical();
			});
			if (SampleSelector.SelectedSample == null || (SampleSelector.SelectedSample != null && !SampleSelector.SelectedSample.IsRunning))
			{
				GUIHelper.DrawArea(new Rect(0f, 165f, (float)((SampleSelector.SelectedSample == null) ? Screen.width : (Screen.width / 3)), (float)(Screen.height - 160 - 5)), false, delegate
				{
					this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, Array.Empty<GUILayoutOption>());
					for (int i = 0; i < this.Samples.Count; i++)
					{
						this.DrawSample(this.Samples[i]);
					}
					GUILayout.EndScrollView();
				});
				if (SampleSelector.SelectedSample != null)
				{
					this.DrawSampleDetails(SampleSelector.SelectedSample);
					return;
				}
			}
			else if (SampleSelector.SelectedSample != null && SampleSelector.SelectedSample.IsRunning)
			{
				GUILayout.BeginArea(new Rect(0f, (float)(Screen.height - 50), (float)Screen.width, 50f), string.Empty);
				GUILayout.FlexibleSpace();
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Back", new GUILayoutOption[]
				{
					GUILayout.MinWidth(100f)
				}))
				{
					SampleSelector.SelectedSample.DestroyUnityObject();
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0009A550 File Offset: 0x00098750
		private void DrawSample(SampleDescriptor sample)
		{
			if (sample.IsLabel)
			{
				GUILayout.Space(15f);
				GUIHelper.DrawCenteredText(sample.DisplayName);
				GUILayout.Space(5f);
				return;
			}
			if (GUILayout.Button(sample.DisplayName, Array.Empty<GUILayoutOption>()))
			{
				sample.IsSelected = true;
				if (SampleSelector.SelectedSample != null)
				{
					SampleSelector.SelectedSample.IsSelected = false;
				}
				SampleSelector.SelectedSample = sample;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0009A5B8 File Offset: 0x000987B8
		private void DrawSampleDetails(SampleDescriptor sample)
		{
			Rect rect = new Rect((float)(Screen.width / 3), 165f, (float)(Screen.width / 3 * 2), (float)(Screen.height - 160 - 5));
			GUI.Box(rect, string.Empty);
			GUILayout.BeginArea(rect);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUIHelper.DrawCenteredText(sample.DisplayName);
			GUILayout.Space(5f);
			GUILayout.Label(sample.Description, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Start Sample", Array.Empty<GUILayoutOption>()))
			{
				sample.CreateUnityObject();
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}

		// Token: 0x04001273 RID: 4723
		public const int statisticsHeight = 160;

		// Token: 0x04001274 RID: 4724
		private List<SampleDescriptor> Samples = new List<SampleDescriptor>();

		// Token: 0x04001275 RID: 4725
		public static SampleDescriptor SelectedSample;

		// Token: 0x04001276 RID: 4726
		private Vector2 scrollPos;
	}
}
