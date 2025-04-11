using System;

// Token: 0x02000090 RID: 144
public class jscall
{
	// Token: 0x0600043E RID: 1086 RVA: 0x00056BD4 File Offset: 0x00054DD4
	public static void cb_get_auth_id(string _id)
	{
		PlayerProfile.id = _id;
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00056BDC File Offset: 0x00054DDC
	public static void cb_get_network(int _network)
	{
		PlayerProfile.network = (NETWORK)_network;
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00056BE4 File Offset: 0x00054DE4
	public static void cb_get_auth_key(string _key)
	{
		PlayerProfile.authkey = _key;
		if (PlayerProfile.authkey.Length > 32 && PlayerProfile.network != NETWORK.FB && PlayerProfile.network != NETWORK.KG)
		{
			string[] array = PlayerProfile.authkey.Split(new char[]
			{
				'|'
			});
			PlayerProfile.authkey = array[0];
			PlayerProfile.session = array[1];
			PlayerProfile.network = NETWORK.OK;
		}
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00056C3F File Offset: 0x00054E3F
	public static void cb_set_key(string _key)
	{
		PlayerProfile.authkey = _key;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00056C47 File Offset: 0x00054E47
	public static void cb_get_auth_country(string _country)
	{
		int.TryParse(_country, out PlayerProfile.country);
		if (PlayerProfile.country > 255 || PlayerProfile.country < 0)
		{
			PlayerProfile.country = 0;
		}
	}
}
