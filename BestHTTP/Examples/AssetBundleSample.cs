using System;
using System.Collections;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200018A RID: 394
	public sealed class AssetBundleSample : MonoBehaviour
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00098FFF File Offset: 0x000971FF
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				GUILayout.Label("Status: " + this.status, Array.Empty<GUILayoutOption>());
				if (this.texture != null)
				{
					GUILayout.Box(this.texture, new GUILayoutOption[]
					{
						GUILayout.MaxHeight(256f)
					});
				}
				if (!this.downloading && GUILayout.Button("Start Download", Array.Empty<GUILayoutOption>()))
				{
					this.UnloadBundle();
					base.StartCoroutine(this.DownloadAssetBundle());
				}
			});
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00099018 File Offset: 0x00097218
		private void OnDestroy()
		{
			this.UnloadBundle();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00099020 File Offset: 0x00097220
		private IEnumerator DownloadAssetBundle()
		{
			this.downloading = true;
			HTTPRequest request = new HTTPRequest(this.URI).Send();
			this.status = "Download started";
			while (request.State < HTTPRequestStates.Finished)
			{
				yield return new WaitForSeconds(0.1f);
				this.status += ".";
			}
			switch (request.State)
			{
			case HTTPRequestStates.Finished:
				if (request.Response.IsSuccess)
				{
					this.status = string.Format("AssetBundle downloaded! Loaded from local cache: {0}", request.Response.IsFromCache.ToString());
					AssetBundleCreateRequest async = AssetBundle.LoadFromMemoryAsync(request.Response.Data);
					yield return async;
					yield return base.StartCoroutine(this.ProcessAssetBundle(async.assetBundle));
					async = null;
				}
				else
				{
					this.status = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", request.Response.StatusCode, request.Response.Message, request.Response.DataAsText);
					Debug.LogWarning(this.status);
				}
				break;
			case HTTPRequestStates.Error:
				this.status = "Request Finished with Error! " + ((request.Exception != null) ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception");
				Debug.LogError(this.status);
				break;
			case HTTPRequestStates.Aborted:
				this.status = "Request Aborted!";
				Debug.LogWarning(this.status);
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				this.status = "Connection Timed Out!";
				Debug.LogError(this.status);
				break;
			case HTTPRequestStates.TimedOut:
				this.status = "Processing the request Timed Out!";
				Debug.LogError(this.status);
				break;
			}
			this.downloading = false;
			yield break;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0009902F File Offset: 0x0009722F
		private IEnumerator ProcessAssetBundle(AssetBundle bundle)
		{
			if (bundle == null)
			{
				yield break;
			}
			this.cachedBundle = bundle;
			AssetBundleRequest asyncAsset = this.cachedBundle.LoadAssetAsync("9443182_orig", typeof(Texture2D));
			yield return asyncAsset;
			this.texture = (asyncAsset.asset as Texture2D);
			yield break;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00099045 File Offset: 0x00097245
		private void UnloadBundle()
		{
			if (this.cachedBundle != null)
			{
				this.cachedBundle.Unload(true);
				this.cachedBundle = null;
			}
		}

		// Token: 0x04001254 RID: 4692
		private Uri URI = new Uri(GUIHelper.BaseURL + "/AssetBundles/WebGL/demobundle.assetbundle");

		// Token: 0x04001255 RID: 4693
		private string status = "Waiting for user interaction";

		// Token: 0x04001256 RID: 4694
		private AssetBundle cachedBundle;

		// Token: 0x04001257 RID: 4695
		private Texture2D texture;

		// Token: 0x04001258 RID: 4696
		private bool downloading;
	}
}
