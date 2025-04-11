using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class DeathMsg : MonoBehaviour
{
	// Token: 0x06000370 RID: 880 RVA: 0x0003DE54 File Offset: 0x0003C054
	private void Awake()
	{
		this.Map = GameObject.Find("Map");
		this.lastupdate = Time.time;
		for (int i = 0; i < 4; i++)
		{
			this.d_attacker[i] = "";
			this.d_victim[i] = "";
		}
		this.gui_style2 = new GUIStyle();
		this.gui_style2.font = FontManager.font[2];
		this.gui_style2.fontSize = 26;
		this.gui_style2.normal.textColor = new Color(1f, 1f, 1f, 1f);
		this.hudmsg = ContentLoader.LoadTexture("hudmsg");
		this.weapon_ntw20 = ContentLoader.LoadTexture("weapon_ntw20");
		this.weapon_vss_desert = ContentLoader.LoadTexture("weapon_vss_desert");
		this.weapon_tank_light = ContentLoader.LoadTexture("weapon_tank_light");
		this.weapon_tank_medium = ContentLoader.LoadTexture("weapon_tank_medium");
		this.weapon_tank_heavy = ContentLoader.LoadTexture("weapon_tank_heavy");
		this.weapon_minigun = ContentLoader.LoadTexture("weapon_minigun");
		this.weapon_minefly = ContentLoader.LoadTexture("weapon_minefly");
		this.weapon_javelin = ContentLoader.LoadTexture("weapon_javelin");
		this.weapon_zaa12 = ContentLoader.LoadTexture("weapon_zaa12");
		this.weapon_zasval = ContentLoader.LoadTexture("weapon_zasval");
		this.weapon_zfn57 = ContentLoader.LoadTexture("weapon_zfn57");
		this.weapon_zkord = ContentLoader.LoadTexture("weapon_zkord");
		this.weapon_zm249 = ContentLoader.LoadTexture("weapon_zm249");
		this.weapon_zminigun = ContentLoader.LoadTexture("weapon_zminigun");
		this.weapon_zsps12 = ContentLoader.LoadTexture("weapon_zsps12");
		this.weapon_mk3 = ContentLoader.LoadTexture("weapon_mk3");
		this.weapon_rkg3 = ContentLoader.LoadTexture("weapon_rkg3");
		this.weapon_tube = ContentLoader.LoadTexture("weapon_tube");
		this.weapon_bulava = ContentLoader.LoadTexture("weapon_bulava");
		this.weapon_katana = ContentLoader.LoadTexture("weapon_katana");
		this.weapon_crossbow = ContentLoader.LoadTexture("weapon_crossbow");
		this.weapon_mauzer = ContentLoader.LoadTexture("weapon_mauzer");
		this.weapon_qbz95 = ContentLoader.LoadTexture("weapon_qbz95");
		this.weapon_mine = ContentLoader.LoadTexture("weapon_mine");
		this.weapon_c4 = ContentLoader.LoadTexture("weapon_c4");
		this.weapon_chopper = ContentLoader.LoadTexture("weapon_chopper");
		this.weapon_shield = ContentLoader.LoadTexture("weapon_shield");
		this.weapon_aksu = ContentLoader.LoadTexture("weapon_aksu");
		this.weapon_m700 = ContentLoader.LoadTexture("weapon_m700");
		this.weapon_stechkin = ContentLoader.LoadTexture("weapon_stechkin");
		this.weapon_at_mine = ContentLoader.LoadTexture("TankMine_kills");
		this.weapon_molotov = ContentLoader.LoadTexture("molotov_kills");
		this.weapon_m202 = ContentLoader.LoadTexture("m202_kills");
		this.weapon_gg = ContentLoader.LoadTexture("gas_gren_kill");
		this.weapon_dpm = ContentLoader.LoadTexture("dpmg_kills");
		this.weapon_m1924 = ContentLoader.LoadTexture("mac1924_kills");
		this.weapon_mg42 = ContentLoader.LoadTexture("mg42_kills");
		this.weapon_sten = ContentLoader.LoadTexture("stenmk2_kills");
		this.weapon_m1a1 = ContentLoader.LoadTexture("m1a1_kills");
		this.weapon_type99 = ContentLoader.LoadTexture("type99_kills");
		this.weapon_jeep = ContentLoader.LoadTexture("humvee");
		this.weapon_bizon = ContentLoader.LoadTexture("weapon_bizon");
		this.weapon_pila = ContentLoader.LoadTexture("weapon_pila");
		this.weapon_groza = ContentLoader.LoadTexture("weapon_groza");
		this.weapon_jackhammer = ContentLoader.LoadTexture("weapon_jackhammer");
		this.weapon_tykva = ContentLoader.LoadTexture("weapon_tykva");
		this.weapon_psg_1 = ContentLoader.LoadTexture("weapon_psg_1");
		this.weapon_krytac = ContentLoader.LoadTexture("weapon_krytac");
		this.weapon_mp5sd = ContentLoader.LoadTexture("weapon_mp5sd");
		this.weapon_colts = ContentLoader.LoadTexture("weapon_colts");
		this.weapon_jackhammer_lady = ContentLoader.LoadTexture("weapon_jackhammer_lady");
		this.weapon_m700_lady = ContentLoader.LoadTexture("weapon_m700_lady");
		this.weapon_mg42_lady = ContentLoader.LoadTexture("weapon_mg42_lady");
		this.weapon_magnum_lady = ContentLoader.LoadTexture("weapon_magnum_lady");
		this.weapon_scorpion = ContentLoader.LoadTexture("weapon_scorpion");
		this.weapon_g36c_veteran = ContentLoader.LoadTexture("weapon_g36c_veteran");
		this.weapon_snowball = ContentLoader.LoadTexture("weapon_snowball");
		this.weapon_fmg9 = ContentLoader.LoadTexture("weapon_fmg9");
		this.weapon_saiga = ContentLoader.LoadTexture("weapon_saiga");
		this.weapon_flamethrower = ContentLoader.LoadTexture("weapon_flamethrower");
		this.weapon_ak47_snow = ContentLoader.LoadTexture("weapon_ak47_snow");
		this.weapon_p90_snow = ContentLoader.LoadTexture("weapon_p90_snow");
		this.weapon_saiga_snow = ContentLoader.LoadTexture("weapon_saiga_snow");
		this.weapon_sr25_snow = ContentLoader.LoadTexture("weapon_sr25_snow");
		this.weapon_usp_snow = ContentLoader.LoadTexture("weapon_usp_snow");
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0003E318 File Offset: 0x0003C518
	private void OnGUI()
	{
		for (int i = 0; i < 4; i++)
		{
			if (!(this.d_attacker[i] == "") && !(this.d_victim[i] == ""))
			{
				float num = GUIManager.YRES(132f) + 8f;
				if (this.d_attacker[i] != "^8WORLD")
				{
					num += GUIManager.DrawColorText(num, (float)(10 + i * 20), this.d_attacker[i], 3);
				}
				num += 4f;
				ITEM item = (ITEM)this.d_weaponid[i];
				if (item <= ITEM.COLTS)
				{
					switch (item)
					{
					case ITEM.AK47:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 55f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.8671875f, 0.21484375f, 0.0625f));
						num += 55f;
						break;
					case ITEM.SVD:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 63f, 16.5f), this.hudmsg, new Rect(0.22265625f, 0.8671875f, 0.24609375f, 0.0625f));
						num += 63f;
						break;
					case ITEM.SKIN_SOLDIER:
					case ITEM.SKIN_GHOST:
					case ITEM.PREMIUM:
					case ITEM.SKIN_SAS:
					case ITEM.SKIN_TERROR:
					case ITEM.SKIN_INSURGENT:
					case ITEM.SKIN_PILOT:
					case ITEM.SKIN_REBEL:
					case ITEM.SKIN_RIDER:
					case ITEM.SKIN_SNIPER:
					case ITEM.SKIN_SPEC:
					case ITEM.SKIN_STALKER:
					case ITEM.SKIN_USMC:
					case ITEM.SKIN_DESANT:
					case ITEM.SKIN_CASPER:
					case ITEM.SKIN_JACK:
					case ITEM.SKIN_SLENDER:
					case ITEM.SKIN_DEFAULT:
					case ITEM.MEDKIT_S:
					case ITEM.MEDKIT_M:
					case ITEM.MEDKIT_L:
					case (ITEM)39:
					case ITEM.SKIN_ARCTIC:
					case ITEM.SKIN_1337:
					case ITEM.VEST:
					case ITEM.SKIN_ELF:
					case ITEM.SKIN_SNOWMAN:
					case ITEM.SKIN_SANTAGIRL:
					case ITEM.SKIN_SANTA:
					case (ITEM)63:
					case ITEM.SKIN_BOMBER:
					case ITEM.SKIN_SURVIVOR:
					case ITEM.SKIN_BANDIT:
					case ITEM.SKIN_MERCENARY:
					case ITEM.SKIN_PRISONER:
					case ITEM.SKIN_MERCGIRL:
					case ITEM.MODE_CONTRA:
					case ITEM.SKIN_KILLER:
					case ITEM.SKIN_COP:
					case ITEM.SKIN_BLOKADOVEC:
					case ITEM.SKIN_SOVIET:
					case ITEM.SKIN_GERMAN:
					case (ITEM)92:
					case ITEM.SKIN_SF1000:
					case ITEM.SKIN_SF1207:
					case ITEM.SKIN_SF1122:
					case ITEM.SKIN_FREDDY:
					case ITEM.SKIN_JASON:
					case ITEM.SKIN_RORSCHACH:
					case ITEM.SKIN_ZBOY:
					case ITEM.SKIN_ZGIRL:
					case ITEM.SKIN_LT_DESERT:
					case ITEM.SKIN_LT_HEXAGON:
					case ITEM.SKIN_LT_MULTICAM:
					case ITEM.SKIN_LT_TIGER:
					case ITEM.SKIN_LT_WOOD:
					case ITEM.SKIN_MT_DESERT:
					case ITEM.SKIN_MT_HEXAGON:
					case ITEM.SKIN_MT_MULTICAM:
					case ITEM.SKIN_MT_TIGER:
					case ITEM.SKIN_MT_WOOD:
					case ITEM.SKIN_HT_DESERT:
					case ITEM.SKIN_HT_HEXAGON:
					case ITEM.SKIN_HT_MULTICAM:
					case ITEM.SKIN_HT_TIGER:
					case ITEM.SKIN_HT_WOOD:
					case ITEM.ZBK18M:
					case ITEM.ZOF26:
					case ITEM.VEHICLE_REPAIR_KIT:
					case ITEM.TANK_MG:
					case ITEM.HELMETPLUS:
					case ITEM.VESTPLUS:
					case ITEM.SKIN_BELSNICKEL:
					case ITEM.SKIN_RABBIT_BOY:
					case ITEM.SKIN_RABBIT_GIRL:
					case ITEM.SKIN_LT_WINTER:
					case ITEM.SKIN_MT_WINTER:
					case ITEM.SKIN_HT_WINTER:
					case (ITEM)154:
					case (ITEM)155:
					case (ITEM)156:
					case ITEM.SKIN_ANARCHIST:
					case ITEM.SKIN_REBELTERROR:
					case ITEM.SKIN_MARINE:
					case ITEM.SKIN_VMF:
					case ITEM.SKIN_VVS:
					case ITEM.M18:
					case ITEM.SKIN_APRIL:
					case ITEM.SKIN_MULATKA:
					case ITEM.SKIN_UBIVASHKA:
					case ITEM.SKIN_TERRORISTKA:
					case ITEM.SKIN_TOXIK:
					case ITEM.WEAPONSMEGAPACK:
						break;
					case ITEM.M61:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 13f, 16.5f), this.hudmsg, new Rect(0.47265625f, 0.8671875f, 0.05078125f, 0.0625f));
						num += 13f;
						break;
					case ITEM.DEAGLE:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 20f, 16.5f), this.hudmsg, new Rect(0.66796875f, 0.8671875f, 0.078125f, 0.0625f));
						num += 20f;
						break;
					case ITEM.SHMEL:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 58f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.80078125f, 0.2265625f, 0.0625f));
						num += 58f;
						break;
					case ITEM.ASVAL:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 56f, 16.5f), this.hudmsg, new Rect(0.234375f, 0.80078125f, 0.21875f, 0.0625f));
						num += 56f;
						break;
					case ITEM.G36C:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 42f, 16.5f), this.hudmsg, new Rect(0.45703125f, 0.80078125f, 0.1640625f, 0.0625f));
						num += 42f;
						break;
					case ITEM.KRISS:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 41f, 16.5f), this.hudmsg, new Rect(0.62109375f, 0.80078125f, 0.16015625f, 0.0625f));
						num += 41f;
						break;
					case ITEM.M4A1:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 57f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.734375f, 0.22265625f, 0.0625f));
						num += 57f;
						break;
					case ITEM.M249:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 52f, 16.5f), this.hudmsg, new Rect(0.46875f, 0.734375f, 0.203125f, 0.0625f));
						num += 52f;
						break;
					case ITEM.SPAS12:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 51f, 16.5f), this.hudmsg, new Rect(0.75f, 0.8671875f, 0.19921875f, 0.0625f));
						num += 51f;
						break;
					case ITEM.VINTOREZ:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 60f, 16.5f), this.hudmsg, new Rect(0.23046875f, 0.734375f, 0.234375f, 0.0625f));
						num += 60f;
						break;
					case ITEM.VSK94:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 52f, 16.5f), this.hudmsg, new Rect(0.7890625f, 0.80078125f, 0.203125f, 0.0625f));
						num += 52f;
						break;
					case ITEM.SHOVEL:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 37f, 16.5f), this.hudmsg, new Rect(0.1640625f, 0.93359375f, 0.14453125f, 0.0625f));
						num += 37f;
						break;
					case ITEM.L96A1MOD:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 75f, 16.5f), this.hudmsg, new Rect(0.140625f, 0.3359375f, 0.29296875f, 0.0625f));
						num += 75f;
						break;
					case ITEM.ZOMBIE:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 16f, 16.5f), this.hudmsg, new Rect(0.5234375f, 0.8671875f, 0.0625f, 0.0625f));
						num += 16f;
						break;
					case ITEM.USP:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 31f, 16.5f), this.hudmsg, new Rect(0.67578125f, 0.734375f, 0.12109375f, 0.0625f));
						num += 31f;
						break;
					case ITEM.M3:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 60f, 16.5f), this.hudmsg, new Rect(0.3125f, 0.93359375f, 0.234375f, 0.0625f));
						num += 60f;
						break;
					case ITEM.M14:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 63f, 16.5f), this.hudmsg, new Rect(0.55078125f, 0.93359375f, 0.24609375f, 0.0625f));
						num += 63f;
						break;
					case ITEM.MP5:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 33f, 16.5f), this.hudmsg, new Rect(0.80078125f, 0.93359375f, 0.12890625f, 0.0625f));
						num += 33f;
						break;
					case ITEM.GLOCK:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 18f, 16.5f), this.hudmsg, new Rect(0.59375f, 0.8671875f, 0.0703125f, 0.0625f));
						num += 18f;
						break;
					case ITEM.BARRETT:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 64f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.66796875f, 0.25f, 0.0625f));
						num += 64f;
						break;
					case ITEM.TMP:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 24f, 16.5f), this.hudmsg, new Rect(0.80078125f, 0.734375f, 0.09375f, 0.0625f));
						num += 24f;
						break;
					case ITEM.KNIFE:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 19f, 16.5f), this.hudmsg, new Rect(0.7890625f, 0.66796875f, 0.07421875f, 0.0625f));
						num += 19f;
						break;
					case ITEM.AXE:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 31f, 16.5f), this.hudmsg, new Rect(0.3828125f, 0.66796875f, 0.12109375f, 0.0625f));
						num += 31f;
						break;
					case ITEM.BAT:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 34f, 16.5f), this.hudmsg, new Rect(0.65234375f, 0.66796875f, 0.1328125f, 0.0625f));
						num += 34f;
						break;
					case ITEM.CROWBAR:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 36f, 16.5f), this.hudmsg, new Rect(0.5078125f, 0.66796875f, 0.140625f, 0.0625f));
						num += 36f;
						break;
					case ITEM.CARAMEL:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 31f, 16.5f), this.hudmsg, new Rect(0.2578125f, 0.66796875f, 0.12109375f, 0.0625f));
						num += 31f;
						break;
					case ITEM.TNT:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 16f, 16.5f), this.hudmsg, new Rect(0.8671875f, 0.66796875f, 0.0625f, 0.0625f));
						num += 16f;
						break;
					case ITEM.AUGA3:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 44f, 16.5f), this.hudmsg, new Rect(0.203125f, 0.6015625f, 0.171875f, 0.0625f));
						num += 44f;
						break;
					case ITEM.SG552:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 50f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.6015625f, 0.1953125f, 0.0625f));
						num += 50f;
						break;
					case ITEM.MORTAR:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_minefly.width / this.weapon_minefly.height), 16f), this.weapon_minefly);
						num += (float)(16 * this.weapon_minefly.width / this.weapon_minefly.height);
						break;
					case ITEM.M14EBR:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 63f, 16.5f), this.hudmsg, new Rect(0.5390625f, 0.6015625f, 0.24609375f, 0.0625f));
						num += 63f;
						break;
					case ITEM.L96A1:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 62f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.53515625f, 0.2421875f, 0.0625f));
						num += 62f;
						break;
					case ITEM.NOVA:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 62f, 16.5f), this.hudmsg, new Rect(0.64453125f, 0.53515625f, 0.2421875f, 0.0625f));
						num += 62f;
						break;
					case ITEM.KORD:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 55f, 16.5f), this.hudmsg, new Rect(0.42578125f, 0.53515625f, 0.21484375f, 0.0625f));
						num += 55f;
						break;
					case ITEM.ANACONDA:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 27f, 16.5f), this.hudmsg, new Rect(0.890625f, 0.53515625f, 0.10546875f, 0.0625f));
						num += 27f;
						break;
					case ITEM.SCAR_H:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 60f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.46875f, 0.234375f, 0.0625f));
						num += 60f;
						break;
					case ITEM.P90:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 44f, 16.5f), this.hudmsg, new Rect(0.25f, 0.53515625f, 0.171875f, 0.0625f));
						num += 44f;
						break;
					case ITEM.GP:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 16f, 16.5f), this.hudmsg, new Rect(0.38671875f, 0.40234375f, 0.0625f, 0.0625f));
						num += 16f;
						break;
					case ITEM.RPK:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 64f, 16.5f), this.hudmsg, new Rect(0.2421875f, 0.46875f, 0.25f, 0.0625f));
						num += 64f;
						break;
					case ITEM.HK416:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 47f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.40234375f, 0.18359375f, 0.0625f));
						num += 47f;
						break;
					case ITEM.AK102:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 53f, 16.5f), this.hudmsg, new Rect(0.75f, 0.46875f, 0.20703125f, 0.0625f));
						num += 53f;
						break;
					case ITEM.SR25:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 64f, 16.5f), this.hudmsg, new Rect(0.49609375f, 0.46875f, 0.25f, 0.0625f));
						num += 64f;
						break;
					case ITEM.MGLMK1:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 49f, 16.5f), this.hudmsg, new Rect(0.19140625f, 0.40234375f, 0.19140625f, 0.0625f));
						num += 49f;
						break;
					case ITEM.MOSIN:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 63f, 16.5f), this.hudmsg, new Rect(0.453125f, 0.40234375f, 0.24609375f, 0.0625f));
						num += 63f;
						break;
					case ITEM.PPSH:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 64f, 16.5f), this.hudmsg, new Rect(0.703125f, 0.40234375f, 0.25f, 0.0625f));
						num += 64f;
						break;
					case ITEM.MP40:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 34f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.3359375f, 0.1328125f, 0.0625f));
						num += 34f;
						break;
					case ITEM.KACPDW:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 45f, 16.5f), this.hudmsg, new Rect(0.4375f, 0.3359375f, 0.17578125f, 0.0625f));
						num += 45f;
						break;
					case ITEM.FAMAS:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 53f, 16.5f), this.hudmsg, new Rect(0.6171875f, 0.3359375f, 0.20703125f, 0.0625f));
						num += 53f;
						break;
					case ITEM.BERETTA:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 18f, 16.5f), this.hudmsg, new Rect(0.828125f, 0.3359375f, 0.0703125f, 0.0625f));
						num += 18f;
						break;
					case ITEM.MACHETE:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 30f, 16.5f), this.hudmsg, new Rect(0.7890625f, 0.6015625f, 0.1171875f, 0.0625f));
						num += 30f;
						break;
					case ITEM.RPG:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 62f, 16.5f), this.hudmsg, new Rect(0.1171875f, 0.26953125f, 0.2421875f, 0.0625f));
						num += 62f;
						break;
					case ITEM.WRENCH:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 33f, 16.5f), this.hudmsg, new Rect(0.36328125f, 0.26953125f, 0.12890625f, 0.0625f));
						num += 33f;
						break;
					case ITEM.AA12:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 62f, 20.5f), this.hudmsg, new Rect(0.5f, 0.25f, 0.2421875f, 0.078125f));
						num += 62f;
						break;
					case ITEM.FN57:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 30f, 16.5f), this.hudmsg, new Rect(0.7421875f, 0.26171875f, 0.1171875f, 0.0625f));
						num += 30f;
						break;
					case ITEM.FS2000:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 48f, 24.5f), this.hudmsg, new Rect(0.00390625f, 0.1640625f, 0.1875f, 0.09375f));
						num += 48f;
						break;
					case ITEM.L85:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 56f, 16.5f), this.hudmsg, new Rect(0.19140625f, 0.171875f, 0.21875f, 0.08203125f));
						num += 56f;
						break;
					case ITEM.MAC10:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 22f, 21.5f), this.hudmsg, new Rect(0.859375f, 0.26953125f, 0.0859375f, 0.0625f));
						num += 22f;
						break;
					case ITEM.PKP:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 62f, 16.5f), this.hudmsg, new Rect(0.4140625f, 0.171875f, 0.2421875f, 0.0625f));
						num += 62f;
						break;
					case ITEM.PM:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 16f, 16.5f), this.hudmsg, new Rect(0.66015625f, 0.171875f, 0.06640625f, 0.0625f));
						num += 16f;
						break;
					case ITEM.TAR21:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 45f, 20.5f), this.hudmsg, new Rect(0.73046875f, 0.16796875f, 0.17578125f, 0.078125f));
						num += 45f;
						break;
					case ITEM.UMP45:
						GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 47f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.1015625f, 0.18359375f, 0.0625f));
						num += 47f;
						break;
					case ITEM.NTW20:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(18 * this.weapon_ntw20.width / this.weapon_ntw20.height), 18f), this.weapon_ntw20);
						num += (float)(18 * this.weapon_ntw20.width / this.weapon_ntw20.height);
						break;
					case ITEM.VINTOREZ_DESERT:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_vss_desert.width / this.weapon_vss_desert.height), 16f), this.weapon_vss_desert);
						num += (float)(16 * this.weapon_vss_desert.width / this.weapon_vss_desert.height);
						break;
					case ITEM.MINIGUN:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_minigun.width / this.weapon_minigun.height), 16f), this.weapon_minigun);
						num += (float)(16 * this.weapon_minigun.width / this.weapon_minigun.height);
						break;
					case ITEM.JAVELIN:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_javelin.width / this.weapon_javelin.height), 16f), this.weapon_javelin);
						num += (float)(16 * this.weapon_javelin.width / this.weapon_javelin.height);
						break;
					case ITEM.ZAA12:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zaa12.width / this.weapon_zaa12.height), 16f), this.weapon_zaa12);
						num += (float)(16 * this.weapon_zaa12.width / this.weapon_zaa12.height);
						break;
					case ITEM.ZASVAL:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zasval.width / this.weapon_zasval.height), 16f), this.weapon_zasval);
						num += (float)(16 * this.weapon_zasval.width / this.weapon_zasval.height);
						break;
					case ITEM.ZFN57:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zfn57.width / this.weapon_zfn57.height), 16f), this.weapon_zfn57);
						num += (float)(16 * this.weapon_zfn57.width / this.weapon_zfn57.height);
						break;
					case ITEM.ZKORD:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zkord.width / this.weapon_zkord.height), 16f), this.weapon_zkord);
						num += (float)(16 * this.weapon_zkord.width / this.weapon_zkord.height);
						break;
					case ITEM.ZM249:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zm249.width / this.weapon_zm249.height), 16f), this.weapon_zm249);
						num += (float)(16 * this.weapon_zm249.width / this.weapon_zm249.height);
						break;
					case ITEM.ZMINIGUN:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zminigun.width / this.weapon_zminigun.height), 16f), this.weapon_zminigun);
						num += (float)(16 * this.weapon_zminigun.width / this.weapon_zminigun.height);
						break;
					case ITEM.ZSPAS12:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_zsps12.width / this.weapon_zsps12.height), 16f), this.weapon_zsps12);
						num += (float)(16 * this.weapon_zsps12.width / this.weapon_zsps12.height);
						break;
					case ITEM.TUBE:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_tube.width / this.weapon_tube.height), 16f), this.weapon_tube);
						num += (float)(16 * this.weapon_tube.width / this.weapon_tube.height);
						break;
					case ITEM.BULAVA:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_bulava.width / this.weapon_bulava.height), 16f), this.weapon_bulava);
						num += (float)(16 * this.weapon_bulava.width / this.weapon_bulava.height);
						break;
					case ITEM.KATANA:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_katana.width / this.weapon_katana.height), 16f), this.weapon_katana);
						num += (float)(16 * this.weapon_katana.width / this.weapon_katana.height);
						break;
					case ITEM.MAUZER:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mauzer.width / this.weapon_mauzer.height), 16f), this.weapon_mauzer);
						num += (float)(16 * this.weapon_mauzer.width / this.weapon_mauzer.height);
						break;
					case ITEM.CROSSBOW:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_crossbow.width / this.weapon_crossbow.height), 16f), this.weapon_crossbow);
						num += (float)(16 * this.weapon_crossbow.width / this.weapon_crossbow.height);
						break;
					case ITEM.QBZ95:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_qbz95.width / this.weapon_qbz95.height), 16f), this.weapon_qbz95);
						num += (float)(16 * this.weapon_qbz95.width / this.weapon_qbz95.height);
						break;
					case ITEM.MK3:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mk3.width / this.weapon_mk3.height), 16f), this.weapon_mk3);
						num += (float)(16 * this.weapon_mk3.width / this.weapon_mk3.height);
						break;
					case ITEM.RKG3:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_rkg3.width / this.weapon_rkg3.height), 16f), this.weapon_rkg3);
						num += (float)(16 * this.weapon_rkg3.width / this.weapon_rkg3.height);
						break;
					case ITEM.MINE:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mine.width / this.weapon_mine.height), 16f), this.weapon_mine);
						num += (float)(16 * this.weapon_mine.width / this.weapon_mine.height);
						break;
					case ITEM.C4:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_c4.width / this.weapon_c4.height), 16f), this.weapon_c4);
						num += (float)(16 * this.weapon_c4.width / this.weapon_c4.height);
						break;
					case ITEM.CHOPPER:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_chopper.width / this.weapon_chopper.height), 16f), this.weapon_chopper);
						num += (float)(16 * this.weapon_chopper.width / this.weapon_chopper.height);
						break;
					case ITEM.SHIELD:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_shield.width / this.weapon_shield.height), 16f), this.weapon_shield);
						num += (float)(16 * this.weapon_shield.width / this.weapon_shield.height);
						break;
					case ITEM.AKSU:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_aksu.width / this.weapon_aksu.height), 16f), this.weapon_aksu);
						num += (float)(16 * this.weapon_aksu.width / this.weapon_aksu.height);
						break;
					case ITEM.M700:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_m700.width / this.weapon_m700.height), 16f), this.weapon_m700);
						num += (float)(16 * this.weapon_m700.width / this.weapon_m700.height);
						break;
					case ITEM.STECHKIN:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_stechkin.width / this.weapon_stechkin.height), 16f), this.weapon_stechkin);
						num += (float)(16 * this.weapon_stechkin.width / this.weapon_stechkin.height);
						break;
					case ITEM.AT_MINE:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_at_mine.width / this.weapon_at_mine.height), 16f), this.weapon_at_mine);
						num += (float)(16 * this.weapon_at_mine.width / this.weapon_at_mine.height);
						break;
					case ITEM.MOLOTOV:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_molotov.width / this.weapon_molotov.height), 16f), this.weapon_molotov);
						num += (float)(16 * this.weapon_molotov.width / this.weapon_molotov.height);
						break;
					case ITEM.M202:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_m202.width / this.weapon_m202.height), 16f), this.weapon_m202);
						num += (float)(16 * this.weapon_m202.width / this.weapon_m202.height);
						break;
					case ITEM.M7A2:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_gg.width / this.weapon_gg.height), 16f), this.weapon_gg);
						num += (float)(16 * this.weapon_gg.width / this.weapon_gg.height);
						break;
					case ITEM.DPM:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_dpm.width / this.weapon_dpm.height), 16f), this.weapon_dpm);
						num += (float)(16 * this.weapon_dpm.width / this.weapon_dpm.height);
						break;
					case ITEM.M1924:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_m1924.width / this.weapon_m1924.height), 16f), this.weapon_m1924);
						num += (float)(16 * this.weapon_m1924.width / this.weapon_m1924.height);
						break;
					case ITEM.MG42:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mg42.width / this.weapon_mg42.height), 16f), this.weapon_mg42);
						num += (float)(16 * this.weapon_mg42.width / this.weapon_mg42.height);
						break;
					case ITEM.STEN_MK2:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_sten.width / this.weapon_sten.height), 16f), this.weapon_sten);
						num += (float)(16 * this.weapon_sten.width / this.weapon_sten.height);
						break;
					case ITEM.M1A1:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_m1a1.width / this.weapon_m1a1.height), 16f), this.weapon_m1a1);
						num += (float)(16 * this.weapon_m1a1.width / this.weapon_m1a1.height);
						break;
					case ITEM.TYPE99:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_type99.width / this.weapon_type99.height), 16f), this.weapon_type99);
						num += (float)(16 * this.weapon_type99.width / this.weapon_type99.height);
						break;
					default:
						switch (item)
						{
						case ITEM.BIZON:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_bizon.width / this.weapon_bizon.height), 16f), this.weapon_bizon);
							num += (float)(16 * this.weapon_bizon.width / this.weapon_bizon.height);
							break;
						case ITEM.GROZA:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_groza.width / this.weapon_groza.height), 16f), this.weapon_groza);
							num += (float)(16 * this.weapon_groza.width / this.weapon_groza.height);
							break;
						case ITEM.JACKHAMMER:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_jackhammer.width / this.weapon_jackhammer.height), 16f), this.weapon_jackhammer);
							num += (float)(16 * this.weapon_jackhammer.width / this.weapon_jackhammer.height);
							break;
						case ITEM.CHAINSAW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_pila.width / this.weapon_pila.height), 16f), this.weapon_pila);
							num += (float)(16 * this.weapon_pila.width / this.weapon_pila.height);
							break;
						case ITEM.PSG_1:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_psg_1.width / this.weapon_psg_1.height), 16f), this.weapon_psg_1);
							num += (float)(16 * this.weapon_psg_1.width / this.weapon_psg_1.height);
							break;
						case ITEM.KRYTAC:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_krytac.width / this.weapon_krytac.height), 16f), this.weapon_krytac);
							num += (float)(16 * this.weapon_krytac.width / this.weapon_krytac.height);
							break;
						case ITEM.MP5SD:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mp5sd.width / this.weapon_mp5sd.height), 16f), this.weapon_mp5sd);
							num += (float)(16 * this.weapon_mp5sd.width / this.weapon_mp5sd.height);
							break;
						case ITEM.COLTS:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_colts.width / this.weapon_colts.height), 16f), this.weapon_colts);
							num += (float)(16 * this.weapon_colts.width / this.weapon_colts.height);
							break;
						}
						break;
					}
				}
				else if (item != ITEM.DEFAULT_DEATH)
				{
					switch (item)
					{
					case ITEM.JACKHAMMER_LADY:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_jackhammer_lady.width / this.weapon_jackhammer_lady.height), 16f), this.weapon_jackhammer_lady);
						num += (float)(16 * this.weapon_jackhammer_lady.width / this.weapon_jackhammer_lady.height);
						break;
					case ITEM.M700_LADY:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_m700_lady.width / this.weapon_m700_lady.height), 16f), this.weapon_m700_lady);
						num += (float)(16 * this.weapon_m700_lady.width / this.weapon_m700_lady.height);
						break;
					case ITEM.MG42_LADY:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_mg42_lady.width / this.weapon_mg42_lady.height), 16f), this.weapon_mg42_lady);
						num += (float)(16 * this.weapon_mg42_lady.width / this.weapon_mg42_lady.height);
						break;
					case ITEM.SHIELD_LADY:
					case ITEM.SKIN_SAPPER:
					case (ITEM)307:
					case (ITEM)310:
					case ITEM.SKIN_SILENTASSASSIN:
					case ITEM.SKIN_GRINCH:
						break;
					case ITEM.MAGNUM_LADY:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_magnum_lady.width / this.weapon_magnum_lady.height), 16f), this.weapon_magnum_lady);
						num += (float)(16 * this.weapon_magnum_lady.width / this.weapon_magnum_lady.height);
						break;
					case ITEM.SCORPION:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_scorpion.width / this.weapon_scorpion.height), 16f), this.weapon_scorpion);
						num += (float)(16 * this.weapon_scorpion.width / this.weapon_scorpion.height);
						break;
					case ITEM.G36C_VETERAN:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_g36c_veteran.width / this.weapon_g36c_veteran.height), 16f), this.weapon_g36c_veteran);
						num += (float)(16 * this.weapon_g36c_veteran.width / this.weapon_g36c_veteran.height);
						break;
					case ITEM.FMG9:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_fmg9.width / this.weapon_fmg9.height), 16f), this.weapon_fmg9);
						num += (float)(16 * this.weapon_fmg9.width / this.weapon_fmg9.height);
						break;
					case ITEM.SAIGA:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_saiga.width / this.weapon_saiga.height), 16f), this.weapon_saiga);
						num += (float)(16 * this.weapon_saiga.width / this.weapon_saiga.height);
						break;
					case ITEM.FLAMETHROWER:
						GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_flamethrower.width / this.weapon_flamethrower.height), 16f), this.weapon_flamethrower);
						num += (float)(16 * this.weapon_flamethrower.width / this.weapon_flamethrower.height);
						break;
					default:
						switch (item)
						{
						case ITEM.AK47_SNOW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_ak47_snow.width / this.weapon_ak47_snow.height), 16f), this.weapon_ak47_snow);
							num += (float)(16 * this.weapon_ak47_snow.width / this.weapon_ak47_snow.height);
							break;
						case ITEM.P90_SNOW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_p90_snow.width / this.weapon_p90_snow.height), 16f), this.weapon_p90_snow);
							num += (float)(16 * this.weapon_p90_snow.width / this.weapon_p90_snow.height);
							break;
						case ITEM.SAIGA_SNOW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_saiga_snow.width / this.weapon_saiga_snow.height), 16f), this.weapon_saiga_snow);
							num += (float)(16 * this.weapon_saiga_snow.width / this.weapon_saiga_snow.height);
							break;
						case ITEM.SR25_SNOW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_sr25_snow.width / this.weapon_sr25_snow.height), 16f), this.weapon_sr25_snow);
							num += (float)(16 * this.weapon_sr25_snow.width / this.weapon_sr25_snow.height);
							break;
						case ITEM.USP_SNOW:
							GUI.DrawTexture(new Rect(num, (float)(10 + i * 20), (float)(16 * this.weapon_usp_snow.width / this.weapon_usp_snow.height), 16f), this.weapon_usp_snow);
							num += (float)(16 * this.weapon_usp_snow.width / this.weapon_usp_snow.height);
							break;
						}
						break;
					}
				}
				else
				{
					GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 16f, 16.5f), this.hudmsg, new Rect(0.90234375f, 0.3359375f, 0.0625f, 0.0625f));
					num += 16f;
				}
				num += 4f;
				if (this.d_headshot[i])
				{
					GUI.DrawTextureWithTexCoords(new Rect(num, (float)(10 + i * 20), 40f, 16.5f), this.hudmsg, new Rect(0.00390625f, 0.93359375f, 0.15625f, 0.0625f));
					num += 40f;
					num += 4f;
				}
				GUIManager.DrawColorText(num, (float)(10 + i * 20), this.d_victim[i], 3);
			}
		}
		if (this.killstreak_draw)
		{
			if (this.killstreak_time + 3f > Time.time)
			{
				this.gui_style2.normal.textColor = new Color(0f, 0f, 0f, 1f);
				GUI.Label(new Rect((float)Screen.width / 2f - 20f + 1f, GUIManager.YRES(220f) + 1f, 200f, 32f), "+" + this.killstreak.ToString(), this.gui_style2);
				if (this.killstreak_color == 0)
				{
					this.gui_style2.normal.textColor = new Color(0f, 0f, 1f, 1f);
				}
				else if (this.killstreak_color == 1)
				{
					this.gui_style2.normal.textColor = new Color(1f, 0f, 0f, 1f);
				}
				else if (this.killstreak_color == 2)
				{
					this.gui_style2.normal.textColor = new Color(0f, 1f, 0f, 1f);
				}
				else if (this.killstreak_color == 3)
				{
					this.gui_style2.normal.textColor = new Color(1f, 1f, 0f, 1f);
				}
				GUI.Label(new Rect((float)Screen.width / 2f - 20f, GUIManager.YRES(220f), 200f, 32f), "+" + this.killstreak.ToString(), this.gui_style2);
				return;
			}
			this.killstreak_draw = false;
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000410EC File Offset: 0x0003F2EC
	public void AddDeathMsg(int attackerid, int victimid, int weaponid, int hitbox)
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		for (int i = 0; i < 3; i++)
		{
			this.d_attacker[i] = this.d_attacker[i + 1];
			this.d_victim[i] = this.d_victim[i + 1];
			this.d_weaponid[i] = this.d_weaponid[i + 1];
			this.d_headshot[i] = this.d_headshot[i + 1];
		}
		this.d_weaponid[3] = weaponid;
		if (hitbox == 1)
		{
			this.d_headshot[3] = true;
		}
		else
		{
			this.d_headshot[3] = false;
		}
		if (attackerid != 255 && attackerid != 254 && attackerid != 253 && attackerid != 252)
		{
			this.d_attacker[3] = "^" + BotsController.Instance.Bots[attackerid].Team.ToString() + BotsController.Instance.Bots[attackerid].Name;
		}
		if (victimid != 253 && victimid != 252)
		{
			this.d_victim[3] = "^" + BotsController.Instance.Bots[victimid].Team.ToString() + BotsController.Instance.Bots[victimid].Name;
		}
		if (victimid == 252)
		{
			this.d_victim[3] = "^1" + Lang.GetLabel(557) + " " + Lang.GetLabel(558);
			return;
		}
		if (victimid == 253)
		{
			this.d_victim[3] = "^1" + Lang.GetLabel(557);
			return;
		}
		if (attackerid == 252)
		{
			this.d_attacker[3] = "^1" + Lang.GetLabel(557) + " " + Lang.GetLabel(558);
			return;
		}
		if (attackerid == 253)
		{
			this.d_attacker[3] = "^1" + Lang.GetLabel(557);
			return;
		}
		if (attackerid == 254)
		{
			this.d_attacker[3] = "^1[" + this.cscl.zs_wave.ToString() + "]" + Lang.GetLabel(556);
			return;
		}
		if (attackerid == 255)
		{
			this.d_attacker[3] = "^8WORLD";
			return;
		}
		if (attackerid == this.cscl.myindex && attackerid != victimid)
		{
			this.killstreak++;
			this.killstreak_time = Time.time;
			this.killstreak_draw = true;
			this.killstreak_color = (int)BotsController.Instance.Bots[victimid].Team;
		}
		if (victimid == this.cscl.myindex)
		{
			this.killstreak = 0;
		}
	}

	// Token: 0x04000657 RID: 1623
	private Client cscl;

	// Token: 0x04000658 RID: 1624
	private GameObject Map;

	// Token: 0x04000659 RID: 1625
	private GUIStyle gui_style2;

	// Token: 0x0400065A RID: 1626
	private string[] d_attacker = new string[4];

	// Token: 0x0400065B RID: 1627
	private string[] d_victim = new string[4];

	// Token: 0x0400065C RID: 1628
	private int[] d_weaponid = new int[4];

	// Token: 0x0400065D RID: 1629
	private bool[] d_headshot = new bool[4];

	// Token: 0x0400065E RID: 1630
	private int[] d_flag = new int[4];

	// Token: 0x0400065F RID: 1631
	private float lastupdate;

	// Token: 0x04000660 RID: 1632
	public int killstreak;

	// Token: 0x04000661 RID: 1633
	private bool killstreak_draw;

	// Token: 0x04000662 RID: 1634
	private float killstreak_time;

	// Token: 0x04000663 RID: 1635
	private int killstreak_color;

	// Token: 0x04000664 RID: 1636
	private Texture2D hudmsg;

	// Token: 0x04000665 RID: 1637
	private Texture2D weapon_ntw20;

	// Token: 0x04000666 RID: 1638
	private Texture2D weapon_vss_desert;

	// Token: 0x04000667 RID: 1639
	private Texture2D weapon_tank_light;

	// Token: 0x04000668 RID: 1640
	private Texture2D weapon_tank_medium;

	// Token: 0x04000669 RID: 1641
	private Texture2D weapon_tank_heavy;

	// Token: 0x0400066A RID: 1642
	private Texture2D weapon_minigun;

	// Token: 0x0400066B RID: 1643
	private Texture2D weapon_minefly;

	// Token: 0x0400066C RID: 1644
	private Texture2D weapon_javelin;

	// Token: 0x0400066D RID: 1645
	private Texture2D weapon_zaa12;

	// Token: 0x0400066E RID: 1646
	private Texture2D weapon_zasval;

	// Token: 0x0400066F RID: 1647
	private Texture2D weapon_zfn57;

	// Token: 0x04000670 RID: 1648
	private Texture2D weapon_zkord;

	// Token: 0x04000671 RID: 1649
	private Texture2D weapon_zm249;

	// Token: 0x04000672 RID: 1650
	private Texture2D weapon_zminigun;

	// Token: 0x04000673 RID: 1651
	private Texture2D weapon_zsps12;

	// Token: 0x04000674 RID: 1652
	private Texture2D weapon_mk3;

	// Token: 0x04000675 RID: 1653
	private Texture2D weapon_rkg3;

	// Token: 0x04000676 RID: 1654
	private Texture2D weapon_tube;

	// Token: 0x04000677 RID: 1655
	private Texture2D weapon_bulava;

	// Token: 0x04000678 RID: 1656
	private Texture2D weapon_katana;

	// Token: 0x04000679 RID: 1657
	private Texture2D weapon_crossbow;

	// Token: 0x0400067A RID: 1658
	private Texture2D weapon_mauzer;

	// Token: 0x0400067B RID: 1659
	private Texture2D weapon_qbz95;

	// Token: 0x0400067C RID: 1660
	private Texture2D weapon_mine;

	// Token: 0x0400067D RID: 1661
	private Texture2D weapon_c4;

	// Token: 0x0400067E RID: 1662
	private Texture2D weapon_chopper;

	// Token: 0x0400067F RID: 1663
	private Texture2D weapon_shield;

	// Token: 0x04000680 RID: 1664
	private Texture2D weapon_aksu;

	// Token: 0x04000681 RID: 1665
	private Texture2D weapon_m700;

	// Token: 0x04000682 RID: 1666
	private Texture2D weapon_stechkin;

	// Token: 0x04000683 RID: 1667
	private Texture2D weapon_at_mine;

	// Token: 0x04000684 RID: 1668
	private Texture2D weapon_molotov;

	// Token: 0x04000685 RID: 1669
	private Texture2D weapon_m202;

	// Token: 0x04000686 RID: 1670
	private Texture2D weapon_gg;

	// Token: 0x04000687 RID: 1671
	private Texture2D weapon_dpm;

	// Token: 0x04000688 RID: 1672
	private Texture2D weapon_m1924;

	// Token: 0x04000689 RID: 1673
	private Texture2D weapon_mg42;

	// Token: 0x0400068A RID: 1674
	private Texture2D weapon_sten;

	// Token: 0x0400068B RID: 1675
	private Texture2D weapon_m1a1;

	// Token: 0x0400068C RID: 1676
	private Texture2D weapon_type99;

	// Token: 0x0400068D RID: 1677
	private Texture2D weapon_jeep;

	// Token: 0x0400068E RID: 1678
	private Texture2D weapon_bizon;

	// Token: 0x0400068F RID: 1679
	private Texture2D weapon_pila;

	// Token: 0x04000690 RID: 1680
	private Texture2D weapon_groza;

	// Token: 0x04000691 RID: 1681
	private Texture2D weapon_jackhammer;

	// Token: 0x04000692 RID: 1682
	private Texture2D weapon_tykva;

	// Token: 0x04000693 RID: 1683
	private Texture2D weapon_psg_1;

	// Token: 0x04000694 RID: 1684
	private Texture2D weapon_krytac;

	// Token: 0x04000695 RID: 1685
	private Texture2D weapon_mp5sd;

	// Token: 0x04000696 RID: 1686
	private Texture2D weapon_colts;

	// Token: 0x04000697 RID: 1687
	private Texture2D weapon_jackhammer_lady;

	// Token: 0x04000698 RID: 1688
	private Texture2D weapon_m700_lady;

	// Token: 0x04000699 RID: 1689
	private Texture2D weapon_mg42_lady;

	// Token: 0x0400069A RID: 1690
	private Texture2D weapon_magnum_lady;

	// Token: 0x0400069B RID: 1691
	private Texture2D weapon_scorpion;

	// Token: 0x0400069C RID: 1692
	private Texture2D weapon_g36c_veteran;

	// Token: 0x0400069D RID: 1693
	private Texture2D weapon_snowball;

	// Token: 0x0400069E RID: 1694
	private Texture2D weapon_fmg9;

	// Token: 0x0400069F RID: 1695
	private Texture2D weapon_saiga;

	// Token: 0x040006A0 RID: 1696
	private Texture2D weapon_flamethrower;

	// Token: 0x040006A1 RID: 1697
	private Texture2D weapon_ak47_snow;

	// Token: 0x040006A2 RID: 1698
	private Texture2D weapon_p90_snow;

	// Token: 0x040006A3 RID: 1699
	private Texture2D weapon_saiga_snow;

	// Token: 0x040006A4 RID: 1700
	private Texture2D weapon_sr25_snow;

	// Token: 0x040006A5 RID: 1701
	private Texture2D weapon_usp_snow;
}
