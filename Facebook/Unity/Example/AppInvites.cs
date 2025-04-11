using System;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013F RID: 319
	internal class AppInvites : MenuBase
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x0008C5D4 File Offset: 0x0008A7D4
		protected override void GetGui()
		{
			if (base.Button("Android Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), null, new FacebookDelegate<IAppInviteResult>(base.HandleResult));
			}
			if (base.Button("Android Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), new FacebookDelegate<IAppInviteResult>(base.HandleResult));
			}
			if (base.Button("iOS Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), null, new FacebookDelegate<IAppInviteResult>(base.HandleResult));
			}
			if (base.Button("iOS Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), new Uri("http://i.imgur.com/zkYlB.jpg"), new FacebookDelegate<IAppInviteResult>(base.HandleResult));
			}
		}
	}
}
