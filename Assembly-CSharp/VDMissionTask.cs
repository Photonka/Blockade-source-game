using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class VDMissionTask
{
	// Token: 0x0600051A RID: 1306 RVA: 0x0006337C File Offset: 0x0006157C
	public VDMissionTask(int id, string _task, int _target_value)
	{
		this.taskID = id;
		this.Task = _task;
		this.target_value = _target_value;
		this.offset_value = 0;
		this.Background = Resources.Load<Texture2D>("VDQuest/Ramka");
		this.Check = Resources.Load<Texture2D>("VDQuest/Check");
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000633D4 File Offset: 0x000615D4
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

	// Token: 0x0600051C RID: 1308 RVA: 0x000635B1 File Offset: 0x000617B1
	public void SetOffsetValue(int _value)
	{
		this.offset_value = _value;
		if (this.current_value + this.offset_value > this.target_value)
		{
			this.offset_value = this.target_value - this.current_value;
		}
	}

	// Token: 0x04000932 RID: 2354
	public PERFORMANCE_STATUS TS = PERFORMANCE_STATUS.PENDING;

	// Token: 0x04000933 RID: 2355
	private int taskID;

	// Token: 0x04000934 RID: 2356
	private string Task;

	// Token: 0x04000935 RID: 2357
	private Texture2D Background;

	// Token: 0x04000936 RID: 2358
	private Texture2D Check;

	// Token: 0x04000937 RID: 2359
	public int target_value;

	// Token: 0x04000938 RID: 2360
	public int current_value;

	// Token: 0x04000939 RID: 2361
	public int offset_value;

	// Token: 0x0400093A RID: 2362
	private Rect CheckBoxRect;

	// Token: 0x0400093B RID: 2363
	private Rect TaskRect;
}
