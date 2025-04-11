using System;

namespace Facebook.Unity.Example
{
	// Token: 0x02000140 RID: 320
	internal class AppLinks : MenuBase
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0008C6C4 File Offset: 0x0008A8C4
		protected override void GetGui()
		{
			if (base.Button("Get App Link"))
			{
				FB.GetAppLink(new FacebookDelegate<IAppLinkResult>(base.HandleResult));
			}
			if (Constants.IsMobile && base.Button("Fetch Deferred App Link"))
			{
				FB.Mobile.FetchDeferredAppLinkData(new FacebookDelegate<IAppLinkResult>(base.HandleResult));
			}
		}
	}
}
