﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200001B RID: 27
public class PasswordGUI : MonoBehaviour
{
	// Token: 0x060000BD RID: 189 RVA: 0x0000C173 File Offset: 0x0000A373
	public void Show(bool val, CServerData _server)
	{
		this.server = _server;
		this.Active = val;
		this._lastState = GM.currGUIState;
		GM.currGUIState = GUIGS.PASSWORD;
		this.msg = Lang.GetLabel(33) + this.server.name;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
	private void OnGUI()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.PASSWORD)
		{
			return;
		}
		GUI.depth = -99;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIManager.tex_black);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - 256f - 2f, (float)Screen.height / 2f - 32f - 2f, 516f, 136f), GUIManager.tex_black);
		Vector2 mpos;
		mpos..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - 256f, (float)Screen.height / 2f - 32f, 512f, 100f), GUIManager.tex_panel);
		Rect rect;
		rect..ctor((float)Screen.width / 2f - 256f, (float)Screen.height / 2f - 32f, 512f, 32f);
		new Rect((float)Screen.width / 2f - 256f, (float)Screen.height / 2f, 512f, 32f);
		Rect r;
		r..ctor((float)Screen.width / 2f - 48f, (float)Screen.height / 2f, 96f, 20f);
		Rect r2 = new Rect((float)Screen.width / 2f - 256f + 10f, (float)Screen.height / 2f + 30f, 128f, 32f);
		Rect r3;
		r3..ctor((float)Screen.width / 2f + 118f, (float)Screen.height / 2f + 30f, 128f, 32f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect, this.msg, GUIManager.gs_style1);
		this.password = GUIManager.DrawEdit(r, mpos, new Color(0f, 1f, 0f, 1f), this.password, 6);
		if (GUIManager.DrawButton(r2, mpos, new Color(1f, 0f, 0f, 1f), Lang.GetLabel(34)))
		{
			this.Active = false;
			GM.currGUIState = this._lastState;
		}
		if (GUIManager.DrawButton(r3, mpos, new Color(0f, 1f, 0f, 1f), Lang.GetLabel(35)))
		{
			int num = 0;
			int.TryParse(this.password, out num);
			this.Active = false;
			ConnectionInfo.PRIVATE = true;
			ConnectionInfo.IP = this.server.ip;
			ConnectionInfo.PORT = this.server.port;
			ConnectionInfo.HOSTNAME = this.server.name;
			ConnectionInfo.PASSWORD = num;
			ConnectionInfo.mode = this.server.gamemode;
			GameController.STATE = GAME_STATES.GAME;
			GM.currExtState = GAME_STATES.NULL;
			SceneManager.LoadScene(1);
		}
	}

	// Token: 0x040000B3 RID: 179
	public bool Active;

	// Token: 0x040000B4 RID: 180
	private string msg = "";

	// Token: 0x040000B5 RID: 181
	private string password = "";

	// Token: 0x040000B6 RID: 182
	private CServerData server;

	// Token: 0x040000B7 RID: 183
	private GUIGS _lastState;
}
