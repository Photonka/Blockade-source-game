using System;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x02000142 RID: 322
	internal class DialogShare : MenuBase
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		protected override bool ShowDialogModeSelector()
		{
			return false;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0008CB74 File Offset: 0x0008AD74
		protected override void GetGui()
		{
			bool enabled = GUI.enabled;
			if (base.Button("Share - Link"))
			{
				FB.ShareLink(new Uri("https://developers.facebook.com/"), "", "", null, new FacebookDelegate<IShareResult>(base.HandleResult));
			}
			if (base.Button("Share - Link Photo"))
			{
				FB.ShareLink(new Uri("https://developers.facebook.com/"), "Link Share", "Look I'm sharing a link", new Uri("http://i.imgur.com/j4M7vCO.jpg"), new FacebookDelegate<IShareResult>(base.HandleResult));
			}
			base.LabelAndTextField("Link", ref this.shareLink);
			base.LabelAndTextField("Title", ref this.shareTitle);
			base.LabelAndTextField("Description", ref this.shareDescription);
			base.LabelAndTextField("Image", ref this.shareImage);
			if (base.Button("Share - Custom"))
			{
				FB.ShareLink(new Uri(this.shareLink), this.shareTitle, this.shareDescription, new Uri(this.shareImage), new FacebookDelegate<IShareResult>(base.HandleResult));
			}
			GUI.enabled = (enabled && (!Constants.IsEditor || (Constants.IsEditor && FB.IsLoggedIn)));
			if (base.Button("Feed Share - No To"))
			{
				FB.FeedShare(string.Empty, new Uri("https://developers.facebook.com/"), "Test Title", "Test caption", "Test Description", new Uri("http://i.imgur.com/zkYlB.jpg"), string.Empty, new FacebookDelegate<IShareResult>(base.HandleResult));
			}
			base.LabelAndTextField("To", ref this.feedTo);
			base.LabelAndTextField("Link", ref this.feedLink);
			base.LabelAndTextField("Title", ref this.feedTitle);
			base.LabelAndTextField("Caption", ref this.feedCaption);
			base.LabelAndTextField("Description", ref this.feedDescription);
			base.LabelAndTextField("Image", ref this.feedImage);
			base.LabelAndTextField("Media Source", ref this.feedMediaSource);
			if (base.Button("Feed Share - Custom"))
			{
				FB.FeedShare(this.feedTo, string.IsNullOrEmpty(this.feedLink) ? null : new Uri(this.feedLink), this.feedTitle, this.feedCaption, this.feedDescription, string.IsNullOrEmpty(this.feedImage) ? null : new Uri(this.feedImage), this.feedMediaSource, new FacebookDelegate<IShareResult>(base.HandleResult));
			}
			GUI.enabled = enabled;
		}

		// Token: 0x040010CC RID: 4300
		private string shareLink = "https://developers.facebook.com/";

		// Token: 0x040010CD RID: 4301
		private string shareTitle = "Link Title";

		// Token: 0x040010CE RID: 4302
		private string shareDescription = "Link Description";

		// Token: 0x040010CF RID: 4303
		private string shareImage = "http://i.imgur.com/j4M7vCO.jpg";

		// Token: 0x040010D0 RID: 4304
		private string feedTo = string.Empty;

		// Token: 0x040010D1 RID: 4305
		private string feedLink = "https://developers.facebook.com/";

		// Token: 0x040010D2 RID: 4306
		private string feedTitle = "Test Title";

		// Token: 0x040010D3 RID: 4307
		private string feedCaption = "Test Caption";

		// Token: 0x040010D4 RID: 4308
		private string feedDescription = "Test Description";

		// Token: 0x040010D5 RID: 4309
		private string feedImage = "http://i.imgur.com/zkYlB.jpg";

		// Token: 0x040010D6 RID: 4310
		private string feedMediaSource = string.Empty;
	}
}
