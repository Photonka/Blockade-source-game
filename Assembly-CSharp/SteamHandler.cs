using System;
using System.Text;
using BestHTTP;
using Facepunch.Steamworks;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class SteamHandler : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x0002C968 File Offset: 0x0002AB68
	private void Start()
	{
		if (CONST.STEAM_HANDLER != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		CONST.STEAM_HANDLER = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0002C994 File Offset: 0x0002AB94
	public static bool Init()
	{
		Config.ForUnity(Application.platform.ToString());
		new Client(1049800U);
		if (Client.Instance == null)
		{
			Debug.LogError("Error starting Steam!");
			return false;
		}
		Client.Instance.MicroTransactions.OnAuthorizationResponse += delegate(bool authorized, int appId, ulong orderId)
		{
			Debug.Log("ORDER ID: " + orderId.ToString() + " STATUS: " + authorized.ToString());
			WEB_HANDLER.STEAM_BUY_ITEM(orderId, 2, new OnRequestFinishedDelegate(Gold.OnSteamBuyItemFinish));
		};
		return true;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0002CA06 File Offset: 0x0002AC06
	public static bool CheckInit()
	{
		return Client.Instance != null;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0002CA14 File Offset: 0x0002AC14
	public static void GetUser()
	{
		if (Client.Instance == null)
		{
			return;
		}
		PlayerProfile.id = Client.Instance.SteamId.ToString();
		PlayerProfile.authkey = PlayerPrefs.GetString(CONST.MD5("STEAM_USER_" + PlayerProfile.id.ToString() + "_AUTH_KEY"), string.Empty);
		int num = 0;
		if (PlayerPrefs.HasKey(CONST.MD5("DefLanguage")))
		{
			num = PlayerPrefs.GetInt(CONST.MD5("DefLanguage"));
			if (num == 0)
			{
				SteamHandler.SteamLang = "ru";
			}
			else
			{
				SteamHandler.SteamLang = "en";
				num = 1;
			}
		}
		else
		{
			SteamHandler.SteamLang = Client.Instance.CurrentLanguage;
			if (SteamHandler.SteamLang.Contains("rus"))
			{
				SteamHandler.SteamLang = "ru";
			}
			else
			{
				SteamHandler.SteamLang = "en";
				num = 1;
			}
			PlayerPrefs.SetInt(CONST.MD5("DefLanguage"), num);
			PlayerPrefs.Save();
		}
		Lang.current = num;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0002CB00 File Offset: 0x0002AD00
	public static void GetAuthTicket()
	{
		if (Client.Instance == null)
		{
			return;
		}
		PlayerProfile.authkey = SteamHandler.GetSteamAuthTicket();
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0002CB14 File Offset: 0x0002AD14
	public static string GetSteamAuthTicket()
	{
		if (Client.Instance == null)
		{
			return string.Empty;
		}
		SteamHandler._appTicket = Client.Instance.Auth.GetAuthSessionTicket();
		if (SteamHandler._appTicket == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (byte b in SteamHandler._appTicket.Data)
		{
			stringBuilder.AppendFormat("{0:x2}", b);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0002CB8A File Offset: 0x0002AD8A
	public static void GetUserAvatar(ulong _steamID)
	{
		if (Client.Instance == null)
		{
			return;
		}
		Client.Instance.Friends.GetAvatar(1, _steamID, new Action<Image>(SteamHandler.OnImage));
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
	private static void OnImage(Image tmp)
	{
		Texture2D texture2D = new Texture2D(tmp.Width, tmp.Height, 5, false);
		int i = 0;
		int width = tmp.Width;
		while (i < width)
		{
			int j = 0;
			int height = tmp.Height;
			while (j < height)
			{
				Color pixel = tmp.GetPixel(i, j);
				texture2D.SetPixel(i, tmp.Height - j, new Color((float)pixel.r / 255f, (float)pixel.g / 255f, (float)pixel.b / 255f, (float)pixel.a / 255f));
				j++;
			}
			i++;
		}
		texture2D.Apply(false);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0002CC5A File Offset: 0x0002AE5A
	private void OnDestroy()
	{
		if (Client.Instance != null)
		{
			Client.Instance.Dispose();
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0002CC5A File Offset: 0x0002AE5A
	public static void CloseConnection()
	{
		if (Client.Instance != null)
		{
			Client.Instance.Dispose();
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0002CC6D File Offset: 0x0002AE6D
	private void Update()
	{
		if (Client.Instance != null)
		{
			Client.Instance.Update();
		}
	}

	// Token: 0x040002CA RID: 714
	private static Auth.Ticket _appTicket;

	// Token: 0x040002CB RID: 715
	public static string SteamLang;
}
