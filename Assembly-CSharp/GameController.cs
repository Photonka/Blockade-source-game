using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using Facebook.Unity;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class GameController : MonoBehaviour
{
	// Token: 0x060001A0 RID: 416 RVA: 0x0002432C File Offset: 0x0002252C
	private void Awake()
	{
		if (GameController.THIS == null)
		{
			GameController.THIS = this;
		}
		if (GameController.STATE == GAME_STATES.GAME)
		{
			GM.currMainState = GAME_STATES.CONNECTING;
			return;
		}
		if (GameController.STATE == GAME_STATES.MAINMENU)
		{
			GM.currExtState = GAME_STATES.NULL;
			GM.currGUIState = GUIGS.SERVERLIST;
			this.BroadcastAll("myGlobalInit", "");
			ServerList.THIS.refresh_servers();
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00024390 File Offset: 0x00022590
	private void Init()
	{
		this.cb_refresh();
		GUIManager.Init(false);
		GUI3.Init();
		ItemsDB.LoadMissingIcons();
		this.PreAuth();
		this.BroadcastAll("myGlobalInit", "");
		ServerList.THIS.refresh_servers();
		GameController.STATE = GAME_STATES.MAINMENU;
		GM.currGUIState = GUIGS.SERVERLIST;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000243E0 File Offset: 0x000225E0
	private void Update()
	{
		if (Input.GetKeyDown(290))
		{
			Config.ChangeMode();
		}
		if (GameController.STATE == GAME_STATES.BREAK || GameController.STATE == GAME_STATES.WAITING_FOR_GAME_DATA || GameController.STATE == GAME_STATES.LOADINGPROFILE || GameController.STATE == GAME_STATES.LOADING_BUNDLES || GameController.STATE == GAME_STATES.STEAMINITERROR || GameController.STATE == GAME_STATES.WAITING_FOR_AUTH_KEY || GameController.STATE == GAME_STATES.WAITING_FOR_AUTH || GameController.STATE == GAME_STATES.LOADING_BUNDLES)
		{
			return;
		}
		if (GameController.STATE == GAME_STATES.NULL)
		{
			Config.Init();
			Lang.Init();
			if (!SteamHandler.Init())
			{
				GameController.STATE = GAME_STATES.STEAMINITERROR;
				return;
			}
			WEB_HANDLER.START_GAME(new OnRequestFinishedDelegate(this.OnRecvGameData));
			GameController.STATE = GAME_STATES.WAITING_FOR_GAME_DATA;
			return;
		}
		else if (GameController.STATE == GAME_STATES.STARTING)
		{
			if (!SteamHandler.CheckInit())
			{
				GameController.STATE = GAME_STATES.STEAMINITERROR;
				return;
			}
			SteamHandler.GetUser();
			GameController.STATE = GAME_STATES.GETPLAYERID;
			return;
		}
		else if (GameController.STATE == GAME_STATES.WAITING_FOR_AUTH_TICKET)
		{
			if (!SteamHandler.CheckInit())
			{
				GameController.STATE = GAME_STATES.STEAMINITERROR;
				return;
			}
			if (string.IsNullOrEmpty(PlayerProfile.authkey))
			{
				SteamHandler.GetAuthTicket();
				return;
			}
			WEB_HANDLER.STEAM_CHECK_TICKET(new OnRequestFinishedDelegate(this.OnRecvSteamAuthKey));
			GameController.STATE = GAME_STATES.WAITING_FOR_AUTH_KEY;
			return;
		}
		else if (GameController.STATE == GAME_STATES.GETPLAYERID)
		{
			if (string.IsNullOrEmpty(PlayerProfile.id))
			{
				return;
			}
			if (string.IsNullOrEmpty(PlayerProfile.authkey))
			{
				SteamHandler.GetAuthTicket();
				GameController.STATE = GAME_STATES.WAITING_FOR_AUTH_TICKET;
				return;
			}
			Debug.Log("My Steam ID: " + PlayerProfile.id);
			Debug.Log("My AuthTicket: " + PlayerProfile.authkey);
			Debug.Log("My Steam Lang: " + SteamHandler.SteamLang);
			GameController.STATE = GAME_STATES.RECV_SOCIAL_DATA;
			return;
		}
		else
		{
			if (GameController.STATE == GAME_STATES.RECV_SOCIAL_DATA)
			{
				GameController.STATE = GAME_STATES.AUTH;
			}
			if (GameController.STATE == GAME_STATES.AUTH)
			{
				base.StartCoroutine(Handler.GetAUTH());
				GameController.STATE = GAME_STATES.WAITING_FOR_AUTH;
			}
			if (GameController.STATE == GAME_STATES.GETPROFILE)
			{
				base.StartCoroutine(Handler.GetProfile());
				GameController.STATE = GAME_STATES.LOADINGPROFILE;
			}
			if (GameController.STATE == GAME_STATES.INIT)
			{
				this.Init();
				return;
			}
			if (GameController.STATE == GAME_STATES.GAME)
			{
				if (Client.THIS == null)
				{
					return;
				}
				if (ZipLoader.THIS == null)
				{
					return;
				}
				if (LoadScreen.THIS == null)
				{
					return;
				}
				if (GM.currMainState == GAME_STATES.CONNECTING)
				{
					if (GM.currExtState == GAME_STATES.NULL)
					{
						LoadScreen.THIS.progress = 0;
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMEAUTH)
					{
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMEAUTHCOMPLITE)
					{
						GM.currExtState = GAME_STATES.GAMELOADINGMAP;
						LoadScreen.THIS.progress = 1;
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADINGMAP)
					{
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADMAPCOMPLITE)
					{
						LoadScreen.THIS.progress = 2;
						GM.currExtState = GAME_STATES.GAMELOADINGMAPCHANGES;
						Client.THIS.send_blockinfo();
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADINGMAPCHANGES)
					{
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADMAPCHANGESCOMPLITE)
					{
						LoadScreen.THIS.progress = 3;
						GM.currExtState = GAME_STATES.GAMEVISUALIZINGMAP;
						ZipLoader.THIS.WebLoadMapFinish();
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMEVISUALIZINGMAP)
					{
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMEVISUALIZINGMAPCOMPLITE)
					{
						LoadScreen.THIS.progress = 4;
						Client.THIS.send_myinfo();
						GM.currExtState = GAME_STATES.GAMELOADINGMYPROFILE;
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADINGMYPROFILE)
					{
						return;
					}
					if (GM.currExtState == GAME_STATES.GAMELOADMYPROFILECOMPLITE)
					{
						LoadScreen.THIS.progress = 5;
						base.gameObject.GetComponent<SpawnManager>().PreSpawn();
						return;
					}
				}
			}
			return;
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000246E4 File Offset: 0x000228E4
	private void BroadcastAll(string fun, object msg)
	{
		foreach (GameObject gameObject in (GameObject[])Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (gameObject && gameObject.transform.parent == null)
			{
				gameObject.gameObject.BroadcastMessage(fun, msg, 1);
			}
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00024741 File Offset: 0x00022941
	private void myGlobalInit()
	{
		SoundManager.Init();
		SkinManager.Init();
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00024750 File Offset: 0x00022950
	private void ShowPopup(int index, string _cost = "")
	{
		int param = 0;
		if (!string.IsNullOrEmpty(_cost))
		{
			int.TryParse(_cost, out param);
		}
		PopUp.ShowBonus(index, param);
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00024777 File Offset: 0x00022977
	public void UpdatePlayerInfo()
	{
		base.StartCoroutine(Handler.ShareScreenshot());
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00002B75 File Offset: 0x00000D75
	private void PreAuth()
	{
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00024785 File Offset: 0x00022985
	private void OnInitComplete()
	{
		FB.ActivateApp();
		if (!FB.IsLoggedIn)
		{
			this.CallFBLogin();
			return;
		}
		this.OnLoggedIn();
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000247A0 File Offset: 0x000229A0
	private void CallFBLogin()
	{
		FB.LogInWithReadPermissions(new List<string>
		{
			"user_friends",
			"email",
			"public_profile"
		}, new FacebookDelegate<ILoginResult>(this.FBLoginCallback));
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000247D9 File Offset: 0x000229D9
	private void CallFBLoginForPublish()
	{
		FB.LogInWithPublishPermissions(new List<string>
		{
			"publish_actions"
		}, new FacebookDelegate<ILoginResult>(this.LogInWithPublishPermissionsCallback));
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000247FC File Offset: 0x000229FC
	private void FBLoginCallback(IResult result)
	{
		if (FB.IsLoggedIn)
		{
			this.CallFBLoginForPublish();
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0002480B File Offset: 0x00022A0B
	private void LogInWithPublishPermissionsCallback(IResult result)
	{
		if (FB.IsLoggedIn)
		{
			this.OnLoggedIn();
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0002481C File Offset: 0x00022A1C
	private void OnLoggedIn()
	{
		if (PlayerPrefs.HasKey("FB_" + AccessToken.CurrentAccessToken.UserId.ToString() + "_KEY"))
		{
			jscall.cb_set_key(PlayerPrefs.GetString("FB_" + AccessToken.CurrentAccessToken.UserId.ToString() + "_KEY"));
		}
		this.POST(AccessToken.CurrentAccessToken.UserId.ToString());
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0002488C File Offset: 0x00022A8C
	public void POST(string ID)
	{
		WWWForm wwwform = new WWWForm();
		string text = CONST.HANDLER_SERVER + "handler.php";
		wwwform.AddField("cmd", 0);
		wwwform.AddField("id", ID);
		wwwform.AddField("authkey", PlayerProfile.authkey);
		WWW www = new WWW(text, wwwform);
		base.StartCoroutine(Handler.WaitForRequest(www));
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000248EA File Offset: 0x00022AEA
	public void jsc_auth_id(string _id)
	{
		jscall.cb_get_auth_id(_id);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000248F2 File Offset: 0x00022AF2
	public void jsc_auth_key(string _key)
	{
		jscall.cb_get_auth_key(_key);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000248FA File Offset: 0x00022AFA
	public void jsc_upserver_url(string _url)
	{
		PlayerProfile.screenShotURL = _url;
		base.StartCoroutine(Handler.UploadPNG());
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0002490E File Offset: 0x00022B0E
	public void jsc_auth_country(string _country)
	{
		jscall.cb_get_auth_country(_country);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00024916 File Offset: 0x00022B16
	public void jsc_network(int _network)
	{
		jscall.cb_get_network(_network);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0002491E File Offset: 0x00022B1E
	public void jsc_server_update_timeout(int _timeout)
	{
		CONST.CFG.SERVER_UPDATE_TIMEOUT = _timeout;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00024926 File Offset: 0x00022B26
	public void jsc_friends(string _friends)
	{
		PlayerProfile.friends = _friends;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00024930 File Offset: 0x00022B30
	public void jsc_friends_online(string _friendsOnline)
	{
		string[] array = _friendsOnline.Split(new char[]
		{
			','
		});
		if (array.Length != 0)
		{
			foreach (string text in array)
			{
				if (text != "")
				{
					PlayerProfile.friendsOnline.Add(text.Split(new char[]
					{
						'|'
					})[0], text.Split(new char[]
					{
						'|'
					})[1]);
				}
			}
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000249A4 File Offset: 0x00022BA4
	public void cb_refresh()
	{
		this.playerRefresh();
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000249AC File Offset: 0x00022BAC
	public void cb_load_profile()
	{
		if (PlayerProfile.id == "0" || PlayerProfile.id.Length <= 3)
		{
			GameController.STATE = GAME_STATES.STARTING;
			return;
		}
		GameController.STATE = GAME_STATES.AUTH;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000249DA File Offset: 0x00022BDA
	public void playerRefresh()
	{
		if (PlayerProfile.id == "0")
		{
			return;
		}
		base.StartCoroutine(Handler.get_stats_player());
		base.StartCoroutine(this.get_bonus_day());
		base.StartCoroutine(Handler.get_steam_user_info());
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00024A13 File Offset: 0x00022C13
	private IEnumerator get_bonus_day()
	{
		if (PlayerProfile.get_bonus_day)
		{
			yield break;
		}
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"10&id=",
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
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			if (array.Length != 3)
			{
				yield break;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (!int.TryParse(array[0], out num))
			{
				yield break;
			}
			if (!int.TryParse(array[1], out num2))
			{
				yield break;
			}
			if (!int.TryParse(array[2], out num3))
			{
				yield break;
			}
			if (num == 2)
			{
				PopUp.ShowBonus(1, 0);
				PlayerProfile.money += 20;
			}
			else if (num == 1)
			{
				PopUp.ShowBonus(2, 0);
				PlayerProfile.money++;
			}
			if (num2 != 1)
			{
				NETWORK network = PlayerProfile.network;
			}
			if (num3 > 0)
			{
				PopUp.ShowBonus(3, num3);
				if (num3 == 3)
				{
					PlayerProfile.money++;
				}
				else if (num3 == 6)
				{
					PlayerProfile.money += 2;
				}
				else if (num3 == 7)
				{
					PlayerProfile.money += 10;
				}
			}
			PlayerProfile.get_bonus_day = true;
		}
		yield break;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00024A1C File Offset: 0x00022C1C
	private void OnRecvGameData(HTTPRequest req, HTTPResponse resp)
	{
		switch (req.State)
		{
		case HTTPRequestStates.Finished:
			if (!resp.IsSuccess)
			{
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				base.gameObject.SetActive(false);
				return;
			}
			if (resp.DataAsText.Contains("ERR"))
			{
				WEB_HANDLER.CHECK_ERROR(resp.DataAsText);
				return;
			}
			base.StartCoroutine(this.SoftStart(resp));
			return;
		case HTTPRequestStates.Error:
			Debug.LogError("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.Aborted:
			Debug.LogWarning("Request Aborted!");
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.ConnectionTimedOut:
			Debug.LogError("Connection Timed Out!");
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.TimedOut:
			Debug.LogError("Processing the request Timed Out!");
			base.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00024B45 File Offset: 0x00022D45
	private IEnumerator SoftStart(HTTPResponse response)
	{
		UTILS.BEGIN_READ(response.Data, 0);
		int items_count = UTILS.READ_LONG();
		ItemsDB.Items = new ItemData[1500];
		ItemData itemData = new ItemData(1000, 0, 0, 0, 2, 0, 0, 0, 0);
		for (int j = 0; j < 6; j++)
		{
			itemData.Upgrades[j][0] = new WeaponUpgrade(0, 0);
		}
		ItemsDB.Items[1000] = itemData;
		ItemsDB.Items[1001] = itemData;
		int num5;
		for (int i = 0; i < items_count; i = num5 + 1)
		{
			int num = UTILS.READ_LONG();
			if (num > ItemsDB.Items.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					i,
					" ITEM ",
					num,
					" OVERFLOW!!!"
				}));
			}
			else
			{
				if (ItemsDB.CheckItem(num))
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						i,
						" ITEM ",
						num,
						"ALREADY ADDED!!!"
					}));
				}
				itemData = new ItemData(num, (int)UTILS.READ_BYTE(), (int)UTILS.READ_BYTE(), (int)UTILS.READ_BYTE(), (int)UTILS.READ_BYTE(), (int)UTILS.READ_BYTE(), UTILS.READ_LONG(), UTILS.READ_LONG(), UTILS.READ_LONG());
				if (itemData.Type < 3)
				{
					for (int k = 0; k < 6; k++)
					{
						itemData.Upgrades[k][0] = new WeaponUpgrade(0, 0);
					}
					while (UTILS.READ_BYTE() != 255)
					{
						int num2 = (int)UTILS.READ_BYTE();
						int num3 = (int)UTILS.READ_BYTE();
						int num4 = UTILS.READ_LONG();
						int cost = UTILS.READ_LONG();
						itemData.Upgrades[num2][num3] = new WeaponUpgrade(num4, cost);
						if (itemData.Type == 1)
						{
							if (ItemsDB.Items[1000].Upgrades[num2][0].Val < num4)
							{
								ItemsDB.Items[1000].Upgrades[num2][0].Val = num4;
							}
						}
						else if (ItemsDB.Items[1001].Upgrades[num2][0].Val < num4)
						{
							ItemsDB.Items[1001].Upgrades[num2][0].Val = num4;
						}
					}
				}
				ItemsDB.Items[num] = itemData;
				yield return null;
			}
			num5 = i;
		}
		yield return null;
		GameController.STATE = GAME_STATES.STARTING;
		yield break;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00024B54 File Offset: 0x00022D54
	private void OnApplicationQuit()
	{
		SteamHandler.CloseConnection();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00024B5C File Offset: 0x00022D5C
	private void OnRecvSteamAuthKey(HTTPRequest req, HTTPResponse resp)
	{
		switch (req.State)
		{
		case HTTPRequestStates.Finished:
		{
			if (!resp.IsSuccess)
			{
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				base.gameObject.SetActive(false);
				return;
			}
			if (resp.DataAsText.Contains("ERR"))
			{
				WEB_HANDLER.CHECK_ERROR(resp.DataAsText);
				return;
			}
			UTILS.BEGIN_READ(resp.Data, 0);
			string text = UTILS.READ_STRING();
			if (string.IsNullOrEmpty(text))
			{
				GameController.STATE = GAME_STATES.STARTING;
				return;
			}
			PlayerPrefs.SetString(CONST.MD5("STEAM_USER_" + PlayerProfile.id.ToString() + "_AUTH_KEY"), text);
			PlayerPrefs.Save();
			PlayerProfile.authkey = text;
			GameController.STATE = GAME_STATES.GETPLAYERID;
			return;
		}
		case HTTPRequestStates.Error:
			Debug.LogError("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.Aborted:
			Debug.LogWarning("Request Aborted!");
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.ConnectionTimedOut:
			Debug.LogError("Connection Timed Out!");
			base.gameObject.SetActive(false);
			return;
		case HTTPRequestStates.TimedOut:
			Debug.LogError("Processing the request Timed Out!");
			base.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x04000175 RID: 373
	public static GAME_STATES STATE = GAME_STATES.NULL;

	// Token: 0x04000176 RID: 374
	public static GameController THIS;

	// Token: 0x04000177 RID: 375
	private float lastPone;
}
