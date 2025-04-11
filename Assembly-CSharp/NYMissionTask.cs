using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class NYMissionTask
{
	// Token: 0x06000506 RID: 1286 RVA: 0x000622C8 File Offset: 0x000604C8
	public NYMissionTask(int id, string _task, int _target_value)
	{
		this.taskID = id;
		this.Task = _task;
		this.target_value = _target_value;
		this.offset_value = 0;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x000622F4 File Offset: 0x000604F4
	public void DrawTask(Rect _r)
	{
		this.CheckBoxRect = new Rect(_r.x - 5f, _r.y - 2f, 20f, 20f);
		this.TaskRect = new Rect(_r.x + 15f, _r.y, _r.width - 50f, 20f);
		GUI.DrawTexture(this.CheckBoxRect, this.Background);
		if (this.current_value + this.offset_value >= this.target_value)
		{
			this.TS = PERFORMANCE_STATUS.COMPLITED;
		}
		if (this.TS == PERFORMANCE_STATUS.COMPLITED)
		{
			GUI.DrawTexture(this.CheckBoxRect, this.Check);
		}
		int fontSize = GUIManager.gs_style1.fontSize;
		TextAnchor alignment = GUIManager.gs_style1.alignment;
		GUIManager.gs_style1.alignment = 0;
		GUIManager.gs_style1.fontSize = 14;
		GUIManager.gs_style1.richText = true;
		GUI.Label(this.TaskRect, string.Format(this.Task, this.target_value), GUIManager.gs_style1);
		GUIManager.gs_style1.richText = false;
		GUIManager.gs_style1.alignment = 2;
		if (this.target_value > 1)
		{
			Color color = GUI.color;
			GUI.color = Color.yellow;
			if (this.TS == PERFORMANCE_STATUS.COMPLITED)
			{
				GUI.color = Color.green;
				GUI.Label(this.TaskRect, this.target_value.ToString() + "/" + this.target_value.ToString(), GUIManager.gs_style1);
			}
			else
			{
				GUI.Label(this.TaskRect, (this.current_value + this.offset_value).ToString() + "/" + this.target_value.ToString(), GUIManager.gs_style1);
			}
			GUI.color = color;
		}
		GUIManager.gs_style1.fontSize = fontSize;
		GUIManager.gs_style1.alignment = alignment;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x000624D1 File Offset: 0x000606D1
	public void SetOffsetValue(int _value)
	{
		this.offset_value = _value;
		if (this.current_value + this.offset_value > this.target_value)
		{
			this.offset_value = this.target_value - this.current_value;
		}
	}

	// Token: 0x0400090A RID: 2314
	public PERFORMANCE_STATUS TS = PERFORMANCE_STATUS.PENDING;

	// Token: 0x0400090B RID: 2315
	private int taskID;

	// Token: 0x0400090C RID: 2316
	private string Task;

	// Token: 0x0400090D RID: 2317
	private Texture2D Background;

	// Token: 0x0400090E RID: 2318
	private Texture2D Check;

	// Token: 0x0400090F RID: 2319
	public int target_value;

	// Token: 0x04000910 RID: 2320
	public int current_value;

	// Token: 0x04000911 RID: 2321
	public int offset_value;

	// Token: 0x04000912 RID: 2322
	private Rect CheckBoxRect;

	// Token: 0x04000913 RID: 2323
	private Rect TaskRect;
}
