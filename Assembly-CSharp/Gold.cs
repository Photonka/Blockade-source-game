using System;
using System.Collections;
using BestHTTP;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class Gold : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x00004A32 File Offset: 0x00002C32
	private void myGlobalInit()
	{
		this.Active = false;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004A3C File Offset: 0x00002C3C
	private void OnGUI()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.GOLD)
		{
			return;
		}
		GUI.Window(903, new Rect((float)Screen.width / 2f - 300f, 199f, 600f, GUIManager.YRES(768f) - 180f), new GUI.WindowFunction(this.DoWindow), "", GUIManager.gs_style3);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004AB0 File Offset: 0x00002CB0
	private void DoWindow(int windowID)
	{
		if (!this.Active)
		{
			return;
		}
		TextAnchor alignment = GUIManager.gs_style3.alignment;
		Color textColor = GUIManager.gs_style3.normal.textColor;
		int fontSize = GUIManager.gs_style3.fontSize;
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_panel);
		GUI.DrawTexture(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f), GUIManager.tex_half_black);
		this.scrollViewVector = GUIManager.BeginScrollView(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f), this.scrollViewVector, new Rect(0f, 0f, 0f, 368f));
		int num = PlayerProfile.moneypay / 30 + 1;
		if (num > 100)
		{
			num = 100;
		}
		GUIManager.gs_style3.fontSize = 22;
		GUIManager.gs_style3.alignment = 0;
		int num2 = 8;
		if (PlayerProfile.network == NETWORK.VK)
		{
			if (DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
			{
				this.DrawLine(1f, 6, 0, 8, num, 305, 0, PlayerProfile.network);
				this.DrawLine(5f, 30, 0, 40, num, 300, 1, PlayerProfile.network);
				this.DrawLine(10f, 60, 0, 80, num, 301, 2, PlayerProfile.network);
				this.DrawLine(30f, 180, 10, 240, num, 302, 3, PlayerProfile.network);
				this.DrawLine(50f, 300, 20, 400, num, 303, 4, PlayerProfile.network);
				this.DrawLine(100f, 600, 60, 800, num, 304, 5, PlayerProfile.network);
				this.DrawLine(500f, 3000, 500, 4000, num, 306, 6, PlayerProfile.network);
				this.DrawLine(1000f, 6000, 3000, 8000, num, 307, 7, PlayerProfile.network);
			}
			else
			{
				this.DrawLine(1f, 4, 0, 6, num, 205, 0, PlayerProfile.network);
				this.DrawLine(5f, 20, 0, 30, num, 200, 1, PlayerProfile.network);
				this.DrawLine(10f, 40, 0, 60, num, 201, 2, PlayerProfile.network);
				this.DrawLine(30f, 120, 10, 180, num, 202, 3, PlayerProfile.network);
				this.DrawLine(50f, 200, 20, 300, num, 203, 4, PlayerProfile.network);
				this.DrawLine(100f, 400, 60, 600, num, 204, 5, PlayerProfile.network);
				this.DrawLine(500f, 2000, 500, 3000, num, 206, 6, PlayerProfile.network);
				this.DrawLine(1000f, 4000, 3000, 6000, num, 207, 7, PlayerProfile.network);
			}
		}
		else if (PlayerProfile.network == NETWORK.OK)
		{
			this.DrawLine(2f, 0, 0, 1, num, 0, 0, PlayerProfile.network);
			this.DrawLine(10f, 0, 0, 5, num, 1, 1, PlayerProfile.network);
			this.DrawLine(20f, 0, 0, 10, num, 2, 2, PlayerProfile.network);
			this.DrawLine(40f, 0, 0, 20, num, 3, 3, PlayerProfile.network);
			this.DrawLine(80f, 0, 0, 40, num, 4, 4, PlayerProfile.network);
			this.DrawLine(240f, 0, 10, 120, num, 5, 5, PlayerProfile.network);
			this.DrawLine(400f, 0, 20, 200, num, 6, 6, PlayerProfile.network);
			this.DrawLine(800f, 0, 60, 400, num, 7, 7, PlayerProfile.network);
			this.DrawLine(4000f, 0, 1300, 2000, num, 8, 8, PlayerProfile.network);
			this.DrawLine(8000f, 0, 4000, 4000, num, 9, 9, PlayerProfile.network);
			num2 = 10;
		}
		else if (PlayerProfile.network == NETWORK.MM)
		{
			this.DrawLine(2f, 0, 0, 1, num, 0, 0, PlayerProfile.network);
			this.DrawLine(7f, 0, 0, 4, num, 1, 1, PlayerProfile.network);
			this.DrawLine(14f, 0, 0, 8, num, 2, 2, PlayerProfile.network);
			this.DrawLine(28f, 0, 0, 16, num, 3, 3, PlayerProfile.network);
			this.DrawLine(56f, 0, 0, 32, num, 4, 4, PlayerProfile.network);
			this.DrawLine(168f, 0, 10, 96, num, 5, 5, PlayerProfile.network);
			this.DrawLine(280f, 0, 20, 160, num, 6, 6, PlayerProfile.network);
			this.DrawLine(560f, 0, 60, 320, num, 7, 7, PlayerProfile.network);
			this.DrawLine(2800f, 0, 900, 1600, num, 8, 8, PlayerProfile.network);
			this.DrawLine(5600f, 0, 2000, 3200, num, 9, 9, PlayerProfile.network);
			num2 = 10;
		}
		else if (PlayerProfile.network == NETWORK.FB)
		{
			if (DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
			{
				this.DrawLine(0.99f, 0, 0, 33, num, 101, 1, PlayerProfile.network);
				this.DrawLine(4.95f, 0, 25, 165, num, 102, 2, PlayerProfile.network);
				this.DrawLine(9.9f, 0, 50, 330, num, 103, 3, PlayerProfile.network);
				this.DrawLine(29.7f, 0, 250, 990, num, 104, 4, PlayerProfile.network);
				this.DrawLine(49.5f, 0, 750, 1650, num, 105, 5, PlayerProfile.network);
			}
			else
			{
				this.DrawLine(1f, 0, 0, 25, num, 1, 1, PlayerProfile.network);
				this.DrawLine(5f, 0, 25, 125, num, 2, 2, PlayerProfile.network);
				this.DrawLine(10f, 0, 50, 250, num, 3, 3, PlayerProfile.network);
				this.DrawLine(30f, 0, 250, 750, num, 4, 4, PlayerProfile.network);
				this.DrawLine(50f, 0, 750, 1250, num, 5, 5, PlayerProfile.network);
			}
			num2 = 10;
		}
		else if (PlayerProfile.network == NETWORK.ST)
		{
			if (DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
			{
				this.DrawLine(0.99f, 0, 0, 33, num, 101, 1, PlayerProfile.network);
				this.DrawLine(4.95f, 0, 25, 165, num, 102, 2, PlayerProfile.network);
				this.DrawLine(9.9f, 0, 50, 330, num, 103, 3, PlayerProfile.network);
				this.DrawLine(29.7f, 0, 250, 990, num, 104, 4, PlayerProfile.network);
				this.DrawLine(49.5f, 0, 750, 1650, num, 105, 5, PlayerProfile.network);
			}
			else
			{
				this.DrawLine(0.1f, 0, 0, 6, num, 1, 1, PlayerProfile.network);
				this.DrawLine(0.5f, 0, 0, 30, num, 2, 2, PlayerProfile.network);
				this.DrawLine(1f, 0, 0, 60, num, 3, 3, PlayerProfile.network);
				this.DrawLine(3f, 0, 10, 180, num, 4, 4, PlayerProfile.network);
				this.DrawLine(5f, 0, 20, 300, num, 5, 5, PlayerProfile.network);
				this.DrawLine(10f, 0, 60, 600, num, 6, 6, PlayerProfile.network);
				this.DrawLine(50f, 0, 500, 3000, num, 7, 7, PlayerProfile.network);
				this.DrawLine(100f, 0, 3000, 6000, num, 8, 8, PlayerProfile.network);
			}
			num2 = 10;
		}
		else if (PlayerProfile.network == NETWORK.KG)
		{
			this.DrawLine(2f, 0, 0, 5, num, 7, 1, PlayerProfile.network);
			this.DrawLine(10f, 0, 0, 25, num, 1, 2, PlayerProfile.network);
			this.DrawLine(50f, 0, 25, 125, num, 2, 3, PlayerProfile.network);
			this.DrawLine(100f, 0, 50, 250, num, 3, 4, PlayerProfile.network);
			this.DrawLine(300f, 0, 250, 750, num, 4, 5, PlayerProfile.network);
			this.DrawLine(500f, 0, 750, 1250, num, 5, 6, PlayerProfile.network);
			num2 = 10;
		}
		GUIManager.gs_style3.fontSize = 16;
		int num3 = 0;
		if (PlayerProfile.network == NETWORK.VK && DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
		{
			GUIManager.gs_style3.normal.textColor = new Color(1f, 0f, 0f, 1f);
			GUIManager.gs_style3.alignment = 1;
			GUI.Label(new Rect(0f, (float)(40 * num2), 600f, 32f), Lang.GetLabel(514), GUIManager.gs_style3);
			num3 = 40;
		}
		if (PlayerProfile.network == NETWORK.FB && DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
		{
			GUIManager.gs_style3.normal.textColor = new Color(1f, 0f, 0f, 1f);
			GUIManager.gs_style3.alignment = 1;
			GUI.Label(new Rect(0f, (float)(40 * num2), 600f, 32f), Lang.GetLabel(514), GUIManager.gs_style3);
			num3 = 40;
		}
		GUIManager.gs_style3.normal.textColor = new Color(0f, 1f, 0f, 1f);
		GUIManager.gs_style3.alignment = 1;
		GUI.Label(new Rect(0f, (float)(40 * num2 + num3), 600f, 32f), Lang.GetLabel(23), GUIManager.gs_style3);
		GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUIManager.EndScrollView();
		GUIManager.gs_style3.alignment = alignment;
		GUIManager.gs_style3.normal.textColor = textColor;
		GUIManager.gs_style3.fontSize = fontSize;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000055A8 File Offset: 0x000037A8
	private void DrawLine(float golos, int money, int bonus, int newmoney, int discount, int itemid, int id, NETWORK network)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= 192f - this.scrollViewVector.y + 39f;
		int num3 = 54;
		if (network == NETWORK.OK)
		{
			num3 += 50;
		}
		int num4 = 40 * id + 8;
		Rect rect;
		rect..ctor(0f, (float)(num4 - 4), 490f, 32f);
		if (rect.Contains(new Vector2(num, num2)))
		{
			if (!this.hover[id])
			{
				this.hover[id] = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.hover[id])
		{
			this.hover[id] = false;
		}
		if (this.hover[id])
		{
			GUI.DrawTexture(new Rect(0f, (float)(num4 - 4), 600f, 32f), GUIManager.tex_half_yellow);
			GUI.DrawTexture(new Rect(0f, (float)(num4 + 32 - 4), 600f, 1f), GUIManager.tex_half_black);
		}
		GUIManager.gs_style3.alignment = 2;
		Rect rect2 = new Rect((float)num3, (float)(num4 + 5), 45f, 32f);
		Rect rect3 = new Rect((float)(num3 + 1), (float)(num4 + 1 + 5), 45f, 32f);
		GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
		GUI.Label(rect3, golos.ToString(), GUIManager.gs_style3);
		GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect2, golos.ToString(), GUIManager.gs_style3);
		GUIManager.gs_style3.alignment = 0;
		string text = "";
		if (network == NETWORK.VK)
		{
			text = "голосов";
			if (id == 0)
			{
				text = "голос";
			}
		}
		else if (network == NETWORK.OK)
		{
			text = "ок";
		}
		else if (network == NETWORK.MM)
		{
			text = "мэйликов";
			if (id == 0)
			{
				text = "мэйлика";
			}
		}
		else if (network == NETWORK.FB)
		{
			text = "USD";
		}
		else if (network == NETWORK.ST)
		{
			text = "USD";
		}
		else if (network == NETWORK.KG)
		{
			text = "KREDS";
		}
		Rect rect4 = new Rect((float)(num3 + 48), (float)(num4 + 8), 256f, 32f);
		Rect rect5 = new Rect((float)(num3 + 48 + 1), (float)(num4 + 1 + 8), 256f, 32f);
		GUIManager.gs_style3.fontSize = 12;
		GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
		GUI.Label(rect5, text, GUIManager.gs_style3);
		GUIManager.gs_style3.normal.textColor = new Color(0.35f, 0.75f, 1f, 1f);
		GUI.Label(rect4, text, GUIManager.gs_style3);
		GUIManager.gs_style3.fontSize = 22;
		GUIManager.gs_style3.alignment = 1;
		num3 += 100;
		GUI.DrawTexture(new Rect((float)num3, (float)(num4 - 4), 32f, 32f), GUIManager.tex_arrow);
		if (network == NETWORK.VK)
		{
			num3 += 30;
			Rect rect6 = new Rect((float)num3, (float)(num4 + 5), 55f, 32f);
			Rect rect7 = new Rect((float)(num3 + 1), (float)(num4 + 1 + 5), 55f, 32f);
			GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
			GUI.Label(rect7, money.ToString(), GUIManager.gs_style3);
			GUIManager.gs_style3.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 1f);
			GUI.Label(rect6, money.ToString(), GUIManager.gs_style3);
			GUI.DrawTexture(new Rect((float)(num3 - 4), (float)(num4 + 2), 64f, 16f), GUIManager.tex_crossline);
		}
		GUIManager.gs_style3.alignment = 2;
		num3 += 60;
		Rect rect8 = new Rect((float)num3, (float)(num4 + 5), 52f, 32f);
		Rect rect9 = new Rect((float)(num3 + 1), (float)(num4 + 1 + 5), 52f, 32f);
		GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
		GUI.Label(rect9, newmoney.ToString(), GUIManager.gs_style3);
		GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect8, newmoney.ToString(), GUIManager.gs_style3);
		num3 += 55;
		GUI.DrawTexture(new Rect((float)num3, (float)(num4 - 4), 32f, 32f), GUIManager.tex_coin);
		if (bonus > 0)
		{
			num3 += 34;
			if (network == NETWORK.OK)
			{
				num3 += 20;
			}
			Rect rect10 = new Rect((float)num3, (float)(num4 + 5), 65f, 32f);
			Rect rect11 = new Rect((float)(num3 + 1), (float)(num4 + 1 + 5), 65f, 32f);
			GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
			GUI.Label(rect11, "+" + bonus.ToString(), GUIManager.gs_style3);
			GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
			GUI.Label(rect10, "+" + bonus.ToString(), GUIManager.gs_style3);
			num3 += 68;
			GUI.DrawTexture(new Rect((float)num3, (float)(num4 - 4), 32f, 32f), GUIManager.tex_bonus);
			num3 += 35;
		}
		else
		{
			num3 += 137;
		}
		if (discount > 0)
		{
			int num5 = money / 100 * discount;
			if (num5 > 0)
			{
				Rect rect12 = new Rect((float)num3, (float)(num4 + 5), 55f, 32f);
				Rect rect13 = new Rect((float)(num3 + 1), (float)(num4 + 1 + 5), 55f, 32f);
				GUIManager.gs_style3.normal.textColor = new Color(0f, 0f, 0f, 1f);
				GUI.Label(rect13, "+" + num5.ToString(), GUIManager.gs_style3);
				GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
				GUI.Label(rect12, "+" + num5.ToString(), GUIManager.gs_style3);
				num3 += 42;
				GUI.DrawTexture(new Rect((float)num3, (float)(num4 - 4), 64f, 32f), GUIManager.tex_discount);
			}
		}
		if (GUI.Button(rect, "", GUIManager.gs_style3))
		{
			if (network == NETWORK.FB)
			{
				if (DateTime.UtcNow >= CONST.EXT.TIME_START)
				{
					DateTime.UtcNow < CONST.EXT.TIME_END;
					return;
				}
			}
			else
			{
				if (network == NETWORK.ST)
				{
					WEB_HANDLER.STEAM_BUY_ITEM((ulong)((long)itemid), 1, new OnRequestFinishedDelegate(Gold.OnSteamBuyItem));
					return;
				}
				Application.ExternalCall("order", new object[]
				{
					"item" + itemid.ToString()
				});
				PlayerProfile.get_player_stats = false;
			}
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005CE9 File Offset: 0x00003EE9
	private IEnumerator ReInit()
	{
		yield return new WaitForSeconds(2f);
		PlayerProfile.get_player_stats = false;
		yield break;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00005CF4 File Offset: 0x00003EF4
	public static void OnSteamBuyItem(HTTPRequest req, HTTPResponse resp)
	{
		switch (req.State)
		{
		case HTTPRequestStates.Finished:
			if (!resp.IsSuccess)
			{
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				return;
			}
			Debug.Log(resp.DataAsText);
			if (resp.DataAsText.Contains("API_ERROR"))
			{
				WEB_HANDLER.CHECK_ERROR(resp.DataAsText);
				return;
			}
			if (resp.DataAsText.Contains("DUBLICATE"))
			{
				Shop.THIS.message = Lang.GetLabel(108);
				return;
			}
			break;
		case HTTPRequestStates.Error:
			Debug.LogError("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
			return;
		case HTTPRequestStates.Aborted:
			Debug.LogWarning("Request Aborted!");
			return;
		case HTTPRequestStates.ConnectionTimedOut:
			Debug.LogError("Connection Timed Out!");
			return;
		case HTTPRequestStates.TimedOut:
			Debug.LogError("Processing the request Timed Out!");
			break;
		default:
			return;
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00005E04 File Offset: 0x00004004
	public static void OnSteamBuyItemFinish(HTTPRequest req, HTTPResponse resp)
	{
		switch (req.State)
		{
		case HTTPRequestStates.Finished:
			if (!resp.IsSuccess)
			{
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				return;
			}
			Debug.Log(resp.DataAsText);
			if (resp.DataAsText.Contains("ERR"))
			{
				WEB_HANDLER.CHECK_ERROR(resp.DataAsText);
				return;
			}
			if (resp.DataAsText.Contains("OK"))
			{
				PlayerProfile.get_player_stats = false;
				GameController.THIS.playerRefresh();
				Inv.needRefresh = true;
				Shop.THIS.message = Lang.GetLabel(107);
				return;
			}
			break;
		case HTTPRequestStates.Error:
			Debug.LogError("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
			return;
		case HTTPRequestStates.Aborted:
			Debug.LogWarning("Request Aborted!");
			return;
		case HTTPRequestStates.ConnectionTimedOut:
			Debug.LogError("Connection Timed Out!");
			return;
		case HTTPRequestStates.TimedOut:
			Debug.LogError("Processing the request Timed Out!");
			break;
		default:
			return;
		}
	}

	// Token: 0x04000059 RID: 89
	public bool Active;

	// Token: 0x0400005A RID: 90
	private bool[] hover = new bool[10];

	// Token: 0x0400005B RID: 91
	private Vector2 scrollViewVector = Vector2.zero;
}
