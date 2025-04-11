using System;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013D RID: 317
	internal class AccessTokenMenu : MenuBase
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0008C546 File Offset: 0x0008A746
		protected override void GetGui()
		{
			if (base.Button("Refresh Access Token"))
			{
				FB.Mobile.RefreshCurrentAccessToken(new FacebookDelegate<IAccessTokenRefreshResult>(base.HandleResult));
			}
		}
	}
}
