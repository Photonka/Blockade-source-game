using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ItemsDrawer : MonoBehaviour
{
	// Token: 0x06000078 RID: 120 RVA: 0x00006835 File Offset: 0x00004A35
	private void Awake()
	{
		ItemsDrawer.THIS = this;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00006840 File Offset: 0x00004A40
	private void myGlobalInit()
	{
		this.goPreviewTrooper = GameObject.Find("Preview/trooper");
		this.goPreview = GameObject.Find("Preview");
		this.goPreviewHelmet = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Helmet");
		this.goPreviewHelmet.SetActive(false);
		this.goPreviewCap = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Cap");
		this.goPreviewCap.SetActive(false);
		this.goPreviewTykva = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/TYKVA");
		this.goPreviewTykva.SetActive(false);
		this.goPreviewKolpak = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/KOLPAK");
		this.goPreviewKolpak.SetActive(false);
		this.goPreviewRoga = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/ROGA");
		this.goPreviewRoga.SetActive(false);
		this.goPreviewMaskBear = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_BEAR");
		this.goPreviewMaskBear.SetActive(false);
		this.goPreviewMaskFox = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_FOX");
		this.goPreviewMaskFox.SetActive(false);
		this.goPreviewMaskRabbit = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/MASK_RABBIT");
		this.goPreviewMaskRabbit.SetActive(false);
		this.goBadge = GameObject.Find("Preview/Bip001/Bip001 Pelvis/Bip001 Spine/Badge");
		foreach (ItemData itemData in ItemsDB.Items)
		{
			if (itemData != null && itemData.preview)
			{
				itemData.preview.SetActive(false);
			}
		}
		base.StartCoroutine(this.Init());
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000069A0 File Offset: 0x00004BA0
	public void DrawItem(int id, ITEMS_THEME theme, Rect r, bool hidecost = false)
	{
		ItemData itemData = ItemsDB.GetItemData(id);
		if (itemData == null)
		{
			GUIManager.DrawText(r, "no data: " + id.ToString(), 16, 4, 8);
			return;
		}
		if (theme == ITEMS_THEME.HALLOWEEN)
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back_HL);
		}
		else if (theme == ITEMS_THEME.LADY)
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back_LADY);
		}
		else if (theme == ITEMS_THEME.NY)
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back_NY);
		}
		else if (theme == ITEMS_THEME.WWII)
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back_WWII);
		}
		else if (theme == ITEMS_THEME.ZOMBIE)
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back_ZM);
		}
		else
		{
			GUI.DrawTexture(r, GUIManager.tex_item_back);
		}
		if (itemData.icon)
		{
			if (itemData.Type == 3 && itemData.Category != 17)
			{
				GUI.DrawTexture(new Rect(r.x + 32f, r.y, 64f, 64f), itemData.icon);
			}
			else
			{
				GUI.DrawTexture(r, itemData.icon);
			}
			if (itemData.Category == 24)
			{
				GUI.DrawTexture(new Rect(r.x + 34f, r.y + 47f, 60f, 15f), GUIManager.tex_item_open);
			}
		}
		Rect r2 = new Rect(r.x + 4f, r.y, 128f, 16f);
		ITEM itemID = (ITEM)itemData.ItemID;
		GUIManager.DrawText2(r2, itemID.ToString(), 10, 3, Color.white);
		if (hidecost)
		{
			if (itemData.Category == 25)
			{
				int num = itemData.DateEnd - (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
				string text;
				if (num >= 86400)
				{
					text = (num / 86400).ToString() + Lang.GetLabel(296);
				}
				else if (num >= 3600)
				{
					text = (num / 86400).ToString() + Lang.GetLabel(297);
				}
				else if (num >= 60)
				{
					text = (num / 60).ToString() + Lang.GetLabel(298);
				}
				else if (num >= 0)
				{
					text = num.ToString() + Lang.GetLabel(299);
				}
				else
				{
					text = "-";
				}
				GUIManager.DrawText(new Rect(r.x, r.y + 44f + 3f, 104f, 16f), text, 20, 5, 8);
				GUI.DrawTexture(new Rect(r.x + 108f, r.y + 44f, 16f, 16f), GUIManager.tex_clock);
			}
			else if (itemData.LastCount > 1 || (itemData.Type == 4 && itemData.Category != 18) || itemData.ItemID == 211)
			{
				GUIManager.DrawText(new Rect(r.x, r.y + 44f + 3f, 120f, 16f), itemData.LastCount.ToString(), 20, 5, 8);
			}
			else if (itemData.Type == 3)
			{
				GUI.DrawTexture(new Rect(r.x + 34f, r.y + 47f, 60f, 15f), GUIManager.tex_item_select);
			}
		}
		else
		{
			if (ItemsDrawer.global_discount > 0)
			{
				GUI.DrawTexture(r, GUIManager.tex_item_back_discount);
			}
			string text2 = itemData.CostGold.ToString();
			if (itemData.Category == 29)
			{
				text2 = "$" + (itemData.CostSocial / 100f).ToString();
				GUIManager.DrawText(new Rect(r.x, r.y + 44f + 3f, 104f, 16f), text2, 20, 5, 8);
				GUI.DrawTexture(new Rect(r.x + 108f, r.y + 44f, 16f, 16f), GUIManager.tex_st);
			}
			else
			{
				GUIManager.DrawText(new Rect(r.x, r.y + 44f + 3f, 104f, 16f), text2, 20, 5, 8);
				GUI.DrawTexture(new Rect(r.x + 108f, r.y + 44f, 16f, 16f), GUIManager.tex_coin);
			}
			if (itemData.Lvl > PlayerProfile.level)
			{
				GUI.DrawTexture(r, GUIManager.tex_item_back_lvl);
				GUIManager.DrawText(r, Lang.GetLabel(341) + itemData.Lvl.ToString(), 16, 4, 3);
			}
		}
		if (GUI.Button(r, "", GUIManager.gs_empty))
		{
			if (itemData.Category == 14 || itemData.Category == 16)
			{
				this.SetSkin(itemData.ItemID);
				if (hidecost)
				{
					this.SkinID = itemData.ItemID;
					base.StartCoroutine(this.set_inv_skin());
				}
			}
			else if (itemData.Category == 15)
			{
				this.ShowVehicle(ItemsDB.GetItemData(itemData.Count));
				this.SetVehicleSkin(itemData.ItemID, true);
				if (hidecost)
				{
					this.VehicleSkinID[itemData.Count] = itemData.ItemID;
					base.StartCoroutine(this.set_tank_skin(itemData.Count, this.VehicleSkinID[itemData.Count]));
				}
			}
			Shop.THIS.OnSelectItem(itemData);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006F34 File Offset: 0x00005134
	public void SetModuleIndex(int id, int _module_index)
	{
		ItemData itemData = ItemsDB.GetItemData(id);
		if (itemData == null)
		{
			return;
		}
		itemData.module_index = _module_index;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00006F54 File Offset: 0x00005154
	public void DrawPlayer()
	{
		GUI.DrawTexture(new Rect(0f, 32f, 180f, GUIManager.YRES(768f) - 180f), GUIManager.tex_half_black);
		GUI.DrawTexture(new Rect(2f, 34f, 176f, 252f), GUIManager.tex_playerback);
		GUI.DrawTexture(new Rect(2f, 32f, 176f, 252f), this.rt, 1);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006FD8 File Offset: 0x000051D8
	private void SetSkin(int skinid)
	{
		if (!this.goPreview)
		{
			return;
		}
		if (this.goPreviewVehicle)
		{
			this.goPreviewVehicle.layer = 2;
		}
		if (this.goPreviewTrooper)
		{
			this.goPreviewTrooper.layer = 10;
		}
		PreviewAnimation component = this.goPreview.GetComponent<PreviewAnimation>();
		if (skinid < 240 || skinid > 300)
		{
			this.goPreviewTrooper.GetComponent<Renderer>().material.mainTexture = SkinManager.GetSkin(0, skinid);
		}
		else
		{
			this.goBadge.GetComponent<Renderer>().material.mainTexture = SkinManager.GetBadge(skinid);
		}
		if (skinid == 32)
		{
			this.goBadge.GetComponent<Renderer>().material.mainTexture = SkinManager.GetBadge(0);
		}
		component.SetState(0);
		if (this.goPreviewWeapon)
		{
			this.goPreviewWeapon.SetActive(false);
		}
		if (this.goPreviewHelmet.activeSelf || this.goPreviewCap.activeSelf)
		{
			if (skinid == 97 || skinid == 99 || skinid == 98)
			{
				this.goPreviewHelmet.SetActive(false);
				this.goPreviewCap.SetActive(true);
			}
			else
			{
				this.goPreviewHelmet.SetActive(true);
				this.goPreviewCap.SetActive(false);
			}
		}
		if (this.goPreviewHelmet.activeSelf)
		{
			this.goPreviewHelmet.GetComponent<Renderer>().material.mainTexture = SkinManager.GetSkin(0, skinid);
		}
		if (this.goPreviewCap.activeSelf)
		{
			this.goPreviewCap.GetComponent<Renderer>().material.mainTexture = SkinManager.GetSkin(0, skinid);
		}
		if (skinid == 311)
		{
			this.goPreviewTrooper.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
			this.goPreviewHelmet.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
			return;
		}
		this.goPreviewTrooper.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
		this.goPreviewHelmet.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000071EC File Offset: 0x000053EC
	private void SetVehicleSkin(int skinid, bool showVehicle)
	{
		if (showVehicle)
		{
			if (this.goPreviewTrooper)
			{
				this.goPreviewTrooper.layer = 2;
			}
			if (this.goPreviewVehicle)
			{
				this.goPreviewVehicle.layer = 10;
				this.goPreviewVehicle.GetComponent<Renderer>().material.mainTexture = SkinManager.GetTankSkin(skinid, 0);
			}
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000724C File Offset: 0x0000544C
	private void Update()
	{
		Vector2 vector;
		vector..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		if (new Rect((float)Screen.width / 2f - 300f, 199f, 200f, 256f).Contains(vector) && Input.GetMouseButtonDown(0))
		{
			this.rotate = Input.mousePosition.x;
		}
		if (Input.GetMouseButton(0) && this.rotate > 0f)
		{
			float num = (this.rotate - Input.mousePosition.x) / this.rotate * 5f;
			if (this.goPreview == null)
			{
				return;
			}
			this.goPreview.transform.eulerAngles = new Vector3(this.goPreview.transform.eulerAngles.x, this.goPreview.transform.eulerAngles.y + num, this.goPreview.transform.eulerAngles.z);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.rotate = 0f;
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00007376 File Offset: 0x00005576
	public void GetInvSkin()
	{
		if (this.SkinID < 0 || this.VehicleSkinID.Count == 0)
		{
			base.StartCoroutine(this.get_inv_skin());
			return;
		}
		this.SetSkin(this.SkinID);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000073A8 File Offset: 0x000055A8
	public IEnumerator get_inv_skin()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"11&id=",
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
			this.VehicleSkinID.Clear();
			string[] array = www.text.Split(new char[]
			{
				'^'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] == ""))
				{
					string[] array2 = array[i].Split(new char[]
					{
						'|'
					});
					int num;
					int.TryParse(array2[0], out num);
					int num2;
					int.TryParse(array2[1], out num2);
					if (num == 0)
					{
						int num3;
						int.TryParse(array2[2], out num3);
						this.SkinID = num2;
						this.SetSkin(num2);
						if (num3 > 0)
						{
							this.SetSkin(num3);
						}
					}
					else
					{
						this.VehicleSkinID[num] = num2;
						this.SetVehicleSkin(num2, false);
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000073B7 File Offset: 0x000055B7
	private IEnumerator set_inv_skin()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"12&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&skin=",
			this.SkinID.ToString(),
			"&wid=0&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			PlayerProfile.skin = this.SkinID;
		}
		yield break;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000073C6 File Offset: 0x000055C6
	private IEnumerator set_tank_skin(int wid, int sid)
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"12&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&skin=",
			sid.ToString(),
			"&wid=",
			wid.ToString(),
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		string error = www.error;
		yield break;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000073DC File Offset: 0x000055DC
	public void EquipWeapon(ItemData weapon)
	{
		if (weapon.ItemID == 211 || (weapon.ItemID > 221 && weapon.ItemID < 227))
		{
			this.EquipHeadAttach(weapon.ItemID, GM.currGUIState != GUIGS.INVENTORY, false);
			return;
		}
		if (weapon.Animation == 0)
		{
			return;
		}
		this.goPreviewTrooper = GameObject.Find("Preview/trooper");
		if (this.goPreviewVehicle)
		{
			this.goPreviewVehicle.layer = 2;
		}
		if (this.goPreviewTrooper.layer == 2)
		{
			this.goPreviewTrooper.layer = 10;
		}
		this.goPreview.GetComponent<PreviewAnimation>().SetState(weapon.Animation);
		if (this.goPreviewWeapon)
		{
			this.goPreviewWeapon.SetActive(false);
		}
		if (weapon.preview)
		{
			weapon.preview.SetActive(true);
			this.goPreviewWeapon = weapon.preview;
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000074CC File Offset: 0x000056CC
	public void EquipHelmet()
	{
		if (this.SkinID == 97 || this.SkinID == 98 || this.SkinID == 99)
		{
			if (this.goPreviewCap.activeSelf)
			{
				this.goPreviewCap.SetActive(false);
				return;
			}
			this.goPreviewCap.SetActive(true);
			this.SetSkin(this.SkinID);
			return;
		}
		else
		{
			if (this.goPreviewHelmet.activeSelf)
			{
				this.goPreviewHelmet.SetActive(false);
				return;
			}
			this.goPreviewHelmet.SetActive(true);
			this.SetSkin(this.SkinID);
			return;
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000755C File Offset: 0x0000575C
	public void EquipHeadAttach(int attachID, bool _visual = false, bool _inv = false)
	{
		if (attachID == 211)
		{
			if (_visual)
			{
				this.goPreviewTykva.SetActive(true);
				this.goPreviewKolpak.SetActive(false);
			}
			else if (_inv)
			{
				this.goPreviewTykva.SetActive(false);
			}
			else if (PlayerProfile.tykva > 0)
			{
				this.goPreviewTykva.SetActive(false);
				base.StartCoroutine(Handler.set_attach(211, 1));
			}
			else
			{
				this.goPreviewTykva.SetActive(true);
				this.goPreviewKolpak.SetActive(false);
				base.StartCoroutine(Handler.set_attach(211, 0));
			}
		}
		if (attachID == 222)
		{
			if (_visual)
			{
				this.goPreviewKolpak.SetActive(true);
				this.goPreviewTykva.SetActive(false);
			}
			else if (_inv)
			{
				this.goPreviewKolpak.SetActive(false);
			}
			else if (PlayerProfile.kolpak > 0)
			{
				this.goPreviewKolpak.SetActive(false);
				base.StartCoroutine(Handler.set_attach(212, 1));
			}
			else
			{
				this.goPreviewKolpak.SetActive(true);
				this.goPreviewTykva.SetActive(false);
				base.StartCoroutine(Handler.set_attach(212, 0));
			}
		}
		if (attachID == 223)
		{
			if (_visual)
			{
				this.goPreviewRoga.SetActive(true);
			}
			else if (_inv)
			{
				this.goPreviewRoga.SetActive(false);
			}
			else if (PlayerProfile.roga > 0)
			{
				this.goPreviewRoga.SetActive(false);
				base.StartCoroutine(Handler.set_attach(241, 1));
			}
			else
			{
				this.goPreviewRoga.SetActive(true);
				base.StartCoroutine(Handler.set_attach(241, 0));
			}
		}
		if (attachID == 224)
		{
			if (_visual)
			{
				this.goPreviewMaskBear.SetActive(true);
				this.goPreviewMaskRabbit.SetActive(false);
				this.goPreviewMaskFox.SetActive(false);
			}
			else if (_inv)
			{
				this.goPreviewMaskBear.SetActive(false);
			}
			else if (PlayerProfile.mask_bear > 0)
			{
				this.goPreviewMaskBear.SetActive(false);
				base.StartCoroutine(Handler.set_attach(231, 1));
			}
			else
			{
				this.goPreviewMaskBear.SetActive(true);
				this.goPreviewMaskRabbit.SetActive(false);
				this.goPreviewMaskFox.SetActive(false);
				base.StartCoroutine(Handler.set_attach(231, 0));
			}
		}
		if (attachID == 225)
		{
			if (_visual)
			{
				this.goPreviewMaskFox.SetActive(true);
				this.goPreviewMaskBear.SetActive(false);
				this.goPreviewMaskRabbit.SetActive(false);
			}
			else if (_inv)
			{
				this.goPreviewMaskFox.SetActive(false);
			}
			else if (PlayerProfile.mask_fox > 0)
			{
				this.goPreviewMaskFox.SetActive(false);
				base.StartCoroutine(Handler.set_attach(232, 1));
			}
			else
			{
				this.goPreviewMaskFox.SetActive(true);
				this.goPreviewMaskBear.SetActive(false);
				this.goPreviewMaskRabbit.SetActive(false);
				base.StartCoroutine(Handler.set_attach(232, 0));
			}
		}
		if (attachID == 226)
		{
			if (_visual)
			{
				this.goPreviewMaskRabbit.SetActive(true);
				this.goPreviewMaskFox.SetActive(false);
				this.goPreviewMaskBear.SetActive(false);
				return;
			}
			if (_inv)
			{
				this.goPreviewMaskRabbit.SetActive(false);
				return;
			}
			if (PlayerProfile.mask_rabbit > 0)
			{
				this.goPreviewMaskRabbit.SetActive(false);
				base.StartCoroutine(Handler.set_attach(233, 1));
				return;
			}
			this.goPreviewMaskRabbit.SetActive(true);
			this.goPreviewMaskFox.SetActive(false);
			this.goPreviewMaskBear.SetActive(false);
			base.StartCoroutine(Handler.set_attach(233, 0));
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000078D8 File Offset: 0x00005AD8
	public void ShowVehicle(ItemData vehicle)
	{
		this.goPreviewCap.SetActive(false);
		this.goPreviewHelmet.SetActive(false);
		this.goPreviewTykva.SetActive(false);
		this.goPreviewKolpak.SetActive(false);
		this.goPreviewRoga.SetActive(false);
		this.goPreviewMaskBear.SetActive(false);
		this.goPreviewMaskFox.SetActive(false);
		this.goPreviewMaskRabbit.SetActive(false);
		this.goPreviewTrooper = GameObject.Find("Preview/trooper");
		if (this.goPreviewTrooper.layer == 10)
		{
			this.goPreviewTrooper.layer = 2;
		}
		if (this.goPreviewWeapon)
		{
			this.goPreviewWeapon.SetActive(false);
		}
		if (this.goPreviewVehicle)
		{
			this.goPreviewVehicle.SetActive(false);
		}
		if (vehicle.preview)
		{
			vehicle.preview.SetActive(true);
			this.goPreviewVehicle = vehicle.preview;
		}
		int skin = 0;
		if (this.VehicleSkinID.ContainsKey(vehicle.ItemID))
		{
			skin = this.VehicleSkinID[vehicle.ItemID];
		}
		else if (vehicle.ItemID == 202)
		{
			skin = 1;
		}
		else if (vehicle.ItemID == 203)
		{
			skin = 2;
		}
		else if (vehicle.ItemID == 204)
		{
			skin = 3;
		}
		if (this.goPreviewVehicle == null)
		{
			return;
		}
		this.goPreviewVehicle.GetComponent<MeshRenderer>().material.mainTexture = SkinManager.GetTankSkin(skin, 0);
		if (this.goPreviewVehicle.layer == 2)
		{
			this.goPreviewVehicle.layer = 10;
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00007A67 File Offset: 0x00005C67
	private IEnumerator Init()
	{
		yield return null;
		GameObject.Find("RenderWeapon/Weapons/TANK_LIGHT").GetComponent<Renderer>().material.mainTexture = SkinManager.GetTankSkin(0, 2);
		GameObject.Find("RenderWeapon/Weapons/TANK_MEDIUM").GetComponent<Renderer>().material.mainTexture = SkinManager.GetTankSkin(1, 2);
		GameObject.Find("RenderWeapon/Weapons/TANK_HEAVY").GetComponent<Renderer>().material.mainTexture = SkinManager.GetTankSkin(2, 2);
		GameObject.Find("RenderWeapon/Weapons/JEEP").GetComponent<Renderer>().material.mainTexture = SkinManager.GetTankSkin(3, 2);
		yield break;
	}

	// Token: 0x04000069 RID: 105
	public static ItemsDrawer THIS;

	// Token: 0x0400006A RID: 106
	public Texture rt;

	// Token: 0x0400006B RID: 107
	private GameObject goPreviewTrooper;

	// Token: 0x0400006C RID: 108
	private GameObject goPreviewWeapon;

	// Token: 0x0400006D RID: 109
	private GameObject goPreview;

	// Token: 0x0400006E RID: 110
	private GameObject goPreviewHelmet;

	// Token: 0x0400006F RID: 111
	private GameObject goPreviewCap;

	// Token: 0x04000070 RID: 112
	private GameObject goPreviewTykva;

	// Token: 0x04000071 RID: 113
	private GameObject goPreviewKolpak;

	// Token: 0x04000072 RID: 114
	private GameObject goPreviewRoga;

	// Token: 0x04000073 RID: 115
	private GameObject goPreviewMaskBear;

	// Token: 0x04000074 RID: 116
	private GameObject goPreviewMaskFox;

	// Token: 0x04000075 RID: 117
	private GameObject goPreviewMaskRabbit;

	// Token: 0x04000076 RID: 118
	private GameObject goPreviewVehicle;

	// Token: 0x04000077 RID: 119
	private GameObject goBadge;

	// Token: 0x04000078 RID: 120
	private int SkinID = -1;

	// Token: 0x04000079 RID: 121
	private Dictionary<int, int> VehicleSkinID = new Dictionary<int, int>();

	// Token: 0x0400007A RID: 122
	public static int global_discount;

	// Token: 0x0400007B RID: 123
	private float rotate;
}
