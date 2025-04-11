using System;
using System.Collections.Generic;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013E RID: 318
	internal class AppEvents : MenuBase
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0008C570 File Offset: 0x0008A770
		protected override void GetGui()
		{
			if (base.Button("Log FB App Event"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.LogAppEvent("fb_mobile_achievement_unlocked", null, new Dictionary<string, object>
				{
					{
						"fb_description",
						"Clicked 'Log AppEvent' button"
					}
				});
				LogView.AddLog("You may see results showing up at https://www.facebook.com/analytics/" + FB.AppId);
			}
		}
	}
}
