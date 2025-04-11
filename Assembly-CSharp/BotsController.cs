using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class BotsController : MonoBehaviour
{
	// Token: 0x06000128 RID: 296 RVA: 0x000183EC File Offset: 0x000165EC
	private void Awake()
	{
		BotsController.Instance = this;
		this.Gui = GameObject.Find("GUI");
		this.pd = (PackData)Object.FindObjectOfType(typeof(PackData));
		this.map = base.GetComponent<Map>();
		this.csrm = base.GetComponent<RagDollManager>();
		this.SkinManager = base.GetComponent<SpawnManager>();
		this.csig = this.Gui.GetComponent<MainGUI>();
		BlockSet blockSet = this.map.GetBlockSet();
		this.teamblock[0] = blockSet.GetBlock("Brick_blue");
		this.teamblock[1] = blockSet.GetBlock("Brick_red");
		this.teamblock[2] = blockSet.GetBlock("Brick_green");
		this.teamblock[3] = blockSet.GetBlock("Brick_yellow");
		this.teamblock[4] = blockSet.GetBlock("ArmoredBrickBlue");
		this.teamblock[5] = blockSet.GetBlock("ArmoredBrickRed");
		this.teamblock[6] = blockSet.GetBlock("ArmoredBrickGreen");
		this.teamblock[7] = blockSet.GetBlock("ArmoredBrickYellow");
		this.BotsGmObj = new GameObject[32];
		for (int i = 0; i < 32; i++)
		{
			this.Bots[i] = new BotData();
			this.CreatePlayer(i);
		}
		this.PlayersLoaded = true;
		this.CreateLocalPlayer();
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00018540 File Offset: 0x00016740
	private void CreateLocalPlayer()
	{
		this.CurrentPlayer = Object.Instantiate<GameObject>(this.pgoLocalPlayer, new Vector3(-1000f, -1000f, -1000f), this.pgoLocalPlayer.transform.rotation);
		this.CurrentPlayer.name = "Player";
		this.cscl = this.CurrentPlayer.AddComponent<Client>();
		this.CurrentPlayer.AddComponent<PlayerControl>();
		this.CurrentPlayer.AddComponent<Sound>();
		this.CurrentPlayer.AddComponent<FX>();
		this.csws = this.CurrentPlayer.AddComponent<WeaponSystem>();
		this.CurrentPlayer.GetComponent<vp_FPController>().client = this.cscl;
		float num = 0f;
		int distpos = Config.distpos;
		if (distpos == 2)
		{
			num = 140f;
		}
		else if (distpos == 1)
		{
			num = 80f;
		}
		else if (distpos == 0)
		{
			num = 40f;
		}
		float[] array = new float[32];
		for (int i = 0; i < 32; i++)
		{
			array[i] = num;
		}
		Camera.main.layerCullDistances = array;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00018640 File Offset: 0x00016840
	private void CreatePlayer(int index)
	{
		this.BotsGmObj[index] = Object.Instantiate<GameObject>(this.pgoPlayer, new Vector3(-1000f, -1000f, -1000f), this.pgoPlayer.transform.rotation);
		this.BotsGmObj[index].AddComponent<Data>();
		this.BotsGmObj[index].AddComponent<TeamColor>();
		this.BotsGmObj[index].AddComponent<Sound>();
		this.BotsGmObj[index].AddComponent<FX>();
		this.BotsGmObj[index].name = "Player_" + index.ToString();
		this.Bots[index].SpecView = GameObject.Find(this.BotsGmObj[index].name + "/specview");
		this.Bots[index].goHelmet = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Helmet");
		this.Bots[index].goCap = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Cap");
		this.Bots[index].goTykva = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Tykva");
		this.Bots[index].goKolpak = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/KOLPAK");
		this.Bots[index].goRoga = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/ROGA");
		this.Bots[index].goMaskBear = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_BEAR");
		this.Bots[index].goMaskFox = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_FOX");
		this.Bots[index].goMaskRabbit = GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_RABBIT");
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine").AddComponent<Data>().SetIndex(this, index, 0);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head").AddComponent<Data>().SetIndex(this, index, 1);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 L Thigh").AddComponent<Data>().SetIndex(this, index, 11);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 L Thigh/Bip001 L Calf").AddComponent<Data>().SetIndex(this, index, 12);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 L Thigh/Bip001 L Calf/Bip001 L Foot").AddComponent<Data>().SetIndex(this, index, 13);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 R Thigh").AddComponent<Data>().SetIndex(this, index, 8);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 R Thigh/Bip001 R Calf").AddComponent<Data>().SetIndex(this, index, 9);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 R Thigh/Bip001 R Calf/Bip001 R Foot").AddComponent<Data>().SetIndex(this, index, 10);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm").AddComponent<Data>().SetIndex(this, index, 5);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm").AddComponent<Data>().SetIndex(this, index, 6);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand").AddComponent<Data>().SetIndex(this, index, 7);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm").AddComponent<Data>().SetIndex(this, index, 2);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm").AddComponent<Data>().SetIndex(this, index, 3);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand").AddComponent<Data>().SetIndex(this, index, 4);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/shield/weapon").AddComponent<Data>().SetIndex(this, index, 77);
		GameObject.Find(this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/shield_lady/weapon").AddComponent<Data>().SetIndex(this, index, 77);
		string str = this.BotsGmObj[index].name + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/";
		this.Bots[index].weapon = new GameObject[350];
		this.Bots[index].weapon[0] = GameObject.Find(str + "xblock");
		this.Bots[index].weapon[33] = GameObject.Find(str + "shovel");
		this.Bots[index].weapon[43] = GameObject.Find(str + "m3");
		this.Bots[index].weapon[44] = GameObject.Find(str + "m14");
		this.Bots[index].weapon[45] = GameObject.Find(str + "mp5");
		this.Bots[index].weapon[2] = GameObject.Find(str + "ak47");
		this.Bots[index].weapon[3] = GameObject.Find(str + "svd");
		this.Bots[index].weapon[46] = GameObject.Find(str + "glock");
		this.Bots[index].weapon[9] = GameObject.Find(str + "deagle");
		this.Bots[index].weapon[10] = GameObject.Find(str + "shmel");
		this.Bots[index].weapon[12] = GameObject.Find(str + "asval");
		this.Bots[index].weapon[13] = GameObject.Find(str + "g36c");
		this.Bots[index].weapon[14] = GameObject.Find(str + "kriss");
		this.Bots[index].weapon[15] = GameObject.Find(str + "m4a1");
		this.Bots[index].weapon[19] = GameObject.Find(str + "vsk94");
		this.Bots[index].weapon[16] = GameObject.Find(str + "m249");
		this.Bots[index].weapon[17] = GameObject.Find(str + "sps12");
		this.Bots[index].weapon[18] = GameObject.Find(str + "vintorez");
		this.Bots[index].weapon[36] = GameObject.Find(str + "medkit_w");
		this.Bots[index].weapon[37] = GameObject.Find(str + "medkit_g");
		this.Bots[index].weapon[38] = GameObject.Find(str + "medkit_o");
		this.Bots[index].weapon[40] = GameObject.Find(str + "usp");
		this.Bots[index].weapon[47] = GameObject.Find(str + "barrett");
		this.Bots[index].weapon[48] = GameObject.Find(str + "tmp");
		this.Bots[index].weapon[50] = GameObject.Find(str + "axe");
		this.Bots[index].weapon[51] = GameObject.Find(str + "bat");
		this.Bots[index].weapon[52] = GameObject.Find(str + "crowbar");
		this.Bots[index].weapon[49] = GameObject.Find(str + "knife");
		this.Bots[index].weapon[53] = GameObject.Find(str + "caramel");
		this.Bots[index].weapon[55] = GameObject.Find(str + "tnt");
		this.Bots[index].weapon[62] = GameObject.Find(str + "minefly/weapon");
		this.Bots[index].weapon[60] = GameObject.Find(str + "auga3");
		this.Bots[index].weapon[61] = GameObject.Find(str + "sg552");
		this.Bots[index].weapon[68] = GameObject.Find(str + "m14ebr");
		this.Bots[index].weapon[69] = GameObject.Find(str + "l96a1/weapon");
		this.Bots[index].weapon[70] = GameObject.Find(str + "nova/weapon");
		this.Bots[index].weapon[71] = GameObject.Find(str + "kord/weapon");
		this.Bots[index].weapon[72] = GameObject.Find(str + "anaconda/weapon");
		this.Bots[index].weapon[73] = GameObject.Find(str + "scar/weapon");
		this.Bots[index].weapon[74] = GameObject.Find(str + "p90/weapon");
		this.Bots[index].weapon[78] = GameObject.Find(str + "rpk/weapon");
		this.Bots[index].weapon[79] = GameObject.Find(str + "hk416/weapon");
		this.Bots[index].weapon[80] = GameObject.Find(str + "ak102/weapon");
		this.Bots[index].weapon[81] = GameObject.Find(str + "sr25/weapon");
		this.Bots[index].weapon[82] = GameObject.Find(str + "mglmk1/weapon");
		this.Bots[index].weapon[89] = GameObject.Find(str + "mosin/weapon");
		this.Bots[index].weapon[90] = GameObject.Find(str + "ppsh/weapon");
		this.Bots[index].weapon[91] = GameObject.Find(str + "mp40/weapon");
		this.Bots[index].weapon[34] = GameObject.Find(str + "l96a1mod/weapon");
		this.Bots[index].weapon[93] = GameObject.Find(str + "kacpdw/weapon");
		this.Bots[index].weapon[94] = GameObject.Find(str + "famas/weapon");
		this.Bots[index].weapon[95] = GameObject.Find(str + "beretta/weapon");
		this.Bots[index].weapon[96] = GameObject.Find(str + "machete/weapon");
		this.Bots[index].weapon[100] = GameObject.Find(str + "rpg7/weapon");
		this.Bots[index].weapon[101] = GameObject.Find(str + "repair_tool/weapon");
		this.Bots[index].weapon[102] = GameObject.Find(str + "aa12/weapon");
		this.Bots[index].weapon[103] = GameObject.Find(str + "fn57/weapon");
		this.Bots[index].weapon[104] = GameObject.Find(str + "fs2000/weapon");
		this.Bots[index].weapon[105] = GameObject.Find(str + "l85/weapon");
		this.Bots[index].weapon[106] = GameObject.Find(str + "mac10/weapon");
		this.Bots[index].weapon[107] = GameObject.Find(str + "pkp/weapon");
		this.Bots[index].weapon[108] = GameObject.Find(str + "pm/weapon");
		this.Bots[index].weapon[109] = GameObject.Find(str + "tar21/weapon");
		this.Bots[index].weapon[110] = GameObject.Find(str + "ump45/weapon");
		this.Bots[index].weapon[111] = GameObject.Find(str + "ntw20/weapon");
		this.Bots[index].weapon[112] = GameObject.Find(str + "vintorez_desert/weapon");
		this.Bots[index].weapon[137] = GameObject.Find(str + "minigun/weapon");
		this.Bots[index].weapon[138] = GameObject.Find(str + "javelin/weapon");
		this.Bots[index].weapon[139] = GameObject.Find(str + "zaa12/weapon");
		this.Bots[index].weapon[140] = GameObject.Find(str + "zasval/weapon");
		this.Bots[index].weapon[141] = GameObject.Find(str + "zfn57/weapon");
		this.Bots[index].weapon[142] = GameObject.Find(str + "zkord/weapon");
		this.Bots[index].weapon[143] = GameObject.Find(str + "zm249/weapon");
		this.Bots[index].weapon[144] = GameObject.Find(str + "zminigun/weapon");
		this.Bots[index].weapon[145] = GameObject.Find(str + "zsps12/weapon");
		this.Bots[index].weapon[157] = GameObject.Find(str + "tube/weapon");
		this.Bots[index].weapon[158] = GameObject.Find(str + "bulava/weapon");
		this.Bots[index].weapon[159] = GameObject.Find(str + "katana/weapon");
		this.Bots[index].weapon[160] = GameObject.Find(str + "mauzer/weapon");
		this.Bots[index].weapon[161] = GameObject.Find(str + "crossbow/weapon");
		this.Bots[index].weapon[162] = GameObject.Find(str + "qbz95/weapon");
		this.Bots[index].weapon[171] = GameObject.Find(str + "mine/weapon");
		this.Bots[index].weapon[172] = GameObject.Find(str + "c4/weapon");
		this.Bots[index].weapon[173] = GameObject.Find(str + "chopper/weapon");
		this.Bots[index].weapon[174] = GameObject.Find(str + "shield/weapon");
		this.Bots[index].weapon[175] = GameObject.Find(str + "aksu/weapon");
		this.Bots[index].weapon[176] = GameObject.Find(str + "m700/weapon");
		this.Bots[index].weapon[177] = GameObject.Find(str + "stechkin/weapon");
		this.Bots[index].weapon[183] = GameObject.Find(str + "at_mine/weapon");
		this.Bots[index].weapon[185] = GameObject.Find(str + "m202/weapon");
		this.Bots[index].weapon[188] = GameObject.Find(str + "dpm/weapon");
		this.Bots[index].weapon[189] = GameObject.Find(str + "macm1924/weapon");
		this.Bots[index].weapon[190] = GameObject.Find(str + "mg42/weapon");
		this.Bots[index].weapon[191] = GameObject.Find(str + "stenmk2/weapon");
		this.Bots[index].weapon[192] = GameObject.Find(str + "m1a1/weapon");
		this.Bots[index].weapon[193] = GameObject.Find(str + "type99/weapon");
		this.Bots[index].weapon[207] = GameObject.Find(str + "bizon/weapon");
		this.Bots[index].weapon[208] = GameObject.Find(str + "groza/weapon");
		this.Bots[index].weapon[209] = GameObject.Find(str + "jackhammer/weapon");
		this.Bots[index].weapon[210] = GameObject.Find(str + "pila/weapon");
		this.Bots[index].weapon[218] = GameObject.Find(str + "psg_1/weapon");
		this.Bots[index].weapon[219] = GameObject.Find(str + "krytac/weapon");
		this.Bots[index].weapon[220] = GameObject.Find(str + "mp5sd/weapon");
		this.Bots[index].weapon[221] = GameObject.Find(str + "colts/weapon");
		this.Bots[index].weapon[301] = GameObject.Find(str + "jackhammer_lady/weapon");
		this.Bots[index].weapon[302] = GameObject.Find(str + "m700_lady/weapon");
		this.Bots[index].weapon[303] = GameObject.Find(str + "mg42_lady/weapon");
		this.Bots[index].weapon[304] = GameObject.Find(str + "shield_lady/weapon");
		this.Bots[index].weapon[305] = GameObject.Find(str + "magnum_lady/weapon");
		this.Bots[index].weapon[308] = GameObject.Find(str + "scorpion/weapon");
		this.Bots[index].weapon[309] = GameObject.Find(str + "g36c_veteran/weapon");
		this.Bots[index].weapon[313] = GameObject.Find(str + "fmg9/weapon");
		this.Bots[index].weapon[314] = GameObject.Find(str + "saiga/weapon");
		this.Bots[index].weapon[315] = GameObject.Find(str + "flamethrower/weapon");
		this.Bots[index].weapon[329] = GameObject.Find(str + "ak47_snow/weapon");
		this.Bots[index].weapon[330] = GameObject.Find(str + "p90_snow/weapon");
		this.Bots[index].weapon[331] = GameObject.Find(str + "saiga_snow/weapon");
		this.Bots[index].weapon[332] = GameObject.Find(str + "sr25_snow/weapon");
		this.Bots[index].weapon[333] = GameObject.Find(str + "usp_snow/weapon");
		this.Bots[index].flash = new GameObject[350];
		this.Bots[index].flash[33] = GameObject.Find(str + "shovel/flash");
		this.Bots[index].flash[43] = GameObject.Find(str + "m3/flash");
		this.Bots[index].flash[44] = GameObject.Find(str + "m14/flash");
		this.Bots[index].flash[45] = GameObject.Find(str + "mp5/flash");
		this.Bots[index].flash[2] = GameObject.Find(str + "ak47/flash");
		this.Bots[index].flash[3] = GameObject.Find(str + "svd/flash");
		this.Bots[index].flash[46] = GameObject.Find(str + "glock/flash");
		this.Bots[index].flash[9] = GameObject.Find(str + "deagle/flash");
		this.Bots[index].flash[10] = GameObject.Find(str + "shmel/flash");
		this.Bots[index].flash[12] = GameObject.Find(str + "asval/flash");
		this.Bots[index].flash[13] = GameObject.Find(str + "g36c/flash");
		this.Bots[index].flash[14] = GameObject.Find(str + "kriss/flash");
		this.Bots[index].flash[15] = GameObject.Find(str + "m4a1/flash");
		this.Bots[index].flash[19] = GameObject.Find(str + "vsk94/flash");
		this.Bots[index].flash[16] = GameObject.Find(str + "m249/flash");
		this.Bots[index].flash[17] = GameObject.Find(str + "sps12/flash");
		this.Bots[index].flash[18] = GameObject.Find(str + "vintorez/flash");
		this.Bots[index].flash[40] = GameObject.Find(str + "usp/flash");
		this.Bots[index].flash[47] = GameObject.Find(str + "barrett/flash");
		this.Bots[index].flash[48] = GameObject.Find(str + "tmp/flash");
		this.Bots[index].flash[50] = GameObject.Find(str + "axe/flash");
		this.Bots[index].flash[51] = GameObject.Find(str + "bat/flash");
		this.Bots[index].flash[52] = GameObject.Find(str + "crowbar/flash");
		this.Bots[index].flash[49] = GameObject.Find(str + "knife/flash");
		this.Bots[index].flash[53] = GameObject.Find(str + "caramel/flash");
		this.Bots[index].flash[62] = GameObject.Find(str + "minefly/flash");
		this.Bots[index].flash[60] = GameObject.Find(str + "auga3/flash");
		this.Bots[index].flash[61] = GameObject.Find(str + "sg552/flash");
		this.Bots[index].flash[68] = GameObject.Find(str + "m14ebr/flash");
		this.Bots[index].flash[69] = GameObject.Find(str + "l96a1/flash");
		this.Bots[index].flash[70] = GameObject.Find(str + "nova/flash");
		this.Bots[index].flash[71] = GameObject.Find(str + "kord/flash");
		this.Bots[index].flash[72] = GameObject.Find(str + "anaconda/flash");
		this.Bots[index].flash[73] = GameObject.Find(str + "scar/flash");
		this.Bots[index].flash[78] = GameObject.Find(str + "rpk/flash");
		this.Bots[index].flash[79] = GameObject.Find(str + "hk416/flash");
		this.Bots[index].flash[80] = GameObject.Find(str + "ak102/flash");
		this.Bots[index].flash[81] = GameObject.Find(str + "sr25/flash");
		this.Bots[index].flash[82] = GameObject.Find(str + "mglmk1/flash");
		this.Bots[index].flash[89] = GameObject.Find(str + "mosin/flash");
		this.Bots[index].flash[90] = GameObject.Find(str + "ppsh/flash");
		this.Bots[index].flash[91] = GameObject.Find(str + "mp40/flash");
		this.Bots[index].flash[93] = GameObject.Find(str + "kacpdw/flash");
		this.Bots[index].flash[94] = GameObject.Find(str + "famas/flash");
		this.Bots[index].flash[95] = GameObject.Find(str + "beretta/flash");
		this.Bots[index].flash[96] = GameObject.Find(str + "machete/flash");
		this.Bots[index].flash[100] = GameObject.Find(str + "rpg7/flash");
		this.Bots[index].flash[101] = GameObject.Find(str + "repair_tool/flash");
		this.Bots[index].flash[102] = GameObject.Find(str + "aa12/flash");
		this.Bots[index].flash[104] = GameObject.Find(str + "fs2000/flash");
		this.Bots[index].flash[105] = GameObject.Find(str + "l85/flash");
		this.Bots[index].flash[106] = GameObject.Find(str + "mac10/flash");
		this.Bots[index].flash[107] = GameObject.Find(str + "pkp/flash");
		this.Bots[index].flash[108] = GameObject.Find(str + "pm/flash");
		this.Bots[index].flash[109] = GameObject.Find(str + "tar21/flash");
		this.Bots[index].flash[110] = GameObject.Find(str + "ump45/flash");
		this.Bots[index].flash[111] = GameObject.Find(str + "ntw20/flash");
		this.Bots[index].flash[137] = GameObject.Find(str + "minigun/flash");
		this.Bots[index].flash[138] = GameObject.Find(str + "javelin/flash");
		this.Bots[index].flash[139] = GameObject.Find(str + "zaa12/flash");
		this.Bots[index].flash[142] = GameObject.Find(str + "zkord/flash");
		this.Bots[index].flash[143] = GameObject.Find(str + "zm249/flash");
		this.Bots[index].flash[144] = GameObject.Find(str + "zminigun/flash");
		this.Bots[index].flash[145] = GameObject.Find(str + "zsps12/flash");
		this.Bots[index].flash[157] = GameObject.Find(str + "tube/flash");
		this.Bots[index].flash[158] = GameObject.Find(str + "bulava/flash");
		this.Bots[index].flash[159] = GameObject.Find(str + "katana/flash");
		this.Bots[index].flash[160] = GameObject.Find(str + "mauzer/flash");
		this.Bots[index].flash[162] = GameObject.Find(str + "qbz95/flash");
		this.Bots[index].flash[173] = GameObject.Find(str + "chopper/flash");
		this.Bots[index].flash[175] = GameObject.Find(str + "aksu/flash");
		this.Bots[index].flash[177] = GameObject.Find(str + "stechkin/flash");
		this.Bots[index].flash[185] = GameObject.Find(str + "m202/flash");
		this.Bots[index].flash[188] = GameObject.Find(str + "dpm/flash");
		this.Bots[index].flash[189] = GameObject.Find(str + "macm1924/flash");
		this.Bots[index].flash[190] = GameObject.Find(str + "mg42/flash");
		this.Bots[index].flash[191] = GameObject.Find(str + "stenmk2/flash");
		this.Bots[index].flash[192] = GameObject.Find(str + "m1a1/flash");
		this.Bots[index].flash[193] = GameObject.Find(str + "type99/flash");
		this.Bots[index].flash[207] = GameObject.Find(str + "bizon/flash");
		this.Bots[index].flash[208] = GameObject.Find(str + "groza/flash");
		this.Bots[index].flash[209] = GameObject.Find(str + "jackhammer/flash");
		this.Bots[index].flash[210] = GameObject.Find(str + "pila/flash");
		this.Bots[index].flash[218] = GameObject.Find(str + "psg_1/flash");
		this.Bots[index].flash[219] = GameObject.Find(str + "krytac/flash");
		this.Bots[index].flash[221] = GameObject.Find(str + "colts/flash");
		this.Bots[index].flash[301] = GameObject.Find(str + "jackhammer_lady/flash");
		this.Bots[index].flash[303] = GameObject.Find(str + "mg42_lady/flash");
		this.Bots[index].flash[305] = GameObject.Find(str + "magnum_lady/flash");
		this.Bots[index].flash[308] = GameObject.Find(str + "scorpion/flash");
		this.Bots[index].flash[309] = GameObject.Find(str + "g36c_veteran/flash");
		this.Bots[index].flash[313] = GameObject.Find(str + "fmg9/flash");
		this.Bots[index].flash[314] = GameObject.Find(str + "saiga/flash");
		this.Bots[index].flash[315] = GameObject.Find(str + "flamethrower/flash");
		this.Bots[index].flamePS = this.Bots[index].flash[315].GetComponent<ParticleSystem>();
		this.Bots[index].flash[329] = GameObject.Find(str + "ak47_snow/flash");
		this.Bots[index].flash[331] = GameObject.Find(str + "saiga_snow/flash");
		this.Bots[index].flash[332] = GameObject.Find(str + "sr25_snow/flash");
		this.Bots[index].m_Top = GameObject.Find(str + "xblock/top");
		this.Bots[index].m_Face = GameObject.Find(str + "xblock/face");
		this.Bots[index].Item = new int[350];
		this.SetPlayerActive(index, false);
		this.Bots[index].position = new Vector3(-1000f, -1000f, -1000f);
		this.pd.PackPlayerPos(index, -1000f, -1000f, -1000f);
		this.SetCurrentWeapon(index, 8);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0001A7FC File Offset: 0x000189FC
	public void SetPlayerActive(int index, bool val)
	{
		if (val)
		{
			this.BotsGmObj[index].layer = 10;
			this.Bots[index].Active = true;
			this.BotsGmObj[index].GetComponent<Animator>().enabled = true;
			foreach (Renderer renderer in this.BotsGmObj[index].GetComponentsInChildren<Renderer>())
			{
				if (!(renderer.gameObject.name == "weapon") && !(renderer.gameObject.name == "face") && !(renderer.gameObject.name == "top") && !(renderer.gameObject.name == "flash") && !(renderer.gameObject.name == "rocket_rpg7") && !(renderer.gameObject.name == "rpg7") && !(renderer.gameObject.name == "crossbow") && !(renderer.gameObject.name == "arrow") && !(renderer.gameObject.name == "Snow"))
				{
					renderer.gameObject.layer = 10;
				}
			}
			foreach (Collider collider in this.BotsGmObj[index].GetComponentsInChildren<Collider>())
			{
				if (collider.transform.parent.GetComponent<Tank>() != null)
				{
					collider.gameObject.layer = 0;
				}
				else if (collider.transform.parent.GetComponent<Car>() != null)
				{
					collider.gameObject.layer = 0;
				}
				else
				{
					collider.gameObject.layer = 10;
				}
			}
			this.SetCurrentWeapon(index, 1);
			return;
		}
		this.BotsGmObj[index].layer = 9;
		this.BotsGmObj[index].GetComponent<Animator>().enabled = false;
		this.Bots[index].Active = false;
		this.Bots[index].Helmet = 0;
		this.Bots[index].Skin = 0;
		foreach (Renderer renderer2 in this.BotsGmObj[index].GetComponentsInChildren<Renderer>())
		{
			if (!(renderer2.gameObject.name == "weapon") && !(renderer2.gameObject.name == "face") && !(renderer2.gameObject.name == "top") && !(renderer2.gameObject.name == "flash") && !(renderer2.gameObject.name == "rocket_rpg7") && !(renderer2.gameObject.name == "rpg7") && !(renderer2.gameObject.name == "crossbow") && !(renderer2.gameObject.name == "arrow") && !(renderer2.gameObject.name == "Snow"))
			{
				renderer2.gameObject.layer = 9;
			}
		}
		foreach (Collider collider2 in this.BotsGmObj[index].GetComponentsInChildren<Collider>())
		{
			if (!(collider2.gameObject.name == "weapon"))
			{
				collider2.gameObject.layer = 9;
			}
		}
		this.BotsGmObj[index].transform.position = new Vector3(-1000f, -1000f, -1000f);
		this.SetCurrentWeapon(index, 8);
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0001ABB0 File Offset: 0x00018DB0
	private void Update()
	{
		if (!this.PlayersLoaded)
		{
			return;
		}
		for (int i = 0; i < 32; i++)
		{
			if (this.Bots[i] != null && this.Bots[i].Active)
			{
				if (this.Bots[i].Dead == 1)
				{
					if (this.Bots[i].flamePS != null && this.Bots[i].flamePS.isPlaying)
					{
						this.Bots[i].flamePS.Stop(true);
					}
					if (this.Bots[i].mySound != null && this.Bots[i].mySound.csas != null && this.Bots[i].mySound.csas.isPlaying && this.Bots[i].mySound.csas.clip != SoundManager.weapon_flamethrower_end)
					{
						this.Bots[i].mySound.csas.pitch = 1f;
						this.Bots[i].mySound.csas.loop = false;
						this.Bots[i].mySound.csas.clip = SoundManager.weapon_flamethrower_end;
						this.Bots[i].mySound.csas.Play();
					}
					if (this.Bots[i].flash[this.Bots[i].WeaponID] != null)
					{
						this.Bots[i].flash[this.Bots[i].WeaponID].SetActive(false);
					}
				}
				else if (this.cscl.myindex != i)
				{
					if (this.Bots[i].mySound == null)
					{
						this.Bots[i].mySound = this.BotsGmObj[i].GetComponent<Sound>();
					}
					if (Time.time > this.Bots[i].flash_time)
					{
						if (this.Bots[i].flamePS != null && this.Bots[i].flamePS.isPlaying)
						{
							this.Bots[i].flamePS.Stop(true);
						}
						if (this.Bots[i].mySound != null && this.Bots[i].mySound.csas != null && this.Bots[i].mySound.csas.isPlaying && this.Bots[i].mySound.csas.clip != SoundManager.weapon_flamethrower_end)
						{
							this.Bots[i].mySound.csas.pitch = 1f;
							this.Bots[i].mySound.csas.loop = false;
							this.Bots[i].mySound.csas.clip = SoundManager.weapon_flamethrower_end;
							this.Bots[i].mySound.csas.Play();
						}
						if (this.Bots[i].flash[this.Bots[i].WeaponID] != null)
						{
							this.Bots[i].flash[this.Bots[i].WeaponID].SetActive(false);
						}
					}
					if (Time.time < this.Bots[i].flash_time && this.Bots[i].WeaponID == 315 && !this.Bots[i].zombie && this.Bots[i].mySound != null && this.Bots[i].mySound.csas != null && !this.Bots[i].mySound.csas.isPlaying && this.Bots[i].mySound.csas.clip != SoundManager.weapon_flamethrower_start)
					{
						this.Bots[i].mySound.csas.pitch = 1f;
						this.Bots[i].mySound.csas.loop = false;
						this.Bots[i].mySound.csas.clip = SoundManager.weapon_flamethrower_start;
						this.Bots[i].mySound.csas.Play();
						vp_Timer.In(SoundManager.weapon_flamethrower_start.length - 0.1f, delegate(object _index)
						{
							if (this.Bots[(int)_index].flash_time > Time.time && this.Bots[(int)_index].mySound.csas.clip != SoundManager.weapon_flamethrower)
							{
								this.Bots[(int)_index].mySound.csas.pitch = 1f;
								this.Bots[(int)_index].mySound.csas.loop = true;
								this.Bots[(int)_index].mySound.csas.clip = SoundManager.weapon_flamethrower;
								this.Bots[(int)_index].mySound.csas.Play();
							}
						}, i, null);
					}
					Vector3 vector = this.Bots[i].oldpos - this.Bots[i].position;
					int num = 0;
					if (this.Bots[i].inVehicle)
					{
						num = 5;
					}
					else
					{
						if (this.Bots[i].State == 2)
						{
							num = 3;
						}
						if ((double)vector.magnitude > 0.05)
						{
							if (this.Bots[i].State == 1 || this.Bots[i].State == 3 || this.Bots[i].State == 4)
							{
								num = 1;
							}
							else if (this.Bots[i].State == 2)
							{
								num = 4;
							}
							else
							{
								num = 2;
							}
						}
					}
					if (num != this.Bots[i].AnimState)
					{
						this.Bots[i].AnimState = num;
						if (num == 0)
						{
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(0f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(false);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(false);
						}
						else if (num == 1)
						{
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(50f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(false);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(false);
						}
						else if (num == 2)
						{
							if (this.map == null)
							{
								this.map = Object.FindObjectOfType<Map>();
							}
							this.Bots[i].b = this.map.GetBlock((int)this.BotsGmObj[i].transform.position.x, (int)this.BotsGmObj[i].transform.position.y, (int)this.BotsGmObj[i].transform.position.z).block;
							this.Bots[i].bUp = this.map.GetBlock((int)this.BotsGmObj[i].transform.position.x, (int)this.BotsGmObj[i].transform.position.y + 1, (int)this.BotsGmObj[i].transform.position.z).block;
							if (this.Bots[i].bUp != null && this.Bots[i].bUp.GetName() == "!Water")
							{
								this.Bots[i].b = this.Bots[i].bUp;
							}
							if (this.Bots[i].b != null)
							{
								if (ZipLoader.GetBlockType(this.Bots[i].b) != this.Bots[i].currBlockType)
								{
									this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
									this.Bots[i].currBlockType = ZipLoader.GetBlockType(this.Bots[i].b);
								}
							}
							else if (1 != this.Bots[i].currBlockType)
							{
								this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
								this.Bots[i].currBlockType = 1;
							}
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Walk(this.Bots[i].currBlockType);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(150f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(false);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(false);
						}
						else if (num == 3)
						{
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(0f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(true);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(false);
						}
						else if (num == 4)
						{
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(50f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(true);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(false);
						}
						else if (num == 5)
						{
							this.BotsGmObj[i].GetComponent<Sound>().PlaySound_Stop(null);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetSpeed(0f);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetCrouch(false);
							this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetJeepGunner(true);
						}
					}
					if (num != 5 || this.Bots[i].inVehiclePos == CONST.VEHICLES.POSITION_JEEP_DRIVER)
					{
						this.BotsGmObj[i].transform.position = Vector3.Lerp(this.BotsGmObj[i].transform.position, this.Bots[i].position, Time.deltaTime * 5f);
						float num2 = this.BotsGmObj[i].transform.eulerAngles.y;
						float num3 = this.Bots[i].rotation.y - num2;
						if (num3 > 180f)
						{
							num2 += 360f;
						}
						if (num3 < -180f)
						{
							num2 -= 360f;
						}
						num2 = Mathf.Lerp(num2, this.Bots[i].rotation.y, Time.deltaTime * 5f);
						this.BotsGmObj[i].transform.eulerAngles = new Vector3(0f, num2, 0f);
						this.BotsGmObj[i].GetComponent<PlayerAnimation>().SetRotation(this.Bots[i].rotation.x);
					}
				}
			}
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0001B61C File Offset: 0x0001981C
	public void SetPosition(int index, float pX, float pY, float pZ)
	{
		this.pd.PackPlayerPos(index, pX, pY, pZ);
		this.BotsGmObj[index].transform.position = new Vector3(pX, pY, pZ);
		this.Bots[index].oldpos = this.Bots[index].position;
		this.Bots[index].position = new Vector3(pX, pY, pZ);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0001B684 File Offset: 0x00019884
	public void UpdatePosition(int index, float pX, float pY, float pZ, float rX, float rY, float rZ, int state)
	{
		if (this.Bots[index].Dead == 1)
		{
			return;
		}
		if (this.Bots[index].Team == 255)
		{
			return;
		}
		this.Bots[index].State = state;
		this.pd.CheckPlayerPos(index);
		this.pd.PackPlayerPos(index, pX, pY, pZ);
		if (!this.Bots[index].Active)
		{
			this.BotsGmObj[index].transform.position = new Vector3(pX, pY, pZ);
			this.BotsGmObj[index].transform.eulerAngles = new Vector3(0f, rY, 0f);
		}
		this.Bots[index].oldpos = this.Bots[index].position;
		this.Bots[index].position = new Vector3(pX, pY, pZ);
		this.Bots[index].rotation = new Vector3(rX, rY, 0f);
		if (this.Bots[index].zombie && Time.time > this.zmupdate)
		{
			this.zmupdate = Time.time + 10f;
			this.BotsGmObj[index].GetComponent<Sound>().PlaySound_ZM_Ambient();
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0001B7B9 File Offset: 0x000199B9
	public void UpdateBlock(int x, int y, int z, int health, bool fx)
	{
		if (health == 0)
		{
			base.StartCoroutine(this.UpdateBlock_coroutine(x, y, z, health, fx, true));
			return;
		}
		base.StartCoroutine(this.UpdateBlock_coroutine(x, y, z, health, fx, false));
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0001B7EA File Offset: 0x000199EA
	private IEnumerator UpdateBlock_coroutine(int x, int y, int z, int health, bool fx, bool del)
	{
		float time = Time.time;
		if (time < this.lastupdate + 0.01f)
		{
			this.lastupdate = time;
			yield return new WaitForSeconds(0.01f);
		}
		else
		{
			this.lastupdate = time;
		}
		if (del)
		{
			this.map.SetBlockAndRecompute(default(BlockData), new Vector3i(x, y, z));
		}
		yield break;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0001B818 File Offset: 0x00019A18
	public void JoinTeamClass(int index, int _team, int _class)
	{
		this.Bots[index].Team = (byte)_team;
		this.BotsGmObj[index].GetComponent<TeamColor>().SetTeam(_team, this.Bots[index].Skin, this.Bots[index].goHelmet, this.Bots[index].goCap, this.Bots[index].Znak);
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0001B87C File Offset: 0x00019A7C
	public void SetAttack(int index, int weaponid)
	{
		if (weaponid == CONST.VEHICLES.VEHICLE_MODUL_TANK_MG)
		{
			if (this.BotsGmObj[index].GetComponentInChildren<Tank>() == null)
			{
				return;
			}
			this.BotsGmObj[index].GetComponent<Sound>().PlaySound_WeaponMGTank(this.BotsGmObj[index].GetComponent<AudioSource>());
			this.BotsGmObj[index].GetComponentInChildren<Tank>().MGFlash.gameObject.SetActive(true);
			this.BotsGmObj[index].GetComponentInChildren<Tank>().FlashTime = Time.time + 0.05f;
			return;
		}
		else
		{
			if (weaponid != 136)
			{
				if (this.Bots[index].WeaponID != weaponid)
				{
					this.SetCurrentWeapon(index, weaponid);
					this.Bots[index].WeaponID = weaponid;
				}
				if (this.Bots[index].mySound == null)
				{
					this.Bots[index].mySound = this.BotsGmObj[index].GetComponent<Sound>();
				}
				if (weaponid != 125)
				{
					if (this.Bots[index].mySound.csas.loop)
					{
						this.Bots[index].mySound.csas.loop = false;
					}
					this.Bots[index].mySound.PlaySound_Weapon(weaponid);
				}
				if (this.Bots[index].flash[weaponid] != null)
				{
					this.Bots[index].flash[weaponid].SetActive(true);
					if (weaponid == 1)
					{
						this.Bots[index].flash_time = Time.time + 0.05f;
						return;
					}
					if (weaponid == 125)
					{
						if (this.Bots[index].flamePS != null && !this.Bots[index].flamePS.isPlaying)
						{
							this.Bots[index].flamePS.Play(true);
						}
						this.Bots[index].flash_time = Time.time + 0.15f;
						return;
					}
					this.Bots[index].flash_time = Time.time + 0.01f;
				}
				return;
			}
			Car car = this.BotsGmObj[index].GetComponentInChildren<Car>();
			if (car == null)
			{
				car = this.BotsGmObj[index].GetComponentInParent<Car>();
			}
			if (car == null)
			{
				return;
			}
			this.BotsGmObj[index].GetComponent<Sound>().PlaySound_WeaponMGTank(this.BotsGmObj[index].GetComponent<AudioSource>());
			car.MGFlash.gameObject.SetActive(true);
			car.FlashTime = Time.time + 0.05f;
			return;
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0001BAD8 File Offset: 0x00019CD8
	public void SetBlock(int x, int y, int z, int team)
	{
		if (team < 0 || team > 7)
		{
			return;
		}
		if (x < 0 || x >= 256 || y < 0 || y >= 64 || z < 0 || z >= 256)
		{
			return;
		}
		BlockData block = new BlockData(this.teamblock[team]);
		block.SetDirection(BotsController.GetDirection(-base.transform.forward));
		this.map.SetBlockAndRecompute(block, new Vector3i(x, y, z));
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0001BB54 File Offset: 0x00019D54
	public void CreateDeadEvent(int attackerid, int victimid, int weaponid)
	{
		if (attackerid >= 32)
		{
			attackerid = victimid;
		}
		this.Bots[victimid].Dead = 1;
		int team = (int)this.Bots[victimid].Team;
		int skin = this.Bots[victimid].Skin;
		if (this.Bots[victimid].zombie)
		{
			team = 4;
			skin = 0;
		}
		bool self = false;
		if (attackerid == this.cscl.myindex)
		{
			self = true;
		}
		if (weaponid != 122 && this.Bots[victimid].weapon[this.Bots[victimid].WeaponID] != null && this.BotsGmObj[attackerid] != null)
		{
			int num = 0;
			if (this.Bots[victimid].Item[198] > 0)
			{
				num = 4;
			}
			this.csrm.CreateWeapon(this.Bots[victimid].weapon[this.Bots[victimid].WeaponID], this.Bots[victimid].weapon[this.Bots[victimid].WeaponID].transform.eulerAngles, this.BotsGmObj[attackerid].transform, this.Bots[victimid].WeaponID, weaponid, this.csig.GetBlockTextureTeam((int)this.Bots[victimid].Team + num));
		}
		this.csrm.CreatePlayerRagDoll(this.BotsGmObj[victimid], this.BotsGmObj[attackerid], victimid, false, team, skin, weaponid, this.Bots[victimid].goHelmet.GetComponent<Renderer>().enabled || this.Bots[victimid].goCap.GetComponent<Renderer>().enabled, self, this.Bots[victimid].goTykva.GetComponent<Renderer>().enabled, this.Bots[victimid].goKolpak.GetComponent<Renderer>().enabled, this.Bots[victimid].goRoga.GetComponent<Renderer>().enabled, this.Bots[victimid].goMaskBear.GetComponent<Renderer>().enabled, this.Bots[victimid].goMaskFox.GetComponent<Renderer>().enabled, this.Bots[victimid].goMaskRabbit.GetComponent<Renderer>().enabled);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0001BD74 File Offset: 0x00019F74
	public void CreateDeadEventSelf(int attackerid, int victimid, int weaponid)
	{
		if (attackerid >= 32)
		{
			attackerid = victimid;
		}
		if (this.CurrentPlayer.GetComponentInChildren<Tank>() != null)
		{
			this.CurrentPlayer.GetComponent<TankController>().currTank = null;
			this.CurrentPlayer.GetComponent<TankController>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().vehicleType = CONST.VEHICLES.NONE;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().zoom = false;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().enabled = false;
			GameObject.Find("RayCastBox").transform.position = new Vector3(-1000f, -1000f, -1000f);
			GameObject.Find("RayCastBox").transform.eulerAngles = new Vector3(-1000f, -1000f, -1000f);
			this.CurrentPlayer.GetComponentInChildren<Tank>().KillSelf();
		}
		else if (this.CurrentPlayer.GetComponentInChildren<Car>() != null)
		{
			this.CurrentPlayer.GetComponent<CarController>().currCar = null;
			this.CurrentPlayer.GetComponent<CarController>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().vehicleType = CONST.VEHICLES.NONE;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().zoom = false;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().enabled = false;
			GameObject.Find("RayCastBox").transform.position = new Vector3(-1000f, -1000f, -1000f);
			GameObject.Find("RayCastBox").transform.eulerAngles = new Vector3(-1000f, -1000f, -1000f);
			this.CurrentPlayer.GetComponentInChildren<Car>().KillSelf();
		}
		else if (this.CurrentPlayer.GetComponentInParent<Car>() != null)
		{
			Car componentInParent = this.CurrentPlayer.GetComponentInParent<Car>();
			this.CurrentPlayer.GetComponent<CarController>().currCar = null;
			this.CurrentPlayer.GetComponent<CarController>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().enabled = false;
			this.CurrentPlayer.GetComponent<TransportExit>().vehicleType = CONST.VEHICLES.NONE;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().zoom = false;
			this.CurrentPlayer.GetComponentInChildren<OrbitCam>().enabled = false;
			GameObject.Find("RayCastBox").transform.position = new Vector3(-1000f, -1000f, -1000f);
			GameObject.Find("RayCastBox").transform.eulerAngles = new Vector3(-1000f, -1000f, -1000f);
			this.CurrentPlayer.transform.parent = null;
			if (this.CurrentPlayer.GetComponent<CarController>().myPosition != CONST.VEHICLES.POSITION_JEEP_GUNNER)
			{
				componentInParent.KillSelf();
			}
		}
		this.Bots[victimid].Dead = 1;
		this.BotsGmObj[victimid].transform.position = this.CurrentPlayer.transform.position;
		if (this.MG == null)
		{
			this.MG = (Minigun)Object.FindObjectOfType(typeof(Minigun));
		}
		if (this.MG != null)
		{
			this.MG.speedUp = false;
		}
		GameObject head = this.csrm.CreatePlayerRagDoll(this.BotsGmObj[victimid], this.BotsGmObj[attackerid], victimid, true, 0, 0, weaponid, false, false, false, false, false, false, false, false);
		this.CurrentPlayer.GetComponentInChildren<vp_FPCamera>().enabled = false;
		this.SkinManager.SpawnCamera(head);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0001C118 File Offset: 0x0001A318
	public void SetCurrentWeapon(int id, int weaponid)
	{
		for (int i = 0; i < 350; i++)
		{
			if (this.Bots[id].weapon[i])
			{
				this.Bots[id].weapon[i].SetActive(false);
			}
			if (this.Bots[id].flash[i] && i != 315)
			{
				this.Bots[id].flash[i].SetActive(false);
			}
		}
		this.Bots[id].WeaponID = weaponid;
		if (this.cscl && this.cscl.myindex == id)
		{
			return;
		}
		if (this.Bots[id].weapon[weaponid])
		{
			this.Bots[id].weapon[weaponid].SetActive(true);
		}
		if (weaponid == 0 && id != PlayerProfile.myindex && ConnectionInfo.mode != CONST.CFG.BUILD_MODE)
		{
			int num = 0;
			if (this.Bots[id].Item[198] > 0)
			{
				num = 4;
			}
			this.csig.SetBlockTextureTeam(this.Bots[id].m_Face, this.Bots[id].m_Top, (int)this.Bots[id].Team + num, false);
			return;
		}
		if (id != PlayerProfile.myindex && ConnectionInfo.mode == CONST.CFG.BUILD_MODE)
		{
			this.csig.SetBlockTextureForBuild(this.Bots[id].m_Face, this.Bots[id].m_Top, 0);
			this.Bots[id].blockFlag = 0;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0001C297 File Offset: 0x0001A497
	public void SetCurrentWeaponBlock(int id, int flag)
	{
		if (flag < 0)
		{
			flag = 0;
		}
		this.csig.SetBlockTextureForBuild(this.Bots[id].m_Face, this.Bots[id].m_Top, flag);
		this.Bots[id].blockFlag = flag;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
	private static BlockDirection GetDirection(Vector3 dir)
	{
		if (Mathf.Abs(dir.z) >= Mathf.Abs(dir.x))
		{
			if (dir.z >= 0f)
			{
				return BlockDirection.Z_PLUS;
			}
			return BlockDirection.Z_MINUS;
		}
		else
		{
			if (dir.x >= 0f)
			{
				return BlockDirection.X_PLUS;
			}
			return BlockDirection.X_MINUS;
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0001C310 File Offset: 0x0001A510
	public void PhysicsBlock(List<Vector3i> pos)
	{
		if (pos.Count == 0)
		{
			return;
		}
		Vector3 vector = Camera.main.transform.position - pos[0];
		if (vector.magnitude <= 128f)
		{
			GameObject gameObject = new GameObject("destroyed", new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			MeshFilter component = gameObject.GetComponent<MeshFilter>();
			BlockSet blockSet = this.map.GetBlockSet();
			MeshBuilder meshBuilder = new MeshBuilder();
			meshBuilder.Clear();
			foreach (Vector3i vector3i in pos)
			{
				CubeBuilder.Build(vector3i, vector3i, this.map, meshBuilder, false);
			}
			component.sharedMesh = meshBuilder.ToMesh(component.sharedMesh);
			if (component.sharedMesh == null)
			{
				Object.Destroy(gameObject);
				return;
			}
			gameObject.GetComponent<Renderer>().sharedMaterials = blockSet.GetMaterials(component.sharedMesh.subMeshCount);
			gameObject.AddComponent<dk>();
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().interpolation = 1;
			if (pos.Count < 32 && vector.magnitude < 64f)
			{
				gameObject.AddComponent<BoxCollider>();
				gameObject.layer = 8;
				gameObject.GetComponent<Rigidbody>().AddTorque(gameObject.transform.right * 40f + gameObject.transform.forward * 20f);
			}
		}
		foreach (Vector3i vector3i2 in pos)
		{
			base.StartCoroutine(this.UpdateBlock_coroutine(vector3i2.x, vector3i2.y, vector3i2.z, 0, false, true));
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0001C508 File Offset: 0x0001A708
	public void CreateFX(float pX, float pY, float pZ)
	{
		new GameObject("destroyed_fx", new Type[]
		{
			typeof(Explode)
		}).transform.position = new Vector3(pX, pY, pZ);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00002B75 File Offset: 0x00000D75
	public void SetController(int id, int cid)
	{
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0001C539 File Offset: 0x0001A739
	public void SetZombie()
	{
		this.csws.SetZombieWeapon();
	}

	// Token: 0x04000149 RID: 329
	public static BotsController Instance;

	// Token: 0x0400014A RID: 330
	public GameObject pgoPlayer;

	// Token: 0x0400014B RID: 331
	public GameObject pgoLocalPlayer;

	// Token: 0x0400014C RID: 332
	public GameObject pgoPlayerCreated;

	// Token: 0x0400014D RID: 333
	private GameObject CurrentPlayer;

	// Token: 0x0400014E RID: 334
	private GameObject Gui;

	// Token: 0x0400014F RID: 335
	public GameObject[] BotsGmObj;

	// Token: 0x04000150 RID: 336
	public BotData[] Bots = new BotData[32];

	// Token: 0x04000151 RID: 337
	private Block[] teamblock = new Block[8];

	// Token: 0x04000152 RID: 338
	private bool PlayersLoaded;

	// Token: 0x04000153 RID: 339
	private PackData pd;

	// Token: 0x04000154 RID: 340
	private SpawnManager SkinManager;

	// Token: 0x04000155 RID: 341
	private RagDollManager csrm;

	// Token: 0x04000156 RID: 342
	private MainGUI csig;

	// Token: 0x04000157 RID: 343
	private Map map;

	// Token: 0x04000158 RID: 344
	private Client cscl;

	// Token: 0x04000159 RID: 345
	private Minigun MG;

	// Token: 0x0400015A RID: 346
	private WeaponSystem csws;

	// Token: 0x0400015B RID: 347
	private float zmupdate;

	// Token: 0x0400015C RID: 348
	private float lastupdate;
}
