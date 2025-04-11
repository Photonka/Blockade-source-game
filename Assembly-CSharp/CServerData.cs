using System;

// Token: 0x0200001F RID: 31
public class CServerData
{
	// Token: 0x060000E5 RID: 229 RVA: 0x0000FB20 File Offset: 0x0000DD20
	public CServerData(int _type, int _gamemode, int _players, int _maxplayers, string _map, ulong _adminid, string _ip, int _port, int _password, int _country_id)
	{
		this.type = _type;
		this.gamemode = _gamemode;
		this.players = _players;
		this.maxplayers = _maxplayers;
		this.adminid = _adminid;
		this.ip = _ip;
		this.port = _port;
		this.password = _password;
		this.hover = false;
		this.country_id = _country_id;
		if (this.type > 0)
		{
			this.name = _map;
			return;
		}
		int.TryParse(_map, out this.map_id);
		this.SetMapNameAndSize();
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
	~CServerData()
	{
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
	private void SetMapNameAndSize()
	{
		if (Lang.current == 1)
		{
			if (this.map_id == 0)
			{
				this.name = "PLANE";
			}
			else if (this.map_id == 1)
			{
				this.name = "CITADEL";
			}
			else if (this.map_id == 2)
			{
				this.name = "CASTLE";
			}
			else if (this.map_id == 3)
			{
				this.name = "FORTPOST 2";
			}
			else if (this.map_id == 4)
			{
				this.name = "HILLS";
			}
			else if (this.map_id == 5)
			{
				this.name = "VILLAGE 2";
			}
			else if (this.map_id == 6)
			{
				this.name = "ISLAND";
			}
			else if (this.map_id == 7)
			{
				this.name = "CATHEDRAL 2";
			}
			else if (this.map_id == 8)
			{
				this.name = "TOWN";
			}
			else if (this.map_id == 9)
			{
				this.name = "TECHNO";
			}
			else if (this.map_id == 10)
			{
				this.name = "EMIDRIS";
			}
			else if (this.map_id == 11)
			{
				this.name = "SLIME";
			}
			else if (this.map_id == 12)
			{
				this.name = "CAPITAL";
			}
			else if (this.map_id == 13)
			{
				this.name = "BREST FORTRESS";
			}
			else if (this.map_id == 14)
			{
				this.name = "SWIRL";
			}
			else if (this.map_id == 15)
			{
				this.name = "PRIPYAT";
			}
			else if (this.map_id == 16)
			{
				this.name = "PRIPYAT 2";
			}
			else if (this.map_id == 17)
			{
				this.name = "KLAZMOS";
			}
			else if (this.map_id == 18)
			{
				this.name = "EAST";
			}
			else if (this.map_id == 19)
			{
				this.name = "EPIC";
			}
			else if (this.map_id == 20)
			{
				this.name = "TOWERS";
			}
			else if (this.map_id == 21)
			{
				this.name = "PARK";
			}
			else if (this.map_id == 22)
			{
				this.name = "WINTER GARDEN";
			}
			else if (this.map_id == 23)
			{
				this.name = "HOSTEL";
			}
			else if (this.map_id == 24)
			{
				this.name = "TEMPLE";
			}
			else if (this.map_id == 25)
			{
				this.name = "HASHER";
			}
			else if (this.map_id == 26)
			{
				this.name = "HARBOR";
			}
			else if (this.map_id == 27)
			{
				this.name = "JUNGLE";
			}
			else if (this.map_id == 28)
			{
				this.name = "SECRET BASE";
			}
			else if (this.map_id == 29)
			{
				this.name = "FOREST LAKE";
			}
			else if (this.map_id == 30)
			{
				this.name = "TOWER";
			}
			else if (this.map_id == 31)
			{
				this.name = "JAPAN";
			}
			else if (this.map_id == 32)
			{
				this.name = "UNDERGROUND";
			}
			else if (this.map_id == 33)
			{
				this.name = "ASYLUM";
			}
			else if (this.map_id == 34)
			{
				this.name = "STAR";
			}
			else if (this.map_id == 35)
			{
				this.name = "TOURNAMENT BATTLE";
			}
			else if (this.map_id == 36)
			{
				this.name = "CITY-17";
			}
			else if (this.map_id == 37)
			{
				this.name = "SIEGE";
			}
			else if (this.map_id == 38)
			{
				this.name = "SIBERIA";
			}
			else if (this.map_id == 39)
			{
				this.name = "DEAD CITY";
			}
			else if (this.map_id == 40)
			{
				this.name = "WINTER CASTLE";
			}
			else if (this.map_id == 41)
			{
				this.name = "ISLANDS 2017";
			}
			else if (this.map_id == 42)
			{
				this.name = "VILLAGE";
			}
			else if (this.map_id == 43)
			{
				this.name = "CATHEDRAL";
			}
			else if (this.map_id == 44)
			{
				this.name = "WINTER CASTLE";
			}
			else if (this.map_id == 45)
			{
				this.name = "BUBBLES";
			}
			else if (this.map_id == 46)
			{
				this.name = "HAMLET";
			}
			else if (this.map_id == 47)
			{
				this.name = "FOREST CASTLE";
			}
			else if (this.map_id == 48)
			{
				this.name = "NEW CITY";
			}
			else if (this.map_id == 49)
			{
				this.name = "AZTEC";
			}
			else if (this.map_id == 50)
			{
				this.name = "FORTPOST";
			}
			else if (this.map_id == 51)
			{
				this.name = "WINTER CITADEL";
			}
			else if (this.map_id == 52)
			{
				this.name = "OLD CITADEL";
			}
			else if (this.map_id == 101)
			{
				this.name = "CASTLES";
			}
			else if (this.map_id == 102)
			{
				this.name = "GLACIER";
			}
			else if (this.map_id == 103)
			{
				this.name = "SCHOOL YARD";
			}
			else if (this.map_id == 104)
			{
				this.name = "RINK";
			}
			else if (this.map_id == 105)
			{
				this.name = "SHIPS";
			}
			else if (this.map_id == 106)
			{
				this.name = "WINTER ARENA";
			}
			else if (this.map_id == 107)
			{
				this.name = "SNOW WARS";
			}
			else if (this.map_id == 108)
			{
				this.name = "LAPLAND";
			}
			else if (this.map_id == 109)
			{
				this.name = "MINES";
			}
			else if (this.map_id == 110)
			{
				this.name = "AREA";
			}
			else if (this.map_id == 401)
			{
				this.name = "SQUARE";
			}
			else if (this.map_id == 402)
			{
				this.name = "SHADOW OF BLOCKADE";
			}
			else if (this.map_id == 403)
			{
				this.name = "WINTER FIGHT";
			}
			else if (this.map_id == 404)
			{
				this.name = "ARCHIPELAGO";
			}
			else if (this.map_id == 405)
			{
				this.name = "LOST";
			}
			else if (this.map_id == 406)
			{
				this.name = "VENICE";
			}
			else if (this.map_id == 407)
			{
				this.name = "COUNTRY HOUSE";
			}
			else if (this.map_id == 408)
			{
				this.name = "TOWER";
			}
			else if (this.map_id == 501)
			{
				this.name = "DUST 2";
			}
			else if (this.map_id == 502)
			{
				this.name = "INFERNO";
			}
			else if (this.map_id == 503)
			{
				this.name = "TRAIN";
			}
			else if (this.map_id == 504)
			{
				this.name = "AZTEC";
			}
			else if (this.map_id == 505)
			{
				this.name = "NUKE";
			}
			else if (this.map_id == 506)
			{
				this.name = "OFFICE";
			}
			else if (this.map_id == 507)
			{
				this.name = "CLANMILL";
			}
			else if (this.map_id == 508)
			{
				this.name = "DUST 1";
			}
			else if (this.map_id == 509)
			{
				this.name = "MANSION";
			}
			else if (this.map_id == 510)
			{
				this.name = "FLY 2";
			}
			else if (this.map_id == 511)
			{
				this.name = "TUSCAN";
			}
			else if (this.map_id == 512)
			{
				this.name = "DUST 2002";
			}
			else if (this.map_id == 513)
			{
				this.name = "MINIDUST";
			}
			else if (this.map_id == 514)
			{
				this.name = "POOL";
			}
			else if (this.map_id == 515)
			{
				this.name = "CROSSROADS";
			}
			else if (this.map_id == 516)
			{
				this.name = "RIVER";
			}
			else if (this.map_id == 517)
			{
				this.name = "DESERT BASE";
			}
			else if (this.map_id == 518)
			{
				this.name = "STATION";
			}
			else if (this.map_id == 519)
			{
				this.name = "DUST";
			}
			else if (this.map_id == 520)
			{
				this.name = "MONO";
			}
			else if (this.map_id == 521)
			{
				this.name = "ANTHILL";
			}
			else if (this.map_id == 522)
			{
				this.name = "ASYLUM 2";
			}
			else if (this.map_id == 523)
			{
				this.name = "LAB";
			}
			else if (this.map_id == 524)
			{
				this.name = "QUARTER";
			}
			else if (this.map_id == 525)
			{
				this.name = "INDIA";
			}
			else if (this.map_id == 526)
			{
				this.name = "MINIDUST 2";
			}
			else if (this.map_id == 527)
			{
				this.name = "CBBLE";
			}
			else if (this.map_id == 528)
			{
				this.name = "RED";
			}
			else if (this.map_id == 529)
			{
				this.name = "AZTERIAL";
			}
			else if (this.map_id == 530)
			{
				this.name = "FACTORY";
			}
			else if (this.map_id == 531)
			{
				this.name = "WAREHOUSE";
			}
			else if (this.map_id == 532)
			{
				this.name = "BUSINESS CENTER";
			}
			else if (this.map_id == 533)
			{
				this.name = "TOWERS";
			}
			else if (this.map_id == 534)
			{
				this.name = "CAMP";
			}
			else if (this.map_id == 535)
			{
				this.name = "ASSAULT";
			}
			else if (this.map_id == 536)
			{
				this.name = "TERMINAL";
			}
			else if (this.map_id == 537)
			{
				this.name = "CHEMLAB";
			}
			else if (this.map_id == 538)
			{
				this.name = "METRO";
			}
			else if (this.map_id == 539)
			{
				this.name = "TRENCH";
			}
			else if (this.map_id == 540)
			{
				this.name = "RUINS";
			}
			else if (this.map_id == 541)
			{
				this.name = "CANALIZATION";
			}
			else if (this.map_id == 542)
			{
				this.name = "LUXVILL";
			}
			else if (this.map_id == 543)
			{
				this.name = "RAILWAY STATION";
			}
			else if (this.map_id == 544)
			{
				this.name = "ITALY";
			}
			else if (this.map_id == 545)
			{
				this.name = "TOURNAMENT CONTRA";
			}
			else if (this.map_id == 546)
			{
				this.name = "SUBWAY1337";
			}
			else if (this.map_id == 547)
			{
				this.name = "HANGAR";
			}
			else if (this.map_id == 548)
			{
				this.name = "2013";
			}
			else if (this.map_id == 549)
			{
				this.name = "BERLIN";
			}
			else if (this.map_id == 550)
			{
				this.name = "DEBRIS";
			}
			else if (this.map_id == 551)
			{
				this.name = "FACE";
			}
			else if (this.map_id == 552)
			{
				this.name = "CACHE";
			}
			else if (this.map_id == 553)
			{
				this.name = "ARENA";
			}
			else if (this.map_id == 554)
			{
				this.name = "BACKSTREET";
			}
			else if (this.map_id == 555)
			{
				this.name = "RUINS 2017";
			}
			else if (this.map_id == 556)
			{
				this.name = "STORM";
			}
			else if (this.map_id == 557)
			{
				this.name = "ADRENALIN";
			}
			else if (this.map_id == 558)
			{
				this.name = "WINTER FACTORY";
			}
			else if (this.map_id == 559)
			{
				this.name = "WINTER ASSAULT";
			}
			else if (this.map_id == 560)
			{
				this.name = "WINTER MANSION";
			}
			else if (this.map_id == 561)
			{
				this.name = "WINTER DUST 1";
			}
			else if (this.map_id == 562)
			{
				this.name = "OLD OFFICE";
			}
			else if (this.map_id == 563)
			{
				this.name = "TUSCAN MINI";
			}
			else if (this.map_id == 564)
			{
				this.name = "ARENA MAYA";
			}
			else if (this.map_id == 565)
			{
				this.name = "NEW DUST";
			}
			else if (this.map_id == 566)
			{
				this.name = "CHAIN";
			}
			else if (this.map_id == 567)
			{
				this.name = "CANYON";
			}
			else if (this.map_id == 601)
			{
				this.name = "MINIDUST 2";
			}
			else if (this.map_id == 602)
			{
				this.name = "POOL";
			}
			else if (this.map_id == 603)
			{
				this.name = "CROSSROADS";
			}
			else if (this.map_id == 604)
			{
				this.name = "RIVER";
			}
			else if (this.map_id == 605)
			{
				this.name = "ARENA35";
			}
			else if (this.map_id == 606)
			{
				this.name = "ARENA50";
			}
			else if (this.map_id == 607)
			{
				this.name = "REPOSITORY";
			}
			else if (this.map_id == 608)
			{
				this.name = "INDIA";
			}
			else if (this.map_id == 609)
			{
				this.name = "MADNESS";
			}
			else if (this.map_id == 610)
			{
				this.name = "HUB";
			}
			else if (this.map_id == 611)
			{
				this.name = "RUINS";
			}
			else if (this.map_id == 612)
			{
				this.name = "MINIAZTEC";
			}
			else if (this.map_id == 613)
			{
				this.name = "MASK";
			}
			else if (this.map_id == 614)
			{
				this.name = "75TH";
			}
			else if (this.map_id == 615)
			{
				this.name = "STYLE";
			}
			else if (this.map_id == 616)
			{
				this.name = "FUROR";
			}
			else if (this.map_id == 617)
			{
				this.name = "ARENA MAYA";
			}
			else if (this.map_id == 618)
			{
				this.name = "SHIP";
			}
			else if (this.map_id == 619)
			{
				this.name = "COLD";
			}
			else if (this.map_id == 620)
			{
				this.name = "STRAIGHT";
			}
			else if (this.map_id == 621)
			{
				this.name = "RISK";
			}
			else if (this.map_id == 622)
			{
				this.name = "MINI AZTEC";
			}
			else if (this.map_id == 623)
			{
				this.name = "WILD WEST";
			}
			else if (this.map_id == 624)
			{
				this.name = "RAILWAY STATION";
			}
			else if (this.map_id == 625)
			{
				this.name = "LOOP";
			}
			else if (this.map_id == 626)
			{
				this.name = "MANSION";
			}
			else if (this.map_id == 627)
			{
				this.name = "HANGAR2";
			}
			else if (this.map_id == 628)
			{
				this.name = "COSMO";
			}
			else if (this.map_id == 629)
			{
				this.name = "MONOCHROME";
			}
			else if (this.map_id == 630)
			{
				this.name = "OPISTION";
			}
			else if (this.map_id == 901)
			{
				this.name = "BREAKTHROUGH";
			}
			else if (this.map_id == 701)
			{
				this.name = "BEACH";
			}
			else if (this.map_id == 702)
			{
				this.name = "CASTLE";
			}
			else if (this.map_id == 703)
			{
				this.name = "DEFENSE";
			}
			else if (this.map_id == 704)
			{
				this.name = "CITY UMBRELLA";
			}
			else if (this.map_id == 705)
			{
				this.name = "VILLAGE";
			}
			else if (this.map_id == 706)
			{
				this.name = "FORT";
			}
			else if (this.map_id == 707)
			{
				this.name = "ABANDONED CITY";
			}
			else if (this.map_id == 708)
			{
				this.name = "AT THE BALL";
			}
			else if (this.map_id == 709)
			{
				this.name = "DEFENCE";
			}
			else if (this.map_id == 710)
			{
				this.name = "UMBRELLA";
			}
			else if (this.map_id == 711)
			{
				this.name = "HIGHWAY";
			}
			else if (this.map_id == 712)
			{
				this.name = "SURVIVAL";
			}
			else if (this.map_id == 1101)
			{
				this.name = "WINTER TOWN";
			}
			else if (this.map_id == 1102)
			{
				this.name = "VALLEY";
			}
			else if (this.map_id == 1103)
			{
				this.name = "WINTER VALLEY";
			}
			else if (this.map_id == 1104)
			{
				this.name = "RIVERTOWN";
			}
			else if (this.map_id == 1105)
			{
				this.name = "SNOWTOWN";
			}
			else if (this.map_id == 1106)
			{
				this.name = "CONFRONTATION";
			}
			else if (this.map_id == 301)
			{
				this.name = "VILLAGE-Z 2";
			}
			else if (this.map_id == 302)
			{
				this.name = "HOUSE";
			}
			else if (this.map_id == 303)
			{
				this.name = "LABYRINTH";
			}
			else if (this.map_id == 304)
			{
				this.name = "MILL";
			}
			else if (this.map_id == 305)
			{
				this.name = "ROCKET";
			}
			else if (this.map_id == 306)
			{
				this.name = "KINGDOM";
			}
			else if (this.map_id == 307)
			{
				this.name = "HOSPITAL";
			}
			else if (this.map_id == 308)
			{
				this.name = "PEACEFUL ATOM";
			}
			else if (this.map_id == 309)
			{
				this.name = "CANALIZATION-Z";
			}
			else if (this.map_id == 310)
			{
				this.name = "TRAP";
			}
			else if (this.map_id == 311)
			{
				this.name = "OLD BUNKER";
			}
			else if (this.map_id == 312)
			{
				this.name = "OLD HOUSE";
			}
			else if (this.map_id == 313)
			{
				this.name = "OLD LABYRINTH";
			}
			else if (this.map_id == 314)
			{
				this.name = "OLD MILL";
			}
			else if (this.map_id == 315)
			{
				this.name = "OLD VILLAGE-Z";
			}
			else if (this.map_id == 316)
			{
				this.name = "OLD ROCKET";
			}
			else if (this.map_id == 317)
			{
				this.name = "BUNKER";
			}
			if (this.name == "")
			{
				this.name = "MAP#" + this.map_id;
			}
			if (this.gamemode == 7 || this.gamemode == 9 || this.gamemode == 10)
			{
				if (this.maxplayers >= 64)
				{
					this.maxplayers -= 64;
					this.name = "in game";
				}
				else
				{
					this.name = "set";
				}
			}
			int num = this.gamemode * 1000;
			if (num > 9000)
			{
				num /= 10;
			}
			if (this.gamemode == 0)
			{
				this.name = "BATTLE#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 1)
			{
				this.name = "SNOWBALLS#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 2)
			{
				this.name = "BUILDING#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 3)
			{
				this.name = "ZOMBIE#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 4)
			{
				this.name = "CAPTURE#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 5)
			{
				this.name = "VERSUS#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 6)
			{
				this.name = "CARNAGE#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 7)
			{
				this.name = "SURVIVAL#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 8)
			{
				this.name = "ALPHA#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 9)
			{
				this.name = "BREAK POINT#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 10)
			{
				this.name = "CLEANER#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
			else if (this.gamemode == 11)
			{
				this.name = "TANKS#" + (this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num).ToString() + " - " + this.name;
			}
		}
		else
		{
			if (this.map_id == 0)
			{
				this.name = "ПЛОСКОСТЬ";
			}
			else if (this.map_id == 1)
			{
				this.name = "ЦИТАДЕЛЬ";
			}
			else if (this.map_id == 2)
			{
				this.name = "ЗАМОК";
			}
			else if (this.map_id == 3)
			{
				this.name = "ФОРПОСТ 2";
			}
			else if (this.map_id == 4)
			{
				this.name = "ХОЛМЫ";
			}
			else if (this.map_id == 5)
			{
				this.name = "ДЕРЕВНЯ 2";
			}
			else if (this.map_id == 6)
			{
				this.name = "ОСТРОВА";
			}
			else if (this.map_id == 7)
			{
				this.name = "СОБОР 2";
			}
			else if (this.map_id == 8)
			{
				this.name = "ГОРОД";
			}
			else if (this.map_id == 9)
			{
				this.name = "ТЕХНО";
			}
			else if (this.map_id == 10)
			{
				this.name = "ЭМИДРИС";
			}
			else if (this.map_id == 11)
			{
				this.name = "СЛИЗНЯК";
			}
			else if (this.map_id == 12)
			{
				this.name = "СТОЛИЦА";
			}
			else if (this.map_id == 13)
			{
				this.name = "БРЕСТСКАЯ КРЕПОСТЬ";
			}
			else if (this.map_id == 14)
			{
				this.name = "ВОДОВОРОТ";
			}
			else if (this.map_id == 15)
			{
				this.name = "ПРИПЯТЬ";
			}
			else if (this.map_id == 16)
			{
				this.name = "ПРИПЯТЬ 2";
			}
			else if (this.map_id == 17)
			{
				this.name = "КЛАЗМОС";
			}
			else if (this.map_id == 18)
			{
				this.name = "ВОСТОК";
			}
			else if (this.map_id == 19)
			{
				this.name = "ЭПИК";
			}
			else if (this.map_id == 20)
			{
				this.name = "БАШНИ";
			}
			else if (this.map_id == 21)
			{
				this.name = "ПАРК";
			}
			else if (this.map_id == 22)
			{
				this.name = "ЗИМНИЙ САД";
			}
			else if (this.map_id == 23)
			{
				this.name = "ОБЩЕЖИТИЕ";
			}
			else if (this.map_id == 24)
			{
				this.name = "ХРАМ";
			}
			else if (this.map_id == 25)
			{
				this.name = "МЯСОРУБКА";
			}
			else if (this.map_id == 26)
			{
				this.name = "ПОРТ";
			}
			else if (this.map_id == 27)
			{
				this.name = "ДЖУНГЛИ";
			}
			else if (this.map_id == 28)
			{
				this.name = "СЕКРЕТНАЯ БАЗА";
			}
			else if (this.map_id == 29)
			{
				this.name = "ЛЕСНОЕ ОЗЕРО";
			}
			else if (this.map_id == 30)
			{
				this.name = "ВЫШКА";
			}
			else if (this.map_id == 31)
			{
				this.name = "ЯПОНИЯ";
			}
			else if (this.map_id == 32)
			{
				this.name = "ПОДЗЕМКА";
			}
			else if (this.map_id == 33)
			{
				this.name = "УБЕЖИЩЕ";
			}
			else if (this.map_id == 34)
			{
				this.name = "ЗВЕЗДА";
			}
			else if (this.map_id == 35)
			{
				this.name = "ТУРНИРНАЯ БИТВА";
			}
			else if (this.map_id == 36)
			{
				this.name = "СИТИ-17";
			}
			else if (this.map_id == 37)
			{
				this.name = "ОСАДА";
			}
			else if (this.map_id == 38)
			{
				this.name = "СИБИРЬ";
			}
			else if (this.map_id == 39)
			{
				this.name = "МЕРТВЫЙ ГОРОД";
			}
			else if (this.map_id == 40)
			{
				this.name = "ЗИМНИЙ ЗАМОК";
			}
			else if (this.map_id == 41)
			{
				this.name = "ОСТРОВА 2017";
			}
			else if (this.map_id == 101)
			{
				this.name = "ЗАМКИ";
			}
			else if (this.map_id == 102)
			{
				this.name = "ЛЕДНИК";
			}
			else if (this.map_id == 103)
			{
				this.name = "ШКОЛЬНЫЙ ДВОР";
			}
			else if (this.map_id == 104)
			{
				this.name = "КАТОК";
			}
			else if (this.map_id == 105)
			{
				this.name = "КОРАБЛИ";
			}
			else if (this.map_id == 106)
			{
				this.name = "ЗИМНЯЯ АРЕНА";
			}
			else if (this.map_id == 107)
			{
				this.name = "СНЕЖНЫЕ ВОЙНЫ";
			}
			else if (this.map_id == 108)
			{
				this.name = "ЛАПЛАНДИЯ";
			}
			else if (this.map_id == 109)
			{
				this.name = "ШАХТЫ";
			}
			else if (this.map_id == 110)
			{
				this.name = "ЗОНА";
			}
			else if (this.map_id == 42)
			{
				this.name = "ДЕРЕВНЯ";
			}
			else if (this.map_id == 43)
			{
				this.name = "СОБОР";
			}
			else if (this.map_id == 44)
			{
				this.name = "ЗИМНИЙ ЗАМОК";
			}
			else if (this.map_id == 45)
			{
				this.name = "ПУЗЫРИ";
			}
			else if (this.map_id == 46)
			{
				this.name = "СЕЛО";
			}
			else if (this.map_id == 47)
			{
				this.name = "ЛЕСНАЯ КРЕПОСТЬ";
			}
			else if (this.map_id == 48)
			{
				this.name = "НОВЫЙ ГОРОД";
			}
			else if (this.map_id == 49)
			{
				this.name = "АЦТЕК";
			}
			else if (this.map_id == 50)
			{
				this.name = "ФОРПОСТ";
			}
			else if (this.map_id == 51)
			{
				this.name = "ЗИМНЯЯ ЦИТАДЕЛЬ";
			}
			else if (this.map_id == 52)
			{
				this.name = "СТАРАЯ ЦИТАДЕЛЬ";
			}
			else if (this.map_id == 401)
			{
				this.name = "ПЛОЩАДЬ";
			}
			else if (this.map_id == 402)
			{
				this.name = "ТЕНЬ БЛОКАДЫ";
			}
			else if (this.map_id == 403)
			{
				this.name = "ЗИМНИЙ БОЙ";
			}
			else if (this.map_id == 404)
			{
				this.name = "АРХИПЕЛАГ";
			}
			else if (this.map_id == 405)
			{
				this.name = "ЗАТЕРЯННЫЙ";
			}
			else if (this.map_id == 406)
			{
				this.name = "ВЕНЕЦИЯ";
			}
			else if (this.map_id == 407)
			{
				this.name = "ДАЧА";
			}
			else if (this.map_id == 408)
			{
				this.name = "ВЫШКА";
			}
			else if (this.map_id == 501)
			{
				this.name = "ДАСТ2";
			}
			else if (this.map_id == 502)
			{
				this.name = "ИНФЕРНО";
			}
			else if (this.map_id == 503)
			{
				this.name = "ТРЕЙН";
			}
			else if (this.map_id == 504)
			{
				this.name = "АЦТЕК";
			}
			else if (this.map_id == 505)
			{
				this.name = "НЮК";
			}
			else if (this.map_id == 506)
			{
				this.name = "ОФИС";
			}
			else if (this.map_id == 507)
			{
				this.name = "КЛАНМИЛЛ";
			}
			else if (this.map_id == 508)
			{
				this.name = "ДАСТ1";
			}
			else if (this.map_id == 509)
			{
				this.name = "МЭНШЕН";
			}
			else if (this.map_id == 510)
			{
				this.name = "ФЛАЙ2";
			}
			else if (this.map_id == 511)
			{
				this.name = "ТУСКАН";
			}
			else if (this.map_id == 512)
			{
				this.name = "ДАСТ 2002";
			}
			else if (this.map_id == 513)
			{
				this.name = "МИНИДАСТ";
			}
			else if (this.map_id == 514)
			{
				this.name = "БАССЕЙН";
			}
			else if (this.map_id == 515)
			{
				this.name = "ПЕРЕКРЁСТОК";
			}
			else if (this.map_id == 516)
			{
				this.name = "РЕКА";
			}
			else if (this.map_id == 517)
			{
				this.name = "БАЗА В ПУСТЫНЕ";
			}
			else if (this.map_id == 518)
			{
				this.name = "СТАНЦИЯ";
			}
			else if (this.map_id == 519)
			{
				this.name = "ПЫЛЬ";
			}
			else if (this.map_id == 520)
			{
				this.name = "МОНО";
			}
			else if (this.map_id == 521)
			{
				this.name = "МУРАВЕЙНИК";
			}
			else if (this.map_id == 522)
			{
				this.name = "УБЕЖИЩЕ2";
			}
			else if (this.map_id == 523)
			{
				this.name = "ЛАБОРАТОРИЯ";
			}
			else if (this.map_id == 524)
			{
				this.name = "КВАРТАЛ";
			}
			else if (this.map_id == 525)
			{
				this.name = "ИНДИЯ";
			}
			else if (this.map_id == 526)
			{
				this.name = "МИНИДАСТ2";
			}
			else if (this.map_id == 527)
			{
				this.name = "КОБЛ";
			}
			else if (this.map_id == 528)
			{
				this.name = "РЭД";
			}
			else if (this.map_id == 529)
			{
				this.name = "АЦТЕРИАЛ";
			}
			else if (this.map_id == 530)
			{
				this.name = "ФАБРИКА";
			}
			else if (this.map_id == 531)
			{
				this.name = "СКЛАДЫ";
			}
			else if (this.map_id == 532)
			{
				this.name = "ДЕЛОВОЙ ЦЕНТР";
			}
			else if (this.map_id == 533)
			{
				this.name = "БАШНИ";
			}
			else if (this.map_id == 534)
			{
				this.name = "ЛАГЕРЬ";
			}
			else if (this.map_id == 535)
			{
				this.name = "АССАУЛТ";
			}
			else if (this.map_id == 536)
			{
				this.name = "ТЕРМИНАЛ";
			}
			else if (this.map_id == 537)
			{
				this.name = "ХИМЛАБ";
			}
			else if (this.map_id == 538)
			{
				this.name = "МЕТРО";
			}
			else if (this.map_id == 539)
			{
				this.name = "ОКОПЫ";
			}
			else if (this.map_id == 540)
			{
				this.name = "РУИНЫ";
			}
			else if (this.map_id == 541)
			{
				this.name = "КАНАЛИЗАЦИЯ";
			}
			else if (this.map_id == 542)
			{
				this.name = "ЛЮКСВИЛЬ";
			}
			else if (this.map_id == 543)
			{
				this.name = "ВОКЗАЛ";
			}
			else if (this.map_id == 544)
			{
				this.name = "ИТАЛИЯ";
			}
			else if (this.map_id == 545)
			{
				this.name = "ТУРНИРНАЯ КОНТРА";
			}
			else if (this.map_id == 546)
			{
				this.name = "ПОДЗЕМКА1337";
			}
			else if (this.map_id == 547)
			{
				this.name = "АНГАР";
			}
			else if (this.map_id == 548)
			{
				this.name = "2013";
			}
			else if (this.map_id == 549)
			{
				this.name = "БЕРЛИН";
			}
			else if (this.map_id == 550)
			{
				this.name = "РАЗВАЛИНЫ";
			}
			else if (this.map_id == 551)
			{
				this.name = "ГРАНЬ";
			}
			else if (this.map_id == 552)
			{
				this.name = "КЭШ";
			}
			else if (this.map_id == 553)
			{
				this.name = "АРЕНА";
			}
			else if (this.map_id == 554)
			{
				this.name = "ЗАКОУЛОК";
			}
			else if (this.map_id == 555)
			{
				this.name = "РУИНЫ 2017";
			}
			else if (this.map_id == 556)
			{
				this.name = "ШТОРМ";
			}
			else if (this.map_id == 557)
			{
				this.name = "АДРЕНАЛИН";
			}
			else if (this.map_id == 558)
			{
				this.name = "ЗИМНЯЯ ФАБРИКА";
			}
			else if (this.map_id == 559)
			{
				this.name = "ЗИМНИЙ АССАУЛТ";
			}
			else if (this.map_id == 560)
			{
				this.name = "ЗИМНИЙ МЕНШЕН";
			}
			else if (this.map_id == 561)
			{
				this.name = "ЗИМНИЙ ДАСТ1";
			}
			else if (this.map_id == 562)
			{
				this.name = "СТАРЫЙ ОФИС";
			}
			else if (this.map_id == 563)
			{
				this.name = "ТУСКАН МИНИ";
			}
			else if (this.map_id == 564)
			{
				this.name = "АРЕНА МАЙЯ";
			}
			else if (this.map_id == 565)
			{
				this.name = "НОВЫЙ ДАСТ";
			}
			else if (this.map_id == 566)
			{
				this.name = "ЦЕПЬ";
			}
			else if (this.map_id == 567)
			{
				this.name = "КАНЬЕН";
			}
			else if (this.map_id == 601)
			{
				this.name = "МИНИДАСТ";
			}
			else if (this.map_id == 602)
			{
				this.name = "БАССЕЙН";
			}
			else if (this.map_id == 603)
			{
				this.name = "ПЕРЕКРЁСТОК";
			}
			else if (this.map_id == 604)
			{
				this.name = "РЕКА";
			}
			else if (this.map_id == 605)
			{
				this.name = "АРЕНА35";
			}
			else if (this.map_id == 606)
			{
				this.name = "АРЕНА50";
			}
			else if (this.map_id == 607)
			{
				this.name = "ХРАНИЛИЩЕ";
			}
			else if (this.map_id == 608)
			{
				this.name = "ИНДИЯ";
			}
			else if (this.map_id == 609)
			{
				this.name = "БЕЗУМИЕ";
			}
			else if (this.map_id == 610)
			{
				this.name = "ЭПИЦЕНТР";
			}
			else if (this.map_id == 611)
			{
				this.name = "РУИНЫ";
			}
			else if (this.map_id == 612)
			{
				this.name = "МИНИАЦТЕК";
			}
			else if (this.map_id == 613)
			{
				this.name = "МАСКА";
			}
			else if (this.map_id == 614)
			{
				this.name = "75ТН";
			}
			else if (this.map_id == 615)
			{
				this.name = "СТАЙЛ";
			}
			else if (this.map_id == 616)
			{
				this.name = "ФУРОР";
			}
			else if (this.map_id == 617)
			{
				this.name = "АРЕНА МАЙЯ";
			}
			else if (this.map_id == 618)
			{
				this.name = "КОРАБЛЬ";
			}
			else if (this.map_id == 619)
			{
				this.name = "ХОЛОД";
			}
			else if (this.map_id == 620)
			{
				this.name = "НАПРЯМИК";
			}
			else if (this.map_id == 621)
			{
				this.name = "РИСК";
			}
			else if (this.map_id == 622)
			{
				this.name = "МИНИ АЦТЕК";
			}
			else if (this.map_id == 623)
			{
				this.name = "ДИКИЙ ЗАПАД";
			}
			else if (this.map_id == 624)
			{
				this.name = "ВОКЗАЛ";
			}
			else if (this.map_id == 625)
			{
				this.name = "ПЕТЛЯ";
			}
			else if (this.map_id == 626)
			{
				this.name = "МЭНШЕН";
			}
			else if (this.map_id == 627)
			{
				this.name = "АНГАР2";
			}
			else if (this.map_id == 628)
			{
				this.name = "КОСМО";
			}
			else if (this.map_id == 629)
			{
				this.name = "МОНОХРОМ";
			}
			else if (this.map_id == 630)
			{
				this.name = "ОПИСТИОН";
			}
			else if (this.map_id == 901)
			{
				this.name = "ПРОРЫВ";
			}
			else if (this.map_id == 701)
			{
				this.name = "ПЛЯЖ";
			}
			else if (this.map_id == 702)
			{
				this.name = "ЗАМОК";
			}
			else if (this.map_id == 703)
			{
				this.name = "ОБОРОНА";
			}
			else if (this.map_id == 704)
			{
				this.name = "ГОРОД АМБРЕЛЛА";
			}
			else if (this.map_id == 705)
			{
				this.name = "ДЕРЕВНЯ";
			}
			else if (this.map_id == 706)
			{
				this.name = "ФОРТ";
			}
			else if (this.map_id == 707)
			{
				this.name = "ЗАБРОШЕННЫЙ ГОРОД";
			}
			else if (this.map_id == 708)
			{
				this.name = "НА БАЛУ";
			}
			else if (this.map_id == 709)
			{
				this.name = "ОБОРОНА";
			}
			else if (this.map_id == 710)
			{
				this.name = "АМБРЕЛЛА";
			}
			else if (this.map_id == 711)
			{
				this.name = "МАГИСТРАЛЬ";
			}
			else if (this.map_id == 712)
			{
				this.name = "ВЫЖИВАНИЕ";
			}
			else if (this.map_id == 1101)
			{
				this.name = "ЗИМНИЙ ГОРОД";
			}
			else if (this.map_id == 1102)
			{
				this.name = "ДОЛИНА";
			}
			else if (this.map_id == 1103)
			{
				this.name = "ЗИМНЯЯ ДОЛИНА";
			}
			else if (this.map_id == 1104)
			{
				this.name = "РИВЕРТАУН";
			}
			else if (this.map_id == 1105)
			{
				this.name = "СНОУТАУН";
			}
			else if (this.map_id == 1106)
			{
				this.name = "ПРОТИВОСТОЯНИЕ";
			}
			else if (this.map_id == 301)
			{
				this.name = "ДЕРЕВНЯ-Z 2";
			}
			else if (this.map_id == 302)
			{
				this.name = "ДОМ";
			}
			else if (this.map_id == 303)
			{
				this.name = "ЛАБИРИНТ";
			}
			else if (this.map_id == 304)
			{
				this.name = "МЕЛЬНИЦА";
			}
			else if (this.map_id == 305)
			{
				this.name = "РАКЕТА";
			}
			else if (this.map_id == 306)
			{
				this.name = "КИНДОМ";
			}
			else if (this.map_id == 307)
			{
				this.name = "ГОСПИТАЛЬ";
			}
			else if (this.map_id == 308)
			{
				this.name = "МИРНЫЙ АТОМ";
			}
			else if (this.map_id == 309)
			{
				this.name = "КАНАЛИЗАЦИЯ-Z";
			}
			else if (this.map_id == 310)
			{
				this.name = "ЛОВУШКА";
			}
			else if (this.map_id == 311)
			{
				this.name = "СТАРЫЙ БУНКЕР";
			}
			else if (this.map_id == 312)
			{
				this.name = "СТАРЫЙ ДОМ";
			}
			else if (this.map_id == 313)
			{
				this.name = "СТАРЫЙ ЛАБИРИНТ";
			}
			else if (this.map_id == 314)
			{
				this.name = "СТАРАЯ МЕЛЬНИЦА";
			}
			else if (this.map_id == 315)
			{
				this.name = "СТАРАЯ ДЕРЕВНЯ-Z";
			}
			else if (this.map_id == 316)
			{
				this.name = "СТАРАЯ РАКЕТА";
			}
			else if (this.map_id == 317)
			{
				this.name = "БУНКЕР";
			}
			if (this.name == "")
			{
				this.name = "КАРТА#" + this.map_id;
			}
			if (this.gamemode == 7 || this.gamemode == 9 || this.gamemode == 10)
			{
				if (this.maxplayers >= 64)
				{
					this.maxplayers -= 64;
					this.name = "идет игра";
				}
				else
				{
					this.name = "набор";
				}
			}
			int num2 = this.gamemode * 1000;
			if (num2 > 9000)
			{
				num2 /= 10;
			}
			if (this.gamemode == 0)
			{
				this.name = string.Concat(new object[]
				{
					"БИТВА#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 1)
			{
				this.name = string.Concat(new object[]
				{
					"СНЕЖКИ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 2)
			{
				this.name = string.Concat(new object[]
				{
					"СТРОЙКА#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 3)
			{
				this.name = string.Concat(new object[]
				{
					"ЗОМБИ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 4)
			{
				this.name = string.Concat(new object[]
				{
					"ЗАХВАТ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 5)
			{
				this.name = string.Concat(new object[]
				{
					"КОНТРА#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 6)
			{
				this.name = string.Concat(new object[]
				{
					"РЕЗНЯ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 7)
			{
				this.name = string.Concat(new object[]
				{
					"ВЫЖИВАНИЕ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 8)
			{
				this.name = string.Concat(new object[]
				{
					"АЛЬФА#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 9)
			{
				this.name = string.Concat(new object[]
				{
					"ПРОРЫВ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 10)
			{
				this.name = string.Concat(new object[]
				{
					"ЗАЧИСТКА#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
			else if (this.gamemode == 11)
			{
				this.name = string.Concat(new object[]
				{
					"ТАНКИ#",
					this.port - CONST.CFG.GAME_PORTS_OFFSET[(int)PlayerProfile.network] - num2,
					" - ",
					this.name
				});
			}
		}
		if (this.map_id == 0)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 2)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 3)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 4)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 5)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 6)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 7)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 8)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 9)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 10)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 11)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 12)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 13)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 14)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 15)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 16)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 17)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 18)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 19)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 20)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 21)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 22)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 23)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 24)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 25)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 26)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 27)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 28)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 29)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 30)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 31)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 32)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 33)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 34)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 35)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 36)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 37)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 38)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 39)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 401)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 402)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 403)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 404)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 405)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 406)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 407)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 408)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 501)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 502)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 503)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 504)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 505)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 506)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 507)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 508)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 509)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 510)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 511)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 512)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 513)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 514)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 515)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 516)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 517)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 518)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 519)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 520)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 521)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 522)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 523)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 524)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 525)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 526)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 527)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 528)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 529)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 530)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 531)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 532)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 533)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 534)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 535)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 536)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 537)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 538)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 539)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 540)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 541)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 542)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 543)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 544)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 545)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 546)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 547)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 548)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 549)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 550)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 551)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 552)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 601)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 602)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 603)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 604)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 605)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 606)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 607)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 608)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 609)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 610)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 611)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 612)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 613)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 614)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 615)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 616)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 617)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 618)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 619)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 620)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 621)
		{
			this.map_size = 0;
			return;
		}
		if (this.map_id == 622)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 623)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 624)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 625)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 626)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 901)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 701)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 702)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 703)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 704)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 705)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 706)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 707)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 708)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 709)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 710)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 711)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 712)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1101)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1102)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1103)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1104)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1105)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 1106)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 301)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 302)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 303)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 304)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 305)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 306)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 307)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 308)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 309)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 310)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 311)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 312)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 313)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 314)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 315)
		{
			this.map_size = 1;
			return;
		}
		if (this.map_id == 316)
		{
			this.map_size = 2;
			return;
		}
		if (this.map_id == 317)
		{
			this.map_size = 1;
		}
	}

	// Token: 0x040000FF RID: 255
	public int type;

	// Token: 0x04000100 RID: 256
	public int gamemode;

	// Token: 0x04000101 RID: 257
	public int players;

	// Token: 0x04000102 RID: 258
	public int maxplayers;

	// Token: 0x04000103 RID: 259
	public int map_id;

	// Token: 0x04000104 RID: 260
	public string name;

	// Token: 0x04000105 RID: 261
	public ulong adminid;

	// Token: 0x04000106 RID: 262
	public string ip;

	// Token: 0x04000107 RID: 263
	public int port;

	// Token: 0x04000108 RID: 264
	public int password;

	// Token: 0x04000109 RID: 265
	public bool hover;

	// Token: 0x0400010A RID: 266
	public int country_id;

	// Token: 0x0400010B RID: 267
	public int map_size;
}
