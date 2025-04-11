using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using Ionic.Zlib;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000120 RID: 288
public class ZipLoader : MonoBehaviour
{
	// Token: 0x06000A33 RID: 2611 RVA: 0x00085934 File Offset: 0x00083B34
	private void Awake()
	{
		ZipLoader.THIS = this;
		this.map = base.GetComponent<Map>();
		BlockSet blockSet = this.map.GetBlockSet();
		this.stoneend = blockSet.GetBlock("Stoneend");
		this.dirt = blockSet.GetBlock("Dirt");
		this.grass = blockSet.GetBlock("Grass");
		this.snow = blockSet.GetBlock("Snow");
		this.sand = blockSet.GetBlock("Sand");
		this.stone = blockSet.GetBlock("Stone");
		this.water = blockSet.GetBlock("!Water");
		this.wood = blockSet.GetBlock("Wood");
		this.wood2 = blockSet.GetBlock("Wood2");
		this.leaf = blockSet.GetBlock("Leaf");
		this.brick = blockSet.GetBlock("Brick");
		this.brick_blue = blockSet.GetBlock("Brick_blue");
		this.brick_red = blockSet.GetBlock("Brick_red");
		this.brick_green = blockSet.GetBlock("Brick_green");
		this.brick_yellow = blockSet.GetBlock("Brick_yellow");
		this.window = blockSet.GetBlock("Window");
		this.box = blockSet.GetBlock("Box");
		this.brick2 = blockSet.GetBlock("Brick2");
		this.stone2 = blockSet.GetBlock("Stone2");
		this.stone3 = blockSet.GetBlock("Stone3");
		this.stone4 = blockSet.GetBlock("Stone4");
		this.tile = blockSet.GetBlock("Tile");
		this.stone5 = blockSet.GetBlock("Stone5");
		this.sand2 = blockSet.GetBlock("Sand2");
		this.stone6 = blockSet.GetBlock("Stone6");
		this.metall1 = blockSet.GetBlock("Metall1");
		this.metall2 = blockSet.GetBlock("Metall2");
		this.stone7 = blockSet.GetBlock("Stone7");
		this.stone8 = blockSet.GetBlock("Stone8");
		this.r_b_blue = blockSet.GetBlock("R_b_blue");
		this.r_b_red = blockSet.GetBlock("R_b_red");
		this.r_b_green = blockSet.GetBlock("R_b_green");
		this.r_b_yellow = blockSet.GetBlock("R_b_yellow");
		this.r_z = blockSet.GetBlock("R_z");
		this.r_c_blue = blockSet.GetBlock("R_c_blue");
		this.r_c_red = blockSet.GetBlock("R_c_red");
		this.r_center = blockSet.GetBlock("R_center");
		this.color1 = blockSet.GetBlock("Color1");
		this.color2 = blockSet.GetBlock("Color2");
		this.color3 = blockSet.GetBlock("Color3");
		this.color4 = blockSet.GetBlock("Color4");
		this.color5 = blockSet.GetBlock("Color5");
		this.color6 = blockSet.GetBlock("Color6");
		this.color7 = blockSet.GetBlock("Color7");
		this.color8 = blockSet.GetBlock("Color8");
		this.color9 = blockSet.GetBlock("Color9");
		this.color10 = blockSet.GetBlock("Color10");
		this.color11 = blockSet.GetBlock("Color11");
		this.color12 = blockSet.GetBlock("Color12");
		this.waterdev = blockSet.GetBlock("Water");
		this.tnt = blockSet.GetBlock("TNT");
		this.danger = blockSet.GetBlock("Danger");
		this.barrel1 = blockSet.GetBlock("Barrel1");
		this.barrel2 = blockSet.GetBlock("Barrel2");
		this.barrel3 = blockSet.GetBlock("Barrel3");
		this.barrel4 = blockSet.GetBlock("Barrel4");
		this.barrel5 = blockSet.GetBlock("Barrel5");
		this.block1 = blockSet.GetBlock("Block1");
		this.box2 = blockSet.GetBlock("Box2");
		this.block2 = blockSet.GetBlock("Block2");
		this.block3 = blockSet.GetBlock("Block3");
		this.block4 = blockSet.GetBlock("Block4");
		this.block5 = blockSet.GetBlock("Block5");
		this.block6 = blockSet.GetBlock("Block6");
		this.block7 = blockSet.GetBlock("Block7");
		this.block8 = blockSet.GetBlock("Block8");
		this.block9 = blockSet.GetBlock("Block9");
		this.block10 = blockSet.GetBlock("Block10");
		this.block11 = blockSet.GetBlock("Block11");
		this.block12 = blockSet.GetBlock("Block12");
		this.block13 = blockSet.GetBlock("Block13");
		this.block14 = blockSet.GetBlock("Block14");
		this.block15 = blockSet.GetBlock("Block15");
		this.block16 = blockSet.GetBlock("Block16");
		this.armored_brick_blue = blockSet.GetBlock("ArmoredBrickBlue");
		this.armored_brick_red = blockSet.GetBlock("ArmoredBrickRed");
		this.armored_brick_green = blockSet.GetBlock("ArmoredBrickGreen");
		this.armored_brick_yellow = blockSet.GetBlock("ArmoredBrickYellow");
		GameObject gameObject = GameObject.Find("GUI");
		this.loadscreen = gameObject.GetComponent<LoadScreen>();
		this.rblock.Clear();
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00085EAF File Offset: 0x000840AF
	public void WebLoadMap(string _mapname)
	{
		this.mapname = _mapname;
		this.mapload = false;
		this.gamemode = PlayerControl.GetGameMode();
		base.StartCoroutine(this.WaitForDownload());
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00085ED7 File Offset: 0x000840D7
	public void WebLoadMapFinish()
	{
		base.StartCoroutine(this.visualize());
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00085EE6 File Offset: 0x000840E6
	private IEnumerator WaitForDownload()
	{
		Debug.LogError("WaitForDownload started");
		int num = 0;
		int.TryParse(this.mapname, out num);
		if (num >= 1000)
		{
			if (CONST.CFG.VERSION == global::Version.RELEASE)
			{
				if (PlayerProfile.network == NETWORK.VK)
				{
					this.f = string.Concat(new object[]
					{
						"http://maps.novalink.kz/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
					this.f2 = string.Concat(new object[]
					{
						"http://31.131.253.108/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
				}
				else if (PlayerProfile.network == NETWORK.OK)
				{
					this.f = string.Concat(new object[]
					{
						"http://5.178.80.226/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
					this.f2 = string.Concat(new object[]
					{
						"http://5.178.80.226/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
				}
				else if (PlayerProfile.network == NETWORK.MM)
				{
					this.f = string.Concat(new object[]
					{
						"http://95.213.130.196:800/mail/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
					this.f2 = string.Concat(new object[]
					{
						"http://95.213.130.196:800/mail/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
				}
				else if (PlayerProfile.network == NETWORK.FB)
				{
					this.f = string.Concat(new object[]
					{
						"http://95.213.130.196:800/fb/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
					this.f2 = string.Concat(new object[]
					{
						"http://95.213.130.196:800/fb/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
				}
				else if (PlayerProfile.network == NETWORK.KG)
				{
					this.f = string.Concat(new object[]
					{
						"http://95.213.130.195/kg/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
					this.f2 = string.Concat(new object[]
					{
						"http://95.213.130.195/kg/maps/",
						this.mapname,
						".map?",
						DateTime.Now.Minute * 60,
						DateTime.Now.Second
					});
				}
			}
			else
			{
				this.f = string.Concat(new object[]
				{
					"http://95.213.130.196/sf/maps/",
					this.mapname,
					".map?",
					DateTime.Now.Minute * 60,
					DateTime.Now.Second
				});
				this.f2 = string.Concat(new object[]
				{
					"http:/95.213.130.196/sf/maps/",
					this.mapname,
					".map?",
					DateTime.Now.Minute * 60,
					DateTime.Now.Second
				});
			}
		}
		else
		{
			this.f = "http://novalink.kz/sf/maps/" + this.mapname + ".map";
			this.f2 = "http://178.89.110.222/sf/maps/" + this.mapname + ".map";
		}
		WWW www = new WWW(this.f);
		yield return www;
		if (www.error == null)
		{
			MonoBehaviour.print("[1]downloaded size: " + www.size.ToString());
		}
		else
		{
			MonoBehaviour.print(string.Concat(new string[]
			{
				"not downloaded: ",
				this.f,
				" (",
				www.error,
				")"
			}));
			www = new WWW(this.f2);
			yield return www;
			if (www.error != null)
			{
				MonoBehaviour.print("not downloaded " + this.f2 + " " + www.error);
				SceneManager.LoadScene(0);
				yield break;
			}
			MonoBehaviour.print("[2]downloaded size: " + www.size.ToString());
		}
		byte[] array = GZipStream.UncompressBuffer(www.bytes);
		MonoBehaviour.print("unpacksize: " + array.Length.ToString());
		int num2 = 0;
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 256; j++)
			{
				for (int k = 0; k < 64; k++)
				{
					int num3 = (int)array[num2];
					num2++;
					if (num3 != 0)
					{
						if (this.gamemode != CONST.CFG.BUILD_MODE && num3 >= 30 && num3 <= 37)
						{
							int num4 = -1;
							if (num3 == 30)
							{
								num4 = 0;
							}
							else if (num3 == 31)
							{
								num4 = 1;
							}
							else if (num3 == 32)
							{
								num4 = 2;
							}
							else if (num3 == 33)
							{
								num4 = 3;
							}
							if (num4 >= 0)
							{
								this.rblock.Add(new CRespawnBlock(num4, i, k, j, 0));
							}
						}
						else if (k == 0)
						{
							this.map.SetBlock(this.stoneend, new Vector3i(i, k, j));
						}
						else
						{
							Vector3i pos = new Vector3i(i, k, j);
							Block block = this.GetBlock(num3);
							if (block == null)
							{
								block = this.brick;
							}
							this.map.SetBlock(block, pos);
						}
					}
				}
			}
		}
		this.mapload = true;
		GM.currExtState = GAME_STATES.GAMELOADMAPCOMPLITE;
		MonoBehaviour.print("rblock = " + this.rblock.Count.ToString());
		yield break;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00085EF5 File Offset: 0x000840F5
	public IEnumerator visualize()
	{
		yield return null;
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Chunk chunk = this.map.GetChunk(new Vector3i(i, k, j));
					if (chunk != null)
					{
						chunk.GetChunkRendererInstance().FastBuild();
						ChunkSunLightComputer.ComputeRays(this.map, i, j);
						chunk.GetChunkRenderer().SetLightDirty();
					}
				}
			}
		}
		GM.currExtState = GAME_STATES.GAMEVISUALIZINGMAPCOMPLITE;
		yield break;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00085F04 File Offset: 0x00084104
	public void SetBlock(int x, int y, int z, int flag)
	{
		Block block = this.GetBlock(flag);
		if (block != null)
		{
			this.map.SetBlock(block, new Vector3i(x, y, z));
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00085F34 File Offset: 0x00084134
	public void SetBlock2(int x, int y, int z, int flag)
	{
		Block block = this.GetBlock(flag);
		if (block != null)
		{
			this.map.SetBlockAndRecompute(new BlockData(block), new Vector3i(x, y, z));
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00085F68 File Offset: 0x00084168
	public Block GetBlock(int flag)
	{
		if (this.gamemode == CONST.CFG.BUILD_MODE && flag == 7)
		{
			return this.waterdev;
		}
		Block result;
		switch (flag)
		{
		case 1:
			result = this.stoneend;
			break;
		case 2:
			result = this.dirt;
			break;
		case 3:
			result = this.grass;
			break;
		case 4:
			result = this.snow;
			break;
		case 5:
			result = this.sand;
			break;
		case 6:
			result = this.stone;
			break;
		case 7:
			result = this.water;
			break;
		case 8:
			result = this.wood;
			break;
		case 9:
			result = this.wood2;
			break;
		case 10:
			result = this.leaf;
			break;
		case 11:
			result = this.brick;
			break;
		case 12:
			result = this.brick_blue;
			break;
		case 13:
			result = this.brick_red;
			break;
		case 14:
			result = this.brick_green;
			break;
		case 15:
			result = this.brick_yellow;
			break;
		case 16:
			result = this.window;
			break;
		case 17:
			result = this.box;
			break;
		case 18:
			result = this.brick2;
			break;
		case 19:
			result = this.stone2;
			break;
		case 20:
			result = this.stone3;
			break;
		case 21:
			result = this.stone4;
			break;
		case 22:
			result = this.tile;
			break;
		case 23:
			result = this.stone5;
			break;
		case 24:
			result = this.sand2;
			break;
		case 25:
			result = this.stone6;
			break;
		case 26:
			result = this.metall1;
			break;
		case 27:
			result = this.metall2;
			break;
		case 28:
			result = this.stone7;
			break;
		case 29:
			result = this.stone8;
			break;
		case 30:
			result = this.r_b_blue;
			break;
		case 31:
			result = this.r_b_red;
			break;
		case 32:
			result = this.r_b_green;
			break;
		case 33:
			result = this.r_b_yellow;
			break;
		case 34:
			result = this.r_z;
			break;
		case 35:
			result = this.r_c_blue;
			break;
		case 36:
			result = this.r_c_red;
			break;
		case 37:
			result = this.r_center;
			break;
		case 38:
			result = this.color1;
			break;
		case 39:
			result = this.color2;
			break;
		case 40:
			result = this.color3;
			break;
		case 41:
			result = this.color4;
			break;
		case 42:
			result = this.color5;
			break;
		case 43:
			result = this.color6;
			break;
		case 44:
			result = this.color7;
			break;
		case 45:
			result = this.color8;
			break;
		case 46:
			result = this.color9;
			break;
		case 47:
			result = this.color10;
			break;
		case 48:
			result = this.color11;
			break;
		case 49:
			result = this.color12;
			break;
		case 50:
			result = this.tnt;
			break;
		case 51:
			result = this.danger;
			break;
		case 52:
			result = this.barrel1;
			break;
		case 53:
			result = this.barrel2;
			break;
		case 54:
			result = this.barrel3;
			break;
		case 55:
			result = this.barrel4;
			break;
		case 56:
			result = this.barrel5;
			break;
		case 57:
			result = this.block1;
			break;
		case 58:
			result = this.box2;
			break;
		case 59:
			result = this.block2;
			break;
		case 60:
			result = this.block3;
			break;
		case 61:
			result = this.block4;
			break;
		case 62:
			result = this.block5;
			break;
		case 63:
			result = this.block6;
			break;
		case 64:
			result = this.block7;
			break;
		case 65:
			result = this.block8;
			break;
		case 66:
			result = this.block9;
			break;
		case 67:
			result = this.block10;
			break;
		case 68:
			result = this.block11;
			break;
		case 69:
			result = this.block12;
			break;
		case 70:
			result = this.block13;
			break;
		case 71:
			result = this.block14;
			break;
		case 72:
			result = this.block15;
			break;
		case 73:
			result = this.block16;
			break;
		case 74:
			result = this.armored_brick_blue;
			break;
		case 75:
			result = this.armored_brick_red;
			break;
		case 76:
			result = this.armored_brick_green;
			break;
		case 77:
			result = this.armored_brick_yellow;
			break;
		default:
			result = null;
			break;
		}
		return result;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00086444 File Offset: 0x00084644
	public static int GetBlock(string name)
	{
		int result = -1;
		if (name == "Stoneend")
		{
			result = 1;
		}
		else if (name == "Dirt")
		{
			result = 2;
		}
		else if (name == "Grass")
		{
			result = 3;
		}
		else if (name == "Snow")
		{
			result = 4;
		}
		else if (name == "Sand")
		{
			result = 5;
		}
		else if (name == "Stone")
		{
			result = 6;
		}
		else if (name == "!Water")
		{
			result = 7;
		}
		else if (name == "Wood")
		{
			result = 8;
		}
		else if (name == "Wood2")
		{
			result = 9;
		}
		else if (name == "Leaf")
		{
			result = 10;
		}
		else if (name == "Brick")
		{
			result = 11;
		}
		else if (name == "Window")
		{
			result = 16;
		}
		else if (name == "Box")
		{
			result = 17;
		}
		else if (name == "Brick2")
		{
			result = 18;
		}
		else if (name == "Stone2")
		{
			result = 19;
		}
		else if (name == "Stone3")
		{
			result = 20;
		}
		else if (name == "Stone4")
		{
			result = 21;
		}
		else if (name == "Tile")
		{
			result = 22;
		}
		else if (name == "Stone5")
		{
			result = 23;
		}
		else if (name == "Sand2")
		{
			result = 24;
		}
		else if (name == "Stone6")
		{
			result = 25;
		}
		else if (name == "Metall1")
		{
			result = 26;
		}
		else if (name == "Metall2")
		{
			result = 27;
		}
		else if (name == "Stone7")
		{
			result = 28;
		}
		else if (name == "Stone8")
		{
			result = 29;
		}
		else if (name == "R_b_blue")
		{
			result = 30;
		}
		else if (name == "R_b_red")
		{
			result = 31;
		}
		else if (name == "R_b_green")
		{
			result = 32;
		}
		else if (name == "R_b_yellow")
		{
			result = 33;
		}
		else if (name == "R_z")
		{
			result = 34;
		}
		else if (name == "R_c_blue")
		{
			result = 35;
		}
		else if (name == "R_c_red")
		{
			result = 36;
		}
		else if (name == "R_center")
		{
			result = 37;
		}
		else if (name == "Color1")
		{
			result = 38;
		}
		else if (name == "Color2")
		{
			result = 39;
		}
		else if (name == "Color3")
		{
			result = 40;
		}
		else if (name == "Color4")
		{
			result = 41;
		}
		else if (name == "Color5")
		{
			result = 42;
		}
		else if (name == "Color6")
		{
			result = 43;
		}
		else if (name == "Color7")
		{
			result = 44;
		}
		else if (name == "Color8")
		{
			result = 45;
		}
		else if (name == "Color9")
		{
			result = 46;
		}
		else if (name == "Color10")
		{
			result = 47;
		}
		else if (name == "Color11")
		{
			result = 48;
		}
		else if (name == "Color12")
		{
			result = 49;
		}
		else if (name == "TNT")
		{
			result = 50;
		}
		else if (name == "Danger")
		{
			result = 51;
		}
		else if (name == "Barrel1")
		{
			result = 52;
		}
		else if (name == "Barrel2")
		{
			result = 53;
		}
		else if (name == "Barrel3")
		{
			result = 54;
		}
		else if (name == "Barrel4")
		{
			result = 55;
		}
		else if (name == "Barrel5")
		{
			result = 56;
		}
		else if (name == "Block1")
		{
			result = 57;
		}
		else if (name == "Box2")
		{
			result = 58;
		}
		else if (name == "Block2")
		{
			result = 59;
		}
		else if (name == "Block3")
		{
			result = 60;
		}
		else if (name == "Block4")
		{
			result = 61;
		}
		else if (name == "Block5")
		{
			result = 62;
		}
		else if (name == "Block6")
		{
			result = 63;
		}
		else if (name == "Block7")
		{
			result = 64;
		}
		else if (name == "Block8")
		{
			result = 65;
		}
		else if (name == "Block9")
		{
			result = 66;
		}
		else if (name == "Block10")
		{
			result = 67;
		}
		else if (name == "Block11")
		{
			result = 68;
		}
		else if (name == "Block12")
		{
			result = 69;
		}
		else if (name == "Block13")
		{
			result = 70;
		}
		else if (name == "Block14")
		{
			result = 71;
		}
		else if (name == "Block15")
		{
			result = 72;
		}
		else if (name == "Block16")
		{
			result = 73;
		}
		else if (name == "ArmoredBrickBlue")
		{
			result = 74;
		}
		else if (name == "ArmoredBrickRed")
		{
			result = 75;
		}
		else if (name == "ArmoredBrickGreen")
		{
			result = 76;
		}
		else if (name == "ArmoredBrickYellow")
		{
			result = 77;
		}
		return result;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00086A30 File Offset: 0x00084C30
	public static int GetBlockType(Block _b)
	{
		if (_b.GetName() == "Stoneend")
		{
			return 4;
		}
		if (_b.GetName() == "Dirt")
		{
			return 1;
		}
		if (_b.GetName() == "Grass")
		{
			return 1;
		}
		if (_b.GetName() == "Snow")
		{
			return 2;
		}
		if (_b.GetName() == "Sand")
		{
			return 1;
		}
		if (_b.GetName() == "Stone")
		{
			return 3;
		}
		if (_b.GetName() == "!Water")
		{
			return 5;
		}
		if (_b.GetName() == "Wood")
		{
			return 6;
		}
		if (_b.GetName() == "Wood2")
		{
			return 6;
		}
		if (_b.GetName() == "Leaf")
		{
			return 1;
		}
		if (_b.GetName() == "Brick")
		{
			return 3;
		}
		if (_b.GetName() == "Brick_red")
		{
			return 3;
		}
		if (_b.GetName() == "Brick_blue")
		{
			return 3;
		}
		if (_b.GetName() == "Brick_green")
		{
			return 3;
		}
		if (_b.GetName() == "Brick_yellow")
		{
			return 3;
		}
		if (_b.GetName() == "Window")
		{
			return 6;
		}
		if (_b.GetName() == "Box")
		{
			return 6;
		}
		if (_b.GetName() == "Brick2")
		{
			return 3;
		}
		if (_b.GetName() == "Stone2")
		{
			return 3;
		}
		if (_b.GetName() == "Stone3")
		{
			return 3;
		}
		if (_b.GetName() == "Stone4")
		{
			return 3;
		}
		if (_b.GetName() == "Tile")
		{
			return 1;
		}
		if (_b.GetName() == "Stone5")
		{
			return 3;
		}
		if (_b.GetName() == "Sand2")
		{
			return 1;
		}
		if (_b.GetName() == "Stone6")
		{
			return 1;
		}
		if (_b.GetName() == "Metall1")
		{
			return 4;
		}
		if (_b.GetName() == "Metall2")
		{
			return 4;
		}
		if (_b.GetName() == "Stone7")
		{
			return 3;
		}
		if (_b.GetName() == "Stone8")
		{
			return 3;
		}
		if (_b.GetName() == "R_b_blue")
		{
			return 1;
		}
		if (_b.GetName() == "R_b_red")
		{
			return 1;
		}
		if (_b.GetName() == "R_b_green")
		{
			return 1;
		}
		if (_b.GetName() == "R_b_yellow")
		{
			return 1;
		}
		if (_b.GetName() == "R_z")
		{
			return 1;
		}
		if (_b.GetName() == "R_c_blue")
		{
			return 1;
		}
		if (_b.GetName() == "R_c_red")
		{
			return 1;
		}
		if (_b.GetName() == "R_center")
		{
			return 1;
		}
		if (_b.GetName() == "Color1")
		{
			return 3;
		}
		if (_b.GetName() == "Color2")
		{
			return 3;
		}
		if (_b.GetName() == "Color3")
		{
			return 3;
		}
		if (_b.GetName() == "Color4")
		{
			return 3;
		}
		if (_b.GetName() == "Color5")
		{
			return 3;
		}
		if (_b.GetName() == "Color6")
		{
			return 3;
		}
		if (_b.GetName() == "Color7")
		{
			return 3;
		}
		if (_b.GetName() == "Color8")
		{
			return 3;
		}
		if (_b.GetName() == "Color9")
		{
			return 3;
		}
		if (_b.GetName() == "Color10")
		{
			return 3;
		}
		if (_b.GetName() == "Color11")
		{
			return 3;
		}
		if (_b.GetName() == "Color12")
		{
			return 3;
		}
		if (_b.GetName() == "TNT")
		{
			return 1;
		}
		if (_b.GetName() == "Danger")
		{
			return 3;
		}
		if (_b.GetName() == "Barrel1")
		{
			return 4;
		}
		if (_b.GetName() == "Barrel2")
		{
			return 4;
		}
		if (_b.GetName() == "Barrel3")
		{
			return 4;
		}
		if (_b.GetName() == "Barrel4")
		{
			return 4;
		}
		if (_b.GetName() == "Barrel5")
		{
			return 4;
		}
		if (_b.GetName() == "Block1")
		{
			return 3;
		}
		if (_b.GetName() == "Box2")
		{
			return 6;
		}
		if (_b.GetName() == "Block2")
		{
			return 3;
		}
		if (_b.GetName() == "Block3")
		{
			return 3;
		}
		if (_b.GetName() == "Block4")
		{
			return 4;
		}
		if (_b.GetName() == "Block5")
		{
			return 4;
		}
		if (_b.GetName() == "Block6")
		{
			return 3;
		}
		if (_b.GetName() == "Block7")
		{
			return 3;
		}
		if (_b.GetName() == "Block8")
		{
			return 3;
		}
		if (_b.GetName() == "Block9")
		{
			return 1;
		}
		if (_b.GetName() == "Block10")
		{
			return 3;
		}
		if (_b.GetName() == "Block11")
		{
			return 3;
		}
		if (_b.GetName() == "Block12")
		{
			return 3;
		}
		if (_b.GetName() == "Block13")
		{
			return 3;
		}
		if (_b.GetName() == "Block14")
		{
			return 3;
		}
		if (_b.GetName() == "Block15")
		{
			return 3;
		}
		if (_b.GetName() == "Block16")
		{
			return 3;
		}
		if (_b.GetName() == "ArmoredBrickBlue")
		{
			return 4;
		}
		if (_b.GetName() == "ArmoredBrickRed")
		{
			return 4;
		}
		if (_b.GetName() == "ArmoredBrickGreen")
		{
			return 4;
		}
		if (_b.GetName() == "ArmoredBrickYellow")
		{
			return 4;
		}
		return 1;
	}

	// Token: 0x04000F30 RID: 3888
	public static ZipLoader THIS;

	// Token: 0x04000F31 RID: 3889
	private Map map;

	// Token: 0x04000F32 RID: 3890
	private Block stoneend;

	// Token: 0x04000F33 RID: 3891
	private Block dirt;

	// Token: 0x04000F34 RID: 3892
	private Block grass;

	// Token: 0x04000F35 RID: 3893
	private Block snow;

	// Token: 0x04000F36 RID: 3894
	private Block sand;

	// Token: 0x04000F37 RID: 3895
	private Block stone;

	// Token: 0x04000F38 RID: 3896
	private Block water;

	// Token: 0x04000F39 RID: 3897
	private Block wood;

	// Token: 0x04000F3A RID: 3898
	private Block wood2;

	// Token: 0x04000F3B RID: 3899
	private Block leaf;

	// Token: 0x04000F3C RID: 3900
	public Block brick;

	// Token: 0x04000F3D RID: 3901
	private Block brick_blue;

	// Token: 0x04000F3E RID: 3902
	private Block brick_red;

	// Token: 0x04000F3F RID: 3903
	private Block brick_green;

	// Token: 0x04000F40 RID: 3904
	private Block brick_yellow;

	// Token: 0x04000F41 RID: 3905
	private Block window;

	// Token: 0x04000F42 RID: 3906
	private Block box;

	// Token: 0x04000F43 RID: 3907
	private Block brick2;

	// Token: 0x04000F44 RID: 3908
	private Block stone2;

	// Token: 0x04000F45 RID: 3909
	private Block stone3;

	// Token: 0x04000F46 RID: 3910
	private Block stone4;

	// Token: 0x04000F47 RID: 3911
	private Block tile;

	// Token: 0x04000F48 RID: 3912
	private Block stone5;

	// Token: 0x04000F49 RID: 3913
	private Block sand2;

	// Token: 0x04000F4A RID: 3914
	private Block stone6;

	// Token: 0x04000F4B RID: 3915
	private Block metall1;

	// Token: 0x04000F4C RID: 3916
	private Block metall2;

	// Token: 0x04000F4D RID: 3917
	private Block stone7;

	// Token: 0x04000F4E RID: 3918
	private Block stone8;

	// Token: 0x04000F4F RID: 3919
	private Block r_b_blue;

	// Token: 0x04000F50 RID: 3920
	private Block r_b_red;

	// Token: 0x04000F51 RID: 3921
	private Block r_b_green;

	// Token: 0x04000F52 RID: 3922
	private Block r_b_yellow;

	// Token: 0x04000F53 RID: 3923
	private Block r_z;

	// Token: 0x04000F54 RID: 3924
	private Block r_c_blue;

	// Token: 0x04000F55 RID: 3925
	private Block r_c_red;

	// Token: 0x04000F56 RID: 3926
	private Block r_center;

	// Token: 0x04000F57 RID: 3927
	private Block color1;

	// Token: 0x04000F58 RID: 3928
	private Block color2;

	// Token: 0x04000F59 RID: 3929
	private Block color3;

	// Token: 0x04000F5A RID: 3930
	private Block color4;

	// Token: 0x04000F5B RID: 3931
	private Block color5;

	// Token: 0x04000F5C RID: 3932
	private Block color6;

	// Token: 0x04000F5D RID: 3933
	private Block color7;

	// Token: 0x04000F5E RID: 3934
	private Block color8;

	// Token: 0x04000F5F RID: 3935
	private Block color9;

	// Token: 0x04000F60 RID: 3936
	private Block color10;

	// Token: 0x04000F61 RID: 3937
	private Block color11;

	// Token: 0x04000F62 RID: 3938
	private Block color12;

	// Token: 0x04000F63 RID: 3939
	private Block waterdev;

	// Token: 0x04000F64 RID: 3940
	private Block tnt;

	// Token: 0x04000F65 RID: 3941
	private Block danger;

	// Token: 0x04000F66 RID: 3942
	private Block barrel1;

	// Token: 0x04000F67 RID: 3943
	private Block barrel2;

	// Token: 0x04000F68 RID: 3944
	private Block barrel3;

	// Token: 0x04000F69 RID: 3945
	private Block barrel4;

	// Token: 0x04000F6A RID: 3946
	private Block barrel5;

	// Token: 0x04000F6B RID: 3947
	private Block block1;

	// Token: 0x04000F6C RID: 3948
	private Block box2;

	// Token: 0x04000F6D RID: 3949
	private Block block2;

	// Token: 0x04000F6E RID: 3950
	private Block block3;

	// Token: 0x04000F6F RID: 3951
	private Block block4;

	// Token: 0x04000F70 RID: 3952
	private Block block5;

	// Token: 0x04000F71 RID: 3953
	private Block block6;

	// Token: 0x04000F72 RID: 3954
	private Block block7;

	// Token: 0x04000F73 RID: 3955
	private Block block8;

	// Token: 0x04000F74 RID: 3956
	private Block block9;

	// Token: 0x04000F75 RID: 3957
	private Block block10;

	// Token: 0x04000F76 RID: 3958
	private Block block11;

	// Token: 0x04000F77 RID: 3959
	private Block block12;

	// Token: 0x04000F78 RID: 3960
	private Block block13;

	// Token: 0x04000F79 RID: 3961
	private Block block14;

	// Token: 0x04000F7A RID: 3962
	private Block block15;

	// Token: 0x04000F7B RID: 3963
	private Block block16;

	// Token: 0x04000F7C RID: 3964
	private Block armored_brick_blue;

	// Token: 0x04000F7D RID: 3965
	private Block armored_brick_red;

	// Token: 0x04000F7E RID: 3966
	private Block armored_brick_green;

	// Token: 0x04000F7F RID: 3967
	private Block armored_brick_yellow;

	// Token: 0x04000F80 RID: 3968
	private string mapname;

	// Token: 0x04000F81 RID: 3969
	public bool mapload;

	// Token: 0x04000F82 RID: 3970
	public int gamemode;

	// Token: 0x04000F83 RID: 3971
	public int mapversion;

	// Token: 0x04000F84 RID: 3972
	private Client cscl;

	// Token: 0x04000F85 RID: 3973
	private PlayerControl cspc;

	// Token: 0x04000F86 RID: 3974
	private LoadScreen loadscreen;

	// Token: 0x04000F87 RID: 3975
	public List<CRespawnBlock> rblock = new List<CRespawnBlock>();

	// Token: 0x04000F88 RID: 3976
	private string f;

	// Token: 0x04000F89 RID: 3977
	private string f2;
}
