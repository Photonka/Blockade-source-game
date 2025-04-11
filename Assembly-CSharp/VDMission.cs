using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class VDMission
{
	// Token: 0x0600051D RID: 1309 RVA: 0x000635E4 File Offset: 0x000617E4
	public VDMission(int id, Vector2 _center, Color _mainColor, AudioSource _AS, bool _finish = false, bool _force_color = false)
	{
		this.inactive = new TweenButton(GUIGS.QUEST, _center, OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/Task"), null, null, null, null, null, _AS, 0f, 20f, 10, 120f, 96f, 0f);
		this.inactiveCurrent = new TweenButton(GUIGS.QUEST, _center, OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/Task_Selected"), null, null, null, Resources.Load<AudioClip>("Sound/onbutton"), Resources.Load<AudioClip>("Sound/clickbutton"), _AS, 0f, 20f, 10, 120f, 96f, 0f);
		this.inactiveCurrent.tt.Add(TWEEN_TYPE.BIGGER);
		this.complited = new TweenButton(GUIGS.QUEST, _center, OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/Task_Complited"), null, null, null, Resources.Load<AudioClip>("Sound/onbutton"), Resources.Load<AudioClip>("Sound/clickbutton"), _AS, 0f, 20f, 10, 120f, 96f, 0f);
		this.complited.tt.Add(TWEEN_TYPE.BIGGER);
		this.missionID = id;
		this.Header = Lang.GetLabel(600 + this.missionID);
		if (id == 1)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(621), 1));
		}
		else if (id == 2)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(622), 1000));
		}
		else if (id == 3)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(623), 600));
		}
		else if (id == 4)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(624), 1000));
		}
		else if (id == 5)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(625), 500));
		}
		else if (id == 6)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(626), 1000));
		}
		else if (id == 7)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(627), 200));
		}
		else if (id == 8)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(628), 100));
		}
		else if (id == 9)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(629), 200));
		}
		else if (id == 10)
		{
			this.TasksList.Add(1, new VDMissionTask(1, Lang.GetLabel(630), 1));
		}
		this.mainColor = _mainColor;
		this.force_color = _force_color;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x000638B4 File Offset: 0x00061AB4
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

	// Token: 0x0600051F RID: 1311 RVA: 0x000639A4 File Offset: 0x00061BA4
	public void DrawMissionText(Rect _r)
	{
		TextAnchor alignment = GUIManager.gs_style3.alignment;
		GUIManager.gs_style3.alignment = 0;
		int fontSize = GUIManager.gs_style3.fontSize;
		GUIManager.gs_style3.fontSize = 20;
		GUI.color = Color.black;
		GUI.Label(new Rect(_r.x + 1f, _r.y + 1f, _r.width, _r.height), this.Header, GUIManager.gs_style3);
		GUI.color = Color.yellow;
		GUI.Label(_r, this.Header, GUIManager.gs_style3);
		GUI.color = Color.white;
		float x = _r.x;
		float num = _r.y + 30f;
		if (this.TasksList.Count > 0)
		{
			foreach (int key in this.TasksList.Keys)
			{
				if (this.MS == PERFORMANCE_STATUS.COMPLITED)
				{
					this.TasksList[key].TS = PERFORMANCE_STATUS.COMPLITED;
				}
				this.TasksList[key].DrawTask(new Rect(x, num, _r.width, 20f));
				num += 20f;
			}
		}
		GUIManager.gs_style3.fontSize = fontSize;
		GUIManager.gs_style3.alignment = alignment;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00063B14 File Offset: 0x00061D14
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

	// Token: 0x06000521 RID: 1313 RVA: 0x00063B75 File Offset: 0x00061D75
	public void DropButtonsSize()
	{
		this.inactiveCurrent.DropSize();
		this.complited.DropSize();
	}

	// Token: 0x0400093C RID: 2364
	public PERFORMANCE_STATUS MS;

	// Token: 0x0400093D RID: 2365
	private int missionID;

	// Token: 0x0400093E RID: 2366
	private string Header;

	// Token: 0x0400093F RID: 2367
	private TweenButton inactive;

	// Token: 0x04000940 RID: 2368
	private TweenButton inactiveCurrent;

	// Token: 0x04000941 RID: 2369
	private TweenButton complited;

	// Token: 0x04000942 RID: 2370
	private Color mainColor;

	// Token: 0x04000943 RID: 2371
	private bool force_color;

	// Token: 0x04000944 RID: 2372
	public Dictionary<int, VDMissionTask> TasksList = new Dictionary<int, VDMissionTask>();

	// Token: 0x04000945 RID: 2373
	private Color c;
}
