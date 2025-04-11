using System;
using System.Collections.Generic;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000189 RID: 393
	public class GUIMessageList
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00098F38 File Offset: 0x00097138
		public void Draw()
		{
			this.Draw((float)Screen.width, 0f);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00098F4C File Offset: 0x0009714C
		public void Draw(float minWidth, float minHeight)
		{
			this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, new GUILayoutOption[]
			{
				GUILayout.MinHeight(minHeight)
			});
			for (int i = 0; i < this.messages.Count; i++)
			{
				GUILayout.Label(this.messages[i], new GUILayoutOption[]
				{
					GUILayout.MinWidth(minWidth)
				});
			}
			GUILayout.EndScrollView();
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00098FB6 File Offset: 0x000971B6
		public void Add(string msg)
		{
			this.messages.Add(msg);
			this.scrollPos = new Vector2(this.scrollPos.x, float.MaxValue);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00098FDF File Offset: 0x000971DF
		public void Clear()
		{
			this.messages.Clear();
		}

		// Token: 0x04001252 RID: 4690
		private List<string> messages = new List<string>();

		// Token: 0x04001253 RID: 4691
		private Vector2 scrollPos;
	}
}
