﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class VictoryDayQuest : MonoBehaviour
{
	// Token: 0x06000522 RID: 1314 RVA: 0x00063B90 File Offset: 0x00061D90
	private void myGlobalInit()
	{
		this.background = Resources.Load<Texture2D>("VDQuest/Map" + Lang.current.ToString());
		this.missionBackground = Resources.Load<Texture2D>("VDQuest/Back");
		this.closeButton = new TweenButton(GUIGS.QUEST, new Vector2(287f, -220f), OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/Close_button_above"), null, null, null, Resources.Load<AudioClip>("Sound/onbutton"), Resources.Load<AudioClip>("Sound/clickbutton"), this.AS, 0f, 20f, 10, 32f, 32f, 0f);
		this.closeButton.tt.Add(TWEEN_TYPE.BIGGER);
		this.HelpButton = new TweenButton(GUIGS.QUEST, new Vector2(360f, 80f), OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/About_button_animation"), null, Resources.Load<Texture2D>("VDQuest/About_button_back"), null, Resources.Load<AudioClip>("Sound/onbutton"), Resources.Load<AudioClip>("Sound/clickbutton"), this.AS, 0f, 20f, 50, 36f, 36f, 0f);
		this.HelpButton.tt.Add(TWEEN_TYPE.BIGGER);
		this.HelpButton.tt.Add(TWEEN_TYPE.ROTATE);
		this.GetReward = new TweenButton(GUIGS.QUEST, new Vector2(170f, 155f), OFFSET_TYPE.FROM_CENTER, Resources.Load<Texture2D>("VDQuest/TakeReward" + Lang.current.ToString()), null, null, null, Resources.Load<AudioClip>("Sound/onbutton"), Resources.Load<AudioClip>("Sound/clickbutton"), this.AS, 0f, 10f, 0, 330f, 120f, 0f);
		this.GetReward.tt.Add(TWEEN_TYPE.BIGGER);
		if (VictoryDayQuest.MissionsList.Count == 0)
		{
			VictoryDayQuest.MissionsList.Add(1, new VDMission(1, new Vector2(-149f, 187f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(2, new VDMission(2, new Vector2(-223f, 81f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(3, new VDMission(3, new Vector2(-180f, -74f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(4, new VDMission(4, new Vector2(-82f, 0f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(5, new VDMission(5, new Vector2(42f, -90f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(6, new VDMission(6, new Vector2(116f, -7f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(7, new VDMission(7, new Vector2(264f, -23f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(8, new VDMission(8, new Vector2(224f, -134f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(9, new VDMission(9, new Vector2(161f, -208f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(10, new VDMission(10, new Vector2(42f, -224f), Color.white, this.AS, false, false));
			VictoryDayQuest.MissionsList.Add(11, new VDMission(11, new Vector2(-4900f, -3100f), Color.white, this.AS, true, true));
		}
		base.StartCoroutine(this.GetPassiveStatus());
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00063F55 File Offset: 0x00062155
	public void ShowQuest()
	{
		GM.currGUIState = GUIGS.QUEST;
		this.missions_status = false;
		base.StartCoroutine(this.GetMissionStatus());
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00062B14 File Offset: 0x00060D14
	public void CloseQuest()
	{
		base.GetComponent<MainMenu>().OpenServerList();
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00063F74 File Offset: 0x00062174
	private void OnGUI()
	{
		if (GM.currGUIState != GUIGS.QUEST)
		{
			return;
		}
		GUI.depth = -99;
		Vector2 mpos;
		mpos..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		if (!this.DrawBase(mpos))
		{
			return;
		}
		if (!VictoryDayQuest.uploading_screenshot && this.closeButton.DrawButton(mpos))
		{
			this.CloseQuest();
		}
		if (this.selectedMission < 11 && VictoryDayQuest.currentMission != 11 && !VictoryDayQuest.uploading_screenshot && this.HelpButton.DrawButton(mpos))
		{
			PopUp.ShowBonus(2017, 1);
		}
		this.missionRect = new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2 + 75), 460f, 210f);
		if (VictoryDayQuest.MissionsList.Count > 0)
		{
			foreach (int num in VictoryDayQuest.MissionsList.Keys)
			{
				if (num < VictoryDayQuest.currentMission)
				{
					VictoryDayQuest.MissionsList[num].MS = PERFORMANCE_STATUS.COMPLITED;
				}
				else if (num == VictoryDayQuest.currentMission)
				{
					VictoryDayQuest.MissionsList[num].MS = PERFORMANCE_STATUS.PENDING;
				}
				else
				{
					VictoryDayQuest.MissionsList[num].MS = PERFORMANCE_STATUS.INACTIVE;
				}
				if (VictoryDayQuest.MissionsList[num].DrawMissionPoint(mpos, this.selectedMission == num) && VictoryDayQuest.MissionsList[num].MS != PERFORMANCE_STATUS.INACTIVE)
				{
					this.selectedMission = num;
				}
			}
			if (VictoryDayQuest.currentMission != 11 && this.selectedMission < 11)
			{
				VictoryDayQuest.MissionsList[this.selectedMission].DrawMissionText(this.missionRect);
			}
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0006413C File Offset: 0x0006233C
	private bool DrawBase(Vector2 mpos)
	{
		Color color = GUI.color;
		GUI.color = new Color(color.r, color.g, color.b, 0.8f);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIManager.tex_black);
		GUI.color = color;
		if (!this.missions_status)
		{
			GUIManager.DrawLoading();
			return false;
		}
		this.mainRect = new Rect((float)(Screen.width / 2 - this.background.width / 2), (float)(Screen.height / 2 - this.background.height / 2), (float)this.background.width, (float)this.background.height);
		this.missionRect = new Rect((float)(Screen.width / 2 - 65), (float)(Screen.height / 2 + 50), 460f, 210f);
		GUI.DrawTexture(this.mainRect, this.background);
		GUI.DrawTexture(this.missionRect, this.missionBackground);
		TextAnchor alignment = GUIManager.gs_style1.alignment;
		GUIManager.gs_style1.richText = true;
		GUIManager.gs_style1.alignment = 4;
		GUIManager.gs_style1.fontSize -= 5;
		if (VictoryDayQuest.currentMission > 11 && this.selectedMission > 10)
		{
			GUIManager.gs_style1.fontSize += 8;
			GUI.color = Color.black;
			GUIManager.gs_style1.alignment = 1;
			GUI.Label(new Rect(this.missionRect.x, this.missionRect.y + 20f, this.missionRect.width, this.missionRect.height), Lang.GetLabel(545), GUIManager.gs_style1);
			GUIManager.gs_style1.fontSize -= 2;
			GUIManager.gs_style1.alignment = 4;
			GUI.Label(new Rect(this.missionRect.x, this.missionRect.y + 10f, this.missionRect.width, this.missionRect.height), Lang.GetLabel(546), GUIManager.gs_style1);
			GUIManager.gs_style1.fontSize += 2;
			GUI.color = Color.white;
			GUIManager.gs_style1.alignment = 1;
			GUI.Label(new Rect(this.missionRect.x, this.missionRect.y + 20f, this.missionRect.width, this.missionRect.height), Lang.GetLabel(545), GUIManager.gs_style1);
			GUIManager.gs_style1.fontSize -= 2;
			GUIManager.gs_style1.alignment = 4;
			GUI.Label(new Rect(this.missionRect.x, this.missionRect.y + 10f, this.missionRect.width, this.missionRect.height), Lang.GetLabel(546), GUIManager.gs_style1);
			GUIManager.gs_style1.fontSize -= 6;
			if ((PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.FB) && !VictoryDayQuest.uploading_screenshot && GUIManager.DrawButton2(new Vector2(this.missionRect.xMax - 180f, this.missionRect.yMax - 25f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				VictoryDayQuest.uploading_screenshot = true;
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				if (PlayerProfile.network == NETWORK.VK)
				{
					Application.ExternalCall("getfotoserver", Array.Empty<object>());
				}
				else
				{
					base.StartCoroutine(Handler.TakeScreenshot(2));
				}
			}
		}
		else if (VictoryDayQuest.currentMission == 11 && this.GetReward.DrawButton(mpos))
		{
			base.StartCoroutine(this.GetMyReward());
		}
		GUIManager.gs_style1.fontSize += 5;
		GUIManager.gs_style1.richText = false;
		GUIManager.gs_style1.alignment = alignment;
		if (this.selectedMission <= 10 && VictoryDayQuest.currentMission < 11 && (PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.FB) && !VictoryDayQuest.uploading_screenshot && GUIManager.DrawButton2(new Vector2(this.missionRect.xMax - 180f, this.missionRect.yMax - 25f), Vector2.zero, Lang.GetLabel(229), 1))
		{
			VictoryDayQuest.uploading_screenshot = true;
			if (Screen.fullScreen)
			{
				Config.ChangeMode();
			}
			if (PlayerProfile.network == NETWORK.VK)
			{
				Application.ExternalCall("getfotoserver", Array.Empty<object>());
			}
			else
			{
				base.StartCoroutine(Handler.TakeScreenshot(2));
			}
		}
		return true;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000645E0 File Offset: 0x000627E0
	private IEnumerator GetMissionStatus()
	{
		string text = string.Concat(new string[]
		{
			CONST.HANDLER_SERVER,
			"300&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&questID=2"
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			if (!www.text.Contains("ERROR"))
			{
				string[] array = www.text.Split(new char[]
				{
					'^'
				});
				int.TryParse(array[0], out VictoryDayQuest.currentMission);
				if (VictoryDayQuest.currentMission > 11)
				{
					this.selectedMission = 11;
				}
				else
				{
					this.selectedMission = VictoryDayQuest.currentMission;
				}
				if (VictoryDayQuest.currentMission < 11)
				{
					VictoryDayQuest.MissionsList[VictoryDayQuest.currentMission].SetStats(array[1]);
				}
			}
			else
			{
				Debug.Log("ERROR");
			}
		}
		if (VictoryDayQuest.MissionsList.Count > 0)
		{
			foreach (VDMission vdmission in VictoryDayQuest.MissionsList.Values)
			{
				vdmission.DropButtonsSize();
			}
		}
		yield return null;
		this.missions_status = true;
		yield break;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000645EF File Offset: 0x000627EF
	private IEnumerator GetPassiveStatus()
	{
		string text = string.Concat(new string[]
		{
			CONST.HANDLER_SERVER,
			"300&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&questID=2"
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null && !www.text.Contains("ERROR"))
		{
			string[] array = www.text.Split(new char[]
			{
				'^'
			});
			int.TryParse(array[0], out VictoryDayQuest.currentMission);
			if (VictoryDayQuest.currentMission > 11)
			{
				this.selectedMission = 11;
			}
			else
			{
				this.selectedMission = VictoryDayQuest.currentMission;
			}
			if (VictoryDayQuest.currentMission < 11 && VictoryDayQuest.MissionsList.ContainsKey(VictoryDayQuest.currentMission))
			{
				VictoryDayQuest.MissionsList[VictoryDayQuest.currentMission].SetStats(array[1]);
			}
		}
		if (VictoryDayQuest.MissionsList.Count > 0)
		{
			foreach (VDMission vdmission in VictoryDayQuest.MissionsList.Values)
			{
				vdmission.DropButtonsSize();
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000645FE File Offset: 0x000627FE
	private IEnumerator GetMyReward()
	{
		string text = string.Concat(new string[]
		{
			CONST.HANDLER_SERVER,
			"302&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&questID=2"
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			if (!www.text.Contains("ERROR"))
			{
				int.TryParse(www.text, out VictoryDayQuest.currentMission);
				if (VictoryDayQuest.currentMission > 11)
				{
					this.selectedMission = 11;
					PopUp.ShowBonus(2017, 2);
					PlayerProfile.money += 50;
					Inv.needRefresh = true;
				}
			}
			else
			{
				Debug.Log("ERROR");
			}
		}
		if (VictoryDayQuest.MissionsList.Count > 0)
		{
			foreach (VDMission vdmission in VictoryDayQuest.MissionsList.Values)
			{
				vdmission.DropButtonsSize();
			}
		}
		yield return null;
		this.missions_status = true;
		yield break;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00064610 File Offset: 0x00062810
	public static void IncMissionTaskValueRuntime(int mID, int tID)
	{
		if (VictoryDayQuest.currentMission != mID)
		{
			return;
		}
		VictoryDayQuest.MissionsList[mID].TasksList[tID].current_value++;
		if (VictoryDayQuest.MissionsList[mID].TasksList[tID].current_value > VictoryDayQuest.MissionsList[mID].TasksList[tID].target_value)
		{
			VictoryDayQuest.MissionsList[mID].TasksList[tID].current_value = VictoryDayQuest.MissionsList[mID].TasksList[tID].target_value;
		}
	}

	// Token: 0x04000946 RID: 2374
	public AudioSource AS;

	// Token: 0x04000947 RID: 2375
	private Rect mainRect;

	// Token: 0x04000948 RID: 2376
	private Texture2D background;

	// Token: 0x04000949 RID: 2377
	private Texture2D missionBackground;

	// Token: 0x0400094A RID: 2378
	private TweenButton closeButton;

	// Token: 0x0400094B RID: 2379
	private TweenButton HelpButton;

	// Token: 0x0400094C RID: 2380
	private TweenButton GetReward;

	// Token: 0x0400094D RID: 2381
	public static int currentMission = 0;

	// Token: 0x0400094E RID: 2382
	private int selectedMission;

	// Token: 0x0400094F RID: 2383
	private Rect missionRect;

	// Token: 0x04000950 RID: 2384
	public static Dictionary<int, VDMission> MissionsList = new Dictionary<int, VDMission>();

	// Token: 0x04000951 RID: 2385
	private bool missions_status;

	// Token: 0x04000952 RID: 2386
	public static bool uploading_screenshot = false;
}
