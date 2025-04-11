using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class Chat : MonoBehaviour
{
	// Token: 0x0600035D RID: 861 RVA: 0x0003D260 File Offset: 0x0003B460
	private void Awake()
	{
		this.Map = GameObject.Find("Map");
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[1];
		this.gui_style.fontSize = 14;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 255f);
		this.lastupdate = Time.time;
		this.tex_chat = (Resources.Load("GUI/base/chat") as Texture2D);
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
	private void Update()
	{
		if (Time.time > this.lastupdate + 10f)
		{
			this.AddMessage(-1, 0, "", 0);
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0003D314 File Offset: 0x0003B514
	private void OnGUI()
	{
		if (!this.show)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			GUIManager.DrawColorText(20f, (float)(Screen.height - 160 + i * 20), this.message[i], 3);
		}
		if (!this.entermode && Input.GetKeyUp(116))
		{
			this.teamsay = 1;
			this.entermode = true;
			this.chat_prefix = Lang.GetLabel(126);
			MainGUI.ForceCursor = true;
		}
		if (!this.entermode && Input.GetKeyUp(13))
		{
			this.teamsay = 0;
			this.entermode = true;
			this.chat_prefix = Lang.GetLabel(127);
			MainGUI.ForceCursor = true;
		}
		if (this.entermode)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - 100f - 80f, (float)(Screen.height - 100 + 1), 275f, 23f), this.tex_chat);
			GUIManager.DrawColorText((float)Screen.width / 2f - 100f - 78f, (float)(Screen.height - 100 + 2), this.chat_prefix, 3);
			GUI.SetNextControlName("input");
			GUIManager.BeginScrollView(new Rect((float)Screen.width / 2f - 100f, (float)(Screen.height - 100 + 4), 200f, 25f), new Vector2(0f, 0f), new Rect(0f, 0f, 0f, 0f));
			TextAnchor alignment = GUIManager.gs_style2.alignment;
			GUIManager.gs_style2.alignment = 0;
			this.stringToEdit = GUI.TextField(new Rect(0f, 0f, 165f, 25f), this.stringToEdit, 64, GUIManager.gs_style2);
			GUIManager.gs_style2.alignment = alignment;
			GUIManager.EndScrollView();
			GUI.FocusControl("input");
			int length = this.stringToEdit.Length;
			this.stringToEdit = this.stringToEdit.Replace("\n", "").Replace("\r", "");
			if (Event.current.type == 4 && Event.current.character == '\n')
			{
				if (this.stringToEdit.Length != 0)
				{
					if (this.cscl == null)
					{
						this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
					}
					this.stringToEdit = this.stringToEdit.Replace("^0", "^8");
					this.stringToEdit = this.stringToEdit.Replace("^1", "^8");
					this.stringToEdit = this.stringToEdit.Replace("^2", "^8");
					this.stringToEdit = this.stringToEdit.Replace("^3", "^8");
					this.stringToEdit = this.stringToEdit.Replace("^4", "^8");
					this.stringToEdit = this.stringToEdit.Replace("^9", "^8");
					this.cscl.send_chat(this.teamsay, this.stringToEdit);
					this.entertime = Time.time + 0.3f;
				}
				this.stringToEdit = "";
				MainGUI.ForceCursor = false;
				this.entermode = false;
			}
			if (this.stringToEdit.Length == 63)
			{
				this.stringToEdit = this.stringToEdit.Substring(0, this.stringToEdit.Length - 1);
			}
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0003D69C File Offset: 0x0003B89C
	public void AddMessage(int index, int team, string msg, int teamchat)
	{
		for (int i = 0; i < 5; i++)
		{
			this.message[i] = this.message[i + 1];
		}
		if (index >= 0 && teamchat < 2)
		{
			string str = "";
			if (teamchat > 0)
			{
				str = "^8(" + Lang.GetLabel(126) + ")";
			}
			switch (team)
			{
			case 0:
				str += "^0";
				break;
			case 1:
				str += "^1";
				break;
			case 2:
				str += "^2";
				break;
			case 3:
				str += "^3";
				break;
			}
			this.message[5] = str + BotsController.Instance.Bots[index].Name + "^8 : " + msg;
		}
		else if (teamchat == 2)
		{
			string text = "";
			switch (team)
			{
			case 0:
				text += "^0";
				break;
			case 1:
				text += "^1";
				break;
			case 2:
				text += "^2";
				break;
			case 3:
				text += "^3";
				break;
			}
			this.message[5] = Lang.GetLabel(665) + text + BotsController.Instance.Bots[index].Name + Lang.GetLabel(666);
		}
		else
		{
			this.message[5] = msg;
		}
		this.lastupdate = Time.time;
	}

	// Token: 0x04000633 RID: 1587
	private Client cscl;

	// Token: 0x04000634 RID: 1588
	private GameObject LocalPlayer;

	// Token: 0x04000635 RID: 1589
	private GameObject Map;

	// Token: 0x04000636 RID: 1590
	private GUIStyle gui_style;

	// Token: 0x04000637 RID: 1591
	private byte teamsay;

	// Token: 0x04000638 RID: 1592
	private bool entermode;

	// Token: 0x04000639 RID: 1593
	private bool messageready;

	// Token: 0x0400063A RID: 1594
	private string stringToEdit = "";

	// Token: 0x0400063B RID: 1595
	private bool userHasHitReturn;

	// Token: 0x0400063C RID: 1596
	private string chat_prefix = "";

	// Token: 0x0400063D RID: 1597
	private string[] message = new string[6];

	// Token: 0x0400063E RID: 1598
	private float lastupdate;

	// Token: 0x0400063F RID: 1599
	private Texture2D tex_chat;

	// Token: 0x04000640 RID: 1600
	private float entertime;

	// Token: 0x04000641 RID: 1601
	private float starttime;

	// Token: 0x04000642 RID: 1602
	public bool show = true;
}
