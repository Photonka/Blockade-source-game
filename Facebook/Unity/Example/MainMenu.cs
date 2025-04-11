using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x02000144 RID: 324
	internal sealed class MainMenu : MenuBase
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		protected override bool ShowBackButton()
		{
			return false;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0008CF98 File Offset: 0x0008B198
		protected override void GetGui()
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			bool enabled = GUI.enabled;
			if (base.Button("FB.Init"))
			{
				FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
				base.Status = "FB.Init() called with " + FB.AppId;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUI.enabled = (enabled && FB.IsInitialized);
			if (base.Button("Login"))
			{
				this.CallFBLogin();
				base.Status = "Login called";
			}
			GUI.enabled = FB.IsLoggedIn;
			if (base.Button("Get publish_actions"))
			{
				this.CallFBLoginForPublish();
				base.Status = "Login (for publish_actions) called";
			}
			GUILayout.Label(GUIContent.none, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MarginFix)
			});
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(GUIContent.none, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MarginFix)
			});
			GUILayout.EndHorizontal();
			if (base.Button("Logout"))
			{
				this.CallFBLogout();
				base.Status = "Logout called";
			}
			GUI.enabled = (enabled && FB.IsInitialized);
			if (base.Button("Share Dialog"))
			{
				base.SwitchMenu(typeof(DialogShare));
			}
			if (base.Button("App Requests"))
			{
				base.SwitchMenu(typeof(AppRequests));
			}
			if (base.Button("Graph Request"))
			{
				base.SwitchMenu(typeof(GraphRequest));
			}
			if (Constants.IsWeb && base.Button("Pay"))
			{
				base.SwitchMenu(typeof(Pay));
			}
			if (base.Button("App Events"))
			{
				base.SwitchMenu(typeof(AppEvents));
			}
			if (base.Button("App Links"))
			{
				base.SwitchMenu(typeof(AppLinks));
			}
			if (Constants.IsMobile && base.Button("App Invites"))
			{
				base.SwitchMenu(typeof(AppInvites));
			}
			if (Constants.IsMobile && base.Button("Access Token"))
			{
				base.SwitchMenu(typeof(AccessTokenMenu));
			}
			GUILayout.EndVertical();
			GUI.enabled = enabled;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0008D1DB File Offset: 0x0008B3DB
		private void CallFBLogin()
		{
			FB.LogInWithReadPermissions(new List<string>
			{
				"public_profile",
				"email",
				"user_friends"
			}, new FacebookDelegate<ILoginResult>(base.HandleResult));
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0008D214 File Offset: 0x0008B414
		private void CallFBLoginForPublish()
		{
			FB.LogInWithPublishPermissions(new List<string>
			{
				"publish_actions"
			}, new FacebookDelegate<ILoginResult>(base.HandleResult));
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0008D237 File Offset: 0x0008B437
		private void CallFBLogout()
		{
			FB.LogOut();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0008D240 File Offset: 0x0008B440
		private void OnInitComplete()
		{
			base.Status = "Success - Check log for details";
			base.LastResponse = "Success Response: OnInitComplete Called\n";
			LogView.AddLog(string.Format("OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'", FB.IsLoggedIn, FB.IsInitialized));
			if (AccessToken.CurrentAccessToken != null)
			{
				LogView.AddLog(AccessToken.CurrentAccessToken.ToString());
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0008D29C File Offset: 0x0008B49C
		private void OnHideUnity(bool isGameShown)
		{
			base.Status = "Success - Check log for details";
			base.LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", isGameShown);
			LogView.AddLog("Is game shown: " + isGameShown.ToString());
		}
	}
}
