using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013B RID: 315
	internal class LogView : ConsoleBase
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0008C158 File Offset: 0x0008A358
		public static void AddLog(string log)
		{
			LogView.events.Insert(0, string.Format("{0}\n{1}\n", DateTime.Now.ToString(LogView.datePatt), log));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0008C190 File Offset: 0x0008A390
		protected void OnGUI()
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			if (base.Button("Back"))
			{
				base.GoBack();
			}
			base.ScrollPosition = GUILayout.BeginScrollView(base.ScrollPosition, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowFullWidth)
			});
			GUILayout.TextArea(string.Join("\n", LogView.events.ToArray<string>()), base.TextStyle, new GUILayoutOption[]
			{
				GUILayout.ExpandHeight(true),
				GUILayout.MaxWidth((float)ConsoleBase.MainWindowWidth)
			});
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
		}

		// Token: 0x040010BF RID: 4287
		private static string datePatt = "M/d/yyyy hh:mm:ss tt";

		// Token: 0x040010C0 RID: 4288
		private static IList<string> events = new List<string>();
	}
}
