using System;
using BestHTTP;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class WEB_HANDLER
{
	// Token: 0x060005B1 RID: 1457 RVA: 0x0006A64E File Offset: 0x0006884E
	public static void START_GAME(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(50, ""), _callback).Send();
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0006A668 File Offset: 0x00068868
	public static void AUTH(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(1, "&DID=" + SystemInfo.deviceUniqueIdentifier), _callback).Send();
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0006A68B File Offset: 0x0006888B
	public static void CHANGE_NICKNAME(string newName, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(7, "&NEW_NAME=" + newName + "&ID=" + PlayerProfile.id), _callback).Send();
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0006A6B4 File Offset: 0x000688B4
	public static void GET_SERVER_LIST(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(4, ""), _callback).Send();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0006A6CD File Offset: 0x000688CD
	public static void GET_FAST_SERVER(string filters, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(5, "&FILTERS=" + filters), _callback).Send();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0006A6EC File Offset: 0x000688EC
	public static void GET_USER_ITEMS(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(2, "&ACC_ID=" + PlayerProfile.id + "&GAME_SESSION=" + PlayerProfile.session), _callback).Send();
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0006A719 File Offset: 0x00068919
	public static void GET_USER_MISSIONS(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(8, "&ACC_ID=" + PlayerProfile.id + "&GAME_SESSION=" + PlayerProfile.session), _callback).Send();
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0006A746 File Offset: 0x00068946
	public static void GET_DAILY_BONUS(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(10, "&ACC_ID=" + PlayerProfile.id + "&GAME_SESSION=" + PlayerProfile.session), _callback).Send();
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0006A774 File Offset: 0x00068974
	public static void GET_MY_LVL_BONUS(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(11, "&ACC_ID=" + PlayerProfile.id + "&GAME_SESSION=" + PlayerProfile.session), _callback).Send();
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0006A7A2 File Offset: 0x000689A2
	public static void BUY_ITEM(int CURRENT_ITEM_ID, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(3, "&ACC_ID=" + PlayerProfile.id + "&ITEM_ID=" + CURRENT_ITEM_ID.ToString()), _callback).Send();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0006A7D1 File Offset: 0x000689D1
	public static void OPEN_ITEM(int CURRENT_ITEM_ID, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(9, "&ACC_ID=" + PlayerProfile.id + "&ITEM_ID=" + CURRENT_ITEM_ID.ToString()), _callback).Send();
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0006A801 File Offset: 0x00068A01
	public static void SET_SKIN(int SKIN_ID, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(6, "&ACC_ID=" + PlayerProfile.id + "&SET=" + SKIN_ID.ToString()), _callback).Send();
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0006A830 File Offset: 0x00068A30
	public static void STEAM_CHECK_TICKET(OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(51, ""), _callback).Send();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0006A84C File Offset: 0x00068A4C
	public static void STEAM_BUY_ITEM(ulong ITEM_ID, int REQ, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(52, string.Concat(new string[]
		{
			"&LANG=",
			SteamHandler.SteamLang,
			"&ITEM_ID=",
			ITEM_ID.ToString(),
			"&REQ=",
			REQ.ToString()
		})), _callback).Send();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0006A8AC File Offset: 0x00068AAC
	public static void GET_STATISTIC_TOP(int page, string search, int mode, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(250, string.Concat(new object[]
		{
			"&acc_id=",
			PlayerProfile.id,
			"&page=",
			page,
			"&search=",
			search,
			"&mode=",
			mode
		})), _callback).Send();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0006A918 File Offset: 0x00068B18
	public static void GET_ANGAR_GAMESCORE(int gameId, int page, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(200, string.Concat(new object[]
		{
			"&acc_id=",
			PlayerProfile.id,
			"&game_id=",
			gameId,
			"&page=",
			page
		})), _callback).Send();
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0006A978 File Offset: 0x00068B78
	public static void SET_ANGAR_GAMESCORE(int gameId, int score, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(201, string.Concat(new object[]
		{
			"&acc_id=",
			PlayerProfile.id,
			"&game_id=",
			gameId,
			"&score=",
			score
		})), _callback).Send();
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0006A9D8 File Offset: 0x00068BD8
	public static void GET_ANGAR_GAMELIVES(int gameId, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(202, string.Concat(new object[]
		{
			"&acc_id=",
			PlayerProfile.id,
			"&game_id=",
			gameId
		})), _callback).Send();
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0006AA28 File Offset: 0x00068C28
	public static void SET_ANGAR_GAMELIVE(int gameId, int live, OnRequestFinishedDelegate _callback)
	{
		new HTTPRequest(WEB_HANDLER.URL_BUILDER(203, string.Concat(new object[]
		{
			"&acc_id=",
			PlayerProfile.id,
			"&game_id=",
			gameId,
			"&live=",
			live
		})), _callback).Send();
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0006AA88 File Offset: 0x00068C88
	public static int CHECK_ERROR(string errMsg)
	{
		if (errMsg.Contains("BANNED"))
		{
			GameController.STATE = GAME_STATES.NULL;
		}
		return 1;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0006AAA0 File Offset: 0x00068CA0
	private static Uri URL_BUILDER(int cmd, string paramsLine = "")
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			cmd,
			WEB_HANDLER.WRITE_STANDART(),
			paramsLine
		});
		text = text + "&SID=" + CONST.MD5(text + "a891a7d64f3f57354b6d93a89aac29ae");
		return new Uri(text);
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0006AAF8 File Offset: 0x00068CF8
	private static string WRITE_STANDART()
	{
		return string.Concat(new object[]
		{
			"&SOC_ID=",
			PlayerProfile.id,
			"&AUTH_KEY=",
			PlayerProfile.authkey,
			"&CURRENT_TIME=",
			Time.time
		});
	}
}
