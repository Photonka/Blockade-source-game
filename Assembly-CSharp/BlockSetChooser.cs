using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class BlockSetChooser : MonoBehaviour
{
	// Token: 0x06000037 RID: 55 RVA: 0x00002D19 File Offset: 0x00000F19
	private void Start()
	{
		this.viewer = base.GetComponent<BlockSetViewer>();
		this.viewer.SetBlockSet(this.blockSetList[this.index]);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002D40 File Offset: 0x00000F40
	private void OnGUI()
	{
		int num = this.index;
		Rect position;
		position..ctor(0f, 0f, 180f, (float)Screen.height);
		this.index = BlockSetChooser.DrawList(position, this.index, this.blockSetList, ref this.scrollPosition);
		if (this.index != num)
		{
			this.viewer.SetBlockSet(this.blockSetList[this.index]);
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002DB0 File Offset: 0x00000FB0
	private static int DrawList(Rect position, int selected, BlockSet[] list, ref Vector2 scrollPosition)
	{
		GUILayout.BeginArea(position, GUI.skin.box);
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, Array.Empty<GUILayoutOption>());
		for (int i = 0; i < list.Length; i++)
		{
			if (!(list[i] == null) && BlockSetChooser.DrawItem(list[i].name, i == selected))
			{
				selected = i;
				Event.current.Use();
			}
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		return selected;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002E28 File Offset: 0x00001028
	private static bool DrawItem(string name, bool selected)
	{
		Rect rect = GUILayoutUtility.GetRect(new GUIContent(name), GUI.skin.box);
		if (selected)
		{
			GUI.Box(rect, GUIContent.none);
		}
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.padding = GUI.skin.box.padding;
		guistyle.alignment = 3;
		GUI.Label(rect, name, guistyle);
		return Event.current.type == null && Event.current.button == 0 && rect.Contains(Event.current.mousePosition);
	}

	// Token: 0x0400002B RID: 43
	[SerializeField]
	private BlockSet[] blockSetList;

	// Token: 0x0400002C RID: 44
	private int index;

	// Token: 0x0400002D RID: 45
	private Vector2 scrollPosition;

	// Token: 0x0400002E RID: 46
	private BlockSetViewer viewer;
}
