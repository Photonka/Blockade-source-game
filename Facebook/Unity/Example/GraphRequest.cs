using System;
using System.Collections;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x02000143 RID: 323
	internal class GraphRequest : MenuBase
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0008CE64 File Offset: 0x0008B064
		protected override void GetGui()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = (enabled && FB.IsLoggedIn);
			if (base.Button("Basic Request - Me"))
			{
				FB.API("/me", 0, new FacebookDelegate<IGraphResult>(base.HandleResult), null);
			}
			if (base.Button("Retrieve Profile Photo"))
			{
				FB.API("/me/picture", 0, new FacebookDelegate<IGraphResult>(this.ProfilePhotoCallback), null);
			}
			if (base.Button("Take and Upload screenshot"))
			{
				base.StartCoroutine(this.TakeScreenshot());
			}
			base.LabelAndTextField("Request", ref this.apiQuery);
			if (base.Button("Custom Request"))
			{
				FB.API(this.apiQuery, 0, new FacebookDelegate<IGraphResult>(base.HandleResult), null);
			}
			if (this.profilePic != null)
			{
				GUILayout.Box(this.profilePic, Array.Empty<GUILayoutOption>());
			}
			GUI.enabled = enabled;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0008CF44 File Offset: 0x0008B144
		private void ProfilePhotoCallback(IGraphResult result)
		{
			if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
			{
				this.profilePic = result.Texture;
			}
			base.HandleResult(result);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0008CF74 File Offset: 0x0008B174
		private IEnumerator TakeScreenshot()
		{
			yield return new WaitForEndOfFrame();
			int width = Screen.width;
			int height = Screen.height;
			Texture2D texture2D = new Texture2D(width, height, 3, false);
			texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			texture2D.Apply();
			byte[] array = ImageConversion.EncodeToPNG(texture2D);
			WWWForm wwwform = new WWWForm();
			wwwform.AddBinaryData("image", array, "InteractiveConsole.png");
			wwwform.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
			FB.API("me/photos", 1, new FacebookDelegate<IGraphResult>(base.HandleResult), wwwform);
			yield break;
		}

		// Token: 0x040010D7 RID: 4311
		private string apiQuery = string.Empty;

		// Token: 0x040010D8 RID: 4312
		private Texture2D profilePic;
	}
}
