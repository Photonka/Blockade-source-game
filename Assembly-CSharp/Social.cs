using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class Social : MonoBehaviour
{
	// Token: 0x06000110 RID: 272 RVA: 0x00017754 File Offset: 0x00015954
	private void myGlobalInit()
	{
		this.Active = false;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00017760 File Offset: 0x00015960
	private void OnGUI()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.SOCIAL)
		{
			return;
		}
		GUI.Window(903, new Rect((float)Screen.width / 2f - 300f, 199f, 600f, GUIManager.YRES(768f) - 180f), new GUI.WindowFunction(this.DoWindow), "", GUIManager.gs_empty);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000177D4 File Offset: 0x000159D4
	private void DoWindow(int windowID)
	{
		if (!this.Active)
		{
			return;
		}
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_panel);
		GUI.DrawTexture(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f), GUIManager.tex_half_black);
		int num = 40;
		GUIManager.DrawText(new Rect(0f, (float)num, 600f, 32f), "Выполните 4 пункта задания и получите 10 монет", 16, 4, 8);
		GUI.DrawTexture(new Rect(172f, (float)(num + 40), 256f, 32f), GUIManager.tex_social);
		GUIManager.DrawText(new Rect(172f, (float)(num + 40), 30f, 32f), "1", 16, 4, 8);
		GUIManager.DrawText(new Rect(200f, (float)(num + 40), 222f, 32f), "Рассказать друзьям", 16, 4, 8);
		if (GUI.Button(new Rect(172f, (float)(num + 40), 256f, 32f), "", GUIManager.gs_style1))
		{
			Application.ExternalCall("social1", new object[]
			{
				""
			});
			this.err = false;
		}
		GUI.DrawTexture(new Rect(172f, (float)(num + 80), 256f, 32f), GUIManager.tex_social);
		GUIManager.DrawText(new Rect(172f, (float)(num + 80), 30f, 32f), "2", 16, 4, 8);
		GUIManager.DrawText(new Rect(200f, (float)(num + 80), 222f, 32f), "Добавить в меню", 16, 4, 8);
		if (GUI.Button(new Rect(172f, (float)(num + 80), 256f, 32f), "", GUIManager.gs_style1))
		{
			Application.ExternalCall("social2", new object[]
			{
				""
			});
			this.err = false;
		}
		GUI.DrawTexture(new Rect(172f, (float)(num + 120), 256f, 32f), GUIManager.tex_social);
		GUIManager.DrawText(new Rect(172f, (float)(num + 120), 30f, 32f), "3", 16, 4, 8);
		GUIManager.DrawText(new Rect(200f, (float)(num + 120), 222f, 32f), "Пригласить друзей", 16, 4, 8);
		if (GUI.Button(new Rect(172f, (float)(num + 120), 256f, 32f), "", GUIManager.gs_style1))
		{
			Application.ExternalCall("social3", new object[]
			{
				""
			});
			this.err = false;
		}
		GUI.DrawTexture(new Rect(172f, (float)(num + 160), 256f, 32f), GUIManager.tex_social);
		GUIManager.DrawText(new Rect(172f, (float)(num + 160), 30f, 32f), "4", 16, 4, 8);
		GUIManager.DrawText(new Rect(200f, (float)(num + 160), 222f, 32f), "Вступить в группу", 16, 4, 8);
		if (GUI.Button(new Rect(172f, (float)(num + 160), 256f, 32f), "", GUIManager.gs_style1))
		{
			Application.ExternalCall("social4", new object[]
			{
				""
			});
			this.err = false;
		}
		if (this.err)
		{
			GUI.DrawTexture(new Rect(108f, (float)(num + 200), 384f, 32f), GUIManager.tex_warning);
			GUIManager.DrawText(new Rect(140f, (float)(num + 200), 352f, 32f), "Не все пункты выполнены!", 16, 4, 8);
		}
		GUI.DrawTexture(new Rect(204f, (float)(num + 240), 192f, 32f), GUIManager.tex_button);
		GUIManager.DrawText(new Rect(204f, (float)(num + 240), 192f, 32f), "Забрать бонус", 16, 4, 8);
		if (GUI.Button(new Rect(204f, (float)(num + 240), 192f, 32f), "", GUIManager.gs_style1))
		{
			Application.ExternalCall("social5", new object[]
			{
				""
			});
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00017C43 File Offset: 0x00015E43
	public void cb_social(string _result)
	{
		this.err = false;
		if (this.done)
		{
			return;
		}
		if (_result != "4")
		{
			this.err = true;
			return;
		}
		this.done = true;
		base.StartCoroutine(this.set_social());
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00017C7E File Offset: 0x00015E7E
	private IEnumerator set_social()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"19&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&name=",
			PlayerProfile.PlayerName,
			"&time=",
			DateTime.Now.Second,
			"&questid=1"
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			PopUp.ShowBonus(4, 1);
			PlayerProfile.money += 10;
			base.gameObject.GetComponent<MainMenu>().quest = false;
		}
		yield break;
	}

	// Token: 0x04000135 RID: 309
	public bool Active;

	// Token: 0x04000136 RID: 310
	private bool err;

	// Token: 0x04000137 RID: 311
	private bool done;
}
