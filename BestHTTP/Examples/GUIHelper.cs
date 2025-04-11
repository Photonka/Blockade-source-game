using System;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000188 RID: 392
	public static class GUIHelper
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x00098E20 File Offset: 0x00097020
		private static void Setup()
		{
			if (GUIHelper.centerAlignedLabel == null)
			{
				GUIHelper.centerAlignedLabel = new GUIStyle(GUI.skin.label);
				GUIHelper.centerAlignedLabel.alignment = 4;
				GUIHelper.rightAlignedLabel = new GUIStyle(GUI.skin.label);
				GUIHelper.rightAlignedLabel.alignment = 5;
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00098E74 File Offset: 0x00097074
		public static void DrawArea(Rect area, bool drawHeader, Action action)
		{
			GUIHelper.Setup();
			GUI.Box(area, string.Empty);
			GUILayout.BeginArea(area);
			if (drawHeader)
			{
				GUIHelper.DrawCenteredText(SampleSelector.SelectedSample.DisplayName);
				GUILayout.Space(5f);
			}
			if (action != null)
			{
				action();
			}
			GUILayout.EndArea();
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00098EC1 File Offset: 0x000970C1
		public static void DrawCenteredText(string msg)
		{
			GUIHelper.Setup();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(msg, GUIHelper.centerAlignedLabel, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00098EF1 File Offset: 0x000970F1
		public static void DrawRow(string key, string value)
		{
			GUIHelper.Setup();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(key, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(value, GUIHelper.rightAlignedLabel, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}

		// Token: 0x0400124E RID: 4686
		public static string BaseURL = "https://besthttpdemosite.azurewebsites.net";

		// Token: 0x0400124F RID: 4687
		private static GUIStyle centerAlignedLabel;

		// Token: 0x04001250 RID: 4688
		private static GUIStyle rightAlignedLabel;

		// Token: 0x04001251 RID: 4689
		public static Rect ClientArea;
	}
}
