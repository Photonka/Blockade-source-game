﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class SaveGUI : MonoBehaviour
{
	// Token: 0x060003CB RID: 971 RVA: 0x0004ADAC File Offset: 0x00048FAC
	private void Awake()
	{
		this.r_window = new Rect(0f, 0f, 600f, 400f);
		this.r_window.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0004ADFE File Offset: 0x00048FFE
	public void SetActive(bool val)
	{
		this.active = val;
		if (val && !this.dataload)
		{
			base.StartCoroutine(this.get_mymaps());
		}
		MainGUI.ForceCursor = this.active;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0004AE2A File Offset: 0x0004902A
	private void OnGUI()
	{
		if (!this.active)
		{
			return;
		}
		GUI.Window(0, this.r_window, new GUI.WindowFunction(this.DrawWindow), "", GUIManager.gs_empty);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0004AE58 File Offset: 0x00049058
	private void DrawWindow(int wid)
	{
		Vector2 mpos;
		mpos..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		mpos.x -= this.r_window.x;
		mpos.y -= this.r_window.y;
		GUI.color = new Color(1f, 1f, 1f, 0.8f);
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_black);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.Label(new Rect(0f, 0f, 600f, 32f), Lang.GetLabel(129), GUIManager.gs_style1);
		GUI.color = new Color(1f, 1f, 1f, 0.6f);
		GUI.DrawTexture(new Rect(0f, 34f, 600f, 366f), GUIManager.tex_black);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		int num = 172;
		int num2 = 60;
		foreach (int mapid in this.map)
		{
			if (GUIManager.DrawButton(new Rect((float)num, (float)num2, 256f, 32f), mpos, new Color(0f, 1f, 0f, 1f), Lang.GetLabel(143) + mapid.ToString()))
			{
				this.SetActive(false);
				if (this.cscl == null)
				{
					this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
				}
				this.cscl.send_private_settings(2, mapid);
			}
			num2 += 40;
		}
		if (PlayerControl.GetGameMode() == 1 || PlayerControl.GetGameMode() == 2)
		{
			if (GUIManager.DrawButton(new Rect((float)num, 298f, 256f, 32f), mpos, new Color(0f, 1f, 0f, 1f), Lang.GetLabel(144)))
			{
				this.SetActive(false);
				base.gameObject.GetComponent<MapSize>().OnActive();
			}
			if (GUIManager.DrawButton(new Rect((float)num, 358f, 256f, 32f), mpos, new Color(1f, 0f, 0f, 1f), Lang.GetLabel(145)))
			{
				this.SetActive(false);
				if (this.cscl == null)
				{
					this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
				}
				this.cscl.send_savemap();
			}
		}
		if (GUIManager.DrawButton(new Rect(462f, 358f, 128f, 32f), mpos, new Color(0f, 1f, 0f, 1f), Lang.GetLabel(123)))
		{
			this.SetActive(false);
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0004B1A8 File Offset: 0x000493A8
	public void Init()
	{
		base.StartCoroutine(this.get_mymaps());
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x0004B1B7 File Offset: 0x000493B7
	private IEnumerator get_mymaps()
	{
		this.dataload = true;
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"18&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			this.map.Clear();
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			int num = 0;
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string s = array2[i];
				if (num == 0)
				{
					if (!int.TryParse(s, out this.csmode))
					{
						this.csmode = 0;
						goto IL_135;
					}
					goto IL_135;
				}
				else
				{
					int item = 0;
					if (int.TryParse(s, out item))
					{
						this.map.Add(item);
						goto IL_135;
					}
				}
				IL_13B:
				i++;
				continue;
				IL_135:
				num++;
				goto IL_13B;
			}
		}
		else
		{
			this.dataload = false;
		}
		yield break;
	}

	// Token: 0x040007FF RID: 2047
	public bool active;

	// Token: 0x04000800 RID: 2048
	private Rect r_window;

	// Token: 0x04000801 RID: 2049
	private PlayerControl cspc;

	// Token: 0x04000802 RID: 2050
	private Client cscl;

	// Token: 0x04000803 RID: 2051
	private List<int> map = new List<int>();

	// Token: 0x04000804 RID: 2052
	private bool dataload;

	// Token: 0x04000805 RID: 2053
	public int csmode;
}
