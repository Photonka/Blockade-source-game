using System;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013C RID: 316
	internal abstract class MenuBase : ConsoleBase
	{
		// Token: 0x06000AF7 RID: 2807
		protected abstract void GetGui();

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		protected virtual bool ShowDialogModeSelector()
		{
			return false;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0006CF70 File Offset: 0x0006B170
		protected virtual bool ShowBackButton()
		{
			return true;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0008C244 File Offset: 0x0008A444
		protected void HandleResult(IResult result)
		{
			if (result == null)
			{
				base.LastResponse = "Null Response\n";
				LogView.AddLog(base.LastResponse);
				return;
			}
			base.LastResponseTexture = null;
			if (!string.IsNullOrEmpty(result.Error))
			{
				base.Status = "Error - Check log for details";
				base.LastResponse = "Error Response:\n" + result.Error;
			}
			else if (result.Cancelled)
			{
				base.Status = "Cancelled - Check log for details";
				base.LastResponse = "Cancelled Response:\n" + result.RawResult;
			}
			else if (!string.IsNullOrEmpty(result.RawResult))
			{
				base.Status = "Success - Check log for details";
				base.LastResponse = "Success Response:\n" + result.RawResult;
			}
			else
			{
				base.LastResponse = "Empty Response\n";
			}
			LogView.AddLog(result.ToString());
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0008C314 File Offset: 0x0008A514
		protected void OnGUI()
		{
			if (base.IsHorizontalLayout())
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			}
			GUILayout.Label(base.GetType().Name, base.LabelStyle, Array.Empty<GUILayoutOption>());
			this.AddStatus();
			base.ScrollPosition = GUILayout.BeginScrollView(base.ScrollPosition, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowFullWidth)
			});
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (this.ShowBackButton())
			{
				this.AddBackButton();
			}
			this.AddLogButton();
			if (this.ShowBackButton())
			{
				GUILayout.Label(GUIContent.none, new GUILayoutOption[]
				{
					GUILayout.MinWidth((float)ConsoleBase.MarginFix)
				});
			}
			GUILayout.EndHorizontal();
			if (this.ShowDialogModeSelector())
			{
				this.AddDialogModeButtons();
			}
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			this.GetGui();
			GUILayout.Space(10f);
			GUILayout.EndVertical();
			GUILayout.EndScrollView();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0008C3FE File Offset: 0x0008A5FE
		private void AddStatus()
		{
			GUILayout.Space(5f);
			GUILayout.Box("Status: " + base.Status, base.TextStyle, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowWidth)
			});
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0008C439 File Offset: 0x0008A639
		private void AddBackButton()
		{
			GUI.enabled = ConsoleBase.MenuStack.Any<string>();
			if (base.Button("Back"))
			{
				base.GoBack();
			}
			GUI.enabled = true;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0008C463 File Offset: 0x0008A663
		private void AddLogButton()
		{
			if (base.Button("Log"))
			{
				base.SwitchMenu(typeof(LogView));
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0008C484 File Offset: 0x0008A684
		private void AddDialogModeButtons()
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			foreach (object obj in Enum.GetValues(typeof(ShareDialogMode)))
			{
				this.AddDialogModeButton((ShareDialogMode)obj);
			}
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		private void AddDialogModeButton(ShareDialogMode mode)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = (enabled && mode != MenuBase.shareDialogMode);
			if (base.Button(mode.ToString()))
			{
				MenuBase.shareDialogMode = mode;
				FB.Mobile.ShareDialogMode = mode;
			}
			GUI.enabled = enabled;
		}

		// Token: 0x040010C1 RID: 4289
		private static ShareDialogMode shareDialogMode;
	}
}
