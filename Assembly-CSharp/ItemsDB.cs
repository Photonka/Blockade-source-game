using System;

// Token: 0x0200003A RID: 58
public class ItemsDB
{
	// Token: 0x060001D4 RID: 468 RVA: 0x00025AFE File Offset: 0x00023CFE
	public static bool CheckItem(int ID)
	{
		return ID > 0 && ItemsDB.Items != null && ID < ItemsDB.Items.Length && ItemsDB.Items[ID] != null;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00025B27 File Offset: 0x00023D27
	public static ItemData GetItemData(int id)
	{
		if (!ItemsDB.CheckItem(id))
		{
			return null;
		}
		return ItemsDB.Items[id];
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00025B3C File Offset: 0x00023D3C
	public static void LoadMissingIcons()
	{
		int i = 0;
		int num = ItemsDB.Items.Length;
		while (i < num)
		{
			if (ItemsDB.Items[i] != null && !(ItemsDB.Items[i].icon != null))
			{
				if (ItemsDB.Items[i].Category == 29 || ItemsDB.Items[i].Category == 26 || ItemsDB.Items[i].Category == 25)
				{
					ItemData itemData = ItemsDB.Items[i];
					ITEM itemID = (ITEM)ItemsDB.Items[i].ItemID;
					itemData.icon = ContentLoader.LoadTexture(itemID.ToString() + Lang.current.ToString());
				}
				else
				{
					ItemData itemData2 = ItemsDB.Items[i];
					ITEM itemID = (ITEM)ItemsDB.Items[i].ItemID;
					itemData2.icon = ContentLoader.LoadTexture(itemID.ToString());
				}
			}
			i++;
		}
	}

	// Token: 0x040001A8 RID: 424
	public static ItemData[] Items;
}
