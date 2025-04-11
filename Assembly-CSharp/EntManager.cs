using System;
using AssemblyCSharp;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class EntManager : MonoBehaviour
{
	// Token: 0x060001DA RID: 474 RVA: 0x00025C4C File Offset: 0x00023E4C
	public static void Clear()
	{
		for (int i = 0; i < 512; i++)
		{
			if (EntManager.Ent[i] != null)
			{
				Object.Destroy(EntManager.Ent[i].go);
				EntManager.Ent[i] = null;
			}
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00025C8C File Offset: 0x00023E8C
	public static CEnt CreateEnt(GameObject pgo, Vector3 position, Vector3 rotation)
	{
		CEnt freeEnt = EntManager.GetFreeEnt();
		if (freeEnt == null)
		{
			return null;
		}
		freeEnt.go = Object.Instantiate<GameObject>(pgo);
		freeEnt.go.transform.position = position;
		freeEnt.go.transform.eulerAngles = rotation;
		return freeEnt;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00025CD3 File Offset: 0x00023ED3
	public static void DeleteEnt(int entid)
	{
		EntManager.Ent[entid] = null;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00025CE0 File Offset: 0x00023EE0
	public static CEnt GetEnt(int uid)
	{
		for (int i = 0; i < 512; i++)
		{
			if (EntManager.Ent[i] != null && EntManager.Ent[i].uid == uid)
			{
				return EntManager.Ent[i];
			}
		}
		return null;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00025D20 File Offset: 0x00023F20
	private static CEnt GetFreeEnt()
	{
		for (int i = 0; i < 512; i++)
		{
			if (EntManager.Ent[i] == null)
			{
				EntManager.Ent[i] = new CEnt();
				EntManager.Ent[i].index = i;
				return EntManager.Ent[i];
			}
		}
		return null;
	}

	// Token: 0x040001A9 RID: 425
	public const int MAX_ENT = 512;

	// Token: 0x040001AA RID: 426
	public static CEnt[] Ent = new CEnt[512];
}
