using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class NYMission
{
	// Token: 0x06000509 RID: 1289 RVA: 0x00062502 File Offset: 0x00060702
	public NYMission(int id, Vector2 _center, Color _mainColor, float _size, AudioSource _AS, bool _finish = false, bool _force_color = false)
	{
		this.mainColor = _mainColor;
		this.force_color = _force_color;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00062524 File Offset: 0x00060724
	public bool DrawMissionPoint(Vector2 mpos, bool selected)
	{
		bool result = false;
		if (this.MS == PERFORMANCE_STATUS.INACTIVE)
		{
			this.inactive.DrawButton(mpos);
		}
		else if (this.MS == PERFORMANCE_STATUS.PENDING)
		{
			if (this.force_color)
			{
				this.c = GUI.color;
				GUI.color = this.mainColor;
			}
			if (!this.inactiveCurrent.draw)
			{
				this.inactiveCurrent.GO();
			}
			else
			{
				this.inactiveCurrent.isSelected = selected;
				result = this.inactiveCurrent.DrawButton(mpos);
			}
			if (this.force_color)
			{
				GUI.color = this.c;
			}
		}
		else
		{
			this.c = GUI.color;
			GUI.color = this.mainColor;
			if (!this.complited.draw)
			{
				this.complited.GO();
			}
			else
			{
				this.complited.isSelected = selected;
				result = this.complited.DrawButton(mpos);
			}
			GUI.color = this.c;
		}
		return result;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00002B75 File Offset: 0x00000D75
	public void DrawMissionText(Rect _r)
	{
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00062614 File Offset: 0x00060814
	public void SetStats(string _stats)
	{
		string[] array = _stats.Split(new char[]
		{
			'|'
		});
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				int.TryParse(array[i], out this.TasksList[i + 1].current_value);
				this.TasksList[i + 1].offset_value = 0;
			}
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00002B75 File Offset: 0x00000D75
	public void DropButtonsSize()
	{
	}

	// Token: 0x04000914 RID: 2324
	public PERFORMANCE_STATUS MS;

	// Token: 0x04000915 RID: 2325
	private int missionID;

	// Token: 0x04000916 RID: 2326
	private string Header;

	// Token: 0x04000917 RID: 2327
	private TweenButton inactive;

	// Token: 0x04000918 RID: 2328
	private TweenButton inactiveCurrent;

	// Token: 0x04000919 RID: 2329
	private TweenButton complited;

	// Token: 0x0400091A RID: 2330
	private Color mainColor;

	// Token: 0x0400091B RID: 2331
	private bool force_color;

	// Token: 0x0400091C RID: 2332
	public Dictionary<int, NYMissionTask> TasksList = new Dictionary<int, NYMissionTask>();

	// Token: 0x0400091D RID: 2333
	private Color c;
}
