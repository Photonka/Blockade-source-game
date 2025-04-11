using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class BlockSetViewer : MonoBehaviour
{
	// Token: 0x0600003C RID: 60 RVA: 0x00002EB8 File Offset: 0x000010B8
	public void SetBlockSet(BlockSet blockSet)
	{
		this.blockSet = blockSet;
		this.index = Mathf.Clamp(this.index, 0, blockSet.GetBlockCount());
		this.BuildBlock(blockSet.GetBlock(this.index));
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002EEC File Offset: 0x000010EC
	private void BuildBlock(Block block)
	{
		base.GetComponent<Renderer>().material = block.GetAtlas().GetMaterial();
		MeshFilter component = base.GetComponent<MeshFilter>();
		block.Build().ToMesh(component.mesh);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002F28 File Offset: 0x00001128
	private void OnGUI()
	{
		Rect position;
		position..ctor((float)(Screen.width - 180), 0f, 180f, (float)Screen.height);
		int num = this.index;
		this.index = BlockSetViewer.DrawList(position, this.index, this.blockSet.GetBlocks(), ref this.scrollPosition);
		if (num != this.index)
		{
			this.BuildBlock(this.blockSet.GetBlock(this.index));
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002FA0 File Offset: 0x000011A0
	private static int DrawList(Rect position, int selected, Block[] list, ref Vector2 scrollPosition)
	{
		GUILayout.BeginArea(position, GUI.skin.box);
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, Array.Empty<GUILayoutOption>());
		for (int i = 0; i < list.Length; i++)
		{
			if (list[i] != null && BlockSetViewer.DrawItem(list[i], i == selected))
			{
				selected = i;
				Event.current.Use();
			}
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		return selected;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000300C File Offset: 0x0000120C
	private static bool DrawItem(Block block, bool selected)
	{
		Rect rect = GUILayoutUtility.GetRect(0f, 40f, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		if (selected)
		{
			GUI.Box(rect, GUIContent.none);
		}
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.alignment = 3;
		Rect position;
		position..ctor(rect.x + 4f, rect.y + 4f, rect.height - 8f, rect.height - 8f);
		block.DrawPreview(position);
		Rect rect2 = rect;
		rect2.xMin = position.xMax + 4f;
		GUI.Label(rect2, block.GetName(), guistyle);
		return Event.current.type == null && Event.current.button == 0 && rect.Contains(Event.current.mousePosition);
	}

	// Token: 0x0400002F RID: 47
	private BlockSet blockSet;

	// Token: 0x04000030 RID: 48
	private int index;

	// Token: 0x04000031 RID: 49
	private Vector2 scrollPosition;
}
