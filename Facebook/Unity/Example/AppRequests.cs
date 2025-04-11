using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x02000141 RID: 321
	internal class AppRequests : MenuBase
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0008C714 File Offset: 0x0008A914
		protected override void GetGui()
		{
			if (base.Button("Select - Filter None"))
			{
				FB.AppRequest("Test Message", null, null, null, null, "", "", new FacebookDelegate<IAppRequestResult>(base.HandleResult));
			}
			if (base.Button("Select - Filter app_users"))
			{
				List<object> list = new List<object>
				{
					"app_users"
				};
				FB.AppRequest("Test Message", null, list, null, new int?(0), string.Empty, string.Empty, new FacebookDelegate<IAppRequestResult>(base.HandleResult));
			}
			if (base.Button("Select - Filter app_non_users"))
			{
				List<object> list2 = new List<object>
				{
					"app_non_users"
				};
				FB.AppRequest("Test Message", null, list2, null, new int?(0), string.Empty, string.Empty, new FacebookDelegate<IAppRequestResult>(base.HandleResult));
			}
			base.LabelAndTextField("Message: ", ref this.requestMessage);
			base.LabelAndTextField("To (optional): ", ref this.requestTo);
			base.LabelAndTextField("Filter (optional): ", ref this.requestFilter);
			base.LabelAndTextField("Exclude Ids (optional): ", ref this.requestExcludes);
			base.LabelAndTextField("Filters: ", ref this.requestExcludes);
			base.LabelAndTextField("Max Recipients (optional): ", ref this.requestMax);
			base.LabelAndTextField("Data (optional): ", ref this.requestData);
			base.LabelAndTextField("Title (optional): ", ref this.requestTitle);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label("Request Action (optional): ", base.LabelStyle, new GUILayoutOption[]
			{
				GUILayout.MaxWidth(200f * base.ScaleFactor)
			});
			this.selectedAction = GUILayout.Toolbar(this.selectedAction, this.actionTypeStrings, base.ButtonStyle, new GUILayoutOption[]
			{
				GUILayout.MinHeight((float)ConsoleBase.ButtonHeight * base.ScaleFactor),
				GUILayout.MaxWidth((float)(ConsoleBase.MainWindowWidth - 150))
			});
			GUILayout.EndHorizontal();
			base.LabelAndTextField("Request Object ID (optional): ", ref this.requestObjectID);
			if (base.Button("Custom App Request"))
			{
				OGActionType? selectedOGActionType = this.GetSelectedOGActionType();
				if (selectedOGActionType != null)
				{
					FB.AppRequest(this.requestMessage, selectedOGActionType.Value, this.requestObjectID, string.IsNullOrEmpty(this.requestTo) ? null : this.requestTo.Split(new char[]
					{
						','
					}), this.requestData, this.requestTitle, new FacebookDelegate<IAppRequestResult>(base.HandleResult));
					return;
				}
				FB.AppRequest(this.requestMessage, string.IsNullOrEmpty(this.requestTo) ? null : this.requestTo.Split(new char[]
				{
					','
				}), string.IsNullOrEmpty(this.requestFilter) ? null : this.requestFilter.Split(new char[]
				{
					','
				}).OfType<object>().ToList<object>(), string.IsNullOrEmpty(this.requestExcludes) ? null : this.requestExcludes.Split(new char[]
				{
					','
				}), new int?(string.IsNullOrEmpty(this.requestMax) ? 0 : int.Parse(this.requestMax)), this.requestData, this.requestTitle, new FacebookDelegate<IAppRequestResult>(base.HandleResult));
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0008CA3C File Offset: 0x0008AC3C
		private OGActionType? GetSelectedOGActionType()
		{
			string a = this.actionTypeStrings[this.selectedAction];
			if (a == 0.ToString())
			{
				return new OGActionType?(0);
			}
			if (a == 1.ToString())
			{
				return new OGActionType?(1);
			}
			if (a == 2.ToString())
			{
				return new OGActionType?(2);
			}
			return null;
		}

		// Token: 0x040010C2 RID: 4290
		private string requestMessage = string.Empty;

		// Token: 0x040010C3 RID: 4291
		private string requestTo = string.Empty;

		// Token: 0x040010C4 RID: 4292
		private string requestFilter = string.Empty;

		// Token: 0x040010C5 RID: 4293
		private string requestExcludes = string.Empty;

		// Token: 0x040010C6 RID: 4294
		private string requestMax = string.Empty;

		// Token: 0x040010C7 RID: 4295
		private string requestData = string.Empty;

		// Token: 0x040010C8 RID: 4296
		private string requestTitle = string.Empty;

		// Token: 0x040010C9 RID: 4297
		private string requestObjectID = string.Empty;

		// Token: 0x040010CA RID: 4298
		private int selectedAction;

		// Token: 0x040010CB RID: 4299
		private string[] actionTypeStrings = new string[]
		{
			"NONE",
			0.ToString(),
			1.ToString(),
			2.ToString()
		};
	}
}
