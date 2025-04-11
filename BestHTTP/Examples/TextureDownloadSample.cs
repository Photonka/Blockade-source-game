using System;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200018C RID: 396
	public sealed class TextureDownloadSample : MonoBehaviour
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x000995A0 File Offset: 0x000977A0
		private void Awake()
		{
			HTTPManager.MaxConnectionPerServer = 1;
			for (int i = 0; i < this.Images.Length; i++)
			{
				this.Textures[i] = new Texture2D(100, 150);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000995DA File Offset: 0x000977DA
		private void OnDestroy()
		{
			HTTPManager.MaxConnectionPerServer = 4;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000995E2 File Offset: 0x000977E2
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, Array.Empty<GUILayoutOption>());
				int num = 0;
				Texture[] textures = this.Textures;
				GUILayout.SelectionGrid(num, textures, 3, Array.Empty<GUILayoutOption>());
				if (this.finishedCount == this.Images.Length && this.allDownloadedFromLocalCache)
				{
					GUIHelper.DrawCenteredText("All images loaded from the local cache!");
				}
				GUILayout.FlexibleSpace();
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label("Max Connection/Server: ", new GUILayoutOption[]
				{
					GUILayout.Width(150f)
				});
				GUILayout.Label(HTTPManager.MaxConnectionPerServer.ToString(), new GUILayoutOption[]
				{
					GUILayout.Width(20f)
				});
				HTTPManager.MaxConnectionPerServer = (byte)GUILayout.HorizontalSlider((float)HTTPManager.MaxConnectionPerServer, 1f, 10f, Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Start Download", Array.Empty<GUILayoutOption>()))
				{
					this.DownloadImages();
				}
				GUILayout.EndScrollView();
			});
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000995FC File Offset: 0x000977FC
		private void DownloadImages()
		{
			this.allDownloadedFromLocalCache = true;
			this.finishedCount = 0;
			for (int i = 0; i < this.Images.Length; i++)
			{
				this.Textures[i] = new Texture2D(100, 150);
				new HTTPRequest(new Uri(this.BaseURL + this.Images[i]), new OnRequestFinishedDelegate(this.ImageDownloaded))
				{
					Tag = this.Textures[i]
				}.Send();
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0009967C File Offset: 0x0009787C
		private void ImageDownloaded(HTTPRequest req, HTTPResponse resp)
		{
			this.finishedCount++;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					ImageConversion.LoadImage(req.Tag as Texture2D, resp.Data);
					this.allDownloadedFromLocalCache = (this.allDownloadedFromLocalCache && resp.IsFromCache);
					return;
				}
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				return;
			case HTTPRequestStates.Error:
				Debug.LogError("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
				return;
			case HTTPRequestStates.Aborted:
				Debug.LogWarning("Request Aborted!");
				return;
			case HTTPRequestStates.ConnectionTimedOut:
				Debug.LogError("Connection Timed Out!");
				return;
			case HTTPRequestStates.TimedOut:
				Debug.LogError("Processing the request Timed Out!");
				return;
			default:
				return;
			}
		}

		// Token: 0x0400125E RID: 4702
		private string BaseURL = GUIHelper.BaseURL + "/images/Demo/";

		// Token: 0x0400125F RID: 4703
		private string[] Images = new string[]
		{
			"One.png",
			"Two.png",
			"Three.png",
			"Four.png",
			"Five.png",
			"Six.png",
			"Seven.png",
			"Eight.png",
			"Nine.png"
		};

		// Token: 0x04001260 RID: 4704
		private Texture2D[] Textures = new Texture2D[9];

		// Token: 0x04001261 RID: 4705
		private bool allDownloadedFromLocalCache;

		// Token: 0x04001262 RID: 4706
		private int finishedCount;

		// Token: 0x04001263 RID: 4707
		private Vector2 scrollPos;
	}
}
